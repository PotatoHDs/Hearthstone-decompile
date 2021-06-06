using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_35h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_BossFungalEnchanter_02 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_BossFungalEnchanter_02.prefab:25c05fb426d9fb544a0ebc10562d2573");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_BossSpellNotHeal_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_BossSpellNotHeal_01.prefab:0ec1d3d0a1f8e0b4a950d383359e6c29");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_Death_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_Death_01.prefab:3eefb2ed7c30d8b4e9d0df6753e3dee1");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_DefeatPlayer_01.prefab:bc8ffc05d0477a74d81bd9ca0438e357");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_EmoteResponse_02 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_EmoteResponse_02.prefab:ca986dbc64768f54f8a017f3f128f226");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_HeroPower_02.prefab:dc3d97af82fdd72459e432738740006b");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_HeroPower_03.prefab:b3c85457720f0f94784965cccc08eb81");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_HeroPower_04.prefab:302e1e07f01bcfa49a513eba2f9e87a1");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_01.prefab:cd257dd25f7049349bfa5baeab0ec1e1");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_02 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_02.prefab:2968be61389f10748919cee312354716");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_03 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_03.prefab:ca2cb80e1d64fcc4cab8ea6a6b6d5c01");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_Idle_01.prefab:52b31b78da5a0814795b9aafc811e307");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_Idle_02.prefab:8b9870e311ec6b54d9a92e51581d973f");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_Idle_03.prefab:9384798e42b21054e82d4ffa1729729e");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_Intro_01.prefab:a16c116a544085549b710c7eaf71aac9");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_IntroGeorge_02 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_IntroGeorge_02.prefab:f8688b867b2d6f04eb3b0ff2709e5cb0");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_PlayerHolyNova_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_PlayerHolyNova_01.prefab:c2bb5c4f04f66ba4b9260a154448f350");

	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_PlayerShadowForm_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_PlayerShadowForm_01.prefab:2c45b907579c33c4ba68fc0b80ce2e8e");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_35h_Female_Human_Idle_01, VO_DALA_BOSS_35h_Female_Human_Idle_02, VO_DALA_BOSS_35h_Female_Human_Idle_03 };

	private static List<string> m_HeroPower = new List<string> { VO_DALA_BOSS_35h_Female_Human_HeroPower_02, VO_DALA_BOSS_35h_Female_Human_HeroPower_03, VO_DALA_BOSS_35h_Female_Human_HeroPower_04 };

	private static List<string> m_HeroPowerFull = new List<string> { VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_01, VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_02, VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_35h_Female_Human_BossFungalEnchanter_02, VO_DALA_BOSS_35h_Female_Human_BossSpellNotHeal_01, VO_DALA_BOSS_35h_Female_Human_Death_01, VO_DALA_BOSS_35h_Female_Human_DefeatPlayer_01, VO_DALA_BOSS_35h_Female_Human_EmoteResponse_02, VO_DALA_BOSS_35h_Female_Human_HeroPower_02, VO_DALA_BOSS_35h_Female_Human_HeroPower_03, VO_DALA_BOSS_35h_Female_Human_HeroPower_04, VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_01, VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_02,
			VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_03, VO_DALA_BOSS_35h_Female_Human_Idle_01, VO_DALA_BOSS_35h_Female_Human_Idle_02, VO_DALA_BOSS_35h_Female_Human_Idle_03, VO_DALA_BOSS_35h_Female_Human_Intro_01, VO_DALA_BOSS_35h_Female_Human_IntroGeorge_02, VO_DALA_BOSS_35h_Female_Human_PlayerHolyNova_01, VO_DALA_BOSS_35h_Female_Human_PlayerShadowForm_01
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
		m_introLine = VO_DALA_BOSS_35h_Female_Human_Intro_01;
		m_deathLine = VO_DALA_BOSS_35h_Female_Human_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_35h_Female_Human_EmoteResponse_02;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_35h_Female_Human_IntroGeorge_02, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Squeamlish")
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerFull);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_35h_Female_Human_BossSpellNotHeal_01);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		if (!(cardId == "CS1_112"))
		{
			if (cardId == "EX1_625")
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_35h_Female_Human_PlayerShadowForm_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_35h_Female_Human_PlayerHolyNova_01);
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
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
			switch (cardId)
			{
			case "LOOT_388":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_35h_Female_Human_BossFungalEnchanter_02);
				break;
			case "LOOT_278t1":
			case "LOOT_278t2":
			case "LOOT_278t3":
			case "LOOT_278t4":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_35h_Female_Human_BossSpellNotHeal_01);
				break;
			}
		}
	}
}
