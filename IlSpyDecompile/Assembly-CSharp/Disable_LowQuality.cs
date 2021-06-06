using UnityEngine;

public class Disable_LowQuality : MonoBehaviour
{
	private void Awake()
	{
		GraphicsManager.Get().RegisterLowQualityDisableObject(base.gameObject);
		if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
		{
			base.gameObject.SetActive(value: false);
		}
	}

	private void OnDestroy()
	{
		GraphicsManager.Get()?.DeregisterLowQualityDisableObject(base.gameObject);
	}
}
