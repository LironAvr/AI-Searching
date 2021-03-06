﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
	internal static class SortedListExtensions
	{
		/// <summary>
		/// Checks if the SortedList is empty.
		/// </summary>
		/// <param name="sortedList">SortedList to check if it is empty.</param>
		/// <returns>True if sortedList is empty, false if it still has elements.</returns>
		internal static bool IsEmpty<TKey, TValue>(this SortedList<TKey, TValue> sortedList)
		{
			return sortedList.Count == 0;
		}

		/// <summary>
		/// Adds a INode to the SortedList.
		/// </summary>
		/// <param name="sortedList">SortedList to add the node to.</param>
		/// <param name="node">Node to add to the sortedList.</param>
		internal static void Add(this SortedList<int, AState> sortedList, AState state)
		{
			sortedList.Add(state.TotalCost, state);
		}

		/// <summary>
		/// Removes the node from the sorted list with the smallest TotalCost and returns that node.
		/// </summary>
		/// <param name="sortedList">SortedList to remove and return the smallest TotalCost node.</param>
		/// <returns>Node with the smallest TotalCost.</returns>
		internal static AState Pop(this SortedList<int, AState> sortedList)
		{
			var top = sortedList.Values[0];
			sortedList.RemoveAt(0);
			return top;
		}
	}
}
