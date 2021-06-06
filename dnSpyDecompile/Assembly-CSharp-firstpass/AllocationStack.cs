using System;
using System.IO;

// Token: 0x02000015 RID: 21
public class AllocationStack : MemoryStreamStack, IDisposable
{
	// Token: 0x060000B1 RID: 177 RVA: 0x00003FC9 File Offset: 0x000021C9
	public MemoryStream Pop()
	{
		return new MemoryStream();
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00003FD0 File Offset: 0x000021D0
	public void Push(MemoryStream stream)
	{
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00003FD0 File Offset: 0x000021D0
	public void Dispose()
	{
	}
}
