using System;

// Token: 0x02000A42 RID: 2626
public struct iTweenIterator
{
	// Token: 0x06008E1F RID: 36383 RVA: 0x002DDC98 File Offset: 0x002DBE98
	public iTweenIterator(iTweenCollection collection)
	{
		this.TweenCollection = collection;
		this.Index = 0;
	}

	// Token: 0x06008E20 RID: 36384 RVA: 0x002DDCA8 File Offset: 0x002DBEA8
	public iTween GetNext()
	{
		if (this.TweenCollection == null)
		{
			return null;
		}
		while (this.Index < this.TweenCollection.LastIndex)
		{
			iTween iTween = this.TweenCollection.Tweens[this.Index];
			this.Index++;
			if (iTween != null)
			{
				return iTween;
			}
		}
		return null;
	}

	// Token: 0x04007633 RID: 30259
	private iTweenCollection TweenCollection;

	// Token: 0x04007634 RID: 30260
	private int Index;
}
