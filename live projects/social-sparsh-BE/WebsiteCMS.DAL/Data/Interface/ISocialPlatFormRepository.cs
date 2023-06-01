using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="SocialPlatforms"/>.
    /// </summary>
    public interface ISocialPlatFormRepository
    {
        /// <summary>
        /// Gets the all social plate form async.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="IQueryable{T}"/> where <c>T</c> is <see cref="SocialPlatforms"/>.</returns>
        IQueryable<SocialPlatforms> GetAllSocialPlateFormAsync();

        /// <summary>
        /// Gets the social plate form by id async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Plateform record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SocialPlatforms"/>.</returns>
        Task<SocialPlatforms?> GetSocialPlateFormByIdAsync(int id);

        /// <summary>
        /// Adds the social plate form async.
        ///  <para>
        ///     Gets the model of type <see cref="SocialPlatforms"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SocialPlatforms"/>.</returns>
        Task<SocialPlatforms> AddSocialPlateFormAsync(SocialPlatforms model);

        /// <summary>
        /// Updates the social plate form async.
        ///   <para>
        ///     Gets the model of type <see cref="SocialPlatforms"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SocialPlatforms"/>.</returns>
        Task<SocialPlatforms> UpdateSocialPlateFormAsync(SocialPlatforms model);

        /// <summary>
        /// Deletes the align async.
        ///  <para>
        ///     Gets The <see cref="int"/> Id of the Specify Plateform record.
        /// </para>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> DeletePlateformAsync(int id);

    }
}
