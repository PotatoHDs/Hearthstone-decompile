using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoH_Garrosh_06 : BoH_Garrosh_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeA_01.prefab:74f7490bd6f8be04c80fc536443bc1bf");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeB_01.prefab:09fdce0758e3f3b45bd8dbc9ea351421");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeC_01.prefab:6909de0b96603ae478a686ad92c3b3c6");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeD_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeD_01.prefab:79f61bece774dd84482b6830ce46d88b");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeA_01.prefab:2470fdc7240cf394b879b0967cd4f840");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Intro_01.prefab:713307af01368b3478d7c3ebf629ddfb");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Victory_01.prefab:ac7a73269ddf1954888c4cedbf847fa4");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6EmoteResponse_01.prefab:2547e740d9d99034288438e91ea31e92");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeC_01.prefab:dc30e96ccf674824e86be402ae9c3ec8");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeD_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeD_01.prefab:ec1b5d4d400c0ef47a881eca1e428f07");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_01.prefab:72e2a139edf1b88479c425be20371632");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_02.prefab:1e10320de01b5d44e86e6fe4da874eb3");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_03.prefab:57ee2ce05ffffb54f9c7274fbe463a1f");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_01.prefab:021b12aeb2b04864389130bfb35aaf09");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_02.prefab:801ae8c93536b8d4a872a2376a520801");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_03.prefab:ca8591c4a46d9384b885d55b1829ebda");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Loss_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Loss_01.prefab:4d5f48fe314ad3843bacacf6e4114e08");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Victory_01.prefab:bd74092d27056d747adc83466eb8e6ee");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeA_01.prefab:b9075095164795e4f8adfca7b9df89a1");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Intro_01.prefab:969f3f9d80615ef468767b822e1d23dd");

	private int GatesAttackedCounter;

	private List<string> m_VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPowerLines = new List<string> { VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_01, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_02, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_03 };

	private List<string> m_VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6IdleLines = new List<string> { VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_01, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_02, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeA_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeB_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeC_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeD_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeA_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Intro_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Victory_01, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6EmoteResponse_01, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeC_01, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeD_01,
			VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_01, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_02, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_03, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_01, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_02, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_03, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Loss_01, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Victory_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeA_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Intro_01
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPowerLines;
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6EmoteResponse_01;
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
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
		switch (missionEvent)
		{
		case 101:
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.7f, 0.7f, 0.7f), 2f);
			yield return new WaitForSeconds(0.5f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.1f, 0.1f, 0.1f), 5f);
			yield return new WaitForSeconds(4f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.25f, 0.25f, 0.25f), 5f);
			break;
		case 102:
			yield return new WaitForSeconds(5f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.4f, 0.4f, 0.4f), 5f);
			break;
		case 103:
			yield return new WaitForSeconds(10f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.6f, 0.6f, 0.6f), 5f);
			break;
		case 104:
			yield return new WaitForSeconds(15f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.8f, 0.8f, 0.8f), 5f);
			break;
		case 105:
			yield return new WaitForSeconds(20f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(1f, 1f, 1f), 5f);
			break;
		case 501:
			yield return PlayLineAlways(actor, VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Victory_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Victory_01);
			break;
		case 502:
			GatesAttackedCounter++;
			if (GatesAttackedCounter == 4)
			{
				yield return PlayLineAlways(actor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeB_01);
			}
			else
			{
				yield return null;
			}
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Loss_01);
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return PlayLineAlways(actor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeA_01);
			break;
		case 3:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeA_01);
			break;
		case 5:
			yield return PlayLineAlways(actor, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeC_01);
			break;
		case 7:
			yield return PlayLineAlways(actor, VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeD_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeD_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}
}
