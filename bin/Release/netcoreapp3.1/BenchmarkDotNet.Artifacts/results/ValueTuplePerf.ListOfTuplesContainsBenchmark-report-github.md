``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.200
  [Host]     : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
  DefaultJob : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT


```
|             Method |         Mean |       Error |      StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------- |-------------:|------------:|------------:|------:|------:|------:|----------:|
|  ListContainsTuple |  62,713.7 us | 1,242.90 us | 1,821.82 us |     - |     - |     - |       6 B |
| ListContainsStruct |  38,893.5 us |   832.35 us | 1,111.16 us |     - |     - |     - |         - |
|   MapContainsTuple |     267.6 us |     5.17 us |     7.24 us |     - |     - |     - |         - |
|  MapContainsStruct | 115,808.9 us | 2,256.40 us | 3,512.95 us |     - |     - |     - |         - |
