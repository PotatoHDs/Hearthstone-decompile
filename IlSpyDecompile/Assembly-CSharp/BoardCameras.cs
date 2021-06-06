using UnityEngine;

public class BoardCameras : MonoBehaviour
{
	public AudioListener m_AudioListener;

	private static BoardCameras s_instance;

	private void Awake()
	{
		s_instance = this;
		if (LoadingScreen.Get() != null)
		{
			LoadingScreen.Get().NotifyMainSceneObjectAwoke(base.gameObject);
		}
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static BoardCameras Get()
	{
		return s_instance;
	}

	public AudioListener GetAudioListener()
	{
		return m_AudioListener;
	}
}
