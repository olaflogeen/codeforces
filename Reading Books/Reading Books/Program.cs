using System;
using System.IO;
using System.Collections.Generic;

namespace Reading_Books
{
    class Program
    {
        static void Main(string[] args)
        {
            //first line
            string[] first_line = Console.ReadLine().Split(' ');
            //n - number of books, k
            int n = int.Parse(first_line[0]);
            int k = int.Parse(first_line[1]);
            List<book> books = new List<book>();
            for (int ix = 0; ix < n; ++ix)
            {
                string l = Console.ReadLine();
                books.Add(new book(l));
            }
            int time = analyze(books.ToArray(), k);
            Console.WriteLine(time);
        }
        
        /**<summary>
         * function that analyzes all the 
         * books that are required to read
         * </summary>
         * <param name="books">books array</param>
         * <param name="k">the minimal books that
         * each sibling has to like</param>
         * <returns>time that is needed to read the books;
         * if it's impossible then returns -1</returns>
        */
        static int analyze(book[] books, int k)
        {
            List<book> alice = new List<book>();
            List<book> bob = new List<book>();
            List<book> common = new List<book>();
            int time = 0;
            //books that alice and bob like
            int like_common = 0;
            //books that ONLY bob likes
            int like_b = 0;
            int like_a = 0;
            foreach (book x in books)
            {
                //if the book is liked by both of the siblings
                if (x.alice && x.bob)
                {
                    like_common++;
                    common.Add(x);
                }
                else if (x.alice)
                {
                    like_a++;
                    alice.Add(x);
                }
                else if (x.bob)
                {
                    like_b++;
                    bob.Add(x);
                }
            }
            //check if there are enough books to satisfy each sibling
            if (k - like_common > like_a || k - like_common > like_b)
            {
                return -1;
            }

            for (int i = k; i > 0; i--)
            {
                int sum = int.MaxValue;
                int both_liked_sum = int.MaxValue;

                int sh_alice = findShortest(alice);
                int sh_bob = findShortest(bob);
                if (alice.Count > 0 && bob.Count > 0)
                {
                    sum = alice[sh_alice].time + bob[sh_bob].time;
                }

                int shortest = findShortest(common);
                if (common.Count > 0)
                {
                    both_liked_sum = common[shortest].time;
                }
                    
                if (sum < both_liked_sum)
                {
                    time += sum;
                    bob.RemoveAt(sh_bob);
                    alice.RemoveAt(sh_alice);
                }
                else
                {
                    time += both_liked_sum;
                    common.RemoveAt(shortest);
                }
            }

            return time;
        }
        
        static int findShortest(List<book> books)
        {
            int time = int.MaxValue;
            int index = -1;
            for(int ix = 0; ix < books.Count; ++ix)
            {
                if (books[ix].time < time)
                {
                    index = ix;
                    time = books[ix].time;
                }
            }
            return index;
        }
        static List<book> sort(List<book> books)
        {
            bool exit = true;
            //bubble sort the books by time needed
            for (int ix = 0; ix < books.Count - 1; ++ix)
            {
                if (books[ix].time > books[ix + 1].time)
                {
                    book a = books[ix];
                    books[ix] = books[ix + 1];
                    books[ix + 1] = a;
                    exit = false;
                }
            }
            if (!exit)
                books = sort(books);
            return books;
        }

        /** <summary>
        * class that includes time that is required to read each book
         bool if alice likes the book and if bob likes it
        * </summary>
        */
        public class book
        { 
            public int time;
            public bool alice;
            public bool bob;

            public book(string line)
            {
                string[] numbers = line.Split(' ');
                time = int.Parse(numbers[0]);
                alice = (int.Parse(numbers[1]) == 1) ? true : false;
                bob = (int.Parse(numbers[2]) == 1) ? true : false;
            }
        }
    }
}
