using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            /*        dublicate      sorted   
              list =  yes             no
           hashset =  no              no
         sortedset =  no              yes
             stack =  yes             no
             queue =  yes             no
          linklist =  yes             no
        dictionary =  yes(by value)   no
  Sorteddictionary =  yes(by value)   yes(by key)
          sortlist =  yes(by value)   yes(by key)
             */

            // List
            /*var names = new List<string>();*/
            /*var names1 = new List<string>() { "Sonoo", "Vimal", "Ratan", "Love" };*/

            //hashSet
            /*var names = new HashSet<string>();
            var names1 = new HashSet<string>() { "Sonoo", "Vimal", "Ratan", "Love" };*/

            //sortset
            /* var names = new SortedSet<string>();
            var names1 = new SortedSet<string>() { "Sonoo", "Vimal", "Ratan", "Love" };*/

            /*names.Add("Himanshu");
            names.Add("naman");
            names.Add("yash");
            names.Add("shrey");
            names.Add("jhonty");
            names.Add("jay");*/

            // stack 
            //Stack<string> names = new Stack<string>();

            //Queue
            /*Queue<string> names = new Queue<string>();
            names.Enqueue("Himanshu");
            names.Enqueue("naman");
            names.Enqueue("yash");
            names.Enqueue("shrey");
            names.Enqueue("jhonty");
            names.Enqueue("jay");*/

            //LinkedList
            /*var names = new LinkedList<string>();

            names.AddLast("Himanshu");
            names.AddLast("naman");
            names.AddLast("yash");
            names.AddLast("shrey");
            names.AddLast("jhonty");
            names.AddFirst("jay");
            names.AddFirst("hemal");
            names.AddLast("hitesh");

            //insert new element before yash
            LinkedListNode<string> node = names.Find("yash");
            names.AddBefore(node, "savan");

            //insert new element after jhonty
            LinkedListNode<string> node1 = names.Find("jhonty");
            names.AddAfter(node1, "Ashok");

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }*/

            // Dictionary
            //Dictionary<int, string> names = new Dictionary<int, string>();

            //SortedDictionary
            //SortedDictionary<int, string> names = new SortedDictionary<int, string>();

            //sortedlist
            SortedList<int, string> names = new SortedList<int, string>();
            names.Add(1, "Himanshu");
            names.Add(4, "darshit");
            names.Add(3, "vishal");
            names.Add(2, "abjal");
            names.Add(5, "shivam");

            foreach (KeyValuePair<int, string> kv in names)
            {
                Console.WriteLine(kv.Key + " " + kv.Value);
            }
        }
    }
}
