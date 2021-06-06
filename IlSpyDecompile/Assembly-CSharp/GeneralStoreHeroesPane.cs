using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using PegasusUtil;
using UnityEngine;

[CustomEditClass]
public class GeneralStoreHeroesPane : GeneralStorePane
{
	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_heroUnpurchasedFrame;

	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_heroPurchasedFrame;

	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_heroAnimationFrame;

	[SerializeField]
	private Vector3 m_unpurchasedHeroButtonSpacing = new Vector3(0f, 0f, 0.285f);

	[SerializeField]
	private Vector3 m_purchasedHeroButtonSpacing = new Vector3(0f, 0f, 0.092f);

	[CustomEditField(Sections = "Layout")]
	public float m_unpurchasedHeroButtonHeight = 0.0275f;

	[CustomEditField(Sections = "Layout")]
	public float m_purchasedHeroButtonHeight;

	[CustomEditField(Sections = "Layout")]
	public float m_purchasedHeroButtonHeightPadding = 0.01f;

	[CustomEditField(Sections = "Layout")]
	public float m_maxPurchasedHeightAdd;

	[CustomEditField(Sections = "Layout/Purchased Section")]
	public GameObject m_purchasedSectionTop;

	[CustomEditField(Sections = "Layout/Purchased Section")]
	public GameObject m_purchasedSectionBottom;

	[CustomEditField(Sections = "Layout/Purchased Section")]
	public GameObject m_purchasedSectionMidTemplate;

	[CustomEditField(Sections = "Layout/Purchased Section")]
	public MultiSliceElement m_purchasedSection;

	[CustomEditField(Sections = "Layout/Purchased Section")]
	public GameObject m_purchasedButtonContainer;

	[CustomEditField(Sections = "Layout/Purchased Section")]
	public Vector3 m_purchasedSectionOffset = new Vector3(0f, 0f, 0.145f);

	[CustomEditField(Sections = "Scroll")]
	public UIBScrollable m_scrollUpdate;

	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_heroSelectionSound;

	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_buttonsSlideUpSound;

	[CustomEditField(Sections = "Purchase Flow")]
	public GameObject m_purchaseAnimationBlocker;

	[CustomEditField(Sections = "Animations")]
	public GameObject m_purchaseAnimationEndBone;

	[CustomEditField(Sections = "Animations")]
	public Vector3 m_purchaseAnimationMidPointWorldOffset = new Vector3(0f, 0f, -7.5f);

	[CustomEditField(Sections = "Animations")]
	public string m_purchaseAnimationName = "HeroSkin_HeroHolderPopOut";

	private List<GeneralStoreHeroesSelectorButton> m_unpurchasedHeroesButtons = new List<GeneralStoreHeroesSelectorButton>();

	private List<GeneralStoreHeroesSelectorButton> m_purchasedHeroesButtons = new List<GeneralStoreHeroesSelectorButton>();

	private GeneralStoreHeroesContent m_heroesContent;

	private bool m_initializeFirstHero;

	private List<GameObject> m_purchasedSectionMidMeshes = new List<GameObject>();

	private int m_currentPurchaseRemovalIdx;

	[CustomEditField(Sections = "Layout")]
	public Vector3 UnpurchasedHeroButtonSpacing
	{
		get
		{
			return m_unpurchasedHeroButtonSpacing;
		}
		set
		{
			m_unpurchasedHeroButtonSpacing = value;
			PositionAllHeroButtons();
		}
	}

	[CustomEditField(Sections = "Layout")]
	public Vector3 PurchasedHeroButtonSpacing
	{
		get
		{
			return m_purchasedHeroButtonSpacing;
		}
		set
		{
			m_purchasedHeroButtonSpacing = value;
			PositionAllHeroButtons();
		}
	}

	private void Awake()
	{
		m_heroesContent = m_parentContent as GeneralStoreHeroesContent;
		PopulateHeroes();
		m_purchaseAnimationBlocker.SetActive(value: false);
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(OnItemPurchased);
		CheatMgr.Get().RegisterCheatHandler("herobuy", OnHeroPurchased_cheat);
	}

	private void OnDestroy()
	{
		if (HearthstoneServices.TryGet<CheatMgr>(out var service))
		{
			service.UnregisterCheatHandler("herobuy", OnHeroPurchased_cheat);
		}
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(OnItemPurchased);
	}

	public override void PrePaneSwappedIn()
	{
		SetupInitialSelectedHero();
	}

	public void RefreshHeroAvailability()
	{
	}

	private void PopulateHeroes()
	{
		SpecialEventManager specialEventManager = SpecialEventManager.Get();
		foreach (CardHeroDbfRecord record in GameDbf.CardHero.GetRecords())
		{
			Network.Bundle heroBundle = null;
			if (StoreManager.Get().GetHeroBundleByCardDbId(record.CardId, out heroBundle) && specialEventManager.IsEventActive(heroBundle.ProductEvent, activeIfDoesNotExist: false))
			{
				CreateNewHeroButton(record, heroBundle).SetSortOrder(record.StoreSortOrder);
			}
		}
		PositionAllHeroButtons();
	}

	private GeneralStoreHeroesSelectorButton CreateNewHeroButton(CardHeroDbfRecord cardHero, Network.Bundle heroBundle)
	{
		if (StoreManager.Get().IsProductAlreadyOwned(heroBundle))
		{
			return CreatePurchasedHeroButton(cardHero, heroBundle);
		}
		return CreateUnpurchasedHeroButton(cardHero, heroBundle);
	}

	private GeneralStoreHeroesSelectorButton CreateUnpurchasedHeroButton(CardHeroDbfRecord cardHero, Network.Bundle heroBundle)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(m_heroUnpurchasedFrame);
		GeneralStoreHeroesSelectorButton component = gameObject.GetComponent<GeneralStoreHeroesSelectorButton>();
		if (component == null)
		{
			Debug.LogError("Prefab does not contain GeneralStoreHeroesSelectorButton component.");
			Object.Destroy(gameObject);
			return null;
		}
		GameUtils.SetParent(component, m_paneContainer, withRotation: true);
		SceneUtils.SetLayer(component, m_paneContainer.layer);
		m_unpurchasedHeroesButtons.Add(component);
		SetupHeroButton(cardHero, component);
		return component;
	}

	public GeneralStoreHeroesSelectorButton CreatePurchasedHeroButton(CardHeroDbfRecord cardHero, Network.Bundle heroBundle)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(m_heroPurchasedFrame);
		GeneralStoreHeroesSelectorButton component = gameObject.GetComponent<GeneralStoreHeroesSelectorButton>();
		if (component == null)
		{
			Debug.LogError("Prefab does not contain GeneralStoreHeroesSelectorButton component.");
			Object.Destroy(gameObject);
			return null;
		}
		GameUtils.SetParent(component, m_purchasedButtonContainer, withRotation: true);
		SceneUtils.SetLayer(component, m_purchasedButtonContainer.layer);
		m_purchasedHeroesButtons.Add(component);
		SetupHeroButton(cardHero, component);
		return component;
	}

	private void SetupHeroButton(CardHeroDbfRecord cardHero, GeneralStoreHeroesSelectorButton heroButton)
	{
		string cardId2 = GameUtils.TranslateDbIdToCardId(cardHero.CardId);
		heroButton.SetCardHeroDbfRecord(cardHero);
		heroButton.SetPurchased(purchased: false);
		heroButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			SelectHero(heroButton);
		});
		DefLoader.Get().LoadFullDef(cardId2, delegate(string cardId, DefLoader.DisposableFullDef fullDef, object data)
		{
			using (fullDef)
			{
				heroButton.UpdatePortrait(fullDef);
				heroButton.UpdateName(fullDef.EntityDef.GetName());
			}
		});
	}

	private void UpdatePurchasedSectionLayout()
	{
		if (m_purchasedHeroesButtons.Count == 0)
		{
			m_purchasedButtonContainer.SetActive(value: false);
			m_purchasedSection.gameObject.SetActive(value: false);
			return;
		}
		m_purchasedButtonContainer.SetActive(value: true);
		m_purchasedSection.gameObject.SetActive(value: true);
		if (m_purchasedSectionMidMeshes.Count < m_purchasedHeroesButtons.Count)
		{
			int num = m_purchasedHeroesButtons.Count - m_purchasedSectionMidMeshes.Count;
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = (GameObject)GameUtils.Instantiate(m_purchasedSectionMidTemplate, m_purchasedSection.gameObject, withRotation: true);
				gameObject.SetActive(value: true);
				m_purchasedSectionMidMeshes.Add(gameObject);
			}
		}
		m_purchasedSection.ClearSlices();
		m_purchasedSection.AddSlice(m_purchasedSectionTop);
		foreach (GameObject purchasedSectionMidMesh in m_purchasedSectionMidMeshes)
		{
			m_purchasedSection.AddSlice(purchasedSectionMidMesh);
		}
		m_purchasedSection.AddSlice(m_purchasedSectionBottom);
		m_purchasedSection.UpdateSlices();
	}

	private void SelectHero(GeneralStoreHeroesSelectorButton button)
	{
		foreach (GeneralStoreHeroesSelectorButton unpurchasedHeroesButton in m_unpurchasedHeroesButtons)
		{
			unpurchasedHeroesButton.Unselect();
		}
		foreach (GeneralStoreHeroesSelectorButton purchasedHeroesButton in m_purchasedHeroesButtons)
		{
			purchasedHeroesButton.Unselect();
		}
		button.Select();
		Options.Get().SetInt(Option.LAST_SELECTED_STORE_HERO_ID, button.GetHeroDbId());
		m_heroesContent.SelectHero(button.GetCardHeroDbfRecord());
		if (!string.IsNullOrEmpty(m_heroSelectionSound))
		{
			SoundManager.Get().LoadAndPlay(m_heroSelectionSound);
		}
	}

	private void SetupInitialSelectedHero()
	{
		if (m_initializeFirstHero)
		{
			return;
		}
		m_initializeFirstHero = true;
		int @int = Options.Get().GetInt(Option.LAST_SELECTED_STORE_HERO_ID, -1);
		if (@int == -1)
		{
			return;
		}
		List<GeneralStoreHeroesSelectorButton> list = new List<GeneralStoreHeroesSelectorButton>();
		list.AddRange(m_unpurchasedHeroesButtons);
		list.AddRange(m_purchasedHeroesButtons);
		foreach (GeneralStoreHeroesSelectorButton item in list)
		{
			if (item.GetHeroDbId() == @int)
			{
				m_heroesContent.SelectHero(item.GetCardHeroDbfRecord(), animate: false);
				item.Select();
				break;
			}
		}
	}

	private void PositionAllHeroButtons()
	{
		PositionUnpurchasedHeroButtons();
		PositionPurchasedHeroButtons();
	}

	private void PositionUnpurchasedHeroButtons()
	{
		m_unpurchasedHeroesButtons.Sort(delegate(GeneralStoreHeroesSelectorButton lhs, GeneralStoreHeroesSelectorButton rhs)
		{
			int sortOrder = lhs.GetSortOrder();
			int sortOrder2 = rhs.GetSortOrder();
			if (sortOrder < sortOrder2)
			{
				return -1;
			}
			return (sortOrder > sortOrder2) ? 1 : 0;
		});
		for (int i = 0; i < m_unpurchasedHeroesButtons.Count; i++)
		{
			m_unpurchasedHeroesButtons[i].transform.localPosition = m_unpurchasedHeroButtonSpacing * i;
		}
	}

	private void PositionPurchasedHeroButtons(bool sortAndSetSectionPos = true)
	{
		if (sortAndSetSectionPos)
		{
			m_purchasedHeroesButtons.Sort(delegate(GeneralStoreHeroesSelectorButton lhs, GeneralStoreHeroesSelectorButton rhs)
			{
				int sortOrder = lhs.GetSortOrder();
				int sortOrder2 = rhs.GetSortOrder();
				if (sortOrder < sortOrder2)
				{
					return -1;
				}
				return (sortOrder > sortOrder2) ? 1 : 0;
			});
			m_purchasedSection.transform.localPosition = m_unpurchasedHeroButtonSpacing * (m_unpurchasedHeroesButtons.Count - 1) + m_purchasedSectionOffset;
		}
		for (int i = 0; i < m_purchasedHeroesButtons.Count; i++)
		{
			m_purchasedHeroesButtons[i].transform.localPosition = m_purchasedHeroButtonSpacing * i;
		}
		UpdatePurchasedSectionLayout();
	}

	private IEnumerator AnimateShowPurchase(int btnIndex)
	{
		m_purchaseAnimationBlocker.SetActive(value: true);
		m_scrollUpdate.Pause(pause: true);
		if (GeneralStore.Get().GetMode() != GeneralStoreMode.HEROES)
		{
			GeneralStore.Get().SetMode(GeneralStoreMode.HEROES);
			yield return new WaitForSeconds(1f);
		}
		GeneralStoreHeroesSelectorButton removeBtn = m_unpurchasedHeroesButtons[btnIndex];
		float percentage = (float)btnIndex / (float)(m_unpurchasedHeroesButtons.Count + m_purchasedHeroesButtons.Count - 1);
		m_scrollUpdate.SetScroll(percentage, iTween.EaseType.easeInOutCirc, 0.2f);
		yield return new WaitForSeconds(0.21f);
		GameObject animateBtnObj = AssetLoader.Get().InstantiatePrefab(m_heroAnimationFrame);
		GeneralStoreHeroesSelectorButton component = animateBtnObj.GetComponent<GeneralStoreHeroesSelectorButton>();
		SceneUtils.SetLayer(component, GameLayer.PerspectiveUI);
		component.transform.position = removeBtn.transform.position;
		component.UpdatePortrait(removeBtn);
		component.UpdateName(removeBtn);
		removeBtn.gameObject.SetActive(value: false);
		PlayMakerFSM animation = component.GetComponent<PlayMakerFSM>();
		FsmVector3 fsmVector = animation.FsmVariables.FindFsmVector3("PopStartPos");
		FsmVector3 fsmVector2 = animation.FsmVariables.FindFsmVector3("PopMidPos");
		FsmVector3 fsmVector3 = animation.FsmVariables.FindFsmVector3("PopEndPos");
		fsmVector.Value = removeBtn.transform.position;
		fsmVector2.Value = removeBtn.transform.position + m_purchaseAnimationMidPointWorldOffset;
		fsmVector3.Value = m_purchaseAnimationEndBone.transform.position;
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		if (camera != null)
		{
			animation.FsmVariables.FindFsmGameObject("CameraObjectShake").Value = camera.gameObject;
		}
		animation.FsmVariables.FindFsmString("PopOutAnimationName").Value = m_purchaseAnimationName;
		animation.SendEvent("PopOut");
		yield return new WaitForSeconds(0.5f);
		m_heroesContent.PlayCurrentHeroPurchaseEmote();
		yield return null;
		FsmBool animComplete2 = animation.FsmVariables.FindFsmBool("AnimationComplete");
		while (!animComplete2.Value)
		{
			yield return null;
		}
		CreatePurchasedHeroButton(removeBtn.GetCardHeroDbfRecord(), null).Select();
		m_unpurchasedHeroesButtons.Remove(removeBtn);
		PositionPurchasedHeroButtons(sortAndSetSectionPos: false);
		yield return new WaitForSeconds(0.25f);
		while (!UniversalInputManager.Get().GetMouseButtonDown(0))
		{
			yield return null;
		}
		animation.SendEvent("EchoHero");
		yield return null;
		animComplete2 = animation.FsmVariables.FindFsmBool("AnimationComplete");
		while (!animComplete2.Value)
		{
			yield return null;
		}
		for (int i = m_currentPurchaseRemovalIdx; i < m_unpurchasedHeroesButtons.Count; i++)
		{
			iTween.MoveTo(m_unpurchasedHeroesButtons[i].gameObject, iTween.Hash("position", m_unpurchasedHeroButtonSpacing * i, "islocal", true, "easetype", iTween.EaseType.easeInOutCirc, "time", 0.25f));
		}
		iTween.MoveTo(m_purchasedSection.gameObject, iTween.Hash("position", m_unpurchasedHeroButtonSpacing * (m_unpurchasedHeroesButtons.Count - 1) + m_purchasedSectionOffset, "islocal", true, "easetype", iTween.EaseType.easeInOutCirc, "time", 0.25f));
		if (!string.IsNullOrEmpty(m_buttonsSlideUpSound))
		{
			SoundManager.Get().LoadAndPlay(m_buttonsSlideUpSound);
		}
		yield return new WaitForSeconds(0.25f);
		Object.Destroy(removeBtn.gameObject);
		Object.Destroy(animateBtnObj);
		m_scrollUpdate.Pause(pause: false);
		m_purchaseAnimationBlocker.SetActive(value: false);
	}

	private void OnItemPurchased(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		if (bundle == null || bundle.Items == null)
		{
			return;
		}
		int num = 0;
		foreach (Network.BundleItem item in bundle.Items)
		{
			if (item.ItemType == ProductType.PRODUCT_TYPE_HIDDEN_LICENSE)
			{
				return;
			}
			if (item != null && item.ItemType == ProductType.PRODUCT_TYPE_HERO)
			{
				num = item.ProductData;
			}
		}
		if (num != 0)
		{
			OnHeroPurchased(num);
		}
	}

	private void OnHeroPurchased(int heroCardDbId)
	{
		int num = m_unpurchasedHeroesButtons.FindIndex((GeneralStoreHeroesSelectorButton e) => e.GetHeroCardDbId() == heroCardDbId);
		if (num == -1)
		{
			Debug.LogError($"Hero Card DB ID {heroCardDbId} does not exist in button list.");
		}
		else
		{
			RunHeroPurchaseAnimation(num);
		}
	}

	private void RunHeroPurchaseAnimation(int btnIndex)
	{
		m_currentPurchaseRemovalIdx = btnIndex;
		StartCoroutine(AnimateShowPurchase(btnIndex));
	}

	private bool OnHeroPurchased_cheat(string func, string[] args, string rawArgs)
	{
		if (args.Length == 0)
		{
			return true;
		}
		int result = -1;
		if (int.TryParse(args[0], out result) && result >= 0 && result < m_unpurchasedHeroesButtons.Count)
		{
			RunHeroPurchaseAnimation(result);
		}
		return true;
	}
}
