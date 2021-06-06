using System.Collections.Generic;
using UnityEngine;

public class TutorialKeywordManager : MonoBehaviour
{
	public TutorialKeywordTooltip m_keywordPanelPrefab;

	private static TutorialKeywordManager s_instance;

	private List<TutorialKeywordTooltip> m_keywordPanels;

	private Actor m_actor;

	private Card m_card;

	private void Awake()
	{
		s_instance = this;
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static TutorialKeywordManager Get()
	{
		return s_instance;
	}

	public void UpdateKeywordHelp(Card c, Actor a)
	{
		UpdateKeywordHelp(c, a, showOnRight: true);
	}

	public void UpdateKeywordHelp(Card card, Actor actor, bool showOnRight, float? overrideScale = null)
	{
		m_card = card;
		UpdateKeywordHelp(card.GetEntity(), actor, showOnRight, overrideScale);
	}

	public void UpdateKeywordHelp(Entity entity, Actor actor, bool showOnRight, float? overrideScale = null)
	{
		float num = 1f;
		if (overrideScale.HasValue)
		{
			num = overrideScale.Value;
		}
		PrepareToUpdateKeywordHelp(actor);
		string[] array = GameState.Get().GetGameEntity().NotifyOfKeywordHelpPanelDisplay(entity);
		if (array != null)
		{
			SetupKeywordPanel(array[0], array[1]);
		}
		SetUpPanels(entity);
		TutorialKeywordTooltip tutorialKeywordTooltip = null;
		float num2 = 0f;
		float num3 = 0f;
		for (int i = 0; i < m_keywordPanels.Count; i++)
		{
			TutorialKeywordTooltip tutorialKeywordTooltip2 = m_keywordPanels[i];
			num2 = 1.05f;
			if (entity.IsHero())
			{
				num2 = 1.2f;
			}
			else if (entity.GetZone() == TAG_ZONE.PLAY)
			{
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					num = 1.7f;
				}
				num2 = 1.45f * num;
			}
			tutorialKeywordTooltip2.transform.localScale = new Vector3(num, num, num);
			num3 = -0.2f * m_actor.GetMeshRenderer().bounds.size.z;
			if ((bool)UniversalInputManager.UsePhoneUI && entity.GetZone() == TAG_ZONE.PLAY)
			{
				num3 += 1.5f;
			}
			if (i == 0)
			{
				if (showOnRight)
				{
					tutorialKeywordTooltip2.transform.position = m_actor.transform.position + new Vector3(m_actor.GetMeshRenderer().bounds.size.x * num2, 0f, m_actor.GetMeshRenderer().bounds.extents.z + num3);
				}
				else
				{
					tutorialKeywordTooltip2.transform.position = m_actor.transform.position + new Vector3((0f - m_actor.GetMeshRenderer().bounds.size.x) * num2, 0f, m_actor.GetMeshRenderer().bounds.extents.z + num3);
				}
			}
			else
			{
				tutorialKeywordTooltip2.transform.position = tutorialKeywordTooltip.transform.position - new Vector3(0f, 0f, tutorialKeywordTooltip.GetHeight() * 0.35f + tutorialKeywordTooltip2.GetHeight() * 0.35f);
			}
			tutorialKeywordTooltip = tutorialKeywordTooltip2;
		}
		GameState.Get().GetGameEntity().NotifyOfHelpPanelDisplay(m_keywordPanels.Count);
	}

	private void PrepareToUpdateKeywordHelp(Actor actor)
	{
		HideKeywordHelp();
		m_actor = actor;
		m_keywordPanels = new List<TutorialKeywordTooltip>();
	}

	private void SetUpPanels(EntityBase entityInfo)
	{
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.TAUNT);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.STEALTH);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.DIVINE_SHIELD);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.ENRAGED);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.CHARGE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.BATTLECRY);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.FROZEN);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.FREEZE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.WINDFURY);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SECRET);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.DEATHRATTLE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.OVERLOAD);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.COMBO);
	}

	private bool SetupKeywordPanelIfNecessary(EntityBase entityInfo, GAME_TAG tag)
	{
		if (entityInfo.HasTag(tag))
		{
			SetupKeywordPanel(tag);
			return true;
		}
		if (entityInfo.HasReferencedTag(tag))
		{
			SetupKeywordRefPanel(tag);
			return true;
		}
		return false;
	}

	public void SetupKeywordPanel(GAME_TAG tag)
	{
		string keywordName = GameStrings.GetKeywordName(tag);
		string keywordText = GameStrings.GetKeywordText(tag);
		SetupKeywordPanel(keywordName, keywordText);
	}

	public void SetupKeywordRefPanel(GAME_TAG tag)
	{
		string keywordName = GameStrings.GetKeywordName(tag);
		string refKeywordText = GameStrings.GetRefKeywordText(tag);
		SetupKeywordPanel(keywordName, refKeywordText);
	}

	public void SetupKeywordPanel(string headline, string description)
	{
		TutorialKeywordTooltip component = Object.Instantiate(m_keywordPanelPrefab.gameObject).GetComponent<TutorialKeywordTooltip>();
		if (!(component == null))
		{
			component.Initialize(headline, description);
			m_keywordPanels.Add(component);
		}
	}

	public void HideKeywordHelp()
	{
		if (m_keywordPanels == null)
		{
			return;
		}
		foreach (TutorialKeywordTooltip keywordPanel in m_keywordPanels)
		{
			if (!(keywordPanel == null))
			{
				Object.Destroy(keywordPanel.gameObject);
			}
		}
	}

	public Card GetCard()
	{
		return m_card;
	}

	public Vector3 GetPositionOfTopPanel()
	{
		if (m_keywordPanels == null || m_keywordPanels.Count == 0)
		{
			return new Vector3(0f, 0f, 0f);
		}
		return m_keywordPanels[0].transform.position;
	}
}
