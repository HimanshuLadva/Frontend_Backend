using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CsObject_class
{
    // CLASS
    /*public class Testing
    {
        //CONSTRUCTOR   
        public int id;
        public String name;
        public Testing(int i, string n)
        {
            Console.WriteLine("Start of class");
            this.id = i;
            this.name = n;
        }

        public void insert(int i, string n)
        {
            id = i;
            name = n;
        }
        public void display()
        {
            Console.WriteLine("Hello Himanshu " + id + " " + name);
        }
        ~Testing()
        {
            Console.WriteLine("this is end of class");
        }
    }*/

    //static memeber
    /*public class Account
    {
        public int accno;
        public String name;
        public static float rateOfInterest = 8.8f;
        public static int count = 0;

        public Account(int accno, String name)
        {
            this.accno = accno;
            this.name = name;
            count++;
        }
        public void display()
        {
            Console.WriteLine("Hello " + accno + " " + name + " " + Account.rateOfInterest);
            Console.WriteLine("Hello "+ count);
        }

    }*/

    // static class
   /* public static class StaticMembers{
        public static string name = "Himanshu";
        public static int val = 123;
        
        public static void fun()
        {
            Console.WriteLine("hello i am in static class");
        }
    }*/

    // static constructor 
    /*public class funny
    {
        public static int val;
        public int val1;
        public funny()
        {
            Console.WriteLine("class funny");
        }
        static funny()
        {
            val = 12;
            Console.WriteLine("static funny");
        }
    }*/

    // struct
    /*public struct Rectangle
    {
        public int height, width;
        public Rectangle(int w, int h)
        {
            height = w;
            width = h;
        }
        public void area()
        {
            Console.WriteLine("Area of Rec "+(width * height));
        }
    }*/

    //enum
    public enum Season { WINTER, SUMMER, MOONSOON, FALL};
    public enum days { sun = 10, mon=9, tue=15, wed=1, thu=5, fri=100, sat=78};

    class Program
    {
        static void Main(string[] args)
        {
            // ENUM
            int x = (int)Season.SUMMER;
            int y = (int)days.fri;
            Console.WriteLine(x+" "+y);

            foreach(days d in Enum.GetValues(typeof(days)))
            {
                Console.WriteLine(d);
            }

            //Struct
           /* Rectangle r = new Rectangle(5, 6);
            r.area();*/

            // static constructor
            /*funny f = new funny();
            Console.WriteLine(funny.val);*/

            // static class
            /*StaticMembers.fun();
            Console.WriteLine(StaticMembers.name);
            Console.WriteLine(StaticMembers.val);*/

            //static
            /*Account a1 = new Account(123456, "karan ladva");
            Account a2 = new Account(123456, "Himanshu ladva");
            a1.display();
            // changing value of static field
            Account.rateOfInterest = 10.6f;
            a1.display();*/

            // class object constructor destructor this
            /*Testing testing = new Testing(101, "ladva");
            Testing testing1 = new Testing(101, "ladva");
            testing.id = 100;
            testing.name = "Hinmanshu";
            testing.insert(100, "Ladva");
            testing.display();*/
        }
    }
}
