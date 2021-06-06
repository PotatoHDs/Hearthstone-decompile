using System.Collections;
using UnityEngine;

public class TB11_CoOpv3 : MissionEntity
{
	private Card m_bossCard;

	private void SetUpBossCard()
	{
		if (m_bossCard == null)
		{
			int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
			Entity entity = GameState.Get().GetEntity(tag);
			if (entity != null)
			{
				m_bossCard = entity.GetCard();
			}
		}
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_BRMA13_1_RESPONSE_05.prefab:ec4f58f21067dde49b2ee26538259c89");
		PreloadSound("VO_NEFARIAN_NEF2_65.prefab:cad99daf56acb69428af9299fe9fb04b");
		PreloadSound("VO_BRMA17_1_RESPONSE_85.prefab:c7bbc928438b13241bde42c6578ad5c8");
		PreloadSound("VO_BRMA17_1_TRANSFORM1_80.prefab:82475f6129d5587448c3aa398a77c580");
		PreloadSound("VO_BRMA17_1_TRANSFORM2_81.prefab:d064be3da78c0f5449db24a40f9a609b");
		PreloadSound("VO_BRMA13_1_TURN1_PT1_02.prefab:ac211cc8ab665034da99720e38b6b994");
		PreloadSound("VO_BRMA17_1_START_78.prefab:76391ad5bad9fcb4382a2bc98d2765d7");
		PreloadSound("VO_BRMA13_1_HP_PRIEST_08.prefab:75d6f8035f037dd43af7c058f318c2fb");
		PreloadSound("VO_BRMA13_1_HP_SHAMAN_13.prefab:e248e28c2032e5c4c84490af8596f093");
		PreloadSound("VO_Innkeeper_Male_Dwarf_Brawl_01.prefab:283019fef346e8f4688167eb0c3bfb3c");
		PreloadSound("VO_Innkeeper_Male_Dwarf_Brawl_02.prefab:a43ebf2271976b447a26d614b80948f0");
		PreloadSound("VO_Innkeeper_Male_Dwarf_NEFARIAN_Tavern_Brawl.prefab:5dfeed5d6b1827848999565cb1ef42fa");
	}

	public override AudioSource GetAnnouncerLine(Card heroCard, Card.AnnouncerLineType type)
	{
		if (heroCard.GetEntity().IsControlledByFriendlySidePlayer())
		{
			switch (Random.Range(0, 2))
			{
			case 0:
				return GetPreloadedSound("VO_Innkeeper_Male_Dwarf_Brawl_01.prefab:283019fef346e8f4688167eb0c3bfb3c");
			case 1:
				return GetPreloadedSound("VO_Innkeeper_Male_Dwarf_Brawl_02.prefab:a43ebf2271976b447a26d614b80948f0");
			}
		}
		if (heroCard.GetEntity().IsControlledByOpposingSidePlayer())
		{
			return GetPreloadedSound("VO_Innkeeper_Male_Dwarf_NEFARIAN_Tavern_Brawl.prefab:5dfeed5d6b1827848999565cb1ef42fa");
		}
		return base.GetAnnouncerLine(heroCard, type);
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		SetUpBossCard();
		if (!(m_bossCard == null) && turn == 1)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechWithCustomGameString("VO_BRMA13_1_RESPONSE_05.prefab:ec4f58f21067dde49b2ee26538259c89", "VO_COOP03_01", Notification.SpeechBubbleDirection.TopRight, m_bossCard.GetActor()));
		}
		yield break;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		SetUpBossCard();
		if (!(m_bossCard == null))
		{
			Actor actor = m_bossCard.GetActor();
			switch (missionEvent)
			{
			case 2:
				GameState.Get().SetBusy(busy: true);
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechWithCustomGameString("VO_BRMA17_1_RESPONSE_85.prefab:c7bbc928438b13241bde42c6578ad5c8", "VO_COOP03_03", Notification.SpeechBubbleDirection.TopRight, actor));
				GameState.Get().SetBusy(busy: false);
				break;
			case 6:
				GameState.Get().SetBusy(busy: true);
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechWithCustomGameString("VO_BRMA13_1_HP_PRIEST_08.prefab:75d6f8035f037dd43af7c058f318c2fb", "VO_COOP03_06", Notification.SpeechBubbleDirection.TopRight, actor));
				GameState.Get().SetBusy(busy: false);
				break;
			case 7:
				GameState.Get().SetBusy(busy: true);
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechWithCustomGameString("VO_BRMA13_1_HP_SHAMAN_13.prefab:e248e28c2032e5c4c84490af8596f093", "VO_COOP03_07", Notification.SpeechBubbleDirection.TopRight, actor));
				GameState.Get().SetBusy(busy: false);
				break;
			case 97:
				GameState.Get().SetBusy(busy: true);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechWithCustomGameString("VO_NEFARIAN_NEF2_65.prefab:cad99daf56acb69428af9299fe9fb04b", "VO_COOP03_02", Notification.SpeechBubbleDirection.TopRight, actor));
				GameState.Get().SetBusy(busy: false);
				break;
			case 98:
				GameState.Get().SetBusy(busy: true);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechWithCustomGameString("VO_BRMA13_1_TURN1_PT1_02.prefab:ac211cc8ab665034da99720e38b6b994", "VO_COOP03_08", Notification.SpeechBubbleDirection.TopRight, actor));
				GameState.Get().SetBusy(busy: false);
				break;
			case 99:
				GameState.Get().SetBusy(busy: true);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechWithCustomGameString("VO_BRMA17_1_START_78.prefab:76391ad5bad9fcb4382a2bc98d2765d7", "VO_COOP03_09", Notification.SpeechBubbleDirection.TopRight, actor));
				GameState.Get().SetBusy(busy: false);
				break;
			}
		}
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}
}
