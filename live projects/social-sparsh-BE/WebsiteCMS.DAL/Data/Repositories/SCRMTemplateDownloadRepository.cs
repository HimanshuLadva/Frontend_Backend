using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using NPOI.HPSF;
using Microsoft.EntityFrameworkCore;
using NPOI.POIFS.NIO;
using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Org.BouncyCastle.Crypto.Tls;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.AspNetCore.Http;
using NPOI.Util;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class SCRMTemplateDownloadRepository : SCRMITemplateDownloadRepository
    {
        private readonly WebsiteCMSDbContext _context;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public readonly SCRMIUserTemplateRepository _userTemplateRepository;

        public SCRMTemplateDownloadRepository(WebsiteCMSDbContext context, IWebHostEnvironment webHostEnvironment, SCRMIUserTemplateRepository userTemplateRepository)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userTemplateRepository = userTemplateRepository;
        }

        public async Task<FileContentResult> DownloadUserTemplateByIdAsync(string userId, int templateId, float templateWidth, float templateHeight, IHeaderDictionary model)
        {
            templateWidth = 500;
            templateHeight = 500;
            if (templateWidth == templateHeight)
            {
                var publicFolder = "/Resources/SCRM/Private/Images/TemplateImages/PublicTemplateImages";
                var premiumFolder = "/Resources/SCRM/Private/Images/TemplateImages/PremiumTemplateImages";
                               

                var record = await _userTemplateRepository.GetUserTemplateByIdAsync("", templateId);
                var templateRecord = _context.tblSCRMTemplate.Where(x => x.Id == record.TemplateId).FirstOrDefault();

                Image originalBgImage = Image.FromFile(Directory.GetCurrentDirectory() + publicFolder + record.TemplateImageURL);
                if (templateRecord != null && templateRecord.IsFree == true)
                    originalBgImage = Image.FromFile(Directory.GetCurrentDirectory() + premiumFolder + record.TemplateImageURL);

                Bitmap bgImageBitmap = new(originalBgImage);
                Image bgImage = ResizeImage(bgImageBitmap, new Size((int)templateWidth, (int)templateHeight));
                Bitmap bitmap = new((int)templateWidth, (int)templateHeight);

                string fontFamilyPath = Directory.GetCurrentDirectory() + "/Resources/SCRM/Public/FontFamilies/";
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.DrawImage(bgImage, 0, 0);

                    foreach (var img in record.ImageFields)
                    {
                        if (img.Value != null && img.IsDisplay == true)
                        {
                            float imageWidth = UpdateSizeAndLocation(templateWidth, 500, (float)img.Height);
                            float imageHeight = UpdateSizeAndLocation(templateHeight, 500, (float)img.Width);

                            float image_X = UpdateSizeAndLocation(templateWidth, 500, (float)img.X);
                            float image_Y = UpdateSizeAndLocation(templateHeight, 500, (float)img.Y);

                            var imgFieldFilePath = Directory.GetCurrentDirectory() + img.Value;

                            if (File.Exists(imgFieldFilePath))
                            {
                                Image originalFieldImage = Image.FromFile(Directory.GetCurrentDirectory() + img.Value);
                                Bitmap fieldImageBitmap = new(originalFieldImage);
                                Image fieldImage = ResizeImage(fieldImageBitmap, new Size((int)imageWidth, (int)imageHeight));
                                g.DrawImage(fieldImage, image_X, image_Y, imageWidth, imageHeight);
                            }
                        }
                    }

                    Dictionary<int, int> header = new();
                    bool isInt = int.TryParse(model.Keys.ToList().Where(x => new Regex(@"\d+").Match(x).Success == true)?.First(), out int size); 
                    var headerRecord = model.Keys.ToList().Where(x => x.Contains('*'))?.FirstOrDefault();
                    if (isInt == true)
                    {
                        header.Add(Convert.ToInt32(size), Convert.ToInt32(model[size.ToString()]));
                    }
                    else if (headerRecord != null)
                    {
                        string[] keys = headerRecord!.Split('*').Select(x => x.Trim()).ToArray();
                        string[] values = model[headerRecord].ToString().Split(',').Select(x => x.Trim()).ToArray();

                        for (int i = 0; i < keys.Length; i++)
                        {
                            header.Add(Convert.ToInt32(keys[i]), Convert.ToInt32(values[i]));
                        }
                    }

                    foreach (var txt in record.TextFields)
                    {
                        if (!string.IsNullOrEmpty(txt.Value))
                        {
                            if (header.ContainsKey(txt.TemplateFieldId) && txt.IsDisplay == true && txt.Value != null)
                            {
                                float text_X = UpdateSizeAndLocation(templateWidth, 500, (float)txt.X);
                                float text_Y = UpdateSizeAndLocation(templateHeight, 500, (float)txt.Y);
                                float text_size = UpdateSizeAndLocation(templateWidth, 500, Convert.ToInt32(header[txt.TemplateFieldId]));

                                float txtAlign_X = 0;
                                float txtAlign_Y = 0;

                                string fontFamilyName = txt.FontFamilyName!.Replace(" ", "");
                                var fontFamily = new PrivateFontCollection();
                                fontFamily.AddFontFile(fontFamilyPath + $"{fontFamilyName}-Regular.ttf");

                                Font txtFont = new(fontFamily.Families[0], text_size, FontStyle.Regular, GraphicsUnit.Pixel);
                                Image txtImage = TextToImage(txt.Value!, txtFont, txt.Color);

                                if (txt.AlignId == 1)
                                {
                                    txtAlign_X = text_X;
                                    txtAlign_Y = text_Y;
                                }
                                else if (txt.AlignId == 2)
                                {
                                    txtAlign_X = text_X + txtImage.Width / 2;
                                    txtAlign_Y = text_Y;
                                }
                                else if (txt.AlignId == 3)
                                {
                                    txtAlign_X = text_X + txtImage.Width;
                                    txtAlign_Y = text_Y;
                                }
                                g.DrawImage(txtImage, txtAlign_X, txtAlign_Y);
                            }
                        }
                    }
                }
                Image image = bitmap;
                string folderPath = "/Resources/SCRM/Private/Images/DownloadImages/";
                string file = Path.GetFileNameWithoutExtension(Guid.NewGuid().ToString() + "_Template") + ".jpg";
                image.Save(Directory.GetCurrentDirectory() + folderPath + file);
                string imagePath = folderPath + file;

                var result = new FileContentResult(File.ReadAllBytes(Directory.GetCurrentDirectory() + imagePath), "image/jpeg")
                {
                    FileDownloadName = file
                };
                return result;
            }
            return null!;
        }

        public static float UpdateSizeAndLocation(float userGiven, float reference, float predictor)
        {
            return (predictor * userGiven) / reference;
        }

        public static Image ResizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent;
            float newImageWidth = imgToResize.Width;
            float newImageHeight = imgToResize.Height;
            float nPercentW = size.Width / (float)sourceWidth;
            float nPercentH = size.Height / (float)sourceHeight;

            if (nPercentH > nPercentW)
            {
                nPercent = nPercentH;
                newImageWidth = UpdateSizeAndLocation(imgToResize.Width, imgToResize.Height, size.Height);
            }
            else
            {
                nPercent = nPercentW;
                newImageHeight = UpdateSizeAndLocation(imgToResize.Height, imgToResize.Width, size.Width);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap originalBitmap = new(destWidth, destHeight);

            if (nPercentH > nPercentW)
                originalBitmap = new((int)newImageWidth, destHeight);
            else
                originalBitmap = new(destWidth, (int)newImageHeight);
            Graphics g = Graphics.FromImage(originalBitmap);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;

            if (nPercentH > nPercentW)
                g.DrawImage(imgToResize, 0, 0, newImageWidth, destHeight);
            else
                g.DrawImage(imgToResize, 0, 0, destWidth, newImageHeight);
            g.Dispose();

            Image originalImage = originalBitmap;
            Bitmap bitmap = new(size.Width, size.Height);
            Graphics g1 = Graphics.FromImage(bitmap);

            float drowImageFrom_X = (originalImage.Width - size.Width) / 2;
            float drowImageFrom_Y = (originalImage.Height - size.Height) / 2;

            g1.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g1.SmoothingMode = SmoothingMode.HighQuality;
            g1.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g1.CompositingQuality = CompositingQuality.HighQuality;

            RectangleF srcRect = new(drowImageFrom_X, drowImageFrom_Y, destWidth, destHeight);
            GraphicsUnit units = GraphicsUnit.Pixel;

            g1.DrawImage(originalImage, 0, 0, srcRect, units);
            g1.Dispose();
            return bitmap;
        }

        public static Bitmap TextToImage(string Text, Font font, string hashColor)
        {
            Color color = ColorTranslator.FromHtml(hashColor);
            int r = Convert.ToInt32(color.R);
            int g = Convert.ToInt32(color.G);
            int b = Convert.ToInt32(color.B);

            Bitmap bitmap = new(1, 1);
            Graphics graphics = Graphics.FromImage(bitmap);
            int width = (int)graphics.MeasureString(Text, font).Width;
            int height = (int)graphics.MeasureString(Text, font).Height;
            bitmap = new Bitmap(bitmap, new Size(width, height));
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.DrawString(Text, font, new SolidBrush(Color.FromArgb(r, g, b)), 0, 0);
            graphics.Flush();
            graphics.Dispose();
            return bitmap;
        }
    }
}
