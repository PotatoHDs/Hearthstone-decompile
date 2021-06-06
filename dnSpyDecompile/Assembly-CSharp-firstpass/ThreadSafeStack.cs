using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x02000013 RID: 19
public class ThreadSafeStack : MemoryStreamStack, IDisposable
{
	// Token: 0x060000A9 RID: 169 RVA: 0x00003E7C File Offset: 0x0000207C
	public MemoryStream Pop()
	{
		Stack<MemoryStream> obj = this.stack;
		MemoryStream result;
		lock (obj)
		{
			if (this.stack.Count == 0)
			{
				result = new MemoryStream();
			}
			else
			{
				result = this.stack.Pop();
			}
		}
		return result;
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00003ED8 File Offset: 0x000020D8
	public void Push(MemoryStream stream)
	{
		Stack<MemoryStream> obj = this.stack;
		lock (obj)
		{
			this.stack.Push(stream);
		}
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00003F20 File Offset: 0x00002120
	public void Dispose()
	{
		Stack<MemoryStream> obj = this.stack;
		lock (obj)
		{
			this.stack.Clear();
		}
	}

	// Token: 0x0400002C RID: 44
	private Stack<MemoryStream> stack = new Stack<MemoryStream>();
}
