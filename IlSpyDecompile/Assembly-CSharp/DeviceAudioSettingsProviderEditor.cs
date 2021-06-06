using UnityEngine;

public class DeviceAudioSettingsProviderEditor : MonoBehaviour, IDeviceAudioSettingsProvider
{
	[SerializeField]
	private float m_volume = 1f;

	[SerializeField]
	private bool m_isMuted;

	public float Volume => m_volume;

	public bool IsMuted => m_isMuted;

	private void Awake()
	{
		base.gameObject.AddComponent<HSDontDestroyOnLoad>();
	}
}
