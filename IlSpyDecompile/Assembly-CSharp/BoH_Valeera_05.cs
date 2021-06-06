using System.Collections;
using System.Collections.Generic;

public class BoH_Valeera_05 : BoH_Valeera_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Valeera_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Valeera_Mission5ExchangeA_02.prefab:cc8e72201171551489548b303bed4779");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5ExchangeC_01.prefab:62fe0a172e6021543b4612f8d2055b14");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5Victory_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5Victory_02.prefab:0b34cc033a237f54daaaf030b9927bda");

	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Death_01 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Death_01.prefab:f20e0ccfc5ebb3146a3e2b9a546043fd");

	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5EmoteResponse_01.prefab:bac843f870c74f14db715dd2404f6e0b");

	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_01.prefab:7d3df54c14e60c34bbe527c3e68645b6");

	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_02.prefab:e96c9d8ce955201489c97775369a0a66");

	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_03.prefab:f762709ca55fb334a93d020487f8bf8d");

	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_01.prefab:31ece93aed91a004cb10fac406bc1b67");

	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_02.prefab:80ce121e13e777446b4143c2900f5f09");

	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_03.prefab:bb17562dbbb4b6d4abaeb2d5690fee53");

	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Intro_01.prefab:53f010d3373fa3147bc15108fd692518");

	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Loss_01.prefab:28c5b4cfee104f84aa3aed5a3b0937a4");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_01.prefab:f4b4d12a295d42045a3ac73ec4acd0f8");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_03.prefab:013fc061ad6a0cf49ad4db4bdc78736f");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeA_01.prefab:2959bca0cd8be9a41beed97b6627554c");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeC_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeC_02.prefab:42a7a272e1572f54c8a5a845607121a7");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeA_02.prefab:2678f0357642663479546b57f56f3928");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeD_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeD_02.prefab:53253aeec062c9144b24b22f2c5281c6");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Intro_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Intro_02.prefab:cb3361637933ceb4b8c76e42b558bf3f");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_01.prefab:0c4eab66504069d45a82c578b6a30037");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_03.prefab:d5be331a0ca0ffd458013319c3ead68f");

	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission5ExchangeD_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission5ExchangeD_01.prefab:5734c86a800699048aa559ec5c4e7ddd");

	public static readonly AssetReference VarianBrassRing = new AssetReference("Varian_BrassRing_Quote.prefab:b192b80fcc22d1145bfa81b476cecc09");

	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	public static readonly AssetReference GarroshBrassRing = new AssetReference("Garrosh_BrassRing_Quote.prefab:9c911310fb2bf7246ae78ef14a1b4dc5");

	public static readonly AssetReference JainaBrassRing = new AssetReference("JainaMid_BrassRing_Quote.prefab:7eba171d881f6764e81abddbb125bb19");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_01, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_02, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_01, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_02, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission5ExchangeD_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Valeera_Mission5ExchangeA_02, VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5ExchangeC_01, VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5Victory_02, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Death_01, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5EmoteResponse_01, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_01, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_02, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_03, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_01,
			VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_02, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_03, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Intro_01, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Loss_01, VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_01, VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_03, VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeA_01, VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeC_02, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeA_02, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeD_02,
			VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Intro_02, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_01, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_03
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return MissionPlayVO(actor, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Intro_02);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetBossIdleLines()
	{
		return m_BossIdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_BossUsesHeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Death_01;
		m_OverrideMusicTrack = MusicPlaylistType.InGame_ICC;
		m_standardEmoteResponseLine = VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 507:
			yield return PlayLineAlways(actor, VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Loss_01);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_01);
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5Victory_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 102:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_01);
			yield return PlayLineAlways(GarroshBrassRing, VO_Story_Hero_Garrosh_Male_Orc_Story_Valeera_Mission5ExchangeA_02);
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 108:
		{
			Actor enemyActorByCardId2 = GetEnemyActorByCardId("Story_06_Garona");
			if (enemyActorByCardId2 != null)
			{
				yield return PlayLineAlways(enemyActorByCardId2, VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission5ExchangeD_01);
			}
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeD_02);
			break;
		}
		case 109:
		{
			Actor enemyActorByCardId = GetEnemyActorByCardId("Story_06_GaronaDormant");
			if (enemyActorByCardId != null)
			{
				yield return PlayLineAlways(enemyActorByCardId, VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission5ExchangeD_01);
			}
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeD_02);
			break;
		}
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return base.RespondToPlayedCardWithTiming(entity);
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return PlayLineAlways(VarianBrassRing, VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeA_02);
			break;
		case 5:
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5ExchangeC_01);
			yield return PlayLineAlways(VarianBrassRing, VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeC_02);
			break;
		}
	}
}
