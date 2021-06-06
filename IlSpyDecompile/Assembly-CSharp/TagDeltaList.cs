using System.Collections.Generic;

public class TagDeltaList
{
	private List<TagDelta> m_deltas = new List<TagDelta>();

	public int Count => m_deltas.Count;

	public TagDelta this[int index] => m_deltas[index];

	public void Add(int tag, int prev, int curr)
	{
		TagDelta tagDelta = new TagDelta();
		tagDelta.tag = tag;
		tagDelta.oldValue = prev;
		tagDelta.newValue = curr;
		m_deltas.Add(tagDelta);
	}
}
