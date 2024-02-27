// WORD PERMUTATIONS
// Recursively find all permutation of the entered text
using System.Diagnostics;
using WordPermutations;

/* TODO: Command line option to sort; sorts can get slow */
/* TODO: Command line option to export as csv file, instead of displaying.
For text with > 8 chars... A) Console writes are slow; can add 5+ seconds; B) Exceeds the console history, losing results at top
*/

var stopWatch = new Stopwatch();
string? textLine = "-";

while ( !string.IsNullOrWhiteSpace( textLine ) )
{
	Console.WriteLine();
	Console.WriteLine( "Enter text:" );
	textLine = Console.ReadLine();

	if ( string.IsNullOrWhiteSpace( textLine ) )
	{
		break;
	}

	if ( textLine.Length > 9 )
	{
		Console.WriteLine( "Text is longer than 9 characters; this will take a while. Are you sure? [Y/N]" );
		var keyInfo = Console.ReadKey();
		if ( keyInfo.KeyChar is not ( 'y' or 'Y' ) )
		{
			continue;
		}
	}

	using var spinner = new Spinner();
	spinner.Start();
	stopWatch.Start();
	// Get all permutations into a list...
	var perms = textLine.Permutations()
		.Select( perm => string.Concat( perm ) )
		.Distinct() //! If there are duplicate characters, then there will be duplicate permutations
		.ToList();

	var timeToGetPerms = stopWatch.Elapsed;
	Console.WriteLine( $"\r\nTime spent getting permutations: {timeToGetPerms}" );

	//TODO: This gets much slower with longer text; maybe do custom sort that only checks the first 4-5 chars?
	// Sort in alphabetical order
	perms.Sort();

	spinner.Stop();

	Console.WriteLine();

	// Display the list of permutations, 10 per row...
	int i = 1;
	foreach ( string perm in perms )
	{
		Console.Write( $"{perm}, " );
		if ( i++ % 10 == 0 )
		{
			Console.WriteLine();
		}
	}

	Console.WriteLine( "\r\n" );
	Console.WriteLine( @$"There are {perms.Count} distinct permutations of ""{textLine}"" ({textLine.Length} chars)." );

	var timeTotal = stopWatch.Elapsed;
	Console.WriteLine( $"Total time spent: {timeTotal}\r\n" );

	stopWatch.Reset();
}