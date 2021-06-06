using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000483 RID: 1155
public class ULDA_Dungeon_Boss_06h : ULDA_Dungeon
{
	// Token: 0x06003E7B RID: 15995 RVA: 0x0014B5C4 File Offset: 0x001497C4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Livewire_Lance_01,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Mirage_Blade_01,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_DeathALT_01,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_DefeatPlayer_01,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_EmoteResponse_01,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_01,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_02,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_03,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_04,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_05,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_Idle_01,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_Idle_02,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_Idle_03,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_Intro_01,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_IntroBrannRespose_01,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Armored_Goon_01,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Madam_Goya_01,
			ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Mischief_Maker_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003E7C RID: 15996 RVA: 0x0014B748 File Offset: 0x00149948
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003E7D RID: 15997 RVA: 0x0014B750 File Offset: 0x00149950
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_EmoteResponse_01;
	}

	// Token: 0x06003E7E RID: 15998 RVA: 0x0014B788 File Offset: 0x00149988
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_IntroBrannRespose_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003E7F RID: 15999 RVA: 0x0014B862 File Offset: 0x00149A62
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003E80 RID: 16000 RVA: 0x0014B878 File Offset: 0x00149A78
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
		if (!(cardId == "ULD_709"))
		{
			if (!(cardId == "CFM_672"))
			{
				if (cardId == "ULD_229")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Mischief_Maker_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Madam_Goya_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Armored_Goon_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003E81 RID: 16001 RVA: 0x0014B88E File Offset: 0x00149A8E
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
		if (!(cardId == "ULD_326t"))
		{
			if (cardId == "ULD_708")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Livewire_Lance_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Mirage_Blade_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002AD1 RID: 10961
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Livewire_Lance_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Livewire_Lance_01.prefab:6d8d09361821ecf46805a5e88a8a310f");

	// Token: 0x04002AD2 RID: 10962
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Mirage_Blade_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Mirage_Blade_01.prefab:c2b0b2d06c4b4ce49b52231c00faa5ef");

	// Token: 0x04002AD3 RID: 10963
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_DeathALT_01.prefab:d8b6b87063c1bed4e9a7301b1fe53799");

	// Token: 0x04002AD4 RID: 10964
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_DefeatPlayer_01.prefab:7efa6080bc4c581428d993ffb2930e3e");

	// Token: 0x04002AD5 RID: 10965
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_EmoteResponse_01.prefab:d112eb7ac63daef4a99d2c91093ece59");

	// Token: 0x04002AD6 RID: 10966
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_01.prefab:2835c6feeb8eb32488c1008ea5222033");

	// Token: 0x04002AD7 RID: 10967
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_02.prefab:01c74b291f9e0bc4caccf2efd3762a9a");

	// Token: 0x04002AD8 RID: 10968
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_03.prefab:7d41bc2ed6df25f4b98c1b721ea7daba");

	// Token: 0x04002AD9 RID: 10969
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_04.prefab:23dd9ecddfd00c345a534e3d0bed8b0d");

	// Token: 0x04002ADA RID: 10970
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_05.prefab:060201ce2f8a3eb4c863349fc3e164b2");

	// Token: 0x04002ADB RID: 10971
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_Idle_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_Idle_01.prefab:1f59eaa3f2cfcd94faca67a031f19350");

	// Token: 0x04002ADC RID: 10972
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_Idle_02 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_Idle_02.prefab:9c5d3eaabd54195459184e8e7d5ef496");

	// Token: 0x04002ADD RID: 10973
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_Idle_03 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_Idle_03.prefab:df2bf04f1d4043747a518c342df19865");

	// Token: 0x04002ADE RID: 10974
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_Intro_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_Intro_01.prefab:0f58b798e775b5848a3e9b7f08322b74");

	// Token: 0x04002ADF RID: 10975
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_IntroBrannRespose_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_IntroBrannRespose_01.prefab:565a41251ba668f409f1cdd1ad130bf3");

	// Token: 0x04002AE0 RID: 10976
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Armored_Goon_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Armored_Goon_01.prefab:25fdcf8825f977042b451106ff42a359");

	// Token: 0x04002AE1 RID: 10977
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Madam_Goya_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Madam_Goya_01.prefab:32053c00561edcc48b08919ba22e0a4c");

	// Token: 0x04002AE2 RID: 10978
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Mischief_Maker_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Mischief_Maker_01.prefab:0a55e9df35c37cc408ecfd161046dde3");

	// Token: 0x04002AE3 RID: 10979
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_01,
		ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_02,
		ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_03,
		ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_04,
		ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_05
	};

	// Token: 0x04002AE4 RID: 10980
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_Idle_01,
		ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_Idle_02,
		ULDA_Dungeon_Boss_06h.VO_ULDA_BOSS_06h_Male_Pandaren_Idle_03
	};

	// Token: 0x04002AE5 RID: 10981
	private HashSet<string> m_playedLines = new HashSet<string>();
}
