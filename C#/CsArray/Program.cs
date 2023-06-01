using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsArray
{
    class Program
    {
        public void PrintArray(int []arr)
        {
            Console.WriteLine("Printing Array");
            for(int i=0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }
                    
        public void Show(params object[] items)
        {
            for(int i =0; i < items.Length; i++)
            {
                Console.WriteLine(items[i]);
            }
        }
        static void Main(string[] args)
        { 
            Program program = new Program();

            //COMMAND LINE ARGUMENT
            Console.WriteLine("Argument length: " + args.Length);
            Console.WriteLine("supplied argument are:");
            foreach(Object obj in args)
            {
                Console.WriteLine(obj);
            }


            // Array Class
            /*int[] arr = new int[] { 7,6,5,4,3,2};
            int[] arr1 = new int[6];
            Array.Sort(arr);
            Array.Copy(arr, arr1, arr.Length);
            Array.Reverse(arr1);
           

            for (int i = 0; i < arr1.Length; i++)
            {
                 Console.Write(arr1[i] + " ");
            }*/

            //PARAMS
            /*//program.Show(1, 2, 3, 4, 5, 6, 7, 8);
            program.Show("himanshu", "darshan", 12, 13, true, "ladva");*/

            // jagged array
            /*int[][] arr = new int[2][];
            arr[0] = new int[] { 11, 21, 56, 78 };
            arr[1] = new int[] { 45, 67, 89, 23, 46, 13 };

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    System.Console.Write(arr[i][j] + " ");
                }
                System.Console.WriteLine();
            }

            int[][] arr1 = new int[3][]
            {
                new int[] {11,21,31,41,51},
                new int[] {11,22,33,44,55,66,77},
                new int[] {1,2,3,4,5}
            }; 

            for(int i = 0; i < arr1.Length; i++)
            {
                for(int j =0; j < arr1[i].Length; j++)
                {
                    System.Console.Write(arr1[i][j] + " ");
                    //for(int k = 0; k < arr1[i][j]; k++)
                    //{
                    //    System.Console.Write(arr1[i][j][k] + " ");
                    //}
                    //System.Console.WriteLine();
                }
                System.Console.WriteLine();
            }*/

            // multidimentional array
            /* int [,] arr1=new int[3, 3]{ { 1,2,3}, { 4,5,6}, { 7,8,9} };
             for (int i = 0; i < 3; i++)
             {
                 for (int j = 0; j < 3; j++)
                 {
                     Console.Write(arr1[i, j] + " ");
                 }
                 Console.WriteLine(); 
             }*/

            //one dimentional array
            /*//int[] arr = new int[5] {10,20,30,40,60};
            //int[] arr = new int[] { 10, 20, 30, 40, 50, 60};
            int[] arr = { 10, 20, 30, 40, 50 };

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }

            //for each loop
            foreach (int i in arr)
            {
                Console.WriteLine(i);
            }

            program.PrintArray(arr);*/
        }
    }
}