using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsExceptionHandling
{
    // user defined exception
    public class InvalidAgeException:Exception
    {
        public InvalidAgeException(string msg):base(msg)
        {
            Console.WriteLine(msg);
        }
    }
    class Program
    {
        static void validate(int age)
        {
            if(age < 18)
            {
                throw new InvalidAgeException("You are not eligible");
            }
        }
        static void Main(string[] args)
        {
            // SystemException class
            /*try
            {
                int[] arr = new int[5];
                arr[10] = 145;
            }
            catch(SystemException e)
            {
                Console.WriteLine(e);
            }*/

            // checked and unchecked exception
            /*unchecked
            {
                int val = int.MaxValue;
                Console.WriteLine(val + 2);
            }*/

            // user defined exception
            /*try
            {
                validate(12);
            }
            catch (InvalidAgeException e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("Rest of code work here");*/

            // try-catch block
            /*try
            {
                int a = 10;
                int b = 0;
                int c = a / b;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e);
            }
            // finally block
            finally
            {
                Console.WriteLine("finally block is executed");
            }
            Console.WriteLine("Rest of code work here");*/
        }
    }
}
