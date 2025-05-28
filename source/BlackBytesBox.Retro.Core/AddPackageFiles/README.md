# BlackBytesBox.Retro.Core

*A streamlined .NET helper for precise control over the Windows console.*

---

## ⭐ Features

- **Attach or create** a console from any GUI process with one call.
- **Disable** the ❌ Close button and Quick-Edit to avoid accidental termination or freezes.
- **UTF-8 everywhere** – print emoji 🎉 and non-Latin scripts seamlessly.
- **Programmatic font switch** (Lucida Console, Consolas, Cascadia Mono, etc.).
- **Move & resize** using *client-area* coordinates for pixel-perfect placement.

---

## 📦 Installation

```bash
# .NET CLI
dotnet add package BlackBytesBox.Retro.Core
```

Targets **.NET 6+** and **.NET Framework 4.7.2+**.

---

## 🚀 Quick Start

```csharp
using BlackBytesBox.Retro.Core;

// 1. Prepare the console
var c = NativeMethods.ConsoleManager;
c.EnsureConsole();
c.DisableCloseButton();
c.DisableQuickEdit();
c.SetFont(NativeMethods.ConsoleManager.ConsoleFont.LucidaConsole);
c.EnableUtf8();

// 2. Align client-area to (0,0)
var deco = NativeMethods.WindowManager.GetWindowDecorations();
c.MoveAndResize(x: 0 - deco.BorderWidth, y: 0);
```

Run once – you’ll have a UTF-8 console anchored to the primary screen’s top-left.

---

## 🧩 API Overview

```text
NativeMethods
├── ConsoleManager
│   ├── EnsureConsole()
│   ├── DisableCloseButton()
│   ├── DisableQuickEdit()
│   ├── SetFont(ConsoleFont font)
│   ├── EnableUtf8()
│   └── MoveAndResize(int x, int y, int? w = null, int? h = null)
└── WindowManager
    └── GetWindowDecorations() : WindowDecorations
```

Full XML docs with nullable annotations ship with the package.


## 📜 License

MIT © BlackBytesBox 2025

> *Built with ❤️ by BlackBytesBox*
