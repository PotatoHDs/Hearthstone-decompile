using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_01h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_BossLackey_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_BossLackey_01.prefab:210378a566be5d64cb7e1cb9831be00b");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_BossMarkedShot_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_BossMarkedShot_01.prefab:53d7f7f6fd7148148b170560acee7859");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_BossPressurePlate_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_BossPressurePlate_01.prefab:8247465a3f19e8c40a60cceec85dd836");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_BossTriggerRapidFire_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_BossTriggerRapidFire_01.prefab:b090f8036c62d81428590821605aefc9");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_Death_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_Death_01.prefab:0667acc72b6264c41a5378f7dae7a25e");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_DefeatPlayer_01.prefab:6e605d1820c14124cbf0ad98f9b6ad93");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_EmoteResponse_01.prefab:004b2084937f4d24b93c0e86a2ed985e");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_HeroPower_01.prefab:cf017339da5e28248b7b5405a15fde10");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_HeroPower_02.prefab:d9469ab5e488c85409d301facb1a0f3f");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_HeroPower_04.prefab:52e3323c9a5fb3e42b7461a5332265b8");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_HeroPower_05.prefab:cabd559ba2df7ff41af8f0bbe3033bc6");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_Idle_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_Idle_01.prefab:055e09a2b533b62469c79d12c7553766");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_Idle_02 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_Idle_02.prefab:aca575db3a6bdd041a0e7864e9d07cd9");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_Idle_03 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_Idle_03.prefab:dfcd00f746d57644482e276d355450a9");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_Intro_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_Intro_01.prefab:0ceccd4cc4c695f499d9afe387bb2094");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_IntroFinley_01.prefab:4179dc8ffc9623446a904c6854f75d4b");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_PlayerBlunderbuss_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_PlayerBlunderbuss_01.prefab:86979e14b68e54f46b3a712b6f429af9");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_PlayerGatlingWand_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_PlayerGatlingWand_01.prefab:321bcdb0194dbf142ab39539f8500276");

	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_PlayerGoblinBomb_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_PlayerGoblinBomb_01.prefab:4af7a4406d320e4418a87bcd21f6618e");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_01h_Male_Human_HeroPower_01, VO_ULDA_BOSS_01h_Male_Human_HeroPower_02, VO_ULDA_BOSS_01h_Male_Human_HeroPower_04, VO_ULDA_BOSS_01h_Male_Human_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_01h_Male_Human_Idle_01, VO_ULDA_BOSS_01h_Male_Human_Idle_02, VO_ULDA_BOSS_01h_Male_Human_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_01h_Male_Human_BossLackey_01, VO_ULDA_BOSS_01h_Male_Human_BossMarkedShot_01, VO_ULDA_BOSS_01h_Male_Human_BossPressurePlate_01, VO_ULDA_BOSS_01h_Male_Human_BossTriggerRapidFire_01, VO_ULDA_BOSS_01h_Male_Human_Death_01, VO_ULDA_BOSS_01h_Male_Human_DefeatPlayer_01, VO_ULDA_BOSS_01h_Male_Human_EmoteResponse_01, VO_ULDA_BOSS_01h_Male_Human_HeroPower_01, VO_ULDA_BOSS_01h_Male_Human_HeroPower_02, VO_ULDA_BOSS_01h_Male_Human_HeroPower_04,
			VO_ULDA_BOSS_01h_Male_Human_HeroPower_05, VO_ULDA_BOSS_01h_Male_Human_Idle_01, VO_ULDA_BOSS_01h_Male_Human_Idle_02, VO_ULDA_BOSS_01h_Male_Human_Idle_03, VO_ULDA_BOSS_01h_Male_Human_Intro_01, VO_ULDA_BOSS_01h_Male_Human_IntroFinley_01, VO_ULDA_BOSS_01h_Male_Human_PlayerBlunderbuss_01, VO_ULDA_BOSS_01h_Male_Human_PlayerGatlingWand_01, VO_ULDA_BOSS_01h_Male_Human_PlayerGoblinBomb_01
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
		m_introLine = VO_ULDA_BOSS_01h_Male_Human_Intro_01;
		m_deathLine = VO_ULDA_BOSS_01h_Male_Human_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_01h_Male_Human_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_01h_Male_Human_IntroFinley_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			case "ULDA_401":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_01h_Male_Human_PlayerBlunderbuss_01);
				break;
			case "ULDA_207":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_01h_Male_Human_PlayerGatlingWand_01);
				break;
			case "BOT_031":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_01h_Male_Human_PlayerGoblinBomb_01);
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
			case "ULD_616":
			case "DAL_613":
			case "DAL_614":
			case "DAL_615":
			case "DAL_739":
			case "DAL_741":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_01h_Male_Human_BossLackey_01);
				break;
			case "DAL_371":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_01h_Male_Human_BossMarkedShot_01);
				break;
			case "ULD_152":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_01h_Male_Human_BossPressurePlate_01);
				break;
			case "DAL_373":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_01h_Male_Human_BossTriggerRapidFire_01);
				break;
			}
		}
	}
}
