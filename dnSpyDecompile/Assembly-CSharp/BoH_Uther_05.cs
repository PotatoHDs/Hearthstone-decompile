using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200054E RID: 1358
public class BoH_Uther_05 : BoH_Uther_Dungeon
{
	// Token: 0x06004ACE RID: 19150 RVA: 0x0016DED1 File Offset: 0x0016C0D1
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			}
		};
	}

	// Token: 0x06004ACF RID: 19151 RVA: 0x0018D3CC File Offset: 0x0018B5CC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Death_01,
			BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5EmoteResponse_01,
			BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_01,
			BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_02,
			BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_03,
			BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_01,
			BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_02,
			BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_03,
			BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Loss_01,
			BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Intro_01,
			BoH_Uther_05.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeA_01,
			BoH_Uther_05.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeB_01,
			BoH_Uther_05.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeC_01,
			BoH_Uther_05.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5Victory_01,
			BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_01,
			BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_02,
			BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeB_01,
			BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_01,
			BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_02,
			BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_01,
			BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_02,
			BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_01,
			BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004AD0 RID: 19152 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004AD1 RID: 19153 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004AD2 RID: 19154 RVA: 0x0018D5A0 File Offset: 0x0018B7A0
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(friendlyActor, BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_02, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004AD3 RID: 19155 RVA: 0x0018D5AF File Offset: 0x0018B7AF
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004AD4 RID: 19156 RVA: 0x0018D5B7 File Offset: 0x0018B7B7
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004AD5 RID: 19157 RVA: 0x0018D5BF File Offset: 0x0018B7BF
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5EmoteResponse_01;
	}

	// Token: 0x06004AD6 RID: 19158 RVA: 0x0018D5D8 File Offset: 0x0018B7D8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004AD7 RID: 19159 RVA: 0x0018D661 File Offset: 0x0018B861
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 501)
		{
			if (missionEvent == 504)
			{
				yield return base.PlayLineAlways(actor, BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Loss_01, 2.5f);
			}
			else
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Uther_05.ArthasBrassRing, BoH_Uther_05.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_02, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004AD8 RID: 19160 RVA: 0x0018D677 File Offset: 0x0018B877
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004AD9 RID: 19161 RVA: 0x0018D68D File Offset: 0x0018B88D
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004ADA RID: 19162 RVA: 0x0018D6A3 File Offset: 0x0018B8A3
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 3)
		{
			if (turn != 7)
			{
				if (turn == 13)
				{
					yield return base.PlayLineAlways(friendlyActor, BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(BoH_Uther_05.ArthasBrassRing, BoH_Uther_05.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_02, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(BoH_Uther_05.ArthasBrassRing, BoH_Uther_05.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(BoH_Uther_05.ArthasBrassRing, BoH_Uther_05.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_05.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004ADB RID: 19163 RVA: 0x0017D1BC File Offset: 0x0017B3BC
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BRMAdventure);
	}

	// Token: 0x04003F2F RID: 16175
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Uther_05.InitBooleanOptions();

	// Token: 0x04003F30 RID: 16176
	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Death_01 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Death_01.prefab:54906235720dd1a4b8eb19b637d7d5e7");

	// Token: 0x04003F31 RID: 16177
	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5EmoteResponse_01 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5EmoteResponse_01.prefab:60d461e053787cf4db229e217b16340a");

	// Token: 0x04003F32 RID: 16178
	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_01 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_01.prefab:5e3816881f0f7b849a61f6a2289ee5b9");

	// Token: 0x04003F33 RID: 16179
	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_02 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_02.prefab:7478ea3636febc64b937e471423d3a2e");

	// Token: 0x04003F34 RID: 16180
	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_03 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_03.prefab:8e3369e156e1d1541a13df4a4f90f741");

	// Token: 0x04003F35 RID: 16181
	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_01 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_01.prefab:9f989fd48cf9cc54ca0df8c4edb99fef");

	// Token: 0x04003F36 RID: 16182
	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_02 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_02.prefab:eb437c7c6810c264681bfcb2e28f98f0");

	// Token: 0x04003F37 RID: 16183
	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_03 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_03.prefab:a4450e194f14d4c4a8ecdf7ef0cbb155");

	// Token: 0x04003F38 RID: 16184
	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Loss_01 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Loss_01.prefab:abc3a388ca7084446a26ca173afd0bfa");

	// Token: 0x04003F39 RID: 16185
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeA_01.prefab:bda83977e08ef5a42838259dd60c3e87");

	// Token: 0x04003F3A RID: 16186
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeB_01.prefab:e3c76fcb46d2ffa4fa6b134ea0275142");

	// Token: 0x04003F3B RID: 16187
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeC_01.prefab:fd73050bdc03ee64dbd4a7b865b7a2a3");

	// Token: 0x04003F3C RID: 16188
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5Victory_01.prefab:e325d9f1482854343a13118882774668");

	// Token: 0x04003F3D RID: 16189
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_01.prefab:8cf590f8fcdd88940a9db94d6891e0ce");

	// Token: 0x04003F3E RID: 16190
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_02.prefab:8afe9c6a2e2b20546ab5da1e9e565f8b");

	// Token: 0x04003F3F RID: 16191
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeB_01.prefab:bd82767ee0e8e2140a35734ab213c2cf");

	// Token: 0x04003F40 RID: 16192
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_01.prefab:8e22b7a203c97d84c92ed1bf18069f86");

	// Token: 0x04003F41 RID: 16193
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_02.prefab:92720addc1a4e564fb9e62f589ac7762");

	// Token: 0x04003F42 RID: 16194
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_01.prefab:2766583d1f91d4c4bbb9eb725d6d21e5");

	// Token: 0x04003F43 RID: 16195
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_02.prefab:a66067c97bc5fee4ead7a451ec7d8e8b");

	// Token: 0x04003F44 RID: 16196
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_01.prefab:419fac51d65b28d4ba8e250d6c96dc21");

	// Token: 0x04003F45 RID: 16197
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_02.prefab:af77db00c5d636f4a888fb81386cf23b");

	// Token: 0x04003F46 RID: 16198
	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Intro_01 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Intro_01.prefab:f5ab5ede04b896744b4bfe5eca00f60f");

	// Token: 0x04003F47 RID: 16199
	public static readonly AssetReference ArthasBrassRing = new AssetReference("Arthas_BrassRing_Quote.prefab:49bb0ac905ae3c54cbf3624451b62ab4");

	// Token: 0x04003F48 RID: 16200
	private List<string> m_HeroPowerLines = new List<string>
	{
		BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_01,
		BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_02,
		BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_03
	};

	// Token: 0x04003F49 RID: 16201
	private List<string> m_IdleLines = new List<string>
	{
		BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_01,
		BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_02,
		BoH_Uther_05.VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_03
	};

	// Token: 0x04003F4A RID: 16202
	private HashSet<string> m_playedLines = new HashSet<string>();
}
