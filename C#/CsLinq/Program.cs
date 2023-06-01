using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsLinQ
{
    public class Student
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public List<string> Subject { get; set; }
    }
    public class Student2
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int rollNo { get; set; }
    }
    public class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Country { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Element Operators
            int[] numbers = { 1,2, 3, 4, 5 };
            // first
            //int result = numbers.First();

            //firstordefault
            //int result = numbers.FirstOrDefault();
            //int result2 = (from num in numbers select num).FirstOrDefault();

            // last
            //int result = numbers.Last();

            //lastordefault
            //int result = numbers.LastOrDefault();

            //Elementat
            //int result = numbers.ElementAt(20);

            //elementordefault
            //int result = numbers.ElementAtOrDefault(20);

            //single
            //int result = numbers.Single(x => x == 2);

            //singleordefault
            /*int result = numbers.SingleOrDefault(x => x == 3);
            Console.WriteLine(result);

            List<Student> objStudent = new List<Student>()
            {
                new Student() { Name = "Akshay Tyagi", Gender = "Male"},
                new Student() { Name = "Vaishali Tyagi", Gender = "Female"},
                new Student() { Name = "Arpita Rai", Gender = "Male"},
                new Student() { Name = "Shubham Rastogi", Gender = "Male"},
                new Student() { Name = "Aman Singhal", Gender = "Male"}
            };

            var result1 = objStudent.SingleOrDefault(i => i.Name == "Akshay Tyagi");
            Console.WriteLine(result1.Name);*/

            // defaultempty
            int[] b = { };
            int[] a = { 1, 2, 3, 4, 5 };

            var result = b.DefaultIfEmpty();
            foreach(int i in result)
            {
                Console.WriteLine(i);
            }

            //Conversion Operators
            /*string[] countries = { "US", "UK", "India", "Russia", "China", "Australia", "Argentina" };
            // tolist
            List<string> result = countries.ToList();
            // toarray
            string[] result = countries.ToArray();
            string[] result2 = (from m in countries select m).ToArray();
            foreach(string s in result2)
            {
                Console.WriteLine(s);
            }*/

            // tolookup
            /*List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            var resultNumbers = numbers.ToLookup(x => x);

            foreach(var i in resultNumbers)
            {
                Console.WriteLine(i.Key);
            }
            List<Employee> objEmployee = new List<Employee>()
            {
                new Employee(){ Name="Akshay Tyagi", Department="IT", Country="India"},
                new Employee(){ Name="Vaishali Tyagi", Department="Marketing", Country="Australia"},
                new Employee(){ Name="Arpita Rai", Department="HR", Country="China"},
                new Employee(){ Name="Shubham Ratogi", Department="Sales", Country="USA"},
                new Employee(){ Name="Himanshu Tyagi", Department="Operations", Country="Canada"}
            };

            var Emp = objEmployee.ToLookup(x => x.Department);
            foreach (var item in Emp)
            {
                Console.WriteLine(item.Key);
                foreach (var item1 in Emp[item.Key])
                {
                    Console.WriteLine(item1.Name + " " + item1.Department + " " + item1.Country);
                }
            }*/

            // tocast
            /*List<int> obj = new List<int>();
            obj.Add(1);
            obj.Add(2);
            obj.Add(3);
            obj.Add(4);
            obj.Add(5);

            var result = obj.Cast<int>();

            foreach(var res in result)
            {
                Console.WriteLine(res);
            }*/

            // tooftype
            /*ArrayList arr = new ArrayList();
            arr.Add("Himanshu");
            arr.Add("Hitesh");
            arr.Add(35);
            arr.Add(12);
            arr.Add(true);

            var result = arr.OfType<bool>();
            foreach(var res in result)
            {
                Console.WriteLine(res);
            }*/

            // asenumerable
            /*int[] NumArray = new int[] { 1, 2, 3, 4, 5 };
            var result = NumArray.OrderBy(x => x);

            foreach(var res in result)
            {
                Console.WriteLine(res);
            }*/

            //todictionary
            /*List<Student2> myStudents = new List<Student2>()
            {
                new Student2() { Name= "Himanshu", Gender = "Male", rollNo = 1},
                new Student2() { Name= "darshit", Gender = "Male", rollNo = 2},
                new Student2() { Name= "yash", Gender = "Male", rollNo = 3},
                new Student2() { Name= "vishal", Gender = "Male", rollNo = 4},
                new Student2() { Name= "dhaval", Gender = "Male", rollNo = 5},
            };
            var result = myStudents.ToDictionary(x => x.Name, y => y.rollNo);
            foreach(var res in result)
            {
                Console.WriteLine(res.Key+" "+res.Value);
            }*/


            //Partition Operators
            /*List<string> myContries = new List<string>() { "india", "monogliya", "bangladesh", "sri-lanka", "western" };
            // take
            //var result = myContries.Take(2);
            // takewhile
            //var result = myContries.TakeWhile(x => x.StartsWith("i"));
            // skip
            var result = myContries.Skip(3);
            foreach(var res in result)
            {
                Console.WriteLine(res);
            }*/

            //Sorting Operators
            /*List<Student> mySorts = new List<Student>() {
               new Student() { Name = "Suresh Dasari", Gender = "Male", Subject = new List<string> { "Mathematics", "Physics" } },
               new Student() { Name = "Rohini Alavala", Gender = "Female", Subject = new List<string> { "Entomology", "Botany" } },
               new Student() { Name = "Praveen Kumar", Gender = "Male", Subject = new List<string> { "Computers", "Operating System", "Java" } },
               new Student() { Name = "Sateesh Chandra", Gender = "Male", Subject = new List<string> { "English", "Social Studies", "Chemistry" } },
               new Student() { Name = "Madhav Sai", Gender = "Male", Subject = new List<string> { "Accounting", "Charted" } }
            };
            List<Student2> myStudents = new List<Student2>()
            {
                new Student2() { Name= "Himanshu", Gender = "Male", rollNo = 1},
                new Student2() { Name= "darshit", Gender = "Male", rollNo = 2},
                new Student2() { Name= "yash", Gender = "Male", rollNo = 3},
                new Student2() { Name= "vishal", Gender = "Male", rollNo = 4},
                new Student2() { Name= "dhaval", Gender = "Male", rollNo = 5},
            };

            //orderby
            //var studentName = mySorts.OrderBy(x => x.Name);

            //orderby decending
            //var studentName = mySorts.OrderByDescending(x => x.Name);

            //thenby
            //var studentName = mySorts.OrderBy(x => x.Name).ThenBy(x => x.Gender);

            //thenby decending
            //var studentName = mySorts.OrderBy(x => x.Name).ThenByDescending(x => x.Gender);
            var studentName = myStudents.OrderBy(x => x.Name).ThenBy(x => x.rollNo);
            foreach(var item in studentName)
            {
                Console.WriteLine(item.Name + " " +item.rollNo);
            }*/

            //Aggregate Function
            /* List<int> myNums = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
             //min
             Console.WriteLine(myNums.Min());
             //max
             Console.WriteLine(myNums.Max());
             //sum
             Console.WriteLine(myNums.Sum());
             //count
             Console.WriteLine(myNums.Count());
             //aggregate function
             Console.WriteLine(myNums.Aggregate((a, b) => a * b));
             string[] charList = { "a", "b", "c", "d" };
             Console.WriteLine(charList.Aggregate((a, b) => a + "." + b));*/

            // lambda expression
            /*List<string> myList = new List<string>();
            myList.Add("himanshu");
            myList.Add("karan");
            myList.Add("shrey");
            myList.Add("jhonty");
            myList.Add("priyank");

            IEnumerable<string> result = myList.Select(x => x);
            foreach(var res in result)
            {
                Console.WriteLine(res);
            }*/

            /*List<int> myList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //Linq mixed syntax
            var mixedSyntax = (from obj in myList where obj > 5 select obj).Sum();
            Console.WriteLine("mixed syntax "+mixedSyntax);

            //Linq method syntax
            var methodSyntax = myList.Where(obj => obj > 5).ToList();

            // Linq query syntax
            IEnumerable<int> result = from numbers in myList where numbers > 5 select numbers;

            
            foreach (int i in methodSyntax)
            {
                Console.WriteLine(i);
            }*/

        }
    }
}
