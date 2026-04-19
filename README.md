```
     ______                                       ______             __  __                               
    /      \                                     /      \           /  |/  |                              
   /$$$$$$  | __    __   ______   ______        /$$$$$$  |  ______  $$ |$$ |  ______    ______   __    __ 
   $$ |__$$ |/  |  /  | /      \ /      \       $$ | _$$/  /      \ $$ |$$ | /      \  /      \ /  |  /  |
   $$    $$ |$$ |  $$ |/$$$$$$  |$$$$$$  |      $$ |/    | $$$$$$  |$$ |$$ |/$$$$$$  |/$$$$$$  |$$ |  $$ |
   $$$$$$$$ |$$ |  $$ |$$ |  $$/ /    $$ |      $$ |$$$$ | /    $$ |$$ |$$ |$$    $$ |$$ |  $$/ $$ |  $$ |
   $$ |  $$ |$$ \__$$ |$$ |     /$$$$$$$ |      $$ \__$$ |/$$$$$$$ |$$ |$$ |$$$$$$$$/ $$ |      $$ \__$$ |
   $$ |  $$ |$$    $$/ $$ |     $$    $$ |      $$    $$/ $$    $$ |$$ |$$ |$$       |$$ |      $$    $$ |
   $$/   $$/  $$$$$$/  $$/       $$$$$$$/        $$$$$$/   $$$$$$$/ $$/ $$/  $$$$$$$/ $$/        $$$$$$$ |
                                                                                                /  \__$$ |
                                                                                                $$    $$/ 
                                                                                                 $$$$$$/  
```

> A clean, ad-free photo and video gallery for Windows. No OneDrive nagging. No Clipchamp. Just your photos.

![Platform](https://img.shields.io/badge/platform-Windows-blue)
![Framework](https://img.shields.io/badge/framework-.NET%2010-purple)
![UI](https://img.shields.io/badge/UI-WinUI%203-blue)
![Status](https://img.shields.io/badge/status-In%20Development-orange)

---

## What is Aura Gallery?

Aura Gallery is a Windows desktop photo and video viewer built as a clean alternative to Microsoft Photos. No ads, no OneDrive promotion, no Clipchamp pop-ups — just a fast, beautiful gallery for your local photos and videos.

---

## Screenshots

> 📸 *Screenshots coming soon*

---

## Features

- 🖼️ **Photo & video gallery** — view all your media in one place
- 📁 **Folder manager** — you choose exactly which folders to include, nothing is auto-loaded
- 🎛️ **Filter by type** — switch between All, Photos, and Videos instantly
- 🎨 **Floating panels** — acrylic navigation, actions, timeline and media panels
- 🌗 **Follows Windows theme** — light and dark mode support
- ⚡ **Fast async loading** — folders scan in the background, UI never freezes
- 🚫 **No OneDrive, no ads, no bloat**

---

## Building from Source

### Requirements

- Windows 10 version 1903 or later
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) with the **Windows App SDK** workload
- .NET 10 SDK

### Steps

1. Clone the repo:
```bash
git clone https://github.com/Stickytapeguy/Aura-Gallery.git
```

2. Open `Aura Gallery.slnx` in Visual Studio

3. Restore NuGet packages (VS does this automatically on first build)

4. Set the build target to `x64` and hit **F5**

---

## Tech Stack

| | |
|---|---|
| Language | C# / .NET 10 |
| UI Framework | WinUI 3 |
| MVVM | CommunityToolkit.Mvvm |
| Dependency Injection | Microsoft.Extensions.DependencyInjection |
| IDE | Visual Studio 2026 Insiders |

---

## Project Status

Aura Gallery is currently in early development. Core photo loading and the main grid view are working. Many features are still being built.

| Feature | Status |
|---|---|
| Photo grid | ✅ Working |
| Video support | ✅ Working |
| Folder manager | 🚧 In progress |
| Single photo view | 🔜 Planned |
| Fullscreen view | 🔜 Planned |
| Timeline panel | 🔜 Planned |
| Editor | 🔜 Planned |
| External drives | 🔜 Planned |

---

## License

This project is currently unlicensed — all rights reserved.
