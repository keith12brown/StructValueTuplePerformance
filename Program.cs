using System;
using BenchmarkDotNet.Running;

namespace ValueTuplePerf
{
   class Program
   {
      static void Main(string[] args)
      {
         BenchmarkRunner.Run<ListOfTuplesContainsBenchmark>();
      }
   }
}
