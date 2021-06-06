using System;
using UnityEngine;

[CustomEditClass]
public class GeneralStoreHeroesContent : GeneralStoreContent
{
	public string m_keyArtFadeAnim = "HeroSkinArt_WipeAway";

	public string m_keyArtAppearAnim = "HeroSkinArtGlowIn";

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_keyArtFadeSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_keyArtAppearSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_previewButtonClick;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_backgroundFlipSound;

	public GameObject m_heroEmptyDisplay;

	public GeneralStoreHeroesContentDisplay m_heroDisplay;

	public MeshRenderer m_renderQuad1;

	public GameObject m_renderToTexture1;

	public MeshRenderer m_renderQuad2;

	public GameObject m_renderToTexture2;

	private GameObject m_currentSelectedHeroBannerFlare;

	private CollectionHeroDef m_currentHeroDef;

	private int m_currentCardBackPreview = -1;

	private int m_currentDisplay = -1;

	private CardHeroDbfRecord m_currentDbfRecord;

	private GeneralStoreHeroesContentDisplay m_heroDisplay1;

	private GeneralStoreHeroesContentDisplay m_heroDisplay2;

	private void Awake()
	{
		m_heroDisplay1 = m_heroDisplay;
		m_heroDisplay2 = UnityEngine.Object.Instantiate(m_heroDisplay);
		m_heroDisplay2.transform.parent = m_heroDisplay1.transform.parent;
		m_heroDisplay2.transform.localPosition = m_heroDisplay1.transform.localPosition;
		m_heroDisplay2.transform.localScale = m_heroDisplay1.transform.localScale;
		m_heroDisplay2.transform.localRotation = m_heroDisplay1.transform.localRotation;
		m_heroDisplay2.gameObject.SetActive(value: false);
		m_heroDisplay1.SetParent(this);
		m_heroDisplay2.SetParent(this);
		m_heroDisplay1.SetKeyArtRenderer(m_renderQuad1);
		m_heroDisplay2.SetKeyArtRenderer(m_renderQuad2);
		m_renderToTexture1.GetComponent<RenderToTexture>().m_RenderToObject = m_heroDisplay1.m_renderArtQuad;
		m_renderToTexture2.GetComponent<RenderToTexture>().m_RenderToObject = m_heroDisplay2.m_renderArtQuad;
	}

	public override bool AnimateEntranceEnd()
	{
		m_parentStore.HideAccentTexture();
		return true;
	}

	public CardHeroDbfRecord GetSelectedHero()
	{
		return m_currentDbfRecord;
	}

	public void SelectHero(CardHeroDbfRecord cardHeroDbfRecord, bool animate = true)
	{
		if (cardHeroDbfRecord != m_currentDbfRecord)
		{
			m_currentDbfRecord = cardHeroDbfRecord;
			Network.Bundle heroBundle = null;
			StoreManager.Get().GetHeroBundleByCardDbId(cardHeroDbfRecord.CardId, out heroBundle);
			SetCurrentMoneyBundle(heroBundle);
			if (m_currentHeroDef != null)
			{
				UnityEngine.Object.Destroy(m_currentHeroDef.gameObject);
				m_currentHeroDef = null;
			}
			m_currentCardBackPreview = cardHeroDbfRecord.CardBackId;
			using DefLoader.DisposableCardDef disposableCardDef = DefLoader.Get().GetCardDef(m_currentDbfRecord.CardId);
			m_currentHeroDef = GameUtils.LoadGameObjectWithComponent<CollectionHeroDef>(disposableCardDef.CardDef.m_CollectionHeroDefPath);
			bool purchased = StoreManager.Get().IsProductAlreadyOwned(heroBundle);
			AnimateAndUpdateDisplays(cardHeroDbfRecord, m_currentCardBackPreview, m_currentHeroDef, purchased);
			PlayHeroMusic();
			UpdateHeroDescription(purchased);
		}
	}

	public void PlayCurrentHeroPurchaseEmote()
	{
		GeneralStoreHeroesContentDisplay currentDisplay = GetCurrentDisplay();
		if (currentDisplay != null)
		{
			currentDisplay.PlayPurchaseEmote();
		}
	}

	public override void StoreShown(bool isCurrent)
	{
		if (m_currentDisplay != -1 && isCurrent)
		{
			PlayHeroMusic();
			ResetHeroPreview();
		}
	}

	public override void PreStoreFlipIn()
	{
		ResetHeroPreview();
	}

	public override void PostStoreFlipIn(bool animatedFlipIn)
	{
		PlayHeroMusic();
	}

	public override void TryBuyWithMoney(Network.Bundle bundle, BuyEvent successBuyCB, BuyEvent failedBuyCB)
	{
		SpecialEventManager specialEventManager = SpecialEventManager.Get();
		SpecialEventType eventType = SpecialEventManager.GetEventType(bundle.ProductEvent);
		if (!specialEventManager.IsEventActive(eventType, activeIfDoesNotExist: false))
		{
			string key = "GLUE_STORE_PRODUCT_NOT_AVAILABLE_TEXT";
			if (specialEventManager.HasEventEnded(eventType))
			{
				key = "GLUE_STORE_PRODUCT_NOT_AVAILABLE_TEXT_HAS_ENDED";
			}
			else if (specialEventManager.GetEventStartTimeUtc(eventType).HasValue && !specialEventManager.HasEventStarted(eventType))
			{
				key = "GLUE_STORE_PRODUCT_NOT_AVAILABLE_TEXT_NOT_YET_STARTED";
			}
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_STORE_PRODUCT_NOT_AVAILABLE_HEADER");
			popupInfo.m_text = GameStrings.Get(key);
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_responseCallback = delegate
			{
				m_parentStore.BlockInterface(blocked: false);
				if (failedBuyCB != null)
				{
					failedBuyCB();
				}
			};
			m_parentStore.BlockInterface(blocked: true);
			DialogManager.Get().ShowPopup(popupInfo);
		}
		else
		{
			successBuyCB?.Invoke();
		}
	}

	protected override void OnRefresh()
	{
		Network.Bundle heroBundle = null;
		if (m_currentDbfRecord != null)
		{
			StoreManager.Get().GetHeroBundleByCardDbId(m_currentDbfRecord.CardId, out heroBundle);
		}
		bool flag = StoreManager.Get().IsProductAlreadyOwned(heroBundle);
		GetCurrentDisplay().ShowPurchasedCheckmark(flag);
		SetCurrentMoneyBundle(heroBundle, force: true);
		UpdateHeroDescription(flag);
	}

	public override bool IsPurchaseDisabled()
	{
		return m_currentDisplay == -1;
	}

	public override string GetMoneyDisplayOwnedText()
	{
		return GameStrings.Get("GLUE_STORE_HERO_BUTTON_COST_OWNED_TEXT");
	}

	private GameObject GetCurrentDisplayContainer()
	{
		return GetCurrentDisplay().gameObject;
	}

	private GameObject GetNextDisplayContainer()
	{
		if ((m_currentDisplay + 1) % 2 != 0)
		{
			return m_heroDisplay2.gameObject;
		}
		return m_heroDisplay1.gameObject;
	}

	private GeneralStoreHeroesContentDisplay GetCurrentDisplay()
	{
		if (m_currentDisplay != 0)
		{
			return m_heroDisplay2;
		}
		return m_heroDisplay1;
	}

	private void AnimateAndUpdateDisplays(CardHeroDbfRecord cardHeroDbfRecord, int cardBackIdx, CollectionHeroDef heroDef, bool purchased)
	{
		GameObject currDisplay = null;
		GameObject gameObject = null;
		if (m_currentDisplay == -1)
		{
			m_currentDisplay = 1;
			currDisplay = m_heroEmptyDisplay;
		}
		else
		{
			currDisplay = GetCurrentDisplayContainer();
		}
		gameObject = GetNextDisplayContainer();
		GeneralStoreHeroesContentDisplay currentDisplay = GetCurrentDisplay();
		m_currentDisplay = (m_currentDisplay + 1) % 2;
		currDisplay.transform.localRotation = Quaternion.identity;
		gameObject.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
		gameObject.SetActive(value: true);
		iTween.StopByName(currDisplay, "ROTATION_TWEEN");
		iTween.StopByName(gameObject, "ROTATION_TWEEN");
		iTween.RotateBy(currDisplay, iTween.Hash("amount", new Vector3(0.5f, 0f, 0f), "time", 0.5f, "name", "ROTATION_TWEEN", "oncomplete", (Action<object>)delegate
		{
			currDisplay.SetActive(value: false);
		}));
		if (m_currentSelectedHeroBannerFlare != null)
		{
			UnityEngine.Object.Destroy(m_currentSelectedHeroBannerFlare);
			m_currentSelectedHeroBannerFlare = null;
		}
		if (m_currentDbfRecord != null && !string.IsNullOrEmpty(m_currentDbfRecord.StoreBannerPrefab))
		{
			m_currentSelectedHeroBannerFlare = AssetLoader.Get().InstantiatePrefab(m_currentDbfRecord.StoreBannerPrefab);
			if (m_currentSelectedHeroBannerFlare != null)
			{
				GameUtils.SetParent(m_currentSelectedHeroBannerFlare, gameObject);
				m_currentSelectedHeroBannerFlare.transform.localPosition = Vector3.zero;
				m_currentSelectedHeroBannerFlare.transform.localRotation = Quaternion.identity;
				m_currentSelectedHeroBannerFlare.gameObject.SetActive(value: true);
			}
		}
		iTween.RotateBy(gameObject, iTween.Hash("amount", new Vector3(0.5f, 0f, 0f), "time", 0.5f, "name", "ROTATION_TWEEN"));
		if (!string.IsNullOrEmpty(m_backgroundFlipSound))
		{
			SoundManager.Get().LoadAndPlay(m_backgroundFlipSound);
		}
		GeneralStoreHeroesContentDisplay currentDisplay2 = GetCurrentDisplay();
		currentDisplay2.UpdateFrame(cardHeroDbfRecord, cardBackIdx, heroDef);
		currentDisplay2.ShowPurchasedCheckmark(purchased);
		currentDisplay2.ResetPreview();
		currentDisplay.ResetPreview();
	}

	private void ResetHeroPreview()
	{
		GetCurrentDisplay().ResetPreview();
	}

	private void PlayHeroMusic()
	{
		if (m_currentHeroDef == null || m_currentHeroDef.m_heroPlaylist == MusicPlaylistType.Invalid || !MusicManager.Get().StartPlaylist(m_currentHeroDef.m_heroPlaylist))
		{
			m_parentStore.ResumePreviousMusicPlaylist();
		}
	}

	private void UpdateHeroDescription(bool purchased)
	{
		if (m_currentDisplay == -1 || m_currentDbfRecord == null)
		{
			m_parentStore.SetChooseDescription(GameStrings.Get("GLUE_STORE_CHOOSE_HERO"));
		}
		else
		{
			string warning = (StoreManager.Get().IsKoreanCustomer() ? GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_HERO") : string.Empty);
			m_parentStore.SetDescription(string.Empty, GetHeroDescriptionString(), warning);
		}
		m_parentStore.HideAccentTexture();
	}

	private string GetHeroDescriptionString()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return m_currentDbfRecord.StoreDesc;
		}
		return m_currentDbfRecord.StoreDescPhone;
	}
}
