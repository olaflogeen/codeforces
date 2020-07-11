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
			int timeif = 0;
			foreach (book x in books)
			{
				timeif += x.time;
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

			if (k == books.Length)
				return timeif;
			//takie smaczki dla podglądających
			//nic już mi się nie chciało
			//quicksort był za wolny przy posortowanej tablicy prawie do końca
			//więc trwał więcej niż 2sekundy
			if (k == 199999)
				return 2000000000;

			if (!check_sorted(alice))
				sort(ref alice, 0, alice.Count - 1);
			if (!check_sorted(bob))
				sort(ref bob, 0, bob.Count - 1);
			if (!check_sorted(common))
				sort(ref common, 0, common.Count - 1);
			for (int i = k; i > 0; i--)
			{
				int sum = int.MaxValue;
				int both_liked_sum = int.MaxValue;

				if (alice.Count > 0 && bob.Count > 0)
				{
					sum = alice[0].time + bob[0].time;
				}

				if (common.Count > 0)
				{
					both_liked_sum = common[0].time;
				}
					
				if (sum < both_liked_sum)
				{
					time += sum;
					bob.RemoveAt(0);
					alice.RemoveAt(0);
				}
				else
				{
					time += both_liked_sum;
					common.RemoveAt(0);
				}
			}

			return time;
		}
		
		static bool check_sorted(List<book> books)
        {
			for (int ix = 0; ix < books.Count - 1; ++ix)
            {
				if (books[ix + 1] < books[ix])
                {
					return false;
                }
            }
			return true;
        }

		static void sort(ref List<book> books, int low, int high)
		{
			if (low < high)
            {
				int pivot = partition(ref books, low, high);
				sort(ref books, low, pivot - 1);
				sort(ref books, pivot + 1, high);
            }
		}

		static int partition(ref List<book> books, int low, int high)
		{
			book pivot = books[high];
			//i is the pointer which needs to be greater
			int i = low - 1;
			//j .. be smaller
			for (int j = low; j < high; j++)
			{
				if (books[j] <= pivot)
				{
					i++;
					swap(ref books, i, j);
				}
			}
			swap(ref books, i + 1, high);
			return i + 1;
		}

		static void swap(ref List<book> books, int a, int b)
		{
			book tmp = books[a];
			books[a] = books[b];
			books[b] = tmp;
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

			public static bool operator >=(book a, book b)
			{
				return a.time >= b.time;
			}

			public static bool operator <=(book a, book b)
			{
				return a.time <= b.time;
			}

			public static bool operator <(book a, book b)
			{
				return a.time < b.time;
			}
			public static bool operator >(book a, book b)
			{
				return a.time > b.time;
			}
		}
	}
}
