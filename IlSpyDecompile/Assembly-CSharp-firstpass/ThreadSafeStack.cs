using System;
using System.Collections.Generic;
using System.IO;

public class ThreadSafeStack : MemoryStreamStack, IDisposable
{
	private Stack<MemoryStream> stack = new Stack<MemoryStream>();

	public MemoryStream Pop()
	{
		lock (stack)
		{
			if (stack.Count == 0)
			{
				return new MemoryStream();
			}
			return stack.Pop();
		}
	}

	public void Push(MemoryStream stream)
	{
		lock (stack)
		{
			stack.Push(stream);
		}
	}

	public void Dispose()
	{
		lock (stack)
		{
			stack.Clear();
		}
	}
}
