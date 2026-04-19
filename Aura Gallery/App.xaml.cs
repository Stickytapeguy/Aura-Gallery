//      ______                                       ______             __  __                               
//     /      \                                     /      \           /  |/  |                              
//    /$$$$$$  | __    __   ______   ______        /$$$$$$  |  ______  $$ |$$ |  ______    ______   __    __ 
//    $$ |__$$ |/  |  /  | /      \ /      \       $$ | _$$/  /      \ $$ |$$ | /      \  /      \ /  |  /  |
//    $$    $$ |$$ |  $$ |/$$$$$$  |$$$$$$  |      $$ |/    | $$$$$$  |$$ |$$ |/$$$$$$  |/$$$$$$  |$$ |  $$ |
//    $$$$$$$$ |$$ |  $$ |$$ |  $$/ /    $$ |      $$ |$$$$ | /    $$ |$$ |$$ |$$    $$ |$$ |  $$/ $$ |  $$ |
//    $$ |  $$ |$$ \__$$ |$$ |     /$$$$$$$ |      $$ \__$$ |/$$$$$$$ |$$ |$$ |$$$$$$$$/ $$ |      $$ \__$$ |
//    $$ |  $$ |$$    $$/ $$ |     $$    $$ |      $$    $$/ $$    $$ |$$ |$$ |$$       |$$ |      $$    $$ |
//    $$/   $$/  $$$$$$/  $$/       $$$$$$$/        $$$$$$/   $$$$$$$/ $$/ $$/  $$$$$$$/ $$/        $$$$$$$ |
//                                                                                                 /  \__$$ |
//                                                                                                 $$    $$/ 
//                                                                                                  $$$$$$/  

//=== Project: Aura Gallery - Clean and ad-free photo and video viewer and editor ===\\
//=== Created: 28/03/2026 in Litomyšl ===\\
//=== Main entry point — first code that runs on launch ===\\

//=== IMPORTS — only keeping what we actually need ===\\

using Aura_Gallery.Services;
using Aura_Gallery.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

//=== ===\\

// ═══════════════════════════════════════════════════ \\
// NAMESPACE — root namespace for the whole app
// ═══════════════════════════════════════════════════ \\

namespace Aura_Gallery;

// ═══════════════════════════════════════════════════ \\
// CLASS — the App itself
// Inherits from Application (built into WinUI3)
// "partial" means the other half is in App.xaml
// ═══════════════════════════════════════════════════ \\

public partial class App : Application
{
    // ── SERVICE PROVIDER ────────────────────────────\\
        //--- This is the "smart factory" that creates our services ---\\
        //--- static = one instance shared across the whole app ---\\
        //--- null! = we promise to set this before using it ---\\
        public static IServiceProvider Services { get; private set; } = null!;
    // ─────────────────────────────────────────────────\\

    // ── CURRENT WINDOW ──────────────────────────────\\
        //--- Holds a reference to the main window ---\\
        //--- static so any page can access it ---\\
        public static Window? CurrentWindow { get; private set; }
    // ─────────────────────────────────────────────────\\

    // ══════════════════════════════════════════════ \\
    // CONSTRUCTOR — runs once when app starts
    // ══════════════════════════════════════════════ \\
        public App()
        {
            InitializeComponent();

            //--- Set up all our services ---\\
            Services = ConfigureServices();
        }
    // ══════════════════════════════════════════════ \\
    // END OF CONSTRUCTOR
    // ══════════════════════════════════════════════ \\


    // ══════════════════════════════════════════════ \\
    // FUNCTION — ConfigureServices
    // Registers all services so they can be injected
    // anywhere in the app automatically
    // ══════════════════════════════════════════════ \\
        private static IServiceProvider ConfigureServices()
        {
            //--- Create a blank service collection ---\\
                var services = new ServiceCollection();
            //--- ---\\

            // ── REGISTER SERVICES ────────────────────\\
                //--- AddSingleton = create once, reuse forever ---\\
                //--- AddTransient = create fresh every time ---\\

                //--- File system service — reads photo folders ---\\
                    services.AddSingleton<IFileService, FileService>();
                //--- ---\\

                services.AddTransient<CollectionViewModel>();

                //--- Build and return the provider ---\\
                    return services.BuildServiceProvider();
                //--- ---\\
    }
    // ══════════════════════════════════════════════ \\

    // ══════════════════════════════════════════════ \\
    // FUNCTION — OnLaunched
    // Runs automatically when the app is launched
    // This is where we create and show the window
    // ══════════════════════════════════════════════ \\
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            //--- Create the main window ---\\
                CurrentWindow = new MainWindow();
        //--- ---\\

        //--- Show it on screen ---\\
            CurrentWindow.Activate();
        //--- ---\\
        }
    // ══════════════════════════════════════════════ \\

}