using System.Collections;
using UnityEngine;

public class FirstPurchaseBox : MonoBehaviour
{
	public GameObject m_BoxBase;

	public GameObject m_BoxLid;

	public GameObject m_CardRootBone;

	public AnimationClip m_RevealCardAnimation;

	public AnimationClip m_GlowOutAnimation;

	private string m_CardId;

	private Actor m_CardActor;

	private PlayMakerFSM m_fsm;

	private PegUIElement m_cardUIElement;

	private GameObject m_InputBlockerPerspectiveUI;

	private GameObject m_InputBlockerCameraMask;

	private void Awake()
	{
		m_fsm = GetComponent<PlayMakerFSM>();
	}

	public void Reset()
	{
		if (m_BoxLid == null)
		{
			m_BoxBase.SetActive(value: true);
		}
		else
		{
			m_BoxBase.SetActive(value: false);
			m_BoxLid.SetActive(value: true);
		}
		if (m_CardRootBone != null)
		{
			m_CardRootBone.SetActive(value: false);
			SceneUtils.EnableColliders(m_CardRootBone, enable: false);
		}
	}

	public void RevealContents()
	{
		m_BoxBase.SetActive(value: true);
		if (m_fsm != null)
		{
			m_fsm.SendEvent("Action");
		}
	}

	[ContextMenu("Fake Purchase")]
	public void FakePurchase()
	{
		PurchaseBundle("NEW1_030");
	}

	public void PurchaseBundle(string cardID)
	{
		if (string.IsNullOrEmpty(cardID))
		{
			Debug.LogWarningFormat("PurchaseBundle() - CardID is empty");
			return;
		}
		if (m_BoxLid != null)
		{
			m_BoxLid.SetActive(value: false);
		}
		m_BoxBase.SetActive(value: true);
		if (m_CardRootBone != null)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(TAG_CARDTYPE.MINION), AssetLoadingOptions.IgnorePrefabPosition);
			gameObject.transform.parent = m_CardRootBone.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			m_CardActor = gameObject.GetComponent<Actor>();
			DefLoader.Get().LoadFullDef(cardID, OnCardDefLoaded, gameObject, new CardPortraitQuality(3, loadPremium: true));
		}
	}

	private void OnCardDefLoaded(string cardID, DefLoader.DisposableFullDef def, object callbackData)
	{
		using (def)
		{
			if (def == null)
			{
				Debug.LogWarningFormat("OnCardDefLoaded() - def for CardID {0} is null", cardID);
				return;
			}
			m_CardId = cardID;
			GameObject go = (GameObject)callbackData;
			m_CardActor.gameObject.SetActive(value: true);
			m_CardActor.SetFullDef(def);
			m_CardActor.SetPremium(TAG_PREMIUM.NORMAL);
			m_CardActor.UpdateAllComponents();
			SceneUtils.SetLayer(go, m_CardRootBone.layer);
			if (m_fsm != null)
			{
				m_fsm.SendEvent("Birth");
			}
			StartCoroutine(RevealCardAndSetupWaitForClick());
		}
	}

	private IEnumerator RevealCardAndSetupWaitForClick()
	{
		if (m_CardRootBone != null)
		{
			SceneUtils.EnableColliders(m_CardRootBone, enable: true);
		}
		m_cardUIElement = m_CardRootBone.GetComponent<PegUIElement>();
		if (m_cardUIElement == null)
		{
			Debug.LogWarning("SetupWaitForClick: PegUIElement missing!");
			yield break;
		}
		Camera camera = CameraUtils.FindFirstByLayer(GameLayer.PerspectiveUI);
		m_InputBlockerPerspectiveUI = CameraUtils.CreateInputBlocker(camera, "FirstPurchaseBoxInputBlocker", base.transform);
		m_InputBlockerPerspectiveUI.AddComponent<PegUIElement>();
		m_InputBlockerPerspectiveUI.layer = base.gameObject.layer;
		Vector3 localPosition = m_InputBlockerPerspectiveUI.transform.localPosition;
		m_InputBlockerPerspectiveUI.transform.localPosition = new Vector3(localPosition.x, 10f, localPosition.z);
		m_InputBlockerCameraMask = Object.Instantiate(m_InputBlockerPerspectiveUI);
		SceneUtils.SetLayer(m_InputBlockerCameraMask, GameLayer.CameraMask);
		m_InputBlockerCameraMask.transform.parent = m_InputBlockerPerspectiveUI.transform;
		m_InputBlockerCameraMask.transform.localPosition = Vector3.zero;
		m_InputBlockerCameraMask.transform.localRotation = Quaternion.identity;
		m_InputBlockerCameraMask.transform.localScale = Vector3.one;
		float seconds = m_RevealCardAnimation.length / 2f;
		yield return new WaitForSeconds(seconds);
		TAG_CLASS @class = DefLoader.Get().GetEntityDef(m_CardId).GetClass();
		NotificationManager.Get().PlayBundleInnkeeperLineForClass(@class);
		Object.Destroy(m_InputBlockerPerspectiveUI);
		if (m_cardUIElement != null)
		{
			m_cardUIElement.AddEventListener(UIEventType.PRESS, OnCardClicked);
		}
	}

	private void PlayInnkeeperLineForClass(TAG_CLASS cardClass)
	{
		bool clickToDismiss = UniversalInputManager.UsePhoneUI;
		string text = string.Empty;
		string soundPath = string.Empty;
		switch (cardClass)
		{
		case TAG_CLASS.DRUID:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_DRUID");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryDruid_01.prefab:2c4672cdfe2a96a45a7ac4f29c17d5b7";
			break;
		case TAG_CLASS.HUNTER:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_HUNTER");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryHunter_01.prefab:77302a32e0268f845a97992117241577";
			break;
		case TAG_CLASS.MAGE:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_MAGE");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryMage_01.prefab:2059ede4ae6efab489ecb4240a08d5bb";
			break;
		case TAG_CLASS.PALADIN:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_PALADIN");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryPaladin_01.prefab:21b7870188f66714b9707961d833b26a";
			break;
		case TAG_CLASS.PRIEST:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_PRIEST");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryPriest_01.prefab:fe9cd14401fd7f14f80950fb99864ce7";
			break;
		case TAG_CLASS.ROGUE:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_ROGUE");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryRogue_01.prefab:aa4c71ab99a240a4885e4a8d034adb1b";
			break;
		case TAG_CLASS.SHAMAN:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_SHAMAN");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryShaman_01.prefab:1101d9f890551164791f277babaa25d9";
			break;
		case TAG_CLASS.WARLOCK:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_WARLOCK");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryWarlock_01.prefab:5eaf5c883b0310e4d91bcfd3debc6eff";
			break;
		case TAG_CLASS.WARRIOR:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_WARRIOR");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryWarrior_01.prefab:41b4581beb2dae945843ed164a6ec710";
			break;
		}
		NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, text, soundPath, null, clickToDismiss);
	}

	private void OnCardClicked(UIEvent e)
	{
		OnCardClicked();
	}

	private void OnCardClicked()
	{
		if (m_fsm != null)
		{
			m_fsm.SendEvent("Death");
		}
		if (m_CardRootBone != null)
		{
			SceneUtils.EnableColliders(m_CardRootBone, enable: false);
		}
		if (m_cardUIElement != null)
		{
			m_cardUIElement.RemoveEventListener(UIEventType.PRESS, OnCardClicked);
		}
		ReturnToStore();
	}

	private void ReturnToStore()
	{
		((GeneralStorePacksPane)((GeneralStore)StoreManager.Get().GetCurrentStore()).GetCurrentPane()).RemoveFirstPurchaseBundle(m_GlowOutAnimation.length);
	}
}
