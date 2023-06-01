using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IBotQuestionRepository
    {
        Task<List<BOTQuestion>?> GetAllQuestionByBotID(long id);
        Task<List<BOTQuestion>?> GetAllQuestion();
        Task<List<BOTQuestion>> GetQuestionOptionComponent(long id);
        Task<BOTQuestion?> GetQuestionComponentById(long id);
        Task<List<BOTQuestion>> GetExceptQuestion(List<BOTQuestion> model, long id);
        Task<bool> DeleteQuestionList(BOTQuestion model);
        Task<BOTQuestion> AddQuestionAsync(BOTQuestion model);
        Task<BOTQuestion?> GetQuestionByTargetId(string targetId, long botId);
        IQueryable<BOTQuestion?> GetQuestionByFrontendId(string frontendId);
        Task<BOTQuestion> UpdateQuestions(BOTQuestion model);
    }
}