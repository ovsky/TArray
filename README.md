# ⏺️ TArray - Ultimate Serializable Array [C# / Unity]

![TArray Logo](https://i.ibb.co/N7bpHv6/xxrect356.png)

#### **TArray** is incredibly fast, universal and serializable array solution for C# and Unity.

The main adventage of **TArray** in comparison to others is that this solution combines unbelievably simple and low amout of code [totally 180 lines] with nearly every expected feature, beacuse of fullness of well-written approaches.

Another is that the **TArray** is not a class like many other extended arrays, but still a structure like classic array - so it is really fast and memory safe and friendly, and also support enumerations and LINQ expressions natively.


## 📘 Installation

### You can install it by:

1. Using Unity Git packages support:
```typescript
Toolabar -> Window/Package Manager -> ＋ -> Add package from Git URL -> "https://github.com/ovsky/TArray.git#upm"
```
2. Unity Git URL suppport, by adding this entry in your packages *manifest.json*:
```typescript
"com.ovsky.tarray": "https://github.com/ovsky/TArray.git#upm"
```
3. Using Unity tarball support and *wget* [Linux] or *start* [Windows] download:

```typescript
Linux: "wget https://github.com/ovsky/TArray/tarball/upm -O ovsky.tarray-upm.tgz"
Windows: "start https://github.com/ovsky/TArray/tarball/upm"
```
```typescript
Use: Toolabar -> Window/Package Manager -> ＋ -> Add package from tarball -> Select: "ovsky.tarray-*.tgz"
```


The *upm* stands for Unity Package Manager - the default manager for Unity-ready external packages/extensions.


## 📖 QuickStart

### Code:
#### So, let's see the most important TArray features:

Multiple declaration possibilities:

```csharp
TArray<int> integerArray = new TArray<int>(5, 5);  // Classic declaration
TArray<int> integerArray = new int[5, 5];          // Array attributed declaration
TArray<int> integerArray = new Array[5, 5];        // Classic array declaration
TArray<int> integerArray = new (5, 5);             // Modern .NET declaration
```
---
Easy conversion support:

```csharp
TArray<int> integerArray = stringArray.Cast<int>();       // Convert string to int array
TArray<string> stringArray = customArray.Cast<string>();  // Convert custom to string array
```
---
Different value `get`/`set` options:

```csharp
int element = integerArray[2, 2];     // Get value using attributes
int element = integerArray.Get(2,2);  // Get value using method

integerArray[2, 2] = 1;               // Set value using attributes
integerArray.Set(2,2, 1);             // Set value using method
```
---
All LINQ operations support:

```csharp
TArray<Pets> petsArray = petsArray.Array.OrderBy(pet => pet.Age).ToArray();    // Sort class array by class property value
TArray<Pets> dogsArray = petsArray.Array.Select(pet => pet is Dog).ToArray();  // Select Dogs from Pets array
TArray<int> integerArray = integerArray.Array.OrderBy(i => i).ToArray();       // Sort int array values
```
---
Type-free array operations:

```csharp
ITArray valueArray;
valueArray = new TArray<int>(5, 5);                   // Declare int array at the field
valueArray = valueArray.Cast<string>();               // Cast the array to string values and apply to undefined field
valueArray = new TArray<bool>(5, 5).Set(0,0, true);   // Then change it to bool array with (0, 0) set to true
TArray<bool> boolArray = valueArray;                  // Apply the undefined value array to array with defined type


```

And more!


## 🗂️ Editor

**TArray Editor** (Property Drawer actually) - is an extendable, flexible, and the most important: it supports all serializable types (existing and user-defined) out of the box, without creating the new Unity Inspector or Editor!


#### Integer Preview:

![TArray Integer Preview](https://i.ibb.co/QfVG8Sy/aint.png)

#### GameObject Preview:

![TArray GameObject Preview](https://i.ibb.co/mTC6JCs/ago.png)

#### Boolean Preview:

![TArray Boolean Preview](https://i.ibb.co/YjT1J3B/abool.png)

## 📝 License:

```
Copyright (C) 2023 - Przemysław Orłowski
```

```
"THE SOFTWARE IS PROVIDED 'AS IT IS' (...)"
also known as:

**The MIT License**: 
https://choosealicense.com/licenses/mit/
```


