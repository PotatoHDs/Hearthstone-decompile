using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x020006F3 RID: 1779
[CustomEditClass]
public class GeneralStoreAdventureContent : GeneralStoreContent
{
	// Token: 0x0600632F RID: 25391 RVA: 0x0020510C File Offset: 0x0020330C
	private void Awake()
	{
		this.m_adventureDisplay1 = this.m_adventureDisplay;
		this.m_adventureDisplay2 = UnityEngine.Object.Instantiate<GeneralStoreAdventureContentDisplay>(this.m_adventureDisplay);
		this.m_adventureDisplay2.transform.parent = this.m_adventureDisplay1.transform.parent;
		this.m_adventureDisplay2.transform.localPosition = this.m_adventureDisplay1.transform.localPosition;
		this.m_adventureDisplay2.transform.localScale = this.m_adventureDisplay1.transform.localScale;
		this.m_adventureDisplay2.transform.localRotation = this.m_adventureDisplay1.transform.localRotation;
		this.m_adventureDisplay2.gameObject.SetActive(false);
		if (this.m_adventureDisplay1.m_rewardChest != null)
		{
			this.m_adventureDisplay1.m_rewardChest.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnAdventuresShowPreviewCard));
			this.m_adventureDisplay2.m_rewardChest.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnAdventuresShowPreviewCard));
			if (!UniversalInputManager.UsePhoneUI)
			{
				this.m_adventureDisplay1.m_rewardChest.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnAdventuresHidePreviewCard));
				this.m_adventureDisplay2.m_rewardChest.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnAdventuresHidePreviewCard));
			}
		}
		AdventureProgressMgr.Get().RegisterProgressUpdatedListener(new AdventureProgressMgr.AdventureProgressUpdatedCallback(this.OnAdventureProgressUpdated));
		this.m_adventureCardPreviewPanel.SetActive(false);
		this.m_parentStore.SetChooseDescription(GameStrings.Get("GLUE_STORE_CHOOSE_ADVENTURE"));
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_adventureCardPreviewOffClicker.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnAdventuresHidePreviewCard));
		}
		foreach (AdventureDbfRecord adventureDbfRecord in GameUtils.GetSortedAdventureRecordsWithStorePrefab())
		{
			string storePrefab = adventureDbfRecord.StorePrefab;
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(storePrefab, AssetLoadingOptions.None);
			if (!(gameObject == null))
			{
				StoreAdventureDef component = gameObject.GetComponent<StoreAdventureDef>();
				if (component == null)
				{
					Debug.LogError(string.Format("StoreAdventureDef not found in object: {0}", storePrefab));
				}
				else
				{
					this.m_storeAdvDefs.Add(adventureDbfRecord.ID, component);
				}
			}
		}
	}

	// Token: 0x06006330 RID: 25392 RVA: 0x00205358 File Offset: 0x00203558
	private void OnDestroy()
	{
		AdventureProgressMgr.Get().RemoveProgressUpdatedListener(new AdventureProgressMgr.AdventureProgressUpdatedCallback(this.OnAdventureProgressUpdated));
	}

	// Token: 0x06006331 RID: 25393 RVA: 0x00205374 File Offset: 0x00203574
	public void SetAdventureId(AdventureDbId adventureId, bool forceImmediate = false)
	{
		if (this.m_selectedAdventureId == adventureId)
		{
			return;
		}
		this.m_selectedAdventureId = adventureId;
		Network.Bundle bundle = null;
		StoreManager.Get().GetAvailableAdventureBundle(this.m_selectedAdventureId, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION, out bundle);
		base.SetCurrentMoneyBundle(bundle, false);
		this.AnimateAndUpdateDisplay((int)adventureId, forceImmediate);
		this.AnimateAdventureRadioButtonBar();
		this.UpdateAdventureDescription(bundle);
		this.UpdateAdventureTypeMusic();
		this.UpdateRadioButtonText(bundle);
	}

	// Token: 0x06006332 RID: 25394 RVA: 0x002053D6 File Offset: 0x002035D6
	public AdventureDbId GetAdventureId()
	{
		return this.m_selectedAdventureId;
	}

	// Token: 0x06006333 RID: 25395 RVA: 0x002053E0 File Offset: 0x002035E0
	public StoreAdventureDef GetStoreAdventureDef(int advId)
	{
		StoreAdventureDef result;
		this.m_storeAdvDefs.TryGetValue(advId, out result);
		return result;
	}

	// Token: 0x06006334 RID: 25396 RVA: 0x002053FD File Offset: 0x002035FD
	public Map<int, StoreAdventureDef> GetStoreAdventureDefs()
	{
		return this.m_storeAdvDefs;
	}

	// Token: 0x06006335 RID: 25397 RVA: 0x00205408 File Offset: 0x00203608
	public override void PostStoreFlipIn(bool animateIn)
	{
		this.UpdateAdventureTypeMusic();
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			1f,
			"time",
			this.m_adventureLogoFadeInTime
		});
		iTween.FadeTo(this.GetCurrentDisplay().m_logo.gameObject, args);
		if (this.m_preorderCardBackReward != null && this.IsPreOrder())
		{
			this.m_preorderCardBackReward.ShowCardBackReward();
		}
	}

	// Token: 0x06006336 RID: 25398 RVA: 0x0020548C File Offset: 0x0020368C
	public override void PreStoreFlipIn()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			0f,
			"time",
			0
		});
		iTween.FadeTo(this.GetCurrentDisplay().m_logo.gameObject, args);
		if (this.m_preorderCardBackReward != null)
		{
			this.m_preorderCardBackReward.HideCardBackReward();
		}
	}

	// Token: 0x06006337 RID: 25399 RVA: 0x002054FA File Offset: 0x002036FA
	public override void PreStoreFlipOut()
	{
		if (this.m_preorderCardBackReward != null)
		{
			this.m_preorderCardBackReward.HideCardBackReward();
		}
	}

	// Token: 0x06006338 RID: 25400 RVA: 0x00205515 File Offset: 0x00203715
	public override bool AnimateEntranceEnd()
	{
		this.m_adventureRadioButton.gameObject.SetActive(true);
		return true;
	}

	// Token: 0x06006339 RID: 25401 RVA: 0x00205529 File Offset: 0x00203729
	public override bool AnimateExitStart()
	{
		this.m_adventureRadioButton.gameObject.SetActive(false);
		return true;
	}

	// Token: 0x0600633A RID: 25402 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool AnimateExitEnd()
	{
		return true;
	}

	// Token: 0x0600633B RID: 25403 RVA: 0x00205540 File Offset: 0x00203740
	public override void TryBuyWithMoney(Network.Bundle bundle, GeneralStoreContent.BuyEvent successBuyCB, GeneralStoreContent.BuyEvent failedBuyCB)
	{
		if (base.IsContentActive())
		{
			if (!AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.VANILLA_HEROES))
			{
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_headerText = GameStrings.Get("GLUE_STORE_ADVENTURE_LOCKED_HEROES_NOT_PURCHASABLE_TITLE");
				popupInfo.m_text = GameStrings.Get("GLUE_STORE_ADVENTURE_LOCKED_HEROES_NOT_PURCHASABLE_TEXT");
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
				return;
			}
		}
		else if (failedBuyCB != null)
		{
			failedBuyCB();
		}
	}

	// Token: 0x0600633C RID: 25404 RVA: 0x002055F2 File Offset: 0x002037F2
	public override void TryBuyWithGold(GeneralStoreContent.BuyEvent successBuyCB = null, GeneralStoreContent.BuyEvent failedBuyCB = null)
	{
		if (successBuyCB != null)
		{
			successBuyCB();
		}
	}

	// Token: 0x0600633D RID: 25405 RVA: 0x00205600 File Offset: 0x00203800
	protected override void OnRefresh()
	{
		Network.Bundle bundle = null;
		StoreManager.Get().GetAvailableAdventureBundle(this.m_selectedAdventureId, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION, out bundle);
		base.SetCurrentMoneyBundle(bundle, false);
		this.UpdateRadioButtonText(bundle);
		this.UpdateAdventureDescription(bundle);
	}

	// Token: 0x0600633E RID: 25406 RVA: 0x0020563D File Offset: 0x0020383D
	protected override void OnBundleChanged(NoGTAPPTransactionData goldBundle, Network.Bundle moneyBundle)
	{
		this.UpdateRadioButtonText(moneyBundle);
		this.UpdateAdventureDescription(moneyBundle);
	}

	// Token: 0x0600633F RID: 25407 RVA: 0x0020564D File Offset: 0x0020384D
	public override void StoreShown(bool isCurrent)
	{
		if (!isCurrent)
		{
			return;
		}
		this.UpdateAdventureTypeMusic();
	}

	// Token: 0x06006340 RID: 25408 RVA: 0x0020565C File Offset: 0x0020385C
	public override void StoreHidden(bool isCurrent)
	{
		foreach (KeyValuePair<string, Actor> keyValuePair in this.m_loadedPreviewCards)
		{
			UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
		}
		this.m_loadedPreviewCards.Clear();
		if (!isCurrent)
		{
			return;
		}
		this.HidePreviewCardPanel();
	}

	// Token: 0x06006341 RID: 25409 RVA: 0x002056D0 File Offset: 0x002038D0
	public override bool IsPurchaseDisabled()
	{
		return this.m_selectedAdventureId == AdventureDbId.INVALID;
	}

	// Token: 0x06006342 RID: 25410 RVA: 0x002056DB File Offset: 0x002038DB
	public override string GetMoneyDisplayOwnedText()
	{
		return GameStrings.Get("GLUE_STORE_DUNGEON_BUTTON_COST_OWNED_TEXT");
	}

	// Token: 0x06006343 RID: 25411 RVA: 0x002056E7 File Offset: 0x002038E7
	private GameObject GetCurrentDisplayContainer()
	{
		return this.GetCurrentDisplay().gameObject;
	}

	// Token: 0x06006344 RID: 25412 RVA: 0x002056F4 File Offset: 0x002038F4
	private GameObject GetNextDisplayContainer()
	{
		if ((this.m_currentDisplay + 1) % 2 != 0)
		{
			return this.m_adventureDisplay2.gameObject;
		}
		return this.m_adventureDisplay1.gameObject;
	}

	// Token: 0x06006345 RID: 25413 RVA: 0x00205719 File Offset: 0x00203919
	private GeneralStoreAdventureContentDisplay GetCurrentDisplay()
	{
		if (this.m_currentDisplay != 0)
		{
			return this.m_adventureDisplay2;
		}
		return this.m_adventureDisplay1;
	}

	// Token: 0x06006346 RID: 25414 RVA: 0x00205730 File Offset: 0x00203930
	private void OnAdventuresShowPreviewCard(UIEvent e)
	{
		StoreAdventureDef storeAdventureDef = this.GetStoreAdventureDef((int)this.m_selectedAdventureId);
		if (storeAdventureDef == null)
		{
			Debug.LogError(string.Format("Unable to find preview cards for {0} adventure.", this.m_selectedAdventureId));
			return;
		}
		string[] previewCards = storeAdventureDef.m_previewCards.ToArray();
		if (previewCards.Length == 0)
		{
			Debug.LogError(string.Format("No preview cards defined for {0} adventure.", this.m_selectedAdventureId));
			return;
		}
		this.m_showPreviewCards = true;
		SoundManager.Get().LoadAndPlay("collection_manager_card_mouse_over.prefab:0d4e20bc78956bc48b5e2963ec39211c");
		foreach (KeyValuePair<string, Actor> keyValuePair in this.m_loadedPreviewCards)
		{
			keyValuePair.Value.gameObject.SetActive(false);
		}
		int loadedPreviewCards = 0;
		int num = 0;
		string[] previewCards2 = previewCards;
		for (int i = 0; i < previewCards2.Length; i++)
		{
			string previewCard = previewCards2[i];
			int cardIndex = num;
			this.LoadAdventurePreviewCard(previewCard, delegate(Actor cardActor)
			{
				cardActor.transform.position = this.m_adventureCardPreviewBones[cardIndex].transform.position;
				cardActor.transform.rotation = this.m_adventureCardPreviewBones[cardIndex].transform.rotation;
				cardActor.transform.parent = this.m_adventureCardPreviewBones[cardIndex].transform;
				cardActor.transform.localScale = Vector3.one;
				int loadedPreviewCards;
				loadedPreviewCards++;
				loadedPreviewCards = loadedPreviewCards;
				cardActor.gameObject.SetActive(this.m_showPreviewCards);
				if (this.m_showPreviewCards && loadedPreviewCards == previewCards.Length)
				{
					this.ShowPreviewCardPanel();
				}
			});
			num++;
		}
	}

	// Token: 0x06006347 RID: 25415 RVA: 0x00205878 File Offset: 0x00203A78
	private void LoadAdventurePreviewCard(string previewCard, GeneralStoreAdventureContent.DelOnAdventurePreviewCardLoaded onLoadComplete)
	{
		Actor previewCard2;
		if (this.m_loadedPreviewCards.TryGetValue(previewCard, out previewCard2))
		{
			onLoadComplete(previewCard2);
			return;
		}
		DefLoader.Get().LoadFullDef(previewCard, delegate(string cardID, DefLoader.DisposableFullDef fullDef, object data)
		{
			AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(fullDef.EntityDef), delegate(AssetReference actorName, GameObject actorObject, object data2)
			{
				using (DefLoader.DisposableFullDef fullDef = fullDef)
				{
					if (actorObject == null)
					{
						Debug.LogWarning(string.Format("FAILED to load actor \"{0}\"", actorName));
						onLoadComplete(null);
					}
					else
					{
						Actor component = actorObject.GetComponent<Actor>();
						if (component == null)
						{
							Debug.LogWarning(string.Format("ERROR actor \"{0}\" has no Actor component", actorName));
							onLoadComplete(null);
						}
						else
						{
							component.SetFullDef(fullDef);
							component.UpdateAllComponents();
							SceneUtils.SetLayer(component.gameObject, this.gameObject.layer, null);
							component.Show();
							this.m_loadedPreviewCards.Add(previewCard, component);
							onLoadComplete(component);
						}
					}
				}
			}, null, AssetLoadingOptions.IgnorePrefabPosition);
		}, null, null);
	}

	// Token: 0x06006348 RID: 25416 RVA: 0x002058E0 File Offset: 0x00203AE0
	private void OnAdventuresHidePreviewCard(UIEvent e)
	{
		this.m_showPreviewCards = false;
		SoundManager.Get().LoadAndPlay("card_shrink.prefab:a4e6170a9f153f94cacee42db7c327fb");
		this.HidePreviewCardPanel();
	}

	// Token: 0x06006349 RID: 25417 RVA: 0x00205904 File Offset: 0x00203B04
	private void ShowPreviewCardPanel()
	{
		this.m_adventureCardPreviewPanel.SetActive(true);
		this.m_adventureCardPreviewPanel.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.StopByName(this.m_adventureCardPreviewPanel, "PreviewCardPanelScale");
		iTween.ScaleTo(this.m_adventureCardPreviewPanel, iTween.Hash(new object[]
		{
			"scale",
			Vector3.one,
			"time",
			0.1f,
			"name",
			"PreviewCardPanelScale",
			"easetype",
			iTween.EaseType.linear
		}));
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_parentStore.BlockInterface(true);
		}
	}

	// Token: 0x0600634A RID: 25418 RVA: 0x002059CC File Offset: 0x00203BCC
	private void HidePreviewCardPanel()
	{
		iTween.StopByName(this.m_adventureCardPreviewPanel, "PreviewCardPanelScale");
		iTween.ScaleTo(this.m_adventureCardPreviewPanel, iTween.Hash(new object[]
		{
			"scale",
			new Vector3(0.02f, 0.02f, 0.02f),
			"time",
			0.1f,
			"name",
			"PreviewCardPanelScale",
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				this.m_adventureCardPreviewPanel.SetActive(false);
				if (UniversalInputManager.UsePhoneUI)
				{
					this.m_parentStore.BlockInterface(false);
				}
			}),
			"easetype",
			iTween.EaseType.linear
		}));
	}

	// Token: 0x0600634B RID: 25419 RVA: 0x00205A74 File Offset: 0x00203C74
	private void UpdateRadioButtonText(Network.Bundle moneyBundle)
	{
		this.m_adventureRadioButton.SetSelected(true);
		if (moneyBundle == null)
		{
			this.m_adventureRadioButtonText.Text = GameStrings.Get("GLUE_STORE_DUNGEON_BUTTON_TEXT_PURCHASED");
			this.m_adventureRadioButtonText.Anchor = UberText.AnchorOptions.Middle;
			this.m_adventureRadioButtonCostText.Text = string.Empty;
		}
		else
		{
			string key;
			if (this.IsPreOrder())
			{
				AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)this.m_selectedAdventureId);
				if (record != null && !string.IsNullOrEmpty(record.StorePreorderRadioText))
				{
					key = record.StorePreorderRadioText;
				}
				else
				{
					key = "GLUE_STORE_DUNGEON_BUTTON_PREORDER_TEXT";
				}
			}
			else
			{
				key = "GLUE_STORE_DUNGEON_BUTTON_TEXT";
			}
			this.m_adventureRadioButtonText.Text = GameStrings.Get(key);
			this.m_adventureRadioButtonText.Anchor = UberText.AnchorOptions.Upper;
			string text = StoreManager.Get().FormatCostBundle(moneyBundle);
			int wingItemCount = StoreManager.Get().GetWingItemCount(moneyBundle.Items);
			this.m_adventureRadioButtonCostText.Text = GameStrings.Format("GLUE_STORE_DUNGEON_BUTTON_COST_TEXT", new object[]
			{
				wingItemCount,
				text
			});
		}
		if (this.m_adventureOwnedCheckmark != null)
		{
			this.m_adventureOwnedCheckmark.SetActive(moneyBundle == null);
		}
	}

	// Token: 0x0600634C RID: 25420 RVA: 0x00205B90 File Offset: 0x00203D90
	private void UpdateAdventureDescription(Network.Bundle bundle)
	{
		if (this.m_selectedAdventureId != AdventureDbId.INVALID)
		{
			string title = string.Empty;
			string desc = string.Empty;
			string warning = string.Empty;
			AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)this.m_selectedAdventureId);
			if (record == null)
			{
				Debug.LogError(string.Format("Unable to find adventure record ID: {0}", this.m_selectedAdventureId));
			}
			else if (bundle == null)
			{
				title = record.StoreOwnedHeadline;
				desc = record.StoreOwnedDesc;
			}
			else if (this.IsPreOrder())
			{
				title = record.StorePreorderHeadline;
				int wingItemCount = StoreManager.Get().GetWingItemCount(bundle.Items);
				DbfLocValue dbfLocValue = record.GetVar(string.Format("STORE_PREORDER_WINGS_{0}_DESC", wingItemCount)) as DbfLocValue;
				desc = ((dbfLocValue == null) ? "" : dbfLocValue.GetString(true));
			}
			else
			{
				int wingItemCount2 = StoreManager.Get().GetWingItemCount(bundle.Items);
				DbfLocValue dbfLocValue2 = record.GetVar(string.Format("STORE_BUY_WINGS_{0}_HEADLINE", wingItemCount2)) as DbfLocValue;
				DbfLocValue dbfLocValue3 = record.GetVar(string.Format("STORE_BUY_WINGS_{0}_DESC", wingItemCount2)) as DbfLocValue;
				title = ((dbfLocValue2 == null) ? "" : dbfLocValue2.GetString(true));
				desc = ((dbfLocValue3 == null) ? "" : dbfLocValue3.GetString(true));
			}
			if (StoreManager.Get().IsKoreanCustomer())
			{
				warning = GameStrings.Get("GLUE_STORE_KOREAN_PRODUCT_DETAILS_ADVENTURE");
			}
			if (this.m_adventureCardPreviewText != null)
			{
				this.m_adventureCardPreviewText.Text = record.StorePreviewRewardsText;
			}
			this.m_parentStore.SetDescription(title, desc, warning);
			StoreAdventureDef storeAdventureDef = this.GetStoreAdventureDef((int)this.m_selectedAdventureId);
			if (!(storeAdventureDef != null))
			{
				return;
			}
			using (AssetHandle<Texture> assetHandle = AssetLoader.Get().LoadAsset<Texture>(storeAdventureDef.m_accentTextureName, AssetLoadingOptions.None))
			{
				this.m_parentStore.SetAccentTexture(assetHandle);
				return;
			}
		}
		this.m_parentStore.HideAccentTexture();
		this.m_parentStore.SetChooseDescription(GameStrings.Get("GLUE_STORE_CHOOSE_ADVENTURE"));
	}

	// Token: 0x0600634D RID: 25421 RVA: 0x00205DA4 File Offset: 0x00203FA4
	private void UpdateAdventureTypeMusic()
	{
		if (this.m_parentStore.GetMode() == GeneralStoreMode.NONE)
		{
			return;
		}
		StoreAdventureDef storeAdventureDef = this.GetStoreAdventureDef((int)this.m_selectedAdventureId);
		if (storeAdventureDef == null || storeAdventureDef.m_playlist == MusicPlaylistType.Invalid || !MusicManager.Get().StartPlaylist(storeAdventureDef.m_playlist))
		{
			this.m_parentStore.ResumePreviousMusicPlaylist();
		}
	}

	// Token: 0x0600634E RID: 25422 RVA: 0x00205DFC File Offset: 0x00203FFC
	private void AnimateAndUpdateDisplay(int id, bool forceImmediate)
	{
		if (this.m_preorderCardBackReward != null)
		{
			this.m_preorderCardBackReward.HideCardBackReward();
		}
		GameObject currDisplay = null;
		if (this.m_currentDisplay == -1)
		{
			this.m_currentDisplay = 1;
			currDisplay = this.m_adventureEmptyDisplay;
		}
		else
		{
			currDisplay = this.GetCurrentDisplayContainer();
		}
		GameObject nextDisplayContainer = this.GetNextDisplayContainer();
		this.m_currentDisplay = (this.m_currentDisplay + 1) % 2;
		nextDisplayContainer.SetActive(true);
		if (!forceImmediate)
		{
			currDisplay.transform.localRotation = Quaternion.identity;
			nextDisplayContainer.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
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
		}
		else
		{
			nextDisplayContainer.transform.localRotation = Quaternion.identity;
			currDisplay.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
			currDisplay.SetActive(false);
		}
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord(id);
		bool flag = this.IsPreOrder();
		StoreAdventureDef storeAdventureDef = this.GetStoreAdventureDef(id);
		GeneralStoreAdventureContentDisplay currentDisplay = this.GetCurrentDisplay();
		currentDisplay.UpdateAdventureType(storeAdventureDef, record);
		currentDisplay.SetPreOrder(flag);
		if (this.m_preorderCardBackReward != null && flag)
		{
			this.m_preorderCardBackReward.SetCardBack(storeAdventureDef.m_preorderCardBackId);
			this.m_preorderCardBackReward.SetPreorderText(storeAdventureDef.m_preorderCardBackTextName);
			this.m_preorderCardBackReward.ShowCardBackReward();
		}
	}

	// Token: 0x0600634F RID: 25423 RVA: 0x00206074 File Offset: 0x00204274
	private void AnimateAdventureRadioButtonBar()
	{
		if (this.m_adventureRadioButtonContainer == null)
		{
			return;
		}
		this.m_adventureRadioButtonContainer.SetActive(false);
		if (this.m_selectedAdventureId == AdventureDbId.INVALID)
		{
			return;
		}
		iTween.Stop(this.m_adventureRadioButtonContainer);
		this.m_adventureRadioButtonContainer.transform.localRotation = Quaternion.identity;
		this.m_adventureRadioButtonContainer.SetActive(true);
		this.m_adventureRadioButton.SetSelected(true);
		iTween.RotateBy(this.m_adventureRadioButtonContainer, iTween.Hash(new object[]
		{
			"amount",
			new Vector3(-1f, 0f, 0f),
			"time",
			this.m_backgroundFlipAnimTime,
			"delay",
			0.001f
		}));
	}

	// Token: 0x06006350 RID: 25424 RVA: 0x00206144 File Offset: 0x00204344
	private void OnAdventureProgressUpdated(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress, object userData)
	{
		if (newProgress == null)
		{
			return;
		}
		if ((oldProgress != null && oldProgress.IsOwned()) || !newProgress.IsOwned())
		{
			return;
		}
		WingDbfRecord record = GameDbf.Wing.GetRecord(newProgress.Wing);
		if (record == null)
		{
			return;
		}
		if (record.AdventureId != (int)this.m_selectedAdventureId)
		{
			return;
		}
		Network.Bundle bundle = null;
		StoreManager.Get().GetAvailableAdventureBundle(this.m_selectedAdventureId, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION, out bundle);
		base.SetCurrentMoneyBundle(bundle, false);
		if (this.m_parentStore != null)
		{
			this.m_parentStore.RefreshContent();
		}
	}

	// Token: 0x06006351 RID: 25425 RVA: 0x002061C8 File Offset: 0x002043C8
	private bool IsPreOrder()
	{
		Network.Bundle currentMoneyBundle = base.GetCurrentMoneyBundle();
		return currentMoneyBundle != null && StoreManager.Get().IsProductPrePurchase(currentMoneyBundle);
	}

	// Token: 0x0400524E RID: 21070
	[CustomEditField(Sections = "General Store")]
	public GeneralStoreAdventureContentDisplay m_adventureDisplay;

	// Token: 0x0400524F RID: 21071
	[CustomEditField(Sections = "Animation/Preorder")]
	public GeneralStoreRewardsCardBack m_preorderCardBackReward;

	// Token: 0x04005250 RID: 21072
	[CustomEditField(Sections = "General Store")]
	public GameObject m_adventureEmptyDisplay;

	// Token: 0x04005251 RID: 21073
	[CustomEditField(Sections = "Rewards")]
	public GameObject m_adventureCardPreviewPanel;

	// Token: 0x04005252 RID: 21074
	[CustomEditField(Sections = "Rewards")]
	public UberText m_adventureCardPreviewText;

	// Token: 0x04005253 RID: 21075
	[CustomEditField(Sections = "Rewards")]
	public List<GameObject> m_adventureCardPreviewBones;

	// Token: 0x04005254 RID: 21076
	[CustomEditField(Sections = "Rewards")]
	public PegUIElement m_adventureCardPreviewOffClicker;

	// Token: 0x04005255 RID: 21077
	[CustomEditField(Sections = "General Store/Buttons")]
	public GameObject m_adventureRadioButtonContainer;

	// Token: 0x04005256 RID: 21078
	[CustomEditField(Sections = "General Store/Buttons")]
	public UberText m_adventureRadioButtonText;

	// Token: 0x04005257 RID: 21079
	[CustomEditField(Sections = "General Store/Buttons")]
	public UberText m_adventureRadioButtonCostText;

	// Token: 0x04005258 RID: 21080
	[CustomEditField(Sections = "General Store/Buttons")]
	public RadioButton m_adventureRadioButton;

	// Token: 0x04005259 RID: 21081
	[CustomEditField(Sections = "General Store/Buttons")]
	public GameObject m_adventureOwnedCheckmark;

	// Token: 0x0400525A RID: 21082
	[CustomEditField(Sections = "Sounds & Music", T = EditType.SOUND_PREFAB)]
	public string m_backgroundFlipSound;

	// Token: 0x0400525B RID: 21083
	[CustomEditField(Sections = "Animation")]
	public float m_backgroundFlipAnimTime = 0.5f;

	// Token: 0x0400525C RID: 21084
	[CustomEditField(Sections = "Animation")]
	public float m_adventureLogoFadeInTime = 0.5f;

	// Token: 0x0400525D RID: 21085
	private bool m_showPreviewCards;

	// Token: 0x0400525E RID: 21086
	private Map<string, Actor> m_loadedPreviewCards = new Map<string, Actor>();

	// Token: 0x0400525F RID: 21087
	private AdventureDbId m_selectedAdventureId;

	// Token: 0x04005260 RID: 21088
	private Map<int, StoreAdventureDef> m_storeAdvDefs = new Map<int, StoreAdventureDef>();

	// Token: 0x04005261 RID: 21089
	private int m_currentDisplay = -1;

	// Token: 0x04005262 RID: 21090
	private GeneralStoreAdventureContentDisplay m_adventureDisplay1;

	// Token: 0x04005263 RID: 21091
	private GeneralStoreAdventureContentDisplay m_adventureDisplay2;

	// Token: 0x04005264 RID: 21092
	public static readonly bool REQUIRE_REAL_MONEY_BUNDLE_OPTION = true;

	// Token: 0x02002266 RID: 8806
	// (Invoke) Token: 0x0601271D RID: 75549
	public delegate void DelOnAdventurePreviewCardLoaded(Actor previewCard);
}
