using System;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using PegasusUtil;
using UnityEngine;

// Token: 0x020006FB RID: 1787
[CustomEditClass]
public class GeneralStoreHeroesPane : GeneralStorePane
{
	// Token: 0x170005EA RID: 1514
	// (get) Token: 0x060063B6 RID: 25526 RVA: 0x00207AFA File Offset: 0x00205CFA
	// (set) Token: 0x060063B7 RID: 25527 RVA: 0x00207B02 File Offset: 0x00205D02
	[CustomEditField(Sections = "Layout")]
	public Vector3 UnpurchasedHeroButtonSpacing
	{
		get
		{
			return this.m_unpurchasedHeroButtonSpacing;
		}
		set
		{
			this.m_unpurchasedHeroButtonSpacing = value;
			this.PositionAllHeroButtons();
		}
	}

	// Token: 0x170005EB RID: 1515
	// (get) Token: 0x060063B8 RID: 25528 RVA: 0x00207B11 File Offset: 0x00205D11
	// (set) Token: 0x060063B9 RID: 25529 RVA: 0x00207B19 File Offset: 0x00205D19
	[CustomEditField(Sections = "Layout")]
	public Vector3 PurchasedHeroButtonSpacing
	{
		get
		{
			return this.m_purchasedHeroButtonSpacing;
		}
		set
		{
			this.m_purchasedHeroButtonSpacing = value;
			this.PositionAllHeroButtons();
		}
	}

	// Token: 0x060063BA RID: 25530 RVA: 0x00207B28 File Offset: 0x00205D28
	private void Awake()
	{
		this.m_heroesContent = (this.m_parentContent as GeneralStoreHeroesContent);
		this.PopulateHeroes();
		this.m_purchaseAnimationBlocker.SetActive(false);
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnItemPurchased));
		CheatMgr.Get().RegisterCheatHandler("herobuy", new CheatMgr.ProcessCheatCallback(this.OnHeroPurchased_cheat), null, null, null);
	}

	// Token: 0x060063BB RID: 25531 RVA: 0x00207B8C File Offset: 0x00205D8C
	private void OnDestroy()
	{
		CheatMgr cheatMgr;
		if (HearthstoneServices.TryGet<CheatMgr>(out cheatMgr))
		{
			cheatMgr.UnregisterCheatHandler("herobuy", new CheatMgr.ProcessCheatCallback(this.OnHeroPurchased_cheat));
		}
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnItemPurchased));
	}

	// Token: 0x060063BC RID: 25532 RVA: 0x00207BCF File Offset: 0x00205DCF
	public override void PrePaneSwappedIn()
	{
		this.SetupInitialSelectedHero();
	}

	// Token: 0x060063BD RID: 25533 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void RefreshHeroAvailability()
	{
	}

	// Token: 0x060063BE RID: 25534 RVA: 0x00207BD8 File Offset: 0x00205DD8
	private void PopulateHeroes()
	{
		SpecialEventManager specialEventManager = SpecialEventManager.Get();
		foreach (CardHeroDbfRecord cardHeroDbfRecord in GameDbf.CardHero.GetRecords())
		{
			Network.Bundle bundle = null;
			if (StoreManager.Get().GetHeroBundleByCardDbId(cardHeroDbfRecord.CardId, out bundle) && specialEventManager.IsEventActive(bundle.ProductEvent, false))
			{
				this.CreateNewHeroButton(cardHeroDbfRecord, bundle).SetSortOrder(cardHeroDbfRecord.StoreSortOrder);
			}
		}
		this.PositionAllHeroButtons();
	}

	// Token: 0x060063BF RID: 25535 RVA: 0x00207C6C File Offset: 0x00205E6C
	private GeneralStoreHeroesSelectorButton CreateNewHeroButton(CardHeroDbfRecord cardHero, Network.Bundle heroBundle)
	{
		if (StoreManager.Get().IsProductAlreadyOwned(heroBundle))
		{
			return this.CreatePurchasedHeroButton(cardHero, heroBundle);
		}
		return this.CreateUnpurchasedHeroButton(cardHero, heroBundle);
	}

	// Token: 0x060063C0 RID: 25536 RVA: 0x00207C8C File Offset: 0x00205E8C
	private GeneralStoreHeroesSelectorButton CreateUnpurchasedHeroButton(CardHeroDbfRecord cardHero, Network.Bundle heroBundle)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(this.m_heroUnpurchasedFrame, AssetLoadingOptions.None);
		GeneralStoreHeroesSelectorButton component = gameObject.GetComponent<GeneralStoreHeroesSelectorButton>();
		if (component == null)
		{
			Debug.LogError("Prefab does not contain GeneralStoreHeroesSelectorButton component.");
			UnityEngine.Object.Destroy(gameObject);
			return null;
		}
		GameUtils.SetParent(component, this.m_paneContainer, true);
		SceneUtils.SetLayer(component, this.m_paneContainer.layer);
		this.m_unpurchasedHeroesButtons.Add(component);
		this.SetupHeroButton(cardHero, component);
		return component;
	}

	// Token: 0x060063C1 RID: 25537 RVA: 0x00207D08 File Offset: 0x00205F08
	public GeneralStoreHeroesSelectorButton CreatePurchasedHeroButton(CardHeroDbfRecord cardHero, Network.Bundle heroBundle)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(this.m_heroPurchasedFrame, AssetLoadingOptions.None);
		GeneralStoreHeroesSelectorButton component = gameObject.GetComponent<GeneralStoreHeroesSelectorButton>();
		if (component == null)
		{
			Debug.LogError("Prefab does not contain GeneralStoreHeroesSelectorButton component.");
			UnityEngine.Object.Destroy(gameObject);
			return null;
		}
		GameUtils.SetParent(component, this.m_purchasedButtonContainer, true);
		SceneUtils.SetLayer(component, this.m_purchasedButtonContainer.layer);
		this.m_purchasedHeroesButtons.Add(component);
		this.SetupHeroButton(cardHero, component);
		return component;
	}

	// Token: 0x060063C2 RID: 25538 RVA: 0x00207D84 File Offset: 0x00205F84
	private void SetupHeroButton(CardHeroDbfRecord cardHero, GeneralStoreHeroesSelectorButton heroButton)
	{
		string cardId2 = GameUtils.TranslateDbIdToCardId(cardHero.CardId, false);
		heroButton.SetCardHeroDbfRecord(cardHero);
		heroButton.SetPurchased(false);
		heroButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.SelectHero(heroButton);
		});
		DefLoader.Get().LoadFullDef(cardId2, delegate(string cardId, DefLoader.DisposableFullDef fullDef, object data)
		{
			try
			{
				heroButton.UpdatePortrait(fullDef);
				heroButton.UpdateName(fullDef.EntityDef.GetName());
			}
			finally
			{
				if (fullDef != null)
				{
					((IDisposable)fullDef).Dispose();
				}
			}
		}, null, null);
	}

	// Token: 0x060063C3 RID: 25539 RVA: 0x00207DFC File Offset: 0x00205FFC
	private void UpdatePurchasedSectionLayout()
	{
		if (this.m_purchasedHeroesButtons.Count == 0)
		{
			this.m_purchasedButtonContainer.SetActive(false);
			this.m_purchasedSection.gameObject.SetActive(false);
			return;
		}
		this.m_purchasedButtonContainer.SetActive(true);
		this.m_purchasedSection.gameObject.SetActive(true);
		if (this.m_purchasedSectionMidMeshes.Count < this.m_purchasedHeroesButtons.Count)
		{
			int num = this.m_purchasedHeroesButtons.Count - this.m_purchasedSectionMidMeshes.Count;
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = (GameObject)GameUtils.Instantiate(this.m_purchasedSectionMidTemplate, this.m_purchasedSection.gameObject, true);
				gameObject.SetActive(true);
				this.m_purchasedSectionMidMeshes.Add(gameObject);
			}
		}
		this.m_purchasedSection.ClearSlices();
		this.m_purchasedSection.AddSlice(this.m_purchasedSectionTop);
		foreach (GameObject obj in this.m_purchasedSectionMidMeshes)
		{
			this.m_purchasedSection.AddSlice(obj);
		}
		this.m_purchasedSection.AddSlice(this.m_purchasedSectionBottom);
		this.m_purchasedSection.UpdateSlices();
	}

	// Token: 0x060063C4 RID: 25540 RVA: 0x00207F44 File Offset: 0x00206144
	private void SelectHero(GeneralStoreHeroesSelectorButton button)
	{
		foreach (GeneralStoreHeroesSelectorButton generalStoreHeroesSelectorButton in this.m_unpurchasedHeroesButtons)
		{
			generalStoreHeroesSelectorButton.Unselect();
		}
		foreach (GeneralStoreHeroesSelectorButton generalStoreHeroesSelectorButton2 in this.m_purchasedHeroesButtons)
		{
			generalStoreHeroesSelectorButton2.Unselect();
		}
		button.Select();
		Options.Get().SetInt(Option.LAST_SELECTED_STORE_HERO_ID, button.GetHeroDbId());
		this.m_heroesContent.SelectHero(button.GetCardHeroDbfRecord(), true);
		if (!string.IsNullOrEmpty(this.m_heroSelectionSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_heroSelectionSound);
		}
	}

	// Token: 0x060063C5 RID: 25541 RVA: 0x00208024 File Offset: 0x00206224
	private void SetupInitialSelectedHero()
	{
		if (this.m_initializeFirstHero)
		{
			return;
		}
		this.m_initializeFirstHero = true;
		int @int = Options.Get().GetInt(Option.LAST_SELECTED_STORE_HERO_ID, -1);
		if (@int == -1)
		{
			return;
		}
		List<GeneralStoreHeroesSelectorButton> list = new List<GeneralStoreHeroesSelectorButton>();
		list.AddRange(this.m_unpurchasedHeroesButtons);
		list.AddRange(this.m_purchasedHeroesButtons);
		foreach (GeneralStoreHeroesSelectorButton generalStoreHeroesSelectorButton in list)
		{
			if (generalStoreHeroesSelectorButton.GetHeroDbId() == @int)
			{
				this.m_heroesContent.SelectHero(generalStoreHeroesSelectorButton.GetCardHeroDbfRecord(), false);
				generalStoreHeroesSelectorButton.Select();
				break;
			}
		}
	}

	// Token: 0x060063C6 RID: 25542 RVA: 0x002080D0 File Offset: 0x002062D0
	private void PositionAllHeroButtons()
	{
		this.PositionUnpurchasedHeroButtons();
		this.PositionPurchasedHeroButtons(true);
	}

	// Token: 0x060063C7 RID: 25543 RVA: 0x002080E0 File Offset: 0x002062E0
	private void PositionUnpurchasedHeroButtons()
	{
		this.m_unpurchasedHeroesButtons.Sort(delegate(GeneralStoreHeroesSelectorButton lhs, GeneralStoreHeroesSelectorButton rhs)
		{
			int sortOrder = lhs.GetSortOrder();
			int sortOrder2 = rhs.GetSortOrder();
			if (sortOrder < sortOrder2)
			{
				return -1;
			}
			if (sortOrder > sortOrder2)
			{
				return 1;
			}
			return 0;
		});
		for (int i = 0; i < this.m_unpurchasedHeroesButtons.Count; i++)
		{
			this.m_unpurchasedHeroesButtons[i].transform.localPosition = this.m_unpurchasedHeroButtonSpacing * (float)i;
		}
	}

	// Token: 0x060063C8 RID: 25544 RVA: 0x00208150 File Offset: 0x00206350
	private void PositionPurchasedHeroButtons(bool sortAndSetSectionPos = true)
	{
		if (sortAndSetSectionPos)
		{
			this.m_purchasedHeroesButtons.Sort(delegate(GeneralStoreHeroesSelectorButton lhs, GeneralStoreHeroesSelectorButton rhs)
			{
				int sortOrder = lhs.GetSortOrder();
				int sortOrder2 = rhs.GetSortOrder();
				if (sortOrder < sortOrder2)
				{
					return -1;
				}
				if (sortOrder > sortOrder2)
				{
					return 1;
				}
				return 0;
			});
			this.m_purchasedSection.transform.localPosition = this.m_unpurchasedHeroButtonSpacing * (float)(this.m_unpurchasedHeroesButtons.Count - 1) + this.m_purchasedSectionOffset;
		}
		for (int i = 0; i < this.m_purchasedHeroesButtons.Count; i++)
		{
			this.m_purchasedHeroesButtons[i].transform.localPosition = this.m_purchasedHeroButtonSpacing * (float)i;
		}
		this.UpdatePurchasedSectionLayout();
	}

	// Token: 0x060063C9 RID: 25545 RVA: 0x002081FD File Offset: 0x002063FD
	private IEnumerator AnimateShowPurchase(int btnIndex)
	{
		this.m_purchaseAnimationBlocker.SetActive(true);
		this.m_scrollUpdate.Pause(true);
		if (GeneralStore.Get().GetMode() != GeneralStoreMode.HEROES)
		{
			GeneralStore.Get().SetMode(GeneralStoreMode.HEROES);
			yield return new WaitForSeconds(1f);
		}
		GeneralStoreHeroesSelectorButton removeBtn = this.m_unpurchasedHeroesButtons[btnIndex];
		float percentage = (float)btnIndex / (float)(this.m_unpurchasedHeroesButtons.Count + this.m_purchasedHeroesButtons.Count - 1);
		this.m_scrollUpdate.SetScroll(percentage, iTween.EaseType.easeInOutCirc, 0.2f, false, true);
		yield return new WaitForSeconds(0.21f);
		GameObject animateBtnObj = AssetLoader.Get().InstantiatePrefab(this.m_heroAnimationFrame, AssetLoadingOptions.None);
		GeneralStoreHeroesSelectorButton component = animateBtnObj.GetComponent<GeneralStoreHeroesSelectorButton>();
		SceneUtils.SetLayer(component, GameLayer.PerspectiveUI);
		component.transform.position = removeBtn.transform.position;
		component.UpdatePortrait(removeBtn);
		component.UpdateName(removeBtn);
		removeBtn.gameObject.SetActive(false);
		PlayMakerFSM animation = component.GetComponent<PlayMakerFSM>();
		FsmVector3 fsmVector = animation.FsmVariables.FindFsmVector3("PopStartPos");
		FsmVector3 fsmVector2 = animation.FsmVariables.FindFsmVector3("PopMidPos");
		FsmVector3 fsmVector3 = animation.FsmVariables.FindFsmVector3("PopEndPos");
		fsmVector.Value = removeBtn.transform.position;
		fsmVector2.Value = removeBtn.transform.position + this.m_purchaseAnimationMidPointWorldOffset;
		fsmVector3.Value = this.m_purchaseAnimationEndBone.transform.position;
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		if (camera != null)
		{
			animation.FsmVariables.FindFsmGameObject("CameraObjectShake").Value = camera.gameObject;
		}
		animation.FsmVariables.FindFsmString("PopOutAnimationName").Value = this.m_purchaseAnimationName;
		animation.SendEvent("PopOut");
		yield return new WaitForSeconds(0.5f);
		this.m_heroesContent.PlayCurrentHeroPurchaseEmote();
		yield return null;
		FsmBool animComplete = animation.FsmVariables.FindFsmBool("AnimationComplete");
		while (!animComplete.Value)
		{
			yield return null;
		}
		this.CreatePurchasedHeroButton(removeBtn.GetCardHeroDbfRecord(), null).Select();
		this.m_unpurchasedHeroesButtons.Remove(removeBtn);
		this.PositionPurchasedHeroButtons(false);
		yield return new WaitForSeconds(0.25f);
		while (!UniversalInputManager.Get().GetMouseButtonDown(0))
		{
			yield return null;
		}
		animation.SendEvent("EchoHero");
		yield return null;
		animComplete = animation.FsmVariables.FindFsmBool("AnimationComplete");
		while (!animComplete.Value)
		{
			yield return null;
		}
		for (int i = this.m_currentPurchaseRemovalIdx; i < this.m_unpurchasedHeroesButtons.Count; i++)
		{
			iTween.MoveTo(this.m_unpurchasedHeroesButtons[i].gameObject, iTween.Hash(new object[]
			{
				"position",
				this.m_unpurchasedHeroButtonSpacing * (float)i,
				"islocal",
				true,
				"easetype",
				iTween.EaseType.easeInOutCirc,
				"time",
				0.25f
			}));
		}
		iTween.MoveTo(this.m_purchasedSection.gameObject, iTween.Hash(new object[]
		{
			"position",
			this.m_unpurchasedHeroButtonSpacing * (float)(this.m_unpurchasedHeroesButtons.Count - 1) + this.m_purchasedSectionOffset,
			"islocal",
			true,
			"easetype",
			iTween.EaseType.easeInOutCirc,
			"time",
			0.25f
		}));
		if (!string.IsNullOrEmpty(this.m_buttonsSlideUpSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_buttonsSlideUpSound);
		}
		yield return new WaitForSeconds(0.25f);
		UnityEngine.Object.Destroy(removeBtn.gameObject);
		UnityEngine.Object.Destroy(animateBtnObj);
		animateBtnObj = null;
		this.m_scrollUpdate.Pause(false);
		this.m_purchaseAnimationBlocker.SetActive(false);
		yield break;
	}

	// Token: 0x060063CA RID: 25546 RVA: 0x00208214 File Offset: 0x00206414
	private void OnItemPurchased(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		if (bundle == null || bundle.Items == null)
		{
			return;
		}
		int num = 0;
		foreach (Network.BundleItem bundleItem in bundle.Items)
		{
			if (bundleItem.ItemType == ProductType.PRODUCT_TYPE_HIDDEN_LICENSE)
			{
				return;
			}
			if (bundleItem != null && bundleItem.ItemType == ProductType.PRODUCT_TYPE_HERO)
			{
				num = bundleItem.ProductData;
			}
		}
		if (num != 0)
		{
			this.OnHeroPurchased(num);
		}
	}

	// Token: 0x060063CB RID: 25547 RVA: 0x00208298 File Offset: 0x00206498
	private void OnHeroPurchased(int heroCardDbId)
	{
		int num = this.m_unpurchasedHeroesButtons.FindIndex((GeneralStoreHeroesSelectorButton e) => e.GetHeroCardDbId() == heroCardDbId);
		if (num == -1)
		{
			Debug.LogError(string.Format("Hero Card DB ID {0} does not exist in button list.", heroCardDbId));
			return;
		}
		this.RunHeroPurchaseAnimation(num);
	}

	// Token: 0x060063CC RID: 25548 RVA: 0x002082F0 File Offset: 0x002064F0
	private void RunHeroPurchaseAnimation(int btnIndex)
	{
		this.m_currentPurchaseRemovalIdx = btnIndex;
		base.StartCoroutine(this.AnimateShowPurchase(btnIndex));
	}

	// Token: 0x060063CD RID: 25549 RVA: 0x00208308 File Offset: 0x00206508
	private bool OnHeroPurchased_cheat(string func, string[] args, string rawArgs)
	{
		if (args.Length == 0)
		{
			return true;
		}
		int num = -1;
		if (int.TryParse(args[0], out num) && num >= 0 && num < this.m_unpurchasedHeroesButtons.Count)
		{
			this.RunHeroPurchaseAnimation(num);
		}
		return true;
	}

	// Token: 0x040052B7 RID: 21175
	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_heroUnpurchasedFrame;

	// Token: 0x040052B8 RID: 21176
	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_heroPurchasedFrame;

	// Token: 0x040052B9 RID: 21177
	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_heroAnimationFrame;

	// Token: 0x040052BA RID: 21178
	[SerializeField]
	private Vector3 m_unpurchasedHeroButtonSpacing = new Vector3(0f, 0f, 0.285f);

	// Token: 0x040052BB RID: 21179
	[SerializeField]
	private Vector3 m_purchasedHeroButtonSpacing = new Vector3(0f, 0f, 0.092f);

	// Token: 0x040052BC RID: 21180
	[CustomEditField(Sections = "Layout")]
	public float m_unpurchasedHeroButtonHeight = 0.0275f;

	// Token: 0x040052BD RID: 21181
	[CustomEditField(Sections = "Layout")]
	public float m_purchasedHeroButtonHeight;

	// Token: 0x040052BE RID: 21182
	[CustomEditField(Sections = "Layout")]
	public float m_purchasedHeroButtonHeightPadding = 0.01f;

	// Token: 0x040052BF RID: 21183
	[CustomEditField(Sections = "Layout")]
	public float m_maxPurchasedHeightAdd;

	// Token: 0x040052C0 RID: 21184
	[CustomEditField(Sections = "Layout/Purchased Section")]
	public GameObject m_purchasedSectionTop;

	// Token: 0x040052C1 RID: 21185
	[CustomEditField(Sections = "Layout/Purchased Section")]
	public GameObject m_purchasedSectionBottom;

	// Token: 0x040052C2 RID: 21186
	[CustomEditField(Sections = "Layout/Purchased Section")]
	public GameObject m_purchasedSectionMidTemplate;

	// Token: 0x040052C3 RID: 21187
	[CustomEditField(Sections = "Layout/Purchased Section")]
	public MultiSliceElement m_purchasedSection;

	// Token: 0x040052C4 RID: 21188
	[CustomEditField(Sections = "Layout/Purchased Section")]
	public GameObject m_purchasedButtonContainer;

	// Token: 0x040052C5 RID: 21189
	[CustomEditField(Sections = "Layout/Purchased Section")]
	public Vector3 m_purchasedSectionOffset = new Vector3(0f, 0f, 0.145f);

	// Token: 0x040052C6 RID: 21190
	[CustomEditField(Sections = "Scroll")]
	public UIBScrollable m_scrollUpdate;

	// Token: 0x040052C7 RID: 21191
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_heroSelectionSound;

	// Token: 0x040052C8 RID: 21192
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_buttonsSlideUpSound;

	// Token: 0x040052C9 RID: 21193
	[CustomEditField(Sections = "Purchase Flow")]
	public GameObject m_purchaseAnimationBlocker;

	// Token: 0x040052CA RID: 21194
	[CustomEditField(Sections = "Animations")]
	public GameObject m_purchaseAnimationEndBone;

	// Token: 0x040052CB RID: 21195
	[CustomEditField(Sections = "Animations")]
	public Vector3 m_purchaseAnimationMidPointWorldOffset = new Vector3(0f, 0f, -7.5f);

	// Token: 0x040052CC RID: 21196
	[CustomEditField(Sections = "Animations")]
	public string m_purchaseAnimationName = "HeroSkin_HeroHolderPopOut";

	// Token: 0x040052CD RID: 21197
	private List<GeneralStoreHeroesSelectorButton> m_unpurchasedHeroesButtons = new List<GeneralStoreHeroesSelectorButton>();

	// Token: 0x040052CE RID: 21198
	private List<GeneralStoreHeroesSelectorButton> m_purchasedHeroesButtons = new List<GeneralStoreHeroesSelectorButton>();

	// Token: 0x040052CF RID: 21199
	private GeneralStoreHeroesContent m_heroesContent;

	// Token: 0x040052D0 RID: 21200
	private bool m_initializeFirstHero;

	// Token: 0x040052D1 RID: 21201
	private List<GameObject> m_purchasedSectionMidMeshes = new List<GameObject>();

	// Token: 0x040052D2 RID: 21202
	private int m_currentPurchaseRemovalIdx;
}
