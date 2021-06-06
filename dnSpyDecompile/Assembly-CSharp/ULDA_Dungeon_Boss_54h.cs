using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004B3 RID: 1203
public class ULDA_Dungeon_Boss_54h : ULDA_Dungeon
{
	// Token: 0x060040B4 RID: 16564 RVA: 0x00159594 File Offset: 0x00157794
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_BossLackey_01,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_BossTriggerHenchClanHogsteed_01,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_BossTriggerSunstruckHenchman_01,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_Death_01,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_DefeatPlayer_01,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_EmoteResponse_01,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_01,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_02,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_03,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_04,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_05,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_Idle_01,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_Idle_02,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_Idle_03,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_Intro_01,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_IntroSpecialElise_01,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_IntroSpecialFinley_01,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_PlayerTrigger_BEEEES_01,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_PlayerTrigger_Blatant_Decoy_01,
			ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_PlayerTrigger_Frightened_Flunky_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060040B5 RID: 16565 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060040B6 RID: 16566 RVA: 0x00159738 File Offset: 0x00157938
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x060040B7 RID: 16567 RVA: 0x00159740 File Offset: 0x00157940
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x060040B8 RID: 16568 RVA: 0x00159748 File Offset: 0x00157948
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_EmoteResponse_01;
	}

	// Token: 0x060040B9 RID: 16569 RVA: 0x00159780 File Offset: 0x00157980
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_Intro_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_IntroSpecialFinley_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		else
		{
			if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x060040BA RID: 16570 RVA: 0x00159899 File Offset: 0x00157A99
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x060040BB RID: 16571 RVA: 0x001598AF File Offset: 0x00157AAF
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "ULD_706"))
		{
			if (!(cardId == "ULD_195"))
			{
				if (cardId == "ULD_134")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_PlayerTrigger_BEEEES_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_PlayerTrigger_Frightened_Flunky_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_PlayerTrigger_Blatant_Decoy_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060040BC RID: 16572 RVA: 0x001598C5 File Offset: 0x00157AC5
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
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 3598724010U)
		{
			if (num <= 1577821516U)
			{
				if (num != 845142957U)
				{
					if (num != 1577821516U)
					{
						goto IL_2E5;
					}
					if (!(cardId == "ULD_180"))
					{
						goto IL_2E5;
					}
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_BossTriggerSunstruckHenchman_01, 2.5f);
					goto IL_2E5;
				}
				else if (!(cardId == "DAL_413"))
				{
					goto IL_2E5;
				}
			}
			else if (num != 3514835915U)
			{
				if (num != 3598724010U)
				{
					goto IL_2E5;
				}
				if (!(cardId == "DAL_614"))
				{
					goto IL_2E5;
				}
			}
			else if (!(cardId == "DAL_613"))
			{
				goto IL_2E5;
			}
		}
		else if (num <= 3785628939U)
		{
			if (num != 3615501629U)
			{
				if (num != 3785628939U)
				{
					goto IL_2E5;
				}
				if (!(cardId == "DAL_741"))
				{
					goto IL_2E5;
				}
			}
			else if (!(cardId == "DAL_615"))
			{
				goto IL_2E5;
			}
		}
		else if (num != 3786761772U)
		{
			if (num != 3819184177U)
			{
				if (num != 3995164034U)
				{
					goto IL_2E5;
				}
				if (!(cardId == "ULD_616"))
				{
					goto IL_2E5;
				}
			}
			else
			{
				if (!(cardId == "DAL_743"))
				{
					goto IL_2E5;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_BossTriggerHenchClanHogsteed_01, 2.5f);
				goto IL_2E5;
			}
		}
		else if (!(cardId == "DAL_739"))
		{
			goto IL_2E5;
		}
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_BossLackey_01, 2.5f);
		IL_2E5:
		yield break;
	}

	// Token: 0x04002EE7 RID: 12007
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_BossLackey_01 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_BossLackey_01.prefab:05f62193a9aad884088fdb5924491605");

	// Token: 0x04002EE8 RID: 12008
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_BossTriggerHenchClanHogsteed_01 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_BossTriggerHenchClanHogsteed_01.prefab:d7a1489340d054e40b25873b8693a2a3");

	// Token: 0x04002EE9 RID: 12009
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_BossTriggerSunstruckHenchman_01 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_BossTriggerSunstruckHenchman_01.prefab:da0a9785deaf7664d9cfc855da1965da");

	// Token: 0x04002EEA RID: 12010
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_Death_01 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_Death_01.prefab:6449ae72adcdba545be6a15582f71d51");

	// Token: 0x04002EEB RID: 12011
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_DefeatPlayer_01.prefab:071b68bbe6c05034e9ba3e5cd6699c4f");

	// Token: 0x04002EEC RID: 12012
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_EmoteResponse_01.prefab:b1cc725f42940474ab28afbde2f94442");

	// Token: 0x04002EED RID: 12013
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_01.prefab:a13be679590dbb4438d5fdf15fa7f399");

	// Token: 0x04002EEE RID: 12014
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_02.prefab:5c9c0bad4fcf4ac458544755791f3ff2");

	// Token: 0x04002EEF RID: 12015
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_03.prefab:7c2efe5ff6df8ee45a5e8de3b28072d5");

	// Token: 0x04002EF0 RID: 12016
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_04.prefab:80f19bff38adc1d44b0d5346bc9dea51");

	// Token: 0x04002EF1 RID: 12017
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_05.prefab:327381c0ac46c204884526b2cdb4b15b");

	// Token: 0x04002EF2 RID: 12018
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_Idle_01 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_Idle_01.prefab:5bbd069bf7f94d64e8e314b772c78a3e");

	// Token: 0x04002EF3 RID: 12019
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_Idle_02 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_Idle_02.prefab:513a143c16b1621479376d7675282347");

	// Token: 0x04002EF4 RID: 12020
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_Idle_03 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_Idle_03.prefab:836ab9ea69c499c4d9cfe47bc2e168ee");

	// Token: 0x04002EF5 RID: 12021
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_Intro_01 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_Intro_01.prefab:93c4480526367f841ba9c3ff78dac470");

	// Token: 0x04002EF6 RID: 12022
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_IntroSpecialElise_01 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_IntroSpecialElise_01.prefab:cb6b9e08404a3f14eaa557b9dd66e539");

	// Token: 0x04002EF7 RID: 12023
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_IntroSpecialFinley_01 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_IntroSpecialFinley_01.prefab:da4f6b2e4d54dbc4994b883bd431e417");

	// Token: 0x04002EF8 RID: 12024
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_PlayerTrigger_BEEEES_01 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_PlayerTrigger_BEEEES_01.prefab:55d6654af49ecf44f9d50859623d098b");

	// Token: 0x04002EF9 RID: 12025
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_PlayerTrigger_Blatant_Decoy_01 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_PlayerTrigger_Blatant_Decoy_01.prefab:b723694ed90bca645bbd037454fdbff6");

	// Token: 0x04002EFA RID: 12026
	private static readonly AssetReference VO_ULDA_BOSS_54h_Female_Hozen_PlayerTrigger_Frightened_Flunky_01 = new AssetReference("VO_ULDA_BOSS_54h_Female_Hozen_PlayerTrigger_Frightened_Flunky_01.prefab:6f7871a9b2658a6419172dba05b25df3");

	// Token: 0x04002EFB RID: 12027
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_01,
		ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_02,
		ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_03,
		ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_04,
		ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_HeroPower_05
	};

	// Token: 0x04002EFC RID: 12028
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_Idle_01,
		ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_Idle_02,
		ULDA_Dungeon_Boss_54h.VO_ULDA_BOSS_54h_Female_Hozen_Idle_03
	};

	// Token: 0x04002EFD RID: 12029
	private HashSet<string> m_playedLines = new HashSet<string>();
}
