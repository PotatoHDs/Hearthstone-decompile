using System;
using UnityEngine;

// Token: 0x020006F8 RID: 1784
[CustomEditClass]
public class GeneralStoreHeroesContent : GeneralStoreContent
{
	// Token: 0x06006392 RID: 25490 RVA: 0x00206C44 File Offset: 0x00204E44
	private void Awake()
	{
		this.m_heroDisplay1 = this.m_heroDisplay;
		this.m_heroDisplay2 = UnityEngine.Object.Instantiate<GeneralStoreHeroesContentDisplay>(this.m_heroDisplay);
		this.m_heroDisplay2.transform.parent = this.m_heroDisplay1.transform.parent;
		this.m_heroDisplay2.transform.localPosition = this.m_heroDisplay1.transform.localPosition;
		this.m_heroDisplay2.transform.localScale = this.m_heroDisplay1.transform.localScale;
		this.m_heroDisplay2.transform.localRotation = this.m_heroDisplay1.transform.localRotation;
		this.m_heroDisplay2.gameObject.SetActive(false);
		this.m_heroDisplay1.SetParent(this);
		this.m_heroDisplay2.SetParent(this);
		this.m_heroDisplay1.SetKeyArtRenderer(this.m_renderQuad1);
		this.m_heroDisplay2.SetKeyArtRenderer(this.m_renderQuad2);
		this.m_renderToTexture1.GetComponent<RenderToTexture>().m_RenderToObject = this.m_heroDisplay1.m_renderArtQuad;
		this.m_renderToTexture2.GetComponent<RenderToTexture>().m_RenderToObject = this.m_heroDisplay2.m_renderArtQuad;
	}

	// Token: 0x06006393 RID: 25491 RVA: 0x00206D6F File Offset: 0x00204F6F
	public override bool AnimateEntranceEnd()
	{
		this.m_parentStore.HideAccentTexture();
		return true;
	}

	// Token: 0x06006394 RID: 25492 RVA: 0x00206D7D File Offset: 0x00204F7D
	public CardHeroDbfRecord GetSelectedHero()
	{
		return this.m_currentDbfRecord;
	}

	// Token: 0x06006395 RID: 25493 RVA: 0x00206D88 File Offset: 0x00204F88
	public void SelectHero(CardHeroDbfRecord cardHeroDbfRecord, bool animate = true)
	{
		if (cardHeroDbfRecord == this.m_currentDbfRecord)
		{
			return;
		}
		this.m_currentDbfRecord = cardHeroDbfRecord;
		Network.Bundle bundle = null;
		StoreManager.Get().GetHeroBundleByCardDbId(cardHeroDbfRecord.CardId, out bundle);
		base.SetCurrentMoneyBundle(bundle, false);
		if (this.m_currentHeroDef != null)
		{
			UnityEngine.Object.Destroy(this.m_currentHeroDef.gameObject);
			this.m_currentHeroDef = null;
		}
		this.m_currentCardBackPreview = cardHeroDbfRecord.CardBackId;
		using (DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(this.m_currentDbfRecord.CardId))
		{
			this.m_currentHeroDef = GameUtils.LoadGameObjectWithComponent<CollectionHeroDef>(cardDef.CardDef.m_CollectionHeroDefPath);
			bool purchased = StoreManager.Get().IsProductAlreadyOwned(bundle);
			this.AnimateAndUpdateDisplays(cardHeroDbfRecord, this.m_currentCardBackPreview, this.m_currentHeroDef, purchased);
			this.PlayHeroMusic();
			this.UpdateHeroDescription(purchased);
		}
	}

	// Token: 0x06006396 RID: 25494 RVA: 0x00206E6C File Offset: 0x0020506C
	public void PlayCurrentHeroPurchaseEmote()
	{
		GeneralStoreHeroesContentDisplay currentDisplay = this.GetCurrentDisplay();
		if (currentDisplay != null)
		{
			currentDisplay.PlayPurchaseEmote();
		}
	}

	// Token: 0x06006397 RID: 25495 RVA: 0x00206E8F File Offset: 0x0020508F
	public override void StoreShown(bool isCurrent)
	{
		if (this.m_currentDisplay == -1 || !isCurrent)
		{
			return;
		}
		this.PlayHeroMusic();
		this.ResetHeroPreview();
	}

	// Token: 0x06006398 RID: 25496 RVA: 0x00206EAA File Offset: 0x002050AA
	public override void PreStoreFlipIn()
	{
		this.ResetHeroPreview();
	}

	// Token: 0x06006399 RID: 25497 RVA: 0x00206EB2 File Offset: 0x002050B2
	public override void PostStoreFlipIn(bool animatedFlipIn)
	{
		this.PlayHeroMusic();
	}

	// Token: 0x0600639A RID: 25498 RVA: 0x00206EBC File Offset: 0x002050BC
	public override void TryBuyWithMoney(Network.Bundle bundle, GeneralStoreContent.BuyEvent successBuyCB, GeneralStoreContent.BuyEvent failedBuyCB)
	{
		SpecialEventManager specialEventManager = SpecialEventManager.Get();
		SpecialEventType eventType = SpecialEventManager.GetEventType(bundle.ProductEvent);
		if (!specialEventManager.IsEventActive(eventType, false))
		{
			string key = "GLUE_STORE_PRODUCT_NOT_AVAILABLE_TEXT";
			if (specialEventManager.HasEventEnded(eventType))
			{
				key = "GLUE_STORE_PRODUCT_NOT_AVAILABLE_TEXT_HAS_ENDED";
			}
			else if (specialEventManager.GetEventStartTimeUtc(eventType) != null && !specialEventManager.HasEventStarted(eventType))
			{
				key = "GLUE_STORE_PRODUCT_NOT_AVAILABLE_TEXT_NOT_YET_STARTED";
			}
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_STORE_PRODUCT_NOT_AVAILABLE_HEADER");
			popupInfo.m_text = GameStrings.Get(key);
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object data)
			{
				this.m_parentStore.BlockInterface(false);
				if (failedBuyCB != null)
				{
					failedBuyCB();
				}
			};
			this.m_parentStore.BlockInterface(true);
			DialogManager.Get().ShowPopup(popupInfo);
			return;
		}
		if (successBuyCB != null)
		{
			successBuyCB();
		}
	}

	// Token: 0x0600639B RID: 25499 RVA: 0x00206FA0 File Offset: 0x002051A0
	protected override void OnRefresh()
	{
		Network.Bundle bundle = null;
		if (this.m_currentDbfRecord != null)
		{
			StoreManager.Get().GetHeroBundleByCardDbId(this.m_currentDbfRecord.CardId, out bundle);
		}
		bool flag = StoreManager.Get().IsProductAlreadyOwned(bundle);
		this.GetCurrentDisplay().ShowPurchasedCheckmark(flag);
		base.SetCurrentMoneyBundle(bundle, true);
		this.UpdateHeroDescription(flag);
	}

	// Token: 0x0600639C RID: 25500 RVA: 0x00206FF6 File Offset: 0x002051F6
	public override bool IsPurchaseDisabled()
	{
		return this.m_currentDisplay == -1;
	}

	// Token: 0x0600639D RID: 25501 RVA: 0x00207001 File Offset: 0x00205201
	public override string GetMoneyDisplayOwnedText()
	{
		return GameStrings.Get("GLUE_STORE_HERO_BUTTON_COST_OWNED_TEXT");
	}

	// Token: 0x0600639E RID: 25502 RVA: 0x0020700D File Offset: 0x0020520D
	private GameObject GetCurrentDisplayContainer()
	{
		return this.GetCurrentDisplay().gameObject;
	}

	// Token: 0x0600639F RID: 25503 RVA: 0x0020701A File Offset: 0x0020521A
	private GameObject GetNextDisplayContainer()
	{
		if ((this.m_currentDisplay + 1) % 2 != 0)
		{
			return this.m_heroDisplay2.gameObject;
		}
		return this.m_heroDisplay1.gameObject;
	}

	// Token: 0x060063A0 RID: 25504 RVA: 0x0020703F File Offset: 0x0020523F
	private GeneralStoreHeroesContentDisplay GetCurrentDisplay()
	{
		if (this.m_currentDisplay != 0)
		{
			return this.m_heroDisplay2;
		}
		return this.m_heroDisplay1;
	}

	// Token: 0x060063A1 RID: 25505 RVA: 0x00207058 File Offset: 0x00205258
	private void AnimateAndUpdateDisplays(CardHeroDbfRecord cardHeroDbfRecord, int cardBackIdx, CollectionHeroDef heroDef, bool purchased)
	{
		GameObject currDisplay = null;
		if (this.m_currentDisplay == -1)
		{
			this.m_currentDisplay = 1;
			currDisplay = this.m_heroEmptyDisplay;
		}
		else
		{
			currDisplay = this.GetCurrentDisplayContainer();
		}
		GameObject nextDisplayContainer = this.GetNextDisplayContainer();
		GeneralStoreHeroesContentDisplay currentDisplay = this.GetCurrentDisplay();
		this.m_currentDisplay = (this.m_currentDisplay + 1) % 2;
		currDisplay.transform.localRotation = Quaternion.identity;
		nextDisplayContainer.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
		nextDisplayContainer.SetActive(true);
		iTween.StopByName(currDisplay, "ROTATION_TWEEN");
		iTween.StopByName(nextDisplayContainer, "ROTATION_TWEEN");
		iTween.RotateBy(currDisplay, iTween.Hash(new object[]
		{
			"amount",
			new Vector3(0.5f, 0f, 0f),
			"time",
			0.5f,
			"name",
			"ROTATION_TWEEN",
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				currDisplay.SetActive(false);
			})
		}));
		if (this.m_currentSelectedHeroBannerFlare != null)
		{
			UnityEngine.Object.Destroy(this.m_currentSelectedHeroBannerFlare);
			this.m_currentSelectedHeroBannerFlare = null;
		}
		if (this.m_currentDbfRecord != null && !string.IsNullOrEmpty(this.m_currentDbfRecord.StoreBannerPrefab))
		{
			this.m_currentSelectedHeroBannerFlare = AssetLoader.Get().InstantiatePrefab(this.m_currentDbfRecord.StoreBannerPrefab, AssetLoadingOptions.None);
			if (this.m_currentSelectedHeroBannerFlare != null)
			{
				GameUtils.SetParent(this.m_currentSelectedHeroBannerFlare, nextDisplayContainer, false);
				this.m_currentSelectedHeroBannerFlare.transform.localPosition = Vector3.zero;
				this.m_currentSelectedHeroBannerFlare.transform.localRotation = Quaternion.identity;
				this.m_currentSelectedHeroBannerFlare.gameObject.SetActive(true);
			}
		}
		iTween.RotateBy(nextDisplayContainer, iTween.Hash(new object[]
		{
			"amount",
			new Vector3(0.5f, 0f, 0f),
			"time",
			0.5f,
			"name",
			"ROTATION_TWEEN"
		}));
		if (!string.IsNullOrEmpty(this.m_backgroundFlipSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_backgroundFlipSound);
		}
		GeneralStoreHeroesContentDisplay currentDisplay2 = this.GetCurrentDisplay();
		currentDisplay2.UpdateFrame(cardHeroDbfRecord, cardBackIdx, heroDef);
		currentDisplay2.ShowPurchasedCheckmark(purchased);
		currentDisplay2.ResetPreview();
		currentDisplay.ResetPreview();
	}

	// Token: 0x060063A2 RID: 25506 RVA: 0x002072DA File Offset: 0x002054DA
	private void ResetHeroPreview()
	{
		this.GetCurrentDisplay().ResetPreview();
	}

	// Token: 0x060063A3 RID: 25507 RVA: 0x002072E7 File Offset: 0x002054E7
	private void PlayHeroMusic()
	{
		if (this.m_currentHeroDef == null || this.m_currentHeroDef.m_heroPlaylist == MusicPlaylistType.Invalid || !MusicManager.Get().StartPlaylist(this.m_currentHeroDef.m_heroPlaylist))
		{
			this.m_parentStore.ResumePreviousMusicPlaylist();
		}
	}

	// Token: 0x060063A4 RID: 25508 RVA: 0x00207328 File Offset: 0x00205528
	private void UpdateHeroDescription(bool purchased)
	{
		if (this.m_currentDisplay == -1 || this.m_currentDbfRecord == null)
		{
			this.m_parentStore.SetChooseDescription(GameStrings.Get("GLUE_STORE_CHOOSE_HERO"));
		}
		else
		{
			string warning = StoreManager.Get().IsKoreanCustomer() ? GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_HERO") : string.Empty;
			this.m_parentStore.SetDescription(string.Empty, this.GetHeroDescriptionString(), warning);
		}
		this.m_parentStore.HideAccentTexture();
	}

	// Token: 0x060063A5 RID: 25509 RVA: 0x0020739D File Offset: 0x0020559D
	private string GetHeroDescriptionString()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return this.m_currentDbfRecord.StoreDesc;
		}
		return this.m_currentDbfRecord.StoreDescPhone;
	}

	// Token: 0x04005283 RID: 21123
	public string m_keyArtFadeAnim = "HeroSkinArt_WipeAway";

	// Token: 0x04005284 RID: 21124
	public string m_keyArtAppearAnim = "HeroSkinArtGlowIn";

	// Token: 0x04005285 RID: 21125
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_keyArtFadeSound;

	// Token: 0x04005286 RID: 21126
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_keyArtAppearSound;

	// Token: 0x04005287 RID: 21127
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_previewButtonClick;

	// Token: 0x04005288 RID: 21128
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_backgroundFlipSound;

	// Token: 0x04005289 RID: 21129
	public GameObject m_heroEmptyDisplay;

	// Token: 0x0400528A RID: 21130
	public GeneralStoreHeroesContentDisplay m_heroDisplay;

	// Token: 0x0400528B RID: 21131
	public MeshRenderer m_renderQuad1;

	// Token: 0x0400528C RID: 21132
	public GameObject m_renderToTexture1;

	// Token: 0x0400528D RID: 21133
	public MeshRenderer m_renderQuad2;

	// Token: 0x0400528E RID: 21134
	public GameObject m_renderToTexture2;

	// Token: 0x0400528F RID: 21135
	private GameObject m_currentSelectedHeroBannerFlare;

	// Token: 0x04005290 RID: 21136
	private CollectionHeroDef m_currentHeroDef;

	// Token: 0x04005291 RID: 21137
	private int m_currentCardBackPreview = -1;

	// Token: 0x04005292 RID: 21138
	private int m_currentDisplay = -1;

	// Token: 0x04005293 RID: 21139
	private CardHeroDbfRecord m_currentDbfRecord;

	// Token: 0x04005294 RID: 21140
	private GeneralStoreHeroesContentDisplay m_heroDisplay1;

	// Token: 0x04005295 RID: 21141
	private GeneralStoreHeroesContentDisplay m_heroDisplay2;
}
