using UnityEngine;

public class ArcEndSphere : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		Material material = GetComponent<Renderer>().GetMaterial();
		Vector2 mainTextureOffset = material.mainTextureOffset;
		mainTextureOffset.x += Time.deltaTime * 1f;
		material.mainTextureOffset = mainTextureOffset;
	}
}
