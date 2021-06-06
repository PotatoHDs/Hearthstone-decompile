using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000440 RID: 1088
public class DALA_Dungeon_Boss_19h : DALA_Dungeon
{
	// Token: 0x06003B3A RID: 15162 RVA: 0x001336D4 File Offset: 0x001318D4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_BossCombo_01,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_BossCombo_02,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_BossEdwin_01,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_Death_01,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_DefeatPlayer_01,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_EmoteResponse_01,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_HeroPower_01,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_HeroPower_03,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_HeroPower_04,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_HeroPowerCombo_01,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_HeroPowerCombo_02,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_HeroPowerCombo_03,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_Idle_01,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_Idle_02,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_Idle_03,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_Intro_01,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_IntroChu_01,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_PlayerEviscerate_01,
			DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_PlayerSmallWeapon_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003B3B RID: 15163 RVA: 0x00133868 File Offset: 0x00131A68
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_EmoteResponse_01;
	}

	// Token: 0x06003B3C RID: 15164 RVA: 0x001338A0 File Offset: 0x00131AA0
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_19h.m_IdleLines;
	}

	// Token: 0x06003B3D RID: 15165 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003B3E RID: 15166 RVA: 0x001338A8 File Offset: 0x00131AA8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_IntroChu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003B3F RID: 15167 RVA: 0x00133982 File Offset: 0x00131B82
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_19h.m_BossHeroPower);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_19h.m_BossHeroPowerCombo);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_19h.m_BossCombo);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_PlayerSmallWeapon_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003B40 RID: 15168 RVA: 0x00133998 File Offset: 0x00131B98
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
		if (cardId == "EX1_124")
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_PlayerEviscerate_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003B41 RID: 15169 RVA: 0x001339AE File Offset: 0x00131BAE
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
		if (cardId == "EX1_613")
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_BossEdwin_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040023BC RID: 9148
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_BossCombo_01 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_BossCombo_01.prefab:148da7870579aa64bad30a0f94ce5d50");

	// Token: 0x040023BD RID: 9149
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_BossCombo_02 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_BossCombo_02.prefab:4307fbbaea5777642b157165bb52b308");

	// Token: 0x040023BE RID: 9150
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_BossEdwin_01 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_BossEdwin_01.prefab:3d2895ec7e3364943b405a8afa8b31ee");

	// Token: 0x040023BF RID: 9151
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_Death_01 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_Death_01.prefab:507e0991497a9334182687b0bfd36526");

	// Token: 0x040023C0 RID: 9152
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_DefeatPlayer_01.prefab:79de80e97ed27b64889c5e2881a11372");

	// Token: 0x040023C1 RID: 9153
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_EmoteResponse_01.prefab:356dadca5fdb91844a6b3a65d0f101b4");

	// Token: 0x040023C2 RID: 9154
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_HeroPower_01 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_HeroPower_01.prefab:57c3195682cd0b8428e697506dc54a09");

	// Token: 0x040023C3 RID: 9155
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_HeroPower_03 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_HeroPower_03.prefab:6b2faed3e88a04543b840434226f3de2");

	// Token: 0x040023C4 RID: 9156
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_HeroPower_04 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_HeroPower_04.prefab:b6e56caae9bddcb41a88b9508c810d26");

	// Token: 0x040023C5 RID: 9157
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_HeroPowerCombo_01 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_HeroPowerCombo_01.prefab:aaf90a661d82ad04aa5bdc72908e3755");

	// Token: 0x040023C6 RID: 9158
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_HeroPowerCombo_02 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_HeroPowerCombo_02.prefab:1b52a3264a3418849a4670fa9515cb15");

	// Token: 0x040023C7 RID: 9159
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_HeroPowerCombo_03 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_HeroPowerCombo_03.prefab:65fd4d2daa95df64086baf8446b38ffd");

	// Token: 0x040023C8 RID: 9160
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_Idle_01 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_Idle_01.prefab:acc43c6c339944a43a542b601e2f43e8");

	// Token: 0x040023C9 RID: 9161
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_Idle_02 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_Idle_02.prefab:278ac34ab6e3f514eb5c31a02c1541d4");

	// Token: 0x040023CA RID: 9162
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_Idle_03 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_Idle_03.prefab:9a7377bbcad54fa47a29bba0cd701310");

	// Token: 0x040023CB RID: 9163
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_Intro_01 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_Intro_01.prefab:137ac6ab94358c643be79969820efde1");

	// Token: 0x040023CC RID: 9164
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_IntroChu_01 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_IntroChu_01.prefab:08292dcf1d7b8ac45a5e26d4f36f6bda");

	// Token: 0x040023CD RID: 9165
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_PlayerEviscerate_01 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_PlayerEviscerate_01.prefab:ca0f6813553528e42b2eac6126c83313");

	// Token: 0x040023CE RID: 9166
	private static readonly AssetReference VO_DALA_BOSS_19h_Female_Gnome_PlayerSmallWeapon_01 = new AssetReference("VO_DALA_BOSS_19h_Female_Gnome_PlayerSmallWeapon_01.prefab:3665f70f588f88442b925746edff3f02");

	// Token: 0x040023CF RID: 9167
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_Idle_01,
		DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_Idle_02,
		DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_Idle_03
	};

	// Token: 0x040023D0 RID: 9168
	private static List<string> m_BossHeroPower = new List<string>
	{
		DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_HeroPower_01,
		DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_HeroPower_03,
		DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_HeroPower_04
	};

	// Token: 0x040023D1 RID: 9169
	private static List<string> m_BossHeroPowerCombo = new List<string>
	{
		DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_HeroPowerCombo_01,
		DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_HeroPowerCombo_02,
		DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_HeroPowerCombo_03
	};

	// Token: 0x040023D2 RID: 9170
	private static List<string> m_BossCombo = new List<string>
	{
		DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_BossCombo_01,
		DALA_Dungeon_Boss_19h.VO_DALA_BOSS_19h_Female_Gnome_BossCombo_02
	};

	// Token: 0x040023D3 RID: 9171
	private HashSet<string> m_playedLines = new HashSet<string>();
}
