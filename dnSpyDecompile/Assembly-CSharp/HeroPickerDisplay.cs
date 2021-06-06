using System;
using UnityEngine;

// Token: 0x020002AB RID: 683
public class HeroPickerDisplay : MonoBehaviour
{
	// Token: 0x06002377 RID: 9079 RVA: 0x000B1330 File Offset: 0x000AF530
	private void Awake()
	{
		base.transform.localPosition = HeroPickerDisplay.HERO_PICKER_START_POSITION;
		AssetLoader.Get().InstantiatePrefab(UniversalInputManager.UsePhoneUI ? "DeckPickerTray_phone.prefab:a30124f640b5b92459bf820a4e3b1ca7" : "DeckPickerTray.prefab:3e13b59cdca14074bbce2b7d903ed895", new PrefabCallback<GameObject>(this.DeckPickerTrayLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		if (HeroPickerDisplay.s_instance != null)
		{
			Debug.LogWarning("HeroPickerDisplay is supposed to be a singleton, but a second instance of it is being created!");
		}
		HeroPickerDisplay.s_instance = this;
		SoundManager.Get().Load(SoundUtils.SquarePanelSlideOnSFX);
		SoundManager.Get().Load(SoundUtils.SquarePanelSlideOffSFX);
	}

	// Token: 0x06002378 RID: 9080 RVA: 0x000B13C5 File Offset: 0x000AF5C5
	private void OnDestroy()
	{
		HeroPickerDisplay.s_instance = null;
	}

	// Token: 0x06002379 RID: 9081 RVA: 0x000B13CD File Offset: 0x000AF5CD
	public static HeroPickerDisplay Get()
	{
		return HeroPickerDisplay.s_instance;
	}

	// Token: 0x0600237A RID: 9082 RVA: 0x000B13D4 File Offset: 0x000AF5D4
	public bool IsShown()
	{
		return base.transform.localPosition == HeroPickerDisplay.HERO_PICKER_END_POSITION;
	}

	// Token: 0x0600237B RID: 9083 RVA: 0x000B13EB File Offset: 0x000AF5EB
	public bool IsHidden()
	{
		return base.transform.localPosition == HeroPickerDisplay.HERO_PICKER_START_POSITION;
	}

	// Token: 0x0600237C RID: 9084 RVA: 0x000B1408 File Offset: 0x000AF608
	public void ShowTray()
	{
		SoundManager.Get().LoadAndPlay(SoundUtils.SquarePanelSlideOnSFX);
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			HeroPickerDisplay.HERO_PICKER_END_POSITION,
			"time",
			0.5f,
			"isLocal",
			true,
			"easeType",
			iTween.EaseType.easeOutBounce
		}));
	}

	// Token: 0x0600237D RID: 9085 RVA: 0x000B1487 File Offset: 0x000AF687
	public void CheatLoadHeroButtons(int amount)
	{
		this.m_deckPickerTray.CheatLoadHeroButtons(amount);
	}

	// Token: 0x0600237E RID: 9086 RVA: 0x000B1498 File Offset: 0x000AF698
	private void DeckPickerTrayLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		Options.SetFormatType(CollectionDeckTrayDeckListContent.s_HeroPickerFormat);
		this.m_deckPickerTray = go.GetComponent<DeckPickerTrayDisplay>();
		this.m_deckPickerTray.UpdateCreateDeckText();
		this.m_deckPickerTray.SetInHeroPicker();
		this.m_deckPickerTray.transform.parent = base.transform;
		this.m_deckPickerTray.transform.localScale = this.m_deckPickerBone.transform.localScale;
		this.m_deckPickerTray.transform.localPosition = this.m_deckPickerBone.transform.localPosition;
		this.m_deckPickerTray.InitAssets();
		this.ShowTray();
	}

	// Token: 0x0600237F RID: 9087 RVA: 0x000B1538 File Offset: 0x000AF738
	public void HideTray(float delay = 0f)
	{
		SoundManager.Get().LoadAndPlay(SoundUtils.SquarePanelSlideOffSFX);
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			HeroPickerDisplay.HERO_PICKER_START_POSITION,
			"time",
			0.5f,
			"isLocal",
			true,
			"oncomplete",
			"OnTrayHidden",
			"oncompletetarget",
			base.gameObject,
			"easeType",
			iTween.EaseType.easeInCubic,
			"delay",
			delay
		}));
	}

	// Token: 0x06002380 RID: 9088 RVA: 0x000B15F3 File Offset: 0x000AF7F3
	private void OnTrayHidden()
	{
		this.m_deckPickerTray.Unload();
		UnityEngine.Object.DestroyImmediate(base.gameObject);
		if (TavernBrawlDisplay.Get() != null)
		{
			TavernBrawlDisplay.Get().EnablePlayButton();
			TavernBrawlDisplay.Get().EnableBackButton(true);
		}
	}

	// Token: 0x040013A5 RID: 5029
	public GameObject m_deckPickerBone;

	// Token: 0x040013A6 RID: 5030
	private static readonly PlatformDependentValue<Vector3> HERO_PICKER_START_POSITION = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(-57.36467f, 2.4869f, -28.6f),
		Phone = new Vector3(-66.4f, 2.4869f, -28.6f)
	};

	// Token: 0x040013A7 RID: 5031
	private static readonly Vector3 HERO_PICKER_END_POSITION = new Vector3(40.6f, 2.4869f, -28.6f);

	// Token: 0x040013A8 RID: 5032
	private static HeroPickerDisplay s_instance;

	// Token: 0x040013A9 RID: 5033
	private DeckPickerTrayDisplay m_deckPickerTray;
}
