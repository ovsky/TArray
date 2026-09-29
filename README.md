<div align="center">

![TArray Logo](https://i.ibb.co/N7bpHv6/xxrect356.png)

# ⏺️ TArray – High-Performance 2D Serializable Arrays for Unity

**The ultimate lightweight struct-based array solution for C# / Unity**

[![Stars](https://img.shields.io/github/stars/ovsky/TArray?style=flat-square)](https://github.com/ovsky/TArray/stargazers)
[![License](https://img.shields.io/badge/license-MIT-blue?style=flat-square)](LICENSE)
[![Unity](https://img.shields.io/badge/Unity-2022.2%2B-blueviolet?style=flat-square)](https://unity.com)
[![Package](https://img.shields.io/badge/UPM-com.ovsky.tarray-brightgreen?style=flat-square)](https://github.com/ovsky/TArray)

</div>

---

## ✨ What's TArray?

**TArray** is an ultra-compact, production-ready 2D array solution for Unity that combines **blazing speed** with **full serialization support**. Unlike traditional array wrappers implemented as classes, TArray is a **value-type struct** (just 180 lines of code) that provides:

- 🚀 **Lightning-fast performance** – Struct-based with zero allocation overhead
- 📐 **True 2D access** – Intuitive `array[x, y]` syntax with bounds checking
- 💾 **Full serialization** – Works seamlessly with Unity's Inspector and SaveGame systems
- 🔄 **Type-safe casting** – Convert between array types without data loss
- 🎨 **Custom Inspector** – Visual grid editor for all serializable types
- 🔗 **LINQ integration** – Full support for `OrderBy`, `Select`, `Where`, etc.

Perfect for **grids, tile maps, game boards, 2D data structures**, and any multidimensional data management.

---

## 🎯 Key Features

### Multiple Declaration Styles
```csharp
TArray<int> grid = new TArray<int>(5, 5);          // Classic
TArray<int> grid = new int[5, 5];                  // Array-like
TArray<int> grid = new (5, 5);                     // Modern C#
```

### Intuitive Get/Set Operations
```csharp
int value = grid[2, 2];                            // Index access
int value = grid.Get(2, 2);                        // Method access
grid[2, 2] = 42;
grid.Set(2, 2, 42);                                // Chainable
grid.Set(2, 2, 42, resize: true);                  // Auto-resize
```

### Type-Free Operations
```csharp
ITArray flexArray = new TArray<int>(5, 5);
flexArray = flexArray.Cast<string>();             // Type conversion
TArray<string> stringGrid = flexArray;             // Typed retrieval
```

### Full LINQ Support
```csharp
TArray<Enemy> enemies = new TArray<Enemy>(10, 10);

// Sort by property
var sorted = enemies.Array.OrderBy(e => e.Health).ToArray();

// Filter and transform
var bosses = enemies.Array.Where(e => e.IsBoss).ToArray();
```

### Array Manipulation
```csharp
grid.Resize(10, 10);                               // Resize with data preservation
grid.AddSize(2, 3);                                // Expand grid
grid.AddRow(newXValues, newYValues);               // Append rows
```

---

## 📦 Installation

### Via Unity Package Manager (Recommended)

**Method 1: Git URL**
```
Toolbar → Window/Package Manager → ＋ → Add package from Git URL
https://github.com/ovsky/TArray.git#upm
```

**Method 2: Manifest Edit**
```json
// packages/manifest.json
{
  "dependencies": {
    "com.ovsky.tarray": "https://github.com/ovsky/TArray.git#upm"
  }
}
```

**Method 3: Tarball**
```bash
# Linux
wget https://github.com/ovsky/TArray/tarball/upm -O ovsky.tarray-upm.tgz

# Windows
start https://github.com/ovsky/TArray/tarball/upm
```
Then in Package Manager: `＋ → Add package from tarball` → select the `.tgz` file

---

## 🚀 Quick Start

### Basic Usage
```csharp
using Extensions;

// Create a 5×5 integer grid
TArray<int> grid = new TArray<int>(5, 5);

// Access elements
grid[0, 0] = 100;
int val = grid[2, 3];

// Query and transform with LINQ
var flattened = grid.Array;
var sorted = grid.Array.OrderBy(x => x);
```

### Working with Custom Types
```csharp
[Serializable]
public class Cell
{
    public int health;
    public bool walkable;
}

TArray<Cell> terrain = new TArray<Cell>(16, 16);
terrain[8, 8].walkable = false;

// Full LINQ support
var walkableCells = terrain.Array
    .Where(c => c.walkable)
    .OrderBy(c => c.health)
    .ToArray();
```

### Inspector Integration
TArray fields appear in the Inspector with a **custom grid editor**:
- See all values at a glance
- Edit directly in the grid
- Supports integers, booleans, GameObjects, strings, and custom types
- Auto-resize capability

---

## 🎨 Editor Preview

<div align="center">

**Integer Grid**
![TArray Integer Preview](https://i.ibb.co/QfVG8Sy/aint.png)

**GameObject References**
![TArray GameObject Preview](https://i.ibb.co/mTC6JCs/ago.png)

**Boolean Grid**
![TArray Boolean Preview](https://i.ibb.co/YjT1J3B/abool.png)

</div>

---

## 📊 Architecture

### Core Structure
```
Packages/
  manifest.json              UPM metadata

Assets/Lab7/
  package.json               Package info (v1.1.0, com.ovsky.tarray)
  TArray/
    Scripts/
      Runtime/
        TArray.cs            → Core struct (ITArray interface, indexing, operations)
      Editor/
        TArrayDrawer.cs      → Custom Inspector grid editor
```

### How It Works

**TArray** uses a **flat backing array** with intelligent 2D indexing:

```csharp
// Internal representation: 1D array stored linearly
data = [val[0,0], val[1,0], val[2,0], val[0,1], val[1,1], val[2,1], ...]

// Index calculation (row-major):
index = (width * y) + x
```

This design gives you:
- ✅ Single allocation (cache-friendly)
- ✅ Struct value semantics (stack-allocated, no GC pressure)
- ✅ Full Unity serialization
- ✅ LINQ compatibility (via `.Array` property)

---

## 💡 Use Cases

- 🎮 **Game Boards** – Chess, checkers, turn-based tactics
- 🗺️ **Tile Maps** – Level layouts, terrain grids, procedural generation
- 📈 **Data Grids** – Spreadsheets, simulation grids, sensor arrays
- 🎯 **Spatial Indexing** – Collision detection, spatial partitioning
- 🎨 **Pixel Data** – Image processing, texture manipulation

---

## 🔧 API Reference

### Properties
| Property | Description |
|----------|-------------|
| `Size` | Returns `Vector2Int` with grid dimensions (width, height) |
| `Length` | Total number of elements |
| `Array` | Access underlying 1D array for LINQ operations |
| `Matrix` | Convert to native C# 2D array `T[,]` |

### Methods
| Method | Description |
|--------|-------------|
| `Get(x, y)` | Retrieve value with bounds checking |
| `Set(x, y, value)` | Set value and return for chaining |
| `Set(x, y, value, resize)` | Set with optional auto-resize |
| `Cast<T1>()` | Convert to different type |
| `Resize(x, y, force)` | Change grid dimensions |
| `AddSize(x, y)` | Expand grid by offset |
| `AddRow(xValues, yValues)` | Append rows |

### Operators
```csharp
TArray<int> grid = new int[5, 5];          // T[] implicit operator
TArray<int> grid = new (5, 5);             // T[,] implicit operator
int[] flat = grid;                         // Implicit to T[]
int[,] matrix = grid;                      // Implicit to T[,]
```

---

## 🛡️ Safety Features

**Built-in Bounds Checking** (Development builds):
```csharp
#if UNITY_EDITOR || DEVELOPMENT_BUILD
if (x < 0 || x >= size.x || y < 0 || y >= size.y)
{
    Debug.LogError($"TArray[{x}, {y}] is out of bounds of size: {size}");
    return default;
}
#endif
```

**Zero-Allocation Indexer** (Release builds):
```csharp
// Optimized path with no safety checks
public T this[int x, int y]
{
    get => data[(size.x * y) + x];
    set => data[(size.x * y) + x] = value;
}
```

---

## 📝 License

```
Copyright (C) 2023 - Przemysław Orłowski

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
EXPRESS OR IMPLIED.

Licensed under the MIT License – see LICENSE file for full text.
```

---

## 🙋 FAQ

**Q: How does TArray compare to native `int[,]`?**  
A: TArray serializes natively in Unity (no custom handlers needed), provides a custom Inspector, and has identical performance. The trade-off is minimal—you get Editor tooling for free.

**Q: Can I use TArray with nested structures?**  
A: Yes! Any serializable type works—custom classes, structs, GameObjects, and even lists (as elements).

**Q: What's the memory overhead?**  
A: Just two fields: `Vector2Int size` (8 bytes) + reference to `T[] data`. No wrapper overhead beyond the native array.

**Q: Does it work with Save/Load systems?**  
A: Yes. Full `[Serializable]` support means it works with Unity's serialization, JSON, and binary formatters.

---

<div align="center">

**Made with ❤️ by [Lab7 Studio](https://www.orlowski.work/)**

[GitHub](https://github.com/ovsky/TArray) • [Issues](https://github.com/ovsky/TArray/issues) • [Discussions](https://github.com/ovsky/TArray/discussions)

</div>
