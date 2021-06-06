using UnityEngine;

public interface ITouchListItem
{
	Bounds LocalBounds { get; }

	bool IsHeader { get; }

	bool Visible { get; set; }

	GameObject gameObject { get; }

	Transform transform { get; }

	T GetComponent<T>() where T : Component;

	void OnScrollOutOfView();
}
