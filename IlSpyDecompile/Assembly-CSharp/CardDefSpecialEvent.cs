using System;

[Serializable]
public class CardDefSpecialEvent
{
	public enum EventSceneMode
	{
		All,
		Arena,
		Gameplay,
		CollectionManager,
		TavernBrawl
	}

	public SpecialEventType EventType;

	public EventSceneMode m_SceneMode;

	public ScenarioDbId m_Scenario;

	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE)]
	public string m_PortraitTextureOverride;

	[CustomEditField(Sections = "Portrait", T = EditType.MATERIAL)]
	public string m_PremiumPortraitMaterialOverride;

	[CustomEditField(Sections = "Portrait", T = EditType.UBERANIMATION)]
	public string m_PremiumUberShaderAnimationOverride;

	[CustomEditField(Sections = "Portrait", T = EditType.CARD_TEXTURE)]
	public string m_PremiumPortraitTextureOverride;

	public static CardDefSpecialEvent FindActiveEvent(CardDef cardDef)
	{
		if (SpecialEventManager.Get() == null)
		{
			return null;
		}
		foreach (CardDefSpecialEvent specialEvent in cardDef.m_SpecialEvents)
		{
			if (SpecialEventManager.Get().IsEventActive(specialEvent.EventType, activeIfDoesNotExist: false))
			{
				SceneMgr.Mode mode = SceneMgr.Mode.INVALID;
				switch (specialEvent.m_SceneMode)
				{
				case EventSceneMode.Arena:
					mode = SceneMgr.Mode.DRAFT;
					break;
				case EventSceneMode.Gameplay:
					mode = SceneMgr.Mode.GAMEPLAY;
					break;
				case EventSceneMode.CollectionManager:
					mode = SceneMgr.Mode.COLLECTIONMANAGER;
					break;
				case EventSceneMode.TavernBrawl:
					mode = SceneMgr.Mode.TAVERN_BRAWL;
					break;
				}
				if (SceneMgr.Get().GetMode() == mode || mode == SceneMgr.Mode.INVALID)
				{
					return specialEvent;
				}
				if (GameMgr.Get().GetMissionId() == (int)specialEvent.m_Scenario)
				{
					return specialEvent;
				}
			}
		}
		return null;
	}
}
