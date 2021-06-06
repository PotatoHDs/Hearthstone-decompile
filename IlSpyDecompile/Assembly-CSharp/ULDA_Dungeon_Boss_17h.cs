using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_17h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_BossCheatDeathTriggerLeperGnome_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_BossCheatDeathTriggerLeperGnome_01.prefab:0fc92e47dd133fe4a8034a891f2e00c3");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_BossNecriumBlade_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_BossNecriumBlade_01.prefab:c1ee367016a08d241ade51fbcf12847a");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_BossPlagueScientist_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_BossPlagueScientist_01.prefab:4effaa1d5f8968f4b9dd315e84e79dd5");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_Death_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_Death_01.prefab:9f23f72e9732ae44da916dc8509b3458");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_DefeatPlayer_01.prefab:25b70b6297e4f2942b078d06f6d257b9");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_EmoteResponse_01.prefab:810df3ccfc55ced408ba3c3ee15da5bc");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_01.prefab:e1d43ec3083d57e44ba87b754237405d");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_02.prefab:13c19d1da951a384d9bc42e8963f988c");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_03.prefab:8b8a6cf7ce5c9b84fa56f8f136e05d1d");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_04.prefab:19392619422ffcb4dbaa13117ea5908f");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_05.prefab:a6df2692510fb494b8abe58d97535f31");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_Idle_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_Idle_01.prefab:6f9522927c1b061409aa51557f222f57");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_Idle_02 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_Idle_02.prefab:ae0accc3c5ad15c44b9fb0d61cd0231c");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_Idle_03 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_Idle_03.prefab:64be5b3354f97f04cb94ebc513a9a38b");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_Intro_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_Intro_01.prefab:5e307745ef7a8dc478c8b4d8ece7b43c");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Diseased_Vulture_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Diseased_Vulture_01.prefab:7be040567d5cb0442a8b06580c513f38");

	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Gnomebliterator_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Gnomebliterator_01.prefab:cb22f7b71be483545a93975944e57548");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_01, VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_02, VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_03, VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_04, VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_17h_Male_Gnome_Idle_01, VO_ULDA_BOSS_17h_Male_Gnome_Idle_02, VO_ULDA_BOSS_17h_Male_Gnome_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_17h_Male_Gnome_BossCheatDeathTriggerLeperGnome_01, VO_ULDA_BOSS_17h_Male_Gnome_BossNecriumBlade_01, VO_ULDA_BOSS_17h_Male_Gnome_BossPlagueScientist_01, VO_ULDA_BOSS_17h_Male_Gnome_Death_01, VO_ULDA_BOSS_17h_Male_Gnome_DefeatPlayer_01, VO_ULDA_BOSS_17h_Male_Gnome_EmoteResponse_01, VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_01, VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_02, VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_03, VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_04,
			VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_05, VO_ULDA_BOSS_17h_Male_Gnome_Idle_01, VO_ULDA_BOSS_17h_Male_Gnome_Idle_02, VO_ULDA_BOSS_17h_Male_Gnome_Idle_03, VO_ULDA_BOSS_17h_Male_Gnome_Intro_01, VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Diseased_Vulture_01, VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Gnomebliterator_01
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
		m_introLine = VO_ULDA_BOSS_17h_Male_Gnome_Intro_01;
		m_deathLine = VO_ULDA_BOSS_17h_Male_Gnome_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_17h_Male_Gnome_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "ULDA_Elise" && cardId != "ULDA_Finley")
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
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_17h_Male_Gnome_BossCheatDeathTriggerLeperGnome_01);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "ULDA_115"))
		{
			if (cardId == "ULD_167")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Diseased_Vulture_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Gnomebliterator_01);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "BOT_286"))
		{
			if (cardId == "ICC_809")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_17h_Male_Gnome_BossPlagueScientist_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_17h_Male_Gnome_BossNecriumBlade_01);
		}
	}
}
