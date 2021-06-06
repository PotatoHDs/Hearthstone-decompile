public struct iTweenIterator
{
	private iTweenCollection TweenCollection;

	private int Index;

	public iTweenIterator(iTweenCollection collection)
	{
		TweenCollection = collection;
		Index = 0;
	}

	public iTween GetNext()
	{
		if (TweenCollection == null)
		{
			return null;
		}
		while (Index < TweenCollection.LastIndex)
		{
			iTween iTween2 = TweenCollection.Tweens[Index];
			Index++;
			if (iTween2 != null)
			{
				return iTween2;
			}
		}
		return null;
	}
}
