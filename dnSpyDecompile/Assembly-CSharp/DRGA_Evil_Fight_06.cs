using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004D4 RID: 1236
public class DRGA_Evil_Fight_06 : DRGA_Dungeon
{
	// Token: 0x06004234 RID: 16948 RVA: 0x00163218 File Offset: 0x00161418
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_01_Hero_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_02_Hero_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_03_Hero_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_04_Hero_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_Death_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_01_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_02_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_03_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossAttack_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossStart_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_EmoteResponse_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_01_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_02_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_03_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_01_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_02_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_03_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04a_01,
			DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04b_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004235 RID: 16949 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004236 RID: 16950 RVA: 0x001633AC File Offset: 0x001615AC
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_IdleLines;
	}

	// Token: 0x06004237 RID: 16951 RVA: 0x001633B4 File Offset: 0x001615B4
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPowerLines;
	}

	// Token: 0x06004238 RID: 16952 RVA: 0x001633BC File Offset: 0x001615BC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_Death_01;
	}

	// Token: 0x06004239 RID: 16953 RVA: 0x001633D4 File Offset: 0x001615D4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600423A RID: 16954 RVA: 0x00163465 File Offset: 0x00161665
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 102:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_02_Hero_01, 2.5f);
				goto IL_2CD;
			}
			goto IL_2CD;
		case 103:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_03_Hero_01, 2.5f);
				goto IL_2CD;
			}
			goto IL_2CD;
		case 104:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_04_Hero_01, 2.5f);
				goto IL_2CD;
			}
			goto IL_2CD;
		case 105:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_01_01, 2.5f);
				goto IL_2CD;
			}
			goto IL_2CD;
		case 107:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_03_01, 2.5f);
				goto IL_2CD;
			}
			goto IL_2CD;
		case 108:
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04a_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04b_01, 2.5f);
			goto IL_2CD;
		case 110:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossAttack_01, 2.5f);
			goto IL_2CD;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_2CD:
		yield break;
	}

	// Token: 0x0600423B RID: 16955 RVA: 0x0016347B File Offset: 0x0016167B
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		yield break;
	}

	// Token: 0x0600423C RID: 16956 RVA: 0x00163491 File Offset: 0x00161691
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "DRGA_BOSS_20t")
		{
			yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_02_01, 2.5f);
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_06.VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_01_Hero_01, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x040031D6 RID: 12758
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_01_Hero_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_01_Hero_01.prefab:1e77e58420152954cb8f992316781641");

	// Token: 0x040031D7 RID: 12759
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_02_Hero_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_02_Hero_01.prefab:a299fe62b2b4d9c4681c14d233cb116d");

	// Token: 0x040031D8 RID: 12760
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_03_Hero_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_03_Hero_01.prefab:e6fe2c06d42869948b2f199d7b8e35fc");

	// Token: 0x040031D9 RID: 12761
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_04_Hero_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Evil_Fight_06_Misc_04_Hero_01.prefab:fe7322a8524c8a04c93b81343d0c81fe");

	// Token: 0x040031DA RID: 12762
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_Death_01.prefab:8b5f623bfdae0eb4586f6f26c9b39b17");

	// Token: 0x040031DB RID: 12763
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_01_01.prefab:fb16c2cf7eb49bd47a082a1316132cae");

	// Token: 0x040031DC RID: 12764
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_02_01.prefab:399bb39311d39144ca24d5f4221a7f6d");

	// Token: 0x040031DD RID: 12765
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_03_01.prefab:f345f31d4b7afc14db4a03c56075ef22");

	// Token: 0x040031DE RID: 12766
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossAttack_01.prefab:59668ab65f84f1347bac2804b4a04938");

	// Token: 0x040031DF RID: 12767
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossStart_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_BossStart_01.prefab:27c92a7e94f477343a41e6866177e9a9");

	// Token: 0x040031E0 RID: 12768
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_EmoteResponse_01.prefab:39837a46025b95b4db470e0ed10c736a");

	// Token: 0x040031E1 RID: 12769
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_01_01.prefab:d0bcf0d932df5e34c8c4ba6854baa70e");

	// Token: 0x040031E2 RID: 12770
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_02_01.prefab:57c4b14887921ac428a69ee33dc0cefa");

	// Token: 0x040031E3 RID: 12771
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_03_01.prefab:aef088329669647478739e69c50f85cd");

	// Token: 0x040031E4 RID: 12772
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_01_01.prefab:7263129d4874a524f9afa365a2807dd5");

	// Token: 0x040031E5 RID: 12773
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_02_01.prefab:e77c6c05cc7228a4d8b0b824f5d72ff8");

	// Token: 0x040031E6 RID: 12774
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_03_01.prefab:df495527fec0e7a4084e37132d4d0fec");

	// Token: 0x040031E7 RID: 12775
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04a_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04a_01.prefab:d548adc9c897790468a03798a16c9456");

	// Token: 0x040031E8 RID: 12776
	private static readonly AssetReference VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04b_01 = new AssetReference("VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Misc_04b_01.prefab:d5f1ea51608702b44ac34baf517a4359");

	// Token: 0x040031E9 RID: 12777
	private List<string> m_VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPowerLines = new List<string>
	{
		DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_01_01,
		DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_02_01,
		DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Boss_HeroPower_03_01
	};

	// Token: 0x040031EA RID: 12778
	private List<string> m_VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_IdleLines = new List<string>
	{
		DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_01_01,
		DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_02_01,
		DRGA_Evil_Fight_06.VO_DRGA_BOSS_20h_Male_Dragon_Evil_Fight_06_Idle_03_01
	};

	// Token: 0x040031EB RID: 12779
	private HashSet<string> m_playedLines = new HashSet<string>();
}
