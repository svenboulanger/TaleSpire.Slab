# TaleSpire.Slab
A .NET Standard 2.0 library for importing and exporting slabs for TaleSpire. The library supports both version and version 2 slabs.

## Usage

You can load slabs using

```csharp
using TaleSpire.Slab;

// Import a slab (any version)
var slab = Slab.Import(slabString);

// Export a slab
string newSlabString = slab.Export();
```

## Making slabs from scratch

You can also build slabs from scratch for either version 1 or version 2 by using the right namespace.

```csharp
using TaleSpire.Slab.V2;

// Create a layout of assets
// First we make the header that tells how many assets there are and which tile it is
var layoutHeader = new LayoutData(tileNGUID, numberOfAssets);
var layout = new Layout(layoutHeader);

// We then specify the location of all the instances of the tile
for (int i = 0; i < numberOfAssets; i++)
{
    layout.Assets[i] = new AssetData(position, rotation, extraBits);
}

// We can finally create the slab
var slab = new Slab(new[] { layout });
```
