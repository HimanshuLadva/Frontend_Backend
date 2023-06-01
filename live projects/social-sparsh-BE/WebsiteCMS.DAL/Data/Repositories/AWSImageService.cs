using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NPOI.HPSF;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public enum AWSDirectory
    {
        SCRMTemplate,
        SCRMCategory,
        SCRMSubCategory,
        SCRMTag,
        Social,
        SCRMTemplateField,
        SCRMUserMetaData,
        BOTchatbot,
        BOTImageOrFile,
        BOTComponent,
        WCMSTemplate,
        WCMSUploadedImages,
        WCMSUserImages,
        WCMSDefaultImages
    }

    public class AWSImageService : IAWSImageService
    {
        private AmazonS3Config AmazonConfig;
        private GetObjectRequest ObjectRequest;
        private BasicAWSCredentials Credentials;
        private AWSConfigurationModel _awsSettings;

        public AWSImageService(IOptions<AWSConfigurationModel> awsSettings)
        {
            _awsSettings = awsSettings.Value;
            AmazonConfig = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };
            ObjectRequest = new GetObjectRequest
            {
                BucketName = _awsSettings.BucketName,
            };
            Credentials = new BasicAWSCredentials(_awsSettings.AWSAccessKey, _awsSettings.AWSSecretKey);
        }

        public string GetImageBaseUrl()
        {
            return _awsSettings.AWSImageUrl!;
        }

        public string GetAWSDirectory(AWSDirectory folderName)
        {
            switch (folderName)
            {
                case AWSDirectory.SCRMTemplate:
                    return "SCRMTemplate";
                case AWSDirectory.SCRMCategory:
                    return "SCRMCategory";
                case AWSDirectory.SCRMSubCategory:
                    return "SCRMSubCategory";
                case AWSDirectory.SCRMTag:
                    return "SCRMTag";
                case AWSDirectory.Social:
                    return "Social";
                case AWSDirectory.SCRMTemplateField:
                    return "SCRMTemplateField";
                case AWSDirectory.SCRMUserMetaData:
                    return "SCRMUserMetaData";
                case AWSDirectory.BOTchatbot:
                    return "BOTchatbot";
                case AWSDirectory.BOTImageOrFile:
                    return "BOTImageOrFile";
                case AWSDirectory.BOTComponent:
                    return "BOTComponent";
                case AWSDirectory.WCMSTemplate:
                    return "WCMSTemplate";
                case AWSDirectory.WCMSUploadedImages:
                    return "Resources/WCMS/Temporary/UploadedImgs";
                case AWSDirectory.WCMSUserImages:
                    return "WCMS/Images/UserImages";
                case AWSDirectory.WCMSDefaultImages:
                    return "WCMS/Images/DefaultImages";
                default:
                    return "Others";
            }
        }

        public async Task<string> UploadFileAsync(MemoryStream memoryStream, string fileName, AWSDirectory folderName)
        {
            // call server
            string AWSFolderName = GetAWSDirectory(folderName);
            var s3Obj = new AWSS3ObjectModel()
            {
                BucketName = $"social-sparsh/{AWSFolderName}",
                InputStream = memoryStream,
                Name = fileName
            };

            try
            {
                var uploadRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = s3Obj.InputStream,
                    Key = s3Obj.Name,
                    BucketName = s3Obj.BucketName,
                    CannedACL = S3CannedACL.NoACL
                };

                // initialise client
                using var client = new AmazonS3Client(Credentials, AmazonConfig);

                // initialise the transfer/upload tools
                var transferUtility = new TransferUtility(client);

                // initiate the file upload
                await transferUtility.UploadAsync(uploadRequest);

                return Path.Combine(AWSFolderName, fileName).Replace("\\", "/");

            }
            catch (Exception ex)
            {
                return ex.Message!;
            }
        }

        public async Task<bool> IsS3FileExists(string fileName, string versionId = "")
        {
            try
            {
                using (var client = new AmazonS3Client(Credentials, AmazonConfig))
                {
                    ObjectRequest.Key = fileName;

                    using (var getObjectResponse = await client.GetObjectAsync(ObjectRequest))
                    {
                        using (var responseStream = getObjectResponse.ResponseStream)
                        {
                            var stream = new MemoryStream();
                            await responseStream.CopyToAsync(stream);
                            stream.Position = 0;

                            return stream != null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<byte[]> GetFileStreamAsync(string fileName)
        {
            try
            {
                using (var client = new AmazonS3Client(Credentials, AmazonConfig))
                {
                    ObjectRequest.Key = fileName;

                    using (var getObjectResponse = await client.GetObjectAsync(ObjectRequest))
                    {
                        using (var responseStream = getObjectResponse.ResponseStream)
                        {
                            var stream = new MemoryStream();
                            await responseStream.CopyToAsync(stream);
                            stream.Position = 0;
                            byte[] byteArr;

                            using (BinaryReader br = new BinaryReader(stream))
                            {
                                byteArr = br.ReadBytes((int)stream.Length);
                            }
                            return byteArr;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task CopyObjectAsync(AWSDirectory SourceDirectory, AWSDirectory DestinationDirectory, string FileName)
        {
            try
            {
                CopyObjectRequest request = new CopyObjectRequest
                {
                    SourceBucket = $"social-sparsh/{GetAWSDirectory(SourceDirectory)}",
                    SourceKey = FileName,
                    DestinationBucket = $"social-sparsh/{GetAWSDirectory(DestinationDirectory)}",
                    DestinationKey = FileName
                };

                using (var client = new AmazonS3Client(Credentials, AmazonConfig))
                {
                    CopyObjectResponse response = await client.CopyObjectAsync(request);
                }
            }
            catch (Exception)
            {
                throw;            
            }
        }

        public async Task<bool> DeleteImageAsync(string imageName)
        {
            try
            {
                var client = new Amazon.S3.AmazonS3Client(Credentials, AmazonConfig);

                var transferUtility = new TransferUtility(client);

                var response = await transferUtility.S3Client.DeleteObjectAsync(new DeleteObjectRequest()
                {
                    // Bucket Name
                    BucketName = "social-sparsh",
                    // File Name
                    Key = imageName
                });
                if (response != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}