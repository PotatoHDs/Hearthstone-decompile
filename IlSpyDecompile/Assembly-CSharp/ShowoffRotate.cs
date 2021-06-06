using UnityEngine;

public class ShowoffRotate : MonoBehaviour
{
	public float Speed = 1f;

	private void Start()
	{
	}

	private void Update()
	{
		base.transform.Rotate(0f, Speed, 0f);
	}
}
