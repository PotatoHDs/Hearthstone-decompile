using UnityEngine;

public class Glow : MonoBehaviour
{
	public void UpdateAlpha(float alpha)
	{
		GetComponent<Renderer>().GetMaterial().SetColor("_TintColor", new Color(1f, 1f, 1f, alpha));
	}
}
