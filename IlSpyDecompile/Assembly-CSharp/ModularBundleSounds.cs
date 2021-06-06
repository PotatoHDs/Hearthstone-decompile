using UnityEngine;

public class ModularBundleSounds : MonoBehaviour
{
	private string m_entrySound;

	private string m_landingSound;

	private string m_exitSound;

	public void Initialize(string entrySound, string landingSound, string exitSound)
	{
		m_entrySound = entrySound;
		m_landingSound = landingSound;
		m_exitSound = exitSound;
	}

	private void PlayEntrySound()
	{
		if (!string.IsNullOrEmpty(m_entrySound))
		{
			SoundManager.Get().LoadAndPlay(m_entrySound);
		}
	}

	private void PlayLandingSound()
	{
		if (!string.IsNullOrEmpty(m_landingSound))
		{
			SoundManager.Get().LoadAndPlay(m_landingSound);
		}
	}

	private void PlayExitSound()
	{
		if (!string.IsNullOrEmpty(m_exitSound))
		{
			SoundManager.Get().LoadAndPlay(m_exitSound);
		}
	}
}
