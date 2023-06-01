using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            // addition program
            /*int a = 10;
            int b = 9;
            int total = a + b;

            if (total == 15)
            {
                Console.Write("total is 15");
            }
            else
            {
                Console.Write("total is another");
            }
            Console.WriteLine("Enter Number");
            int num = Convert.ToInt32(Console.ReadLine());

            if (num % 2 == 0)
            {
                Console.WriteLine("It is even number");
            }
            else
            {
                Console.WriteLine("It is odd number");
            }
            Console.ReadLine();*/

            // check grade
            /*Console.WriteLine("Enter a number to check grade:");
            int num = Convert.ToInt32(Console.ReadLine());

            if (num < 0 || num > 100)
            {
                Console.WriteLine("wrong number");
            }
            else if (num >= 0 && num < 50)
            {
                Console.WriteLine("Fail");
            }
            else if (num >= 50 && num < 60)
            {
                Console.WriteLine("D Grade");
            }
            else if (num >= 60 && num < 70)
            {
                Console.WriteLine("C Grade");
            }
            else if (num >= 70 && num < 80)
            {
                Console.WriteLine("B Grade");
            }
            else if (num >= 80 && num < 90)
            {
                Console.WriteLine("A Grade");
            }
            else if (num >= 90 && num <= 100)
            {
                Console.WriteLine("A+ Grade");
            }*/

            //Switch Statement
            /*switch(10)
            {
                case 1:
                    Console.WriteLine("Hello i am 1");
                    break;
                case 10:
                    Console.WriteLine("Hello i am 10");
                    break;
            }*/

           // loop
           for(int j = 0; j <= 5; j++)
            {
                Console.WriteLine(j);
            }
            int i = 0;
            while(i < 5)
            {
                Console.WriteLine(i);
                i++;
            }

        }
    }
}
