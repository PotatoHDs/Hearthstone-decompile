using UnityEngine;

public abstract class Rectangle
{
	public RectInt position;

	public bool used;

	public Rectangle(int x, int y, int w, int h)
	{
		position = new RectInt(x, y, w, h);
	}

	public bool Contains(RectInt rect)
	{
		if (position.x <= rect.x && position.y <= rect.y && position.xMax >= rect.xMax)
		{
			return position.yMax >= rect.yMax;
		}
		return false;
	}

	public abstract Rectangle Insert(int w, int h);

	public abstract bool Remove(RectInt rect);
}
