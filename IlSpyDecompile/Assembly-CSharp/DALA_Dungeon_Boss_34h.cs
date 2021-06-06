using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_34h : DALA_Dungeon
{
	private static readonly AssetReference CarouselGryphon_DALA_Boss_34h_Death = new AssetReference("CarouselGryphon_DALA_Boss_34h_Death.prefab:9a5d763ff8219e64493f0c394518157a");

	private static readonly AssetReference CarouselGryphon_DALA_Boss_34h_Emote_01 = new AssetReference("CarouselGryphon_DALA_Boss_34h_Emote_01.prefab:6ab7a927d2c03c74ca44623c844c1e47");

	private static readonly AssetReference CarouselGryphon_DALA_Boss_34h_Emote_02 = new AssetReference("CarouselGryphon_DALA_Boss_34h_Emote_02.prefab:98c16c64b2f2d1e41bc8ce4e974f2f95");

	private static readonly AssetReference CarouselGryphon_DALA_Boss_34h_Emote_03 = new AssetReference("CarouselGryphon_DALA_Boss_34h_Emote_03.prefab:194e38fef6218364c962b73e7610ef17");

	private static readonly AssetReference CarouselGryphon_DALA_Boss_34h_Start = new AssetReference("CarouselGryphon_DALA_Boss_34h_Start.prefab:2373356c6e363214eb50c87492284296");

	private static readonly AssetReference CarouselGryphon_DALA_Boss_34h_Taunt = new AssetReference("CarouselGryphon_DALA_Boss_34h_Taunt.prefab:392547bde2ed1cd45b457f1d0a6788a3");

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { CarouselGryphon_DALA_Boss_34h_Death, CarouselGryphon_DALA_Boss_34h_Emote_01, CarouselGryphon_DALA_Boss_34h_Emote_02, CarouselGryphon_DALA_Boss_34h_Emote_03, CarouselGryphon_DALA_Boss_34h_Start, CarouselGryphon_DALA_Boss_34h_Taunt };
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = CarouselGryphon_DALA_Boss_34h_Start;
		m_deathLine = CarouselGryphon_DALA_Boss_34h_Death;
		m_standardEmoteResponseLine = CarouselGryphon_DALA_Boss_34h_Taunt;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Eudora")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { CarouselGryphon_DALA_Boss_34h_Emote_01, CarouselGryphon_DALA_Boss_34h_Emote_02, CarouselGryphon_DALA_Boss_34h_Emote_03 };
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		_ = missionEvent;
		yield return base.HandleMissionEventWithTiming(missionEvent);
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
		}
	}
}
