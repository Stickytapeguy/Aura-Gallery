//=== Project: Aura Gallery - Clean and ad-free photo and video viewer and editor ===\\
//=== Created: 28/03/2026 in Litomyšl ===\\

//=== IMPORTS ===\\

using Aura_Gallery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

//=== ===\\

// ═══════════════════════════════════════════════════ \\
// NAMESPACE
// ═══════════════════════════════════════════════════ \\

namespace Aura_Gallery.Services;

// ═══════════════════════════════════════════════════ \\
// INTERFACE — a contract/blueprint
// Any class that "implements" this MUST have all
// of these functions — no exceptions
// Think of it like a job description
// ═══════════════════════════════════════════════════ \\

public interface IFileService
{
    // ── FOLDER FUNCTIONS ────────────────────────────\\
        //--- These deal with reading folders ---\\

        //--- Returns all watched folder paths the user added ---\\
            IEnumerable<string> GetWatchedFolders();
        //--- ---\\

        //--- Adds a new folder to watch e.g. C:\Photos ---\\
            void AddWatchedFolder(string folderPath);

        //--- Removes a folder from the watch list ---\\
            void RemoveWatchedFolder(string folderPath);
        //--- ---\\
    // ─────────────────────────────────────────────────\\

    // ── PHOTO FUNCTIONS ─────────────────────────────\\
        //--- These deal with loading photos ---\\

        //--- Scans a folder and returns all photos in it ---\\
        //--- async = runs in background so UI doesn't freeze ---\\
            Task<IEnumerable<Photo>> GetPhotosFromFolderAsync(string folderPath);
        //--- ---\\

        //--- Scans ALL watched folders and returns every photo ---\\
            Task<IEnumerable<Photo>> GetAllPhotosAsync();
        //--- ---\\

        //--- Loads a single photo's metadata (size, date, dimensions) ---\\
            Task<Photo> GetPhotoDetailsAsync(string filePath);
        //--- ---\\
    // ─────────────────────────────────────────────────\\

    // ── VIDEO FUNCTIONS ─────────────────────────────\\
        //--- Same as photo functions but filters for video files ---\\
            Task<IEnumerable<Photo>> GetVideosFromFolderAsync(string folderPath);
            Task<IEnumerable<Photo>> GetAllVideosAsync();
    //--- ---\\
    // ─────────────────────────────────────────────────\\

    // ── FILE TYPE HELPERS ───────────────────────────\\
        //--- These check what kind of file something is ---\\

        //--- Returns true if the file is a supported image ---\\
            bool IsImageFile(string filePath);
        //--- ---\\

        //--- Returns true if the file is a supported video ---\\
            bool IsVideoFile(string filePath);
    //--- ---\\
    // ─────────────────────────────────────────────────\\
}