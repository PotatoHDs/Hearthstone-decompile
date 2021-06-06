using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipPanelManager : MonoBehaviour
{
	public enum Orientation
	{
		RightTop,
		RightBottom,
		LeftMiddle
	}

	public TooltipPanel m_tooltipPanelPrefab;

	private static TooltipPanelManager s_instance;

	private Pool<TooltipPanel> m_tooltipPanelPool = new Pool<TooltipPanel>();

	private List<TooltipPanel> m_tooltipPanels = new List<TooltipPanel>();

	private Actor m_actor;

	private Card m_card;

	private float scaleToUse;

	private const float FADE_IN_TIME = 0.125f;

	private const float DELAY_BEFORE_FADE_IN = 0.4f;

	private static readonly GAME_TAG[] spellpowerTags = new GAME_TAG[9]
	{
		GAME_TAG.SPELLPOWER,
		GAME_TAG.SPELLPOWER_ARCANE,
		GAME_TAG.SPELLPOWER_FIRE,
		GAME_TAG.SPELLPOWER_FROST,
		GAME_TAG.SPELLPOWER_NATURE,
		GAME_TAG.SPELLPOWER_HOLY,
		GAME_TAG.SPELLPOWER_SHADOW,
		GAME_TAG.SPELLPOWER_FEL,
		GAME_TAG.SPELLPOWER_PHYSICAL
	};

	private void Awake()
	{
		s_instance = this;
		scaleToUse = TooltipPanel.GAMEPLAY_SCALE;
		m_tooltipPanelPool.SetCreateItemCallback(CreateKeywordPanel);
		m_tooltipPanelPool.SetDestroyItemCallback(DestroyKeywordPanel);
		m_tooltipPanelPool.SetExtensionCount(1);
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().RegisterSceneUnloadedEvent(OnSceneUnloaded);
		}
	}

	private void OnDestroy()
	{
		m_tooltipPanelPool.ReleaseAll();
		m_tooltipPanelPool.Clear();
		s_instance = null;
	}

	public static TooltipPanelManager Get()
	{
		return s_instance;
	}

	public void UpdateKeywordPanelsPosition(Card card, bool showOnRight)
	{
		Actor actor = card.GetActor();
		if (!(actor == null) && !(actor.GetMeshRenderer() == null))
		{
			bool inHand = card.GetZone() is ZoneHand;
			bool isHeroPower = card.GetEntity() != null && card.GetEntity().IsHeroPower();
			StartCoroutine(PositionPanelsForGame(actor.GetMeshRenderer().gameObject, showOnRight, inHand, isHeroPower));
		}
	}

	public void UpdateKeywordHelp(Card card, Actor actor, bool showOnRight = true, float? overrideScale = null, Vector3? overrideOffset = null)
	{
		m_card = card;
		UpdateKeywordHelp(card.GetEntity(), actor, showOnRight, overrideScale, overrideOffset);
	}

	private EntityBase GetDesiredEntityBaseForEntity(Entity entity)
	{
		if (entity == null)
		{
			return null;
		}
		int num = entity.GetTag(GAME_TAG.ALTERNATE_MOUSE_OVER_CARD);
		if (num != 0)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(num);
			if (entityDef != null)
			{
				return entityDef;
			}
			Log.Gameplay.PrintError("TooltipPanelManager.GetDesiredEntityBaseForEntity(): Unable to load EntityDef for card ID {0}.", num);
		}
		return entity;
	}

	public void UpdateKeywordHelp(Entity entity, Actor actor, bool showOnRight, float? overrideScale = null, Vector3? overrideOffset = null)
	{
		m_card = entity.GetCard();
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.SHOW_CRAZY_KEYWORD_TOOLTIP))
		{
			if (TutorialKeywordManager.Get() != null)
			{
				TutorialKeywordManager.Get().UpdateKeywordHelp(entity, actor, showOnRight, overrideScale);
			}
			return;
		}
		bool flag = m_card.GetZone() is ZoneHand;
		bool isHeroPower = entity.IsHeroPower();
		if (overrideScale.HasValue)
		{
			scaleToUse = overrideScale.Value;
		}
		else if (flag)
		{
			scaleToUse = TooltipPanel.HAND_SCALE;
		}
		else
		{
			scaleToUse = TooltipPanel.GAMEPLAY_SCALE;
		}
		PrepareToUpdateKeywordHelp(actor);
		string[] array = GameState.Get().GetGameEntity().NotifyOfKeywordHelpPanelDisplay(entity);
		if (array != null)
		{
			SetupTooltipPanel(array[0], array[1]);
		}
		SetUpPanels(GetDesiredEntityBaseForEntity(entity));
		StartCoroutine(PositionPanelsForGame(actor.GetMeshRenderer().gameObject, showOnRight, flag, isHeroPower, overrideOffset));
		GameState.Get().GetGameEntity().NotifyOfHelpPanelDisplay(m_tooltipPanels.Count);
	}

	private IEnumerator PositionPanelsForGame(GameObject actorObject, bool showOnRight, bool inHand, bool isHeroPower, Vector3? overrideOffset = null)
	{
		TooltipPanel prevPanel = null;
		for (int i = 0; i < m_tooltipPanels.Count; i++)
		{
			TooltipPanel curPanel = m_tooltipPanels[i];
			while (curPanel != null && !curPanel.Destroyed && !curPanel.IsTextRendered())
			{
				yield return null;
			}
			if (curPanel == null || curPanel.gameObject == null || actorObject == null || curPanel.Destroyed)
			{
				break;
			}
			if (i == 0)
			{
				if (overrideOffset.HasValue)
				{
					if (showOnRight)
					{
						TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0f, 0f, 1f), actorObject, new Vector3(1f, 0f, 1f), overrideOffset.Value);
					}
					else
					{
						TransformUtil.SetPoint(curPanel.gameObject, new Vector3(1f, 0f, 1f), actorObject, new Vector3(0f, 0f, 1f), overrideOffset.Value);
					}
				}
				else if (inHand)
				{
					if (showOnRight)
					{
						TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0f, 0f, 1f), actorObject, new Vector3(1f, 0f, 1f), Vector3.zero);
					}
					else
					{
						TransformUtil.SetPoint(curPanel.gameObject, new Vector3(1f, 0f, 1f), actorObject, new Vector3(0f, 0f, 1f), new Vector3(-0.15f, 0f, 0f));
					}
				}
				else if (isHeroPower)
				{
					if ((bool)UniversalInputManager.UsePhoneUI)
					{
						TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0f, 0f, 1f), actorObject, new Vector3(1f, 0f, 1f), new Vector3(0.6f, 0f, 0.2f));
					}
					else
					{
						TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0f, 0f, 1f), actorObject, new Vector3(1f, 0f, 1f), new Vector3(0.38f, 0f, -0.05f));
					}
				}
				else if ((bool)UniversalInputManager.UsePhoneUI)
				{
					if (showOnRight)
					{
						TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0f, 0f, 1f), actorObject, new Vector3(1f, 0f, 1f), new Vector3(1.5f, 0f, 2f));
					}
					else
					{
						TransformUtil.SetPoint(curPanel.gameObject, new Vector3(1f, 0f, 1f), actorObject, new Vector3(0f, 0f, 1f), new Vector3(-1.8f, 0f, 2f));
					}
				}
				else if (showOnRight)
				{
					TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0f, 0f, 1f), actorObject, new Vector3(1f, 0f, 1f), new Vector3(0.5f * scaleToUse + 0.15f, 0f, 0.8f));
				}
				else
				{
					TransformUtil.SetPoint(curPanel.gameObject, new Vector3(1f, 0f, 1f), actorObject, new Vector3(0f, 0f, 1f), new Vector3(-0.78f * scaleToUse - 0.15f, 0f, 0.8f));
				}
			}
			else
			{
				TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0f, 0f, 1f), prevPanel.gameObject, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0.17f));
			}
			prevPanel = curPanel;
		}
	}

	public void UpdateKeywordHelpForHistoryCard(Entity entity, Actor actor, UberText createdByText)
	{
		m_card = entity.GetCard();
		scaleToUse = TooltipPanel.HISTORY_SCALE;
		PrepareToUpdateKeywordHelp(actor);
		string[] array = GameState.Get().GetGameEntity().NotifyOfKeywordHelpPanelDisplay(entity);
		if (array != null)
		{
			SetupTooltipPanel(array[0], array[1]);
		}
		SetUpPanels(GetDesiredEntityBaseForEntity(entity));
		StartCoroutine(PositionPanelsForHistory(actor, createdByText));
	}

	private IEnumerator PositionPanelsForHistory(Actor actor, UberText createdByText)
	{
		GameObject firstRelativeAnchor;
		if (createdByText.gameObject.activeSelf)
		{
			firstRelativeAnchor = createdByText.gameObject;
		}
		else
		{
			GameObject historyKeywordBone = actor.FindBone("HistoryKeywordBone");
			if (historyKeywordBone == null)
			{
				Error.AddDevWarning("Missing Bone", "Missing HistoryKeywordBone on {0}", actor);
				yield return null;
			}
			firstRelativeAnchor = historyKeywordBone;
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_tooltipPanels.Clear();
		}
		TooltipPanel prevPanel = null;
		bool showHorizontally = false;
		for (int i = 0; i < m_tooltipPanels.Count; i++)
		{
			TooltipPanel curPanel = m_tooltipPanels[i];
			while (curPanel != null && !curPanel.Destroyed && !curPanel.IsTextRendered())
			{
				yield return null;
			}
			if (curPanel == null || curPanel.Destroyed)
			{
				continue;
			}
			if (i == 0)
			{
				TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0.5f, 0f, 1f), firstRelativeAnchor, new Vector3(0.5f, 0f, 0f));
			}
			else
			{
				float num = prevPanel.GetHeight() * 0.35f + curPanel.GetHeight() * 0.35f;
				if (prevPanel.transform.position.z - num < -8.3f)
				{
					showHorizontally = true;
				}
				if (showHorizontally)
				{
					TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0f, 0f, 1f), prevPanel.gameObject, new Vector3(1f, 0f, 1f), Vector3.zero);
				}
				else
				{
					TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0.5f, 0f, 1f), prevPanel.gameObject, new Vector3(0.5f, 0f, 0f), new Vector3(0f, 0f, 0.06094122f));
				}
			}
			prevPanel = curPanel;
		}
	}

	public void UpdateKeywordHelpForCollectionManager(EntityDef entityDef, Actor actor, Orientation orientation)
	{
		scaleToUse = TooltipPanel.COLLECTION_MANAGER_SCALE;
		PrepareToUpdateKeywordHelp(actor);
		SetUpPanels(entityDef);
		StartCoroutine(PositionPanelsForCM(actor, orientation));
	}

	private IEnumerator PositionPanelsForCM(Actor actor, Orientation orientation = Orientation.RightTop)
	{
		GameObject actorObject = actor.GetMeshRenderer().gameObject;
		TooltipPanel prevPanel = null;
		int maxPanelCount = m_tooltipPanels.Count;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			maxPanelCount = Mathf.Min(m_tooltipPanels.Count, 3);
		}
		Vector3 actorStartAnchor;
		Vector3 panelStartAnchor;
		Vector3 panelEndAnchor;
		switch (orientation)
		{
		case Orientation.LeftMiddle:
			actorStartAnchor = new Vector3(-1f, 0f, 0.5f);
			panelStartAnchor = new Vector3(1f, 0f, 0.4f);
			panelEndAnchor = new Vector3(0f, 0f, 0f);
			break;
		case Orientation.RightBottom:
			actorStartAnchor = new Vector3(1f, 0f, 0f);
			panelStartAnchor = Vector3.zero;
			panelEndAnchor = new Vector3(0f, 0f, 1f);
			break;
		case Orientation.RightTop:
			actorStartAnchor = new Vector3(1f, 0f, 1f);
			panelStartAnchor = new Vector3(0f, 0f, 1f);
			panelEndAnchor = Vector3.zero;
			break;
		default:
			Log.All.PrintError("TooltipPanelManager.PositionPanelsForCM received a bad orientation value: " + orientation);
			actorStartAnchor = Vector3.zero;
			panelStartAnchor = Vector3.zero;
			panelEndAnchor = Vector3.zero;
			break;
		}
		for (int i = 0; i < m_tooltipPanels.Count; i++)
		{
			TooltipPanel panel = m_tooltipPanels[i];
			if (i >= maxPanelCount)
			{
				panel.gameObject.SetActive(value: false);
				continue;
			}
			while (panel != null && !panel.Destroyed && !panel.IsTextRendered())
			{
				yield return null;
			}
			if (panel == null || panel.Destroyed)
			{
				continue;
			}
			if (actor.IsSpellActive(SpellType.GHOSTCARD))
			{
				Spell spell = actor.GetSpell(SpellType.GHOSTCARD);
				if (spell != null)
				{
					RenderToTexture componentInChildren = spell.gameObject.GetComponentInChildren<RenderToTexture>();
					if (componentInChildren != null)
					{
						actorObject = componentInChildren.GetRenderToObject();
					}
				}
			}
			if (i == 0)
			{
				TransformUtil.SetPoint(panel.gameObject, panelStartAnchor, actorObject, actorStartAnchor, Vector3.zero);
				if (actor.isMissingCard())
				{
					RenderToTexture component = actor.m_missingCardEffect.GetComponent<RenderToTexture>();
					if (component != null)
					{
						Log.All.Print("Missing card keyword tooltip offset: " + component.GetOffscreenPositionOffset().ToString());
						panel.gameObject.transform.position -= component.GetOffscreenPositionOffset();
					}
				}
			}
			else
			{
				TransformUtil.SetPoint(panel.gameObject, panelStartAnchor, prevPanel.gameObject, panelEndAnchor, Vector3.zero);
			}
			prevPanel = panel;
		}
	}

	public void UpdateGhostCardHelpForCollectionManager(Actor actor, GhostCard.Type ghostType, Orientation orientation)
	{
		scaleToUse = TooltipPanel.COLLECTION_MANAGER_SCALE;
		PrepareToUpdateGhostCardHelp(actor);
		string text = (UniversalInputManager.Get().IsTouchMode() ? "_TOUCH" : "");
		string headline;
		string description;
		switch (ghostType)
		{
		case GhostCard.Type.NOT_VALID:
			headline = GameStrings.Get("GLUE_GHOST_CARD_NOT_VALID_TITLE");
			description = GameStrings.Get("GLUE_GHOST_CARD_NOT_VALID_DESCRIPTION" + text);
			break;
		case GhostCard.Type.MISSING_UNCRAFTABLE:
		case GhostCard.Type.MISSING:
			headline = GameStrings.Get("GLUE_GHOST_CARD_MISSING_TITLE");
			description = GameStrings.Get("GLUE_GHOST_CARD_MISSING_DESCRIPTION" + text);
			break;
		default:
			return;
		}
		SetupTooltipPanel(headline, description);
		StartCoroutine(PositionPanelsForCM(actor, orientation));
	}

	public void UpdateKeywordHelpForDeckHelper(EntityDef entityDef, Actor actor)
	{
		scaleToUse = 3.75f;
		PrepareToUpdateKeywordHelp(actor);
		SetUpPanels(entityDef);
		StartCoroutine(PositionPanelsForForge(actor.GetMeshRenderer().gameObject));
	}

	public void UpdateKeywordHelpForAdventure(EntityDef entityDef, Actor actor)
	{
		scaleToUse = TooltipPanel.ADVENTURE_SCALE;
		PrepareToUpdateKeywordHelp(actor);
		SetUpPanels(entityDef);
		StartCoroutine(PositionPanelsForForge(actor.GetMeshRenderer().gameObject));
	}

	public void UpdateKeywordHelpForForge(EntityDef entityDef, Actor actor, int cardChoice = 0)
	{
		scaleToUse = TooltipPanel.FORGE_SCALE;
		PrepareToUpdateKeywordHelp(actor);
		SetUpPanels(entityDef);
		StartCoroutine(PositionPanelsForForge(actor.GetMeshRenderer().gameObject, cardChoice));
	}

	private IEnumerator PositionPanelsForForge(GameObject actorObject, int cardChoice = 0)
	{
		TooltipPanel prevPanel = null;
		for (int i = 0; i < m_tooltipPanels.Count; i++)
		{
			TooltipPanel panel = m_tooltipPanels[i];
			while (panel != null && !panel.Destroyed && !panel.IsTextRendered())
			{
				yield return null;
			}
			if (panel == null || panel.Destroyed)
			{
				continue;
			}
			if (i == 0)
			{
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					TransformUtil.SetPoint(panel.gameObject, new Vector3(0f, 0f, 1f), actorObject, (cardChoice == 3) ? new Vector3(0f, 0f, 1f) : new Vector3(1f, 0f, 1f), (cardChoice == 3) ? new Vector3(-31f, 0f, 0f) : Vector3.zero);
				}
				else
				{
					TransformUtil.SetPoint(panel.gameObject, new Vector3(0f, 0f, 1f), actorObject, new Vector3(1f, 0f, 1f), Vector3.zero);
				}
			}
			else
			{
				TransformUtil.SetPoint(panel.gameObject, new Vector3(0f, 0f, 1f), prevPanel.gameObject, new Vector3(0f, 0f, 0f), Vector3.zero);
			}
			prevPanel = panel;
		}
	}

	public void UpdateKeywordHelpForPackOpening(EntityDef entityDef, Actor actor)
	{
		scaleToUse = 2.75f;
		PrepareToUpdateKeywordHelp(actor);
		SetUpPanels(entityDef);
		StartCoroutine(PositionPanelsForPackOpening(actor.GetMeshRenderer().gameObject));
	}

	private IEnumerator PositionPanelsForPackOpening(GameObject actorObject)
	{
		TooltipPanel prevPanel = null;
		for (int i = 0; i < m_tooltipPanels.Count; i++)
		{
			TooltipPanel panel = m_tooltipPanels[i];
			while (panel != null && !panel.Destroyed && !panel.IsTextRendered())
			{
				yield return null;
			}
			if (!(panel == null) && !panel.Destroyed)
			{
				if (i == 0)
				{
					TransformUtil.SetPoint(panel.gameObject, new Vector3(1f, 0f, 1f), actorObject, new Vector3(0f, 0f, 1f), Vector3.zero);
					panel.transform.position = panel.transform.position - new Vector3(1.2f, 0f, 0f);
				}
				else
				{
					TransformUtil.SetPoint(panel.gameObject, new Vector3(0f, 0f, 1f), prevPanel.gameObject, new Vector3(0f, 0f, 0f), Vector3.zero);
				}
				prevPanel = panel;
			}
		}
	}

	public void UpdateKeywordHelpForMulliganCard(Entity entity, Actor actor)
	{
		m_card = entity.GetCard();
		scaleToUse = TooltipPanel.MULLIGAN_SCALE;
		PrepareToUpdateKeywordHelp(actor);
		string[] array = GameState.Get().GetGameEntity().NotifyOfKeywordHelpPanelDisplay(entity);
		if (array != null)
		{
			SetupTooltipPanel(array[0], array[1]);
		}
		SetUpPanels(GetDesiredEntityBaseForEntity(entity));
		StartCoroutine(PositionPanelsForMulligan(actor.GetMeshRenderer().gameObject));
	}

	private IEnumerator PositionPanelsForMulligan(GameObject actorObject)
	{
		TooltipPanel prevPanel = null;
		bool showHorizontally = false;
		for (int i = 0; i < m_tooltipPanels.Count; i++)
		{
			TooltipPanel curPanel = m_tooltipPanels[i];
			while (curPanel != null && !curPanel.Destroyed && !curPanel.IsTextRendered())
			{
				yield return null;
			}
			if (curPanel == null || curPanel.Destroyed)
			{
				continue;
			}
			if (i == 0)
			{
				TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0.5f, 0f, 1f), actorObject, new Vector3(0.5f, 0f, 0f), new Vector3(-0.112071f, 0f, -0.1244259f));
			}
			else
			{
				float num = prevPanel.GetHeight() * 0.35f + curPanel.GetHeight() * 0.35f;
				if (prevPanel.transform.position.z - num < -8.3f)
				{
					showHorizontally = true;
				}
				if (showHorizontally)
				{
					TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0f, 0f, 1f), prevPanel.gameObject, new Vector3(1f, 0f, 1f), Vector3.zero);
				}
				else
				{
					TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0.5f, 0f, 1f), prevPanel.gameObject, new Vector3(0.5f, 0f, 0f), new Vector3(0f, 0f, 0.1588802f));
				}
			}
			prevPanel = curPanel;
		}
	}

	private void PrepareToUpdateKeywordHelp(Actor actor)
	{
		HideKeywordHelp();
		m_actor = actor;
		m_tooltipPanels.Clear();
	}

	private void PrepareToUpdateGhostCardHelp(Actor actor)
	{
		HideTooltipPanels();
		m_actor = actor;
		m_tooltipPanels.Clear();
	}

	private void SetUpPanels(EntityBase entityInfo)
	{
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SHIFTING);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SHIFTING_MINION);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SHIFTING_WEAPON);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SHIFTING_SPELL);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.FLOOPY);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.BOSS);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.WILD);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.HALL_OF_FAME);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.START_OF_COMBAT);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.EMPOWER);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.TAUNT);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.STEALTH);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.DIVINE_SHIELD);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_ARCANE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_FIRE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_FROST);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_NATURE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_HOLY);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_SHADOW);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_FEL);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_PHYSICAL);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.ENRAGED_TOOLTIP);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.CHARGE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.BATTLECRY);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.FROZEN);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.FREEZE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.WINDFURY);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.MEGA_WINDFURY);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.ECHO);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.RUSH);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.MODULAR);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.OVERKILL);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.PROPHECY);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.ETHEREAL);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.MARK_OF_EVIL);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.WAND);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.TWINSPELL);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.REBORN);
		if (entityInfo.GetZone() != TAG_ZONE.SECRET)
		{
			SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SECRET);
		}
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.DEATHRATTLE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.OVERLOAD);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.COMBO);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SILENCE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.COUNTER);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.IMMUNE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPARE_PART);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.INSPIRE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.DISCOVER);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.CTHUN);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.AUTOATTACK);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.MINION_TYPE_REFERENCE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.JADE_GOLEM);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.GRIMY_GOONS);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.JADE_LOTUS);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.KABAL);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.QUEST);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SIDEQUEST);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.POISONOUS);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.ADAPT);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.LIFESTEAL);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.RECRUIT);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.DUNGEON_PASSIVE_BUFF);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.START_OF_GAME);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.CASTSWHENDRAWN);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SHRINE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.FATIGUEREFERENCE);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.OUTCAST);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.STUDY);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLBURST);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.CORRUPT);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.DORMANT);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.CORRUPTEDCARD);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.FRENZY);
		SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.BLOOD_GEM);
		if (entityInfo.IsHeroPower())
		{
			SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.AI_MUST_PLAY);
		}
	}

	private bool SetupKeywordPanelIfNecessary(EntityBase entityInfo, GAME_TAG tag)
	{
		int num = 0;
		if (entityInfo.HasTag(tag))
		{
			num = entityInfo.GetTag(tag);
		}
		else if (entityInfo.HasCachedTagForDormant(tag))
		{
			num = entityInfo.GetCachedTagForDormant(tag);
		}
		int num2 = 0;
		if (entityInfo.HasReferencedTag(tag))
		{
			num2 = entityInfo.GetReferencedTag(tag);
		}
		if (num == 0 && num2 == 0)
		{
			return false;
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER && GameStrings.HasCollectionKeywordText(tag))
		{
			if (GAME_TAG.EMPOWER == tag)
			{
				if (entityInfo.GetClass() != TAG_CLASS.NEUTRAL)
				{
					tag = GetEmpowerTagByClass(entityInfo.GetClass());
				}
				if (CollectionManager.Get().IsInEditMode())
				{
					CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
					string galakrondCardIdByClass = GameUtils.GetGalakrondCardIdByClass(editedDeck.GetClass());
					if (editedDeck.GetCardIdCount(galakrondCardIdByClass) > 0)
					{
						tag = GetEmpowerTagByClass(editedDeck.GetClass());
					}
				}
			}
			SetupCollectionKeywordPanel(tag);
			return true;
		}
		if (num != 0 && GameStrings.HasKeywordText(tag))
		{
			GAME_TAG[] array = spellpowerTags;
			foreach (GAME_TAG gAME_TAG in array)
			{
				if (tag == gAME_TAG)
				{
					int num3 = entityInfo.GetTag(tag);
					string empty = string.Empty;
					empty = ((num3 <= 0) ? GameStrings.Get(GameStrings.GetRefKeywordTextKey(tag)) : GameStrings.Format(GameStrings.GetKeywordTextKey(tag), num3));
					string keywordName = GameStrings.GetKeywordName(tag);
					SetupTooltipPanel(keywordName, empty);
					return true;
				}
			}
			if (tag == GAME_TAG.WINDFURY && num > 1)
			{
				if (num == 3)
				{
					SetupKeywordPanel(GAME_TAG.MEGA_WINDFURY);
					return true;
				}
				return false;
			}
			switch (tag)
			{
			case GAME_TAG.SHIFTING:
			case GAME_TAG.SHIFTING_MINION:
			case GAME_TAG.SHIFTING_WEAPON:
			case GAME_TAG.SHIFTING_SPELL:
			{
				int num4 = entityInfo.GetTag(GAME_TAG.TRANSFORMED_FROM_CARD);
				if (num4 == 0)
				{
					return false;
				}
				EntityDef entityDef = DefLoader.Get().GetEntityDef(num4);
				string description = GameStrings.Get(GameStrings.GetKeywordTextKey(tag));
				SetupTooltipPanel(entityDef.GetName(), description);
				return true;
			}
			case GAME_TAG.AI_MUST_PLAY:
				if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
				{
					int controllerId = entityInfo.GetControllerId();
					Player player = GameState.Get().GetPlayer(controllerId);
					if (player != null && !player.IsAI())
					{
						return false;
					}
				}
				break;
			}
			if (tag == GAME_TAG.EMPOWER && SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
			{
				int controllerId2 = entityInfo.GetControllerId();
				Player player2 = GameState.Get().GetPlayer(controllerId2);
				if (player2 != null && player2.HasTag(GAME_TAG.PROXY_GALAKROND))
				{
					Entity entity = GameState.Get().GetEntity(player2.GetTag(GAME_TAG.PROXY_GALAKROND));
					tag = GetEmpowerTagByClass(entity.GetClass());
				}
			}
			SetupKeywordPanel(tag);
			return true;
		}
		if (num2 != 0 && GameStrings.HasRefKeywordText(tag))
		{
			SetupKeywordRefPanel(tag);
			return true;
		}
		return false;
	}

	private Vector3 GetPanelPosition(TooltipPanel panel)
	{
		Vector3 result = new Vector3(0f, 0f, 0f);
		TooltipPanel tooltipPanel = null;
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < m_tooltipPanels.Count; i++)
		{
			TooltipPanel tooltipPanel2 = m_tooltipPanels[i];
			num = (m_card.GetEntity().IsHero() ? 1.2f : ((m_card.GetEntity().GetZone() != TAG_ZONE.PLAY) ? 0.85f : 1.05f));
			if (m_actor.GetMeshRenderer() == null)
			{
				return result;
			}
			num2 = -0.2f * m_actor.GetMeshRenderer().bounds.size.z;
			if (tooltipPanel2 == panel)
			{
				result = ((i != 0) ? (tooltipPanel.transform.position - new Vector3(0f, 0f, tooltipPanel.GetHeight() * 0.35f + tooltipPanel2.GetHeight() * 0.35f)) : (m_actor.transform.position + new Vector3(m_actor.GetMeshRenderer().bounds.size.x * num, 0f, m_actor.GetMeshRenderer().bounds.extents.z + num2)));
			}
			tooltipPanel = tooltipPanel2;
		}
		return result;
	}

	private void SetupCollectionKeywordPanel(GAME_TAG tag)
	{
		string keywordName = GameStrings.GetKeywordName(tag);
		string description = GameStrings.Get(GameStrings.GetCollectionKeywordTextKey(tag));
		SetupTooltipPanel(keywordName, description);
	}

	private void SetupKeywordPanel(GAME_TAG tag)
	{
		string keywordName = GameStrings.GetKeywordName(tag);
		string description = GameStrings.Get(GameStrings.GetKeywordTextKey(tag));
		SetupTooltipPanel(keywordName, description);
	}

	private void SetupKeywordRefPanel(GAME_TAG tag)
	{
		string keywordName = GameStrings.GetKeywordName(tag);
		string description = GameStrings.Get(GameStrings.GetRefKeywordTextKey(tag));
		SetupTooltipPanel(keywordName, description);
	}

	private void SetupTooltipPanel(string headline, string description)
	{
		TooltipPanel tooltipPanel = m_tooltipPanelPool.Acquire();
		if (!(tooltipPanel == null))
		{
			tooltipPanel.Reset();
			tooltipPanel.Initialize(headline, description);
			tooltipPanel.SetScale(scaleToUse);
			m_tooltipPanels.Add(tooltipPanel);
			FadeInPanel(tooltipPanel);
		}
	}

	private void FadeInPanel(TooltipPanel helpPanel)
	{
		CleanTweensOnPanel(helpPanel);
		float num = 0.4f;
		if (GameState.Get() != null && GameState.Get().GetBooleanGameOption(GameEntityOption.KEYWORD_HELP_DELAY_OVERRIDDEN))
		{
			num = 0f;
		}
		iTween.ValueTo(base.gameObject, iTween.Hash("onupdatetarget", base.gameObject, "onupdate", "OnUberTextFadeUpdate", "time", 0.125f, "delay", num, "to", 1f, "from", 0f));
	}

	private void OnUberTextFadeUpdate(float newValue)
	{
		foreach (TooltipPanel tooltipPanel in m_tooltipPanels)
		{
			RenderUtils.SetAlpha(tooltipPanel.gameObject, newValue, includeInactive: true);
		}
	}

	private void CleanTweensOnPanel(TooltipPanel helpPanel)
	{
		iTween.Stop(base.gameObject);
		RenderUtils.SetAlpha(helpPanel.gameObject, 0f, includeInactive: true);
	}

	public void ShowKeywordHelp()
	{
		foreach (TooltipPanel tooltipPanel in m_tooltipPanels)
		{
			tooltipPanel.gameObject.SetActive(value: true);
		}
	}

	public void HideKeywordHelp()
	{
		GameState gameState = GameState.Get();
		if (gameState != null && gameState.GetBooleanGameOption(GameEntityOption.SHOW_CRAZY_KEYWORD_TOOLTIP) && TutorialKeywordManager.Get() != null)
		{
			TutorialKeywordManager.Get().HideKeywordHelp();
		}
		HideTooltipPanels();
	}

	public void HideTooltipPanels()
	{
		foreach (TooltipPanel tooltipPanel in m_tooltipPanels)
		{
			if (!(tooltipPanel == null))
			{
				CleanTweensOnPanel(tooltipPanel);
				tooltipPanel.gameObject.SetActive(value: false);
				m_tooltipPanelPool.Release(tooltipPanel);
			}
		}
	}

	public Card GetCard()
	{
		return m_card;
	}

	public Vector3 GetPositionOfTopPanel()
	{
		if (m_tooltipPanels.Count == 0)
		{
			return new Vector3(0f, 0f, 0f);
		}
		return m_tooltipPanels[0].transform.position;
	}

	public TooltipPanel CreateKeywordPanel(int i)
	{
		return Object.Instantiate(m_tooltipPanelPrefab);
	}

	private void DestroyKeywordPanel(TooltipPanel panel)
	{
		if (panel != null)
		{
			Object.Destroy(panel.gameObject);
		}
	}

	private void OnSceneUnloaded(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		foreach (TooltipPanel tooltipPanel in m_tooltipPanels)
		{
			Object.Destroy(tooltipPanel.gameObject);
		}
		m_tooltipPanels.Clear();
		m_tooltipPanelPool.Clear();
		Object.Destroy(m_actor);
		m_actor = null;
		Object.Destroy(m_card);
		m_card = null;
	}

	private GAME_TAG GetEmpowerTagByClass(TAG_CLASS tagClass)
	{
		GAME_TAG result = GAME_TAG.EMPOWER;
		switch (tagClass)
		{
		case TAG_CLASS.PRIEST:
			result = GAME_TAG.EMPOWER_PRIEST;
			break;
		case TAG_CLASS.ROGUE:
			result = GAME_TAG.EMPOWER_ROGUE;
			break;
		case TAG_CLASS.SHAMAN:
			result = GAME_TAG.EMPOWER_SHAMAN;
			break;
		case TAG_CLASS.WARLOCK:
			result = GAME_TAG.EMPOWER_WARLOCK;
			break;
		case TAG_CLASS.WARRIOR:
			result = GAME_TAG.EMPOWER_WARRIOR;
			break;
		}
		return result;
	}
}
