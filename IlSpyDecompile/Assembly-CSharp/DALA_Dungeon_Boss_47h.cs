using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_47h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_47h_Lavanthor_Death = new AssetReference("VO_DALA_BOSS_47h_Lavanthor_Death.prefab:7e208dd4154b0654a870063a4d336090");

	private static readonly AssetReference VO_DALA_BOSS_47h_Lavanthor_DefeatPlayer = new AssetReference("VO_DALA_BOSS_47h_Lavanthor_DefeatPlayer.prefab:c200bd9d856ebfd4e8c6469989200ec1");

	private static readonly AssetReference VO_DALA_BOSS_47h_Lavanthor_EmoteResponse = new AssetReference("VO_DALA_BOSS_47h_Lavanthor_EmoteResponse.prefab:15154e48f0b0d1e4dbc63ccfe61b0284");

	private static readonly AssetReference VO_DALA_BOSS_47h_Lavanthor_Intro = new AssetReference("VO_DALA_BOSS_47h_Lavanthor_Intro.prefab:b2e242f69f7a7e44c8fc14ed07d35736");

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_DALA_BOSS_47h_Lavanthor_Death, VO_DALA_BOSS_47h_Lavanthor_DefeatPlayer, VO_DALA_BOSS_47h_Lavanthor_EmoteResponse, VO_DALA_BOSS_47h_Lavanthor_Intro };
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_47h_Lavanthor_Intro;
		m_deathLine = VO_DALA_BOSS_47h_Lavanthor_Death;
		m_standardEmoteResponseLine = VO_DALA_BOSS_47h_Lavanthor_EmoteResponse;
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
