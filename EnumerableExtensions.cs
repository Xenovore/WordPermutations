namespace WordPermutations;
using System;
using System.Collections.Generic;
using System.Linq;

// Source: http://stackoverflow.com/questions/774457/combination-generator-in-linq#12012418

public static class EnumerableExtensions
{
	private static IEnumerable<TSource> Prepend<TSource>( this IEnumerable<TSource> source, TSource item )
	{
		ArgumentNullException.ThrowIfNull( source );

		return GetItem();

		IEnumerable<TSource> GetItem()
		{
			yield return item;

			foreach ( var item in source )
			{
				yield return item;
			}
		}
	}

	public static IEnumerable<IEnumerable<TSource>> Permutations<TSource>( this IEnumerable<TSource> source )
	{
		ArgumentNullException.ThrowIfNull( source );

		var list = source.ToList();

		if ( list.Count > 1 )
		{
			return from s in list
				   from p in  Permutations( list.Take( list.IndexOf( s ) ).Concat( list.Skip( list.IndexOf( s ) + 1 ) ) )
				   select p.Prepend( s );
		}

		return new[] { list };
	}
}
