using UnityEngine;

public abstract class CarouselItem
{
	public abstract void Show(Carousel parent);

	public abstract void Hide();

	public abstract void Clear();

	public abstract GameObject GetGameObject();

	public abstract bool IsLoaded();
}
