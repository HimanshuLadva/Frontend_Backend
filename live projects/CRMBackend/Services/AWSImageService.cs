using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using CRMBackend.Data.Interface;
using CRMBackend.Models;
using Microsoft.Extensions.Options;

namespace CRMBackend.Data.Repositories
{
    public enum AWSDirectory
    {
        Photos,
        Documents
    }

    public class AWSImageService : IAWSImageService
    {
        private AmazonS3Config AmazonConfig;
        private BasicAWSCredentials Credentials;
        private AWSConfigurationModel _awsSettings;

        public AWSImageService(IOptions<AWSConfigurationModel> awsSettings)
        {
            _awsSettings = awsSettings.Value;
            AmazonConfig = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };
            Credentials = new BasicAWSCredentials(_awsSettings.AWSAccessKey, _awsSettings.AWSSecretKey);
        }

        public string GetImageBaseUrl()
        {
            return "https://rmbackend.s3.amazonaws.com/";
        }

        public string GetAWSDirectory(AWSDirectory folderName)
        {
            switch (folderName)
            {
                case AWSDirectory.Photos:
                    return "Photos";
                case AWSDirectory.Documents:
                    return "Documents";
                default:
                    return "Others";
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file, AWSDirectory folderName)
        {
            await using var memoryStream = new MemoryStream();
            await file!.CopyToAsync(memoryStream);
            string fileName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));

            // call server
            string AWSFolderName = GetAWSDirectory(folderName);
            var s3Obj = new AWSS3ObjectModel()
            {
                BucketName = $"rmbackend/{AWSFolderName}",
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

        public async Task<bool> DeleteImageAsync(string imageName)
        {
            try
            {
                var client = new Amazon.S3.AmazonS3Client(Credentials, AmazonConfig);

                var transferUtility = new TransferUtility(client);

                var response = await transferUtility.S3Client.DeleteObjectAsync(new DeleteObjectRequest()
                {
                    // Bucket Name
                    BucketName = "rmbackend",
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