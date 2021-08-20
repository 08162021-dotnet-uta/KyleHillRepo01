// Topics Discussed

// Type Inference (var)
// Parsing
// Casting (implicit, explicit)
// Scopes (namespace, class, method, block)
// Single Responsibility (part of SOLID)
// DRY (don't repeat yourself)
// Method (signature, parameter, argument)
// Primitive Types (int, bool, string, float, double)
// Type Families (value, reference)
// Method Signature (access modifier, optionally non-access modifiers, return type, identifier, optionally parameters)
// Exception Handling

using System;

namespace Calculator
{
  class Program
  {
    static void Main(string[] args)
    {

      // int x = 10;
      // long y = x; // implicit casting

      // long x = 10;
      // int y = (int)x; // checked, unchecked exception, explicit casting
      // int z = x as int; // null, 0 explicit casting

      // input stuff
      // int input1, input2;

      // if (Input(out int input1, out int input2))
      // {
      //   // compute stuff
      //   int result1 = Add(input1, input2);
      //   int result2 = Subtract(input1, input2);

      //   // output stuff
      //   Print(result1, result2, result1, result2);
      // }

      int data = 10; // value type
      Int32 data2 = new Int32(); // reference


      PassByValueNoBoxing(data);
      System.Console.WriteLine(data); // 10

      PassByValueWithBoxing(ref data);
      System.Console.WriteLine(data); // 20
    }

    static void PassByValueNoBoxing(int entry)
    {
      entry = 20;
    }

    static void PassByValueWithBoxing(ref int entry)
    {
      entry = 20;
    }

    static int Add(int input1, int input2)
    {
      // compute stuff
      return (int)input1 + (int)input2; // type inference, casting
    }

    static int Subtract(int input1, int input2)
    {
      // compute stuff
      return (int)input1 - (int)input2; // type inference, casting
    }

    static void Print(params int[] results)
    {
      // output stuff
      foreach (var result in results) // type inference
      {
        Console.WriteLine(result);
      }
    }

    static bool CustomTryParse(string s, out int result)
    {
      try
      {
        result = int.Parse(s);

        return true;
      }
      catch
      {
        result = 0;
        return false;
      }
    }

    static bool Input(out int i1, out int i2)
    {
      // input stuff
      // int input1, input2;

      if (int.TryParse(Console.ReadLine(), out i1) & int.TryParse(Console.ReadLine(), out i2))
      {
        // i1 = input1;
        // i2 = input2;

        return true;
      }
      else
      {
        // i1 = i2 = 0;

        return false;
      }

      // try
      // {
      //   var input1 = int.Parse(Console.ReadLine()); //123a, 123 // type inference, parsing
      //   var input2 = int.Parse(Console.ReadLine()); // error

      //   return new int[] { input1, input2 };
      // }
      // catch (Exception ex)
      // {
      //   //throw ex; // points to the original error
      //   throw new Exception("we are sorry for the inconvenience", ex); // create a new error
      // }
      // finally
      // {
      //   // always run
      // }
    }
  }
}
