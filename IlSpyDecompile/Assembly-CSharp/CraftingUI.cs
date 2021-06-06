using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
	public UberText m_bankAmountText;

	public CreateButton m_buttonCreate;

	public DisenchantButton m_buttonDisenchant;

	public GameObject m_soulboundNotification;

	public UberText m_soulboundTitle;

	public UberText m_soulboundDesc;

	public UberText m_disenchantValue;

	public UberText m_craftValue;

	public GameObject m_wildTheming;

	public float m_disenchantDelayBeforeCardExplodes;

	public float m_disenchantDelayBeforeCardFlips;

	public float m_disenchantDelayBeforeBallsComeOut;

	public float m_craftDelayBeforeConstructSpell;

	public float m_craftDelayBeforeGhostDeath;

	public GameObject m_glowballs;

	public SoundDef m_craftingSound;

	public SoundDef m_disenchantSound;

	public Collider m_mouseOverCollider;

	private Actor m_explodingActor;

	private Actor m_constructingActor;

	private bool m_isAnimating;

	private List<GameObject> m_thingsToDestroy = new List<GameObject>();

	private GameObject m_activeObject;

	private bool m_enabled;

	private bool m_mousedOver;

	private Notification m_craftNotification;

	private bool m_initializedPositions;

	private void Update()
	{
		if (!m_enabled)
		{
			return;
		}
		RaycastHit hitInfo;
		if (m_isAnimating)
		{
			m_mousedOver = false;
		}
		else if (Physics.Raycast(Camera.main.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition()), layerMask: (LayerMask)512, hitInfo: out hitInfo, maxDistance: Camera.main.farClipPlane))
		{
			if (hitInfo.collider == m_mouseOverCollider)
			{
				NotifyOfMouseOver();
			}
			else
			{
				NotifyOfMouseOut();
			}
		}
	}

	private void OnDisable()
	{
		StopCurrentAnim(forceCleanup: true);
	}

	public void UpdateWildTheming()
	{
		if (!(m_wildTheming == null))
		{
			if (!CraftingManager.Get().GetShownCardInfo(out var entityDef, out var _))
			{
				m_wildTheming.SetActive(value: false);
			}
			else
			{
				m_wildTheming.SetActive(GameUtils.IsWildCard(entityDef));
			}
		}
	}

	public void UpdateCraftingButtonsAndSoulboundText()
	{
		UpdateBankText();
		if (!CraftingManager.Get().GetShownCardInfo(out var entityDef, out var premium))
		{
			m_buttonDisenchant.DisableButton();
			m_buttonCreate.DisableButton();
			return;
		}
		NetCache.CardDefinition cardDefinition = new NetCache.CardDefinition
		{
			Name = entityDef.GetCardId(),
			Premium = premium
		};
		int numOwnedIncludePending = CraftingManager.Get().GetNumOwnedIncludePending();
		bool flag = false;
		string empty = string.Empty;
		string empty2 = string.Empty;
		TAG_CARD_SET cardSet = entityDef.GetCardSet();
		string cardSetName = GameStrings.GetCardSetName(cardSet);
		NetCache.CardValue cardValue = CraftingManager.Get().GetCardValue(cardDefinition.Name, cardDefinition.Premium);
		empty = GameStrings.Get("GLUE_CRAFTING_SOULBOUND");
		if (numOwnedIncludePending <= 0)
		{
			empty = cardSetName;
			empty2 = (Network.IsLoggedIn() ? entityDef.GetHowToEarnText(cardDefinition.Premium) : GameStrings.Get("GLUE_CRAFTING_SOULBOUND_OFFLINE_DESC"));
		}
		else
		{
			CardSetDbfRecord cardSet2 = GameDbf.GetIndex().GetCardSet(cardSet);
			empty2 = (cardSet2.IsFeaturedCardSet ? GameStrings.Get("GLUE_CRAFTING_SOULBOUND_FEATURED_DESC") : (cardSet2.IsCoreCardSet ? GameStrings.Get("GLUE_CRAFTING_SOULBOUND_CORE_DESC") : (Network.IsLoggedIn() ? GameStrings.Get("GLUE_CRAFTING_SOULBOUND_DESC") : GameStrings.Get("GLUE_CRAFTING_SOULBOUND_OFFLINE_DESC"))));
		}
		bool willBecomeActiveInFuture;
		if (cardValue == null)
		{
			flag = false;
		}
		else if (IsCraftingEventForCardActive(cardDefinition.Name, premium, out willBecomeActiveInFuture) && Network.IsLoggedIn())
		{
			int numClientTransactions = CraftingManager.Get().GetNumClientTransactions();
			int num = cardValue.GetBuyValue();
			if (numClientTransactions < 0)
			{
				num = cardValue.GetSellValue();
			}
			int num2 = cardValue.GetSellValue();
			if (numClientTransactions > 0)
			{
				num2 = cardValue.GetBuyValue();
			}
			m_disenchantValue.Text = "+" + num2;
			m_craftValue.Text = "-" + num;
			flag = true;
		}
		else
		{
			flag = false;
			if (willBecomeActiveInFuture)
			{
				empty = GameStrings.Get("GLUE_CRAFTING_EVENT_NOT_ACTIVE_TITLE");
				empty2 = GameStrings.Format("GLUE_CRAFTING_EVENT_NOT_ACTIVE_DESCRIPTION", cardSetName);
			}
		}
		m_soulboundTitle.Text = empty;
		m_soulboundDesc.Text = empty2;
		if (!flag)
		{
			m_buttonDisenchant.DisableButton();
			m_buttonCreate.DisableButton();
			m_soulboundNotification.SetActive(value: true);
			m_activeObject = m_soulboundNotification;
			return;
		}
		if (!FixedRewardsMgr.Get().CanCraftCard(cardDefinition.Name, cardDefinition.Premium))
		{
			m_buttonDisenchant.DisableButton();
			m_buttonCreate.DisableButton();
			m_soulboundNotification.SetActive(value: true);
			m_activeObject = m_soulboundNotification;
			return;
		}
		m_soulboundNotification.SetActive(value: false);
		m_activeObject = base.gameObject;
		if (numOwnedIncludePending <= 0)
		{
			m_buttonDisenchant.DisableButton();
		}
		else
		{
			m_buttonDisenchant.EnableButton();
		}
		int num3 = (entityDef.IsElite() ? 1 : 2);
		long arcaneDustBalance = NetCache.Get().GetArcaneDustBalance();
		bool flag2 = CraftingManager.Get().GetNumClientTransactions() < 0;
		if (numOwnedIncludePending >= num3 || arcaneDustBalance < GetCardBuyValue(cardDefinition.Name, cardDefinition.Premium) || (RankMgr.Get().IsCardLockedInCurrentLeague(entityDef) && !flag2))
		{
			m_buttonCreate.DisableButton();
		}
		else
		{
			m_buttonCreate.EnableButton();
		}
	}

	public void DoDisenchant()
	{
		if (CraftingManager.Get().GetNumOwnedIncludePending() > 0)
		{
			UpdateTips();
			CraftingManager.Get().AdjustUnCommitedArcaneDustChanges(GetCardSellValue(CraftingManager.Get().GetShownActor().GetEntityDef()
				.GetCardId(), CraftingManager.Get().GetShownActor().GetPremium()));
			Options.Get().SetBool(Option.HAS_DISENCHANTED, val: true);
			CraftingManager.Get().NotifyOfTransaction(-1);
			UpdateCraftingButtonsAndSoulboundText();
			if (m_isAnimating)
			{
				CraftingManager.Get().FinishFlipCurrentActorEarly();
			}
			StopCurrentAnim();
			StartCoroutine(DoDisenchantAnims());
			CraftingManager.Get().StartCoroutine(StartCraftCooldown());
		}
	}

	public void CleanUpEffects()
	{
		if (m_explodingActor != null)
		{
			Spell spell = m_explodingActor.GetSpell(SpellType.DECONSTRUCT);
			if (spell != null && spell.GetActiveState() != 0)
			{
				m_explodingActor.GetSpell(SpellType.DECONSTRUCT).GetComponent<PlayMakerFSM>().SendEvent("Cancel");
				m_explodingActor.Hide();
			}
		}
		if (m_constructingActor != null)
		{
			Spell spell2 = m_constructingActor.GetSpell(SpellType.CONSTRUCT);
			if (spell2 != null && spell2.GetActiveState() != 0)
			{
				m_constructingActor.GetSpell(SpellType.CONSTRUCT).GetComponent<PlayMakerFSM>().SendEvent("Cancel");
				m_constructingActor.Hide();
			}
		}
		GetComponent<PlayMakerFSM>().SendEvent("Cancel");
		m_isAnimating = false;
	}

	public void DoCreate()
	{
		if (!CraftingManager.Get().GetShownCardInfo(out var entityDef, out var _))
		{
			return;
		}
		int numOwnedIncludePending = CraftingManager.Get().GetNumOwnedIncludePending();
		int num = (entityDef.IsElite() ? 1 : 2);
		if (numOwnedIncludePending < num)
		{
			UpdateTips();
			CraftingManager.Get().AdjustUnCommitedArcaneDustChanges(-GetCardBuyValue(CraftingManager.Get().GetShownActor().GetEntityDef()
				.GetCardId(), CraftingManager.Get().GetShownActor().GetPremium()));
			if (!Options.Get().GetBool(Option.HAS_CRAFTED))
			{
				Options.Get().SetBool(Option.HAS_CRAFTED, val: true);
			}
			CraftingManager.Get().NotifyOfTransaction(1);
			if (CraftingManager.Get().GetNumOwnedIncludePending() > 1)
			{
				CraftingManager.Get().ForceNonGhostFlagOn();
			}
			UpdateCraftingButtonsAndSoulboundText();
			StopCurrentAnim();
			StartCoroutine(DoCreateAnims());
			CraftingManager.Get().StartCoroutine(StartDisenchantCooldown());
		}
	}

	public void UpdateBankText()
	{
		long arcaneDustBalance = NetCache.Get().GetArcaneDustBalance();
		m_bankAmountText.Text = arcaneDustBalance.ToString();
		BnetBar.Get().RefreshCurrency();
		if ((bool)UniversalInputManager.UsePhoneUI && CraftingTray.Get() != null)
		{
			ArcaneDustAmount.Get().UpdateCurrentDustAmount();
		}
	}

	public void Disable(Vector3 hidePosition)
	{
		m_enabled = false;
		iTween.MoveTo(m_activeObject, iTween.Hash("time", 0.4f, "position", hidePosition));
		HideTips();
		StopCurrentAnim();
	}

	public bool IsEnabled()
	{
		return m_enabled;
	}

	public void Enable(Vector3 showPosition, Vector3 hidePosition)
	{
		if (!m_initializedPositions)
		{
			base.transform.position = hidePosition;
			m_soulboundNotification.transform.position = base.transform.position;
			m_soulboundTitle.Text = GameStrings.Get("GLUE_CRAFTING_SOULBOUND");
			m_soulboundDesc.Text = GameStrings.Get("GLUE_CRAFTING_SOULBOUND_DESC");
			m_activeObject = base.gameObject;
			m_initializedPositions = true;
		}
		m_enabled = true;
		UpdateCraftingButtonsAndSoulboundText();
		UpdateWildTheming();
		m_activeObject.SetActive(value: true);
		iTween.MoveTo(m_activeObject, iTween.Hash("time", 0.5f, "position", showPosition));
		ShowFirstTimeTips();
	}

	public void SetStartingActive()
	{
		m_soulboundNotification.SetActive(value: false);
		base.gameObject.SetActive(value: false);
	}

	private void ShowFirstTimeTips()
	{
		if (!(m_activeObject == m_soulboundNotification) && !Options.Get().GetBool(Option.HAS_CRAFTED) && UserAttentionManager.CanShowAttentionGrabber("CraftingUI.ShowFirstTimeTips"))
		{
			CreateDisenchantNotification();
			CreateCraftNotification();
		}
	}

	private void CreateDisenchantNotification()
	{
		m_buttonDisenchant.IsButtonEnabled();
	}

	private void CreateCraftNotification()
	{
		if (m_buttonCreate.IsButtonEnabled())
		{
			Vector3 position;
			Notification.PopUpArrowDirection direction;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				position = new Vector3(73.3f, 1f, 55.4f);
				direction = Notification.PopUpArrowDirection.Down;
			}
			else
			{
				position = new Vector3(55f, 1f, -56f);
				direction = Notification.PopUpArrowDirection.Left;
			}
			if (m_craftNotification == null)
			{
				m_craftNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, 16f * Vector3.one, GameStrings.Get("GLUE_COLLECTION_TUTORIAL06"), convertLegacyPosition: false);
			}
			if (m_craftNotification != null)
			{
				m_craftNotification.ShowPopUpArrow(direction);
			}
		}
	}

	private void UpdateTips()
	{
		if (Options.Get().GetBool(Option.HAS_CRAFTED) || !UserAttentionManager.CanShowAttentionGrabber("CraftingUI.UpdateTips"))
		{
			HideTips();
		}
		else if (m_craftNotification == null)
		{
			CreateCraftNotification();
		}
		else if (!m_buttonCreate.IsButtonEnabled())
		{
			NotificationManager.Get().DestroyNotification(m_craftNotification, 0f);
		}
	}

	private void HideTips()
	{
		if (m_craftNotification != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(m_craftNotification);
		}
	}

	private void NotifyOfMouseOver()
	{
		if (!m_mousedOver)
		{
			m_mousedOver = true;
			GetComponent<PlayMakerFSM>().SendEvent("Idle");
		}
	}

	private void NotifyOfMouseOut()
	{
		if (m_mousedOver)
		{
			m_mousedOver = false;
			GetComponent<PlayMakerFSM>().SendEvent("IdleCancel");
		}
	}

	private int GetCardBuyValue(string cardID, TAG_PREMIUM premium)
	{
		NetCache.CardValue cardValue = CraftingManager.Get().GetCardValue(cardID, premium);
		if (CraftingManager.Get().GetNumClientTransactions() >= 0)
		{
			return cardValue.GetBuyValue();
		}
		return cardValue.GetSellValue();
	}

	private int GetCardSellValue(string cardID, TAG_PREMIUM premium)
	{
		NetCache.CardValue cardValue = CraftingManager.Get().GetCardValue(cardID, premium);
		if (CraftingManager.Get().GetNumClientTransactions() <= 0)
		{
			return cardValue.GetSellValue();
		}
		return cardValue.GetBuyValue();
	}

	public static bool IsCraftingEventForCardActive(string cardID, TAG_PREMIUM premium, out bool willBecomeActiveInFuture)
	{
		willBecomeActiveInFuture = false;
		if (GameUtils.IsClassicCard(cardID))
		{
			return IsCraftingEventForCardActive(GameUtils.TranslateDbIdToCardId(GameUtils.GetCardTagValue(cardID, GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID)), premium, out willBecomeActiveInFuture);
		}
		CardDbfRecord cardRecord = GameUtils.GetCardRecord(cardID);
		if (cardRecord == null)
		{
			Debug.LogWarning($"CraftingUI.IsCraftingEventForCardActive could not find DBF record for card {cardID}, assuming it cannot be crafted or disenchanted");
			return false;
		}
		string eventName = cardRecord.CraftingEvent;
		if (premium == TAG_PREMIUM.GOLDEN && !string.IsNullOrEmpty(cardRecord.GoldenCraftingEvent))
		{
			eventName = cardRecord.GoldenCraftingEvent;
		}
		bool num = SpecialEventManager.Get().IsEventActive(eventName, activeIfDoesNotExist: true);
		if (!num)
		{
			willBecomeActiveInFuture = SpecialEventManager.Get().IsStartTimeInTheFuture(eventName);
		}
		return num;
	}

	private void StopCurrentAnim(bool forceCleanup = false)
	{
		if (!m_isAnimating && !forceCleanup)
		{
			return;
		}
		StopAllCoroutines();
		CleanUpEffects();
		foreach (GameObject item in m_thingsToDestroy)
		{
			if (!(item == null))
			{
				Log.Crafting.Print("StopCurrentAnim: Destroying GameObject {0}", item);
				Object.Destroy(item);
			}
		}
	}

	private IEnumerator StartDisenchantCooldown()
	{
		if (m_buttonDisenchant.GetComponent<Collider>().enabled)
		{
			m_buttonDisenchant.GetComponent<Collider>().enabled = false;
			yield return new WaitForSeconds(1f);
			m_buttonDisenchant.GetComponent<Collider>().enabled = true;
		}
	}

	private IEnumerator StartCraftCooldown()
	{
		if (m_buttonCreate.GetComponent<Collider>().enabled)
		{
			m_buttonCreate.GetComponent<Collider>().enabled = false;
			yield return new WaitForSeconds(1f);
			m_buttonCreate.GetComponent<Collider>().enabled = true;
		}
	}

	private IEnumerator DoDisenchantAnims()
	{
		SoundManager.Get().Play(m_disenchantSound.GetComponent<AudioSource>());
		SoundManager.Get().Stop(m_craftingSound.GetComponent<AudioSource>());
		m_isAnimating = true;
		PlayMakerFSM playmaker = GetComponent<PlayMakerFSM>();
		playmaker.SendEvent("Birth");
		yield return new WaitForSeconds(m_disenchantDelayBeforeCardExplodes);
		while (CraftingManager.Get().GetShownActor() == null)
		{
			yield return null;
		}
		m_explodingActor = CraftingManager.Get().GetShownActor();
		Actor oldActor = m_explodingActor;
		m_thingsToDestroy.Add(m_explodingActor.gameObject);
		Log.Crafting.Print("Adding {0} to thingsToDestroy", m_explodingActor.gameObject);
		UpdateBankText();
		if (CraftingManager.Get().IsCancelling())
		{
			yield break;
		}
		CraftingManager.Get().LoadGhostActorIfNecessary();
		m_explodingActor.ActivateSpellBirthState(SpellType.DECONSTRUCT);
		yield return new WaitForSeconds(m_disenchantDelayBeforeCardFlips);
		if (CraftingManager.Get().IsCancelling())
		{
			yield break;
		}
		CraftingManager.Get().FlipUpsideDownCard(m_explodingActor);
		yield return new WaitForSeconds(m_disenchantDelayBeforeBallsComeOut);
		if (!CraftingManager.Get().IsCancelling())
		{
			playmaker.SendEvent("Action");
			yield return new WaitForSeconds(1f);
			m_isAnimating = false;
			yield return new WaitForSeconds(10f);
			if (oldActor != null)
			{
				Object.Destroy(oldActor.gameObject);
			}
		}
	}

	private IEnumerator DoCreateAnims()
	{
		Actor shownActor = CraftingManager.Get().GetShownActor();
		SoundManager.Get().Play(m_craftingSound.GetComponent<AudioSource>());
		SoundManager.Get().Stop(m_disenchantSound.GetComponent<AudioSource>());
		m_isAnimating = true;
		CraftingManager.Get().FlipCurrentActor();
		GetComponent<PlayMakerFSM>().SendEvent("Birth");
		yield return new WaitForSeconds(m_craftDelayBeforeConstructSpell);
		if (CraftingManager.Get().IsCancelling())
		{
			yield break;
		}
		m_constructingActor = CraftingManager.Get().LoadNewActorAndConstructIt();
		UpdateBankText();
		yield return new WaitForSeconds(m_craftDelayBeforeGhostDeath);
		if (!CraftingManager.Get().IsCancelling())
		{
			if (shownActor.HasCardDef && shownActor.PlayEffectDef != null)
			{
				GameUtils.PlayCardEffectDefSounds(shownActor.PlayEffectDef);
			}
			CraftingManager.Get().FinishCreateAnims();
			yield return new WaitForSeconds(1f);
			m_isAnimating = false;
		}
	}
}
