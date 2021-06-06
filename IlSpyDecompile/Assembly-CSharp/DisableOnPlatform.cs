using UnityEngine;

public class DisableOnPlatform : MonoBehaviour
{
	public ScreenCategory m_screenCategory;

	private void OnEnable()
	{
		UpdateState();
	}

	private void Update()
	{
		UpdateState();
	}

	private void UpdateState()
	{
		if (Application.IsPlaying(this) && PlatformSettings.Screen == m_screenCategory)
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
