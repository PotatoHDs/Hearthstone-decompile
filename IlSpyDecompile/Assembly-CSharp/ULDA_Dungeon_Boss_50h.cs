using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_50h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_BossHyenaAlpha_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_BossHyenaAlpha_01.prefab:cf30112d4814caf43a0b382397f28fd7");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_BossKoboldSandtrooper_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_BossKoboldSandtrooper_01.prefab:709a5199fa8579a4c95acfc00b4a4f38");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_BossMarkedShot_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_BossMarkedShot_01.prefab:b3a000ae6f5784a4ca32df9b966aff01");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_DeathALT_01.prefab:8d07bcbe136dc76468943479e8c9cc00");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_DefeatPlayer_01.prefab:47ee05a5ccb26cb44985a261a88e34f6");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_EmoteResponse_01.prefab:1b453b9dc0b9088469267002499654a8");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_01.prefab:0179726d05f3fdf43a0ac711d17eb756");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_02.prefab:d1e0cffd867979e41af4962c08bd8d61");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_03.prefab:6641bf43c63d3414cbc04751abcbbf13");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_04.prefab:6b0869d7cd1846f4cbb875f2e0422553");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_05.prefab:20197153fb4e7b34bb54f8186cedbed9");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_Idle_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_Idle_01.prefab:cceebdf2900f871498e93a209485865b");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_Idle_02 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_Idle_02.prefab:e212f90192f2eb349b2da0d0c0202888");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_Idle_03 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_Idle_03.prefab:46c802af5c80a8d4e85b0c5c8adcec31");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_Intro_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_Intro_01.prefab:f2574e701b4bf29428035e8200398500");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_IntroBrann_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_IntroBrann_01.prefab:47009177b2f223943a96e9ece415a063");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_IntroFinley_01.prefab:7a69e245b94c09542b44d9d1aea884e3");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_PlayerBlunderbussTreasure_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_PlayerBlunderbussTreasure_01.prefab:70670ae1a682d5d448b93e26974153a5");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_PlayerSwarmofLocusts_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_PlayerSwarmofLocusts_01.prefab:bb405a98bf5a4834fb17c51385d15a85");

	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_PlayerUntamedBeastmaster_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_PlayerUntamedBeastmaster_01.prefab:80c19b52f5284854190add70b9ba2f85");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_01, VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_02, VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_03, VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_04, VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_50h_Male_Gnoll_Idle_01, VO_ULDA_BOSS_50h_Male_Gnoll_Idle_02, VO_ULDA_BOSS_50h_Male_Gnoll_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_50h_Male_Gnoll_BossHyenaAlpha_01, VO_ULDA_BOSS_50h_Male_Gnoll_BossKoboldSandtrooper_01, VO_ULDA_BOSS_50h_Male_Gnoll_BossMarkedShot_01, VO_ULDA_BOSS_50h_Male_Gnoll_DeathALT_01, VO_ULDA_BOSS_50h_Male_Gnoll_DefeatPlayer_01, VO_ULDA_BOSS_50h_Male_Gnoll_EmoteResponse_01, VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_01, VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_02, VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_03, VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_04,
			VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_05, VO_ULDA_BOSS_50h_Male_Gnoll_Idle_01, VO_ULDA_BOSS_50h_Male_Gnoll_Idle_02, VO_ULDA_BOSS_50h_Male_Gnoll_Idle_03, VO_ULDA_BOSS_50h_Male_Gnoll_Intro_01, VO_ULDA_BOSS_50h_Male_Gnoll_IntroBrann_01, VO_ULDA_BOSS_50h_Male_Gnoll_IntroFinley_01, VO_ULDA_BOSS_50h_Male_Gnoll_PlayerBlunderbussTreasure_01, VO_ULDA_BOSS_50h_Male_Gnoll_PlayerSwarmofLocusts_01, VO_ULDA_BOSS_50h_Male_Gnoll_PlayerUntamedBeastmaster_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_50h_Male_Gnoll_Intro_01;
		m_deathLine = VO_ULDA_BOSS_50h_Male_Gnoll_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_50h_Male_Gnoll_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_50h_Male_Gnoll_IntroBrann_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_50h_Male_Gnoll_IntroFinley_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (missionEvent == 101)
		{
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerLines);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ULDA_401":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_50h_Male_Gnoll_PlayerBlunderbussTreasure_01);
				break;
			case "ULD_713":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_50h_Male_Gnoll_PlayerSwarmofLocusts_01);
				break;
			case "TRL_405":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_50h_Male_Gnoll_PlayerUntamedBeastmaster_01);
				break;
			}
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ULD_154":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_50h_Male_Gnoll_BossHyenaAlpha_01);
				break;
			case "ULD_184":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_50h_Male_Gnoll_BossKoboldSandtrooper_01);
				break;
			case "DAL_371":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_50h_Male_Gnoll_BossMarkedShot_01);
				break;
			}
		}
	}
}
