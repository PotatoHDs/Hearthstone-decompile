using UnityEngine;

public class GreedyPacker
{
	private Rectangle root;

	public GreedyPacker(int w, int h)
	{
		root = new Column(0, 0, w, h);
	}

	public RectInt Insert(int w, int h)
	{
		return root.Insert(w, h)?.position ?? new RectInt(-1, -1, -1, -1);
	}

	public void Remove(RectInt pos)
	{
		root.Remove(pos);
	}
}
