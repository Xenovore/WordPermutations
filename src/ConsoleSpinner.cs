namespace WordPermutations;

using System;

public sealed class Spinner : IDisposable
{
	const string _sequence = @"/-\|";
	readonly int _posLeft;
	readonly int _posTop;
	readonly int _delay;
	readonly Thread _thread;
	int _counter;
	bool _isActive;
	//ConsoleColor _prevFgColor;
	//ConsoleColor _prevBgColor;

	public Spinner( int posLeft = -1, int posTop = -1, int delay = 500 )
	{
		_posLeft = posLeft != -1 ? posLeft : Console.CursorLeft;
		_posTop = posTop != -1 ? posTop : Console.CursorTop;
		_delay = delay;
		_thread = new Thread( Spin );
	}

	public void Dispose()
	{
		Stop();
	}

	public void Start()
	{
		//_prevFgColor = Console.ForegroundColor;
		//_prevBgColor = Console.BackgroundColor;
		_counter = 0;
		_isActive = true;
		if ( !_thread.IsAlive ) { _thread.Start(); }
	}

	public void Stop()
	{
		_isActive = false;
		//Console.ForegroundColor = _prevFgColor;
		//Console.BackgroundColor = _prevBgColor;
		//Draw( ' ' );
	}

	void Spin()
	{
		while ( _isActive )
		{
			DrawSequence();
			Thread.Sleep( _delay );
		}
	}

	void DrawSequence()
	{
		Draw( _sequence[++_counter % _sequence.Length] );
	}

	void Draw( char c )
	{
		Console.SetCursorPosition( _posLeft, _posTop );
		//Console.ForegroundColor = ConsoleColor.Yellow;
		//Console.BackgroundColor = ConsoleColor.DarkBlue;

		Console.Write( c );
	}
}