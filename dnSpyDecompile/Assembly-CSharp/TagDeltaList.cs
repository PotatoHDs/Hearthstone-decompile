using System;
using System.Collections.Generic;

// Token: 0x0200092C RID: 2348
public class TagDeltaList
{
	// Token: 0x060081A1 RID: 33185 RVA: 0x002A3248 File Offset: 0x002A1448
	public void Add(int tag, int prev, int curr)
	{
		TagDelta tagDelta = new TagDelta();
		tagDelta.tag = tag;
		tagDelta.oldValue = prev;
		tagDelta.newValue = curr;
		this.m_deltas.Add(tagDelta);
	}

	// Token: 0x1700075C RID: 1884
	// (get) Token: 0x060081A2 RID: 33186 RVA: 0x002A327C File Offset: 0x002A147C
	public int Count
	{
		get
		{
			return this.m_deltas.Count;
		}
	}

	// Token: 0x1700075D RID: 1885
	public TagDelta this[int index]
	{
		get
		{
			return this.m_deltas[index];
		}
	}

	// Token: 0x04006CE0 RID: 27872
	private List<TagDelta> m_deltas = new List<TagDelta>();
}
