using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace StructValueTuplePerf
{
   [MemoryDiagnoser]
   //[SimpleJob(targetCount: 5)]
   public class StructTupleCompare
   {
      private const int RUNS = 10000;

      private const int MID = RUNS / 2;

      private List<(int Age, int Salary)> _persons;

      private List<Person> _spersons;

      private Dictionary<(int Age, int Salary), (int Age, int Salary)> _personsMap = new Dictionary<(int Age, int Salary), (int Age, int Salary)>();

      private Dictionary<Person, Person> _spersonsMap = new Dictionary<Person, Person>();

      [GlobalSetup]
      public void GlobalSetup()
      {
         _persons = Enumerable
            .Range(0, RUNS)
            .Select(i => (Age: i, Salary: i))
            .ToList();

         _spersons = Enumerable
         .Range(0, RUNS)
         .Select(i => new Person() { Age = i, Salary = i })
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
           .Select(i => new Person() { Age = i, Salary = i }))
         {
            _spersonsMap[item] = item;
         }
      }

      [Benchmark]
      public void ListContainsTuple()
      {
         for (var i = 0; i < RUNS; i++)
         {
            _persons.Contains((MID, MID));
         }
      }

      [Benchmark]
      public void ListContainsStruct()
      {
         for (var i = 0; i < RUNS; i++)
         {
            _spersons.Contains(new Person() { Age = MID, Salary = MID });
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
            _spersonsMap.ContainsKey(new Person() { Age = i, Salary = i });
         }
      }
   }

   public struct Person : IEquatable<Person>
   {
      public int Age;
      public int Salary;

      public bool Equals([AllowNull] Person other) => other.Age == Age && other.Salary == Salary;

   }
}
