using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A75 RID: 2677
public class Row : Rectangle
{
	// Token: 0x17000820 RID: 2080
	// (get) Token: 0x06008FCB RID: 36811 RVA: 0x002E9800 File Offset: 0x002E7A00
	private int widthAvailable
	{
		get
		{
			if (this.used)
			{
				return 0;
			}
			if (this.columns == null)
			{
				return this.position.width;
			}
			int num = this.position.width;
			foreach (Rectangle rectangle in this.columns)
			{
				num -= rectangle.position.width;
			}
			return num;
		}
	}

	// Token: 0x06008FCC RID: 36812 RVA: 0x002E9550 File Offset: 0x002E7750
	public Row(int x, int y, int w, int h) : base(x, y, w, h)
	{
	}

	// Token: 0x06008FCD RID: 36813 RVA: 0x002E9888 File Offset: 0x002E7A88
	public override Rectangle Insert(int w, int h)
	{
		if (this.used)
		{
			return null;
		}
		if (h > this.position.height)
		{
			return null;
		}
		if (this.columns == null && w == this.position.width && h == this.position.height)
		{
			this.used = true;
			return this;
		}
		if (this.columns == null)
		{
			this.columns = new List<Rectangle>();
		}
		foreach (Rectangle rectangle in this.columns)
		{
			Rectangle rectangle2 = rectangle.Insert(w, h);
			if (rectangle2 != null)
			{
				return rectangle2;
			}
		}
		if (h > this.widthAvailable)
		{
			return null;
		}
		Column column = new Column(this.position.x + this.position.width - this.widthAvailable, this.position.y, w, this.position.height);
		this.columns.Add(column);
		return column.Insert(w, h);
	}

	// Token: 0x06008FCE RID: 36814 RVA: 0x002E9998 File Offset: 0x002E7B98
	public override bool Remove(RectInt rect)
	{
		if (!base.Contains(rect))
		{
			return false;
		}
		if (this.used && this.position.Equals(rect))
		{
			this.used = false;
			return true;
		}
		if (this.columns != null)
		{
			using (List<Rectangle>.Enumerator enumerator = this.columns.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Remove(rect))
					{
						return true;
					}
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x04007879 RID: 30841
	private List<Rectangle> columns;
}
