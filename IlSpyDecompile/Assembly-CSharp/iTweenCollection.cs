using System;

public class iTweenCollection
{
	public int LastIndex;

	public int Count;

	public iTween[] Tweens = new iTween[256];

	public int DeletedCount;

	public void Add(iTween tween)
	{
		if (tween != null)
		{
			if (LastIndex >= Tweens.Length)
			{
				Array.Resize(ref Tweens, Tweens.Length * 2);
			}
			Tweens[LastIndex] = tween;
			LastIndex++;
			Count++;
		}
	}

	public void Remove(iTween tween)
	{
		if (tween == null || tween.destroyed)
		{
			return;
		}
		for (int i = 0; i < LastIndex; i++)
		{
			if (Tweens[i] == tween)
			{
				Tweens[i].destroyed = true;
				Tweens[i] = null;
				Count--;
				DeletedCount++;
				break;
			}
		}
	}

	public iTweenIterator GetIterator()
	{
		return new iTweenIterator(this);
	}

	public void CleanUp()
	{
		if (DeletedCount == 0)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < LastIndex; i++)
		{
			if (Tweens[i] != null)
			{
				Tweens[num] = Tweens[i];
				num++;
			}
		}
		LastIndex -= DeletedCount;
		DeletedCount = 0;
	}
}
