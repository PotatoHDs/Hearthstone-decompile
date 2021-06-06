using System;
using System.Collections.Generic;
using System.IO;

public class ThreadUnsafeStack : MemoryStreamStack, IDisposable
{
	private Stack<MemoryStream> stack = new Stack<MemoryStream>();

	public MemoryStream Pop()
	{
		if (stack.Count == 0)
		{
			return new MemoryStream();
		}
		return stack.Pop();
	}

	public void Push(MemoryStream stream)
	{
		stack.Push(stream);
	}

	public void Dispose()
	{
		stack.Clear();
	}
}
