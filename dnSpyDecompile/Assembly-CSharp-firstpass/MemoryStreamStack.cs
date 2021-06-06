using System;
using System.IO;

// Token: 0x02000012 RID: 18
public interface MemoryStreamStack : IDisposable
{
	// Token: 0x060000A7 RID: 167
	MemoryStream Pop();

	// Token: 0x060000A8 RID: 168
	void Push(MemoryStream stream);
}
