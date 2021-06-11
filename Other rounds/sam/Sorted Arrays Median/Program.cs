using System;

namespace Sorted_Arrays_Median
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] n1 = new int[] { 1, 3, 5, 9, 10};
            int[] n2 = new int[] { 2};
            double median = FindMedianSortedArrays(n1, n2);
            Console.WriteLine(median);
        }

        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int index1 = 0;
            int index2 = 0;
            double median = 0;
            
            int length = nums1.Length + nums2.Length;
            int m = (length + 1) / 2;
            for (int ix = 0; ix < m; ++ix)
            {
                if (length % 2 == 1 && ix  == (int)Math.Floor((float)length/2f))
                {
                    median = compare(nums1, nums2, ref index1, ref index2, false);
                    return median;
                }
                else if(length % 2 == 0 && (ix == length / 2 - 1 || ix == length / 2))
                {
                    median += compare(nums1, nums2, ref index1, ref index2, false)/2d;
                }


                compare(nums1, nums2, ref index1, ref index2, true);
            }
            return median;
        }

        static int compare(int[] a1, int[] a2, ref int i1, ref int i2, bool add)
        {
            if (i1 >= a1.Length)
            {
                i2 += (add) ? 1 : 0;
                return a2[i2 - ((add) ? 1 : 0)];
            }
            if (i2 >= a2.Length)
            {
                i1 += (add) ? 1 : 0;
                return a1[i1 - ((add) ? 1 : 0)];
            }

            if (a1[i1] < a2[i2])
            {
                i1 += (add) ? 1 : 0;
                return a1[i1 - ((add) ? 1 : 0)];
            }
            else
            {
                i2 += (add) ? 1 : 0;
                return a2[i2 - ((add) ? 1 : 0)];
            }
        }
    }
}
