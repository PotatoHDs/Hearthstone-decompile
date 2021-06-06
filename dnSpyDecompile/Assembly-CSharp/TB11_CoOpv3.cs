using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005CF RID: 1487
public class TB11_CoOpv3 : MissionEntity
{
	// Token: 0x0600517B RID: 20859 RVA: 0x001AC85C File Offset: 0x001AAA5C
	private void SetUpBossCard()
	{
		if (this.m_bossCard == null)
		{
			int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
			Entity entity = GameState.Get().GetEntity(tag);
			if (entity != null)
			{
				this.m_bossCard = entity.GetCard();
			}
		}
	}

	// Token: 0x0600517C RID: 20860 RVA: 0x001AC8A4 File Offset: 0x001AAAA4
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA13_1_RESPONSE_05.prefab:ec4f58f21067dde49b2ee26538259c89");
		base.PreloadSound("VO_NEFARIAN_NEF2_65.prefab:cad99daf56acb69428af9299fe9fb04b");
		base.PreloadSound("VO_BRMA17_1_RESPONSE_85.prefab:c7bbc928438b13241bde42c6578ad5c8");
		base.PreloadSound("VO_BRMA17_1_TRANSFORM1_80.prefab:82475f6129d5587448c3aa398a77c580");
		base.PreloadSound("VO_BRMA17_1_TRANSFORM2_81.prefab:d064be3da78c0f5449db24a40f9a609b");
		base.PreloadSound("VO_BRMA13_1_TURN1_PT1_02.prefab:ac211cc8ab665034da99720e38b6b994");
		base.PreloadSound("VO_BRMA17_1_START_78.prefab:76391ad5bad9fcb4382a2bc98d2765d7");
		base.PreloadSound("VO_BRMA13_1_HP_PRIEST_08.prefab:75d6f8035f037dd43af7c058f318c2fb");
		base.PreloadSound("VO_BRMA13_1_HP_SHAMAN_13.prefab:e248e28c2032e5c4c84490af8596f093");
		base.PreloadSound("VO_Innkeeper_Male_Dwarf_Brawl_01.prefab:283019fef346e8f4688167eb0c3bfb3c");
		base.PreloadSound("VO_Innkeeper_Male_Dwarf_Brawl_02.prefab:a43ebf2271976b447a26d614b80948f0");
		base.PreloadSound("VO_Innkeeper_Male_Dwarf_NEFARIAN_Tavern_Brawl.prefab:5dfeed5d6b1827848999565cb1ef42fa");
	}

	// Token: 0x0600517D RID: 20861 RVA: 0x001AC938 File Offset: 0x001AAB38
	public override AudioSource GetAnnouncerLine(Card heroCard, Card.AnnouncerLineType type)
	{
		if (heroCard.GetEntity().IsControlledByFriendlySidePlayer())
		{
			int num = UnityEngine.Random.Range(0, 2);
			if (num == 0)
			{
				return base.GetPreloadedSound("VO_Innkeeper_Male_Dwarf_Brawl_01.prefab:283019fef346e8f4688167eb0c3bfb3c");
			}
			if (num == 1)
			{
				return base.GetPreloadedSound("VO_Innkeeper_Male_Dwarf_Brawl_02.prefab:a43ebf2271976b447a26d614b80948f0");
			}
		}
		if (heroCard.GetEntity().IsControlledByOpposingSidePlayer())
		{
			return base.GetPreloadedSound("VO_Innkeeper_Male_Dwarf_NEFARIAN_Tavern_Brawl.prefab:5dfeed5d6b1827848999565cb1ef42fa");
		}
		return base.GetAnnouncerLine(heroCard, type);
	}

	// Token: 0x0600517E RID: 20862 RVA: 0x001AC99C File Offset: 0x001AAB9C
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		this.SetUpBossCard();
		if (this.m_bossCard == null)
		{
			yield break;
		}
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_BRMA13_1_RESPONSE_05.prefab:ec4f58f21067dde49b2ee26538259c89", "VO_COOP03_01", Notification.SpeechBubbleDirection.TopRight, this.m_bossCard.GetActor(), 1f, true, false));
		}
		yield break;
	}

	// Token: 0x0600517F RID: 20863 RVA: 0x001AC9B2 File Offset: 0x001AABB2
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.SetUpBossCard();
		if (this.m_bossCard == null)
		{
			yield break;
		}
		Actor actor = this.m_bossCard.GetActor();
		if (missionEvent <= 6)
		{
			if (missionEvent != 2)
			{
				if (missionEvent == 6)
				{
					GameState.Get().SetBusy(true);
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_BRMA13_1_HP_PRIEST_08.prefab:75d6f8035f037dd43af7c058f318c2fb", "VO_COOP03_06", Notification.SpeechBubbleDirection.TopRight, actor, 1f, true, false));
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_BRMA17_1_RESPONSE_85.prefab:c7bbc928438b13241bde42c6578ad5c8", "VO_COOP03_03", Notification.SpeechBubbleDirection.TopRight, actor, 1f, true, false));
				GameState.Get().SetBusy(false);
			}
		}
		else if (missionEvent != 7)
		{
			switch (missionEvent)
			{
			case 97:
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_NEFARIAN_NEF2_65.prefab:cad99daf56acb69428af9299fe9fb04b", "VO_COOP03_02", Notification.SpeechBubbleDirection.TopRight, actor, 1f, true, false));
				GameState.Get().SetBusy(false);
				break;
			case 98:
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_BRMA13_1_TURN1_PT1_02.prefab:ac211cc8ab665034da99720e38b6b994", "VO_COOP03_08", Notification.SpeechBubbleDirection.TopRight, actor, 1f, true, false));
				GameState.Get().SetBusy(false);
				break;
			case 99:
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_BRMA17_1_START_78.prefab:76391ad5bad9fcb4382a2bc98d2765d7", "VO_COOP03_09", Notification.SpeechBubbleDirection.TopRight, actor, 1f, true, false));
				GameState.Get().SetBusy(false);
				break;
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("VO_BRMA13_1_HP_SHAMAN_13.prefab:e248e28c2032e5c4c84490af8596f093", "VO_COOP03_07", Notification.SpeechBubbleDirection.TopRight, actor, 1f, true, false));
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06005180 RID: 20864 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x040048FC RID: 18684
	private Card m_bossCard;
}
