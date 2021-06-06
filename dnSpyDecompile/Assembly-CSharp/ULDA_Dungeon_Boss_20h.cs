using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000491 RID: 1169
public class ULDA_Dungeon_Boss_20h : ULDA_Dungeon
{
	// Token: 0x06003F1A RID: 16154 RVA: 0x0014EBC4 File Offset: 0x0014CDC4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_BossTriggerAncestralGuardian_01,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_BossTriggerWeapon_01,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_Death_01,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_DefeatPlayer_01,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_EmoteResponse_01,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_01,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_02,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_03,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_04,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_05,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_Idle_01,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_Idle_03,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_Intro_01,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_PlayerTrigger_Bone_Wraith_01,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_PlayerTrigger_Brazen_Zealot_01,
			ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_PlayerTrigger_Tomb_Warden_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003F1B RID: 16155 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003F1C RID: 16156 RVA: 0x0014ED28 File Offset: 0x0014CF28
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003F1D RID: 16157 RVA: 0x0014ED30 File Offset: 0x0014CF30
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_EmoteResponse_01;
	}

	// Token: 0x06003F1E RID: 16158 RVA: 0x0014ED68 File Offset: 0x0014CF68
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "ULDA_Reno")
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

	// Token: 0x06003F1F RID: 16159 RVA: 0x0014EE13 File Offset: 0x0014D013
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent == 102)
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_BossTriggerWeapon_01, 2.5f);
			}
			else
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
		}
		yield break;
	}

	// Token: 0x06003F20 RID: 16160 RVA: 0x0014EE29 File Offset: 0x0014D029
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
		if (!(cardId == "ULD_253"))
		{
			if (!(cardId == "ULD_275"))
			{
				if (cardId == "ULD_145")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_PlayerTrigger_Brazen_Zealot_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_PlayerTrigger_Bone_Wraith_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_PlayerTrigger_Tomb_Warden_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003F21 RID: 16161 RVA: 0x0014EE3F File Offset: 0x0014D03F
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
		if (cardId == "ULD_207")
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_BossTriggerAncestralGuardian_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002BC1 RID: 11201
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_BossTriggerAncestralGuardian_01 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_BossTriggerAncestralGuardian_01.prefab:206a03ce91069e244b9cb2b04ff90125");

	// Token: 0x04002BC2 RID: 11202
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_BossTriggerWeapon_01 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_BossTriggerWeapon_01.prefab:0ed3c3973dec4e241a3871330f4e3d0a");

	// Token: 0x04002BC3 RID: 11203
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_Death_01 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_Death_01.prefab:42e1b8e1228d7ac41adc8d8bbdb2b831");

	// Token: 0x04002BC4 RID: 11204
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_DefeatPlayer_01.prefab:072e9fc9889729c40850763e922535a7");

	// Token: 0x04002BC5 RID: 11205
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_EmoteResponse_01.prefab:22461a25d9693684eb7c70dae90d78ce");

	// Token: 0x04002BC6 RID: 11206
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_01.prefab:d5342977bbefe3a4a9e56bce1e0ac2ef");

	// Token: 0x04002BC7 RID: 11207
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_02.prefab:b94cdc25ceaae574a918661ec2229123");

	// Token: 0x04002BC8 RID: 11208
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_03.prefab:588bd8cb07dc6c94e85d1d9a3103c9f3");

	// Token: 0x04002BC9 RID: 11209
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_04.prefab:31863d17b9bce3e41a4201e49a9c376b");

	// Token: 0x04002BCA RID: 11210
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_05.prefab:57ce66b312db4f5418e9585c29855ac8");

	// Token: 0x04002BCB RID: 11211
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_Idle_01 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_Idle_01.prefab:764c23a359c072f478666c42e4409efb");

	// Token: 0x04002BCC RID: 11212
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_Idle_03 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_Idle_03.prefab:37cc8e56e16c26144a2197df83d95c90");

	// Token: 0x04002BCD RID: 11213
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_Intro_01 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_Intro_01.prefab:02a752d5d9a8cbe478b7c332251e1348");

	// Token: 0x04002BCE RID: 11214
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_PlayerTrigger_Bone_Wraith_01 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_PlayerTrigger_Bone_Wraith_01.prefab:2ce9e0a1acab4ce49981cb6b064c81d0");

	// Token: 0x04002BCF RID: 11215
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_PlayerTrigger_Brazen_Zealot_01 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_PlayerTrigger_Brazen_Zealot_01.prefab:a4dace3165843af438c842f132889e1e");

	// Token: 0x04002BD0 RID: 11216
	private static readonly AssetReference VO_ULDA_BOSS_20h_Male_Ghost_PlayerTrigger_Tomb_Warden_01 = new AssetReference("VO_ULDA_BOSS_20h_Male_Ghost_PlayerTrigger_Tomb_Warden_01.prefab:4cd4e0aefc494f349a0c64f9741d85dd");

	// Token: 0x04002BD1 RID: 11217
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_01,
		ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_02,
		ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_03,
		ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_04,
		ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_HeroPower_05
	};

	// Token: 0x04002BD2 RID: 11218
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_Idle_01,
		ULDA_Dungeon_Boss_20h.VO_ULDA_BOSS_20h_Male_Ghost_Idle_03
	};

	// Token: 0x04002BD3 RID: 11219
	private HashSet<string> m_playedLines = new HashSet<string>();
}
