using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_42h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_BossCrushingHand_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_BossCrushingHand_01.prefab:cedaca41e3476114bb40ba0232879d32");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_BossDustDevil_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_BossDustDevil_01.prefab:3d008fc61c188a24bb6dabca39cb45c5");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_BossSandstormElemental_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_BossSandstormElemental_01.prefab:5959d1f06c5bf2740a86fe2c982fba03");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_Death_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_Death_01.prefab:dd918dedf2b3b65408ff34e74e4bf0fe");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_DefeatPlayer_01.prefab:536049549f204b54ebf0ae19989feff0");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_EmoteResponse_01.prefab:c56a739731416ad4796ea7b5046692ac");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_01.prefab:b6acc464be384f94a8922260c1e02935");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_02.prefab:791a3f238360a4f41924af56cf5526ae");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_03.prefab:3c0f649e5dbf05a45980e0b752fe649c");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_04.prefab:ce12123c528174243b92d52b1a0cead6");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_Idle_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_Idle_01.prefab:0d637de1df77bbf42b8e2c1b3b1b934a");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_Idle_02 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_Idle_02.prefab:cb30f25b73f9d8b49b1e546cf467719e");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_Idle_03 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_Idle_03.prefab:59b6a956a46a0c6479de8584bc9139c9");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_Intro_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_Intro_01.prefab:7e5b42817311e254daa409cb18b88bcc");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_IntroFinley_01.prefab:36e38e92e18f1544b85c66053309018a");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_PlayerFarakkiBattleAxe_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_PlayerFarakkiBattleAxe_01.prefab:ff57e5b557e902e41a33107f9cc19414");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandbinder_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandbinder_01.prefab:5e7b52c2cabbb014587f0d9361e69065");

	private static readonly AssetReference VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandDrudge_01 = new AssetReference("VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandDrudge_01.prefab:7599c448afe801940b8514aad22de783");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_01, VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_02, VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_03, VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_04 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_42h_Male_SandTroll_Idle_01, VO_ULDA_BOSS_42h_Male_SandTroll_Idle_02, VO_ULDA_BOSS_42h_Male_SandTroll_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_42h_Male_SandTroll_BossCrushingHand_01, VO_ULDA_BOSS_42h_Male_SandTroll_BossDustDevil_01, VO_ULDA_BOSS_42h_Male_SandTroll_BossSandstormElemental_01, VO_ULDA_BOSS_42h_Male_SandTroll_Death_01, VO_ULDA_BOSS_42h_Male_SandTroll_DefeatPlayer_01, VO_ULDA_BOSS_42h_Male_SandTroll_EmoteResponse_01, VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_01, VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_02, VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_03, VO_ULDA_BOSS_42h_Male_SandTroll_HeroPower_04,
			VO_ULDA_BOSS_42h_Male_SandTroll_Idle_01, VO_ULDA_BOSS_42h_Male_SandTroll_Idle_02, VO_ULDA_BOSS_42h_Male_SandTroll_Idle_03, VO_ULDA_BOSS_42h_Male_SandTroll_Intro_01, VO_ULDA_BOSS_42h_Male_SandTroll_IntroFinley_01, VO_ULDA_BOSS_42h_Male_SandTroll_PlayerFarakkiBattleAxe_01, VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandbinder_01, VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandDrudge_01
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
		m_introLine = VO_ULDA_BOSS_42h_Male_SandTroll_Intro_01;
		m_deathLine = VO_ULDA_BOSS_42h_Male_SandTroll_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_42h_Male_SandTroll_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "ULDA_Finley")
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
			case "TRL_304":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_42h_Male_SandTroll_PlayerFarakkiBattleAxe_01);
				break;
			case "GIL_581":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandbinder_01);
				break;
			case "TRL_131":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_42h_Male_SandTroll_PlayerSandDrudge_01);
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
			case "LOOT_060":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_42h_Male_SandTroll_BossCrushingHand_01);
				break;
			case "EX1_243":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_42h_Male_SandTroll_BossDustDevil_01);
				break;
			case "ULD_158":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_42h_Male_SandTroll_BossSandstormElemental_01);
				break;
			}
		}
	}
}
