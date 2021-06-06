using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000406 RID: 1030
public class GIL_Dungeon_Boss_63h : GIL_Dungeon
{
	// Token: 0x060038EF RID: 14575 RVA: 0x0011E874 File Offset: 0x0011CA74
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_63h_Female_Harpy_Intro_01.prefab:277992cd8bf83aa46a6f11beaa48074c",
			"VO_GILA_BOSS_63h_Female_Harpy_EmoteResponse_01.prefab:af25a266f8d1fe34388f2573a0b63bfd",
			"VO_GILA_BOSS_63h_Female_Harpy_Death_01.prefab:b7ad65011bf4b5d44a17c3409f1e3056",
			"VO_GILA_BOSS_63h_Female_Harpy_HeroPower_01.prefab:a2b0d7918b085ae47a2299b6bccd5acf",
			"VO_GILA_BOSS_63h_Female_Harpy_HeroPower_02.prefab:2fae3a49a855c0440af8dd3e19ff6e85",
			"VO_GILA_BOSS_63h_Female_Harpy_HeroPower_03.prefab:67795a869d43f364584bbbecdd81d421"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060038F0 RID: 14576 RVA: 0x0011E910 File Offset: 0x0011CB10
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_63h_Female_Harpy_Intro_01.prefab:277992cd8bf83aa46a6f11beaa48074c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_63h_Female_Harpy_EmoteResponse_01.prefab:af25a266f8d1fe34388f2573a0b63bfd", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060038F1 RID: 14577 RVA: 0x0011E997 File Offset: 0x0011CB97
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_63h_Female_Harpy_Death_01.prefab:b7ad65011bf4b5d44a17c3409f1e3056";
	}

	// Token: 0x060038F2 RID: 14578 RVA: 0x0011E99E File Offset: 0x0011CB9E
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060038F3 RID: 14579 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060038F4 RID: 14580 RVA: 0x0011E9B4 File Offset: 0x0011CBB4
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		if (missionEvent == 101)
		{
			string text = base.PopRandomLineWithChance(this.m_RandomLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x04001DE0 RID: 7648
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DE1 RID: 7649
	private List<string> m_RandomLines = new List<string>
	{
		"VO_GILA_BOSS_63h_Female_Harpy_HeroPower_01.prefab:a2b0d7918b085ae47a2299b6bccd5acf",
		"VO_GILA_BOSS_63h_Female_Harpy_HeroPower_02.prefab:2fae3a49a855c0440af8dd3e19ff6e85",
		"VO_GILA_BOSS_63h_Female_Harpy_HeroPower_03.prefab:67795a869d43f364584bbbecdd81d421"
	};
}
