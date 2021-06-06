using System.Collections;
using System.Collections.Generic;

public class BoH_Garrosh_04 : BoH_Garrosh_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4Death_01 = new AssetReference("VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4Death_01.prefab:bfa5b4579b1945b1a0beba43c0bcb993");

	private static readonly AssetReference VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4EmoteResponse_01 = new AssetReference("VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4EmoteResponse_01.prefab:66222a97e5eb40f4a5d5a700da9d71d6");

	private static readonly AssetReference VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_01 = new AssetReference("VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_01.prefab:ed3c00e07654463286fa69563f566695");

	private static readonly AssetReference VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_02 = new AssetReference("VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_02.prefab:4f3a87b7dd6d43178a416c61eea55821");

	private static readonly AssetReference VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_03 = new AssetReference("VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_03.prefab:87afd0aa3ed94c9abf14a340502b2690");

	private static readonly AssetReference VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4Loss_01 = new AssetReference("VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4Loss_01.prefab:1de1f7f8705644c499d73059cf8d6e6c");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeA_01.prefab:2c7ead4c7d2299f4fa0d9777633a973c");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeB_01.prefab:012246e019320354288264519deffaf2");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeC_01.prefab:8f17be315bfaae7429fe82fcf776ea7a");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Intro_01.prefab:e784b22048a1082498984a2ed37ddb42");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Victory_01.prefab:3119bee73eaea8b4b9efa03b5ce1b8c8");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4ExchangeC_01.prefab:5eef752e55d6aea4080aeac7e0aeb67c");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_01.prefab:cf21762ad6240494c8097a3c816d661f");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_02.prefab:0ed5fe130a8c147408b9cad50624ee34");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_03.prefab:cb890dc97b3acd743b7c6cc4db678a6b");

	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	private List<string> m_VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPowerLines = new List<string> { VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_01, VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_02, VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4Death_01, VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4EmoteResponse_01, VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_01, VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_02, VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_03, VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4Loss_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeA_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeB_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeC_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Intro_01,
			VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Victory_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4ExchangeC_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_02, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_03
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

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPowerLines;
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4EmoteResponse_01;
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
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(busy: true);
			while (m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(busy: false);
			yield break;
		}
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
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_01);
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_02);
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_03);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return PlayLineAlways(actor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeA_01);
			break;
		case 3:
			yield return PlayLineAlways(actor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeB_01);
			break;
		case 7:
			yield return PlayLineAlways(actor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeC_01);
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4ExchangeC_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BRMAdventure);
	}
}
