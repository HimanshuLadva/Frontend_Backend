using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Data.Interface;

namespace WebsiteCMS.BLL.Interfaces
{
    /// <summary>
    ///     An Interface That contains method Declarations for performing tasks related to chatbot records.
    /// </summary>
    public interface IBOTchatbotService
    {
        /// <summary>
        ///     An asynchronous method to map model of type <see cref="BOTChatBotModel"/> to entity of type <see cref="BOTChatBot"/> and save it to the database.
        ///     <para>
        ///         Gets the model of type <see cref="BOTChatBotModel"/>.
        ///     </para>
        /// </summary>
        /// <param name="model">The model of type <see cref="BOTChatBotModel"/>.</param>
        /// <returns>Returns a <see cref="Task" /> of <see cref= "long"/> that contains id of the created record.</returns>
        Task<long> CreateBotAsync(BOTChatBotModel model);
        /// <summary>
        /// Deletes the bot async.
        ///     <para>
        ///         Gets the <see cref="long"/> id. of the record to be deleted.
        ///     </para>
        ///     <para>
        ///         Calls <see cref="IBOTChatBOTRepository.DeleteBotAsync(long)"/> to delete record from Database.
        ///     </para>
        /// </summary>
        /// <param name="botId">The <see cref="long"/> id. of the record to be deleted.</param>
        /// <returns>Returns a <see cref="Task" /> of <see cref= "long"/> Id of the Deleted record.</returns>
        Task<long> DeleteBotAsync(long botId);
        /// <summary>
        ///      An asynchronous method to get the bot list from database and map to model of type <see cref="BOTChatBotModel"/>.
        ///      <para>
        ///         Calls <see cref="IBaseRepository.GetUserId()"/> to get the active user and pass it to <see cref="IBOTChatBOTRepository.GetBotByUserIdAsync(string)"/> to get all the bots of the user.
        ///      </para>
        /// </summary>
        /// <returns>Returns a <see cref="Task" /> of <see cref= "List{T}"/> where <c>T</c> is <see cref="BOTChatBotModel"/>.</returns>
        Task<List<BOTChatBotModel>?> GetBotListAsync();
        /// <summary>
        ///     An asynchronous method to map to model of type <see cref="BOTHistoryModel"/> to entity of type <see cref="BOTHistory"/> and save it to the database.
        ///     <para>
        ///         Gets the model of type <see cref="BOTHistoryModel"/>.
        ///     </para>
        ///     <para>
        ///         Calls <see cref="IBOTHistoryRepository.AddReplyAsync(BOTHistory)"/> to save the conversation.
        ///     </para>
        /// </summary>
        /// <param name="model">The model of type <see cref="BOTHistoryModel"/>.</param>
        /// <returns>Returns a <see cref="Task" /> of <see cref= "BOTHistoryModel?"/>.</returns>
        Task<BOTHistoryModel?> SaveReplyAsync(BOTHistoryModel model);
        /// <summary>
        ///     Starts the bot and creates a new visitor/session record for the visitor.
        ///     <para>
        ///         Gets the <see cref="long"/> id of the chatbot record.
        ///     </para>
        ///     <para>
        ///         Gets the <see cref="Guid"/> session a Unique Id for a new visitor.
        ///     </para>
        ///     <para>
        ///         Calls <see cref="IBOTSessionTracker.GetVisitorSession(Guid)"/> to create a new visitor.
        ///     </para>
        /// </summary>
        /// <param name="BotId">The bot id.</param>
        /// <param name="session">The session.</param>
        /// <returns>Returns a <see cref="Task" /> of <see cref= "Dictionary{TKey, TValue}"/> where <c>TKey</c> is <see cref="string"/> and <c>TValue</c> is <see cref="object?"/>.</returns>
        Task<Dictionary<string, object>?> StartBot(long BotId, Guid session);
        /// <summary>
        ///     Updates the index of the questions in to the <see cref="List{T}"/> of type <see cref="BOTQuestionViewModel"/>.
        ///     <para>
        ///         Gets the <see cref="List{T}"/> of models of type <see cref="BOTQuestionBaseViewModel"/>
        ///     </para>
        /// </summary>
        /// <param name="models">The <see cref="List{T}"/> of models of type <see cref="BOTQuestionBaseViewModel"/>.</param>
        /// <returns>A <see cref="List{T}"/> of models of type <see cref="BOTQuestionViewModel"/>.</returns>
        List<BOTQuestionViewModel> UpdateIndex(List<BOTQuestionViewModel> models);
    }
}