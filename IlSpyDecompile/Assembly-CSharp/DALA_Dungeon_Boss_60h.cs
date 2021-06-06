using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_60h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Death_03 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Death_03.prefab:91c97f2113caf5d4da4e60ac34e39ba6");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_DefeatPlayer_02.prefab:adc29f00e12bfaf47a7339fd3487e8a9");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_EmoteResponse_01.prefab:08225fdd9ba844e45abf869181733f72");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_HeroPower_01.prefab:b72cce13135e55e4598b54aef801c22b");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_HeroPower_02.prefab:5be333e58b79bb74e8750858b88d7cd0");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_HeroPower_03.prefab:49715743f3d78634ca06950b6cf5fb7a");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_HeroPower_04.prefab:ae04e3a849f20264092fce92e76b5061");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Idle_01.prefab:e9602fe7e5572a7479b648c0451599d5");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Idle_02.prefab:6ddb5d5dbc5d8fb4c82b267384fde9fe");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Idle_03.prefab:b6331b5a121661d43a81162595c68d7d");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Intro_01.prefab:055c4ee8194fc5d43b42b59bd84468ad");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_03 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_03.prefab:12efa1dd4e9b5e24dab0a09e61655b5b");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_04 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_04.prefab:982ae5988e6e0f948a1981e443456882");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerPlaysFloatingHead_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerPlaysFloatingHead_01.prefab:ad61843dd935ca44dadb5e98fc15e823");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerSavedByHead_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerSavedByHead_01.prefab:47fa09d01ffcdb04dbf095a0365fb542");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_60h_Male_Human_Idle_01, VO_DALA_BOSS_60h_Male_Human_Idle_02, VO_DALA_BOSS_60h_Male_Human_Idle_03 };

	private static List<string> m_CopySpell = new List<string> { VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_03, VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_04 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_60h_Male_Human_Death_03, VO_DALA_BOSS_60h_Male_Human_DefeatPlayer_02, VO_DALA_BOSS_60h_Male_Human_EmoteResponse_01, VO_DALA_BOSS_60h_Male_Human_HeroPower_01, VO_DALA_BOSS_60h_Male_Human_HeroPower_02, VO_DALA_BOSS_60h_Male_Human_HeroPower_03, VO_DALA_BOSS_60h_Male_Human_HeroPower_04, VO_DALA_BOSS_60h_Male_Human_Idle_01, VO_DALA_BOSS_60h_Male_Human_Idle_02, VO_DALA_BOSS_60h_Male_Human_Idle_03,
			VO_DALA_BOSS_60h_Male_Human_Intro_01, VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_03, VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_04, VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerPlaysFloatingHead_01, VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerSavedByHead_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_60h_Male_Human_Intro_01;
		m_deathLine = VO_DALA_BOSS_60h_Male_Human_Death_03;
		m_standardEmoteResponseLine = VO_DALA_BOSS_60h_Male_Human_EmoteResponse_01;
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_60h_Male_Human_HeroPower_01, VO_DALA_BOSS_60h_Male_Human_HeroPower_02, VO_DALA_BOSS_60h_Male_Human_HeroPower_03, VO_DALA_BOSS_60h_Male_Human_HeroPower_04 };
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 526:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerPlaysFloatingHead_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerSavedByHead_01);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_CopySpell);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
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
			yield return base.RespondToPlayedCardWithTiming(entity);
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
		}
	}
}
