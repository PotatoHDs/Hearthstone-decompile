using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_23h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_23h_SharkyMcFin_Death = new AssetReference("VO_DALA_BOSS_23h_SharkyMcFin_Death.prefab:19fffba6da6499444a0c0895b3e26307");

	private static readonly AssetReference VO_DALA_BOSS_23h_SharkyMcFin_DefeatPlayer = new AssetReference("VO_DALA_BOSS_23h_SharkyMcFin_DefeatPlayer.prefab:78db84ca05e10a749b5653311fe40572");

	private static readonly AssetReference VO_DALA_BOSS_23h_SharkyMcFin_EmoteResponse = new AssetReference("VO_DALA_BOSS_23h_SharkyMcFin_EmoteResponse.prefab:d8ccfc2635d6b3d4e9b0a26b2ac3bc41");

	private static readonly AssetReference VO_DALA_BOSS_23h_SharkyMcFin_Intro = new AssetReference("VO_DALA_BOSS_23h_SharkyMcFin_Intro.prefab:0ef7854270498b843825a831374135e7");

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_DALA_BOSS_23h_SharkyMcFin_Death, VO_DALA_BOSS_23h_SharkyMcFin_DefeatPlayer, VO_DALA_BOSS_23h_SharkyMcFin_EmoteResponse, VO_DALA_BOSS_23h_SharkyMcFin_Intro };
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_23h_SharkyMcFin_Intro;
		m_deathLine = VO_DALA_BOSS_23h_SharkyMcFin_Death;
		m_standardEmoteResponseLine = VO_DALA_BOSS_23h_SharkyMcFin_EmoteResponse;
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
