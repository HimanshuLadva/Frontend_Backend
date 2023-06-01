using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsFunction
{
    class Program
    {
        //function
        public void Show(string message)
        {
            Console.WriteLine("Hii" + message);
        }

        // call by value
        public string Foo(string message)
        {
            return "hii hello" + message;
        }

        //call by reference
        public void fun(ref int i)
        {
            i *= i;
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            /*program.Show("hello Himanshu");
            string data = program.Foo("Ladva");
            Console.WriteLine(data);*/
            int i = 50;
            Console.WriteLine(i);
            program.fun(ref i);
            Console.WriteLine(i);
        }
    }
}
