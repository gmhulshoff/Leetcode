using System;
using System.Linq;

public class Program
{
	public static void Main()
	{
		TestRemove(ToArray(3, 2, 2, 3), 3);
		TestRemove(ToArray(0, 1, 2, 2, 3, 0, 4, 2), 2);
		WriteArray("SortedSquares", Solution.SortedSquares(-4, -1, 0, 3, 11));
		WriteArray("DuplicateZeros", Solution.DuplicateZeros(1, 0, 2, 3, 0, 4, 5, 0));
		TestMerge(ToArray(1, 2, 3, 0, 0, 0), 3, ToArray(2, 5, 6), 3);
		TestMerge(ToArray(0), 0, ToArray(1), 1);
	}

	static void TestRemove(int[] nums, int val)
	{
		WriteArray($"RemoveElements ({val})", nums);
		WriteArray($"{Solution.RemoveElements(nums, val)} =>", nums);
	}

	static void TestMerge(int[] t1, int m, int[] t2, int n)
	{
		Solution.Merge(t1, m, t2, n);
		WriteArray("TestMerge", t1);
	}

	static int[] ToArray(params int[] arr) => arr;
	static void WriteArray(string title, int[] arr) => Console.WriteLine(title + ": " + string.Join(',', arr));
	public class Solution
	{
		public static int RemoveElements(int[] nums, int val)
		{
			return RemoveElements(nums, val, 0, nums.Length);
		}

		static int RemoveElements(int[] nums, int val, int i, int len)
		{
			if (i == len)
				return len;
			if (nums[i] != val)
				return RemoveElements(nums, val, i + 1, len - 1);
			var offset = 1;
			while (i < len - offset && nums[i + offset] == val)
				offset++;
			if (i < len - offset)
				nums[i] = nums[i + offset];
			return RemoveElements(nums, val, i + 1, len - 1);
		}

		public static void Merge(int[] nums1, int m, int[] nums2, int n)
		{
			if (n > 0 && (m == 0 || nums1[m - 1] <= nums2[n - 1]))
				Merge(nums1, m, nums2, n - 1, nums2[n - 1]);
			else if (m > 0 && (n == 0 || nums1[m - 1] > nums2[n - 1]))
				Merge(nums1, m - 1, nums2, n, nums1[m - 1]);
		}

		static void Merge(int[] nums1, int m, int[] nums2, int n, int last)
		{
			nums1[m + n] = last;
			Merge(nums1, m, nums2, n);
		}

		public static int[] DuplicateZeros(params int[] arr)
		{
			var zc = arr.Count(n => n == 0);
			cp(arr, zc, arr.Length - 1 + zc);
			return arr;
		}

		static int cp(int[] arr, int zc, int i)
		{
			if (zc == 0)
				return 0;
			if (i < arr.Length)
				arr[i] = arr[i - zc];
			if (arr[i - zc] != 0)
				return cp(arr, zc, i - 1);
			i--;
			if (i < arr.Length)
				arr[i] = 0;
			return cp(arr, zc - 1, i - 1);
		}

		public static int[] SortedSquares(params int[] nums)
		{
			for (var i = 0; i < nums.Length; i++)
				insSorted(nums[i] * nums[i], nums, i);
			return nums;
		}

		static void insSorted(int n, int[] sorted, int i)
		{
			for (; i > 0 && sorted[i - 1] > n; i--)
				sorted[i] = sorted[i - 1];
			sorted[i] = n;
		}
	}
}
