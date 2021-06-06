using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_43h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_BossGnomeferatu_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_BossGnomeferatu_01.prefab:1f50840d32b924346a1b2d7618bd6c1a");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_BossSiphonSoul_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_BossSiphonSoul_01.prefab:236fe058a193aa04ea2ebc32529f2e08");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_BossSoulInfusion_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_BossSoulInfusion_01.prefab:62771f4e154f7ce43b0d437ec5a09b63");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_DeathALT_01.prefab:fd1b466e8a781df4d90ccb953afe6f64");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_DefeatPlayer_01.prefab:438ff5a226b67b544889dd690db95ee8");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_EmoteResponse_01.prefab:ad8f8039cd80e6f4ebdaa473e44c155e");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_HeroPower_01.prefab:1c3729a4f9b71ff42bcec0d0bc6b50aa");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_HeroPower_02.prefab:bd1f00ab53b348b438fe3acfb68b2c81");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_HeroPower_03.prefab:15625ab2f64ad2d4887720071fd0b197");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_HeroPower_04.prefab:f5c56522df6dbf04496484ee0b7a607b");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_HeroPower_05.prefab:941daf7a6b214794b89fc110b8bbef24");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_Idle1_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_Idle1_01.prefab:31150cedd029ba54989300d32c484d1c");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_Idle2_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_Idle2_01.prefab:d6c17c65068a0504ab426314cb49f99e");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_Idle3_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_Idle3_01.prefab:1ab4758de3265f44ab6d04ac0b3521d1");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_Intro_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_Intro_01.prefab:fe99cd47663465d42a597a8eb48be752");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_IntroElise_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_IntroElise_01.prefab:fbe3c5d185103724abd1eeef16b85e81");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_PlayerBookofSpecters_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_PlayerBookofSpecters_01.prefab:ffaa292aa121ef24cb388e5547146c6f");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_PlayerBookoftheDeadTreasure_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_PlayerBookoftheDeadTreasure_01.prefab:57f3417a58cebf84d8c1a9d195c0840f");

	private static readonly AssetReference VO_ULDA_BOSS_43h_Female_Human_PlayerPlague_01 = new AssetReference("VO_ULDA_BOSS_43h_Female_Human_PlayerPlague_01.prefab:d3a6bc6ac0f714a4c969c017fedc34af");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_43h_Female_Human_HeroPower_01, VO_ULDA_BOSS_43h_Female_Human_HeroPower_02, VO_ULDA_BOSS_43h_Female_Human_HeroPower_03, VO_ULDA_BOSS_43h_Female_Human_HeroPower_04, VO_ULDA_BOSS_43h_Female_Human_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_43h_Female_Human_Idle1_01, VO_ULDA_BOSS_43h_Female_Human_Idle2_01, VO_ULDA_BOSS_43h_Female_Human_Idle3_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_43h_Female_Human_BossGnomeferatu_01, VO_ULDA_BOSS_43h_Female_Human_BossSiphonSoul_01, VO_ULDA_BOSS_43h_Female_Human_BossSoulInfusion_01, VO_ULDA_BOSS_43h_Female_Human_DeathALT_01, VO_ULDA_BOSS_43h_Female_Human_DefeatPlayer_01, VO_ULDA_BOSS_43h_Female_Human_EmoteResponse_01, VO_ULDA_BOSS_43h_Female_Human_HeroPower_01, VO_ULDA_BOSS_43h_Female_Human_HeroPower_02, VO_ULDA_BOSS_43h_Female_Human_HeroPower_03, VO_ULDA_BOSS_43h_Female_Human_HeroPower_04,
			VO_ULDA_BOSS_43h_Female_Human_HeroPower_05, VO_ULDA_BOSS_43h_Female_Human_Idle1_01, VO_ULDA_BOSS_43h_Female_Human_Idle2_01, VO_ULDA_BOSS_43h_Female_Human_Idle3_01, VO_ULDA_BOSS_43h_Female_Human_Intro_01, VO_ULDA_BOSS_43h_Female_Human_IntroElise_01, VO_ULDA_BOSS_43h_Female_Human_PlayerBookofSpecters_01, VO_ULDA_BOSS_43h_Female_Human_PlayerBookoftheDeadTreasure_01, VO_ULDA_BOSS_43h_Female_Human_PlayerPlague_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_43h_Female_Human_Intro_01;
		m_deathLine = VO_ULDA_BOSS_43h_Female_Human_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_43h_Female_Human_EmoteResponse_01;
	}

	public override void OnPlayThinkEmote()
	{
		if (m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (currentPlayer.IsFriendlySide() && !currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			if (m_IdleLines.Count != 0)
			{
				string line = m_IdleLines[0];
				m_IdleLines.RemoveAt(0);
				Gameplay.Get().StartCoroutine(PlayBossLine(actor, line));
			}
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_43h_Female_Human_IntroElise_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
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
		_ = missionEvent;
		yield return base.HandleMissionEventWithTiming(missionEvent);
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
			case "GIL_548":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_43h_Female_Human_PlayerBookofSpecters_01);
				break;
			case "ULDA_006":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_43h_Female_Human_PlayerBookoftheDeadTreasure_01);
				break;
			case "ULD_172":
			case "ULD_707":
			case "ULD_715":
			case "ULD_717":
			case "ULD_718":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_43h_Female_Human_PlayerPlague_01);
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
			case "ICC_407":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_43h_Female_Human_BossGnomeferatu_01);
				break;
			case "EX1_309":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_43h_Female_Human_BossSiphonSoul_01);
				break;
			case "BOT_263":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_43h_Female_Human_BossSoulInfusion_01);
				break;
			}
		}
	}
}
