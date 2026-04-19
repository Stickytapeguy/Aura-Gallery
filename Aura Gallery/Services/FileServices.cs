
//=== Project: Aura Gallery - Clean and ad-free photo and video viewer and editor ===\\
//=== Created: 28/03/2026 in Litomyšl ===\\

//=== IMPORTS ===\\

using Aura_Gallery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

//=== ===\\

// ═══════════════════════════════════════════════════ \\
// NAMESPACE
// ═══════════════════════════════════════════════════ \\

namespace Aura_Gallery.Services;

// ═══════════════════════════════════════════════════ \\
// CLASS — FileService
// The real implementation of IFileService
// Actually talks to the file system
// ═══════════════════════════════════════════════════ \\

    public class FileService : IFileService
    {
        // ── SUPPORTED FILE TYPES ─────────────────────────\\
            //--- These are all the formats Aura Gallery can open ---\\

            //--- Supported image extensions ---\\
                private static readonly HashSet<string> ImageExtensions = new(StringComparer.OrdinalIgnoreCase)
                        {
                            ".jpg", ".jpeg", ".png", ".gif", ".bmp",
                            ".tiff", ".tif", ".webp", ".heic", ".heif", ".raw"
                        };
            //--- ---\\

            //--- Supported video extensions ---\\
                private static readonly HashSet<string> VideoExtensions = new(StringComparer.OrdinalIgnoreCase)
                        {
                            ".mp4", ".mov", ".avi", ".mkv", ".wmv",
                            ".m4v", ".flv", ".webm", ".3gp"
                        };
            //--- ---\\

        // ──────────────────────────────────────────────────\\

        // ── WATCHED FOLDERS LIST ─────────────────────────\\
            //--- List of folders the user has added to Aura Gallery ---\\
            //--- Persisted in app settings later — for now stored in memory ---\\
            private readonly List<string> _watchedFolders = new();
    // ──────────────────────────────────────────────────\\

    // ══════════════════════════════════════════════════ \\
    // CONSTRUCTOR — FileService
    // Starts empty — user adds folders manually
    // No auto-loading, user is always in control
    // ══════════════════════════════════════════════════ \\

    public FileService()
    {
        //--- Start with no folders ---\\
        //--- User adds them through the Folder Manager ---\\
    }

    // ══════════════════════════════════════════════════ \\
    // END OF CONSTRUCTOR
    // ══════════════════════════════════════════════════ \\

    // ══════════════════════════════════════════════════ \\
    // FUNCTION — GetWatchedFolders
    // Returns the list of folders the user is watching
    // ══════════════════════════════════════════════════ \\

    public IEnumerable<string> GetWatchedFolders()
            {
                //--- Return a copy so nobody can modify the internal list ---\\
                    return _watchedFolders.AsReadOnly();
                //--- ---\\
            }

        // ══════════════════════════════════════════════════ \\
        // END OF FUNCTION — GetWatchedFolders
        // ══════════════════════════════════════════════════ \\

        // ══════════════════════════════════════════════════ \\
        // FUNCTION — AddWatchedFolder
        // Adds a new folder to the watch list
        // Ignores duplicates and non-existent folders
        // ══════════════════════════════════════════════════ \\

            public void AddWatchedFolder(string folderPath)
            {
                //--- Only add if folder exists and isn't already in the list ---\\
                    if (Directory.Exists(folderPath) && !_watchedFolders.Contains(folderPath))
                        _watchedFolders.Add(folderPath);
                //--- ---\\
            }

        // ══════════════════════════════════════════════════ \\
        // END OF FUNCTION — AddWatchedFolder
        // ══════════════════════════════════════════════════ \\

        // ══════════════════════════════════════════════════ \\
        // FUNCTION — RemoveWatchedFolder
        // Removes a folder from the watch list
        // ══════════════════════════════════════════════════ \\

            public void RemoveWatchedFolder(string folderPath)
            {
                //--- Simply remove it if it exists in the list ---\\
                    _watchedFolders.Remove(folderPath);
                //--- ---\\
            }

        // ══════════════════════════════════════════════════ \\
        // END OF FUNCTION — RemoveWatchedFolder
        // ══════════════════════════════════════════════════ \\

        // ══════════════════════════════════════════════════ \\
        // FUNCTION — GetPhotosFromFolderAsync
        // Scans one folder and returns all image files in it
        // async = runs in background so UI never freezes
        // ══════════════════════════════════════════════════ \\

            public async Task<IEnumerable<Photo>> GetPhotosFromFolderAsync(string folderPath)
            {
                //--- Run the file scan on a background thread ---\\
                    return await Task.Run(() =>
                    {
                        //--- If folder doesn't exist return empty list ---\\
                            if (!Directory.Exists(folderPath))
                                return Enumerable.Empty<Photo>();
                        //--- ---\\

                        //--- Scan all files recursively, filter to images only ---\\
                            return Directory
                                .EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories)
                                .Where(f => IsImageFile(f))
                                .Select(f => BuildPhoto(f, isVideo: false));
                        //--- ---\\
                    });
                //--- ---\\
            }

        // ══════════════════════════════════════════════════ \\
        // END OF FUNCTION — GetPhotosFromFolderAsync
        // ══════════════════════════════════════════════════ \\

        // ══════════════════════════════════════════════════ \\
        // FUNCTION — GetAllPhotosAsync
        // Scans ALL watched folders and returns every image
        // ══════════════════════════════════════════════════ \\

            public async Task<IEnumerable<Photo>> GetAllPhotosAsync()
            {
                //--- Start scanning all folders at the same time ---\\
                    var tasks = _watchedFolders.Select(f => GetPhotosFromFolderAsync(f));
                //--- ---\\

                //--- Wait for all scans to finish ---\\
                    var results = await Task.WhenAll(tasks);
                //--- ---\\

                //--- Flatten all results into one list ---\\
                    return results.SelectMany(r => r);
                //--- ---\\
            }

        // ══════════════════════════════════════════════════ \\
        // END OF FUNCTION — GetAllPhotosAsync
        // ══════════════════════════════════════════════════ \\

        // ══════════════════════════════════════════════════ \\
        // FUNCTION — GetVideosFromFolderAsync
        // Same as GetPhotosFromFolderAsync but for videos
        // ══════════════════════════════════════════════════ \\

            public async Task<IEnumerable<Photo>> GetVideosFromFolderAsync(string folderPath)
            {
                //--- Run the file scan on a background thread ---\\
                    return await Task.Run(() =>
                    {
                        //--- If folder doesn't exist return empty list ---\\
                            if (!Directory.Exists(folderPath))
                                return Enumerable.Empty<Photo>();
                        //--- ---\\

                        //--- Scan all files, filter to videos only ---\\
                            return Directory
                                .EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories)
                                .Where(f => IsVideoFile(f))
                                .Select(f => BuildPhoto(f, isVideo: true));
                        //--- ---\\
                    });
                    //--- ---\\
            }

        // ══════════════════════════════════════════════════ \\
        // END OF FUNCTION — GetVideosFromFolderAsync
        // ══════════════════════════════════════════════════ \\

        // ══════════════════════════════════════════════════ \\
        // FUNCTION — GetAllVideosAsync
        // Scans ALL watched folders and returns every video
        // ══════════════════════════════════════════════════ \\

            public async Task<IEnumerable<Photo>> GetAllVideosAsync()
            {
                //--- Start scanning all folders at the same time ---\\
                    var tasks = _watchedFolders.Select(f => GetVideosFromFolderAsync(f));
                //--- ---\\

                //--- Wait for all scans to finish ---\\
                    var results = await Task.WhenAll(tasks);
                //--- ---\\

                //--- Flatten all results into one list ---\\
                    return results.SelectMany(r => r);
                //--- ---\\
            }

        // ══════════════════════════════════════════════════ \\
        // END OF FUNCTION — GetAllVideosAsync
        // ══════════════════════════════════════════════════ \\

        // ══════════════════════════════════════════════════ \\
        // FUNCTION — GetPhotoDetailsAsync
        // Loads full metadata for a single photo file
        // ══════════════════════════════════════════════════ \\

            public async Task<Photo> GetPhotoDetailsAsync(string filePath)
            {
                //--- Run on background thread ---\\
                    return await Task.Run(() => BuildPhoto(filePath, IsVideoFile(filePath)));
                //--- ---\\
            }

        // ══════════════════════════════════════════════════ \\
        // END OF FUNCTION — GetPhotoDetailsAsync
        // ══════════════════════════════════════════════════ \\

        // ══════════════════════════════════════════════════ \\
        // FUNCTION — IsImageFile
        // Returns true if the file extension is a supported image
        // ══════════════════════════════════════════════════ \\

            public bool IsImageFile(string filePath)
            {
                //--- Check if the extension is in our supported list ---\\
                    return ImageExtensions.Contains(Path.GetExtension(filePath));
                //--- ---\\
            }

        // ══════════════════════════════════════════════════ \\
        // END OF FUNCTION — IsImageFile
        // ══════════════════════════════════════════════════ \\

        // ══════════════════════════════════════════════════ \\
        // FUNCTION — IsVideoFile
        // Returns true if the file extension is a supported video
        // ══════════════════════════════════════════════════ \\

            public bool IsVideoFile(string filePath)
            {
                //--- Check if the extension is in our supported list ---\\
                    return VideoExtensions.Contains(Path.GetExtension(filePath));
                //--- ---\\
            }

        // ══════════════════════════════════════════════════ \\
        // END OF FUNCTION — IsVideoFile
        // ══════════════════════════════════════════════════ \\

        // ══════════════════════════════════════════════════ \\
        // FUNCTION — BuildPhoto (private helper)
        // Creates a Photo object from a file path
        // Called internally by all the scan functions above
        // ══════════════════════════════════════════════════ \\

            private static Photo BuildPhoto(string filePath, bool isVideo)
            {
                //--- Get basic file info from the OS ---\\
                    var info = new FileInfo(filePath);
                //--- ---\\

                //--- Build and return the Photo object ---\\
                    return new Photo
                    {
                        //--- File location and name ---\\
                            FilePath = filePath,
                            FileName = info.Name,
                        //--- ---\\

                        //--- Use last write time as fallback if no EXIF date ---\\
                            DateTaken = info.LastWriteTime,
                        //--- ---\\

                        //--- File size in raw bytes ---\\
                            FileSizeBytes = info.Length,
                        //--- ---\\

                        //--- Mark whether it's a video ---\\
                            IsVideo = isVideo,
                        //--- ---\\

                        //--- Dimensions start at 0 — loaded later when photo is opened ---\\
                            Width = 0,
                            Height = 0,
                        //--- ---\\
                    };
                //--- ---\\
            }

        // ══════════════════════════════════════════════════ \\
        // END OF FUNCTION — BuildPhoto
        // ══════════════════════════════════════════════════ \\

    }

    // ═══════════════════════════════════════════════════ \\
    // END OF CLASS — FileService
    // ═══════════════════════════════════════════════════ \\