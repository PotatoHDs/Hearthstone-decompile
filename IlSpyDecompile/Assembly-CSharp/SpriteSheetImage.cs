using UnityEngine;

public class SpriteSheetImage : MonoBehaviour
{
	public int spriteIndex;

	private Material spriteMaterial;

	private void Start()
	{
		UpdateSprite();
	}

	private void UpdateSprite()
	{
		spriteMaterial = GetComponent<MeshRenderer>().GetMaterial();
		if (!(spriteMaterial == null))
		{
			float x = spriteMaterial.mainTextureScale.x;
			float y = spriteMaterial.mainTextureScale.y;
			int num = (int)(1f / x);
			float num2 = 0f;
			num2 = ((spriteIndex <= num) ? ((float)spriteIndex * x) : ((float)(spriteIndex % num) * x));
			float y2 = 1f - ((float)Mathf.CeilToInt(spriteIndex / num) * y + y);
			spriteMaterial.mainTextureOffset = new Vector2(num2, y2);
			spriteMaterial.renderQueue += 1000;
		}
	}
}
