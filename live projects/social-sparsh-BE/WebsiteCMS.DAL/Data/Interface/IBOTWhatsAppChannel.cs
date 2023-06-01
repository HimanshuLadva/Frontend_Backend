using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IBOTWhatsAppChannel
    {
        Task<RestResponse> DeleteTemplateAsync(string templateName);
        Task<RestResponse> EditTemplateAsync(ICollection<JObject> Components, string message_template_id);
        Task<object> Execute(BOTWhatsappMessageTemplateModel model);
        Task<object> GenerateResponse(BOTWhatsappMessageTemplateModel model);
        Task<ObjectResult> GetTemplateAsync(string bussinessId);
        Task<string> SendWhatsAppMessageAsync(string id, string phoneNumber);
    }
}