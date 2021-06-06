using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_49h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerEVILRecruiter_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerEVILRecruiter_01.prefab:21f99f609007e6548b8fc12e98083aea");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerLackey_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerLackey_01.prefab:e5d705e8a24b0824e85f98750ee2ce35");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerRiftcleaver_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerRiftcleaver_01.prefab:bb1c5092a9fc0aa46bce66ff0676a384");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_Death_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_Death_01.prefab:bb40cacc9d30f774aaf352dd9bd33305");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_DefeatPlayer_01.prefab:43188fc4e15678a459c842f28a30b6c5");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_EmoteResponse_01.prefab:bcd2ac1cf3a3ba54986b7b11ffc73be5");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_01.prefab:57ea8d1ad71085844a362352f137d4a1");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_02.prefab:36491b19f6be8af488dc9f8f8c22dbe1");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_03.prefab:f1ca1e97b6e5c7746b737c1918f56bfe");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_04.prefab:8f0dbe3a39e3300489baf5bda9edac3b");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_05.prefab:fa344fe57bdb4234590881301f2c62b2");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_01.prefab:d6cbf671596a90148a1694b45995841a");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_02 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_02.prefab:4714919e5e5762346a295638ed7ecc9d");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_03 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_03.prefab:c42c3adfb3a41384da298a85f557935d");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_Intro_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_Intro_01.prefab:359a118384be6294e93ecdf9c79235e5");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_IntroResponse_Reno_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_IntroResponse_Reno_01.prefab:e66e97b6a2fa11449b689d1575c97b16");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Golden_Scarab_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Golden_Scarab_01.prefab:bd6dd8268e630984c81089cba724fdb7");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Vilefiend_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Vilefiend_01.prefab:a255d1eb5c4d4c04a9aefc36e365ba9a");

	private static readonly AssetReference VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTriggerSackofLamps_01 = new AssetReference("VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTriggerSackofLamps_01.prefab:56336d6ac94c70a46903d13a417e23c0");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_01, VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_02, VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_03, VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_04, VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_01, VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_02, VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerEVILRecruiter_01, VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerLackey_01, VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerRiftcleaver_01, VO_ULDA_BOSS_49h_Male_Wyrmtongue_Death_01, VO_ULDA_BOSS_49h_Male_Wyrmtongue_DefeatPlayer_01, VO_ULDA_BOSS_49h_Male_Wyrmtongue_EmoteResponse_01, VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_01, VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_02, VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_03, VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_04,
			VO_ULDA_BOSS_49h_Male_Wyrmtongue_HeroPower_05, VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_01, VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_02, VO_ULDA_BOSS_49h_Male_Wyrmtongue_Idle_03, VO_ULDA_BOSS_49h_Male_Wyrmtongue_Intro_01, VO_ULDA_BOSS_49h_Male_Wyrmtongue_IntroResponse_Reno_01, VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Golden_Scarab_01, VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Vilefiend_01, VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTriggerSackofLamps_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_49h_Male_Wyrmtongue_Intro_01;
		m_deathLine = VO_ULDA_BOSS_49h_Male_Wyrmtongue_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_49h_Male_Wyrmtongue_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Reno")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_49h_Male_Wyrmtongue_IntroResponse_Reno_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ULDA_040":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTriggerSackofLamps_01);
				break;
			case "ULD_188":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Golden_Scarab_01);
				break;
			case "ULD_450":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_49h_Male_Wyrmtongue_PlayerTrigger_Vilefiend_01);
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
			case "ULD_162":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerEVILRecruiter_01);
				break;
			case "DAL_413":
			case "DAL_613":
			case "DAL_741":
			case "DAL_615":
			case "DAL_614":
			case "DAL_739":
			case "ULD_616":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerLackey_01);
				break;
			case "ULD_165":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_49h_Male_Wyrmtongue_BossTriggerRiftcleaver_01);
				break;
			}
		}
	}
}
