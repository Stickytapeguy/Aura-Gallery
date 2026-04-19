//=== Project: Aura Gallery - Clean and ad-free photo and video viewer and editor ===\\
//=== Created: 28/03/2026 in Litomyšl ===\\

//=== IMPORTS ===\\

using Aura_Gallery.Models;
using Aura_Gallery.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

//=== ===\\

// ═══════════════════════════════════════════════════ \\
// NAMESPACE
// ═══════════════════════════════════════════════════ \\

namespace Aura_Gallery.ViewModels;

// ═══════════════════════════════════════════════════ \\
// CLASS — CollectionViewModel
// The brain behind the main gallery view
// Loads photos from all included watched folders
// and exposes them to the UI with filtering support
// "partial" is required by CommunityToolkit.Mvvm
// ═══════════════════════════════════════════════════ \\

public partial class CollectionViewModel : ObservableObject
{
    // ── INJECTED SERVICES ────────────────────────────\\
        //--- IFileService is injected via DI — never new'd up directly ---\\
        private readonly IFileService _fileService;
    // ─────────────────────────────────────────────────\\

    // ── COLLECTIONS ──────────────────────────────────\\
        //--- All photos + videos loaded from watched folders ---\\
        //--- ObservableCollection = UI updates automatically when this changes ---\\
        public ObservableCollection<Photo> AllMedia { get; } = new();

        //--- The filtered subset currently shown in the grid ---\\
        public ObservableCollection<Photo> FilteredMedia { get; } = new();
    // ─────────────────────────────────────────────────\\

    // ── OBSERVABLE PROPERTIES ────────────────────────\\
        //--- [ObservableProperty] auto-generates the full property + change notification ---\\
        //--- CommunityToolkit turns _isLoading into public IsLoading { get; set; } ---\\

        //--- True while folders are being scanned — UI shows a spinner ---\\
        [ObservableProperty]
        private bool _isLoading;

        //--- Current filter: "All", "Photos", "Videos" ---\\
        [ObservableProperty]
        private string _activeFilter = "All";

    // ─────────────────────────────────────────────────\\

    // ══════════════════════════════════════════════════ \\
    // CONSTRUCTOR — CollectionViewModel
    // Receives IFileService from the DI container
    // ══════════════════════════════════════════════════ \\

        public CollectionViewModel(IFileService fileService)
        {
            //--- Store the injected service for use in load functions ---\\
                _fileService = fileService;
            //--- ---\\
        }

    // ══════════════════════════════════════════════════ \\
    // END OF CONSTRUCTOR
    // ══════════════════════════════════════════════════ \\

    // ══════════════════════════════════════════════════ \\
    // FUNCTION — LoadAsync
    // Scans all watched folders marked IncludeInGallery
    // and populates AllMedia + FilteredMedia
    // [RelayCommand] auto-generates LoadCommand for XAML binding
    // ══════════════════════════════════════════════════ \\

        [RelayCommand]
        public async Task LoadAsync()
        {
            //--- Don't scan again if already loading ---\\
                if (IsLoading) return;
            //--- ---\\

            //--- Signal UI to show loading spinner ---\\
                IsLoading = true;
            //--- ---\\

            //--- Clear any previously loaded media ---\\
                AllMedia.Clear();
            //--- ---\\

            // ── LOAD MEDIA ───────────────────────────\\
                //--- GetAllPhotosAsync only returns folders with IncludeInGallery = true ---\\
                    var photos = await _fileService.GetAllPhotosAsync();
                    var videos = await _fileService.GetAllVideosAsync();
                //--- ---\\

                //--- Add everything into the master list ---\\
                    foreach (var photo in photos)
                        AllMedia.Add(photo);

                    foreach (var video in videos)
                        AllMedia.Add(video);
                //--- ---\\
            // ─────────────────────────────────────────\\

            //--- Apply whatever filter is currently active ---\\
                ApplyFilter(ActiveFilter);
            //--- ---\\

            //--- Signal UI that loading is done ---\\
                IsLoading = false;
            //--- ---\\
        }

    // ══════════════════════════════════════════════════ \\
    // END OF FUNCTION — LoadAsync
    // ══════════════════════════════════════════════════ \\

    // ══════════════════════════════════════════════════ \\
    // FUNCTION — SetFilterCommand
    // Called when the user taps All / Photos / Videos
    // Re-runs the filter and updates FilteredMedia
    // [RelayCommand] generates SetFilterCommand for XAML
    // ══════════════════════════════════════════════════ \\

        [RelayCommand]
        public void SetFilter(string filter)
        {
            //--- Store the active filter so UI can highlight the right button ---\\
                ActiveFilter = filter;
            //--- ---\\

            //--- Re-apply the filter to refresh FilteredMedia ---\\
                ApplyFilter(filter);
            //--- ---\\
        }

    // ══════════════════════════════════════════════════ \\
    // END OF FUNCTION — SetFilterCommand
    // ══════════════════════════════════════════════════ \\

    // ══════════════════════════════════════════════════ \\
    // FUNCTION — ApplyFilter (private helper)
    // Rebuilds FilteredMedia from AllMedia based on filter
    // "All"    = everything
    // "Photos" = IsVideo is false
    // "Videos" = IsVideo is true
    // ══════════════════════════════════════════════════ \\

        private void ApplyFilter(string filter)
        {
            //--- Clear the currently displayed list ---\\
                FilteredMedia.Clear();
            //--- ---\\

            // ── FILTER LOGIC ─────────────────────────\\
                var filtered = filter switch
                {
                    //--- Photos only ---\\
                        "Photos" => AllMedia.Where(p => !p.IsVideo),
                    //--- Videos only ---\\
                        "Videos" => AllMedia.Where(p => p.IsVideo),
                    //--- Default: show everything ---\\
                        _ => AllMedia.AsEnumerable(),
                };
            // ─────────────────────────────────────────\\

            //--- Populate FilteredMedia with the result ---\\
                foreach (var item in filtered)
                    FilteredMedia.Add(item);
            //--- ---\\
        }

    // ══════════════════════════════════════════════════ \\
    // END OF FUNCTION — ApplyFilter
    // ══════════════════════════════════════════════════ \\

}

// ═══════════════════════════════════════════════════ \\
// END OF CLASS — CollectionViewModel
// ═══════════════════════════════════════════════════ \\
