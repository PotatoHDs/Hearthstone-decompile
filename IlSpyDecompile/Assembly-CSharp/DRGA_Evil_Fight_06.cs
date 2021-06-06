using System.Collections;
using System.Collections.Generic;

public class DRGA_Evil_Fight_06 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_01_Hero_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_01_Hero_01.prefab:1e77e58420152954cb8f992316781641");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_02_Hero_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_02_Hero_01.prefab:a299fe62b2b4d9c4681c14d233cb116d");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_03_Hero_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_03_Hero_01.prefab:e6fe2c06d42869948b2f199d7b8e35fc");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_04_Hero_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_04_Hero_01.prefab:fe7322a8524c8a04c93b81343d0c81fe");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_Death_01.prefab:8b5f623bfdae0eb4586f6f26c9b39b17");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_01_01.prefab:fb16c2cf7eb49bd47a082a1316132cae");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_02_01.prefab:399bb39311d39144ca24d5f4221a7f6d");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_03_01.prefab:f345f31d4b7afc14db4a03c56075ef22");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossAttack_01.prefab:59668ab65f84f1347bac2804b4a04938");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossStart_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossStart_01.prefab:27c92a7e94f477343a41e6866177e9a9");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_EmoteResponse_01.prefab:39837a46025b95b4db470e0ed10c736a");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_01_01.prefab:d0bcf0d932df5e34c8c4ba6854baa70e");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_02_01.prefab:57c4b14887921ac428a69ee33dc0cefa");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_03_01.prefab:aef088329669647478739e69c50f85cd");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_01_01.prefab:7263129d4874a524f9afa365a2807dd5");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_02_01.prefab:e77c6c05cc7228a4d8b0b824f5d72ff8");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_03_01.prefab:df495527fec0e7a4084e37132d4d0fec");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04a_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04a_01.prefab:d548adc9c897790468a03798a16c9456");

	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04b_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04b_01.prefab:d5f1ea51608702b44ac34baf517a4359");

	private List<string> m_VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPowerLines = new List<string> { VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_01_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_02_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_03_01 };

	private List<string> m_VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_IdleLines = new List<string> { VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_01_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_02_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_03_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_01_Hero_01, VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_02_Hero_01, VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_03_Hero_01, VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_04_Hero_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_Death_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_01_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_02_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_03_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossAttack_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossStart_01,
			VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_EmoteResponse_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_01_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_02_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_03_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_01_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_02_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_03_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04a_01, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04b_01
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
		return m_VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_Death_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 102:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_02_Hero_01);
			}
			break;
		case 103:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_03_Hero_01);
			}
			break;
		case 104:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_04_Hero_01);
			}
			break;
		case 105:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_01_01);
			}
			break;
		case 107:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_03_01);
			}
			break;
		case 108:
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04a_01);
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04b_01);
			break;
		case 110:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossAttack_01);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (cardId == "DRGA_BOSS_20t")
		{
			yield return PlayLineAlways(actor, VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_02_01);
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_01_Hero_01);
			}
		}
	}
}
