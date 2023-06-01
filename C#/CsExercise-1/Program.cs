using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsExercise_01
{
    class Program
    {
        static void Main(string[] args)
        {
            int row, x = 0, y = 1;
            Console.WriteLine("Enter the number");
            row = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= row; i++)
            {
                y = 1;
                for(int j = 1; j <= i + x; j++)
                {
                    if (j <= ((i + x) / 2))
                    {
                        Console.Write((char)(y + 64));
                        y++;
                    }
                    else
                    {
                        Console.Write((char)(y + 64));
                        y--;
                    }
                }
                x++;
                Console.WriteLine();
            }
        }
    }
}
