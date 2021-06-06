using System;

// Token: 0x02000A43 RID: 2627
public class iTweenCollection
{
	// Token: 0x06008E21 RID: 36385 RVA: 0x002DDCFC File Offset: 0x002DBEFC
	public void Add(iTween tween)
	{
		if (tween == null)
		{
			return;
		}
		if (this.LastIndex >= this.Tweens.Length)
		{
			Array.Resize<iTween>(ref this.Tweens, this.Tweens.Length * 2);
		}
		this.Tweens[this.LastIndex] = tween;
		this.LastIndex++;
		this.Count++;
	}

	// Token: 0x06008E22 RID: 36386 RVA: 0x002DDD5C File Offset: 0x002DBF5C
	public void Remove(iTween tween)
	{
		if (tween == null || tween.destroyed)
		{
			return;
		}
		for (int i = 0; i < this.LastIndex; i++)
		{
			if (this.Tweens[i] == tween)
			{
				this.Tweens[i].destroyed = true;
				this.Tweens[i] = null;
				this.Count--;
				this.DeletedCount++;
				return;
			}
		}
	}

	// Token: 0x06008E23 RID: 36387 RVA: 0x002DDDC5 File Offset: 0x002DBFC5
	public iTweenIterator GetIterator()
	{
		return new iTweenIterator(this);
	}

	// Token: 0x06008E24 RID: 36388 RVA: 0x002DDDD0 File Offset: 0x002DBFD0
	public void CleanUp()
	{
		if (this.DeletedCount == 0)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < this.LastIndex; i++)
		{
			if (this.Tweens[i] != null)
			{
				this.Tweens[num] = this.Tweens[i];
				num++;
			}
		}
		this.LastIndex -= this.DeletedCount;
		this.DeletedCount = 0;
	}

	// Token: 0x04007635 RID: 30261
	public int LastIndex;

	// Token: 0x04007636 RID: 30262
	public int Count;

	// Token: 0x04007637 RID: 30263
	public iTween[] Tweens = new iTween[256];

	// Token: 0x04007638 RID: 30264
	public int DeletedCount;
}
