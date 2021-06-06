using UnityEngine;

public class HeroPickerDisplay : MonoBehaviour
{
	public GameObject m_deckPickerBone;

	private static readonly PlatformDependentValue<Vector3> HERO_PICKER_START_POSITION = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(-57.36467f, 2.4869f, -28.6f),
		Phone = new Vector3(-66.4f, 2.4869f, -28.6f)
	};

	private static readonly Vector3 HERO_PICKER_END_POSITION = new Vector3(40.6f, 2.4869f, -28.6f);

	private static HeroPickerDisplay s_instance;

	private DeckPickerTrayDisplay m_deckPickerTray;

	private void Awake()
	{
		base.transform.localPosition = HERO_PICKER_START_POSITION;
		AssetLoader.Get().InstantiatePrefab(UniversalInputManager.UsePhoneUI ? "DeckPickerTray_phone.prefab:a30124f640b5b92459bf820a4e3b1ca7" : "DeckPickerTray.prefab:3e13b59cdca14074bbce2b7d903ed895", DeckPickerTrayLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		if (s_instance != null)
		{
			Debug.LogWarning("HeroPickerDisplay is supposed to be a singleton, but a second instance of it is being created!");
		}
		s_instance = this;
		SoundManager.Get().Load(SoundUtils.SquarePanelSlideOnSFX);
		SoundManager.Get().Load(SoundUtils.SquarePanelSlideOffSFX);
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static HeroPickerDisplay Get()
	{
		return s_instance;
	}

	public bool IsShown()
	{
		return base.transform.localPosition == HERO_PICKER_END_POSITION;
	}

	public bool IsHidden()
	{
		return base.transform.localPosition == HERO_PICKER_START_POSITION;
	}

	public void ShowTray()
	{
		SoundManager.Get().LoadAndPlay(SoundUtils.SquarePanelSlideOnSFX);
		iTween.MoveTo(base.gameObject, iTween.Hash("position", HERO_PICKER_END_POSITION, "time", 0.5f, "isLocal", true, "easeType", iTween.EaseType.easeOutBounce));
	}

	public void CheatLoadHeroButtons(int amount)
	{
		m_deckPickerTray.CheatLoadHeroButtons(amount);
	}

	private void DeckPickerTrayLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		Options.SetFormatType(CollectionDeckTrayDeckListContent.s_HeroPickerFormat);
		m_deckPickerTray = go.GetComponent<DeckPickerTrayDisplay>();
		m_deckPickerTray.UpdateCreateDeckText();
		m_deckPickerTray.SetInHeroPicker();
		m_deckPickerTray.transform.parent = base.transform;
		m_deckPickerTray.transform.localScale = m_deckPickerBone.transform.localScale;
		m_deckPickerTray.transform.localPosition = m_deckPickerBone.transform.localPosition;
		m_deckPickerTray.InitAssets();
		ShowTray();
	}

	public void HideTray(float delay = 0f)
	{
		SoundManager.Get().LoadAndPlay(SoundUtils.SquarePanelSlideOffSFX);
		iTween.MoveTo(base.gameObject, iTween.Hash("position", (Vector3)HERO_PICKER_START_POSITION, "time", 0.5f, "isLocal", true, "oncomplete", "OnTrayHidden", "oncompletetarget", base.gameObject, "easeType", iTween.EaseType.easeInCubic, "delay", delay));
	}

	private void OnTrayHidden()
	{
		m_deckPickerTray.Unload();
		Object.DestroyImmediate(base.gameObject);
		if (TavernBrawlDisplay.Get() != null)
		{
			TavernBrawlDisplay.Get().EnablePlayButton();
			TavernBrawlDisplay.Get().EnableBackButton(enable: true);
		}
	}
}
