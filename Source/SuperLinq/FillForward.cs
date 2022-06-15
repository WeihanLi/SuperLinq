﻿namespace SuperLinq;

public static partial class SuperEnumerable
{
	/// <summary>
	/// Returns a sequence with each null reference or value in the source
	/// replaced with the previous non-null reference or value seen in
	/// that sequence.
	/// </summary>
	/// <param name="source">The source sequence.</param>
	/// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
	/// <returns>
	/// An <see cref="IEnumerable{T}"/> with null references or values
	/// replaced.
	/// </returns>
	/// <remarks>
	/// This method uses deferred execution semantics and streams its
	/// results. If references or values are null at the start of the
	/// sequence then they remain null.
	/// </remarks>

	public static IEnumerable<T> FillForward<T>(this IEnumerable<T> source)
	{
		return source.FillForward(e => e == null);
	}

	/// <summary>
	/// Returns a sequence with each missing element in the source replaced
	/// with the previous non-missing element seen in that sequence. An
	/// additional parameter specifies a function used to determine if an
	/// element is considered missing or not.
	/// </summary>
	/// <param name="source">The source sequence.</param>
	/// <param name="predicate">The function used to determine if
	/// an element in the sequence is considered missing.</param>
	/// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
	/// <returns>
	/// An <see cref="IEnumerable{T}"/> with missing values replaced.
	/// </returns>
	/// <remarks>
	/// This method uses deferred execution semantics and streams its
	/// results. If elements are missing at the start of the sequence then
	/// they remain missing.
	/// </remarks>

	public static IEnumerable<T> FillForward<T>(this IEnumerable<T> source, Func<T, bool> predicate)
	{
		source.ThrowIfNull();
		predicate.ThrowIfNull();

		return FillForwardImpl(source, predicate, null);
	}

	/// <summary>
	/// Returns a sequence with each missing element in the source replaced
	/// with one based on the previous non-missing element seen in that
	/// sequence. Additional parameters specifiy two functions, one used to
	/// determine if an element is considered missing or not and another
	/// to provide the replacement for the missing element.
	/// </summary>
	/// <param name="source">The source sequence.</param>
	/// <param name="predicate">The function used to determine if
	/// an element in the sequence is considered missing.</param>
	/// <param name="fillSelector">The function used to produce the element
	/// that will replace the missing one. Its first argument receives the
	/// current element considered missing while the second argument
	/// receives the previous non-missing element.</param>
	/// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
	/// <returns>
	/// An <see cref="IEnumerable{T}"/> with missing values replaced.
	/// </returns>
	/// <remarks>
	/// This method uses deferred execution semantics and streams its
	/// results. If elements are missing at the start of the sequence then
	/// they remain missing.
	/// </remarks>

	public static IEnumerable<T> FillForward<T>(this IEnumerable<T> source, Func<T, bool> predicate, Func<T, T, T> fillSelector)
	{
		source.ThrowIfNull();
		predicate.ThrowIfNull();
		fillSelector.ThrowIfNull();

		return FillForwardImpl(source, predicate, fillSelector);
	}

	static IEnumerable<T> FillForwardImpl<T>(IEnumerable<T> source, Func<T, bool> predicate, Func<T, T, T>? fillSelector)
	{
		(bool, T) seed = default;

		foreach (var item in source)
		{
			if (predicate(item))
			{
				yield return seed is (true, { } someSeed)
						   ? fillSelector != null
							 ? fillSelector(item, someSeed)
							 : someSeed
						   : item;
			}
			else
			{
				seed = (true, item);
				yield return item;
			}
		}
	}
}