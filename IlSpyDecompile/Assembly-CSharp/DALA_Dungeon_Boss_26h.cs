using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_26h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_26h_DalaranFountainGolem_Death = new AssetReference("VO_DALA_BOSS_26h_DalaranFountainGolem_Death.prefab:96a8fe4578eb0f24db266fc11ce39e56");

	private static readonly AssetReference VO_DALA_BOSS_26h_DalaranFountainGolem_DefeatPlayer = new AssetReference("VO_DALA_BOSS_26h_DalaranFountainGolem_DefeatPlayer.prefab:f2401c70f688a5b4d8938932beaebee4");

	private static readonly AssetReference VO_DALA_BOSS_26h_DalaranFountainGolem_EmoteResponse = new AssetReference("VO_DALA_BOSS_26h_DalaranFountainGolem_EmoteResponse.prefab:f0c832b4e45f0db4ab850fc140c1f9f3");

	private static readonly AssetReference VO_DALA_BOSS_26h_DalaranFountainGolem_Intro = new AssetReference("VO_DALA_BOSS_26h_DalaranFountainGolem_Intro.prefab:06e0e9a059c18d64dbbaa86404a76315");

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_DALA_BOSS_26h_DalaranFountainGolem_Death, VO_DALA_BOSS_26h_DalaranFountainGolem_DefeatPlayer, VO_DALA_BOSS_26h_DalaranFountainGolem_EmoteResponse, VO_DALA_BOSS_26h_DalaranFountainGolem_Intro };
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_26h_DalaranFountainGolem_Intro;
		m_deathLine = VO_DALA_BOSS_26h_DalaranFountainGolem_Death;
		m_standardEmoteResponseLine = VO_DALA_BOSS_26h_DalaranFountainGolem_EmoteResponse;
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
