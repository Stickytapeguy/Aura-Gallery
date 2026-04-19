//=== Project: Aura Gallery - Clean and ad-free photo and video viewer and editor ===\\
//=== Created: 28/03/2026 in Litomyšl ===\\

//=== IMPORTS ===\\

using Aura_Gallery.Services;
using Aura_Gallery.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Storage.Pickers;
using WinRT.Interop;

//=== ===\\

// ═══════════════════════════════════════════════════ \\
// NAMESPACE
// ═══════════════════════════════════════════════════ \\

namespace Aura_Gallery.Views;

// ═══════════════════════════════════════════════════ \\
// CLASS — CollectionPage
// The main gallery page — hosts the photo grid
// and all floating panels
// ═══════════════════════════════════════════════════ \\

public sealed partial class CollectionPage : Page
{
    // ── VIEWMODEL ────────────────────────────────────\\
        //--- Pulled from DI — the brain behind this page ---\\
        public CollectionViewModel ViewModel { get; }
    // ─────────────────────────────────────────────────\\

    // ══════════════════════════════════════════════════ \\
    // CONSTRUCTOR — CollectionPage
    // ══════════════════════════════════════════════════ \\

        public CollectionPage()
        {
            ViewModel = App.Services.GetRequiredService<CollectionViewModel>();
            InitializeComponent();
        }

    // ══════════════════════════════════════════════════ \\
    // END OF CONSTRUCTOR
    // ══════════════════════════════════════════════════ \\


    private async void TempAddFolderButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var picker = new FolderPicker();
        picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
        picker.FileTypeFilter.Add("*");

        var hwnd = WindowNative.GetWindowHandle(App.CurrentWindow);
        InitializeWithWindow.Initialize(picker, hwnd);

        var folder = await picker.PickSingleFolderAsync();
        if (folder != null)
        {
            var fileService = App.Services.GetRequiredService<IFileService>();
            fileService.AddWatchedFolder(folder.Path);
            await ViewModel.LoadAsync();
        }
    }

}

// ═══════════════════════════════════════════════════ \\
// END OF CLASS — CollectionPage
// ═══════════════════════════════════════════════════ \\
