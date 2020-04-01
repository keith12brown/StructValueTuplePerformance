using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace ValueTuplePerf
{
   [MemoryDiagnoser]
   //[SimpleJob(targetCount: 5)]
   public class ListOfTuplesContainsBenchmark
   {
      private const int RUNS = 10000;

      private const int MID = RUNS / 2;

      private List<(int Age, int Salary)> _persons;

      private List<Persons> _spersons;

      private Dictionary<(int Age, int Salary), (int Age, int Salary)> _personsMap = new Dictionary<(int Age, int Salary), (int Age, int Salary)>();

      private Dictionary<Persons, Persons> _spersonsMap = new Dictionary<Persons, Persons>();

      [GlobalSetup]
      public void GlobalSetup()
      {
         _persons = Enumerable
            .Range(0, RUNS)
            .Select(i => (Age: i, Salary: i))
            .ToList();

         _spersons = Enumerable
         .Range(0, RUNS)
         .Select(i => new Persons() { Age = i, Salary = i })
         .ToList();

         var x_persons = Enumerable
            .Range(0, RUNS)
            .Select(i => new { Key = (Age: i, Salary: i), Value = (Age: i, Salary: i) });
         foreach (var item in x_persons)
         { 
            _personsMap[item.Key] = item.Value;
         }

         foreach (var item in Enumerable
           .Range(0, RUNS)
           .Select(i => new Persons() { Age = i, Salary = i }))
         {
            _spersonsMap[item] = item;
         }
      }

      [Benchmark]
      public void ListTupleContains()
      {
         for (var i = 0; i < RUNS; i++)
         {
            _persons.Contains((MID, MID));
         }
      }

      [Benchmark]
      public void ListStructContains()
      {
         for (var i = 0; i < RUNS; i++)
         {
            _spersons.Contains(new Persons() { Age = MID, Salary = MID });
         }
      }

      [Benchmark]
      public void MapContainsTuple()
      {
         for (var i = 0; i < RUNS; i++)
         {
            _personsMap.ContainsKey((i, i));
         }
      }

      [Benchmark]
      public void MapContainsStruct()
      {
         for (var i = 0; i < RUNS; i++)
         {
            _spersonsMap.ContainsKey(new Persons() { Age = i, Salary = i });
         }
      }
   }


   public struct Persons : IEquatable<Persons>
   {
      public int Age;
      public int Salary;

      public bool Equals([AllowNull] Persons other) => other.Age == Age && other.Salary == Salary;

   }
}
