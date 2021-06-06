using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A72 RID: 2674
public class Column : Rectangle
{
	// Token: 0x1700081F RID: 2079
	// (get) Token: 0x06008FC0 RID: 36800 RVA: 0x002E94C8 File Offset: 0x002E76C8
	private int heightAvailable
	{
		get
		{
			if (this.used)
			{
				return 0;
			}
			if (this.rows == null)
			{
				return this.position.height;
			}
			int num = this.position.height;
			foreach (Rectangle rectangle in this.rows)
			{
				num -= rectangle.position.height;
			}
			return num;
		}
	}

	// Token: 0x06008FC1 RID: 36801 RVA: 0x002E9550 File Offset: 0x002E7750
	public Column(int x, int y, int w, int h) : base(x, y, w, h)
	{
	}

	// Token: 0x06008FC2 RID: 36802 RVA: 0x002E9560 File Offset: 0x002E7760
	public override Rectangle Insert(int w, int h)
	{
		if (this.used)
		{
			return null;
		}
		if (w > this.position.width)
		{
			return null;
		}
		if (this.rows == null && this.position.width == w && this.position.height == h)
		{
			this.used = true;
			return this;
		}
		if (this.rows == null)
		{
			this.rows = new List<Rectangle>();
		}
		foreach (Rectangle rectangle in this.rows)
		{
			Rectangle rectangle2 = rectangle.Insert(w, h);
			if (rectangle2 != null)
			{
				return rectangle2;
			}
		}
		if (h > this.heightAvailable)
		{
			return null;
		}
		Row row = new Row(this.position.x, this.position.y + this.position.height - this.heightAvailable, this.position.width, h);
		this.rows.Add(row);
		this.rows.Sort((Rectangle lhs, Rectangle rhs) => lhs.position.height.CompareTo(rhs.position.height));
		return row.Insert(w, h);
	}

	// Token: 0x06008FC3 RID: 36803 RVA: 0x002E96A0 File Offset: 0x002E78A0
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
		if (this.rows != null)
		{
			using (List<Rectangle>.Enumerator enumerator = this.rows.GetEnumerator())
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

	// Token: 0x04007875 RID: 30837
	private List<Rectangle> rows;
}
