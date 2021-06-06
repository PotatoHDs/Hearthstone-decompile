using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_30h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_30h_Male_Rat_Death = new AssetReference("VO_DALA_BOSS_30h_Male_Rat_Death.prefab:fb52c37daa34db346a08f66d8c66b8ce");

	private static readonly AssetReference VO_DALA_BOSS_30h_Male_Rat_DefeatPlayer = new AssetReference("VO_DALA_BOSS_30h_Male_Rat_DefeatPlayer.prefab:04d6baeac2746274185b97dfbccc033c");

	private static readonly AssetReference VO_DALA_BOSS_30h_Male_Rat_EmoteResponse = new AssetReference("VO_DALA_BOSS_30h_Male_Rat_EmoteResponse.prefab:5b16504a10dbb4f45af73cb1b4da7553");

	private static readonly AssetReference VO_DALA_BOSS_30h_Male_Rat_Intro = new AssetReference("VO_DALA_BOSS_30h_Male_Rat_Intro.prefab:fb83d73d78d4ce64eaae1db5f7715261");

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_DALA_BOSS_30h_Male_Rat_Death, VO_DALA_BOSS_30h_Male_Rat_DefeatPlayer, VO_DALA_BOSS_30h_Male_Rat_EmoteResponse, VO_DALA_BOSS_30h_Male_Rat_Intro };
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_30h_Male_Rat_Intro;
		m_deathLine = VO_DALA_BOSS_30h_Male_Rat_Death;
		m_standardEmoteResponseLine = VO_DALA_BOSS_30h_Male_Rat_EmoteResponse;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Vessina" && cardId != "DALA_Barkeye" && cardId != "DALA_Squeamlish")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
		}
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
