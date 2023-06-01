using AutoMapper;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NPOI.HPSF;
using NPOI.SS.Formula.Atp;
using NPOI.Util;
using Org.BouncyCastle.Asn1.Tsp;
using System.IO.Compression;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.Global.Configurations;

namespace WebsiteCMS.DAL.Data.Repositories
{
    /// <summary>
    /// The WCMSTemplate repository.
    /// </summary>
    public class WCMSTemplateRepository : IWCMSTemplateRepository
    {
        private readonly WebsiteCMSDbContext _Context;
        private readonly IBaseRepository _baseRepository;
        private readonly IAWSImageService _imageRepository;
        private static string baseURL;
        /// <summary>
        /// Initializes a new instance of the <see cref="WCMSTemplateRepository"/> class.
        /// </summary>
        /// <param name="dummyDbContext">The dummy db context.</param>
        /// <param name="baseRepository">The base repository.</param>
        /// <param name="imageRepository">The image repository.</param>
        public WCMSTemplateRepository(WebsiteCMSDbContext dummyDbContext, IBaseRepository baseRepository, IAWSImageService imageRepository)
        {
            _Context = dummyDbContext;
            _baseRepository = baseRepository;
            _imageRepository = imageRepository;
            baseURL = _imageRepository.GetImageBaseUrl();
        }

        public List<WCMSTemplatesModel> GetAllTemplate(string hostInfo)
        {
            var tempModel = _Context.tblWCMSTemplates.Select(x => new WCMSTemplatesModel()
            {
                Id = x.Id,
                Name = x.Name,
                StoredPathURL = x.StoredPathURL,
                CoverImageURL = !string.IsNullOrEmpty(x.CoverImageURL) ? hostInfo + x.CoverImageURL : string.Empty
            }).ToList();
            return tempModel;
        }

        public List<string> GetAllPages(int templateId)
        {
            var pages = _Context.tblWCMSTemplatePages.Where(x => x.TemplateId == templateId).Select(x => x.DisplayPageName).ToList();
            return pages;
        }

        public List<KeyValuePair<string, List<WCMSTemplatePageFieldsModel>>> GetAllTemplateFields(int templateId)
        {
            var user = _baseRepository.GetUser();
            var userTemplateInfo = _Context.tblWCMSUserTemplates.Where(x => x.TemplateId == templateId && x.ApplicationUserId == user.Id).Include(x => x.Template).ThenInclude(x => x!.TemplatePages).FirstOrDefault();

            if (userTemplateInfo != null)
            {
                var pages = _Context.tblWCMSTemplatePages.Where(x => x.TemplateId == templateId)
                    .Include(x => x.TemplatePageType)
                    .Include(x => x.TemplatePageFields)!.ThenInclude(x => x.FieldsMaster).ThenInclude(x => x.FieldType)
                    .Include(x => x.TemplatePageFields)!.ThenInclude(x => x.FieldsMaster).ThenInclude(x => x.MasterType)
                    .Include(x => x.Template).ThenInclude(x => x.TemplateFieldsMasterChild)
                    .Include(x => x.TemplatePageFields)!.ThenInclude(x => x.UserTemplateDetails!.Where(x => x.UserTemplateId == userTemplateInfo.Id))!.ThenInclude(x => x.UserTemplateDetailsChilds)
                    .ToList();

                var fields = new List<KeyValuePair<string, List<WCMSTemplatePageFieldsModel>>>();
                foreach (var page in pages)
                {
                    List<WCMSTemplatePageFieldsModel> fieldsNew = new();
                    foreach (var field in page.TemplatePageFields!.Where(x => x.FieldsMaster!.ParentId == 0))
                    {
                        if (field.UserTemplateDetails!.Count != 0)
                        {
                            var Temp = new WCMSTemplatePageFieldsModel()
                            {
                                Id = field.Id,
                                Name = field.FieldsMaster!.Name,
                                TemplatePageId = field.TemplatePageId,
                                FieldMasterId = field.FieldsMasterId,
                                MasterType = field.FieldsMaster.MasterType!.Type,
                                Type = field.FieldsMaster.FieldType!.Type,
                                Value = field.UserTemplateDetails!.FirstOrDefault()!.Value
                            };
                            if (field.FieldsMaster.FieldType.Type == "array")
                            {
                                for (int i = 1; i <= Convert.ToInt32(field.UserTemplateDetails!.SingleOrDefault()!.Value); i++)
                                {
                                    var childs = field.UserTemplateDetails!.SingleOrDefault()!.UserTemplateDetailsChilds!.Where(x => x.Group == i).ToList();

                                    Child childnew = new()
                                    {
                                        childId = i
                                    };
                                    foreach (var child in childs)
                                    {
                                        var childField = new WCMSTemplatePageFieldsModel()
                                        {
                                            Id = child.TemplatePageFieldsId,
                                            Name = child.TemplatePageFields!.FieldsMaster!.Name,
                                            TemplatePageId = child.TemplatePageFields.TemplatePageId,
                                            FieldMasterId = child.TemplatePageFields!.FieldsMasterId,
                                            MasterType = child.TemplatePageFields.FieldsMaster!.MasterType!.Type,
                                            Type = child.TemplatePageFields.FieldsMaster.FieldType!.Type,
                                            Value = child.Value,
                                            ParentId = child.UserTemplateDetailsId
                                        };

                                        if (child.TemplatePageFields.FieldsMaster.FieldType!.Type == "file")
                                        {
                                            childField.Value = baseURL + _imageRepository.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + Path.GetFileName(childField.Value);
                                        }
                                        childnew.ChildFields.Add(childField);
                                    }
                                    Temp.Childs.Add(childnew);
                                }
                                fieldsNew.Add(Temp);
                            }
                            else
                            {
                                Temp.Childs = new List<Child>()
                                {
                                    new Child()
                                    {
                                        childId = 1
                                    }
                                };
                                if (field.FieldsMaster.FieldType.Type == "file")
                                {
                                    Temp.Value = baseURL + _imageRepository.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + Path.GetFileName(Temp.Value);
                                }
                                fieldsNew.Add(Temp);
                            }
                        }
                        else
                        {
                            var Temp = new WCMSTemplatePageFieldsModel()
                            {
                                Id = field.Id,
                                Name = field.FieldsMaster!.Name,
                                TemplatePageId = field.TemplatePageId,
                                FieldMasterId = field.FieldsMasterId,
                                MasterType = field.FieldsMaster.FieldType!.Type,
                                Type = field.FieldsMaster.FieldType.Type,
                                Childs = new List<Child>()
                                    {
                                        new Child()
                                        {
                                            childId= 1
                                        }
                                    }
                            };
                            if (field.FieldsMaster.FieldType.Type == "array")
                            {
                                var child = page.TemplatePageFields!.Where(x => x.FieldsMaster!.ParentId == field.FieldsMasterId).ToList();

                                Temp.Childs = new List<Child>() {
                                    new Child()
                                    {
                                        childId = 1,
                                        ChildFields = child.Select(x => new WCMSTemplatePageFieldsModel()
                                        {
                                            Id = x.Id,
                                            Name = x.FieldsMaster!.Name,
                                            TemplatePageId = x.FieldsMasterId,
                                            FieldMasterId = x.FieldsMasterId,
                                            MasterType = x.FieldsMaster.MasterType!.Type,
                                            Type = x.FieldsMaster.FieldType!.Type,
                                            Childs = new List<Child>()
                                        }).ToList()
                                    }
                                };
                            }
                            fieldsNew.Add(Temp);
                        }
                    }
                    fields.Add(new KeyValuePair<string, List<WCMSTemplatePageFieldsModel>>(page.DisplayPageName!, fieldsNew));
                }
                return fields;
            }
            else
            {
                List<WCMSTemplatePages> pages = _Context.tblWCMSTemplatePages.Where(x => x.TemplateId == templateId)
                    .Include(x => x.TemplatePageType)
                    .Include(x => x.TemplatePageFields)!.ThenInclude(x => x.FieldsMaster).ThenInclude(x => x.FieldType)
                    .Include(x => x.TemplatePageFields)!.ThenInclude(x => x.FieldsMaster).ThenInclude(x => x.MasterType)
                    .Include(x => x.Template).ThenInclude(x => x.TemplateFieldsMasterChild)
                    .ToList();

                var templatefields = new List<KeyValuePair<string, List<WCMSTemplatePageFieldsModel>>>();

                foreach (var page in pages)
                {
                    List<WCMSTemplatePageFieldsModel> fields = page.TemplatePageFields!.Select(x => new WCMSTemplatePageFieldsModel()
                    {
                        Id = x.Id,
                        TemplatePageId = x.TemplatePageId,
                        FieldMasterId = x.FieldsMasterId,
                        MasterType = x.FieldsMaster!.MasterType!.Type,
                        Name = x.FieldsMaster.Name,
                        ParentId = x.FieldsMaster.ParentId,
                        Type = x.FieldsMaster.FieldType!.Type,
                    }).ToList();

                    List<WCMSTemplatePageFieldsModel> fieldsNew = new();

                    foreach (var item in fields.Where(data => data.ParentId == 0))
                    {
                        item.Childs = new List<Child>() {
                            new Child()
                            {
                                childId = 1,
                                ChildFields = fields.Where(data => data.ParentId == item.FieldMasterId).ToList()
                            }
                        };
                        fieldsNew.Add(item);
                    }
                    templatefields.Add(new KeyValuePair<string, List<WCMSTemplatePageFieldsModel>>(page.DisplayPageName!, fieldsNew));
                }
                return templatefields;
            }
        }

        public List<WCMSTemplatePageFieldsModel> GetMetaFields(int templateId)
        {
            var meta = _Context.tblWCMSFieldsMasters.Where(x => x.MasterTypeId == 2).Include(x => x.FieldType).ToList();
            List<WCMSTemplatePageFieldsModel> MetaFieldList = new();
            foreach (var item in meta)
            {
                var metafield = new WCMSTemplatePageFieldsModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Type = item.FieldType!.Type,
                };
                MetaFieldList.Add(metafield);
            }

            return MetaFieldList;
        }

        public async Task<string> StoreImages(IFormFile file)
        {
            await using var memoryStream = new MemoryStream();
            await file!.CopyToAsync(memoryStream);
            string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(file!.FileName));
            string imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.WCMSUploadedImages);
            return baseURL + imageUrl;
        }

        public int SaveTemplateInfo(WCMSTemplatePageFieldsModel[] infoModel, int templateId, string[] files)
        {
            var userTemplate = _Context.tblWCMSUserTemplates.Where(x => x.ApplicationUserId == _baseRepository.GetUserId() && x.TemplateId == templateId)
                .Include(x => x.Template).ThenInclude(x => x.TemplatePages)!.ThenInclude(x => x.TemplatePageFields)!.ThenInclude(x => x.FieldsMaster).ThenInclude(x => x.FieldType)
                .Include(x => x.UserTemplateDetails)!.ThenInclude(x => x.UserTemplateDetailsChilds)
                .FirstOrDefault();

            if (userTemplate != null)
            {
                List<WCMSTemplatePageFields> fileList = new();

                var typeId = _Context.tblWCMSFieldType.Where(x => x.Type == "file").Select(x => x.Id).FirstOrDefault();
                foreach (var page in userTemplate.Template!.TemplatePages!.ToList())
                {
                    fileList.AddRange(page.TemplatePageFields!.Where(x => x.FieldsMaster!.FieldtypeId == typeId).ToList());
                }

                foreach (var file in fileList)
                {
                    if (file.FieldsMaster!.ParentId != 0)
                    {
                        if (file.UserTemplateDetailsChilds != null)
                        {
                            foreach (var item in file.UserTemplateDetailsChilds)
                            {
                                if (item.Value != null)
                                {
                                    var FileName = Path.GetFileName(item.Value);
                                    if (_imageRepository.IsS3FileExists(_imageRepository.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + FileName, "").Result)
                                    {
                                        _ = _imageRepository.DeleteImageAsync(_imageRepository.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + FileName).Result;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (file.UserTemplateDetails != null)
                        {
                            foreach (var item in file.UserTemplateDetails)
                            {
                                if (item.Value != null)
                                {
                                    var FileName = Path.GetFileName(item.Value);
                                    if (_imageRepository.IsS3FileExists(_imageRepository.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + FileName, "").Result)
                                    {
                                        _ = _imageRepository.DeleteImageAsync(_imageRepository.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + FileName).Result;
                                    }
                                }
                            }
                        }
                    }
                }

                var details = _Context.tblWCMSUserTemplateDetails.Where(x => x.UserTemplateId == userTemplate.Id).Include(x => x.UserTemplateDetailsChilds).ToList();
                List<WCMSUserTemplateDetailsChilds> childs = new();
                if (details.Where(x => x.HasChilds == 1)?.Select(x => x?.UserTemplateDetailsChilds).Count() != 0)
                {
                    childs = details.Where(x => x.HasChilds == 1)?.Select(x => x?.UserTemplateDetailsChilds?.AsEnumerable())?.Aggregate((x, y) => x?.Concat(y!))!.ToList()!;
                    _Context.tblWCMSUserTemplateDetailsChilds.RemoveRange(childs);
                }
                _Context.tblWCMSUserTemplateDetails.RemoveRange(details);
                _Context.SaveChanges();
            }
            else
            {
                userTemplate = new WCMSUserTemplates()
                {
                    TemplateId = templateId,
                    ApplicationUserId = _baseRepository.GetUserId(),
                    ColorGroupId = 0,
                    FontGroupId = 0,
                };
                _Context.tblWCMSUserTemplates.Add(userTemplate);
                _Context.SaveChanges();
            }

            foreach (var info in infoModel)
            {
                if (info.Childs.FirstOrDefault()!.ChildFields.Count != 0)
                {
                    var infodata = new WCMSUserTemplateDetails()
                    {
                        UserTemplateId = userTemplate.Id,
                        TemplatePageFieldId = info.Id,
                        Value = info.Childs.Count().ToString(),
                        HasChilds = 1
                    };
                    _Context.tblWCMSUserTemplateDetails.Add(infodata);
                    _Context.SaveChanges();

                    foreach (Child item in info.Childs)
                    {
                        foreach (var val in item.ChildFields)
                        {
                            if (val.Value != null)
                            {
                                var data = new WCMSUserTemplateDetailsChilds()
                                {
                                    UserTemplateDetailsId = infodata.Id,
                                    Group = item.childId,
                                    TemplatePageFieldsId = val.Id,
                                    Value = val.Value
                                };
                                if (val.Type == "file")
                                {
                                    data.Value = "images/" + Path.GetFileName(data.Value);
                                }
                                _Context.tblWCMSUserTemplateDetailsChilds.Add(data);
                            }
                        }
                    }
                }
                else
                {
                    if (info.Value != null)
                    {
                        var data = new WCMSUserTemplateDetails()
                        {
                            UserTemplateId = userTemplate.Id,
                            TemplatePageFieldId = info.Id,
                            Value = info.Value.ToString(),
                            HasChilds = 0
                        };
                        if (info.Type == "file")
                        {
                            data.Value = "images/" + Path.GetFileName(data.Value);
                        }
                        _Context.tblWCMSUserTemplateDetails.Add(data);
                    }
                }
            }
            _Context.SaveChanges();

            foreach (var item in files)
            {
                var fileName = Path.GetFileName(item);
                var DestinationPath = _imageRepository.GetImageBaseUrl() + _imageRepository.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/";
                if (!_imageRepository.IsS3FileExists(Path.Combine(DestinationPath, fileName!), "").Result)
                {
                    _imageRepository.CopyObjectAsync(AWSDirectory.WCMSUploadedImages, AWSDirectory.WCMSUserImages, fileName).Wait();
                }
            }

            return userTemplate.Id;
        }

        public FileContentResult Download(int userTemplateId, string folderName)
        {
            var Usertemplate = _Context.tblWCMSUserTemplates.Find(userTemplateId);
            string date = String.Format("{0}.zip", DateTime.UtcNow.ToString("yyyyMMddHHmmss"));
            string tempName = _Context.tblWCMSTemplates.Where(x => x.Id == Usertemplate!.TemplateId).Select(x => x.Name).First();
            string filename = tempName + "-" + date;

            ZipFile.CreateFromDirectory(Path.Combine(DirectoryConfig.Get(AppDirectory.WCMSTemporary), folderName), Path.Combine(DirectoryConfig.Get(AppDirectory.WCMSZip), filename));
            var result = new FileContentResult(File.ReadAllBytes(Path.Combine(DirectoryConfig.Get(AppDirectory.WCMSZip), filename)), "application/zip")
            {
                FileDownloadName = $"{tempName}.zip"
            };

            DeleteFolderandZip(Path.Combine(DirectoryConfig.Get(AppDirectory.WCMSTemporary), folderName));
            foreach (string file in Directory.GetFiles(DirectoryConfig.Get(AppDirectory.WCMSZip)))
            {
                File.Delete(file);
            }
            return result;
        }

        public void DeleteFolderandZip(string path)
        {
            foreach (string filename in Directory.GetFiles(path))
            {
                File.Delete(filename);
            }
            foreach (string subfolder in Directory.GetDirectories(path))
            {
                DeleteFolderandZip(subfolder);
            }
            Directory.Delete(path);
        }

        public void CopyTemplate(DirectoryInfo directory, string destinationDir)
        {
            foreach (string dir in Directory.GetDirectories(directory.FullName, "*", SearchOption.AllDirectories))
            {
                string dirToCreate = dir.Replace(directory.FullName, destinationDir);
                Directory.CreateDirectory(dirToCreate);
            }

            foreach (string newPath in Directory.GetFiles(directory.FullName, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(directory.FullName, destinationDir), true);
            }
        }

        public string PreviewTemplate(int previewId)
        {
            string foldername = EditTemplate(previewId);
            return "/Preview/" + foldername + "/index.html";
        }

        public string DefaultPreviewTemplate(int templateId)
        {
            var PreviewId = _Context.tblWCMSUserTemplates.Where(x => x.TemplateId == templateId && x.IsPreview == 1).Select(x => x.Id).FirstOrDefault();
            string foldername = EditTemplate(PreviewId);
            return "/Preview/" + foldername + "/index.html";
        }

        public string EditTemplate(int previewId)
        {
            var DestinationPath = DirectoryConfig.Get(AppDirectory.WCMSTemporary);
            var SourcePath = _imageRepository.GetImageBaseUrl() + DirectoryConfig.Get(AppDirectory.WCMSUserImgs);

            var userTemplate = _Context.tblWCMSUserTemplates.Where(x => x.Id == previewId)
                .Include(x => x.Template).ThenInclude(x => x!.TemplatePages)!.ThenInclude(x => x.TemplatePageType)
                .Include(x => x.Template).ThenInclude(x => x!.TemplatePages)!.ThenInclude(x => x.TemplatePageFields)!.ThenInclude(x => x.FieldsMaster).ThenInclude(x => x!.FieldType)
                .Include(x => x.UserTemplateDetails)!.ThenInclude(x => x.UserTemplateDetailsChilds)
                .Include(x => x.UserTemplatesChild)
                .Include(x => x.Template).ThenInclude(x => x.TemplateFieldsMasterChild)
                .FirstOrDefault();

            var categories = _Context.tblWCMSProductCategories.Where(x => x.ApplicationUserId == userTemplate!.ApplicationUserId)
                .Include(x => x.Products)!.ThenInclude(x => x.Fields).ThenInclude(x => x.ProductFields).ThenInclude(x => x.FieldType)
                .ToList();

            string Foldername = string.Format("{0}-{1}", userTemplate!.Template!.Name, DateTime.UtcNow.ToString("yyyyMMddHHmmss"));

            CopyTemplate(new DirectoryInfo(userTemplate!.Template!.StoredPathURL!), Path.Combine(DestinationPath, Foldername));

            List<WCMSTemplatePageFields> files = new();

            var typeId = _Context.tblWCMSFieldType.Where(x => x.Type == "file").Select(x => x.Id).FirstOrDefault();
            foreach (var page in userTemplate.Template.TemplatePages!.ToList())
            {
                files.AddRange(page.TemplatePageFields!.Where(x => x.FieldsMaster!.FieldtypeId == typeId).ToList());
            }

            foreach (var file in files)
            {
                if (file.FieldsMaster!.ParentId != 0)
                {
                    if (file.UserTemplateDetailsChilds != null)
                    {
                        foreach (var item in file.UserTemplateDetailsChilds)
                        {
                            var FileName = Path.GetFileName(item.Value);
                            if (!File.Exists(Path.Combine(DestinationPath, Foldername, item.Value!)))
                            {
                                var byteArr = _imageRepository.GetFileStreamAsync(_imageRepository.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + FileName);
                                File.WriteAllBytes(Path.Combine(DestinationPath, Foldername, item.Value!), byteArr.Result);
                            }
                        }
                    }
                }
                else
                {
                    if (file.UserTemplateDetails != null)
                    {
                        foreach (var item in file.UserTemplateDetails!)
                        {
                            if (item.Value != null)
                            {
                                var FileName = Path.GetFileName(item.Value);
                                if (!File.Exists(Path.Combine(DestinationPath, Foldername, item.Value!)))
                                {
                                    var byteArr = _imageRepository.GetFileStreamAsync(_imageRepository.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + FileName);
                                    File.WriteAllBytes(Path.Combine(DestinationPath, Foldername, item.Value!), byteArr.Result);
                                }
                            }
                        }
                    }
                }
            }
            var GATagAndFacebookPixelFields = _Context.tblWCMSFieldsMasters.Where(x => x.MasterType!.Type == "GATag" || x.MasterType.Type == "FacebookPixel").Include(x => x.MasterType).ToList();

            foreach (var page in userTemplate.Template.TemplatePages!.ToList())
            {
                if (page.TemplatePageType!.Type != "Product")
                {
                    var EditedPage = EditTemplatePages(page.PagePath!, page.TemplatePageFields!.ToList(), Foldername);
                    if (!string.IsNullOrEmpty(userTemplate.GATagId) && !string.IsNullOrEmpty(userTemplate.FacebookPixelId))
                    {
                        EditedPage = GATagAndFacebookPixel(EditedPage!, GATagAndFacebookPixelFields, userTemplate);
                    }
                    else
                    {
                        EditedPage = RegexReplace(EditedPage!, "GATagHeader", "");
                        EditedPage = RegexReplace(EditedPage, "GATagBody", "");
                        EditedPage = RegexReplace(EditedPage, "FacebookPixel", "");
                    }
                    if (EditedPage != null)
                    {
                        File.WriteAllText(Path.Combine(DestinationPath, Foldername, page.PagePath!), EditedPage);
                    }
                }
            }

            if (userTemplate.ColorGroupId != 0 && userTemplate.UserTemplatesChild != null)
            {
                ColorsAndFonts(userTemplate, Foldername);
            }
            if (categories.Count > 0 && userTemplate.Template.TemplatePages!.Any(x => x.TemplatePageType.Type.ToLower() == "header"))
            {
                EditProductPage(categories.ToList(), Foldername, userTemplate, GATagAndFacebookPixelFields);
            }

            foreach (var page in userTemplate.Template.TemplatePages!.Where(x => x.TemplatePageType!.Type == "Normal").Select(x => x.PagePath).ToList())
            {
                if (File.Exists(Path.Combine(DestinationPath, Foldername, "header.html")))
                {
                    var header = File.ReadAllText(Path.Combine(DestinationPath, Foldername, "header.html"));
                    var readfile = File.ReadAllText(Path.Combine(DestinationPath, Foldername, page));
                    readfile = Regex.Replace(readfile, "{{Header}}", header);
                    if (File.Exists(Path.Combine(DestinationPath, Foldername, "Footer.html")))
                    {
                        var footer = File.ReadAllText(Path.Combine(DestinationPath, Foldername, "Footer.html"));
                        readfile = Regex.Replace(readfile, "{{Footer}}", footer);
                    }
                    File.WriteAllText(Path.Combine(DestinationPath, Foldername, page), readfile);
                }
            }

            File.Delete(Path.Combine(DestinationPath, Foldername, "header.html"));
            File.Delete(Path.Combine(DestinationPath, Foldername, "Footer.html"));

            return Foldername;
        }

        /// <summary>
        /// Edits the template pages.
        /// <para>
        ///     Gets the <see cref="string "/> page name of the template.
        /// </para>
        /// <para>
        ///     Gets the <see cref="List{T}"/> of <see cref="WCMSTemplatePageFields"/> Fields with values.
        /// </para>
        /// <para>
        ///     Gets the <see cref="string"/> folder name where template is stored.
        /// </para>
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="foldername">The foldername.</param>
        /// <returns>Returns <see cref="string"/> of edited page.</returns>
        public string? EditTemplatePages(string page, List<WCMSTemplatePageFields> fields, string foldername)
        {
            if (!File.Exists(Path.Combine(DirectoryConfig.Get(AppDirectory.WCMSTemporary), foldername + "/" + page)))
            {
                return null;
            }
            var doc = new HtmlDocument();
            doc.Load(Path.Combine(DirectoryConfig.Get(AppDirectory.WCMSTemporary), foldername + "/" + page));

            foreach (var field in fields.Where(x => x.FieldsMaster!.MasterTypeId == 1))
            {
                if (field.FieldsMaster!.FieldType!.Type == "array")
                {
                    if (field.UserTemplateDetails != null)
                    {
                        var parent = doc.DocumentNode.SelectSingleNode("//*[contains(@class,\"" + field.FieldsMaster.Key + "\")]");

                        var childValues = field.UserTemplateDetails.SingleOrDefault()!.UserTemplateDetailsChilds!.ToList();

                        string innnerHtml = "";

                        if (field.UserTemplateDetails.SingleOrDefault()!.UserTemplateDetailsChilds != null)
                        {
                            for (int i = 1; i <= Convert.ToInt32(field.UserTemplateDetails.SingleOrDefault()!.Value); i++)
                            {
                                if (i == 1)
                                {
                                    var childNode = parent.InnerHtml;
                                    foreach (var item in childValues.Where(x => x.Group == i).ToList())
                                    {
                                        childNode = Regex.Replace(childNode, "{{" + item.TemplatePageFields!.FieldsMaster!.Key + "}}", item.Value!.ToString());
                                    }
                                    innnerHtml += childNode;
                                }
                                else
                                {
                                    var childNode = parent.InnerHtml;

                                    foreach (var item in childValues.Where(x => x.Group == i).ToList())
                                    {
                                        if (item.TemplatePageFields!.FieldsMaster!.Selector != null && item.TemplatePageFields.FieldsMaster.NewSelector != null)
                                        {
                                            childNode = Regex.Replace(childNode, item.TemplatePageFields.FieldsMaster.Selector, item.TemplatePageFields.FieldsMaster.NewSelector);
                                        }
                                        childNode = Regex.Replace(childNode, "{{" + item.TemplatePageFields.FieldsMaster.Key + "}}", item.Value!.ToString());
                                    }
                                    innnerHtml += childNode;
                                }
                            }
                            doc.DocumentNode.SelectSingleNode("//*[contains(@class,\"" + field.FieldsMaster.Key + "\")]").InnerHtml = innnerHtml;
                        }
                        else
                        {
                            if (field.FieldsMaster.IsOptional == true)
                            {
                                doc.DocumentNode.SelectSingleNode(field.FieldsMaster.Selector).Remove();
                            }
                        }
                    }
                    else
                    {
                        if (field.FieldsMaster.IsOptional == true)
                        {
                            doc.DocumentNode.SelectSingleNode(field.FieldsMaster.Selector).Remove();
                        }
                    }
                }
                else
                {
                    if (field.FieldsMaster.ParentId == 0 && field.FieldsMaster.FieldType.Type != "array")
                    {
                        if (field.UserTemplateDetails != null)
                        {
                            doc.DocumentNode.InnerHtml = Regex.Replace(doc.DocumentNode.InnerHtml, "{{" + field.FieldsMaster.Key + "}}", field.UserTemplateDetails.SingleOrDefault()!.Value!.ToString());
                        }
                        else
                        {
                            if (field.FieldsMaster.IsOptional == true)
                            {
                                doc.DocumentNode.SelectSingleNode("//*[contains(text(),\"{{" + field.FieldsMaster.Key + "}}\") or contains(@src,\"{{" + field.FieldsMaster.Key + "}}\") or contains(@class,\"{{" + field.FieldsMaster.Key + "}}\") or contains(@href,\"{{" + field.FieldsMaster.Key + "}}\") ]").Remove();
                            }
                        }
                    }
                }
            }

            string MetaFields = "";
            foreach (var item in fields.Where(x => x.FieldsMaster!.MasterTypeId == 2))
            {
                if (item.UserTemplateDetails != null)
                {
                    var meta = item.FieldsMaster!.Syntax;
                    if (meta != null)
                    {
                        meta = Regex.Replace(meta, "{{" + item.FieldsMaster.Key + "}}", item.UserTemplateDetails.SingleOrDefault()!.Value!);
                        MetaFields += meta + Environment.NewLine;
                    }
                }
            }
            if (MetaFields == "")
            {
                var EditedPage = Regex.Replace(doc.DocumentNode.InnerHtml, "{{Meta}}", "");
                return EditedPage;
            }
            else
            {
                var EditedPage = Regex.Replace(doc.DocumentNode.InnerHtml, "{{Meta}}", MetaFields);
                return EditedPage;
            }
        }

        /// <summary>
        /// Colors the and fonts.
        /// <para>
        ///     Gets the <see cref="WCMSUserTemplates"/> info which is related between user and template.
        /// </para>
        /// <para>
        ///     Gets the <see cref="string"/> folder name where template stored.
        /// </para>
        /// </summary>
        /// <param name="userTemplates">The user templates.</param>
        /// <param name="foldername">The foldername.</param>
        public void ColorsAndFonts(WCMSUserTemplates userTemplates, string foldername)
        {
            var doc = new HtmlDocument();
            doc.Load(Path.Combine(DirectoryConfig.Get(AppDirectory.WCMSTemporary), foldername + "/css/" + "color-variation.css"));

            var imports = "";
            var variables = ":root {\n";

            var colors = userTemplates.Template!.TemplateFieldsMasterChild!.Where(x => x.Group == userTemplates.ColorGroupId && x.MasterType!.Type == "Color").ToList();
            var fonts = userTemplates.Template!.TemplateFieldsMasterChild!.Where(x => x.Group == userTemplates.ColorGroupId && x.MasterType!.Type == "Font").ToList();
            foreach (var color in colors.Select(x => x.FieldsMaster).ToList())
            {
                var syntax = color!.Syntax;
                syntax = Regex.Replace(syntax!, "{{" + color.Key + "}}", colors.Where(x => x.FieldsMasterId == color.Id).SingleOrDefault()!.Value);
                variables += syntax + Environment.NewLine;
            }
            foreach (var font in fonts.Select(x => x.FieldsMaster).ToList())
            {
                var syntax = font!.Syntax;
                syntax = Regex.Replace(syntax!, "{{" + font.Key + "}}", font.Name);
                variables += syntax + Environment.NewLine;
                imports += fonts.Where(x => x.FieldsMasterId == font.Id).SingleOrDefault()!.Value + Environment.NewLine;
            }
            imports += Environment.NewLine;
            imports += variables + Environment.NewLine + "}";
            File.WriteAllText(Path.Combine(DirectoryConfig.Get(AppDirectory.WCMSTemporary), foldername + "/css/color-variation.css"), imports);
        }

        /// <summary>
        /// GS the a tag and facebook pixel.
        /// <para>
        ///     Gets the <see cref="string"/> html page in string formate.
        /// </para>
        /// <para>
        ///     Gets the <see cref="List{T}"/> of <see cref="WCMSFieldsMaster"/> filed with values.
        /// </para>
        /// <para>
        ///     Gets the <see cref="WCMSUserTemplates"/> Id which is related between user and template.
        /// </para>
        /// </summary>
        /// <param name="editedPage">The edited page.</param>
        /// <param name="GATagAndFacebookPixelFields">The GA Tag and Facebook Pixel fields.</param>
        /// <param name="userTemplate">The user template.</param>
        /// <returns>A string.</returns>
        public string GATagAndFacebookPixel(string editedPage, List<WCMSFieldsMaster> GATagAndFacebookPixelFields, WCMSUserTemplates userTemplate)
        {
            var GATag = GATagAndFacebookPixelFields.Where(x => x.MasterType!.Type == "GATag").ToList();
            var FacebookPixel = GATagAndFacebookPixelFields.Where(x => x.MasterType!.Type == "FacebookPixel").SingleOrDefault();

            var GATagHeader = RegexReplace(GATag.Where(x => x.ParentId == 0).SingleOrDefault()!.Syntax!, GATag.Where(x => x.ParentId == 0).SingleOrDefault()!.Key, userTemplate.GATagId!);
            editedPage = RegexReplace(editedPage, "GATagHeader", string.IsNullOrEmpty(userTemplate.GATagId) ? "" : GATagHeader);

            var GATagBody = RegexReplace(GATag.Where(x => x.ParentId != 0).SingleOrDefault()!.Syntax!, GATag.Where(x => x.ParentId != 0).SingleOrDefault()!.Key, userTemplate.GATagId!);
            editedPage = RegexReplace(editedPage, "GATagBody", string.IsNullOrEmpty(userTemplate.GATagId) ? "" : GATagBody);

            var FacebookPixelHeader = RegexReplace(FacebookPixel!.Syntax!, FacebookPixel.Key, userTemplate.FacebookPixelId!);
            editedPage = RegexReplace(editedPage, "FacebookPixel", string.IsNullOrEmpty(userTemplate.FacebookPixelId) ? "" : FacebookPixelHeader);

            return editedPage;
        }

        public void DeleteTemplateInfo()
        {
            WCMSUserTemplates ut = _Context.tblWCMSUserTemplates.Where(x => x.ApplicationUserId == _baseRepository.GetUserId()).First();

            List<WCMSUserTemplateDetails> Infos = _Context.tblWCMSUserTemplateDetails.Where(x => x.UserTemplateId == ut.Id).ToList();

            foreach (WCMSUserTemplateDetails info in Infos)
            {
                _Context.tblWCMSUserTemplateDetails.Remove(info);
            }
            _Context.SaveChanges();
        }

        /// <summary>
        /// Edits the product page.
        /// <para>
        ///     Gets the <see cref="List{T}"/> of <see cref="WCMSProductCategories"/> Categories.
        /// </para>
        /// <para>
        ///     Gets the <see cref="string"/> folder name where template is stored.
        /// </para>
        /// <para>
        ///     Gets the <see cref="WCMSUserTemplates"/> Id which is related between user and template.
        /// </para>
        /// <para>
        ///     Gets the <see cref="List{T}"/> of <see cref="WCMSFieldsMaster"/> fields.
        /// </para>
        /// </summary>
        /// <param name="categories">The categories.</param>
        /// <param name="foldername">The foldername.</param>
        /// <param name="userTemplate">The user template.</param>
        /// <param name="GATagAndFacebookPixelFields">The GA Tag and Facebook Pixel fields.</param>
        public void EditProductPage(List<WCMSProductCategories> categories, string foldername, WCMSUserTemplates userTemplate, List<WCMSFieldsMaster> GATagAndFacebookPixelFields)
        {
            var DestinationPath = DirectoryConfig.Get(AppDirectory.WCMSTemporary);
            var doc = new HtmlDocument();
            string childNodes = "";

            if (categories.Any(x => x.IsUserDefined == true))
            {
                doc.Load(Path.Combine(DestinationPath, foldername, "header.html"));
                var LinksParent = doc.DocumentNode.SelectSingleNode("//*[contains(@class,\"{{Categories}}\")]");
                string innerHtml = LinksParent.ChildNodes[3].OuterHtml;
                childNodes = LinksParent.ChildNodes[1].OuterHtml;
                foreach (var item in categories)
                {
                    var option = Regex.Replace(innerHtml, "{{CategoryId}}", item.Id.ToString());
                    option = Regex.Replace(option, "{{Name}}", item.CategoryName);
                    option = Regex.Replace(option, "{{PageLink}}", $"Category-{item.Id}.html");
                    childNodes += option;
                }
                doc.DocumentNode.SelectSingleNode("//*[contains(@class,\"{{Categories}}\")]").InnerHtml = childNodes;
                File.WriteAllText(Path.Combine(DestinationPath, foldername, "header.html"), doc.DocumentNode.OuterHtml);

                doc.Load(Path.Combine(DestinationPath, foldername, "products.html"));
                doc.DocumentNode.SelectSingleNode("//*[contains(@class,\"{{Categories}}\")]").InnerHtml = childNodes;
                File.WriteAllText(Path.Combine(DestinationPath, foldername, "products.html"), doc.DocumentNode.OuterHtml);
                foreach (var item in categories)
                {
                    string EditedPage = EditCategoryPages(doc, item);
                    if (userTemplate.GATagId != null && userTemplate.FacebookPixelId != null)
                    {
                        EditedPage = GATagAndFacebookPixel(EditedPage!, GATagAndFacebookPixelFields, userTemplate);
                    }
                    else
                    {
                        EditedPage = RegexReplace(EditedPage!, "GATagHeader", "");
                        EditedPage = RegexReplace(EditedPage, "GATagBody", "");
                        EditedPage = RegexReplace(EditedPage, "FacebookPixel", "");
                    }
                    var header = File.ReadAllText(Path.Combine(DestinationPath, foldername, "header.html"));
                    var footer = File.ReadAllText(Path.Combine(DestinationPath, foldername, "Footer.html"));

                    EditedPage = EditedPage.Replace("{{Header}}", header).Replace("{{Footer}}", footer);
                    File.WriteAllText(Path.Combine(DestinationPath, foldername, $"Category-{item.Id}.html"), EditedPage, Encoding.UTF8);
                }
            }
            else
            {
                doc.Load(Path.Combine(DestinationPath, foldername, "header.html"));
                var LinksParent = doc.DocumentNode.SelectSingleNode("//*[contains(@class,\"{{Categories}}\")]");
                string innerHtml = LinksParent.ChildNodes[3].OuterHtml;
                childNodes = LinksParent.ChildNodes[1].OuterHtml;

                foreach (var item in categories[0].Products!)
                {
                    var option = Regex.Replace(innerHtml, "{{CategoryId}}", item.Id.ToString());
                    option = Regex.Replace(option, "{{Name}}", item.Fields.Where(x => x.ProductFields.Name == "ProductHeading").First().FieldValue);
                    option = Regex.Replace(option, "{{PageLink}}", $"ProductDetails-{item.Id}.html");
                    childNodes += option;
                }
                doc.DocumentNode.SelectSingleNode("//*[contains(@class,\"{{Categories}}\")]").InnerHtml = childNodes;

                File.WriteAllText(Path.Combine(DestinationPath, foldername, "header.html"), doc.DocumentNode.OuterHtml);
            }

            var products = categories.Select(x => x.Products!.ToList()).Aggregate((x, y) => x.Concat(y).ToList());
            EditProductDetails(products, foldername, userTemplate, GATagAndFacebookPixelFields);

            File.Delete(Path.Combine(DestinationPath, foldername, "products.html"));
        }

        /// <summary>
        /// Edits the category pages.
        /// <para>
        ///     Gets the <see cref="HtmlDocument"/> html file. 
        /// </para>
        /// <para>
        ///     Gets the <see cref="WCMSProductCategories"/> category.
        /// </para>
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="category">The category.</param>
        /// <returns>Returns <see cref="string"/> html page in string formate.</returns>
        private string EditCategoryPages(HtmlDocument doc, WCMSProductCategories category)
        {
            var CategoryDoc = new HtmlDocument();
            CategoryDoc.LoadHtml(doc.DocumentNode.OuterHtml);
            var parentNode = CategoryDoc.DocumentNode.SelectSingleNode("//*[contains(@class,\"{{Products}}\")]");
            string ParentHtml = parentNode.OuterHtml;
            string children = "";
            if (category.Products != null)
            {

                foreach (var product in category.Products)
                {
                    var child = parentNode.ChildNodes[1].OuterHtml;
                    foreach (var item in product.Fields.Where(x => x.IsBannerField == true))
                    {
                        child = Regex.Replace(child, "{{" + item.ProductFields.Name + "}}", item.FieldValue);
                    }
                    child = Regex.Replace(child, "{{ProductLink}}", $"ProductDetails-{product.Id}.html");
                    children += child;
                }
            }

            CategoryDoc.DocumentNode.SelectSingleNode("//*[contains(@class,\"{{Products}}\")]").InnerHtml = children;

            return CategoryDoc.DocumentNode.OuterHtml;
        }

        /// <summary>
        /// Edits the product details.
        ///<para>
        ///    Gets the <see cref="List{T}"/> of <see cref="WCMSCategoryWiseProducts"/> products.
        ///</para>
        ///<para>
        ///    Gets the <see cref="string"/> folder name where template is stored.
        ///</para>
        ///<para>
        ///    Gets the <see cref="WCMSUserTemplates"/> Id which is related between user and template.
        ///</para>
        ///<para>
        ///    Gets the <see cref="List{T}"/> of <see cref="WCMSFieldsMaster"/> fields.
        ///</para>
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="foldername">The foldername.</param>
        /// <param name="userTemplate">The user template.</param>
        /// <param name="GATagAndFacebookPixelFields">The g a tag and facebook pixel fields.</param>
        private void EditProductDetails(List<WCMSCategoryWiseProducts> products, string foldername, WCMSUserTemplates userTemplate, List<WCMSFieldsMaster> GATagAndFacebookPixelFields)
        {
            var DestinationPath = DirectoryConfig.Get(AppDirectory.WCMSTemporary);
            foreach (var product in products)
            {
                foreach (var item in product.Fields.Where(x => x.ProductFields.FieldType.Type.ToLower() == "file"))
                {
                    var FileName = Path.GetFileName(item.FieldValue);
                    if (!File.Exists(Path.Combine(DestinationPath, foldername, item.FieldValue!)))
                    {
                        var byteArr = _imageRepository.GetFileStreamAsync(_imageRepository.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + FileName);
                        File.WriteAllBytes(Path.Combine(DestinationPath, foldername, item.FieldValue!), byteArr.Result);
                    }
                }
                var HtmlDoc = new HtmlDocument();
                HtmlDoc.Load("Resources/WCMS/Temporary/" + foldername + "/" + "ProductDetails.html");
                var parentNode = HtmlDoc.DocumentNode.SelectSingleNode("//*[contains(@class,\"{{Products}}\")]");
                string ParentHtml = parentNode.OuterHtml;
                string children = ParentHtml;
                foreach (var field in product.Fields)
                {
                    children = children.Replace("{{" + field.ProductFields.Name + "}}", field.FieldValue);
                }

                HtmlDoc.DocumentNode.SelectSingleNode("//*[contains(@class,\"{{Products}}\")]").InnerHtml = children;
                var header = File.ReadAllText(Path.Combine(DirectoryConfig.Get(AppDirectory.WCMSTemporary), $"{foldername}/header.html"));
                var footer = File.ReadAllText(Path.Combine(DirectoryConfig.Get(AppDirectory.WCMSTemporary), $"{foldername}/Footer.html"));

                var EditedPage = HtmlDoc.DocumentNode.OuterHtml;
                EditedPage = EditedPage.Replace("{{Header}}", header).Replace("{{Footer}}", footer);
                if (userTemplate.GATagId != null && userTemplate.FacebookPixelId != null)
                {
                    EditedPage = GATagAndFacebookPixel(EditedPage!, GATagAndFacebookPixelFields, userTemplate);
                }
                else
                {
                    EditedPage = RegexReplace(EditedPage!, "GATagHeader", "");
                    EditedPage = RegexReplace(EditedPage, "GATagBody", "");
                    EditedPage = RegexReplace(EditedPage, "FacebookPixel", "");
                }
                File.WriteAllText(Path.Combine(DirectoryConfig.Get(AppDirectory.WCMSTemporary), $"{foldername}/ProductDetails-{product.Id}.html"), EditedPage);
            }
            File.Delete(Path.Combine(DestinationPath, foldername, "ProductDetails.html"));
        }

        public WCMSGlobleSettingsViewModel GetGlobleSettings()
        {
            var Templates = _Context.tblWCMSTemplates.Select(x => new WCMSTemplatesModel()
            {
                Id = x.Id,
                Name = x.Name,
                CoverImageURL = !string.IsNullOrEmpty(x.CoverImageURL) ? baseURL + x.CoverImageURL : string.Empty,
                StoredPathURL = x.StoredPathURL
            }).ToList();
            var adg = _Context.tblWCMSFieldsMasters.Include(x => x.MasterType).Include(x => x.FieldType);
            var GATag = adg.Where(x => x.MasterType!.Type == "GATag" && x.IsUserVisible == true).SingleOrDefault();
            var FacebookPixel = _Context.tblWCMSFieldsMasters.Include(x => x.MasterType).Include(x => x.FieldType).Where(x => x.MasterType!.Type == "FacebookPixel").SingleOrDefault();
            var SocialChannel = _Context.tblWCMSFieldsMasters.Include(x => x.MasterType).Include(x => x.FieldType).Where(x => x.MasterType!.Type == "SocialChannel").SingleOrDefault();
            var Platforms = _Context.tblSocialPlatforms.Select(x => new SocialPlatformsModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            var Globle = new WCMSGlobleSettingsViewModel()
            {
                Templates = Templates,
                GATag = new WCMSFieldsMasterModel()
                {
                    Id = GATag!.Id,
                    MasterType = GATag.MasterType!.Type,
                    FieldType = GATag.FieldType!.Type,
                    Name = GATag.Name
                },
                FacebookPixel = new WCMSFieldsMasterModel()
                {
                    Id = FacebookPixel!.Id,
                    MasterType = FacebookPixel.MasterType!.Type,
                    FieldType = FacebookPixel.FieldType!.Type,
                    Name = FacebookPixel.Name
                },
                SocialChannels = new SocialChannelModel()
                {
                    Id = SocialChannel!.Id,
                    MasterType = SocialChannel.MasterType!.Type,
                    FieldType = SocialChannel.FieldType!.Type,
                    Name = SocialChannel.Name,
                    SocialPlatforms = Platforms
                },
            };

            return Globle;
        }

        public WCMSColorsAndFontsModel GetColorsAndFontsByTemplateId(int templateId)
        {
            var fields = _Context.tblWCMSFieldsMasterChild.Where(x => x.TemplateId == templateId)
                .Include(x => x.FieldsMaster).ThenInclude(x => x.FieldType)
                .Include(x => x.MasterType)
                .ToList();

            if (fields != null)
            {
                var fontsAndColors = new WCMSColorsAndFontsModel();

                var colors = fields.Where(x => x.MasterType!.Type == "Color").ToList();
                var fonts = fields.Where(x => x.MasterType!.Type == "Font").ToList();

                List<Set> colorSet = new();
                List<Set> fontSet = new();

                int i = 1;
                while (colors.Any(x => x.Group == i))
                {
                    Set set = new();

                    var color = colors.Where(x => x.Group == i).Select(x => new WCMSFieldsMasterModel()
                    {
                        Id = x.Id,
                        Name = x.FieldsMaster!.Name,
                        MasterType = x.FieldsMaster.MasterType!.Type,
                        FieldType = x.FieldsMaster.FieldType!.Type,
                        Value = x.Value
                    }).ToList();

                    set.SetId = i;
                    set.SetValues = color;
                    colorSet.Add(set);
                    i++;
                }

                int j = 1;
                while (colors.Any(x => x.Group == j))
                {
                    Set set = new();

                    var font = fonts.Where(x => x.Group == j).Select(x => new WCMSFieldsMasterModel()
                    {
                        Id = x.Id,
                        Name = x.FieldsMaster!.Name,
                        MasterType = x.FieldsMaster.MasterType!.Type,
                        FieldType = x.FieldsMaster.FieldType!.Type,
                        Value = x.Name!
                    }).ToList();

                    set.SetId = j;
                    set.SetValues = font;
                    fontSet.Add(set);
                    j++;
                }

                fontsAndColors.Colors = colorSet;
                fontsAndColors.Fonts = fontSet;
                return fontsAndColors;
            }
            else
            {
                return null;
            }
        }

        public int SaveGeneralSettings(WCMSGlobleSettingsModel globleSettingsModel)
        {
            var userTemplate = _Context.tblWCMSUserTemplates.Where(x => x.TemplateId == globleSettingsModel.TemplateId && x.ApplicationUserId == _baseRepository.GetUserId()).SingleOrDefault();
            if (userTemplate != null)
            {
                userTemplate.TemplateId = globleSettingsModel.TemplateId;
                userTemplate.ColorGroupId = globleSettingsModel.ColorGroupId;
                userTemplate.FontGroupId = globleSettingsModel.FontGroupId;
                userTemplate.GATagId = globleSettingsModel.GATagId;
                userTemplate.FacebookPixelId = globleSettingsModel.facebookPixelId;

                var socials = _Context.tblWCMSUserTemplatesChild.Where(x => x.UserTemplateId == userTemplate.Id).ToList();
                _Context.tblWCMSUserTemplatesChild.RemoveRange(socials);

                foreach (var socialChannel in globleSettingsModel.SocialChannels)
                {
                    var userTemplatesChild = new WCMSUserTemplatesChild()
                    {
                        UserTemplateId = userTemplate.Id,
                        PlatformId = socialChannel.PlateformId,
                        Value = socialChannel.Value
                    };
                    _Context.tblWCMSUserTemplatesChild.Add(userTemplatesChild);
                }
                _Context.SaveChanges();
                return userTemplate.Id;
            }
            else
            {
                var userTemplateNew = new WCMSUserTemplates()
                {
                    TemplateId = globleSettingsModel.TemplateId,
                    ColorGroupId = globleSettingsModel.ColorGroupId,
                    FontGroupId = globleSettingsModel.FontGroupId,
                    GATagId = globleSettingsModel.GATagId,
                    FacebookPixelId = globleSettingsModel.facebookPixelId
                };
                _Context.tblWCMSUserTemplates.Add(userTemplateNew);

                foreach (var socialChannel in globleSettingsModel.SocialChannels)
                {
                    var userTemplatesChild = new WCMSUserTemplatesChild()
                    {
                        UserTemplateId = userTemplate!.Id,
                        PlatformId = socialChannel.PlateformId,
                        Value = socialChannel.Value
                    };
                    _Context.tblWCMSUserTemplatesChild.Add(userTemplatesChild);
                }
                _Context.SaveChanges();
                return userTemplate!.Id;
            }
        }

        /// <summary>
        /// Regexes replace method to replace string with specific pattern.
        /// <para>
        ///     Gets the <see cref="string"/> input string.
        /// </para>
        /// <para>
        ///     Gets the <see cref="string"/> key which will replace by value.
        /// </para>
        /// <para>
        ///     Gets the <see cref="string"/> value
        /// </para>
        /// </summary>
        /// <param name="string">The page.</param>
        /// <param name="pattern">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Returns <see cref="string"/> replaced string</returns>
        public string RegexReplace(string inputString, string pattern, string value)
        {
            return Regex.Replace(inputString, "{{" + pattern + "}}", value);
        }
    }
}