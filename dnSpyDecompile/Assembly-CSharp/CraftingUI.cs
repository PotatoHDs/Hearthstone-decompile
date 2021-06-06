using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000120 RID: 288
public class CraftingUI : MonoBehaviour
{
	// Token: 0x0600130D RID: 4877 RVA: 0x0006D0F0 File Offset: 0x0006B2F0
	private void Update()
	{
		if (!this.m_enabled)
		{
			return;
		}
		if (this.m_isAnimating)
		{
			this.m_mousedOver = false;
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		LayerMask mask = 512;
		RaycastHit raycastHit;
		if (!Physics.Raycast(ray, out raycastHit, Camera.main.farClipPlane, mask))
		{
			return;
		}
		if (raycastHit.collider == this.m_mouseOverCollider)
		{
			this.NotifyOfMouseOver();
			return;
		}
		this.NotifyOfMouseOut();
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x0006D170 File Offset: 0x0006B370
	private void OnDisable()
	{
		this.StopCurrentAnim(true);
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x0006D17C File Offset: 0x0006B37C
	public void UpdateWildTheming()
	{
		if (this.m_wildTheming == null)
		{
			return;
		}
		EntityDef def;
		TAG_PREMIUM tag_PREMIUM;
		if (!CraftingManager.Get().GetShownCardInfo(out def, out tag_PREMIUM))
		{
			this.m_wildTheming.SetActive(false);
			return;
		}
		this.m_wildTheming.SetActive(GameUtils.IsWildCard(def));
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x0006D1C8 File Offset: 0x0006B3C8
	public void UpdateCraftingButtonsAndSoulboundText()
	{
		this.UpdateBankText();
		EntityDef entityDef;
		TAG_PREMIUM premium;
		if (!CraftingManager.Get().GetShownCardInfo(out entityDef, out premium))
		{
			this.m_buttonDisenchant.DisableButton();
			this.m_buttonCreate.DisableButton();
			return;
		}
		NetCache.CardDefinition cardDefinition = new NetCache.CardDefinition
		{
			Name = entityDef.GetCardId(),
			Premium = premium
		};
		int numOwnedIncludePending = CraftingManager.Get().GetNumOwnedIncludePending();
		string text = string.Empty;
		string text2 = string.Empty;
		TAG_CARD_SET cardSet = entityDef.GetCardSet();
		string cardSetName = GameStrings.GetCardSetName(cardSet);
		NetCache.CardValue cardValue = CraftingManager.Get().GetCardValue(cardDefinition.Name, cardDefinition.Premium);
		text = GameStrings.Get("GLUE_CRAFTING_SOULBOUND");
		if (numOwnedIncludePending <= 0)
		{
			text = cardSetName;
			if (!Network.IsLoggedIn())
			{
				text2 = GameStrings.Get("GLUE_CRAFTING_SOULBOUND_OFFLINE_DESC");
			}
			else
			{
				text2 = entityDef.GetHowToEarnText(cardDefinition.Premium);
			}
		}
		else
		{
			CardSetDbfRecord cardSet2 = GameDbf.GetIndex().GetCardSet(cardSet);
			if (cardSet2.IsFeaturedCardSet)
			{
				text2 = GameStrings.Get("GLUE_CRAFTING_SOULBOUND_FEATURED_DESC");
			}
			else if (cardSet2.IsCoreCardSet)
			{
				text2 = GameStrings.Get("GLUE_CRAFTING_SOULBOUND_CORE_DESC");
			}
			else if (!Network.IsLoggedIn())
			{
				text2 = GameStrings.Get("GLUE_CRAFTING_SOULBOUND_OFFLINE_DESC");
			}
			else
			{
				text2 = GameStrings.Get("GLUE_CRAFTING_SOULBOUND_DESC");
			}
		}
		bool flag;
		bool flag2;
		if (cardValue == null)
		{
			flag = false;
		}
		else if (CraftingUI.IsCraftingEventForCardActive(cardDefinition.Name, premium, out flag2) && Network.IsLoggedIn())
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
			this.m_disenchantValue.Text = "+" + num2.ToString();
			this.m_craftValue.Text = "-" + num.ToString();
			flag = true;
		}
		else
		{
			flag = false;
			if (flag2)
			{
				text = GameStrings.Get("GLUE_CRAFTING_EVENT_NOT_ACTIVE_TITLE");
				text2 = GameStrings.Format("GLUE_CRAFTING_EVENT_NOT_ACTIVE_DESCRIPTION", new object[]
				{
					cardSetName
				});
			}
		}
		this.m_soulboundTitle.Text = text;
		this.m_soulboundDesc.Text = text2;
		if (!flag)
		{
			this.m_buttonDisenchant.DisableButton();
			this.m_buttonCreate.DisableButton();
			this.m_soulboundNotification.SetActive(true);
			this.m_activeObject = this.m_soulboundNotification;
			return;
		}
		if (!FixedRewardsMgr.Get().CanCraftCard(cardDefinition.Name, cardDefinition.Premium))
		{
			this.m_buttonDisenchant.DisableButton();
			this.m_buttonCreate.DisableButton();
			this.m_soulboundNotification.SetActive(true);
			this.m_activeObject = this.m_soulboundNotification;
			return;
		}
		this.m_soulboundNotification.SetActive(false);
		this.m_activeObject = base.gameObject;
		if (numOwnedIncludePending <= 0)
		{
			this.m_buttonDisenchant.DisableButton();
		}
		else
		{
			this.m_buttonDisenchant.EnableButton();
		}
		int num3 = entityDef.IsElite() ? 1 : 2;
		long arcaneDustBalance = NetCache.Get().GetArcaneDustBalance();
		bool flag3 = CraftingManager.Get().GetNumClientTransactions() < 0;
		if (numOwnedIncludePending >= num3 || arcaneDustBalance < (long)this.GetCardBuyValue(cardDefinition.Name, cardDefinition.Premium) || (RankMgr.Get().IsCardLockedInCurrentLeague(entityDef) && !flag3))
		{
			this.m_buttonCreate.DisableButton();
			return;
		}
		this.m_buttonCreate.EnableButton();
	}

	// Token: 0x06001311 RID: 4881 RVA: 0x0006D4EC File Offset: 0x0006B6EC
	public void DoDisenchant()
	{
		if (CraftingManager.Get().GetNumOwnedIncludePending() <= 0)
		{
			return;
		}
		this.UpdateTips();
		CraftingManager.Get().AdjustUnCommitedArcaneDustChanges(this.GetCardSellValue(CraftingManager.Get().GetShownActor().GetEntityDef().GetCardId(), CraftingManager.Get().GetShownActor().GetPremium()));
		Options.Get().SetBool(Option.HAS_DISENCHANTED, true);
		CraftingManager.Get().NotifyOfTransaction(-1);
		this.UpdateCraftingButtonsAndSoulboundText();
		if (this.m_isAnimating)
		{
			CraftingManager.Get().FinishFlipCurrentActorEarly();
		}
		this.StopCurrentAnim(false);
		base.StartCoroutine(this.DoDisenchantAnims());
		CraftingManager.Get().StartCoroutine(this.StartCraftCooldown());
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x0006D598 File Offset: 0x0006B798
	public void CleanUpEffects()
	{
		if (this.m_explodingActor != null)
		{
			Spell spell = this.m_explodingActor.GetSpell(SpellType.DECONSTRUCT);
			if (spell != null && spell.GetActiveState() != SpellStateType.NONE)
			{
				this.m_explodingActor.GetSpell(SpellType.DECONSTRUCT).GetComponent<PlayMakerFSM>().SendEvent("Cancel");
				this.m_explodingActor.Hide();
			}
		}
		if (this.m_constructingActor != null)
		{
			Spell spell2 = this.m_constructingActor.GetSpell(SpellType.CONSTRUCT);
			if (spell2 != null && spell2.GetActiveState() != SpellStateType.NONE)
			{
				this.m_constructingActor.GetSpell(SpellType.CONSTRUCT).GetComponent<PlayMakerFSM>().SendEvent("Cancel");
				this.m_constructingActor.Hide();
			}
		}
		base.GetComponent<PlayMakerFSM>().SendEvent("Cancel");
		this.m_isAnimating = false;
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x0006D664 File Offset: 0x0006B864
	public void DoCreate()
	{
		EntityDef entityDef;
		TAG_PREMIUM tag_PREMIUM;
		if (!CraftingManager.Get().GetShownCardInfo(out entityDef, out tag_PREMIUM))
		{
			return;
		}
		int numOwnedIncludePending = CraftingManager.Get().GetNumOwnedIncludePending();
		int num = entityDef.IsElite() ? 1 : 2;
		if (numOwnedIncludePending >= num)
		{
			return;
		}
		this.UpdateTips();
		CraftingManager.Get().AdjustUnCommitedArcaneDustChanges(-this.GetCardBuyValue(CraftingManager.Get().GetShownActor().GetEntityDef().GetCardId(), CraftingManager.Get().GetShownActor().GetPremium()));
		if (!Options.Get().GetBool(Option.HAS_CRAFTED))
		{
			Options.Get().SetBool(Option.HAS_CRAFTED, true);
		}
		CraftingManager.Get().NotifyOfTransaction(1);
		if (CraftingManager.Get().GetNumOwnedIncludePending() > 1)
		{
			CraftingManager.Get().ForceNonGhostFlagOn();
		}
		this.UpdateCraftingButtonsAndSoulboundText();
		this.StopCurrentAnim(false);
		base.StartCoroutine(this.DoCreateAnims());
		CraftingManager.Get().StartCoroutine(this.StartDisenchantCooldown());
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x0006D748 File Offset: 0x0006B948
	public void UpdateBankText()
	{
		long arcaneDustBalance = NetCache.Get().GetArcaneDustBalance();
		this.m_bankAmountText.Text = arcaneDustBalance.ToString();
		BnetBar.Get().RefreshCurrency();
		if (UniversalInputManager.UsePhoneUI && CraftingTray.Get() != null)
		{
			ArcaneDustAmount.Get().UpdateCurrentDustAmount();
		}
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x0006D7A0 File Offset: 0x0006B9A0
	public void Disable(Vector3 hidePosition)
	{
		this.m_enabled = false;
		iTween.MoveTo(this.m_activeObject, iTween.Hash(new object[]
		{
			"time",
			0.4f,
			"position",
			hidePosition
		}));
		this.HideTips();
		this.StopCurrentAnim(false);
	}

	// Token: 0x06001316 RID: 4886 RVA: 0x0006D7FD File Offset: 0x0006B9FD
	public bool IsEnabled()
	{
		return this.m_enabled;
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x0006D808 File Offset: 0x0006BA08
	public void Enable(Vector3 showPosition, Vector3 hidePosition)
	{
		if (!this.m_initializedPositions)
		{
			base.transform.position = hidePosition;
			this.m_soulboundNotification.transform.position = base.transform.position;
			this.m_soulboundTitle.Text = GameStrings.Get("GLUE_CRAFTING_SOULBOUND");
			this.m_soulboundDesc.Text = GameStrings.Get("GLUE_CRAFTING_SOULBOUND_DESC");
			this.m_activeObject = base.gameObject;
			this.m_initializedPositions = true;
		}
		this.m_enabled = true;
		this.UpdateCraftingButtonsAndSoulboundText();
		this.UpdateWildTheming();
		this.m_activeObject.SetActive(true);
		iTween.MoveTo(this.m_activeObject, iTween.Hash(new object[]
		{
			"time",
			0.5f,
			"position",
			showPosition
		}));
		this.ShowFirstTimeTips();
	}

	// Token: 0x06001318 RID: 4888 RVA: 0x0006D8E2 File Offset: 0x0006BAE2
	public void SetStartingActive()
	{
		this.m_soulboundNotification.SetActive(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001319 RID: 4889 RVA: 0x0006D8FC File Offset: 0x0006BAFC
	private void ShowFirstTimeTips()
	{
		if (this.m_activeObject == this.m_soulboundNotification)
		{
			return;
		}
		if (Options.Get().GetBool(Option.HAS_CRAFTED) || !UserAttentionManager.CanShowAttentionGrabber("CraftingUI.ShowFirstTimeTips"))
		{
			return;
		}
		this.CreateDisenchantNotification();
		this.CreateCraftNotification();
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x0006D93C File Offset: 0x0006BB3C
	private void CreateDisenchantNotification()
	{
		this.m_buttonDisenchant.IsButtonEnabled();
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x0006D94C File Offset: 0x0006BB4C
	private void CreateCraftNotification()
	{
		if (!this.m_buttonCreate.IsButtonEnabled())
		{
			return;
		}
		Vector3 position;
		Notification.PopUpArrowDirection direction;
		if (UniversalInputManager.UsePhoneUI)
		{
			position = new Vector3(73.3f, 1f, 55.4f);
			direction = Notification.PopUpArrowDirection.Down;
		}
		else
		{
			position = new Vector3(55f, 1f, -56f);
			direction = Notification.PopUpArrowDirection.Left;
		}
		if (this.m_craftNotification == null)
		{
			this.m_craftNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, 16f * Vector3.one, GameStrings.Get("GLUE_COLLECTION_TUTORIAL06"), false, NotificationManager.PopupTextType.BASIC);
		}
		if (this.m_craftNotification != null)
		{
			this.m_craftNotification.ShowPopUpArrow(direction);
		}
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x0006D9FC File Offset: 0x0006BBFC
	private void UpdateTips()
	{
		if (Options.Get().GetBool(Option.HAS_CRAFTED) || !UserAttentionManager.CanShowAttentionGrabber("CraftingUI.UpdateTips"))
		{
			this.HideTips();
			return;
		}
		if (this.m_craftNotification == null)
		{
			this.CreateCraftNotification();
			return;
		}
		if (!this.m_buttonCreate.IsButtonEnabled())
		{
			NotificationManager.Get().DestroyNotification(this.m_craftNotification, 0f);
		}
	}

	// Token: 0x0600131D RID: 4893 RVA: 0x0006DA64 File Offset: 0x0006BC64
	private void HideTips()
	{
		if (this.m_craftNotification != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.m_craftNotification);
		}
	}

	// Token: 0x0600131E RID: 4894 RVA: 0x0006DA84 File Offset: 0x0006BC84
	private void NotifyOfMouseOver()
	{
		if (this.m_mousedOver)
		{
			return;
		}
		this.m_mousedOver = true;
		base.GetComponent<PlayMakerFSM>().SendEvent("Idle");
	}

	// Token: 0x0600131F RID: 4895 RVA: 0x0006DAA6 File Offset: 0x0006BCA6
	private void NotifyOfMouseOut()
	{
		if (!this.m_mousedOver)
		{
			return;
		}
		this.m_mousedOver = false;
		base.GetComponent<PlayMakerFSM>().SendEvent("IdleCancel");
	}

	// Token: 0x06001320 RID: 4896 RVA: 0x0006DAC8 File Offset: 0x0006BCC8
	private int GetCardBuyValue(string cardID, TAG_PREMIUM premium)
	{
		NetCache.CardValue cardValue = CraftingManager.Get().GetCardValue(cardID, premium);
		if (CraftingManager.Get().GetNumClientTransactions() >= 0)
		{
			return cardValue.GetBuyValue();
		}
		return cardValue.GetSellValue();
	}

	// Token: 0x06001321 RID: 4897 RVA: 0x0006DAFC File Offset: 0x0006BCFC
	private int GetCardSellValue(string cardID, TAG_PREMIUM premium)
	{
		NetCache.CardValue cardValue = CraftingManager.Get().GetCardValue(cardID, premium);
		if (CraftingManager.Get().GetNumClientTransactions() <= 0)
		{
			return cardValue.GetSellValue();
		}
		return cardValue.GetBuyValue();
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x0006DB30 File Offset: 0x0006BD30
	public static bool IsCraftingEventForCardActive(string cardID, TAG_PREMIUM premium, out bool willBecomeActiveInFuture)
	{
		willBecomeActiveInFuture = false;
		if (GameUtils.IsClassicCard(cardID))
		{
			return CraftingUI.IsCraftingEventForCardActive(GameUtils.TranslateDbIdToCardId(GameUtils.GetCardTagValue(cardID, GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID), false), premium, out willBecomeActiveInFuture);
		}
		CardDbfRecord cardRecord = GameUtils.GetCardRecord(cardID);
		if (cardRecord == null)
		{
			Debug.LogWarning(string.Format("CraftingUI.IsCraftingEventForCardActive could not find DBF record for card {0}, assuming it cannot be crafted or disenchanted", cardID));
			return false;
		}
		string eventName = cardRecord.CraftingEvent;
		if (premium == TAG_PREMIUM.GOLDEN && !string.IsNullOrEmpty(cardRecord.GoldenCraftingEvent))
		{
			eventName = cardRecord.GoldenCraftingEvent;
		}
		bool flag = SpecialEventManager.Get().IsEventActive(eventName, true);
		if (!flag)
		{
			willBecomeActiveInFuture = SpecialEventManager.Get().IsStartTimeInTheFuture(eventName);
		}
		return flag;
	}

	// Token: 0x06001323 RID: 4899 RVA: 0x0006DBB8 File Offset: 0x0006BDB8
	private void StopCurrentAnim(bool forceCleanup = false)
	{
		if (!this.m_isAnimating && !forceCleanup)
		{
			return;
		}
		base.StopAllCoroutines();
		this.CleanUpEffects();
		foreach (GameObject gameObject in this.m_thingsToDestroy)
		{
			if (!(gameObject == null))
			{
				Log.Crafting.Print("StopCurrentAnim: Destroying GameObject {0}", new object[]
				{
					gameObject
				});
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	// Token: 0x06001324 RID: 4900 RVA: 0x0006DC44 File Offset: 0x0006BE44
	private IEnumerator StartDisenchantCooldown()
	{
		if (!this.m_buttonDisenchant.GetComponent<Collider>().enabled)
		{
			yield break;
		}
		this.m_buttonDisenchant.GetComponent<Collider>().enabled = false;
		yield return new WaitForSeconds(1f);
		this.m_buttonDisenchant.GetComponent<Collider>().enabled = true;
		yield break;
	}

	// Token: 0x06001325 RID: 4901 RVA: 0x0006DC53 File Offset: 0x0006BE53
	private IEnumerator StartCraftCooldown()
	{
		if (!this.m_buttonCreate.GetComponent<Collider>().enabled)
		{
			yield break;
		}
		this.m_buttonCreate.GetComponent<Collider>().enabled = false;
		yield return new WaitForSeconds(1f);
		this.m_buttonCreate.GetComponent<Collider>().enabled = true;
		yield break;
	}

	// Token: 0x06001326 RID: 4902 RVA: 0x0006DC62 File Offset: 0x0006BE62
	private IEnumerator DoDisenchantAnims()
	{
		SoundManager.Get().Play(this.m_disenchantSound.GetComponent<AudioSource>(), null, null, null);
		SoundManager.Get().Stop(this.m_craftingSound.GetComponent<AudioSource>());
		this.m_isAnimating = true;
		PlayMakerFSM playmaker = base.GetComponent<PlayMakerFSM>();
		playmaker.SendEvent("Birth");
		yield return new WaitForSeconds(this.m_disenchantDelayBeforeCardExplodes);
		while (CraftingManager.Get().GetShownActor() == null)
		{
			yield return null;
		}
		this.m_explodingActor = CraftingManager.Get().GetShownActor();
		Actor oldActor = this.m_explodingActor;
		this.m_thingsToDestroy.Add(this.m_explodingActor.gameObject);
		Log.Crafting.Print("Adding {0} to thingsToDestroy", new object[]
		{
			this.m_explodingActor.gameObject
		});
		this.UpdateBankText();
		if (CraftingManager.Get().IsCancelling())
		{
			yield break;
		}
		CraftingManager.Get().LoadGhostActorIfNecessary();
		this.m_explodingActor.ActivateSpellBirthState(SpellType.DECONSTRUCT);
		yield return new WaitForSeconds(this.m_disenchantDelayBeforeCardFlips);
		if (CraftingManager.Get().IsCancelling())
		{
			yield break;
		}
		CraftingManager.Get().FlipUpsideDownCard(this.m_explodingActor);
		yield return new WaitForSeconds(this.m_disenchantDelayBeforeBallsComeOut);
		if (CraftingManager.Get().IsCancelling())
		{
			yield break;
		}
		playmaker.SendEvent("Action");
		yield return new WaitForSeconds(1f);
		this.m_isAnimating = false;
		yield return new WaitForSeconds(10f);
		if (oldActor != null)
		{
			UnityEngine.Object.Destroy(oldActor.gameObject);
		}
		yield break;
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x0006DC71 File Offset: 0x0006BE71
	private IEnumerator DoCreateAnims()
	{
		Actor shownActor = CraftingManager.Get().GetShownActor();
		SoundManager.Get().Play(this.m_craftingSound.GetComponent<AudioSource>(), null, null, null);
		SoundManager.Get().Stop(this.m_disenchantSound.GetComponent<AudioSource>());
		this.m_isAnimating = true;
		CraftingManager.Get().FlipCurrentActor();
		base.GetComponent<PlayMakerFSM>().SendEvent("Birth");
		yield return new WaitForSeconds(this.m_craftDelayBeforeConstructSpell);
		if (CraftingManager.Get().IsCancelling())
		{
			yield break;
		}
		this.m_constructingActor = CraftingManager.Get().LoadNewActorAndConstructIt();
		this.UpdateBankText();
		yield return new WaitForSeconds(this.m_craftDelayBeforeGhostDeath);
		if (CraftingManager.Get().IsCancelling())
		{
			yield break;
		}
		if (shownActor.HasCardDef && shownActor.PlayEffectDef != null)
		{
			GameUtils.PlayCardEffectDefSounds(shownActor.PlayEffectDef);
		}
		CraftingManager.Get().FinishCreateAnims();
		yield return new WaitForSeconds(1f);
		this.m_isAnimating = false;
		yield break;
	}

	// Token: 0x04000C50 RID: 3152
	public UberText m_bankAmountText;

	// Token: 0x04000C51 RID: 3153
	public CreateButton m_buttonCreate;

	// Token: 0x04000C52 RID: 3154
	public DisenchantButton m_buttonDisenchant;

	// Token: 0x04000C53 RID: 3155
	public GameObject m_soulboundNotification;

	// Token: 0x04000C54 RID: 3156
	public UberText m_soulboundTitle;

	// Token: 0x04000C55 RID: 3157
	public UberText m_soulboundDesc;

	// Token: 0x04000C56 RID: 3158
	public UberText m_disenchantValue;

	// Token: 0x04000C57 RID: 3159
	public UberText m_craftValue;

	// Token: 0x04000C58 RID: 3160
	public GameObject m_wildTheming;

	// Token: 0x04000C59 RID: 3161
	public float m_disenchantDelayBeforeCardExplodes;

	// Token: 0x04000C5A RID: 3162
	public float m_disenchantDelayBeforeCardFlips;

	// Token: 0x04000C5B RID: 3163
	public float m_disenchantDelayBeforeBallsComeOut;

	// Token: 0x04000C5C RID: 3164
	public float m_craftDelayBeforeConstructSpell;

	// Token: 0x04000C5D RID: 3165
	public float m_craftDelayBeforeGhostDeath;

	// Token: 0x04000C5E RID: 3166
	public GameObject m_glowballs;

	// Token: 0x04000C5F RID: 3167
	public SoundDef m_craftingSound;

	// Token: 0x04000C60 RID: 3168
	public SoundDef m_disenchantSound;

	// Token: 0x04000C61 RID: 3169
	public Collider m_mouseOverCollider;

	// Token: 0x04000C62 RID: 3170
	private Actor m_explodingActor;

	// Token: 0x04000C63 RID: 3171
	private Actor m_constructingActor;

	// Token: 0x04000C64 RID: 3172
	private bool m_isAnimating;

	// Token: 0x04000C65 RID: 3173
	private List<GameObject> m_thingsToDestroy = new List<GameObject>();

	// Token: 0x04000C66 RID: 3174
	private GameObject m_activeObject;

	// Token: 0x04000C67 RID: 3175
	private bool m_enabled;

	// Token: 0x04000C68 RID: 3176
	private bool m_mousedOver;

	// Token: 0x04000C69 RID: 3177
	private Notification m_craftNotification;

	// Token: 0x04000C6A RID: 3178
	private bool m_initializedPositions;
}
