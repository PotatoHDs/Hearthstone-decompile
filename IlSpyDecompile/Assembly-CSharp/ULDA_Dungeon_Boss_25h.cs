using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_25h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_BossCrystalMerchant_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_BossCrystalMerchant_01.prefab:3eb5a7a9990665148b470ca6be88e106");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_BossTriggerPlatedBeetleDeathrattle_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_BossTriggerPlatedBeetleDeathrattle_01.prefab:8e96b66d6319f3748a1354b0876583ad");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_BossWhirlkickMaster_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_BossWhirlkickMaster_01.prefab:9c5569a166b7d04449ef78ed63115d6b");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_Death_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_Death_01.prefab:79d5bad257ecaa041982253d28c8cc8a");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_DefeatPlayer_01.prefab:c62d0bc8daf02eb4bbf933e2965b6132");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_EmoteResponse_01.prefab:a8128f4f1643d0143ae4d364b3ac8264");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_01.prefab:1bb99fa2cc2ab9649a3e2861611e7948");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_02.prefab:fa60da7f193e6a840ba6803927c6c1ee");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_03.prefab:02fc7d8992482844981059a2644342cf");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_Idle_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_Idle_01.prefab:36473927d33c6aa4281fbc530568b65b");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_Idle_02 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_Idle_02.prefab:1b04883e394e7e9419bfff09303f020e");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_Idle_03 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_Idle_03.prefab:90e35723200f64e4183ce1af6b407072");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_Intro_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_Intro_01.prefab:6b9d4e2b8f8264146923e435ac904355");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_IntroBrann_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_IntroBrann_01.prefab:20e80b7557f2726408240ee2d058e9bd");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_IntroFinley_01.prefab:81f88d793350cdb45b5a85a3e400b30c");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_PlayerBugCollector_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_PlayerBugCollector_01.prefab:565195085f4af444dbf89d5aa96b5e11");

	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_PlayerFinleysMount_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_PlayerFinleysMount_01.prefab:1f534cab6dc1fb249a6d841fa11af2a8");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_01, VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_02, VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_03 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_25h_Female_SandTroll_Idle_01, VO_ULDA_BOSS_25h_Female_SandTroll_Idle_02, VO_ULDA_BOSS_25h_Female_SandTroll_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_25h_Female_SandTroll_BossCrystalMerchant_01, VO_ULDA_BOSS_25h_Female_SandTroll_BossTriggerPlatedBeetleDeathrattle_01, VO_ULDA_BOSS_25h_Female_SandTroll_BossWhirlkickMaster_01, VO_ULDA_BOSS_25h_Female_SandTroll_Death_01, VO_ULDA_BOSS_25h_Female_SandTroll_DefeatPlayer_01, VO_ULDA_BOSS_25h_Female_SandTroll_EmoteResponse_01, VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_01, VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_02, VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_03, VO_ULDA_BOSS_25h_Female_SandTroll_Idle_01,
			VO_ULDA_BOSS_25h_Female_SandTroll_Idle_02, VO_ULDA_BOSS_25h_Female_SandTroll_Idle_03, VO_ULDA_BOSS_25h_Female_SandTroll_Intro_01, VO_ULDA_BOSS_25h_Female_SandTroll_IntroBrann_01, VO_ULDA_BOSS_25h_Female_SandTroll_IntroFinley_01, VO_ULDA_BOSS_25h_Female_SandTroll_PlayerBugCollector_01, VO_ULDA_BOSS_25h_Female_SandTroll_PlayerFinleysMount_01
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
		m_introLine = VO_ULDA_BOSS_25h_Female_SandTroll_Intro_01;
		m_deathLine = VO_ULDA_BOSS_25h_Female_SandTroll_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_25h_Female_SandTroll_EmoteResponse_01;
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (missionEvent == 101)
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_25h_Female_SandTroll_BossTriggerPlatedBeetleDeathrattle_01);
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
		if (!(cardId == "ULD_712"))
		{
			if (cardId == "ULDA_501")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_25h_Female_SandTroll_PlayerFinleysMount_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_25h_Female_SandTroll_PlayerBugCollector_01);
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
		if (!(cardId == "ULD_133"))
		{
			if (cardId == "ULD_231")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_25h_Female_SandTroll_BossWhirlkickMaster_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_25h_Female_SandTroll_BossCrystalMerchant_01);
		}
	}
}
