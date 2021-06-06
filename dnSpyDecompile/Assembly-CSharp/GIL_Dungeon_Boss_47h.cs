using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003F8 RID: 1016
public class GIL_Dungeon_Boss_47h : GIL_Dungeon
{
	// Token: 0x06003869 RID: 14441 RVA: 0x0011C868 File Offset: 0x0011AA68
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_47h_Male_Monster_IntroALT_01.prefab:e5a8c96c1c0518a4db0b59172a471a3b",
			"VO_GILA_BOSS_47h_Male_Monster_EmoteResponse_01.prefab:0dd0da0712e288b4bbb226eca3eb8e3f",
			"VO_GILA_BOSS_47h_Male_Monster_Death_01.prefab:fd4b005d9ff09134ba3f48da2397786d",
			"VO_GILA_BOSS_47h_Male_Monster_HeroPower_01.prefab:b26250c73e5df194fa2cd34852999952",
			"VO_GILA_BOSS_47h_Male_Monster_HeroPower_02.prefab:ac052081d4ffc8940830be182ae0a5e1",
			"VO_GILA_BOSS_47h_Male_Monster_HeroPower_03.prefab:fcd5d6d4a4d62fa4f99bc2592b23a573"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600386A RID: 14442 RVA: 0x0011C904 File Offset: 0x0011AB04
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_47h_Male_Monster_IntroALT_01.prefab:e5a8c96c1c0518a4db0b59172a471a3b", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_47h_Male_Monster_EmoteResponse_01.prefab:0dd0da0712e288b4bbb226eca3eb8e3f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600386B RID: 14443 RVA: 0x0011C98B File Offset: 0x0011AB8B
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_47h_Male_Monster_Death_01.prefab:fd4b005d9ff09134ba3f48da2397786d";
	}

	// Token: 0x0600386C RID: 14444 RVA: 0x0011C992 File Offset: 0x0011AB92
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (entity.HasTag(GAME_TAG.BATTLECRY))
		{
			string text = base.PopRandomLineWithChance(this.m_RandomLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x0600386D RID: 14445 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x0600386E RID: 14446 RVA: 0x0011C9A8 File Offset: 0x0011ABA8
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (entity.HasTag(GAME_TAG.BATTLECRY))
		{
			string text = base.PopRandomLineWithChance(this.m_RandomLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x04001DB9 RID: 7609
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DBA RID: 7610
	private List<string> m_RandomLines = new List<string>
	{
		"VO_GILA_BOSS_47h_Male_Monster_HeroPower_01.prefab:b26250c73e5df194fa2cd34852999952",
		"VO_GILA_BOSS_47h_Male_Monster_HeroPower_02.prefab:ac052081d4ffc8940830be182ae0a5e1",
		"VO_GILA_BOSS_47h_Male_Monster_HeroPower_03.prefab:fcd5d6d4a4d62fa4f99bc2592b23a573"
	};
}
