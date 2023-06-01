using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SCRMTemplateFieldTypeModel"/>.
    /// </summary>
    public interface SCRMITemplateFieldTypeRepository
    {
        /// <summary>
        /// Gets the all template field type async.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SCRMTemplateFieldTypeModel"/>.</returns>
        Task<List<SCRMTemplateFieldTypeModel>> GetAllTemplateFieldTypeAsync();

        /// <summary>
        /// Gets the template field type by id async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify TemplateField Type record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTemplateFieldTypeModel"/>.</returns>
        Task<SCRMTemplateFieldTypeModel> GetTemplateFieldTypeByIdAsync(int id);

        /// <summary>
        /// Adds the template field type async.
        ///   <para>
        ///     Gets the model of type <see cref="SCRMTemplateFieldTypeModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTemplateFieldTypeModel"/>.</returns>
        Task<SCRMTemplateFieldTypeModel> AddTemplateFieldTypeAsync(SCRMTemplateFieldTypeModel model);

        /// <summary>
        /// Updates the template field type async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify TemplateField Type record.
        /// </para>
        ///  <para>
        ///     Gets the model of type <see cref="SCRMTemplateFieldTypeModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SCRMTemplateFieldTypeModel"/>.</returns>
        Task<SCRMTemplateFieldTypeModel> UpdateTemplateFieldTypeAsync(int id, SCRMTemplateFieldTypeModel model);

        /// <summary>
        /// Updates the template field type status async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify TemplateField Type record.
        /// </para>
        ///  <para>
        ///     Gets the model of type <see cref="SCRMUpdateStatusModel"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        ///  <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> UpdateTemplateFieldTypeStatusAsync(int id, SCRMUpdateStatusModel model);

        /// <summary>
        /// Deletes the template field type async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify TemplateField Type record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task DeleteTemplateFieldTypeAsync(int id);
    }
}
