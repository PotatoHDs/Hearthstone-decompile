using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000439 RID: 1081
public class DALA_Dungeon_Boss_12h : DALA_Dungeon
{
	// Token: 0x06003AE1 RID: 15073 RVA: 0x001312E4 File Offset: 0x0012F4E4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_BossKoboldLibrarian_01,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_BossWarlockFire_01,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Death_01,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_DefeatPlayer_01,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_EmoteResponse_01,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_HeroPower_01,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_HeroPower_02,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_HeroPower_03,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_HeroPower_04,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_HeroPowerOne_01,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_HeroPowerOne_02,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_HeroPowerOne_03,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Idle_01,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Idle_02,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Idle_03,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Idle_04,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Idle_05,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Idle_09,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Intro_01,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_IntroChu_01,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_PlayerBabblingBook_01,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_PlayerBookOfSpectres_01,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_PlayerBookWyrm_01,
			DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_PlayerCabalistTome_01
		};
		base.SetBossVOLines(list);
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003AE2 RID: 15074 RVA: 0x001314CC File Offset: 0x0012F6CC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_EmoteResponse_01;
	}

	// Token: 0x06003AE3 RID: 15075 RVA: 0x00131504 File Offset: 0x0012F704
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (this.m_IdleSongLines.Count == 0)
		{
			return;
		}
		string line = this.m_IdleSongLines[0];
		this.m_IdleSongLines.RemoveAt(0);
		Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
	}

	// Token: 0x06003AE4 RID: 15076 RVA: 0x00131590 File Offset: 0x0012F790
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_George" && cardId != "DALA_Rakanishu" && cardId != "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003AE5 RID: 15077 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003AE6 RID: 15078 RVA: 0x00131655 File Offset: 0x0012F855
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 102)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerOne);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPower);
		}
		yield break;
	}

	// Token: 0x06003AE7 RID: 15079 RVA: 0x0013166B File Offset: 0x0012F86B
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
		if (!(cardId == "KAR_009"))
		{
			if (!(cardId == "GIL_548"))
			{
				if (!(cardId == "KAR_033"))
				{
					if (cardId == "OG_090")
					{
						yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_PlayerCabalistTome_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_PlayerBookWyrm_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_PlayerBookOfSpectres_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_PlayerBabblingBook_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003AE8 RID: 15080 RVA: 0x00131681 File Offset: 0x0012F881
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
		if (!(cardId == "LOOT_014"))
		{
			if (cardId == "CS2_062" || cardId == "EX1_308")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_BossWarlockFire_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_BossKoboldLibrarian_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400231D RID: 8989
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_BossKoboldLibrarian_01 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_BossKoboldLibrarian_01.prefab:c4ea6160f285a8041a7af7a4e94692d5");

	// Token: 0x0400231E RID: 8990
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_BossWarlockFire_01 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_BossWarlockFire_01.prefab:a8d5838aa6f449549974afc98124ec66");

	// Token: 0x0400231F RID: 8991
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_Death_01 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_Death_01.prefab:56812f5afbd0bf3479fd032f74523365");

	// Token: 0x04002320 RID: 8992
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_DefeatPlayer_01.prefab:b407121c31baf0841aa622e90ad2c914");

	// Token: 0x04002321 RID: 8993
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_EmoteResponse_01.prefab:094da1d1c9ee77f42ba6ebe6dc282af8");

	// Token: 0x04002322 RID: 8994
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_HeroPower_01 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_HeroPower_01.prefab:3201c16ca8c60824698cea51ac405e5a");

	// Token: 0x04002323 RID: 8995
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_HeroPower_02 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_HeroPower_02.prefab:3e3ec1e5872235c408ff993576d46954");

	// Token: 0x04002324 RID: 8996
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_HeroPower_03 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_HeroPower_03.prefab:e4548e1c94e6c634380f596c71023f8a");

	// Token: 0x04002325 RID: 8997
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_HeroPower_04 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_HeroPower_04.prefab:fc54bdd28cf3e8e44876392635d7f0e4");

	// Token: 0x04002326 RID: 8998
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_HeroPowerOne_01 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_HeroPowerOne_01.prefab:2c88303e921aa214ea51ed3aad08821b");

	// Token: 0x04002327 RID: 8999
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_HeroPowerOne_02 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_HeroPowerOne_02.prefab:3827b1bc856c8b341815f3fd316fde67");

	// Token: 0x04002328 RID: 9000
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_HeroPowerOne_03 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_HeroPowerOne_03.prefab:ecfae9b3afa8ce741b11931263741102");

	// Token: 0x04002329 RID: 9001
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_Idle_01 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_Idle_01.prefab:cd694d2162815d6479fbfa82dca9e059");

	// Token: 0x0400232A RID: 9002
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_Idle_02 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_Idle_02.prefab:5105ea76d8a9c5f47b357ebde68f86bf");

	// Token: 0x0400232B RID: 9003
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_Idle_03 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_Idle_03.prefab:dd44af340b2253144b642b417d967a0a");

	// Token: 0x0400232C RID: 9004
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_Idle_04 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_Idle_04.prefab:e5795e4bb0f614f40a31037485b6f966");

	// Token: 0x0400232D RID: 9005
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_Idle_05 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_Idle_05.prefab:f46931357ae078d4cbcffa839727f450");

	// Token: 0x0400232E RID: 9006
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_Idle_09 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_Idle_09.prefab:7a03d5200365105479c93a3fbbc8ecde");

	// Token: 0x0400232F RID: 9007
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_Intro_01 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_Intro_01.prefab:b4ac8ec00c00ff5449c62275a06fc1a8");

	// Token: 0x04002330 RID: 9008
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_IntroChu_01 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_IntroChu_01.prefab:468b3754ca20c31438dea75d82a58730");

	// Token: 0x04002331 RID: 9009
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_PlayerBabblingBook_01 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_PlayerBabblingBook_01.prefab:2c28a21064da51e44af2d891ef6eb248");

	// Token: 0x04002332 RID: 9010
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_PlayerBookOfSpectres_01 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_PlayerBookOfSpectres_01.prefab:6c1198fb7d76d9649b07b71353f54675");

	// Token: 0x04002333 RID: 9011
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_PlayerBookWyrm_01 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_PlayerBookWyrm_01.prefab:a5de50bd587defb439d29160811b4c5b");

	// Token: 0x04002334 RID: 9012
	private static readonly AssetReference VO_DALA_BOSS_12h_Male_Undead_PlayerCabalistTome_01 = new AssetReference("VO_DALA_BOSS_12h_Male_Undead_PlayerCabalistTome_01.prefab:53700bd93e0b7cc40a2f7b527c28666a");

	// Token: 0x04002335 RID: 9013
	private List<string> m_IdleSongLines = new List<string>
	{
		DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Idle_01,
		DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Idle_02,
		DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Idle_03,
		DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Idle_04,
		DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Idle_05,
		DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_Idle_09
	};

	// Token: 0x04002336 RID: 9014
	private List<string> m_HeroPower = new List<string>
	{
		DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_HeroPower_01,
		DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_HeroPower_02,
		DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_HeroPower_03,
		DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_HeroPower_04
	};

	// Token: 0x04002337 RID: 9015
	private List<string> m_HeroPowerOne = new List<string>
	{
		DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_HeroPowerOne_01,
		DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_HeroPowerOne_02,
		DALA_Dungeon_Boss_12h.VO_DALA_BOSS_12h_Male_Undead_HeroPowerOne_03
	};

	// Token: 0x04002338 RID: 9016
	private HashSet<string> m_playedLines = new HashSet<string>();
}
