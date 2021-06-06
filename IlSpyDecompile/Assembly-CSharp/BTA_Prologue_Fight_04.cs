using System.Collections;
using System.Collections.Generic;

public class BTA_Prologue_Fight_04 : BTA_Prologue_Dungeon
{
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Death_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Death_01.prefab:bda78fc04409a744caf0eadf24de8ad3");

	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_EmoteResponse_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_EmoteResponse_01.prefab:a89ac351649b3ba4199cbf6ce5d98115");

	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01.prefab:d709b0d41c836994ea8f9a306c4fb5fa");

	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01.prefab:5ad535b39059dfd4a985822ddd8f14c3");

	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_03_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_03_01.prefab:3b6849fc7802c904ca47756574db5586");

	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_01_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_01_01.prefab:8667ecc5c7790f34690e30edbb02c5b6");

	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_02_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_02_01.prefab:3f5b4b42e76a61841ab189eba591467b");

	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_03_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_03_01.prefab:c41c0562aff85d84095a087388d058dd");

	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_VictoryDenied_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_VictoryDenied_01.prefab:1c8dce2472478624c94c3e6724756b52");

	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Intro_01_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Intro_01_01.prefab:2d65e89207967c345b5cc266d4bf1132");

	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Loss_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Loss_01.prefab:a535ac5f5ed9a8b4e8914e953b5cdcab");

	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01.prefab:52445d793154e1349806280df9847140");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01.prefab:6bd9109a56494b64ab5470b701c98e73");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeB_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeB_01.prefab:ffa43268a21de49448f0bf541efc757d");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01.prefab:1110d2427a78b1247a4a6f0808b92f90");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Intro_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Intro_02_01.prefab:47a87745f40e6614aa04b9713ff41900");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01.prefab:025ebb519e1b1d0458fd38ae3d67c9b6");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory01_01.prefab:02f5d3f496785184a9db2e7787320ff9");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory02_01.prefab:0a74f62cf504d704f996f6a2c8c0a323");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory03_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory03_01.prefab:e244e3714595a914fbde9bc4456e4fd4");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory04_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory04_01.prefab:de7f2efcb115d784486d4efb31b6891f");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission4_Intro_01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission4_Intro_01_01.prefab:edf38deebdf0f7242b98f20339352507");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_01_01.prefab:7e85d5356eb5ad9408188b73aba53220");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_02_01.prefab:58fa059877f95cb469dd52c66e4193b9");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_Shocked_at_Illidan_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_Shocked_at_Illidan_02_01.prefab:db6d711d63bd5694eb2053399da5ba66");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01.prefab:d0ab7c13fe71848479f487e4464654e8");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01.prefab:31c20bb2df92f2c4aba657f6b5cccf6c");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01.prefab:51c9801d61f6742438e62bd5fdddc970");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_01_01.prefab:07cf70a55274df346b0d4313605f0926");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_02_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_02_01.prefab:a4a6900d484fd754ca17f0e29f8a8be2");

	public static readonly AssetReference IllidanBrassRing = new AssetReference("DemonHunter_Illidan_Popup_BrassRing.prefab:8c007b8e8be417c4fbd9738960e6f7f0");

	public static readonly AssetReference TyrandeBrassRing = new AssetReference("YoungTyrande_Popup_BrassRing.prefab:79f13833a3f5e97449ef744f460e9fbd");

	public static readonly AssetReference MalfurionBrassRing = new AssetReference("YoungMalfurion_Popup_BrassRing.prefab:5544ac85196277542a7fa0b1a9b578df");

	private List<string> m_VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_Lines = new List<string> { VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_03_01 };

	private List<string> m_VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_Lines = new List<string> { VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_01_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_02_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_03_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Death_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_EmoteResponse_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_03_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_01_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_02_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_03_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_VictoryDenied_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Intro_01_01,
			VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Loss_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeB_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Intro_02_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory01_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory02_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory03_01,
			VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory04_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission4_Intro_01_01, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_01_01, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_02_01, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_Shocked_at_Illidan_02_01, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01, VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01, VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_01_01, VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_02_01
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

	public override List<string> GetIdleLines()
	{
		return m_VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_Lines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_Lines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Death_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		switch (missionEvent)
		{
		case 100:
			yield return PlayLineAlways(MalfurionBrassRing, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_01_01);
			yield return PlayLineAlways(MalfurionBrassRing, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_02_01);
			break;
		case 101:
			yield return PlayLineAlways(MalfurionBrassRing, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_02_01);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_VictoryDenied_01);
			yield return PlayLineAlways(IllidanBrassRing, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory03_01);
			yield return PlayLineAlways(IllidanBrassRing, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory04_01);
			GameState.Get().SetBusy(busy: false);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	public IEnumerator PlayVictoryLines()
	{
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory01_01);
		yield return PlayLineAlways(IllidanBrassRing, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory02_01);
		GameState.Get().SetBusy(busy: false);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		if (!(cardId == "BT_430"))
		{
			if (cardId == "BT_512")
			{
				yield return PlayLineAlways(TyrandeBrassRing, VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_02_01);
			}
		}
		else
		{
			yield return PlayLineAlways(TyrandeBrassRing, VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_01_01);
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
			yield return PlayLineAlways(MalfurionBrassRing, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01);
			break;
		case 5:
			yield return PlayLineAlways(TyrandeBrassRing, VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01);
			break;
		case 6:
			yield return PlayLineAlways(actor, VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01);
			break;
		case 7:
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01);
			break;
		case 9:
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeB_01);
			break;
		case 10:
			yield return PlayLineAlways(MalfurionBrassRing, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_Shocked_at_Illidan_02_01);
			break;
		case 11:
			yield return PlayLineAlways(actor, VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Loss_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHPrologueBoss);
	}
}
