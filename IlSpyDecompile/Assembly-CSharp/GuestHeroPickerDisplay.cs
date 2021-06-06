using System.Collections;
using Hearthstone.UI;
using UnityEngine;

[CustomEditClass]
public class GuestHeroPickerDisplay : MonoBehaviour
{
	public AsyncReference m_trayControllerReference;

	public AsyncReference m_trayControllerReference_phone;

	private static GuestHeroPickerDisplay s_instance;

	private GuestHeroPickerTrayDisplay m_heroPickerTray;

	private Vector3 startOffset = new Vector3(-120f, 0f, 0f);

	private Vector3 startPosition;

	private void Awake()
	{
		if (s_instance != null)
		{
			Debug.LogWarning("GuestHeroPickerDisplay is supposed to be a singleton, but a second instance of it is being created!");
		}
		s_instance = this;
		startPosition = base.transform.localPosition;
		base.transform.localPosition = startPosition + startOffset;
		m_trayControllerReference.RegisterReadyListener<VisualController>(OnTrayControllerReady);
		m_trayControllerReference_phone.RegisterReadyListener<VisualController>(OnTrayControllerReady);
		SoundManager.Get().Load(SoundUtils.SquarePanelSlideOnSFX);
		SoundManager.Get().Load(SoundUtils.SquarePanelSlideOffSFX);
	}

	private void OnTrayControllerReady(VisualController trayController)
	{
		m_heroPickerTray = trayController.GetComponentInChildren<GuestHeroPickerTrayDisplay>();
		if (m_heroPickerTray == null)
		{
			Debug.LogError("GuestHeroPickerTrayDisplay component not found in GuestHeroPickerTray object.");
		}
		else if (trayController == null)
		{
			Debug.LogError("trayController was null in OnTrayControllerReady!");
		}
		else
		{
			m_heroPickerTray.InitAssets();
		}
	}

	public void ShowTray()
	{
		Hashtable args = iTween.Hash("position", startPosition, "time", 1f, "isLocal", true, "oncomplete", "OnTrayShown", "oncompletetarget", base.gameObject, "easeType", iTween.EaseType.easeOutBounce);
		SoundManager.Get().LoadAndPlay(SoundUtils.SquarePanelSlideOnSFX);
		iTween.MoveTo(base.gameObject, args);
	}

	public void OnTrayShown()
	{
		m_heroPickerTray.EnableBackButton(enabled: true);
		if (PvPDungeonRunScene.Get() != null)
		{
			PvPDungeonRunScene.Get().OnHeroPickerShown();
		}
	}

	public void HideTray(float delay = 0f)
	{
		Hashtable args = iTween.Hash("position", startPosition + startOffset, "time", 1f, "isLocal", true, "oncomplete", "OnTrayHidden", "oncompletetarget", base.gameObject, "easeType", iTween.EaseType.easeOutBounce, "delay", delay);
		SoundManager.Get().LoadAndPlay(SoundUtils.SquarePanelSlideOffSFX);
		iTween.MoveTo(base.gameObject, args);
	}

	private void OnTrayHidden()
	{
		m_heroPickerTray.Unload();
		Object.DestroyImmediate(base.gameObject);
		if (TavernBrawlDisplay.Get() != null)
		{
			TavernBrawlDisplay.Get().OnHeroPickerClosed();
		}
		if (PvPDungeonRunScene.Get() != null)
		{
			PvPDungeonRunScene.Get().OnHeroPickerHidden();
		}
	}

	public static GuestHeroPickerDisplay Get()
	{
		return s_instance;
	}
}
