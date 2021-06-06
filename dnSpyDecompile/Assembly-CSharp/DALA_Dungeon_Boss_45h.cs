using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200045A RID: 1114
public class DALA_Dungeon_Boss_45h : DALA_Dungeon
{
	// Token: 0x06003C7E RID: 15486 RVA: 0x0013B688 File Offset: 0x00139888
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_BossBigMinion_01,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_BossDamageSpell_01,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_BossDemonBolt_01,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_Death_01,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_DefeatPlayer_01,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_EmoteResponse_01,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerHuge_01,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerHuge_02,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerLarge_01,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerLarge_03,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerMedium_02,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerMedium_03,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerMedium_04,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerSmall_01,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerSmall_02,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerSmall_03,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_Idle_01,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_Idle_02,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_Idle_03,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_Intro_01,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_IntroSqueamlish_01,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_PlayerEntomb_01,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_PlayerEyeforanEyeTrigger_01,
			DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_PlayerPatches_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003C7F RID: 15487 RVA: 0x0013B86C File Offset: 0x00139A6C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_EmoteResponse_01;
	}

	// Token: 0x06003C80 RID: 15488 RVA: 0x0013B8A4 File Offset: 0x00139AA4
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_45h.m_IdleLines;
	}

	// Token: 0x06003C81 RID: 15489 RVA: 0x0013B8AC File Offset: 0x00139AAC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Squeamlish")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_IntroSqueamlish_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Tekahn")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
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

	// Token: 0x06003C82 RID: 15490 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003C83 RID: 15491 RVA: 0x0013B993 File Offset: 0x00139B93
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_45h.m_HeroPowerHuge);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_45h.m_HeroPowerLarge);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_45h.m_HeroPowerMedium);
			break;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_45h.m_HeroPowerSmall);
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_BossBigMinion_01, 2.5f);
			break;
		case 106:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_PlayerEyeforanEyeTrigger_01, 2.5f);
			break;
		case 107:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_PlayerPatches_02, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003C84 RID: 15492 RVA: 0x0013B9A9 File Offset: 0x00139BA9
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (cardId == "LOE_104")
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_PlayerEntomb_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003C85 RID: 15493 RVA: 0x0013B9BF File Offset: 0x00139BBF
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "TRL_555"))
		{
			if (cardId == "CS2_062" || cardId == "EX1_312" || cardId == "OG_239")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_BossDamageSpell_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_BossDemonBolt_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002616 RID: 9750
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_BossBigMinion_01 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_BossBigMinion_01.prefab:c9b5c007adc1d47479cb63fb745440bd");

	// Token: 0x04002617 RID: 9751
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_BossDamageSpell_01 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_BossDamageSpell_01.prefab:103109527d0edec4d831219d9f912c62");

	// Token: 0x04002618 RID: 9752
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_BossDemonBolt_01 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_BossDemonBolt_01.prefab:fb60f2c2d816b1c4da1d0aab3efe871b");

	// Token: 0x04002619 RID: 9753
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_Death_01 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_Death_01.prefab:391eccc27cc158f4891c0137404b57b7");

	// Token: 0x0400261A RID: 9754
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_DefeatPlayer_01.prefab:4c90adfa1ac2a914883b037e84c53291");

	// Token: 0x0400261B RID: 9755
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_EmoteResponse_01.prefab:270ba001adc69d346b11e0da01575706");

	// Token: 0x0400261C RID: 9756
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_HeroPowerHuge_01 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_HeroPowerHuge_01.prefab:1273a7741e390404bbae704ecf0af063");

	// Token: 0x0400261D RID: 9757
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_HeroPowerHuge_02 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_HeroPowerHuge_02.prefab:e7d00fe290f2b2940828884cd589c5c2");

	// Token: 0x0400261E RID: 9758
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_HeroPowerLarge_01 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_HeroPowerLarge_01.prefab:6f85f31d6f01dad42ba7081167fee6fb");

	// Token: 0x0400261F RID: 9759
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_HeroPowerLarge_03 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_HeroPowerLarge_03.prefab:1f465cbe004558f44b721730368109cb");

	// Token: 0x04002620 RID: 9760
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_HeroPowerMedium_02 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_HeroPowerMedium_02.prefab:03f11ba113733f347970bfc6d66e9b07");

	// Token: 0x04002621 RID: 9761
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_HeroPowerMedium_03 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_HeroPowerMedium_03.prefab:9f2a8ed468c1a7b409ae4474a8445fa5");

	// Token: 0x04002622 RID: 9762
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_HeroPowerMedium_04 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_HeroPowerMedium_04.prefab:4c7091808ee85d54291ab51adef09946");

	// Token: 0x04002623 RID: 9763
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_HeroPowerSmall_01 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_HeroPowerSmall_01.prefab:acff87aa9f3b49540ab389e22db4ab00");

	// Token: 0x04002624 RID: 9764
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_HeroPowerSmall_02 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_HeroPowerSmall_02.prefab:3143292133cb7264da7ae438cb182bd7");

	// Token: 0x04002625 RID: 9765
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_HeroPowerSmall_03 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_HeroPowerSmall_03.prefab:2e01bacaf6f390a4fb6d037b45b1d405");

	// Token: 0x04002626 RID: 9766
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_Idle_01 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_Idle_01.prefab:b1e880467bb31424bbb4bfdcb399cde9");

	// Token: 0x04002627 RID: 9767
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_Idle_02 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_Idle_02.prefab:716973a1e23e5e347a8e4468a6b80fcf");

	// Token: 0x04002628 RID: 9768
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_Idle_03 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_Idle_03.prefab:23cdc76d6145d8a4b8c96d51a7677c6e");

	// Token: 0x04002629 RID: 9769
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_Intro_01 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_Intro_01.prefab:b648d91b63b48f245a62f9be0e5ddd0c");

	// Token: 0x0400262A RID: 9770
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_IntroSqueamlish_01.prefab:806ab394e027f4647bae43c9088712c6");

	// Token: 0x0400262B RID: 9771
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_PlayerEntomb_01 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_PlayerEntomb_01.prefab:431fe0b8b5e767240a1d59775c8b192c");

	// Token: 0x0400262C RID: 9772
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_PlayerEyeforanEyeTrigger_01 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_PlayerEyeforanEyeTrigger_01.prefab:b979c5b6460858942849bf99336fef9c");

	// Token: 0x0400262D RID: 9773
	private static readonly AssetReference VO_DALA_BOSS_45h_Male_Observer_PlayerPatches_02 = new AssetReference("VO_DALA_BOSS_45h_Male_Observer_PlayerPatches_02.prefab:528d096fa0569d845861f108881b39b2");

	// Token: 0x0400262E RID: 9774
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_Idle_01,
		DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_Idle_02,
		DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_Idle_03
	};

	// Token: 0x0400262F RID: 9775
	private static List<string> m_HeroPowerHuge = new List<string>
	{
		DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerHuge_01,
		DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerHuge_02
	};

	// Token: 0x04002630 RID: 9776
	private static List<string> m_HeroPowerLarge = new List<string>
	{
		DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerLarge_01,
		DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerLarge_03
	};

	// Token: 0x04002631 RID: 9777
	private static List<string> m_HeroPowerMedium = new List<string>
	{
		DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerMedium_02,
		DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerMedium_03,
		DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerMedium_04
	};

	// Token: 0x04002632 RID: 9778
	private static List<string> m_HeroPowerSmall = new List<string>
	{
		DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerSmall_01,
		DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerSmall_02,
		DALA_Dungeon_Boss_45h.VO_DALA_BOSS_45h_Male_Observer_HeroPowerSmall_03
	};

	// Token: 0x04002633 RID: 9779
	private HashSet<string> m_playedLines = new HashSet<string>();
}
