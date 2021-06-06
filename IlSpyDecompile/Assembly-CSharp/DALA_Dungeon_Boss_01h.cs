using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_01h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_01h_Chomper_Death = new AssetReference("VO_DALA_BOSS_01h_Chomper_Death.prefab:a67617e34bd46ad4b86ce38b27538336");

	private static readonly AssetReference VO_DALA_BOSS_01h_Chomper_DefeatPlayer = new AssetReference("VO_DALA_BOSS_01h_Chomper_DefeatPlayer.prefab:7b9e096137b452c4bb0122120a526089");

	private static readonly AssetReference VO_DALA_BOSS_01h_Chomper_EmoteResponse = new AssetReference("VO_DALA_BOSS_01h_Chomper_EmoteResponse.prefab:a3805142083d27642ab9ace616499a88");

	private static readonly AssetReference VO_DALA_BOSS_01h_Chomper_Intro = new AssetReference("VO_DALA_BOSS_01h_Chomper_Intro.prefab:a4808c11753e77b43947a481f0fa7f43");

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_DALA_BOSS_01h_Chomper_Death, VO_DALA_BOSS_01h_Chomper_DefeatPlayer, VO_DALA_BOSS_01h_Chomper_EmoteResponse, VO_DALA_BOSS_01h_Chomper_Intro };
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_01h_Chomper_Intro;
		m_deathLine = VO_DALA_BOSS_01h_Chomper_Death;
		m_standardEmoteResponseLine = VO_DALA_BOSS_01h_Chomper_EmoteResponse;
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
