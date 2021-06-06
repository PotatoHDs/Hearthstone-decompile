using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_74h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerBrazenZealot_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerBrazenZealot_01.prefab:4ddef806194558c4bad1f0f6cda87b40");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerDesperateMeasures_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerDesperateMeasures_01.prefab:10b6300309d799a4ca3bd33cffa6d536");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerPenance_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerPenance_01.prefab:ba5866db8f497404fbcdbf6b1dc9f2d5");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_Death_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_Death_01.prefab:a8743560fe64e17478acd7fef0788774");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_DefeatPlayer_01.prefab:434dba6b805689141bfcd4c065dc2c6b");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_EmoteResponse_01.prefab:15fee89c4c3068b40a4abb6ee7e1d0f1");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_01.prefab:21ee719686c9556479e167bf56c79415");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_02.prefab:6a7dac9f196114c42acc563b5c67f114");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_04.prefab:25e6a7304a976c14fa8ba326ffbebe5f");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_05.prefab:6f71f32b1a87a7a4c85e7e09924f7088");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_01.prefab:8ebb64f437aaf31489ba8471cae04fb1");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_02 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_02.prefab:22a4b041347a41a46af780d35761e93f");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_03 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_03.prefab:4482bb176cacb6d41af0a5f144051f7c");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_Intro_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_Intro_01.prefab:b1476815603ebc944a393d4f15901024");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_High_Priest_Amet_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_High_Priest_Amet_01.prefab:77016998e60f11a4a92be46558f2c45b");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Living_Monument_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Living_Monument_01.prefab:f3046fb234083d14786c4129883a9745");

	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Mogu_Cultist_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Mogu_Cultist_01.prefab:a06f53a896bb4384b9259e93c706c62b");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_01, VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_02, VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_04, VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_01, VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_02, VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerBrazenZealot_01, VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerDesperateMeasures_01, VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerPenance_01, VO_ULDA_BOSS_74h_Male_NefersetTolvir_Death_01, VO_ULDA_BOSS_74h_Male_NefersetTolvir_DefeatPlayer_01, VO_ULDA_BOSS_74h_Male_NefersetTolvir_EmoteResponse_01, VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_01, VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_02, VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_04, VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_05,
			VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_01, VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_02, VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_03, VO_ULDA_BOSS_74h_Male_NefersetTolvir_Intro_01, VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_High_Priest_Amet_01, VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Living_Monument_01, VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Mogu_Cultist_01
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
		m_introLine = VO_ULDA_BOSS_74h_Male_NefersetTolvir_Intro_01;
		m_deathLine = VO_ULDA_BOSS_74h_Male_NefersetTolvir_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_74h_Male_NefersetTolvir_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
			case "ULD_262":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_High_Priest_Amet_01);
				break;
			case "ULD_705":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Mogu_Cultist_01);
				break;
			case "ULD_193":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Living_Monument_01);
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
			case "ULD_145":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerBrazenZealot_01);
				break;
			case "DAL_141":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerDesperateMeasures_01);
				break;
			case "ULD_714":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerPenance_01);
				break;
			}
		}
	}
}
