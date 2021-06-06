using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006F0 RID: 1776
public class FirstPurchaseBox : MonoBehaviour
{
	// Token: 0x060062E8 RID: 25320 RVA: 0x00203759 File Offset: 0x00201959
	private void Awake()
	{
		this.m_fsm = base.GetComponent<PlayMakerFSM>();
	}

	// Token: 0x060062E9 RID: 25321 RVA: 0x00203768 File Offset: 0x00201968
	public void Reset()
	{
		if (this.m_BoxLid == null)
		{
			this.m_BoxBase.SetActive(true);
		}
		else
		{
			this.m_BoxBase.SetActive(false);
			this.m_BoxLid.SetActive(true);
		}
		if (this.m_CardRootBone != null)
		{
			this.m_CardRootBone.SetActive(false);
			SceneUtils.EnableColliders(this.m_CardRootBone, false);
		}
	}

	// Token: 0x060062EA RID: 25322 RVA: 0x002037CF File Offset: 0x002019CF
	public void RevealContents()
	{
		this.m_BoxBase.SetActive(true);
		if (this.m_fsm != null)
		{
			this.m_fsm.SendEvent("Action");
		}
	}

	// Token: 0x060062EB RID: 25323 RVA: 0x002037FB File Offset: 0x002019FB
	[ContextMenu("Fake Purchase")]
	public void FakePurchase()
	{
		this.PurchaseBundle("NEW1_030");
	}

	// Token: 0x060062EC RID: 25324 RVA: 0x00203808 File Offset: 0x00201A08
	public void PurchaseBundle(string cardID)
	{
		if (string.IsNullOrEmpty(cardID))
		{
			Debug.LogWarningFormat("PurchaseBundle() - CardID is empty", Array.Empty<object>());
			return;
		}
		if (this.m_BoxLid != null)
		{
			this.m_BoxLid.SetActive(false);
		}
		this.m_BoxBase.SetActive(true);
		if (this.m_CardRootBone != null)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(TAG_CARDTYPE.MINION), AssetLoadingOptions.IgnorePrefabPosition);
			gameObject.transform.parent = this.m_CardRootBone.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			this.m_CardActor = gameObject.GetComponent<Actor>();
			DefLoader.Get().LoadFullDef(cardID, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnCardDefLoaded), gameObject, new CardPortraitQuality(3, true));
		}
	}

	// Token: 0x060062ED RID: 25325 RVA: 0x002038FC File Offset: 0x00201AFC
	private void OnCardDefLoaded(string cardID, DefLoader.DisposableFullDef def, object callbackData)
	{
		try
		{
			if (def == null)
			{
				Debug.LogWarningFormat("OnCardDefLoaded() - def for CardID {0} is null", new object[]
				{
					cardID
				});
			}
			else
			{
				this.m_CardId = cardID;
				GameObject go = (GameObject)callbackData;
				this.m_CardActor.gameObject.SetActive(true);
				this.m_CardActor.SetFullDef(def);
				this.m_CardActor.SetPremium(TAG_PREMIUM.NORMAL);
				this.m_CardActor.UpdateAllComponents();
				SceneUtils.SetLayer(go, this.m_CardRootBone.layer, null);
				if (this.m_fsm != null)
				{
					this.m_fsm.SendEvent("Birth");
				}
				base.StartCoroutine(this.RevealCardAndSetupWaitForClick());
			}
		}
		finally
		{
			if (def != null)
			{
				((IDisposable)def).Dispose();
			}
		}
	}

	// Token: 0x060062EE RID: 25326 RVA: 0x002039C8 File Offset: 0x00201BC8
	private IEnumerator RevealCardAndSetupWaitForClick()
	{
		if (this.m_CardRootBone != null)
		{
			SceneUtils.EnableColliders(this.m_CardRootBone, true);
		}
		this.m_cardUIElement = this.m_CardRootBone.GetComponent<PegUIElement>();
		if (this.m_cardUIElement == null)
		{
			Debug.LogWarning("SetupWaitForClick: PegUIElement missing!");
			yield break;
		}
		Camera camera = CameraUtils.FindFirstByLayer(GameLayer.PerspectiveUI);
		this.m_InputBlockerPerspectiveUI = CameraUtils.CreateInputBlocker(camera, "FirstPurchaseBoxInputBlocker", base.transform);
		this.m_InputBlockerPerspectiveUI.AddComponent<PegUIElement>();
		this.m_InputBlockerPerspectiveUI.layer = base.gameObject.layer;
		Vector3 localPosition = this.m_InputBlockerPerspectiveUI.transform.localPosition;
		this.m_InputBlockerPerspectiveUI.transform.localPosition = new Vector3(localPosition.x, 10f, localPosition.z);
		this.m_InputBlockerCameraMask = UnityEngine.Object.Instantiate<GameObject>(this.m_InputBlockerPerspectiveUI);
		SceneUtils.SetLayer(this.m_InputBlockerCameraMask, GameLayer.CameraMask);
		this.m_InputBlockerCameraMask.transform.parent = this.m_InputBlockerPerspectiveUI.transform;
		this.m_InputBlockerCameraMask.transform.localPosition = Vector3.zero;
		this.m_InputBlockerCameraMask.transform.localRotation = Quaternion.identity;
		this.m_InputBlockerCameraMask.transform.localScale = Vector3.one;
		float seconds = this.m_RevealCardAnimation.length / 2f;
		yield return new WaitForSeconds(seconds);
		TAG_CLASS @class = DefLoader.Get().GetEntityDef(this.m_CardId).GetClass();
		NotificationManager.Get().PlayBundleInnkeeperLineForClass(@class);
		UnityEngine.Object.Destroy(this.m_InputBlockerPerspectiveUI);
		if (this.m_cardUIElement != null)
		{
			this.m_cardUIElement.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OnCardClicked));
		}
		yield break;
	}

	// Token: 0x060062EF RID: 25327 RVA: 0x002039D8 File Offset: 0x00201BD8
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

	// Token: 0x060062F0 RID: 25328 RVA: 0x00203AEC File Offset: 0x00201CEC
	private void OnCardClicked(UIEvent e)
	{
		this.OnCardClicked();
	}

	// Token: 0x060062F1 RID: 25329 RVA: 0x00203AF4 File Offset: 0x00201CF4
	private void OnCardClicked()
	{
		if (this.m_fsm != null)
		{
			this.m_fsm.SendEvent("Death");
		}
		if (this.m_CardRootBone != null)
		{
			SceneUtils.EnableColliders(this.m_CardRootBone, false);
		}
		if (this.m_cardUIElement != null)
		{
			this.m_cardUIElement.RemoveEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OnCardClicked));
		}
		this.ReturnToStore();
	}

	// Token: 0x060062F2 RID: 25330 RVA: 0x00203B66 File Offset: 0x00201D66
	private void ReturnToStore()
	{
		((GeneralStorePacksPane)((GeneralStore)StoreManager.Get().GetCurrentStore()).GetCurrentPane()).RemoveFirstPurchaseBundle(this.m_GlowOutAnimation.length);
	}

	// Token: 0x0400520D RID: 21005
	public GameObject m_BoxBase;

	// Token: 0x0400520E RID: 21006
	public GameObject m_BoxLid;

	// Token: 0x0400520F RID: 21007
	public GameObject m_CardRootBone;

	// Token: 0x04005210 RID: 21008
	public AnimationClip m_RevealCardAnimation;

	// Token: 0x04005211 RID: 21009
	public AnimationClip m_GlowOutAnimation;

	// Token: 0x04005212 RID: 21010
	private string m_CardId;

	// Token: 0x04005213 RID: 21011
	private Actor m_CardActor;

	// Token: 0x04005214 RID: 21012
	private PlayMakerFSM m_fsm;

	// Token: 0x04005215 RID: 21013
	private PegUIElement m_cardUIElement;

	// Token: 0x04005216 RID: 21014
	private GameObject m_InputBlockerPerspectiveUI;

	// Token: 0x04005217 RID: 21015
	private GameObject m_InputBlockerCameraMask;
}
