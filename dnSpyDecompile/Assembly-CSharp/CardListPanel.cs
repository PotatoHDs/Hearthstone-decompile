using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AFC RID: 2812
[CustomEditClass]
public class CardListPanel : MonoBehaviour
{
	// Token: 0x17000885 RID: 2181
	// (get) Token: 0x060095C5 RID: 38341 RVA: 0x0030833C File Offset: 0x0030653C
	// (set) Token: 0x060095C6 RID: 38342 RVA: 0x00308344 File Offset: 0x00306544
	[CustomEditField(Sections = "Variables")]
	public float CardSpacing
	{
		get
		{
			return this.m_CardSpacing;
		}
		set
		{
			this.m_CardSpacing = value;
			this.UpdateCardPositions();
		}
	}

	// Token: 0x060095C7 RID: 38343 RVA: 0x00308353 File Offset: 0x00306553
	private void Awake()
	{
		this.m_leftArrowNested.gameObject.SetActive(false);
		this.m_rightArrowNested.gameObject.SetActive(false);
	}

	// Token: 0x060095C8 RID: 38344 RVA: 0x00308377 File Offset: 0x00306577
	public void Show(List<CollectibleCard> cards)
	{
		if (cards != null)
		{
			this.m_cards = cards;
		}
		this.SetupPagingArrows();
		this.m_numPages = (this.m_cards.Count + 3 - 1) / 3;
		this.ShowPage(0);
	}

	// Token: 0x060095C9 RID: 38345 RVA: 0x003083A8 File Offset: 0x003065A8
	private void ShowPage(int pageNum)
	{
		if (pageNum < 0 || pageNum >= this.m_numPages)
		{
			Log.All.PrintWarning(string.Concat(new object[]
			{
				"CardListPanel.ShowPage: attempting to show invalid pageNum=",
				pageNum,
				" numPages=",
				this.m_numPages
			}), Array.Empty<object>());
			return;
		}
		this.m_pageNum = pageNum;
		base.StopCoroutine("TransitionPage");
		base.StartCoroutine("TransitionPage");
	}

	// Token: 0x060095CA RID: 38346 RVA: 0x00308422 File Offset: 0x00306622
	private IEnumerator TransitionPage()
	{
		if (this.m_leftArrow != null)
		{
			this.m_leftArrow.gameObject.SetActive(false);
		}
		if (this.m_rightArrow != null)
		{
			this.m_rightArrow.gameObject.SetActive(false);
		}
		List<Spell> list = new List<Spell>();
		foreach (Actor actor in this.m_cardActors)
		{
			UnityEngine.Object.Destroy(actor.gameObject);
		}
		this.m_cardActors.Clear();
		list.Clear();
		int num = this.m_pageNum * 3;
		int num2 = Mathf.Min(3, this.m_cards.Count - num);
		for (int i = 0; i < num2; i++)
		{
			CollectibleCard collectibleCard = this.m_cards[num + i];
			using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(collectibleCard.CardId, null))
			{
				Actor component = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(fullDef.EntityDef, TAG_PREMIUM.NORMAL), AssetLoadingOptions.IgnorePrefabPosition).GetComponent<Actor>();
				component.SetCardDef(fullDef.DisposableCardDef);
				component.SetEntityDef(fullDef.EntityDef);
				GameUtils.SetParent(component, base.gameObject, false);
				SceneUtils.SetLayer(component, base.gameObject.layer);
				List<CardChangeDbfRecord> cardChangeRecords = GameUtils.GetCardChangeRecords(collectibleCard.CardId);
				GameObject gameObject = null;
				switch (collectibleCard.CardType)
				{
				case TAG_CARDTYPE.HERO:
					gameObject = AssetLoader.Get().InstantiatePrefab("Card_Hand_Hero_NerfGlows.prefab:6f101676067a4514f8641429c0592adc", AssetLoadingOptions.None);
					break;
				case TAG_CARDTYPE.MINION:
					gameObject = AssetLoader.Get().InstantiatePrefab("Card_Hand_Ally_NerfGlows.prefab:a693fa02720fcb644b3223d7d75d26eb", AssetLoadingOptions.None);
					break;
				case TAG_CARDTYPE.SPELL:
					gameObject = AssetLoader.Get().InstantiatePrefab("Card_Hand_Ability_NerfGlows.prefab:adb8690f5caa2a84eb9431b8f09664db", AssetLoadingOptions.None);
					break;
				case TAG_CARDTYPE.WEAPON:
					gameObject = AssetLoader.Get().InstantiatePrefab("Card_Hand_Weapon_NerfGlows.prefab:645b0cbf4d3be464a8e4fe447f6a0dee", AssetLoadingOptions.None);
					break;
				}
				if (gameObject != null)
				{
					CardNerfGlows component2 = gameObject.GetComponent<CardNerfGlows>();
					if (component2 != null)
					{
						TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, component.transform);
						SceneUtils.SetLayer(component2, component.gameObject.layer);
						component2.SetGlowsForCard(collectibleCard, cardChangeRecords);
					}
					else
					{
						Debug.LogError("CardListPanel.cs: Nerf Glows GameObject " + gameObject + " does not have a CardNerfGlows script attached.");
					}
				}
				this.m_cardActors.Add(component);
			}
		}
		this.UpdateCardPositions();
		foreach (Actor actor2 in this.m_cardActors)
		{
			list.Add(actor2.ActivateSpellBirthState(SpellType.DEATHREVERSE));
			actor2.ContactShadow(true);
		}
		SoundManager.Get().LoadAndPlay("collection_manager_card_move_invalid_or_click.prefab:777caa6f44f027747a03f3d85bcc897c");
		yield return new WaitForSeconds(0.2f);
		if (this.m_leftArrow != null)
		{
			this.m_leftArrow.gameObject.SetActive(this.m_pageNum != 0);
		}
		if (this.m_rightArrow != null)
		{
			this.m_rightArrow.gameObject.SetActive(this.m_pageNum < this.m_numPages - 1);
		}
		yield break;
	}

	// Token: 0x060095CB RID: 38347 RVA: 0x00308434 File Offset: 0x00306634
	private void UpdateCardPositions()
	{
		int count = this.m_cardActors.Count;
		for (int i = 0; i < count; i++)
		{
			Component component = this.m_cardActors[i];
			Vector3 zero = Vector3.zero;
			float num = ((float)i - (float)(count - 1) / 2f) * this.m_CardSpacing;
			zero.x += num;
			component.transform.localPosition = zero;
		}
	}

	// Token: 0x060095CC RID: 38348 RVA: 0x00308498 File Offset: 0x00306698
	private void SetupPagingArrows()
	{
		if (this.m_cards.Count > 3)
		{
			this.m_leftArrowNested.gameObject.SetActive(true);
			this.m_rightArrowNested.gameObject.SetActive(true);
			GameObject gameObject = this.m_leftArrowNested.PrefabGameObject(false);
			SceneUtils.SetLayer(gameObject, this.m_leftArrowNested.gameObject.layer, null);
			this.m_leftArrow = gameObject.GetComponent<UIBButton>();
			this.m_leftArrow.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.TurnPage(false);
			});
			gameObject = this.m_rightArrowNested.PrefabGameObject(false);
			SceneUtils.SetLayer(gameObject, this.m_rightArrowNested.gameObject.layer, null);
			this.m_rightArrow = gameObject.GetComponent<UIBButton>();
			this.m_rightArrow.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.TurnPage(true);
			});
			HighlightState componentInChildren = this.m_rightArrow.GetComponentInChildren<HighlightState>();
			if (componentInChildren)
			{
				componentInChildren.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
				return;
			}
		}
		else
		{
			this.m_leftArrowNested.gameObject.SetActive(false);
			this.m_rightArrowNested.gameObject.SetActive(false);
		}
	}

	// Token: 0x060095CD RID: 38349 RVA: 0x003085BC File Offset: 0x003067BC
	private void TurnPage(bool right)
	{
		HighlightState componentInChildren = this.m_rightArrow.GetComponentInChildren<HighlightState>();
		if (componentInChildren)
		{
			componentInChildren.ChangeState(ActorStateType.NONE);
		}
		this.ShowPage(this.m_pageNum + (right ? 1 : -1));
	}

	// Token: 0x04007D84 RID: 32132
	[CustomEditField(Sections = "Object Links")]
	public NestedPrefab m_leftArrowNested;

	// Token: 0x04007D85 RID: 32133
	[CustomEditField(Sections = "Object Links")]
	public NestedPrefab m_rightArrowNested;

	// Token: 0x04007D86 RID: 32134
	[SerializeField]
	private float m_CardSpacing = 2.3f;

	// Token: 0x04007D87 RID: 32135
	private UIBButton m_leftArrow;

	// Token: 0x04007D88 RID: 32136
	private UIBButton m_rightArrow;

	// Token: 0x04007D89 RID: 32137
	private const int MAX_CARDS_PER_PAGE = 3;

	// Token: 0x04007D8A RID: 32138
	private int m_numPages = 1;

	// Token: 0x04007D8B RID: 32139
	private int m_pageNum;

	// Token: 0x04007D8C RID: 32140
	private List<CollectibleCard> m_cards = new List<CollectibleCard>();

	// Token: 0x04007D8D RID: 32141
	private List<Actor> m_cardActors = new List<Actor>();
}
