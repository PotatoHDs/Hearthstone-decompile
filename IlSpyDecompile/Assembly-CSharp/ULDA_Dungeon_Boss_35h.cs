using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_35h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_BossBoneWraith_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_BossBoneWraith_01.prefab:efefe1e6ba6cdad43968953f3a5fd9c1");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_BossEarthquake_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_BossEarthquake_01.prefab:d9ad5d85626b4d74ea0617a1a4572cfc");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_BossLavaShock_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_BossLavaShock_01.prefab:b7ec166690a7ab948898bf70e4ec89a1");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_Death_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_Death_01.prefab:45bfeb4623a61db4bbaaf7ebe29bd753");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_DefeatPlayer_01.prefab:0e750a0c209070c4b890997d3dbae173");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_EmoteResponse_01.prefab:8837d6f82d9ecb84dacddf76ea1890a6");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_01.prefab:c1ec17c9051f45941853200dcc24db77");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_02.prefab:1d3f70dd36eeb884988eedcc4e2cc8e5");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_03.prefab:8b03b5b72de0d514ab2b127dfcd91156");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_04.prefab:89f57723c2679fd4683df3686b335201");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPowerRare_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPowerRare_01.prefab:8273e9e21c1ce5a489bf52716577048b");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_01.prefab:c3ae169a36043894783b8ddaece563c4");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_02 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_02.prefab:013a2830b46c0654a9ec36a987574cad");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_IdleRare_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_IdleRare_01.prefab:104d12a7571c8f14899ef769d83ebb3b");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_Intro_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_Intro_01.prefab:69e8dbb2a1090d0468b7c2f5692f420a");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_Misc_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_Misc_01.prefab:a49e3b90561852f438676adb6c76713a");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerAlAkir_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerAlAkir_01.prefab:fa56605e9c15a3c47834ca318eda3a2c");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerCamel_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerCamel_01.prefab:1d335fd6adf9ed240b6b106f58dd4954");

	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerRolltheBones_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerRolltheBones_01.prefab:c201eb6b42bbbf2479b0b8f4e2ac6259");

	private List<string> m_HeroPowerLines = new List<string>
	{
		VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_01, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_02, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_03, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_04, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_01, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_02, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_03, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_04, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_01, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_02,
		VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_03, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_04, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPowerRare_01
	};

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_01, VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_02, VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_01, VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_02, VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_01, VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_02, VO_ULDA_BOSS_35h_Male_BoneWraith_IdleRare_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_35h_Male_BoneWraith_BossBoneWraith_01, VO_ULDA_BOSS_35h_Male_BoneWraith_BossEarthquake_01, VO_ULDA_BOSS_35h_Male_BoneWraith_BossLavaShock_01, VO_ULDA_BOSS_35h_Male_BoneWraith_Death_01, VO_ULDA_BOSS_35h_Male_BoneWraith_DefeatPlayer_01, VO_ULDA_BOSS_35h_Male_BoneWraith_EmoteResponse_01, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_01, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_02, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_03, VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_04,
			VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPowerRare_01, VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_01, VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_02, VO_ULDA_BOSS_35h_Male_BoneWraith_IdleRare_01, VO_ULDA_BOSS_35h_Male_BoneWraith_Intro_01, VO_ULDA_BOSS_35h_Male_BoneWraith_Misc_01, VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerAlAkir_01, VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerCamel_01, VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerRolltheBones_01
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

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_35h_Male_BoneWraith_Intro_01;
		m_deathLine = VO_ULDA_BOSS_35h_Male_BoneWraith_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_35h_Male_BoneWraith_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "ULDA_Reno" && cardId != "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
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
		if (missionEvent == 101)
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_35h_Male_BoneWraith_Misc_01);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "NEW1_010":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerAlAkir_01);
				break;
			case "LOE_020":
			case "ULD_182":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerCamel_01);
				break;
			case "ICC_201":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerRolltheBones_01);
				break;
			}
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ULD_275":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_35h_Male_BoneWraith_BossBoneWraith_01);
				break;
			case "ULD_181":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_35h_Male_BoneWraith_BossEarthquake_01);
				break;
			case "BRM_011":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_35h_Male_BoneWraith_BossLavaShock_01);
				break;
			}
		}
	}
}
