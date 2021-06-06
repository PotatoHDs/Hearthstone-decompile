using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200034C RID: 844
public class TooltipPanelManager : MonoBehaviour
{
	// Token: 0x060030EB RID: 12523 RVA: 0x000FBB48 File Offset: 0x000F9D48
	private void Awake()
	{
		TooltipPanelManager.s_instance = this;
		this.scaleToUse = TooltipPanel.GAMEPLAY_SCALE;
		this.m_tooltipPanelPool.SetCreateItemCallback(new Pool<TooltipPanel>.CreateItemCallback(this.CreateKeywordPanel));
		this.m_tooltipPanelPool.SetDestroyItemCallback(new Pool<TooltipPanel>.DestroyItemCallback(this.DestroyKeywordPanel));
		this.m_tooltipPanelPool.SetExtensionCount(1);
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().RegisterSceneUnloadedEvent(new SceneMgr.SceneUnloadedCallback(this.OnSceneUnloaded));
		}
	}

	// Token: 0x060030EC RID: 12524 RVA: 0x000FBBC2 File Offset: 0x000F9DC2
	private void OnDestroy()
	{
		this.m_tooltipPanelPool.ReleaseAll();
		this.m_tooltipPanelPool.Clear();
		TooltipPanelManager.s_instance = null;
	}

	// Token: 0x060030ED RID: 12525 RVA: 0x000FBBE1 File Offset: 0x000F9DE1
	public static TooltipPanelManager Get()
	{
		return TooltipPanelManager.s_instance;
	}

	// Token: 0x060030EE RID: 12526 RVA: 0x000FBBE8 File Offset: 0x000F9DE8
	public void UpdateKeywordPanelsPosition(Card card, bool showOnRight)
	{
		Actor actor = card.GetActor();
		if (actor == null || actor.GetMeshRenderer(false) == null)
		{
			return;
		}
		bool inHand = card.GetZone() is ZoneHand;
		bool isHeroPower = card.GetEntity() != null && card.GetEntity().IsHeroPower();
		base.StartCoroutine(this.PositionPanelsForGame(actor.GetMeshRenderer(false).gameObject, showOnRight, inHand, isHeroPower, null));
	}

	// Token: 0x060030EF RID: 12527 RVA: 0x000FBC60 File Offset: 0x000F9E60
	public void UpdateKeywordHelp(Card card, Actor actor, bool showOnRight = true, float? overrideScale = null, Vector3? overrideOffset = null)
	{
		this.m_card = card;
		this.UpdateKeywordHelp(card.GetEntity(), actor, showOnRight, overrideScale, overrideOffset);
	}

	// Token: 0x060030F0 RID: 12528 RVA: 0x000FBC7C File Offset: 0x000F9E7C
	private EntityBase GetDesiredEntityBaseForEntity(Entity entity)
	{
		if (entity == null)
		{
			return null;
		}
		int tag = entity.GetTag(GAME_TAG.ALTERNATE_MOUSE_OVER_CARD);
		if (tag != 0)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(tag, true);
			if (entityDef != null)
			{
				return entityDef;
			}
			Log.Gameplay.PrintError("TooltipPanelManager.GetDesiredEntityBaseForEntity(): Unable to load EntityDef for card ID {0}.", new object[]
			{
				tag
			});
		}
		return entity;
	}

	// Token: 0x060030F1 RID: 12529 RVA: 0x000FBCD0 File Offset: 0x000F9ED0
	public void UpdateKeywordHelp(Entity entity, Actor actor, bool showOnRight, float? overrideScale = null, Vector3? overrideOffset = null)
	{
		this.m_card = entity.GetCard();
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.SHOW_CRAZY_KEYWORD_TOOLTIP))
		{
			if (TutorialKeywordManager.Get() != null)
			{
				TutorialKeywordManager.Get().UpdateKeywordHelp(entity, actor, showOnRight, overrideScale);
			}
			return;
		}
		bool flag = this.m_card.GetZone() is ZoneHand;
		bool isHeroPower = entity.IsHeroPower();
		if (overrideScale != null)
		{
			this.scaleToUse = overrideScale.Value;
		}
		else if (flag)
		{
			this.scaleToUse = TooltipPanel.HAND_SCALE;
		}
		else
		{
			this.scaleToUse = TooltipPanel.GAMEPLAY_SCALE;
		}
		this.PrepareToUpdateKeywordHelp(actor);
		string[] array = GameState.Get().GetGameEntity().NotifyOfKeywordHelpPanelDisplay(entity);
		if (array != null)
		{
			this.SetupTooltipPanel(array[0], array[1]);
		}
		this.SetUpPanels(this.GetDesiredEntityBaseForEntity(entity));
		base.StartCoroutine(this.PositionPanelsForGame(actor.GetMeshRenderer(false).gameObject, showOnRight, flag, isHeroPower, overrideOffset));
		GameState.Get().GetGameEntity().NotifyOfHelpPanelDisplay(this.m_tooltipPanels.Count);
	}

	// Token: 0x060030F2 RID: 12530 RVA: 0x000FBDD8 File Offset: 0x000F9FD8
	private IEnumerator PositionPanelsForGame(GameObject actorObject, bool showOnRight, bool inHand, bool isHeroPower, Vector3? overrideOffset = null)
	{
		TooltipPanel prevPanel = null;
		int num;
		for (int i = 0; i < this.m_tooltipPanels.Count; i = num + 1)
		{
			TooltipPanel curPanel = this.m_tooltipPanels[i];
			while (curPanel != null && !curPanel.Destroyed && !curPanel.IsTextRendered())
			{
				yield return null;
			}
			if (curPanel == null || curPanel.gameObject == null || actorObject == null || curPanel.Destroyed)
			{
				yield break;
			}
			if (i == 0)
			{
				if (overrideOffset != null)
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
					if (UniversalInputManager.UsePhoneUI)
					{
						TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0f, 0f, 1f), actorObject, new Vector3(1f, 0f, 1f), new Vector3(0.6f, 0f, 0.2f));
					}
					else
					{
						TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0f, 0f, 1f), actorObject, new Vector3(1f, 0f, 1f), new Vector3(0.38f, 0f, -0.05f));
					}
				}
				else if (UniversalInputManager.UsePhoneUI)
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
					TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0f, 0f, 1f), actorObject, new Vector3(1f, 0f, 1f), new Vector3(0.5f * this.scaleToUse + 0.15f, 0f, 0.8f));
				}
				else
				{
					TransformUtil.SetPoint(curPanel.gameObject, new Vector3(1f, 0f, 1f), actorObject, new Vector3(0f, 0f, 1f), new Vector3(-0.78f * this.scaleToUse - 0.15f, 0f, 0.8f));
				}
			}
			else
			{
				TransformUtil.SetPoint(curPanel.gameObject, new Vector3(0f, 0f, 1f), prevPanel.gameObject, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0.17f));
			}
			prevPanel = curPanel;
			curPanel = null;
			num = i;
		}
		yield break;
	}

	// Token: 0x060030F3 RID: 12531 RVA: 0x000FBE0C File Offset: 0x000FA00C
	public void UpdateKeywordHelpForHistoryCard(Entity entity, Actor actor, UberText createdByText)
	{
		this.m_card = entity.GetCard();
		this.scaleToUse = TooltipPanel.HISTORY_SCALE;
		this.PrepareToUpdateKeywordHelp(actor);
		string[] array = GameState.Get().GetGameEntity().NotifyOfKeywordHelpPanelDisplay(entity);
		if (array != null)
		{
			this.SetupTooltipPanel(array[0], array[1]);
		}
		this.SetUpPanels(this.GetDesiredEntityBaseForEntity(entity));
		base.StartCoroutine(this.PositionPanelsForHistory(actor, createdByText));
	}

	// Token: 0x060030F4 RID: 12532 RVA: 0x000FBE78 File Offset: 0x000FA078
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
				Error.AddDevWarning("Missing Bone", "Missing HistoryKeywordBone on {0}", new object[]
				{
					actor
				});
				yield return null;
			}
			firstRelativeAnchor = historyKeywordBone;
			historyKeywordBone = null;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_tooltipPanels.Clear();
		}
		TooltipPanel prevPanel = null;
		bool showHorizontally = false;
		int num2;
		for (int i = 0; i < this.m_tooltipPanels.Count; i = num2 + 1)
		{
			TooltipPanel curPanel = this.m_tooltipPanels[i];
			while (curPanel != null && !curPanel.Destroyed && !curPanel.IsTextRendered())
			{
				yield return null;
			}
			if (!(curPanel == null) && !curPanel.Destroyed)
			{
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
				curPanel = null;
			}
			num2 = i;
		}
		yield break;
	}

	// Token: 0x060030F5 RID: 12533 RVA: 0x000FBE95 File Offset: 0x000FA095
	public void UpdateKeywordHelpForCollectionManager(EntityDef entityDef, Actor actor, TooltipPanelManager.Orientation orientation)
	{
		this.scaleToUse = TooltipPanel.COLLECTION_MANAGER_SCALE;
		this.PrepareToUpdateKeywordHelp(actor);
		this.SetUpPanels(entityDef);
		base.StartCoroutine(this.PositionPanelsForCM(actor, orientation));
	}

	// Token: 0x060030F6 RID: 12534 RVA: 0x000FBEC4 File Offset: 0x000FA0C4
	private IEnumerator PositionPanelsForCM(Actor actor, TooltipPanelManager.Orientation orientation = TooltipPanelManager.Orientation.RightTop)
	{
		GameObject actorObject = actor.GetMeshRenderer(false).gameObject;
		TooltipPanel prevPanel = null;
		int maxPanelCount = this.m_tooltipPanels.Count;
		if (UniversalInputManager.UsePhoneUI)
		{
			maxPanelCount = Mathf.Min(this.m_tooltipPanels.Count, 3);
		}
		Vector3 actorStartAnchor;
		Vector3 panelStartAnchor;
		Vector3 panelEndAnchor;
		switch (orientation)
		{
		case TooltipPanelManager.Orientation.RightTop:
			actorStartAnchor = new Vector3(1f, 0f, 1f);
			panelStartAnchor = new Vector3(0f, 0f, 1f);
			panelEndAnchor = Vector3.zero;
			break;
		case TooltipPanelManager.Orientation.RightBottom:
			actorStartAnchor = new Vector3(1f, 0f, 0f);
			panelStartAnchor = Vector3.zero;
			panelEndAnchor = new Vector3(0f, 0f, 1f);
			break;
		case TooltipPanelManager.Orientation.LeftMiddle:
			actorStartAnchor = new Vector3(-1f, 0f, 0.5f);
			panelStartAnchor = new Vector3(1f, 0f, 0.4f);
			panelEndAnchor = new Vector3(0f, 0f, 0f);
			break;
		default:
			Log.All.PrintError("TooltipPanelManager.PositionPanelsForCM received a bad orientation value: " + orientation, Array.Empty<object>());
			actorStartAnchor = Vector3.zero;
			panelStartAnchor = Vector3.zero;
			panelEndAnchor = Vector3.zero;
			break;
		}
		int num;
		for (int i = 0; i < this.m_tooltipPanels.Count; i = num + 1)
		{
			TooltipPanel panel = this.m_tooltipPanels[i];
			if (i >= maxPanelCount)
			{
				panel.gameObject.SetActive(false);
			}
			else
			{
				while (panel != null && !panel.Destroyed && !panel.IsTextRendered())
				{
					yield return null;
				}
				if (!(panel == null) && !panel.Destroyed)
				{
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
								Log.All.Print("Missing card keyword tooltip offset: " + component.GetOffscreenPositionOffset().ToString(), Array.Empty<object>());
								panel.gameObject.transform.position -= component.GetOffscreenPositionOffset();
							}
						}
					}
					else
					{
						TransformUtil.SetPoint(panel.gameObject, panelStartAnchor, prevPanel.gameObject, panelEndAnchor, Vector3.zero);
					}
					prevPanel = panel;
					panel = null;
				}
			}
			num = i;
		}
		yield break;
	}

	// Token: 0x060030F7 RID: 12535 RVA: 0x000FBEE4 File Offset: 0x000FA0E4
	public void UpdateGhostCardHelpForCollectionManager(Actor actor, GhostCard.Type ghostType, TooltipPanelManager.Orientation orientation)
	{
		this.scaleToUse = TooltipPanel.COLLECTION_MANAGER_SCALE;
		this.PrepareToUpdateGhostCardHelp(actor);
		string str = UniversalInputManager.Get().IsTouchMode() ? "_TOUCH" : "";
		string headline;
		string description;
		if (ghostType == GhostCard.Type.NOT_VALID)
		{
			headline = GameStrings.Get("GLUE_GHOST_CARD_NOT_VALID_TITLE");
			description = GameStrings.Get("GLUE_GHOST_CARD_NOT_VALID_DESCRIPTION" + str);
		}
		else
		{
			if (ghostType != GhostCard.Type.MISSING && ghostType != GhostCard.Type.MISSING_UNCRAFTABLE)
			{
				return;
			}
			headline = GameStrings.Get("GLUE_GHOST_CARD_MISSING_TITLE");
			description = GameStrings.Get("GLUE_GHOST_CARD_MISSING_DESCRIPTION" + str);
		}
		this.SetupTooltipPanel(headline, description);
		base.StartCoroutine(this.PositionPanelsForCM(actor, orientation));
	}

	// Token: 0x060030F8 RID: 12536 RVA: 0x000FBF81 File Offset: 0x000FA181
	public void UpdateKeywordHelpForDeckHelper(EntityDef entityDef, Actor actor)
	{
		this.scaleToUse = 3.75f;
		this.PrepareToUpdateKeywordHelp(actor);
		this.SetUpPanels(entityDef);
		base.StartCoroutine(this.PositionPanelsForForge(actor.GetMeshRenderer(false).gameObject, 0));
	}

	// Token: 0x060030F9 RID: 12537 RVA: 0x000FBFB6 File Offset: 0x000FA1B6
	public void UpdateKeywordHelpForAdventure(EntityDef entityDef, Actor actor)
	{
		this.scaleToUse = TooltipPanel.ADVENTURE_SCALE;
		this.PrepareToUpdateKeywordHelp(actor);
		this.SetUpPanels(entityDef);
		base.StartCoroutine(this.PositionPanelsForForge(actor.GetMeshRenderer(false).gameObject, 0));
	}

	// Token: 0x060030FA RID: 12538 RVA: 0x000FBFF0 File Offset: 0x000FA1F0
	public void UpdateKeywordHelpForForge(EntityDef entityDef, Actor actor, int cardChoice = 0)
	{
		this.scaleToUse = TooltipPanel.FORGE_SCALE;
		this.PrepareToUpdateKeywordHelp(actor);
		this.SetUpPanels(entityDef);
		base.StartCoroutine(this.PositionPanelsForForge(actor.GetMeshRenderer(false).gameObject, cardChoice));
	}

	// Token: 0x060030FB RID: 12539 RVA: 0x000FC02A File Offset: 0x000FA22A
	private IEnumerator PositionPanelsForForge(GameObject actorObject, int cardChoice = 0)
	{
		TooltipPanel prevPanel = null;
		int num;
		for (int i = 0; i < this.m_tooltipPanels.Count; i = num + 1)
		{
			TooltipPanel panel = this.m_tooltipPanels[i];
			while (panel != null && !panel.Destroyed && !panel.IsTextRendered())
			{
				yield return null;
			}
			if (!(panel == null) && !panel.Destroyed)
			{
				if (i == 0)
				{
					if (UniversalInputManager.UsePhoneUI)
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
				panel = null;
			}
			num = i;
		}
		yield break;
	}

	// Token: 0x060030FC RID: 12540 RVA: 0x000FC047 File Offset: 0x000FA247
	public void UpdateKeywordHelpForPackOpening(EntityDef entityDef, Actor actor)
	{
		this.scaleToUse = 2.75f;
		this.PrepareToUpdateKeywordHelp(actor);
		this.SetUpPanels(entityDef);
		base.StartCoroutine(this.PositionPanelsForPackOpening(actor.GetMeshRenderer(false).gameObject));
	}

	// Token: 0x060030FD RID: 12541 RVA: 0x000FC07B File Offset: 0x000FA27B
	private IEnumerator PositionPanelsForPackOpening(GameObject actorObject)
	{
		TooltipPanel prevPanel = null;
		int num;
		for (int i = 0; i < this.m_tooltipPanels.Count; i = num + 1)
		{
			TooltipPanel panel = this.m_tooltipPanels[i];
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
				panel = null;
			}
			num = i;
		}
		yield break;
	}

	// Token: 0x060030FE RID: 12542 RVA: 0x000FC094 File Offset: 0x000FA294
	public void UpdateKeywordHelpForMulliganCard(Entity entity, Actor actor)
	{
		this.m_card = entity.GetCard();
		this.scaleToUse = TooltipPanel.MULLIGAN_SCALE;
		this.PrepareToUpdateKeywordHelp(actor);
		string[] array = GameState.Get().GetGameEntity().NotifyOfKeywordHelpPanelDisplay(entity);
		if (array != null)
		{
			this.SetupTooltipPanel(array[0], array[1]);
		}
		this.SetUpPanels(this.GetDesiredEntityBaseForEntity(entity));
		base.StartCoroutine(this.PositionPanelsForMulligan(actor.GetMeshRenderer(false).gameObject));
	}

	// Token: 0x060030FF RID: 12543 RVA: 0x000FC10A File Offset: 0x000FA30A
	private IEnumerator PositionPanelsForMulligan(GameObject actorObject)
	{
		TooltipPanel prevPanel = null;
		bool showHorizontally = false;
		int num2;
		for (int i = 0; i < this.m_tooltipPanels.Count; i = num2 + 1)
		{
			TooltipPanel curPanel = this.m_tooltipPanels[i];
			while (curPanel != null && !curPanel.Destroyed && !curPanel.IsTextRendered())
			{
				yield return null;
			}
			if (!(curPanel == null) && !curPanel.Destroyed)
			{
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
				curPanel = null;
			}
			num2 = i;
		}
		yield break;
	}

	// Token: 0x06003100 RID: 12544 RVA: 0x000FC120 File Offset: 0x000FA320
	private void PrepareToUpdateKeywordHelp(Actor actor)
	{
		this.HideKeywordHelp();
		this.m_actor = actor;
		this.m_tooltipPanels.Clear();
	}

	// Token: 0x06003101 RID: 12545 RVA: 0x000FC13A File Offset: 0x000FA33A
	private void PrepareToUpdateGhostCardHelp(Actor actor)
	{
		this.HideTooltipPanels();
		this.m_actor = actor;
		this.m_tooltipPanels.Clear();
	}

	// Token: 0x06003102 RID: 12546 RVA: 0x000FC154 File Offset: 0x000FA354
	private void SetUpPanels(EntityBase entityInfo)
	{
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SHIFTING);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SHIFTING_MINION);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SHIFTING_WEAPON);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SHIFTING_SPELL);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.FLOOPY);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.BOSS);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.WILD);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.HALL_OF_FAME);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.START_OF_COMBAT);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.EMPOWER);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.TAUNT);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.STEALTH);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.DIVINE_SHIELD);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_ARCANE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_FIRE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_FROST);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_NATURE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_HOLY);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_SHADOW);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_FEL);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER_PHYSICAL);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.ENRAGED_TOOLTIP);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.CHARGE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.BATTLECRY);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.FROZEN);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.FREEZE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.WINDFURY);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.MEGA_WINDFURY);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.ECHO);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.RUSH);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.MODULAR);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.OVERKILL);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.PROPHECY);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.ETHEREAL);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.MARK_OF_EVIL);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.WAND);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.TWINSPELL);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.REBORN);
		if (entityInfo.GetZone() != TAG_ZONE.SECRET)
		{
			this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SECRET);
		}
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.DEATHRATTLE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.OVERLOAD);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.COMBO);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SILENCE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.COUNTER);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.IMMUNE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPARE_PART);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.INSPIRE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.DISCOVER);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.CTHUN);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.AUTOATTACK);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.MINION_TYPE_REFERENCE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.JADE_GOLEM);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.GRIMY_GOONS);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.JADE_LOTUS);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.KABAL);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.QUEST);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SIDEQUEST);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.POISONOUS);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.ADAPT);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.LIFESTEAL);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.RECRUIT);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.DUNGEON_PASSIVE_BUFF);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.START_OF_GAME);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.CASTSWHENDRAWN);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SHRINE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.FATIGUEREFERENCE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.OUTCAST);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.STUDY);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLBURST);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.CORRUPT);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.DORMANT);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.CORRUPTEDCARD);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.FRENZY);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.BLOOD_GEM);
		if (entityInfo.IsHeroPower())
		{
			this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.AI_MUST_PLAY);
		}
	}

	// Token: 0x06003103 RID: 12547 RVA: 0x000FC550 File Offset: 0x000FA750
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
					tag = this.GetEmpowerTagByClass(entityInfo.GetClass());
				}
				if (CollectionManager.Get().IsInEditMode())
				{
					CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
					string galakrondCardIdByClass = GameUtils.GetGalakrondCardIdByClass(editedDeck.GetClass());
					if (editedDeck.GetCardIdCount(galakrondCardIdByClass, true) > 0)
					{
						tag = this.GetEmpowerTagByClass(editedDeck.GetClass());
					}
				}
			}
			this.SetupCollectionKeywordPanel(tag);
			return true;
		}
		if (num != 0 && GameStrings.HasKeywordText(tag))
		{
			foreach (GAME_TAG game_TAG in TooltipPanelManager.spellpowerTags)
			{
				if (tag == game_TAG)
				{
					int tag2 = entityInfo.GetTag(tag);
					string description = string.Empty;
					if (tag2 > 0)
					{
						description = GameStrings.Format(GameStrings.GetKeywordTextKey(tag), new object[]
						{
							tag2
						});
					}
					else
					{
						description = GameStrings.Get(GameStrings.GetRefKeywordTextKey(tag));
					}
					string keywordName = GameStrings.GetKeywordName(tag);
					this.SetupTooltipPanel(keywordName, description);
					return true;
				}
			}
			if (tag == GAME_TAG.WINDFURY && num > 1)
			{
				if (num == 3)
				{
					this.SetupKeywordPanel(GAME_TAG.MEGA_WINDFURY);
					return true;
				}
				return false;
			}
			else
			{
				if (tag != GAME_TAG.SHIFTING_MINION && tag != GAME_TAG.SHIFTING_WEAPON && tag != GAME_TAG.SHIFTING_SPELL && tag != GAME_TAG.SHIFTING)
				{
					if (tag == GAME_TAG.AI_MUST_PLAY && SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
					{
						int controllerId = entityInfo.GetControllerId();
						Player player = GameState.Get().GetPlayer(controllerId);
						if (player != null && !player.IsAI())
						{
							return false;
						}
					}
					if (tag == GAME_TAG.EMPOWER && SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
					{
						int controllerId2 = entityInfo.GetControllerId();
						Player player2 = GameState.Get().GetPlayer(controllerId2);
						if (player2 != null && player2.HasTag(GAME_TAG.PROXY_GALAKROND))
						{
							Entity entity = GameState.Get().GetEntity(player2.GetTag(GAME_TAG.PROXY_GALAKROND));
							tag = this.GetEmpowerTagByClass(entity.GetClass());
						}
					}
					this.SetupKeywordPanel(tag);
					return true;
				}
				int tag3 = entityInfo.GetTag(GAME_TAG.TRANSFORMED_FROM_CARD);
				if (tag3 == 0)
				{
					return false;
				}
				EntityDef entityDef = DefLoader.Get().GetEntityDef(tag3, true);
				string description2 = GameStrings.Get(GameStrings.GetKeywordTextKey(tag));
				this.SetupTooltipPanel(entityDef.GetName(), description2);
				return true;
			}
		}
		else
		{
			if (num2 != 0 && GameStrings.HasRefKeywordText(tag))
			{
				this.SetupKeywordRefPanel(tag);
				return true;
			}
			return false;
		}
	}

	// Token: 0x06003104 RID: 12548 RVA: 0x000FC7E0 File Offset: 0x000FA9E0
	private Vector3 GetPanelPosition(TooltipPanel panel)
	{
		Vector3 result = new Vector3(0f, 0f, 0f);
		TooltipPanel tooltipPanel = null;
		for (int i = 0; i < this.m_tooltipPanels.Count; i++)
		{
			TooltipPanel tooltipPanel2 = this.m_tooltipPanels[i];
			float num;
			if (this.m_card.GetEntity().IsHero())
			{
				num = 1.2f;
			}
			else if (this.m_card.GetEntity().GetZone() == TAG_ZONE.PLAY)
			{
				num = 1.05f;
			}
			else
			{
				num = 0.85f;
			}
			if (this.m_actor.GetMeshRenderer(false) == null)
			{
				return result;
			}
			float num2 = -0.2f * this.m_actor.GetMeshRenderer(false).bounds.size.z;
			if (tooltipPanel2 == panel)
			{
				if (i == 0)
				{
					result = this.m_actor.transform.position + new Vector3(this.m_actor.GetMeshRenderer(false).bounds.size.x * num, 0f, this.m_actor.GetMeshRenderer(false).bounds.extents.z + num2);
				}
				else
				{
					result = tooltipPanel.transform.position - new Vector3(0f, 0f, tooltipPanel.GetHeight() * 0.35f + tooltipPanel2.GetHeight() * 0.35f);
				}
			}
			tooltipPanel = tooltipPanel2;
		}
		return result;
	}

	// Token: 0x06003105 RID: 12549 RVA: 0x000FC96C File Offset: 0x000FAB6C
	private void SetupCollectionKeywordPanel(GAME_TAG tag)
	{
		string keywordName = GameStrings.GetKeywordName(tag);
		string description = GameStrings.Get(GameStrings.GetCollectionKeywordTextKey(tag));
		this.SetupTooltipPanel(keywordName, description);
	}

	// Token: 0x06003106 RID: 12550 RVA: 0x000FC994 File Offset: 0x000FAB94
	private void SetupKeywordPanel(GAME_TAG tag)
	{
		string keywordName = GameStrings.GetKeywordName(tag);
		string description = GameStrings.Get(GameStrings.GetKeywordTextKey(tag));
		this.SetupTooltipPanel(keywordName, description);
	}

	// Token: 0x06003107 RID: 12551 RVA: 0x000FC9BC File Offset: 0x000FABBC
	private void SetupKeywordRefPanel(GAME_TAG tag)
	{
		string keywordName = GameStrings.GetKeywordName(tag);
		string description = GameStrings.Get(GameStrings.GetRefKeywordTextKey(tag));
		this.SetupTooltipPanel(keywordName, description);
	}

	// Token: 0x06003108 RID: 12552 RVA: 0x000FC9E4 File Offset: 0x000FABE4
	private void SetupTooltipPanel(string headline, string description)
	{
		TooltipPanel tooltipPanel = this.m_tooltipPanelPool.Acquire();
		if (tooltipPanel == null)
		{
			return;
		}
		tooltipPanel.Reset();
		tooltipPanel.Initialize(headline, description);
		tooltipPanel.SetScale(this.scaleToUse);
		this.m_tooltipPanels.Add(tooltipPanel);
		this.FadeInPanel(tooltipPanel);
	}

	// Token: 0x06003109 RID: 12553 RVA: 0x000FCA34 File Offset: 0x000FAC34
	private void FadeInPanel(TooltipPanel helpPanel)
	{
		this.CleanTweensOnPanel(helpPanel);
		float num = 0.4f;
		if (GameState.Get() != null && GameState.Get().GetBooleanGameOption(GameEntityOption.KEYWORD_HELP_DELAY_OVERRIDDEN))
		{
			num = 0f;
		}
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"onupdatetarget",
			base.gameObject,
			"onupdate",
			"OnUberTextFadeUpdate",
			"time",
			0.125f,
			"delay",
			num,
			"to",
			1f,
			"from",
			0f
		}));
	}

	// Token: 0x0600310A RID: 12554 RVA: 0x000FCAF4 File Offset: 0x000FACF4
	private void OnUberTextFadeUpdate(float newValue)
	{
		foreach (TooltipPanel tooltipPanel in this.m_tooltipPanels)
		{
			RenderUtils.SetAlpha(tooltipPanel.gameObject, newValue, true);
		}
	}

	// Token: 0x0600310B RID: 12555 RVA: 0x000FCB4C File Offset: 0x000FAD4C
	private void CleanTweensOnPanel(TooltipPanel helpPanel)
	{
		iTween.Stop(base.gameObject);
		RenderUtils.SetAlpha(helpPanel.gameObject, 0f, true);
	}

	// Token: 0x0600310C RID: 12556 RVA: 0x000FCB6C File Offset: 0x000FAD6C
	public void ShowKeywordHelp()
	{
		foreach (TooltipPanel tooltipPanel in this.m_tooltipPanels)
		{
			tooltipPanel.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600310D RID: 12557 RVA: 0x000FCBC4 File Offset: 0x000FADC4
	public void HideKeywordHelp()
	{
		GameState gameState = GameState.Get();
		if (gameState != null && gameState.GetBooleanGameOption(GameEntityOption.SHOW_CRAZY_KEYWORD_TOOLTIP) && TutorialKeywordManager.Get() != null)
		{
			TutorialKeywordManager.Get().HideKeywordHelp();
		}
		this.HideTooltipPanels();
	}

	// Token: 0x0600310E RID: 12558 RVA: 0x000FCC04 File Offset: 0x000FAE04
	public void HideTooltipPanels()
	{
		foreach (TooltipPanel tooltipPanel in this.m_tooltipPanels)
		{
			if (!(tooltipPanel == null))
			{
				this.CleanTweensOnPanel(tooltipPanel);
				tooltipPanel.gameObject.SetActive(false);
				this.m_tooltipPanelPool.Release(tooltipPanel);
			}
		}
	}

	// Token: 0x0600310F RID: 12559 RVA: 0x000FCC7C File Offset: 0x000FAE7C
	public Card GetCard()
	{
		return this.m_card;
	}

	// Token: 0x06003110 RID: 12560 RVA: 0x000FCC84 File Offset: 0x000FAE84
	public Vector3 GetPositionOfTopPanel()
	{
		if (this.m_tooltipPanels.Count == 0)
		{
			return new Vector3(0f, 0f, 0f);
		}
		return this.m_tooltipPanels[0].transform.position;
	}

	// Token: 0x06003111 RID: 12561 RVA: 0x000FCCBE File Offset: 0x000FAEBE
	public TooltipPanel CreateKeywordPanel(int i)
	{
		return UnityEngine.Object.Instantiate<TooltipPanel>(this.m_tooltipPanelPrefab);
	}

	// Token: 0x06003112 RID: 12562 RVA: 0x000FCCCB File Offset: 0x000FAECB
	private void DestroyKeywordPanel(TooltipPanel panel)
	{
		if (panel != null)
		{
			UnityEngine.Object.Destroy(panel.gameObject);
		}
	}

	// Token: 0x06003113 RID: 12563 RVA: 0x000FCCE4 File Offset: 0x000FAEE4
	private void OnSceneUnloaded(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		foreach (TooltipPanel tooltipPanel in this.m_tooltipPanels)
		{
			UnityEngine.Object.Destroy(tooltipPanel.gameObject);
		}
		this.m_tooltipPanels.Clear();
		this.m_tooltipPanelPool.Clear();
		UnityEngine.Object.Destroy(this.m_actor);
		this.m_actor = null;
		UnityEngine.Object.Destroy(this.m_card);
		this.m_card = null;
	}

	// Token: 0x06003114 RID: 12564 RVA: 0x000FCD74 File Offset: 0x000FAF74
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

	// Token: 0x04001B40 RID: 6976
	public TooltipPanel m_tooltipPanelPrefab;

	// Token: 0x04001B41 RID: 6977
	private static TooltipPanelManager s_instance;

	// Token: 0x04001B42 RID: 6978
	private Pool<TooltipPanel> m_tooltipPanelPool = new Pool<TooltipPanel>();

	// Token: 0x04001B43 RID: 6979
	private List<TooltipPanel> m_tooltipPanels = new List<TooltipPanel>();

	// Token: 0x04001B44 RID: 6980
	private Actor m_actor;

	// Token: 0x04001B45 RID: 6981
	private Card m_card;

	// Token: 0x04001B46 RID: 6982
	private float scaleToUse;

	// Token: 0x04001B47 RID: 6983
	private const float FADE_IN_TIME = 0.125f;

	// Token: 0x04001B48 RID: 6984
	private const float DELAY_BEFORE_FADE_IN = 0.4f;

	// Token: 0x04001B49 RID: 6985
	private static readonly GAME_TAG[] spellpowerTags = new GAME_TAG[]
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

	// Token: 0x020016ED RID: 5869
	public enum Orientation
	{
		// Token: 0x0400B2D0 RID: 45776
		RightTop,
		// Token: 0x0400B2D1 RID: 45777
		RightBottom,
		// Token: 0x0400B2D2 RID: 45778
		LeftMiddle
	}
}
