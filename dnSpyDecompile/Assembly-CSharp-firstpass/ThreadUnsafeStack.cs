using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x02000014 RID: 20
public class ThreadUnsafeStack : MemoryStreamStack, IDisposable
{
	// Token: 0x060000AD RID: 173 RVA: 0x00003F7B File Offset: 0x0000217B
	public MemoryStream Pop()
	{
		if (this.stack.Count == 0)
		{
			return new MemoryStream();
		}
		return this.stack.Pop();
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00003F9B File Offset: 0x0000219B
	public void Push(MemoryStream stream)
	{
		this.stack.Push(stream);
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00003FA9 File Offset: 0x000021A9
	public void Dispose()
	{
		this.stack.Clear();
	}

	// Token: 0x0400002D RID: 45
	private Stack<MemoryStream> stack = new Stack<MemoryStream>();
}
