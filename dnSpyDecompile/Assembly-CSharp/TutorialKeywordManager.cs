using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005DF RID: 1503
public class TutorialKeywordManager : MonoBehaviour
{
	// Token: 0x06005260 RID: 21088 RVA: 0x001B0DBF File Offset: 0x001AEFBF
	private void Awake()
	{
		TutorialKeywordManager.s_instance = this;
	}

	// Token: 0x06005261 RID: 21089 RVA: 0x001B0DC7 File Offset: 0x001AEFC7
	private void OnDestroy()
	{
		TutorialKeywordManager.s_instance = null;
	}

	// Token: 0x06005262 RID: 21090 RVA: 0x001B0DCF File Offset: 0x001AEFCF
	public static TutorialKeywordManager Get()
	{
		return TutorialKeywordManager.s_instance;
	}

	// Token: 0x06005263 RID: 21091 RVA: 0x001B0DD8 File Offset: 0x001AEFD8
	public void UpdateKeywordHelp(Card c, Actor a)
	{
		this.UpdateKeywordHelp(c, a, true, null);
	}

	// Token: 0x06005264 RID: 21092 RVA: 0x001B0DF7 File Offset: 0x001AEFF7
	public void UpdateKeywordHelp(Card card, Actor actor, bool showOnRight, float? overrideScale = null)
	{
		this.m_card = card;
		this.UpdateKeywordHelp(card.GetEntity(), actor, showOnRight, overrideScale);
	}

	// Token: 0x06005265 RID: 21093 RVA: 0x001B0E10 File Offset: 0x001AF010
	public void UpdateKeywordHelp(Entity entity, Actor actor, bool showOnRight, float? overrideScale = null)
	{
		float num = 1f;
		if (overrideScale != null)
		{
			num = overrideScale.Value;
		}
		this.PrepareToUpdateKeywordHelp(actor);
		string[] array = GameState.Get().GetGameEntity().NotifyOfKeywordHelpPanelDisplay(entity);
		if (array != null)
		{
			this.SetupKeywordPanel(array[0], array[1]);
		}
		this.SetUpPanels(entity);
		TutorialKeywordTooltip tutorialKeywordTooltip = null;
		for (int i = 0; i < this.m_keywordPanels.Count; i++)
		{
			TutorialKeywordTooltip tutorialKeywordTooltip2 = this.m_keywordPanels[i];
			float num2 = 1.05f;
			if (entity.IsHero())
			{
				num2 = 1.2f;
			}
			else if (entity.GetZone() == TAG_ZONE.PLAY)
			{
				if (UniversalInputManager.UsePhoneUI)
				{
					num = 1.7f;
				}
				num2 = 1.45f * num;
			}
			tutorialKeywordTooltip2.transform.localScale = new Vector3(num, num, num);
			float num3 = -0.2f * this.m_actor.GetMeshRenderer(false).bounds.size.z;
			if (UniversalInputManager.UsePhoneUI && entity.GetZone() == TAG_ZONE.PLAY)
			{
				num3 += 1.5f;
			}
			if (i == 0)
			{
				if (showOnRight)
				{
					tutorialKeywordTooltip2.transform.position = this.m_actor.transform.position + new Vector3(this.m_actor.GetMeshRenderer(false).bounds.size.x * num2, 0f, this.m_actor.GetMeshRenderer(false).bounds.extents.z + num3);
				}
				else
				{
					tutorialKeywordTooltip2.transform.position = this.m_actor.transform.position + new Vector3(-this.m_actor.GetMeshRenderer(false).bounds.size.x * num2, 0f, this.m_actor.GetMeshRenderer(false).bounds.extents.z + num3);
				}
			}
			else
			{
				tutorialKeywordTooltip2.transform.position = tutorialKeywordTooltip.transform.position - new Vector3(0f, 0f, tutorialKeywordTooltip.GetHeight() * 0.35f + tutorialKeywordTooltip2.GetHeight() * 0.35f);
			}
			tutorialKeywordTooltip = tutorialKeywordTooltip2;
		}
		GameState.Get().GetGameEntity().NotifyOfHelpPanelDisplay(this.m_keywordPanels.Count);
	}

	// Token: 0x06005266 RID: 21094 RVA: 0x001B1083 File Offset: 0x001AF283
	private void PrepareToUpdateKeywordHelp(Actor actor)
	{
		this.HideKeywordHelp();
		this.m_actor = actor;
		this.m_keywordPanels = new List<TutorialKeywordTooltip>();
	}

	// Token: 0x06005267 RID: 21095 RVA: 0x001B10A0 File Offset: 0x001AF2A0
	private void SetUpPanels(EntityBase entityInfo)
	{
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.TAUNT);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.STEALTH);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.DIVINE_SHIELD);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SPELLPOWER);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.ENRAGED);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.CHARGE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.BATTLECRY);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.FROZEN);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.FREEZE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.WINDFURY);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.SECRET);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.DEATHRATTLE);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.OVERLOAD);
		this.SetupKeywordPanelIfNecessary(entityInfo, GAME_TAG.COMBO);
	}

	// Token: 0x06005268 RID: 21096 RVA: 0x001B1163 File Offset: 0x001AF363
	private bool SetupKeywordPanelIfNecessary(EntityBase entityInfo, GAME_TAG tag)
	{
		if (entityInfo.HasTag(tag))
		{
			this.SetupKeywordPanel(tag);
			return true;
		}
		if (entityInfo.HasReferencedTag(tag))
		{
			this.SetupKeywordRefPanel(tag);
			return true;
		}
		return false;
	}

	// Token: 0x06005269 RID: 21097 RVA: 0x001B118C File Offset: 0x001AF38C
	public void SetupKeywordPanel(GAME_TAG tag)
	{
		string keywordName = GameStrings.GetKeywordName(tag);
		string keywordText = GameStrings.GetKeywordText(tag);
		this.SetupKeywordPanel(keywordName, keywordText);
	}

	// Token: 0x0600526A RID: 21098 RVA: 0x001B11B0 File Offset: 0x001AF3B0
	public void SetupKeywordRefPanel(GAME_TAG tag)
	{
		string keywordName = GameStrings.GetKeywordName(tag);
		string refKeywordText = GameStrings.GetRefKeywordText(tag);
		this.SetupKeywordPanel(keywordName, refKeywordText);
	}

	// Token: 0x0600526B RID: 21099 RVA: 0x001B11D4 File Offset: 0x001AF3D4
	public void SetupKeywordPanel(string headline, string description)
	{
		TutorialKeywordTooltip component = UnityEngine.Object.Instantiate<GameObject>(this.m_keywordPanelPrefab.gameObject).GetComponent<TutorialKeywordTooltip>();
		if (component == null)
		{
			return;
		}
		component.Initialize(headline, description);
		this.m_keywordPanels.Add(component);
	}

	// Token: 0x0600526C RID: 21100 RVA: 0x001B1218 File Offset: 0x001AF418
	public void HideKeywordHelp()
	{
		if (this.m_keywordPanels == null)
		{
			return;
		}
		foreach (TutorialKeywordTooltip tutorialKeywordTooltip in this.m_keywordPanels)
		{
			if (!(tutorialKeywordTooltip == null))
			{
				UnityEngine.Object.Destroy(tutorialKeywordTooltip.gameObject);
			}
		}
	}

	// Token: 0x0600526D RID: 21101 RVA: 0x001B1284 File Offset: 0x001AF484
	public Card GetCard()
	{
		return this.m_card;
	}

	// Token: 0x0600526E RID: 21102 RVA: 0x001B128C File Offset: 0x001AF48C
	public Vector3 GetPositionOfTopPanel()
	{
		if (this.m_keywordPanels == null || this.m_keywordPanels.Count == 0)
		{
			return new Vector3(0f, 0f, 0f);
		}
		return this.m_keywordPanels[0].transform.position;
	}

	// Token: 0x04004982 RID: 18818
	public TutorialKeywordTooltip m_keywordPanelPrefab;

	// Token: 0x04004983 RID: 18819
	private static TutorialKeywordManager s_instance;

	// Token: 0x04004984 RID: 18820
	private List<TutorialKeywordTooltip> m_keywordPanels;

	// Token: 0x04004985 RID: 18821
	private Actor m_actor;

	// Token: 0x04004986 RID: 18822
	private Card m_card;
}
