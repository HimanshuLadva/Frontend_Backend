using System;
//using second;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*namespace first
{
    public class Hello
    {
        public void sayHello()
        {
            Console.WriteLine("hello first namespace");
        }
    }
}
namespace second
{
    public class Hii
    {
        public void sayHi()
        {
            Console.WriteLine("hello second namespace");
        }
    }
}*/
namespace CsOOP
{
    // inheritance
    /* public class Employee
     {
         public float salary = 40000;
     }
     public class Programmer:Employee
     {
         public float bonus = 10000;
     }
     public class fullStack:Programmer
     {
         public float mergeSalary = 600000;
     }*/

    // aggregation
    /*public class Address
    {
        public string addressLine, city, state;
        public Address(string addressLine, string city, string state)
        {
            this.addressLine = addressLine;
            this.city = city;
            this.state = state;
        }
    }

    public class Employee
    {
        public int id;
        public string name;
        public Address address;

        public Employee(int id, string name, Address address)
        {
            this.id = id;
            this.name = name;
            this.address = address;
        }

        public void display()
        {
            Console.WriteLine(id+" "+name+" "+address.addressLine+" "+address.city+" "+address.state);
        }
    }*/

    // method overloding
    /*public class methodOl
    {
        public int add(int a, int b)
        {
            Console.WriteLine("you are in int");
            return a + b;
        }
        public float add(float a, float b)
        {
            Console.WriteLine("you are in float");
            return a + b; 
        } 
    } */

    // method overridding
    /*public class Animal
    {
        virtual public void eat()
        {
            Console.WriteLine("Animal is eatting");
        }
    }

    public class eagel:Animal
    {
        override public void eat()
        {
            Console.WriteLine("Eagel is eatting");
        }
    }*/

    // base keyword
    /*public class Animal
    {
        public string color = "white";

        public Animal()
        {
            Console.WriteLine("this is animal class");
        }
    }

    public class dogi:Animal
    {
        public string color = "black";

        public dogi()
        {
            Console.WriteLine("this is dogi class");
        }
        public void display()
        {
            Console.WriteLine(color);
            Console.WriteLine(base.color);
        }
    }*/

    //sealed  
    /*public class Animal
    {
        virtual public void run()
        {
            Console.WriteLine("i am run");
        }
        virtual public void eat()
        {
            Console.WriteLine("i am eat");
        }
    }
    public class Dog:Animal
    {
        public override void run()
        {
            Console.WriteLine("dog running");
        }
        public sealed override void eat()
        {
            Console.WriteLine("dog eatting");
        }
    }

    public class Puppy:Dog
    {
        public override void run()
        {
            Console.WriteLine("puppy running");
        }
        public override void eat()
        {
            Console.WriteLine("puppy eatting");
        }
    }*/

    // Abstract class
    /*public abstract class Shape
    {
        public abstract void draw();
        public void foo()
        {
            Console.WriteLine("hello i am in shape");
        }
    }
    public class Rectangle:Shape
    {
        public override void draw()
        {
            Console.WriteLine("Rectangle draw method");
        }
    }

    public class Circle: Shape
    {
        public override void draw()
        {
            Console.WriteLine("Circle draw method");
        }
    }*/

    //interface
    /*public interface Drawable
    {
        // interface method is by default abstract and public
        void draw();
    }
    public class Rectangle:Drawable
    {
        public void draw()
        {
            Console.WriteLine("drawing rectangle");
        }
    }

    public class Circle: Drawable
    {
        public void draw()
        {
            Console.WriteLine("drawing circle");
        }
    }*/

    // Access specifier
    class ProtectedTest
    {
        protected string name = "Himanshu";
        protected void msg(string msg)
        {
            Console.WriteLine("Hello "+ msg);
        }
    }

    class Child:ProtectedTest
    {
        public void display()
        {
            Console.WriteLine("my name is "+name);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Access specifier
            Child c = new Child();
            //ProtectedTest pt = new ProtectedTest();
            //Console.WriteLine(pt.name);
            c.display();
           

            // namespace
            /*first.Hello h1 = new first.Hello();
            h1.sayHello();
            Hii h2 = new Hii();
            h2.sayHi();*/

            // interface
            /*Drawable d1;
            d1 = new Circle();
            d1.draw();
            d1 = new Rectangle();
            d1.draw();*/

            // Abstract class
            /*Circle c1 = new Circle();
            c1.draw();*/

            // sealed keyword
            /*Dog d1 = new Dog();
            d1.run();
            d1.eat();*/

            // base keyword
            /*dogi d1 = new dogi();
            d1.display();*/

            // method overriding
            /*eagel e1 = new eagel();
            e1.eat();*/

            // method overloding
           /* methodOl m1 = new methodOl();
            Console.WriteLine(m1.add(1.8f,2.5f));*/

            // aggregation
            /*Address a1 = new Address("shapar", "rajkot", "gujarat");
            Employee e1 = new Employee(101, "himanshu",a1);
            e1.display();*/

            // inheritance
            /*fullStack f1 = new fullStack();
            Programmer p1 = new Programmer();
            Console.WriteLine("Salary"+ " "+f1.salary);
            Console.WriteLine("Bonus" +" "+f1.bonus);
            Console.WriteLine("mergeSalary"+" "+f1.mergeSalary);*/
        }
    }
}
