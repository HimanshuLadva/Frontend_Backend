using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Repositories;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of that contain AWS Image/>.
    /// </summary>
    public interface IAWSImageService
    {
        /// <summary>
        /// Uploads the file async.
        /// </summary>
        /// <param name="memoryStream">The memory stream.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="FolderName">The folder name.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="string"/>.</returns>
        Task<string> UploadFileAsync(MemoryStream memoryStream, string fileName, AWSDirectory FolderName);

        /// <summary>
        /// Deletes the image async.
        /// </summary>
        /// <param name="imageName">The image name.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> DeleteImageAsync(string imageName);

        /// <summary>
        /// Gets the a w s directory.
        /// </summary>
        /// <param name="folderName">The folder name.</param>
        /// <returns>Returns a Entity type <see cref="string"/>.</returns>
        string GetAWSDirectory(AWSDirectory folderName);

        /// <summary>
        /// Are the s3 file exists.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="versionId">The version id.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> IsS3FileExists(string fileName, string versionId = "");

        /// <summary>
        /// Gets the image base url.
        /// </summary>
        /// <returns>Returns a Entity type <see cref="string"/>.</returns>
        string GetImageBaseUrl();

        /// <summary>
        /// Gets the file stream async.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="byte[]"/>.</returns>
        Task<byte[]> GetFileStreamAsync(string fileName);

        /// <summary>
        /// Copies the object async.
        /// </summary>
        /// <param name="SourceDirectory">The source directory.</param>
        /// <param name="DestinationDirectory">The destination directory.</param>
        /// <param name="FileName">The file name.</param>
        /// <returns>A Task.</returns>
        Task CopyObjectAsync(AWSDirectory SourceDirectory, AWSDirectory DestinationDirectory, string FileName);
    }
}
