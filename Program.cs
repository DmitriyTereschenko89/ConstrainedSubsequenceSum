Solution solution = new();
Console.WriteLine(solution.ConstrainedSubsetSum(new int[] { 10, 2, -10, 5, 20 }, 2));
Console.WriteLine(solution.ConstrainedSubsetSum(new int[] { -1, -2, -3 }, 1));
Console.WriteLine(solution.ConstrainedSubsetSum(new int[] { 10, -2, -10, -5, 20 }, 2));

public class Solution
{
	private class Item
	{
		public int value;
		public int index;

		public Item(int value, int index)
		{
			this.value = value;
			this.index = index;
		}
	}

	private class MaxHeap
	{
		private readonly List<Item> heap;

		private void Swap(int i, int j)
		{
			(heap[i], heap[j]) = (heap[j], heap[i]);
		}

		private void SiftDown(int curIdx, int endIdx)
		{
			int childOneIdx = curIdx * 2 + 1;
			while (childOneIdx <= endIdx)
			{
				int swapIdx = childOneIdx;
				int childTwoIdx = curIdx * 2 + 2;
				if (childTwoIdx <= endIdx && heap[childTwoIdx].value > heap[childOneIdx].value)
				{
					swapIdx = childTwoIdx;
				}
				if (heap[swapIdx].value > heap[curIdx].value)
				{
					Swap(swapIdx, curIdx);
					curIdx = swapIdx;
					childOneIdx = curIdx * 2 + 1;
				}
				else
				{
					return;
				}
			}
		}

		private void SiftUp(int curIdx)
		{
			int parentIdx = (curIdx - 1) / 2;
			while (parentIdx >= 0 && heap[parentIdx].value < heap[curIdx].value)
			{
				Swap(parentIdx, curIdx);
				curIdx = parentIdx;
				parentIdx = (curIdx - 1) / 2;
			}
		}

		public MaxHeap()
		{
			heap = new List<Item>();
		}

		public void Push(int value, int index)
		{
			heap.Add(new Item(value, index));
			SiftUp(heap.Count - 1);
		}

		public int PeekIndex()
		{
			return heap[0].index;
		}

		public int PeekValue()
		{
			return heap[0].value;
		}

		public bool IsEmpty()
		{
			return heap.Count == 0;
		}

		public void Pop()
		{
			Swap(0, heap.Count - 1);
			heap.RemoveAt(heap.Count - 1);
			SiftDown(0, heap.Count - 1);
		}
	}

	public int ConstrainedSubsetSum(int[] nums, int k)
	{
		MaxHeap maxHeap = new();
		maxHeap.Push(nums[0], 0);
		int maxSubsetSum = nums[0];
		for (int i = 1; i < nums.Length; ++i)
		{
			while (i - maxHeap.PeekIndex() > k)
			{
				maxHeap.Pop();
			}
			int currSubsetSum = Math.Max(0, maxHeap.PeekValue()) + nums[i];
			maxSubsetSum = Math.Max(maxSubsetSum, currSubsetSum);
			maxHeap.Push(currSubsetSum, i);
		}
		return maxSubsetSum;
	}
}