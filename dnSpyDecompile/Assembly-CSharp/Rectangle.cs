using System;
using UnityEngine;

// Token: 0x02000A74 RID: 2676
public abstract class Rectangle
{
	// Token: 0x06008FC7 RID: 36807 RVA: 0x002E9781 File Offset: 0x002E7981
	public Rectangle(int x, int y, int w, int h)
	{
		this.position = new RectInt(x, y, w, h);
	}

	// Token: 0x06008FC8 RID: 36808 RVA: 0x002E979C File Offset: 0x002E799C
	public bool Contains(RectInt rect)
	{
		return this.position.x <= rect.x && this.position.y <= rect.y && this.position.xMax >= rect.xMax && this.position.yMax >= rect.yMax;
	}

	// Token: 0x06008FC9 RID: 36809
	public abstract Rectangle Insert(int w, int h);

	// Token: 0x06008FCA RID: 36810
	public abstract bool Remove(RectInt rect);

	// Token: 0x04007877 RID: 30839
	public RectInt position;

	// Token: 0x04007878 RID: 30840
	public bool used;
}
