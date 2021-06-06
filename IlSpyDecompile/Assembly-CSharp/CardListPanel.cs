using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class CardListPanel : MonoBehaviour
{
	[CustomEditField(Sections = "Object Links")]
	public NestedPrefab m_leftArrowNested;

	[CustomEditField(Sections = "Object Links")]
	public NestedPrefab m_rightArrowNested;

	[SerializeField]
	private float m_CardSpacing = 2.3f;

	private UIBButton m_leftArrow;

	private UIBButton m_rightArrow;

	private const int MAX_CARDS_PER_PAGE = 3;

	private int m_numPages = 1;

	private int m_pageNum;

	private List<CollectibleCard> m_cards = new List<CollectibleCard>();

	private List<Actor> m_cardActors = new List<Actor>();

	[CustomEditField(Sections = "Variables")]
	public float CardSpacing
	{
		get
		{
			return m_CardSpacing;
		}
		set
		{
			m_CardSpacing = value;
			UpdateCardPositions();
		}
	}

	private void Awake()
	{
		m_leftArrowNested.gameObject.SetActive(value: false);
		m_rightArrowNested.gameObject.SetActive(value: false);
	}

	public void Show(List<CollectibleCard> cards)
	{
		if (cards != null)
		{
			m_cards = cards;
		}
		SetupPagingArrows();
		m_numPages = (m_cards.Count + 3 - 1) / 3;
		ShowPage(0);
	}

	private void ShowPage(int pageNum)
	{
		if (pageNum < 0 || pageNum >= m_numPages)
		{
			Log.All.PrintWarning("CardListPanel.ShowPage: attempting to show invalid pageNum=" + pageNum + " numPages=" + m_numPages);
		}
		else
		{
			m_pageNum = pageNum;
			StopCoroutine("TransitionPage");
			StartCoroutine("TransitionPage");
		}
	}

	private IEnumerator TransitionPage()
	{
		if (m_leftArrow != null)
		{
			m_leftArrow.gameObject.SetActive(value: false);
		}
		if (m_rightArrow != null)
		{
			m_rightArrow.gameObject.SetActive(value: false);
		}
		List<Spell> list = new List<Spell>();
		foreach (Actor cardActor in m_cardActors)
		{
			Object.Destroy(cardActor.gameObject);
		}
		m_cardActors.Clear();
		list.Clear();
		int num = m_pageNum * 3;
		int num2 = Mathf.Min(3, m_cards.Count - num);
		for (int i = 0; i < num2; i++)
		{
			CollectibleCard collectibleCard = m_cards[num + i];
			using DefLoader.DisposableFullDef disposableFullDef = DefLoader.Get().GetFullDef(collectibleCard.CardId);
			Actor component = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(disposableFullDef.EntityDef, TAG_PREMIUM.NORMAL), AssetLoadingOptions.IgnorePrefabPosition).GetComponent<Actor>();
			component.SetCardDef(disposableFullDef.DisposableCardDef);
			component.SetEntityDef(disposableFullDef.EntityDef);
			GameUtils.SetParent(component, base.gameObject);
			SceneUtils.SetLayer(component, base.gameObject.layer);
			List<CardChangeDbfRecord> cardChangeRecords = GameUtils.GetCardChangeRecords(collectibleCard.CardId);
			GameObject gameObject = null;
			switch (collectibleCard.CardType)
			{
			case TAG_CARDTYPE.MINION:
				gameObject = AssetLoader.Get().InstantiatePrefab("Card_Hand_Ally_NerfGlows.prefab:a693fa02720fcb644b3223d7d75d26eb");
				break;
			case TAG_CARDTYPE.SPELL:
				gameObject = AssetLoader.Get().InstantiatePrefab("Card_Hand_Ability_NerfGlows.prefab:adb8690f5caa2a84eb9431b8f09664db");
				break;
			case TAG_CARDTYPE.WEAPON:
				gameObject = AssetLoader.Get().InstantiatePrefab("Card_Hand_Weapon_NerfGlows.prefab:645b0cbf4d3be464a8e4fe447f6a0dee");
				break;
			case TAG_CARDTYPE.HERO:
				gameObject = AssetLoader.Get().InstantiatePrefab("Card_Hand_Hero_NerfGlows.prefab:6f101676067a4514f8641429c0592adc");
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
					Debug.LogError(string.Concat("CardListPanel.cs: Nerf Glows GameObject ", gameObject, " does not have a CardNerfGlows script attached."));
				}
			}
			m_cardActors.Add(component);
		}
		UpdateCardPositions();
		foreach (Actor cardActor2 in m_cardActors)
		{
			list.Add(cardActor2.ActivateSpellBirthState(SpellType.DEATHREVERSE));
			cardActor2.ContactShadow(visible: true);
		}
		SoundManager.Get().LoadAndPlay("collection_manager_card_move_invalid_or_click.prefab:777caa6f44f027747a03f3d85bcc897c");
		yield return new WaitForSeconds(0.2f);
		if (m_leftArrow != null)
		{
			m_leftArrow.gameObject.SetActive(m_pageNum != 0);
		}
		if (m_rightArrow != null)
		{
			m_rightArrow.gameObject.SetActive(m_pageNum < m_numPages - 1);
		}
	}

	private void UpdateCardPositions()
	{
		int count = m_cardActors.Count;
		for (int i = 0; i < count; i++)
		{
			Actor actor = m_cardActors[i];
			Vector3 zero = Vector3.zero;
			float num = ((float)i - (float)(count - 1) / 2f) * m_CardSpacing;
			zero.x += num;
			actor.transform.localPosition = zero;
		}
	}

	private void SetupPagingArrows()
	{
		if (m_cards.Count > 3)
		{
			m_leftArrowNested.gameObject.SetActive(value: true);
			m_rightArrowNested.gameObject.SetActive(value: true);
			GameObject gameObject = m_leftArrowNested.PrefabGameObject();
			SceneUtils.SetLayer(gameObject, m_leftArrowNested.gameObject.layer);
			m_leftArrow = gameObject.GetComponent<UIBButton>();
			m_leftArrow.AddEventListener(UIEventType.RELEASE, delegate
			{
				TurnPage(right: false);
			});
			gameObject = m_rightArrowNested.PrefabGameObject();
			SceneUtils.SetLayer(gameObject, m_rightArrowNested.gameObject.layer);
			m_rightArrow = gameObject.GetComponent<UIBButton>();
			m_rightArrow.AddEventListener(UIEventType.RELEASE, delegate
			{
				TurnPage(right: true);
			});
			HighlightState componentInChildren = m_rightArrow.GetComponentInChildren<HighlightState>();
			if ((bool)componentInChildren)
			{
				componentInChildren.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
		}
		else
		{
			m_leftArrowNested.gameObject.SetActive(value: false);
			m_rightArrowNested.gameObject.SetActive(value: false);
		}
	}

	private void TurnPage(bool right)
	{
		HighlightState componentInChildren = m_rightArrow.GetComponentInChildren<HighlightState>();
		if ((bool)componentInChildren)
		{
			componentInChildren.ChangeState(ActorStateType.NONE);
		}
		ShowPage(m_pageNum + (right ? 1 : (-1)));
	}
}
