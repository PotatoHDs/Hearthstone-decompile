using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_74h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_BossEmptyHand_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_BossEmptyHand_01.prefab:6279c38e191bd6f40836841a135bc4a3");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_BossSoulfire_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_BossSoulfire_01.prefab:81610318de5ee41418f2a7f63691c0d6");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_Death_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_Death_01.prefab:550f70f20f886e343b994b051615a11f");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_DefeatPlayer_01.prefab:2a6cfb0036906b740be9d9ea89040241");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_EmoteResponse_01.prefab:a6444bb5a5063b54d96a1f2421c2899f");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPower_01.prefab:d0831452c2a17964fa2fdcbab8f1ce50");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPower_02.prefab:6b4eb949ad69e7641bd8801531956068");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPower_03.prefab:fe92466765ba592418d0f3e91bdf5a6e");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPower_04.prefab:010c3460ebe56b842accee8d3b164baa");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_01.prefab:d62f9bd347a604d418c35a414e45b6ee");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_02 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_02.prefab:a2d0d283167c27244a2758f594737696");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_01.prefab:812773881fe4f0b4bb4648dd840d160d");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_02 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_02.prefab:7f45c4c900d43214d890384045dd843f");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_Idle_01.prefab:144ce81414439bd4392c863e5f6f4859");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_Idle_02.prefab:23216cd59de29194fad9ce964168f9f9");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_Idle_03.prefab:1b45ff20c00d7f849a0f4157596902cc");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_Intro_01.prefab:6d244bdac3ea32a4184e5a900fb808ee");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_PlayerDemon_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_PlayerDemon_01.prefab:d9380cb44e4f34a44be70b098f40fb48");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_01.prefab:0c5b7454d1baed84dac0e313bae9fd51");

	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_02 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_02.prefab:50f915c7be85e734bb7c4a2062e3df18");

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_74h_Female_Human_Idle_01, VO_DALA_BOSS_74h_Female_Human_Idle_02, VO_DALA_BOSS_74h_Female_Human_Idle_03 };

	private static List<string> m_BossHeroPower = new List<string> { VO_DALA_BOSS_74h_Female_Human_HeroPower_01, VO_DALA_BOSS_74h_Female_Human_HeroPower_02, VO_DALA_BOSS_74h_Female_Human_HeroPower_03, VO_DALA_BOSS_74h_Female_Human_HeroPower_04 };

	private static List<string> m_BossHeroPowerSmall = new List<string> { VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_01, VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_02 };

	private static List<string> m_BossHeroPowerLarge = new List<string> { VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_01, VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_02 };

	private static List<string> m_PlayerDiscard = new List<string> { VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_01, VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_02 };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_74h_Female_Human_BossEmptyHand_01, VO_DALA_BOSS_74h_Female_Human_BossSoulfire_01, VO_DALA_BOSS_74h_Female_Human_Death_01, VO_DALA_BOSS_74h_Female_Human_DefeatPlayer_01, VO_DALA_BOSS_74h_Female_Human_EmoteResponse_01, VO_DALA_BOSS_74h_Female_Human_HeroPower_01, VO_DALA_BOSS_74h_Female_Human_HeroPower_02, VO_DALA_BOSS_74h_Female_Human_HeroPower_03, VO_DALA_BOSS_74h_Female_Human_HeroPower_04, VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_01,
			VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_02, VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_01, VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_02, VO_DALA_BOSS_74h_Female_Human_Idle_01, VO_DALA_BOSS_74h_Female_Human_Idle_02, VO_DALA_BOSS_74h_Female_Human_Idle_03, VO_DALA_BOSS_74h_Female_Human_Intro_01, VO_DALA_BOSS_74h_Female_Human_PlayerDemon_01, VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_01, VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_02
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
		m_introLine = VO_DALA_BOSS_74h_Female_Human_Intro_01;
		m_deathLine = VO_DALA_BOSS_74h_Female_Human_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_74h_Female_Human_EmoteResponse_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
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
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossHeroPowerSmall);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossHeroPower);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossHeroPowerLarge);
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_74h_Female_Human_BossEmptyHand_01);
			break;
		case 105:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_74h_Female_Human_PlayerDemon_01);
			break;
		case 106:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerDiscard);
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
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "EX1_308")
			{
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_74h_Female_Human_BossSoulfire_01);
			}
		}
	}
}
