//=== Project: Aura Gallery - Clean and ad-free photo and video viewer and editor ===\\
//=== Created: 28/03/2026 in Litomyšl ===\\

//=== IMPORTS ===\\

using System;
using System.IO;

//=== ===\\

// ═══════════════════════════════════════════════════ \\
// NAMESPACE — tells C# this file belongs to AuraGallery
// Think of it like a folder address for code
// ═══════════════════════════════════════════════════ \\

namespace Aura_Gallery.Models;

// ═══════════════════════════════════════════════════ \\
// CLASS — a blueprint for what a "Photo" object is
// Every photo in the app will be one of these
// ═══════════════════════════════════════════════════ \\

    public class Photo
    {
        // ── PROPERTIES ────────────────────────────────── \\
            //--- These are the pieces of data every photo has ---\\

            //--- Full path to the file e.g. C:\Photos\dog.jpg ---\\
                public string FilePath { get; set; } = string.Empty;
            //--- ---\\

            //--- Just the filename e.g. "dog.jpg" ---\\
                public string FileName { get; set; } = string.Empty;
            //--- ---\\

            //--- The date the photo was taken (nullable — some files have no date) ---\\
                public DateTime? DateTaken { get; set; }
            //--- ---\\

            //--- File size in bytes — we'll convert to MB for display ---\\
                public long FileSizeBytes { get; set; }
            //--- ---\\

            //--- Width of the image in pixels ---\\
                public int Width { get; set; }
            //--- ---\\

            //--- Height of the image in pixels ---\\
                public int Height { get; set; }
            //--- ---\\

            //--- Is this a video file? (mp4, mov etc) ---\\
                public bool IsVideo { get; set; }
            //--- ---\\

            //--- Is this photo marked as a favourite? ---\\
                public bool IsFavourite { get; set; }
            //--- ---\\

        // ──────────────────────────────────── \\

        // ── COMPUTED PROPERTIES ────────────────────────── \\
            //--- These calculate their value from other properties ---\\
            //--- No need to set them manually ---\\

            //--- Returns file size as a readable string e.g. "3.2 MB" ---\\
                public string FileSizeDisplay =>
                    FileSizeBytes < 1024 * 1024
                        ? $"{FileSizeBytes / 1024.0:F1} KB"
                        : $"{FileSizeBytes / (1024.0 * 1024):F1} MB";
            //--- ---\\

            //--- Returns resolution as a readable string e.g. "4032 × 3024" ---\\
                public string ResolutionDisplay =>
                    Width > 0 && Height > 0
                        ? $"{Width} × {Height}"
                        : "Unknown";
            //--- ---\\

            //--- Returns the file extension in uppercase e.g. "JPG" ---\\
                public string FileType =>
                    Path.GetExtension(FilePath).TrimStart('.').ToUpper();
            //--- ---\\
        // ──────────────────────────────────── \\
    }