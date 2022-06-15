﻿using System.Collections;
using System.Collections.ObjectModel;

namespace SuperLinq;

public static partial class SuperEnumerable
{
	/// <summary>
	/// Groups the adjacent elements of a sequence according to a
	/// specified key selector function.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of
	/// <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by
	/// <paramref name="keySelector"/>.</typeparam>
	/// <param name="source">A sequence whose elements to group.</param>
	/// <param name="keySelector">A function to extract the key for each
	/// element.</param>
	/// <returns>A sequence of groupings where each grouping
	/// (<see cref="IGrouping{TKey,TElement}"/>) contains the key
	/// and the adjacent elements in the same order as found in the
	/// source sequence.</returns>
	/// <remarks>
	/// This method is implemented by using deferred execution and
	/// streams the groupings. The grouping elements, however, are
	/// buffered. Each grouping is therefore yielded as soon as it
	/// is complete and before the next grouping occurs.
	/// </remarks>

	public static IEnumerable<IGrouping<TKey, TSource>> GroupAdjacent<TSource, TKey>(
		this IEnumerable<TSource> source,
		Func<TSource, TKey> keySelector)
	{
		return GroupAdjacent(source, keySelector, null);
	}

	/// <summary>
	/// Groups the adjacent elements of a sequence according to a
	/// specified key selector function and compares the keys by using a
	/// specified comparer.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of
	/// <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by
	/// <paramref name="keySelector"/>.</typeparam>
	/// <param name="source">A sequence whose elements to group.</param>
	/// <param name="keySelector">A function to extract the key for each
	/// element.</param>
	/// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to
	/// compare keys.</param>
	/// <returns>A sequence of groupings where each grouping
	/// (<see cref="IGrouping{TKey,TElement}"/>) contains the key
	/// and the adjacent elements in the same order as found in the
	/// source sequence.</returns>
	/// <remarks>
	/// This method is implemented by using deferred execution and
	/// streams the groupings. The grouping elements, however, are
	/// buffered. Each grouping is therefore yielded as soon as it
	/// is complete and before the next grouping occurs.
	/// </remarks>

	public static IEnumerable<IGrouping<TKey, TSource>> GroupAdjacent<TSource, TKey>(
		this IEnumerable<TSource> source,
		Func<TSource, TKey> keySelector,
		IEqualityComparer<TKey>? comparer)
	{
		source.ThrowIfNull();
		keySelector.ThrowIfNull();

		return GroupAdjacent(source, keySelector, e => e, comparer);
	}

	/// <summary>
	/// Groups the adjacent elements of a sequence according to a
	/// specified key selector function and projects the elements for
	/// each group by using a specified function.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of
	/// <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by
	/// <paramref name="keySelector"/>.</typeparam>
	/// <typeparam name="TElement">The type of the elements in the
	/// resulting groupings.</typeparam>
	/// <param name="source">A sequence whose elements to group.</param>
	/// <param name="keySelector">A function to extract the key for each
	/// element.</param>
	/// <param name="elementSelector">A function to map each source
	/// element to an element in the resulting grouping.</param>
	/// <returns>A sequence of groupings where each grouping
	/// (<see cref="IGrouping{TKey,TElement}"/>) contains the key
	/// and the adjacent elements (of type <typeparamref name="TElement"/>)
	/// in the same order as found in the source sequence.</returns>
	/// <remarks>
	/// This method is implemented by using deferred execution and
	/// streams the groupings. The grouping elements, however, are
	/// buffered. Each grouping is therefore yielded as soon as it
	/// is complete and before the next grouping occurs.
	/// </remarks>

	public static IEnumerable<IGrouping<TKey, TElement>> GroupAdjacent<TSource, TKey, TElement>(
		this IEnumerable<TSource> source,
		Func<TSource, TKey> keySelector,
		Func<TSource, TElement> elementSelector)
	{
		return GroupAdjacent(source, keySelector, elementSelector, null);
	}

	/// <summary>
	/// Groups the adjacent elements of a sequence according to a
	/// specified key selector function. The keys are compared by using
	/// a comparer and each group's elements are projected by using a
	/// specified function.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of
	/// <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by
	/// <paramref name="keySelector"/>.</typeparam>
	/// <typeparam name="TElement">The type of the elements in the
	/// resulting groupings.</typeparam>
	/// <param name="source">A sequence whose elements to group.</param>
	/// <param name="keySelector">A function to extract the key for each
	/// element.</param>
	/// <param name="elementSelector">A function to map each source
	/// element to an element in the resulting grouping.</param>
	/// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to
	/// compare keys.</param>
	/// <returns>A sequence of groupings where each grouping
	/// (<see cref="IGrouping{TKey,TElement}"/>) contains the key
	/// and the adjacent elements (of type <typeparamref name="TElement"/>)
	/// in the same order as found in the source sequence.</returns>
	/// <remarks>
	/// This method is implemented by using deferred execution and
	/// streams the groupings. The grouping elements, however, are
	/// buffered. Each grouping is therefore yielded as soon as it
	/// is complete and before the next grouping occurs.
	/// </remarks>

	public static IEnumerable<IGrouping<TKey, TElement>> GroupAdjacent<TSource, TKey, TElement>(
		this IEnumerable<TSource> source,
		Func<TSource, TKey> keySelector,
		Func<TSource, TElement> elementSelector,
		IEqualityComparer<TKey>? comparer)
	{
		source.ThrowIfNull();
		keySelector.ThrowIfNull();
		elementSelector.ThrowIfNull();

		return GroupAdjacentImpl(source, keySelector, elementSelector, CreateGroupAdjacentGrouping,
								 comparer ?? EqualityComparer<TKey>.Default);
	}

	/// <summary>
	/// Groups the adjacent elements of a sequence according to a
	/// specified key selector function. The keys are compared by using
	/// a comparer and each group's elements are projected by using a
	/// specified function.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of
	/// <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by
	/// <paramref name="keySelector"/>.</typeparam>
	/// <typeparam name="TResult">The type of the elements in the
	/// resulting sequence.</typeparam>
	/// <param name="source">A sequence whose elements to group.</param>
	/// <param name="keySelector">A function to extract the key for each
	/// element.</param>
	/// <param name="resultSelector">A function to map each key and
	/// associated source elements to a result object.</param>
	/// <returns>A collection of elements of type
	/// <typeparamref name="TResult" /> where each element represents
	/// a projection over a group and its key.</returns>
	/// <remarks>
	/// This method is implemented by using deferred execution and
	/// streams the groupings. The grouping elements, however, are
	/// buffered. Each grouping is therefore yielded as soon as it
	/// is complete and before the next grouping occurs.
	/// </remarks>

	public static IEnumerable<TResult> GroupAdjacent<TSource, TKey, TResult>(
		this IEnumerable<TSource> source,
		Func<TSource, TKey> keySelector,
		Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
	{
		source.ThrowIfNull();
		keySelector.ThrowIfNull();
		resultSelector.ThrowIfNull();

		// This should be removed once the target framework is bumped to something that supports covariance
		TResult ResultSelectorWrapper(TKey key, IList<TSource> group) => resultSelector(key, group);

		return GroupAdjacentImpl(source, keySelector, i => i, ResultSelectorWrapper,
								 EqualityComparer<TKey>.Default);
	}

	/// <summary>
	/// Groups the adjacent elements of a sequence according to a
	/// specified key selector function. The keys are compared by using
	/// a comparer and each group's elements are projected by using a
	/// specified function.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of
	/// <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by
	/// <paramref name="keySelector"/>.</typeparam>
	/// <typeparam name="TResult">The type of the elements in the
	/// resulting sequence.</typeparam>
	/// <param name="source">A sequence whose elements to group.</param>
	/// <param name="keySelector">A function to extract the key for each
	/// element.</param>
	/// <param name="resultSelector">A function to map each key and
	/// associated source elements to a result object.</param>
	/// <param name="comparer">An <see cref="IEqualityComparer{TKey}"/> to
	/// compare keys.</param>
	/// <returns>A collection of elements of type
	/// <typeparamref name="TResult" /> where each element represents
	/// a projection over a group and its key.</returns>
	/// <remarks>
	/// This method is implemented by using deferred execution and
	/// streams the groupings. The grouping elements, however, are
	/// buffered. Each grouping is therefore yielded as soon as it
	/// is complete and before the next grouping occurs.
	/// </remarks>

	public static IEnumerable<TResult> GroupAdjacent<TSource, TKey, TResult>(
		this IEnumerable<TSource> source,
		Func<TSource, TKey> keySelector,
		Func<TKey, IEnumerable<TSource>, TResult> resultSelector,
		IEqualityComparer<TKey>? comparer)
	{
		source.ThrowIfNull();
		keySelector.ThrowIfNull();
		resultSelector.ThrowIfNull();

		// This should be removed once the target framework is bumped to something that supports covariance
		TResult ResultSelectorWrapper(TKey key, IList<TSource> group) => resultSelector(key, group);
		return GroupAdjacentImpl(source, keySelector, i => i, ResultSelectorWrapper,
								 comparer ?? EqualityComparer<TKey>.Default);
	}

	static IEnumerable<TResult> GroupAdjacentImpl<TSource, TKey, TElement, TResult>(
		this IEnumerable<TSource> source,
		Func<TSource, TKey> keySelector,
		Func<TSource, TElement> elementSelector,
		Func<TKey, IList<TElement>, TResult> resultSelector,
		IEqualityComparer<TKey> comparer)
	{
		using var iterator = source.GetEnumerator();

		(TKey, List<TElement>) group = default;

		while (iterator.MoveNext())
		{
			var key = keySelector(iterator.Current);
			var element = elementSelector(iterator.Current);

			if (group is (var k, { } members))
			{
				if (comparer.Equals(k, key))
				{
					members.Add(element);
					continue;
				}
				else
				{
					yield return resultSelector(k, members);
				}
			}

			group = (key, new List<TElement> { element });
		}

		{
			if (group is (var k, { } members))
				yield return resultSelector(k, members);
		}
	}

	static IGrouping<TKey, TElement> CreateGroupAdjacentGrouping<TKey, TElement>(TKey key, IList<TElement> members) =>
		Grouping.Create(key, members.IsReadOnly ? members : new ReadOnlyCollection<TElement>(members));

	static class Grouping
	{
		public static Grouping<TKey, TElement> Create<TKey, TElement>(TKey key, IEnumerable<TElement> members) =>
			new Grouping<TKey, TElement>(key, members);
	}

	[Serializable]
	sealed class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
	{
		readonly IEnumerable<TElement> _members;

		public Grouping(TKey key, IEnumerable<TElement> members)
		{
			Key = key;
			_members = members;
		}

		public TKey Key { get; }

		public IEnumerator<TElement> GetEnumerator() => _members.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}