using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.AssetManager;
using UnityEngine;

[CustomEditClass]
public class GeneralStoreAdventureContent : GeneralStoreContent
{
	public delegate void DelOnAdventurePreviewCardLoaded(Actor previewCard);

	[CustomEditField(Sections = "General Store")]
	public GeneralStoreAdventureContentDisplay m_adventureDisplay;

	[CustomEditField(Sections = "Animation/Preorder")]
	public GeneralStoreRewardsCardBack m_preorderCardBackReward;

	[CustomEditField(Sections = "General Store")]
	public GameObject m_adventureEmptyDisplay;

	[CustomEditField(Sections = "Rewards")]
	public GameObject m_adventureCardPreviewPanel;

	[CustomEditField(Sections = "Rewards")]
	public UberText m_adventureCardPreviewText;

	[CustomEditField(Sections = "Rewards")]
	public List<GameObject> m_adventureCardPreviewBones;

	[CustomEditField(Sections = "Rewards")]
	public PegUIElement m_adventureCardPreviewOffClicker;

	[CustomEditField(Sections = "General Store/Buttons")]
	public GameObject m_adventureRadioButtonContainer;

	[CustomEditField(Sections = "General Store/Buttons")]
	public UberText m_adventureRadioButtonText;

	[CustomEditField(Sections = "General Store/Buttons")]
	public UberText m_adventureRadioButtonCostText;

	[CustomEditField(Sections = "General Store/Buttons")]
	public RadioButton m_adventureRadioButton;

	[CustomEditField(Sections = "General Store/Buttons")]
	public GameObject m_adventureOwnedCheckmark;

	[CustomEditField(Sections = "Sounds & Music", T = EditType.SOUND_PREFAB)]
	public string m_backgroundFlipSound;

	[CustomEditField(Sections = "Animation")]
	public float m_backgroundFlipAnimTime = 0.5f;

	[CustomEditField(Sections = "Animation")]
	public float m_adventureLogoFadeInTime = 0.5f;

	private bool m_showPreviewCards;

	private Map<string, Actor> m_loadedPreviewCards = new Map<string, Actor>();

	private AdventureDbId m_selectedAdventureId;

	private Map<int, StoreAdventureDef> m_storeAdvDefs = new Map<int, StoreAdventureDef>();

	private int m_currentDisplay = -1;

	private GeneralStoreAdventureContentDisplay m_adventureDisplay1;

	private GeneralStoreAdventureContentDisplay m_adventureDisplay2;

	public static readonly bool REQUIRE_REAL_MONEY_BUNDLE_OPTION = true;

	private void Awake()
	{
		m_adventureDisplay1 = m_adventureDisplay;
		m_adventureDisplay2 = UnityEngine.Object.Instantiate(m_adventureDisplay);
		m_adventureDisplay2.transform.parent = m_adventureDisplay1.transform.parent;
		m_adventureDisplay2.transform.localPosition = m_adventureDisplay1.transform.localPosition;
		m_adventureDisplay2.transform.localScale = m_adventureDisplay1.transform.localScale;
		m_adventureDisplay2.transform.localRotation = m_adventureDisplay1.transform.localRotation;
		m_adventureDisplay2.gameObject.SetActive(value: false);
		if (m_adventureDisplay1.m_rewardChest != null)
		{
			m_adventureDisplay1.m_rewardChest.AddEventListener(UIEventType.ROLLOVER, OnAdventuresShowPreviewCard);
			m_adventureDisplay2.m_rewardChest.AddEventListener(UIEventType.ROLLOVER, OnAdventuresShowPreviewCard);
			if (!UniversalInputManager.UsePhoneUI)
			{
				m_adventureDisplay1.m_rewardChest.AddEventListener(UIEventType.ROLLOUT, OnAdventuresHidePreviewCard);
				m_adventureDisplay2.m_rewardChest.AddEventListener(UIEventType.ROLLOUT, OnAdventuresHidePreviewCard);
			}
		}
		AdventureProgressMgr.Get().RegisterProgressUpdatedListener(OnAdventureProgressUpdated);
		m_adventureCardPreviewPanel.SetActive(value: false);
		m_parentStore.SetChooseDescription(GameStrings.Get("GLUE_STORE_CHOOSE_ADVENTURE"));
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_adventureCardPreviewOffClicker.AddEventListener(UIEventType.RELEASE, OnAdventuresHidePreviewCard);
		}
		foreach (AdventureDbfRecord item in GameUtils.GetSortedAdventureRecordsWithStorePrefab())
		{
			string storePrefab = item.StorePrefab;
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(storePrefab);
			if (!(gameObject == null))
			{
				StoreAdventureDef component = gameObject.GetComponent<StoreAdventureDef>();
				if (component == null)
				{
					Debug.LogError($"StoreAdventureDef not found in object: {storePrefab}");
				}
				else
				{
					m_storeAdvDefs.Add(item.ID, component);
				}
			}
		}
	}

	private void OnDestroy()
	{
		AdventureProgressMgr.Get().RemoveProgressUpdatedListener(OnAdventureProgressUpdated);
	}

	public void SetAdventureId(AdventureDbId adventureId, bool forceImmediate = false)
	{
		if (m_selectedAdventureId != adventureId)
		{
			m_selectedAdventureId = adventureId;
			Network.Bundle bundle = null;
			StoreManager.Get().GetAvailableAdventureBundle(m_selectedAdventureId, REQUIRE_REAL_MONEY_BUNDLE_OPTION, out bundle);
			SetCurrentMoneyBundle(bundle);
			AnimateAndUpdateDisplay((int)adventureId, forceImmediate);
			AnimateAdventureRadioButtonBar();
			UpdateAdventureDescription(bundle);
			UpdateAdventureTypeMusic();
			UpdateRadioButtonText(bundle);
		}
	}

	public AdventureDbId GetAdventureId()
	{
		return m_selectedAdventureId;
	}

	public StoreAdventureDef GetStoreAdventureDef(int advId)
	{
		m_storeAdvDefs.TryGetValue(advId, out var value);
		return value;
	}

	public Map<int, StoreAdventureDef> GetStoreAdventureDefs()
	{
		return m_storeAdvDefs;
	}

	public override void PostStoreFlipIn(bool animateIn)
	{
		UpdateAdventureTypeMusic();
		Hashtable args = iTween.Hash("amount", 1f, "time", m_adventureLogoFadeInTime);
		iTween.FadeTo(GetCurrentDisplay().m_logo.gameObject, args);
		if (m_preorderCardBackReward != null && IsPreOrder())
		{
			m_preorderCardBackReward.ShowCardBackReward();
		}
	}

	public override void PreStoreFlipIn()
	{
		Hashtable args = iTween.Hash("amount", 0f, "time", 0);
		iTween.FadeTo(GetCurrentDisplay().m_logo.gameObject, args);
		if (m_preorderCardBackReward != null)
		{
			m_preorderCardBackReward.HideCardBackReward();
		}
	}

	public override void PreStoreFlipOut()
	{
		if (m_preorderCardBackReward != null)
		{
			m_preorderCardBackReward.HideCardBackReward();
		}
	}

	public override bool AnimateEntranceEnd()
	{
		m_adventureRadioButton.gameObject.SetActive(value: true);
		return true;
	}

	public override bool AnimateExitStart()
	{
		m_adventureRadioButton.gameObject.SetActive(value: false);
		return true;
	}

	public override bool AnimateExitEnd()
	{
		return true;
	}

	public override void TryBuyWithMoney(Network.Bundle bundle, BuyEvent successBuyCB, BuyEvent failedBuyCB)
	{
		if (IsContentActive())
		{
			if (!AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.VANILLA_HEROES))
			{
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_headerText = GameStrings.Get("GLUE_STORE_ADVENTURE_LOCKED_HEROES_NOT_PURCHASABLE_TITLE");
				popupInfo.m_text = GameStrings.Get("GLUE_STORE_ADVENTURE_LOCKED_HEROES_NOT_PURCHASABLE_TEXT");
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
		else if (failedBuyCB != null)
		{
			failedBuyCB();
		}
	}

	public override void TryBuyWithGold(BuyEvent successBuyCB = null, BuyEvent failedBuyCB = null)
	{
		successBuyCB?.Invoke();
	}

	protected override void OnRefresh()
	{
		Network.Bundle bundle = null;
		StoreManager.Get().GetAvailableAdventureBundle(m_selectedAdventureId, REQUIRE_REAL_MONEY_BUNDLE_OPTION, out bundle);
		SetCurrentMoneyBundle(bundle);
		UpdateRadioButtonText(bundle);
		UpdateAdventureDescription(bundle);
	}

	protected override void OnBundleChanged(NoGTAPPTransactionData goldBundle, Network.Bundle moneyBundle)
	{
		UpdateRadioButtonText(moneyBundle);
		UpdateAdventureDescription(moneyBundle);
	}

	public override void StoreShown(bool isCurrent)
	{
		if (isCurrent)
		{
			UpdateAdventureTypeMusic();
		}
	}

	public override void StoreHidden(bool isCurrent)
	{
		foreach (KeyValuePair<string, Actor> loadedPreviewCard in m_loadedPreviewCards)
		{
			UnityEngine.Object.Destroy(loadedPreviewCard.Value.gameObject);
		}
		m_loadedPreviewCards.Clear();
		if (isCurrent)
		{
			HidePreviewCardPanel();
		}
	}

	public override bool IsPurchaseDisabled()
	{
		return m_selectedAdventureId == AdventureDbId.INVALID;
	}

	public override string GetMoneyDisplayOwnedText()
	{
		return GameStrings.Get("GLUE_STORE_DUNGEON_BUTTON_COST_OWNED_TEXT");
	}

	private GameObject GetCurrentDisplayContainer()
	{
		return GetCurrentDisplay().gameObject;
	}

	private GameObject GetNextDisplayContainer()
	{
		if ((m_currentDisplay + 1) % 2 != 0)
		{
			return m_adventureDisplay2.gameObject;
		}
		return m_adventureDisplay1.gameObject;
	}

	private GeneralStoreAdventureContentDisplay GetCurrentDisplay()
	{
		if (m_currentDisplay != 0)
		{
			return m_adventureDisplay2;
		}
		return m_adventureDisplay1;
	}

	private void OnAdventuresShowPreviewCard(UIEvent e)
	{
		StoreAdventureDef storeAdventureDef = GetStoreAdventureDef((int)m_selectedAdventureId);
		if (storeAdventureDef == null)
		{
			Debug.LogError($"Unable to find preview cards for {m_selectedAdventureId} adventure.");
			return;
		}
		string[] previewCards = storeAdventureDef.m_previewCards.ToArray();
		if (previewCards.Length == 0)
		{
			Debug.LogError($"No preview cards defined for {m_selectedAdventureId} adventure.");
			return;
		}
		m_showPreviewCards = true;
		SoundManager.Get().LoadAndPlay("collection_manager_card_mouse_over.prefab:0d4e20bc78956bc48b5e2963ec39211c");
		foreach (KeyValuePair<string, Actor> loadedPreviewCard in m_loadedPreviewCards)
		{
			loadedPreviewCard.Value.gameObject.SetActive(value: false);
		}
		int loadedPreviewCards = 0;
		int num = 0;
		string[] array = previewCards;
		foreach (string previewCard in array)
		{
			int cardIndex = num;
			LoadAdventurePreviewCard(previewCard, delegate(Actor cardActor)
			{
				cardActor.transform.position = m_adventureCardPreviewBones[cardIndex].transform.position;
				cardActor.transform.rotation = m_adventureCardPreviewBones[cardIndex].transform.rotation;
				cardActor.transform.parent = m_adventureCardPreviewBones[cardIndex].transform;
				cardActor.transform.localScale = Vector3.one;
				int num2 = loadedPreviewCards + 1;
				loadedPreviewCards = num2;
				cardActor.gameObject.SetActive(m_showPreviewCards);
				if (m_showPreviewCards && loadedPreviewCards == previewCards.Length)
				{
					ShowPreviewCardPanel();
				}
			});
			num++;
		}
	}

	private void LoadAdventurePreviewCard(string previewCard, DelOnAdventurePreviewCardLoaded onLoadComplete)
	{
		if (m_loadedPreviewCards.TryGetValue(previewCard, out var value))
		{
			onLoadComplete(value);
			return;
		}
		DefLoader.Get().LoadFullDef(previewCard, delegate(string cardID, DefLoader.DisposableFullDef fullDef, object data)
		{
			AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(fullDef.EntityDef), delegate(AssetReference actorName, GameObject actorObject, object data2)
			{
				using (fullDef)
				{
					if (actorObject == null)
					{
						Debug.LogWarning($"FAILED to load actor \"{actorName}\"");
						onLoadComplete(null);
					}
					else
					{
						Actor component = actorObject.GetComponent<Actor>();
						if (component == null)
						{
							Debug.LogWarning($"ERROR actor \"{actorName}\" has no Actor component");
							onLoadComplete(null);
						}
						else
						{
							component.SetFullDef(fullDef);
							component.UpdateAllComponents();
							SceneUtils.SetLayer(component.gameObject, base.gameObject.layer);
							component.Show();
							m_loadedPreviewCards.Add(previewCard, component);
							onLoadComplete(component);
						}
					}
				}
			}, null, AssetLoadingOptions.IgnorePrefabPosition);
		});
	}

	private void OnAdventuresHidePreviewCard(UIEvent e)
	{
		m_showPreviewCards = false;
		SoundManager.Get().LoadAndPlay("card_shrink.prefab:a4e6170a9f153f94cacee42db7c327fb");
		HidePreviewCardPanel();
	}

	private void ShowPreviewCardPanel()
	{
		m_adventureCardPreviewPanel.SetActive(value: true);
		m_adventureCardPreviewPanel.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.StopByName(m_adventureCardPreviewPanel, "PreviewCardPanelScale");
		iTween.ScaleTo(m_adventureCardPreviewPanel, iTween.Hash("scale", Vector3.one, "time", 0.1f, "name", "PreviewCardPanelScale", "easetype", iTween.EaseType.linear));
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_parentStore.BlockInterface(blocked: true);
		}
	}

	private void HidePreviewCardPanel()
	{
		iTween.StopByName(m_adventureCardPreviewPanel, "PreviewCardPanelScale");
		iTween.ScaleTo(m_adventureCardPreviewPanel, iTween.Hash("scale", new Vector3(0.02f, 0.02f, 0.02f), "time", 0.1f, "name", "PreviewCardPanelScale", "oncomplete", (Action<object>)delegate
		{
			m_adventureCardPreviewPanel.SetActive(value: false);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				m_parentStore.BlockInterface(blocked: false);
			}
		}, "easetype", iTween.EaseType.linear));
	}

	private void UpdateRadioButtonText(Network.Bundle moneyBundle)
	{
		m_adventureRadioButton.SetSelected(selected: true);
		if (moneyBundle == null)
		{
			m_adventureRadioButtonText.Text = GameStrings.Get("GLUE_STORE_DUNGEON_BUTTON_TEXT_PURCHASED");
			m_adventureRadioButtonText.Anchor = UberText.AnchorOptions.Middle;
			m_adventureRadioButtonCostText.Text = string.Empty;
		}
		else
		{
			string key;
			if (IsPreOrder())
			{
				AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)m_selectedAdventureId);
				key = ((record == null || string.IsNullOrEmpty(record.StorePreorderRadioText)) ? "GLUE_STORE_DUNGEON_BUTTON_PREORDER_TEXT" : ((string)record.StorePreorderRadioText));
			}
			else
			{
				key = "GLUE_STORE_DUNGEON_BUTTON_TEXT";
			}
			m_adventureRadioButtonText.Text = GameStrings.Get(key);
			m_adventureRadioButtonText.Anchor = UberText.AnchorOptions.Upper;
			string text = StoreManager.Get().FormatCostBundle(moneyBundle);
			int wingItemCount = StoreManager.Get().GetWingItemCount(moneyBundle.Items);
			m_adventureRadioButtonCostText.Text = GameStrings.Format("GLUE_STORE_DUNGEON_BUTTON_COST_TEXT", wingItemCount, text);
		}
		if (m_adventureOwnedCheckmark != null)
		{
			m_adventureOwnedCheckmark.SetActive(moneyBundle == null);
		}
	}

	private void UpdateAdventureDescription(Network.Bundle bundle)
	{
		if (m_selectedAdventureId != 0)
		{
			string title = string.Empty;
			string desc = string.Empty;
			string warning = string.Empty;
			AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)m_selectedAdventureId);
			if (record == null)
			{
				Debug.LogError($"Unable to find adventure record ID: {m_selectedAdventureId}");
			}
			else if (bundle == null)
			{
				title = record.StoreOwnedHeadline;
				desc = record.StoreOwnedDesc;
			}
			else if (IsPreOrder())
			{
				title = record.StorePreorderHeadline;
				int wingItemCount = StoreManager.Get().GetWingItemCount(bundle.Items);
				DbfLocValue dbfLocValue = record.GetVar($"STORE_PREORDER_WINGS_{wingItemCount}_DESC") as DbfLocValue;
				desc = ((dbfLocValue == null) ? "" : dbfLocValue.GetString());
			}
			else
			{
				int wingItemCount2 = StoreManager.Get().GetWingItemCount(bundle.Items);
				DbfLocValue dbfLocValue2 = record.GetVar($"STORE_BUY_WINGS_{wingItemCount2}_HEADLINE") as DbfLocValue;
				DbfLocValue dbfLocValue3 = record.GetVar($"STORE_BUY_WINGS_{wingItemCount2}_DESC") as DbfLocValue;
				title = ((dbfLocValue2 == null) ? "" : dbfLocValue2.GetString());
				desc = ((dbfLocValue3 == null) ? "" : dbfLocValue3.GetString());
			}
			if (StoreManager.Get().IsKoreanCustomer())
			{
				warning = GameStrings.Get("GLUE_STORE_KOREAN_PRODUCT_DETAILS_ADVENTURE");
			}
			if (m_adventureCardPreviewText != null)
			{
				m_adventureCardPreviewText.Text = record.StorePreviewRewardsText;
			}
			m_parentStore.SetDescription(title, desc, warning);
			StoreAdventureDef storeAdventureDef = GetStoreAdventureDef((int)m_selectedAdventureId);
			if (storeAdventureDef != null)
			{
				using AssetHandle<Texture> accentTexture = AssetLoader.Get().LoadAsset<Texture>(storeAdventureDef.m_accentTextureName);
				m_parentStore.SetAccentTexture(accentTexture);
			}
		}
		else
		{
			m_parentStore.HideAccentTexture();
			m_parentStore.SetChooseDescription(GameStrings.Get("GLUE_STORE_CHOOSE_ADVENTURE"));
		}
	}

	private void UpdateAdventureTypeMusic()
	{
		if (m_parentStore.GetMode() != 0)
		{
			StoreAdventureDef storeAdventureDef = GetStoreAdventureDef((int)m_selectedAdventureId);
			if (storeAdventureDef == null || storeAdventureDef.m_playlist == MusicPlaylistType.Invalid || !MusicManager.Get().StartPlaylist(storeAdventureDef.m_playlist))
			{
				m_parentStore.ResumePreviousMusicPlaylist();
			}
		}
	}

	private void AnimateAndUpdateDisplay(int id, bool forceImmediate)
	{
		if (m_preorderCardBackReward != null)
		{
			m_preorderCardBackReward.HideCardBackReward();
		}
		GameObject currDisplay = null;
		GameObject gameObject = null;
		if (m_currentDisplay == -1)
		{
			m_currentDisplay = 1;
			currDisplay = m_adventureEmptyDisplay;
		}
		else
		{
			currDisplay = GetCurrentDisplayContainer();
		}
		gameObject = GetNextDisplayContainer();
		m_currentDisplay = (m_currentDisplay + 1) % 2;
		gameObject.SetActive(value: true);
		if (!forceImmediate)
		{
			currDisplay.transform.localRotation = Quaternion.identity;
			gameObject.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
			iTween.StopByName(currDisplay, "ROTATION_TWEEN");
			iTween.StopByName(gameObject, "ROTATION_TWEEN");
			iTween.RotateBy(currDisplay, iTween.Hash("amount", new Vector3(0.5f, 0f, 0f), "time", 0.5f, "name", "ROTATION_TWEEN", "oncomplete", (Action<object>)delegate
			{
				currDisplay.SetActive(value: false);
			}));
			iTween.RotateBy(gameObject, iTween.Hash("amount", new Vector3(0.5f, 0f, 0f), "time", 0.5f, "name", "ROTATION_TWEEN"));
			if (!string.IsNullOrEmpty(m_backgroundFlipSound))
			{
				SoundManager.Get().LoadAndPlay(m_backgroundFlipSound);
			}
		}
		else
		{
			gameObject.transform.localRotation = Quaternion.identity;
			currDisplay.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
			currDisplay.SetActive(value: false);
		}
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord(id);
		bool flag = IsPreOrder();
		StoreAdventureDef storeAdventureDef = GetStoreAdventureDef(id);
		GeneralStoreAdventureContentDisplay currentDisplay = GetCurrentDisplay();
		currentDisplay.UpdateAdventureType(storeAdventureDef, record);
		currentDisplay.SetPreOrder(flag);
		if (m_preorderCardBackReward != null && flag)
		{
			m_preorderCardBackReward.SetCardBack(storeAdventureDef.m_preorderCardBackId);
			m_preorderCardBackReward.SetPreorderText(storeAdventureDef.m_preorderCardBackTextName);
			m_preorderCardBackReward.ShowCardBackReward();
		}
	}

	private void AnimateAdventureRadioButtonBar()
	{
		if (!(m_adventureRadioButtonContainer == null))
		{
			m_adventureRadioButtonContainer.SetActive(value: false);
			if (m_selectedAdventureId != 0)
			{
				iTween.Stop(m_adventureRadioButtonContainer);
				m_adventureRadioButtonContainer.transform.localRotation = Quaternion.identity;
				m_adventureRadioButtonContainer.SetActive(value: true);
				m_adventureRadioButton.SetSelected(selected: true);
				iTween.RotateBy(m_adventureRadioButtonContainer, iTween.Hash("amount", new Vector3(-1f, 0f, 0f), "time", m_backgroundFlipAnimTime, "delay", 0.001f));
			}
		}
	}

	private void OnAdventureProgressUpdated(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress, object userData)
	{
		if (newProgress == null || (oldProgress != null && oldProgress.IsOwned()) || !newProgress.IsOwned())
		{
			return;
		}
		WingDbfRecord record = GameDbf.Wing.GetRecord(newProgress.Wing);
		if (record != null && record.AdventureId == (int)m_selectedAdventureId)
		{
			Network.Bundle bundle = null;
			StoreManager.Get().GetAvailableAdventureBundle(m_selectedAdventureId, REQUIRE_REAL_MONEY_BUNDLE_OPTION, out bundle);
			SetCurrentMoneyBundle(bundle);
			if (m_parentStore != null)
			{
				m_parentStore.RefreshContent();
			}
		}
	}

	private bool IsPreOrder()
	{
		Network.Bundle currentMoneyBundle = GetCurrentMoneyBundle();
		if (currentMoneyBundle != null)
		{
			return StoreManager.Get().IsProductPrePurchase(currentMoneyBundle);
		}
		return false;
	}
}
