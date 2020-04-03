using System;
using BenchmarkDotNet.Running;

namespace StructValueTuplePerf
{
   class Program
   {
      static void Main(string[] args)
      {
         BenchmarkRunner.Run<StructTupleCompare>();
      }
   }
}
