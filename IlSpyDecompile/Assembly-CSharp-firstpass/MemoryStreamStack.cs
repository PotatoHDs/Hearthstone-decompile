using System;
using System.IO;

public interface MemoryStreamStack : IDisposable
{
	MemoryStream Pop();

	void Push(MemoryStream stream);
}
