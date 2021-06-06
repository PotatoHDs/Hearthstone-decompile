using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_43h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_BossTimeWarp_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_BossTimeWarp_01.prefab:183d37915f836cf4e9cc5545136e7771");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_Death_02 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_Death_02.prefab:9f007104467bc8b47bddf8814f36a796");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_DefeatPlayer_01.prefab:03602ba877b971942b82211782b024a4");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_EmoteResponse_01.prefab:fd1117caffcf287419549fd83cb9c45a");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_Intro_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_Intro_01.prefab:9eeced40701ab874d9a2224f876db8a3");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_PlayerNozdormu_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_PlayerNozdormu_01.prefab:64d2445dabb900e499ccee0f17b585e7");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_PlayerTemporus_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_PlayerTemporus_01.prefab:70aafb0f704a8774ca8bc4f2c1bd6024");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_PlayerTimeWarp_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_PlayerTimeWarp_01.prefab:825ee80004394b3428f00c3f67def6d9");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_01.prefab:d23bbbe5030cca14b9b6b27122ec31c2");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_02 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_02.prefab:d5e11881cc8e7da4bb5808e5c53d27fa");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_03 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_03.prefab:1bd952c1fcbb3284e8d0f6a4523f22e1");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_04 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_04.prefab:15086e2a34483e142a09f38854c2d840");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_05 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_05.prefab:06821994a69f851468ac0694e344503d");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_06 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_06.prefab:83ca55c537e3df3429ac37a67ffe25f9");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_TurnOne_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_TurnOne_01.prefab:7702bf67f79a89643a49d91745bc0000");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_01 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_01.prefab:7f7f9af17e3db0249ac4b62a87f5681f");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_02 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_02.prefab:da28857aa4b21e348a94d31e5397972a");

	private static readonly AssetReference VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_03 = new AssetReference("VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_03.prefab:46f1b3f800c9f304aa55bf2599315e43");

	private List<string> m_RopeExplodes = new List<string> { VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_01, VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_02, VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_03, VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_04, VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_05, VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_06 };

	private List<string> m_TurnStart = new List<string> { VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_01, VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_02, VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_43h_Female_BloodElf_BossTimeWarp_01, VO_DALA_BOSS_43h_Female_BloodElf_Death_02, VO_DALA_BOSS_43h_Female_BloodElf_DefeatPlayer_01, VO_DALA_BOSS_43h_Female_BloodElf_EmoteResponse_01, VO_DALA_BOSS_43h_Female_BloodElf_Intro_01, VO_DALA_BOSS_43h_Female_BloodElf_PlayerNozdormu_01, VO_DALA_BOSS_43h_Female_BloodElf_PlayerTemporus_01, VO_DALA_BOSS_43h_Female_BloodElf_PlayerTimeWarp_01, VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_01, VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_02,
			VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_03, VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_04, VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_05, VO_DALA_BOSS_43h_Female_BloodElf_RopeExplode_06, VO_DALA_BOSS_43h_Female_BloodElf_TurnOne_01, VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_01, VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_02, VO_DALA_BOSS_43h_Female_BloodElf_TurnStart_03
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_43h_Female_BloodElf_Intro_01;
		m_deathLine = VO_DALA_BOSS_43h_Female_BloodElf_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_43h_Female_BloodElf_EmoteResponse_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Rakanishu")
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
		switch (missionEvent)
		{
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_TurnStart);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_RopeExplodes);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_43h_Female_BloodElf_TurnOne_01);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "EX1_560":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_43h_Female_BloodElf_PlayerNozdormu_01);
				break;
			case "LOOT_538":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_43h_Female_BloodElf_PlayerTemporus_01);
				break;
			case "UNG_028t":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_43h_Female_BloodElf_PlayerTimeWarp_01);
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
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "UNG_028t")
			{
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_43h_Female_BloodElf_BossTimeWarp_01);
			}
		}
	}
}
