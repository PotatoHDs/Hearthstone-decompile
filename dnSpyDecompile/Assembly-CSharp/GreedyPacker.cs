using System;
using UnityEngine;

// Token: 0x02000A73 RID: 2675
public class GreedyPacker
{
	// Token: 0x06008FC4 RID: 36804 RVA: 0x002E972C File Offset: 0x002E792C
	public GreedyPacker(int w, int h)
	{
		this.root = new Column(0, 0, w, h);
	}

	// Token: 0x06008FC5 RID: 36805 RVA: 0x002E9744 File Offset: 0x002E7944
	public RectInt Insert(int w, int h)
	{
		Rectangle rectangle = this.root.Insert(w, h);
		if (rectangle == null)
		{
			return new RectInt(-1, -1, -1, -1);
		}
		return rectangle.position;
	}

	// Token: 0x06008FC6 RID: 36806 RVA: 0x002E9772 File Offset: 0x002E7972
	public void Remove(RectInt pos)
	{
		this.root.Remove(pos);
	}

	// Token: 0x04007876 RID: 30838
	private Rectangle root;
}
