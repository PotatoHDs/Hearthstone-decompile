using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003E9 RID: 1001
public class GIL_Dungeon_Boss_32h : GIL_Dungeon
{
	// Token: 0x060037E0 RID: 14304 RVA: 0x0011AABC File Offset: 0x00118CBC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_32h_Female_Sprite_Intro_01.prefab:7da1bbcac53b07947807b53a82b5fb0c",
			"VO_GILA_BOSS_32h_Female_Sprite_EmoteResponse_01.prefab:047de08f89dd6d648ae29bd17fe4a201",
			"VO_GILA_BOSS_32h_Female_Sprite_Death_01.prefab:865008e5ebcb3ad41a9fe76d3e400773",
			"VO_GILA_BOSS_32h_Female_Sprite_HeroPower_01.prefab:7758b3cb7c06072498e6c91d0cfeca79",
			"VO_GILA_BOSS_32h_Female_Sprite_HeroPower_02.prefab:77e3de115374db848bf3f3441123e1c9",
			"VO_GILA_BOSS_32h_Female_Sprite_HeroPower_03.prefab:5c068ac1cfce92043905c4d0ab6c860a",
			"VO_GILA_BOSS_32h_Female_Sprite_HeroPower_04.prefab:d957d877bb1d7154b8573a47031bdbf1",
			"VO_GILA_BOSS_32h_Female_Sprite_EventPlaysAoEBuff_01.prefab:5f698b1cc65290c4c9f1db1cb30869a4",
			"VO_GILA_BOSS_32h_Female_Sprite_EventPlaysAoEBuff_02.prefab:d20aec1c64396dd47ab5d7dd4318dd68",
			"VO_GILA_BOSS_32h_Female_Sprite_EventPlaysBigMinion_01.prefab:432161c3bcb0f3043bcd0dfbe5ad13e4",
			"VO_GILA_BOSS_32h_Female_Sprite_EventPlaysBigMinion_02.prefab:4bf7fa157adb5e5448034cf92565a9df"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060037E1 RID: 14305 RVA: 0x0011AB90 File Offset: 0x00118D90
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060037E2 RID: 14306 RVA: 0x0011ABA6 File Offset: 0x00118DA6
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_32h_Female_Sprite_HeroPower_01.prefab:7758b3cb7c06072498e6c91d0cfeca79",
			"VO_GILA_BOSS_32h_Female_Sprite_HeroPower_02.prefab:77e3de115374db848bf3f3441123e1c9",
			"VO_GILA_BOSS_32h_Female_Sprite_HeroPower_03.prefab:5c068ac1cfce92043905c4d0ab6c860a",
			"VO_GILA_BOSS_32h_Female_Sprite_HeroPower_04.prefab:d957d877bb1d7154b8573a47031bdbf1"
		};
	}

	// Token: 0x060037E3 RID: 14307 RVA: 0x0011ABD9 File Offset: 0x00118DD9
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_32h_Female_Sprite_Death_01.prefab:865008e5ebcb3ad41a9fe76d3e400773";
	}

	// Token: 0x060037E4 RID: 14308 RVA: 0x0011ABE0 File Offset: 0x00118DE0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_32h_Female_Sprite_Intro_01.prefab:7da1bbcac53b07947807b53a82b5fb0c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_32h_Female_Sprite_EmoteResponse_01.prefab:047de08f89dd6d648ae29bd17fe4a201", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060037E5 RID: 14309 RVA: 0x0011AC67 File Offset: 0x00118E67
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent == 102)
			{
				string text = base.PopRandomLineWithChance(this.m_BigMinion);
				if (text != null)
				{
					yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
				}
			}
		}
		else
		{
			string text = base.PopRandomLineWithChance(this.m_PlayerAoE);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x04001D99 RID: 7577
	private List<string> m_PlayerAoE = new List<string>
	{
		"VO_GILA_BOSS_32h_Female_Sprite_EventPlaysAoEBuff_01.prefab:5f698b1cc65290c4c9f1db1cb30869a4",
		"VO_GILA_BOSS_32h_Female_Sprite_EventPlaysAoEBuff_02.prefab:d20aec1c64396dd47ab5d7dd4318dd68"
	};

	// Token: 0x04001D9A RID: 7578
	private List<string> m_BigMinion = new List<string>
	{
		"VO_GILA_BOSS_32h_Female_Sprite_EventPlaysBigMinion_01.prefab:432161c3bcb0f3043bcd0dfbe5ad13e4",
		"VO_GILA_BOSS_32h_Female_Sprite_EventPlaysBigMinion_02.prefab:4bf7fa157adb5e5448034cf92565a9df"
	};
}
