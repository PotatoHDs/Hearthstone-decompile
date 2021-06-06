using System;

// Token: 0x0200086F RID: 2159
[Serializable]
public class CardDefSpecialEvent
{
	// Token: 0x060075BE RID: 30142 RVA: 0x0025CA28 File Offset: 0x0025AC28
	public static CardDefSpecialEvent FindActiveEvent(CardDef cardDef)
	{
		if (SpecialEventManager.Get() == null)
		{
			return null;
		}
		foreach (CardDefSpecialEvent cardDefSpecialEvent in cardDef.m_SpecialEvents)
		{
			if (SpecialEventManager.Get().IsEventActive(cardDefSpecialEvent.EventType, false))
			{
				SceneMgr.Mode mode = SceneMgr.Mode.INVALID;
				switch (cardDefSpecialEvent.m_SceneMode)
				{
				case CardDefSpecialEvent.EventSceneMode.Arena:
					mode = SceneMgr.Mode.DRAFT;
					break;
				case CardDefSpecialEvent.EventSceneMode.Gameplay:
					mode = SceneMgr.Mode.GAMEPLAY;
					break;
				case CardDefSpecialEvent.EventSceneMode.CollectionManager:
					mode = SceneMgr.Mode.COLLECTIONMANAGER;
					break;
				case CardDefSpecialEvent.EventSceneMode.TavernBrawl:
					mode = SceneMgr.Mode.TAVERN_BRAWL;
					break;
				}
				if (SceneMgr.Get().GetMode() == mode || mode == SceneMgr.Mode.INVALID)
				{
					return cardDefSpecialEvent;
				}
				if (GameMgr.Get().GetMissionId() == (int)cardDefSpecialEvent.m_Scenario)
				{
					return cardDefSpecialEvent;
				}
			}
		}
		return null;
	}

	// Token: 0x04005CBE RID: 23742
	public SpecialEventType EventType;

	// Token: 0x04005CBF RID: 23743
	public CardDefSpecialEvent.EventSceneMode m_SceneMode;

	// Token: 0x04005CC0 RID: 23744
	public ScenarioDbId m_Scenario;

	// Token: 0x04005CC1 RID: 23745
	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE)]
	public string m_PortraitTextureOverride;

	// Token: 0x04005CC2 RID: 23746
	[CustomEditField(Sections = "Portrait", T = EditType.MATERIAL)]
	public string m_PremiumPortraitMaterialOverride;

	// Token: 0x04005CC3 RID: 23747
	[CustomEditField(Sections = "Portrait", T = EditType.UBERANIMATION)]
	public string m_PremiumUberShaderAnimationOverride;

	// Token: 0x04005CC4 RID: 23748
	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE)]
	public string m_PremiumPortraitTextureOverride;

	// Token: 0x0200248F RID: 9359
	public enum EventSceneMode
	{
		// Token: 0x0400EADB RID: 60123
		All,
		// Token: 0x0400EADC RID: 60124
		Arena,
		// Token: 0x0400EADD RID: 60125
		Gameplay,
		// Token: 0x0400EADE RID: 60126
		CollectionManager,
		// Token: 0x0400EADF RID: 60127
		TavernBrawl
	}
}
