//=== Project: Aura Gallery - Clean and ad-free photo and video viewer and editor ===\\
//=== Created: 28/03/2026 in Litomyšl ===\\

using Microsoft.UI.Xaml;
using System.Collections;
using Aura_Gallery.Views;

namespace Aura_Gallery;

public sealed partial class MainWindow : Window
{
    // ══════════════════════════════════════════════ \\
    // CONSTRUCTOR — creates the main window
    // ══════════════════════════════════════════════ \\
    public MainWindow()
    {
        InitializeComponent();

        
        Content = new CollectionPage();
    }
    // ══════════════════════════════════════════════ \\
    // END OF CONSTRUCTOR
    // ══════════════════════════════════════════════ \\
}