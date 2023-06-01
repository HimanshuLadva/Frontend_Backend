using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IBOTChannelSelector<T> 
        where T : class
    {
        Task<object> Execute(T model);
        Task<object> GenerateResponse(T model);
    }

    //public class WhatsAppChannel : IBOTChannelSelector<WhatsApp>
    //{
    //    public object Execute(WhatsApp model)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<object> GenerateResponse(WhatsApp model)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
