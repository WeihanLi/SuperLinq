﻿namespace SuperLinq;

public static partial class SuperEnumerable
{
	/// <summary>
	///	    Ranks each item in the sequence in ascending order by a specified key using a default comparer. The rank is
	///		equal to index + 1 of the first element of the item's equality set.
	/// </summary>
	/// <typeparam name="TSource">
	///	    The type of the elements in the source sequence
	/// </typeparam>
	/// <typeparam name="TKey">
	///	    The type of the key used to rank items in the sequence
	/// </typeparam>
	/// <param name="source">
	///	    The sequence of items to rank
	/// </param>
	/// <param name="keySelector">
	///	    A key selector function which returns the value by which to rank items in the sequence
	/// </param>
	/// <returns>
	///	    A sorted sequence of items and their rank.
	/// </returns>
	/// <exception cref="ArgumentNullException">
	///	    <paramref name="source"/> or <paramref name="keySelector"/> is <see langword="null"/>.
	/// </exception>
	/// <remarks>
	/// <para>
	///	    This method is implemented by using deferred execution. However, <paramref name="source"/> will be consumed
	///     in it's entirety immediately when first element of the returned sequence is consumed.
	/// </para>
	/// </remarks>
	public static IEnumerable<(TSource item, int rank)> RankBy<TSource, TKey>(
		this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
	{
		ArgumentNullException.ThrowIfNull(source);
		ArgumentNullException.ThrowIfNull(keySelector);

		return RankCore(source, keySelector, comparer: null, isDense: false);
	}

	///  <summary>
	/// 	Ranks each item in the sequence in the order defined by <paramref name="sortDirection"/> by a specified key
	///		using a default comparer. The rank is equal to index + 1 of the first element of the item's equality set.
	///  </summary>
	///  <typeparam name="TSource">
	/// 	The type of the elements in the source sequence
	///  </typeparam>
	///  <typeparam name="TKey">
	/// 	The type of the key used to rank items in the sequence
	///  </typeparam>
	///  <param name="source">
	/// 	The sequence of items to rank
	///  </param>
	///  <param name="keySelector">
	/// 	A key selector function which returns the value by which to rank items in the sequence
	///  </param>
	///  <param name="sortDirection">
	///		Defines the ordering direction for the sequence
	///	 </param>
	///  <returns>
	/// 	A sorted sequence of items and their rank.
	///  </returns>
	///  <exception cref="ArgumentNullException">
	/// 	<paramref name="source"/> or <paramref name="keySelector"/> is <see langword="null"/>.
	///  </exception>
	///  <remarks>
	///  <para>
	/// 	This method is implemented by using deferred execution. However, <paramref name="source"/> will be consumed
	///     in it's entirety immediately when first element of the returned sequence is consumed.
	///  </para>
	///  </remarks>
	public static IEnumerable<(TSource item, int rank)> RankBy<TSource, TKey>(
		this IEnumerable<TSource> source,
		Func<TSource, TKey> keySelector,
		OrderByDirection sortDirection)
	{
		ArgumentNullException.ThrowIfNull(source);
		ArgumentNullException.ThrowIfNull(keySelector);

		return RankCore(source, keySelector, comparer: null, isDense: false, sortDirection);
	}

	/// <summary>
	///	    Ranks each item in the sequence in ascending order by a specified key using a caller-supplied comparer. The
	///		rank is equal to index + 1 of the first element of the item's equality set.
	/// </summary>
	/// <typeparam name="TSource">
	///	    The type of the elements in the source sequence
	/// </typeparam>
	/// <typeparam name="TKey">
	///	    The type of the key used to rank items in the sequence
	/// </typeparam>
	/// <param name="source">
	///	    The sequence of items to rank
	/// </param>
	/// <param name="keySelector">
	///	    A key selector function which returns the value by which to rank items in the sequence
	/// </param>
	/// <param name="comparer">
	///	    An object that defines the comparison semantics for keys used to rank items
	/// </param>
	/// <returns>
	///	    A sorted sequence of items and their rank.
	/// </returns>
	/// <exception cref="ArgumentNullException">
	///	    <paramref name="source"/> or <paramref name="keySelector"/> is <see langword="null"/>.
	/// </exception>
	/// <remarks>
	/// <para>
	///	    This method is implemented by using deferred execution. However, <paramref name="source"/> will be consumed
	///     in it's entirety immediately when first element of the returned sequence is consumed.
	/// </para>
	/// </remarks>
	public static IEnumerable<(TSource item, int rank)> RankBy<TSource, TKey>(
		this IEnumerable<TSource> source,
		Func<TSource, TKey> keySelector,
		IComparer<TKey> comparer)
	{
		ArgumentNullException.ThrowIfNull(source);
		ArgumentNullException.ThrowIfNull(keySelector);

		return RankCore(source, keySelector, comparer, isDense: false);
	}

	///  <summary>
	/// 	Ranks each item in the sequence in the order defined by <paramref name="sortDirection"/> by a specified key
	///		using a caller-supplied comparer. The rank is equal to index + 1 of the first element of the item's equality set.
	///  </summary>
	///  <typeparam name="TSource">
	/// 	The type of the elements in the source sequence
	///  </typeparam>
	///  <typeparam name="TKey">
	/// 	The type of the key used to rank items in the sequence
	///  </typeparam>
	///  <param name="source">
	/// 	The sequence of items to rank
	///  </param>
	///  <param name="keySelector">
	/// 	A key selector function which returns the value by which to rank items in the sequence
	///  </param>
	///  <param name="comparer">
	/// 	An object that defines the comparison semantics for keys used to rank items
	///  </param>
	///  <param name="sortDirection">
	///		Defines the ordering direction for the sequence
	///  </param>
	///  <returns>
	/// 	A sorted sequence of items and their rank.
	///  </returns>
	///  <exception cref="ArgumentNullException">
	/// 	<paramref name="source"/> or <paramref name="keySelector"/> is <see langword="null"/>.
	///  </exception>
	///  <remarks>
	///  <para>
	/// 	This method is implemented by using deferred execution. However, <paramref name="source"/> will be consumed
	///     in it's entirety immediately when first element of the returned sequence is consumed.
	///  </para>
	///  </remarks>
	public static IEnumerable<(TSource item, int rank)> RankBy<TSource, TKey>(
		this IEnumerable<TSource> source,
		Func<TSource, TKey> keySelector,
		IComparer<TKey> comparer,
		OrderByDirection sortDirection)
	{
		ArgumentNullException.ThrowIfNull(source);
		ArgumentNullException.ThrowIfNull(keySelector);

		return RankCore(source, keySelector, comparer, isDense: false, sortDirection);
	}
}
