using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing;
using Microsoft.AspNetCore.Routing.Constraints;
using System;
using Org.BouncyCastle.Asn1.Cms;
using NPOI.SS.Formula.Functions;
using NPOI.POIFS.Crypt.Dsig;
using System.Drawing.Imaging;
using WebsiteCMS.Global.Configurations;
using NPOI.Util;
using System.IO;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class SCRMTemplateRepository : SCRMITemplateRepository
    {
        private readonly WebsiteCMSDbContext _context;
        public readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAWSImageService _imageRepository;

        public SCRMTemplateRepository(WebsiteCMSDbContext context, IWebHostEnvironment webHostEnvironment, IAWSImageService imageRepository)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imageRepository = imageRepository;
        }

        public async Task<IPagedList<SCRMTemplateModel>> GetAllTemplateAsync(SCRMRequestParams requestParams)
        {
            var records = new List<SCRMTemplateModel>();
            records = await _context.tblSCRMTemplate
                   .Select(x => new SCRMTemplateModel()
                   {
                       Id = x.Id,
                       Name = x.Name,
                       //CategoryId = x.CategoryId,
                       //Category = x.Category.Name,
                       LanguageId = x.LanguageId,
                       LanguageName = x.Language.Name,
                       ColorId = x.ColorId,
                       ColorName = x.Color.Name,
                       TemplateImageURL = x.IsFree ? x.PremiumTemplatePreviewImageURL : x.PublicTemplatePreviewImageURL,
                       Tags = x.Tags.Select(t => new SCRMTemplateTagModel()
                       {
                           Id = t.Id,
                           TemplateId = t.TemplateId,
                           Template = t.Template.Name,
                           TagId = t.TagId,
                           Tag = t.Tag.Name
                       }).ToList(),
                       SubCategories = x.SubCategory.Select(p => new SCRMTemplateSubCategoryModel()
                       {
                           Id = p.Id,
                           TemplateId = p.TemplateId,
                           Template = p.Template.Name,
                           SubCategoryId = p.SubCategoryId,
                           SubCategory = p.SubCategory.Name,
                           Captions = p.SubCategory.Captions.Select(q => new SCRMCaptionsModel()
                           {
                               Id = q.Id,
                               SCRMSubCategoryId = p.SubCategoryId,
                               Caption = q.Caption
                           }).ToList()
                       }).ToList(),
                       Categories = x.Category.Select(p => new SCRMTemplateCategoryModel()
                       {
                           Id = p.Id,
                           TemplateId = p.TemplateId,
                           Template = p.Template.Name,
                           CategoryId = p.CategoryId,
                           Category = p.Category.Name,
                           Captions = p.Category.Captions.Select(q => new SCRMCaptionsModel()
                           {
                               Id = q.Id,
                               SCRMCategoryID = p.CategoryId,
                               Caption = q.Caption
                           }).ToList()
                       }).ToList(),
                       IsActive = x.IsActive,
                       IsFree = x.IsFree
                   }).ToListAsync();

            if (requestParams.search != null)
                records = await records.Where(x => x.Name.ToLower().Contains(requestParams.search.ToLower())).ToListAsync();

            if (requestParams.isActive == "Active" || requestParams.isActive == "active")
                records = await records.Where(x => x.IsActive == true).ToListAsync();

            requestParams.recordCount = records.Count;

            var data = await SortResult(records, requestParams);
            return data;
        }

        public async Task<SCRMTemplateModel> GetTemplateByIdAsync(int id)
        {
            string premiumFolder = DirectoryConfig.Get(AppDirectory.SCRMLOwQltPreTempImgs) + "/";
            string publicFolder = DirectoryConfig.Get(AppDirectory.SCRMLOwQltPubTempImgs) + "/";

            var record = await _context.tblSCRMTemplate.Where(x => x.Id == id)
                .Select(x => new SCRMTemplateModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    //CategoryId = x.CategoryId,
                    //Category = x.Category.Name,
                    LanguageId = x.LanguageId,
                    LanguageName = x.Language.Name,
                    ColorId = x.ColorId,
                    ColorName = x.Color.Name,
                    TemplateImageURL = x.IsFree ? x.PremiumTemplatePreviewImageURL : x.PublicTemplatePreviewImageURL,
                    Tags = x.Tags.Select(t => new SCRMTemplateTagModel()
                    {
                        Id = t.Id,
                        TemplateId = t.TemplateId,
                        Template = t.Template.Name,
                        TagId = t.TagId,
                        Tag = t.Tag.Name
                    }).ToList(),
                    SubCategories = x.SubCategory.Select(p => new SCRMTemplateSubCategoryModel()
                    {
                        Id = p.Id,
                        TemplateId = p.TemplateId,
                        Template = p.Template.Name,
                        SubCategoryId = p.SubCategoryId,
                        SubCategory = p.SubCategory.Name,
                        Captions = p.SubCategory.Captions.Select(q => new SCRMCaptionsModel()
                        {
                            Id = q.Id,
                            SCRMSubCategoryId = p.SubCategoryId,
                            Caption = q.Caption
                        }).ToList()
                    }).ToList(),
                    Categories = x.Category.Select(p => new SCRMTemplateCategoryModel()
                    {
                        Id = p.Id,
                        TemplateId = p.TemplateId,
                        Template = p.Template.Name,
                        CategoryId = p.CategoryId,
                        Category = p.Category.Name,
                        Captions = p.Category.Captions.Select(q => new SCRMCaptionsModel()
                        {
                            Id = q.Id,
                            SCRMCategoryID = p.CategoryId,
                            Caption = q.Caption
                        }).ToList()
                    }).ToList(),
                    IsActive = x.IsActive,
                    IsFree = x.IsFree
                }).FirstOrDefaultAsync();
            return record!;
        }

        private async Task<SCRMTemplateUrls> UploadImageWithWatermark(IFormFile file)
        {
            string previewUrl = "";
            string imageUrl = string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                using (var bgImage = Image.FromStream(memoryStream))
                {
                    Bitmap bitmap = new(bgImage);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.DrawImage(bgImage, bgImage.Width, bgImage.Height);
                        Image watermarkImage = WaterMark(bgImage.Width, bgImage.Height, 45, -25);
                        for (int i = 0; i < bgImage.Width; i += watermarkImage.Width + 100)
                            for (int j = 0; j < bgImage.Height; j += watermarkImage.Height + 50)
                                g.DrawImage(watermarkImage, i, j);

                        // upload image with watermark
                        string docName = $"{Guid.NewGuid()}.{file.FileName.Split(".").Last()}";
                        await using (MemoryStream memory = new MemoryStream())
                        {
                            bitmap.Save(memory, ImageFormat.Png);
                            imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.SCRMTemplate);
                        }

                        Image image = bitmap;
                        previewUrl = await VaryQualityLevel(image, docName);
                    }
                }
            }

            return new SCRMTemplateUrls()
            {
                regularImageUrl = imageUrl,
                previewImageUrl = previewUrl,
            };
        }

        private async Task<SCRMTemplateUrls> UploadImage(IFormFile file)
        {
            await using var memoryStream1 = new MemoryStream();
            await file!.CopyToAsync(memoryStream1);
            string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));
            string imageUrl = await _imageRepository.UploadFileAsync(memoryStream1, docName, AWSDirectory.SCRMTemplate);

            Image img = null;
            // file to Image
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                img = Image.FromStream(memoryStream);
            }
            string previewUrl = await VaryQualityLevel(img, docName);

            return new SCRMTemplateUrls()
            {
                regularImageUrl = imageUrl,
                previewImageUrl = previewUrl,
            };
        }

        public static bool CheckAspectRatio(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                using (var img = Image.FromStream(memoryStream))
                {
                    if (img.Width == img.Height)
                        return true;
                    else
                        return false;
                };
            };
        }

        public async Task<SCRMTemplateModel> AddTemplateAsync(SCRMTemplateModel model)
        {
            var textFieldTBL = _context.tblSCRMTemplateFieldText;
            var imageFieldTBL = _context.tblSCRMTemplateFieldImage;
            bool isAspectRatioAllow = CheckAspectRatio(model.TemplateImage!);

            if (isAspectRatioAllow)
            {
                var normalImages = await UploadImage(model.TemplateImage!);
                var imagesWithWaterMark = await UploadImageWithWatermark(model.TemplateImage!);

                model.TemplateImageURL = imagesWithWaterMark.previewImageUrl;
                model.PublicTemplateImageURL = imagesWithWaterMark.regularImageUrl;
                model.PublicTemplatePreviewImageURL = imagesWithWaterMark.previewImageUrl;
                model.PremiumTemplateImageURL = normalImages.regularImageUrl;
                model.PremiumTemplatePreviewImageURL = normalImages.previewImageUrl;

                string[] tags = model.TagCollection.Split(',').Select(x => x.Trim()).ToArray();

                var data = new SCRMTemplate
                {
                    Name = model.Name,
                    //CategoryId = model.CategoryId,
                    TemplateImageURL = model.TemplateImageURL!,
                    PublicTemplateImageURL = model.PublicTemplateImageURL!,
                    PublicTemplatePreviewImageURL = model.PublicTemplatePreviewImageURL!,
                    PremiumTemplateImageURL = model.PremiumTemplateImageURL!,
                    PremiumTemplatePreviewImageURL = model.PremiumTemplatePreviewImageURL!,
                    LanguageId = model.LanguageId,
                    ColorId = model.ColorId,
                    Tags = new List<SCRMTemplateTag>(),
                    SubCategory = new List<SCRMTemplateSubCategory>(),
                    Category = new List<SCRMTemplateCategory>()
                };

                foreach (var tag in tags)
                {
                    data.Tags.Add(new SCRMTemplateTag()
                    {
                        TemplateId = data.Id,
                        TagId = Convert.ToInt32(tag)
                    });
                }

                if (!string.IsNullOrEmpty(model.SubCategoryCollection))
                {
                    string[] subCategories = model.SubCategoryCollection.Split(',').Select(x => x.Trim()).ToArray();

                    if (subCategories.Length > 0)
                    {
                        foreach (var subCat in subCategories)
                        {
                            data.SubCategory.Add(new SCRMTemplateSubCategory()
                            {
                                TemplateId = data.Id,
                                SubCategoryId = Convert.ToInt32(subCat)
                            });
                        }
                    }
                }

                if (!string.IsNullOrEmpty(model.CategoryCollection))
                {
                    string[] Categories = model.CategoryCollection.Split(',').Select(x => x.Trim()).ToArray();

                    if (Categories.Length > 0)
                    {
                        foreach (var Cat in Categories)
                        {
                            data.Category.Add(new SCRMTemplateCategory()
                            {
                                TemplateId = data.Id,
                                CategoryId = Convert.ToInt32(Cat)
                            });
                        }
                    }
                }

                _context.tblSCRMTemplate.Add(data);
                await _context.SaveChangesAsync();

                var templateField = await _context.tblSCRMTemplateField.Where(x => x.IsActive == true).ToListAsync();

                double yCorrdinate = 0;
                foreach (var field in templateField)
                {
                    if (field.TemplateFieldTypeId == 1)
                    {
                        var textField = new SCRMTemplateFieldText()
                        {
                            TemplateId = data.Id,
                            TemplateFieldId = field.Id,
                            Y = yCorrdinate
                        };
                        textFieldTBL.Add(textField);
                        yCorrdinate += 25;
                    }
                    else if (field.TemplateFieldTypeId == 2)
                    {
                        var imageField = new SCRMTemplateFieldImage()
                        {
                            TemplateId = data.Id,
                            TemplateFieldId = field.Id
                        };
                        imageFieldTBL.Add(imageField);
                    }
                }
                await _context.SaveChangesAsync();

                var record = await GetTemplateByIdAsync(data.Id);
                return record;
            }
            else
            {
                throw new Exception("Please Enter Valid Image For Template...!");
            }
        }

        public async Task<SCRMTemplateModel> UpdateTemplateAsync(int id, SCRMTemplateModel model)
        {
            var textFieldTBL = _context.tblSCRMTemplateFieldText;
            var imageFieldTBL = _context.tblSCRMTemplateFieldImage;
            var templateTagTBL = _context.tblSCRMTemplateTag;

            if (model.TemplateImage != null)
            {
                var normalImages = await UploadImage(model.TemplateImage!);
                var imagesWithWaterMark = await UploadImageWithWatermark(model.TemplateImage!);
                bool isAspectRatioAllow = CheckAspectRatio(model.TemplateImage!);
                if (isAspectRatioAllow == true)
                {
                    model.TemplateImageURL = normalImages.regularImageUrl;
                    model.PublicTemplateImageURL = imagesWithWaterMark.regularImageUrl;
                    model.PublicTemplatePreviewImageURL = imagesWithWaterMark.previewImageUrl;
                    model.PremiumTemplateImageURL = normalImages.regularImageUrl;
                    model.PremiumTemplatePreviewImageURL = normalImages.previewImageUrl;
                }
                else
                    throw new Exception("Please Enter Valid Image For Template...!");
            }

            string[] tags = model.TagCollection.Split(',').Select(x => x.Trim()).ToArray();

            var data = await _context.tblSCRMTemplate.FindAsync(id);
            if (data != null)
            {
                data.Id = id;
                data.Name = model.Name;
                //data.CategoryId = model.CategoryId;
                data.LanguageId = model.LanguageId != 0 ? model.LanguageId : data.LanguageId;
                data.ColorId = model.ColorId != 0 ? model.ColorId : data.ColorId;
                data.TemplateImageURL = model.IsFree ? model!.PremiumTemplatePreviewImageURL! : model!.PublicTemplatePreviewImageURL!;
                if (data.TemplateImageURL != null)
                {
                    bool isDeletedOrNot = await _imageRepository.DeleteImageAsync(data.TemplateImageURL!);
                    data.PublicTemplateImageURL = model.PublicTemplateImageURL!;
                    data.PublicTemplatePreviewImageURL = model.PublicTemplatePreviewImageURL!;
                    data.PremiumTemplateImageURL = model.PremiumTemplateImageURL!;
                    data.PremiumTemplatePreviewImageURL = model.PremiumTemplatePreviewImageURL!;
                }
                data.Tags = new List<SCRMTemplateTag>();
                data.SubCategory = new List<SCRMTemplateSubCategory>();
                data.Category = new List<SCRMTemplateCategory>();

                var existTags = templateTagTBL.Where(x => x.TemplateId == data.Id);
                if (existTags != null)
                    foreach (var tag in existTags)
                    {
                        templateTagTBL.Remove(tag);
                    }

                foreach (var tag in tags)
                {
                    data.Tags.Add(new SCRMTemplateTag()
                    {
                        TemplateId = data.Id,
                        TagId = Convert.ToInt32(tag)
                    });
                }

                var existSubCategories = _context.tblSCRMTemplateSubCategory.Where(x => x.TemplateId == data.Id);
                if (existSubCategories != null)
                {
                    foreach (var subCat in existSubCategories)
                    {
                        _context.tblSCRMTemplateSubCategory.Remove(subCat);
                    }
                }

                if (!string.IsNullOrEmpty(model.SubCategoryCollection))
                {
                    string[] subCateglries = model.SubCategoryCollection.Split(',').Select(x => x.Trim()).ToArray();

                    foreach (var subCat in subCateglries)
                    {
                        data.SubCategory.Add(new SCRMTemplateSubCategory()
                        {
                            TemplateId = data.Id,
                            SubCategoryId = Convert.ToInt32(subCat)
                        });
                    }
                }

                var existCategories = _context.tblSCRMTemplateCategory.Where(x => x.TemplateId == data.Id);
                if (existCategories != null)
                {
                    foreach (var Cat in existCategories)
                    {
                        _context.tblSCRMTemplateCategory.Remove(Cat);
                    }
                }

                if (!string.IsNullOrEmpty(model.CategoryCollection))
                {
                    string[] Categories = model.CategoryCollection.Split(',').Select(x => x.Trim()).ToArray();

                    foreach (var Cat in Categories)
                    {
                        data.Category.Add(new SCRMTemplateCategory()
                        {
                            TemplateId = data.Id,
                            CategoryId = Convert.ToInt32(Cat)
                        });
                    }
                }

                data.IsActive = model.IsActive == data.IsActive ? data.IsActive : model.IsActive;
                data.IsFree = model.IsFree == data.IsFree ? data.IsFree : model.IsFree;
                data.CreatedDate = data.CreatedDate;
                data.UpdatedDate = DateTime.Now;
                data.IsDeleted = data.IsDeleted;

                _context.tblSCRMTemplate.Update(data);
                await _context.SaveChangesAsync();

                var record = await GetTemplateByIdAsync(data.Id);
                return record;
            }
            return null!;
        }

        public async Task<bool> UpdateTemplateStatusAsync(int id, SCRMUpdateStatusModel model)
        {
            var data = await _context.tblSCRMTemplate.FindAsync(id);
            if (data != null)
            {
                data.IsActive = model.IsActive;
                data.UpdatedDate = DateTime.Now;

                _context.tblSCRMTemplate.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteTemplateAsync(int id)
        {
            var record = _context.tblSCRMTemplate.Where(x => x.Id == id).FirstOrDefault();
            bool isDeleted = await _imageRepository.DeleteImageAsync(record!.TemplateImageURL);
            _context.tblSCRMTemplate.Remove(record!);

            await _context.SaveChangesAsync();
        }

        public static async Task<IPagedList<SCRMTemplateModel>> SortResult(List<SCRMTemplateModel> source, SCRMRequestParams requestParams)
        {
            IOrderedEnumerable<SCRMTemplateModel> data = source.OrderBy(s => s.Name);
            if (requestParams.sortBy != null)
                if (requestParams.orderBy == "Desc" || requestParams.orderBy == "desc")
                    data = source.OrderByDescending(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));
                else
                    data = source.OrderBy(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));

            return await data.ToPagedListAsync(requestParams.pageNumber, requestParams.pageSize);
        }

        public static Bitmap WaterMark(float imageWidth, float imageHeight, int alpha, int rotation)
        {
            //var fontFamily = new PrivateFontCollection();
            //string fontFamilyPath = _webHostEnvironment.WebRootPath + "/Resources/SCRM/Public/FontFamilies";
            //fontFamily.AddFontFile(fontFamilyPath + $"Amaranth-Regular.ttf");
            //Font font = new(fontFamily.Families[0], 40, FontStyle.Bold, GraphicsUnit.Pixel);

            string Text = "© WeyBee";
            Font font = new("Arial", 40, FontStyle.Bold, GraphicsUnit.Pixel);
            string hashColor = "#808080";

            Color color = ColorTranslator.FromHtml(hashColor);
            int r = Convert.ToInt32(color.R);
            int g = Convert.ToInt32(color.G);
            int b = Convert.ToInt32(color.B);

            Bitmap bitmap = new(1, 1);
            Graphics graphics = Graphics.FromImage(bitmap);
            int width = (int)graphics.MeasureString(Text, font).Width;
            int height = (int)graphics.MeasureString(Text, font).Height;

            float newRotation = 0;
            if (rotation < 0)
                newRotation = -rotation;
            double imgWidth = width * Math.Cos(newRotation) + height * Math.Sin(newRotation);
            double imgHeight = width * Math.Sin(90 - newRotation) + height * Math.Cos(newRotation);

            bitmap = new Bitmap(bitmap, new Size((int)imgWidth, (int)(imgHeight / 1.5)));
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.TranslateTransform(0, (int)(imgHeight / 2.2));
            graphics.RotateTransform(rotation);
            graphics.DrawString(Text, font, new SolidBrush(Color.FromArgb(alpha, r, g, b)), 0, 0);
            graphics.Flush();
            graphics.Dispose();
            return bitmap;
        }

        public static float UpdateSizeAndLocation(float userGiven, float reference, float predictor)
        {
            return (predictor * userGiven) / reference;
        }

        private async Task<string> VaryQualityLevel(Image file, string fileName)
        {
            // High quality to Law quality
            Bitmap bmp1 = new Bitmap(file);
            string imageUrl = string.Empty;

            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

            var lawQualityImage = "preview" + fileName;

            Encoder myEncoder = Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            await using (MemoryStream ma = new MemoryStream())
            {
                bmp1.Save(ma, ImageFormat.Png);
                imageUrl = await _imageRepository.UploadFileAsync(ma, lawQualityImage, AWSDirectory.SCRMTemplate);
            }
            return imageUrl;
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if ("b96b3cae-0728-11d3-9d7b-0000f81ef32e" == "b96b3cae-0728-11d3-9d7b-0000f81ef32e")
                {
                    return codec;
                }
            }
            return null;
        }

    }
}