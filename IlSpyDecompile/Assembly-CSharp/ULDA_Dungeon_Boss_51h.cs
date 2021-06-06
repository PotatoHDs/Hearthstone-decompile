using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_51h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerAnubisathDefender_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerAnubisathDefender_01.prefab:3e2dfe63028792941a30fd3c818caba6");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerEmbalmingRitual_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerEmbalmingRitual_01.prefab:08aef91a3a8b66244bd306daea060474");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerPsychopomp_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerPsychopomp_01.prefab:2b2bc6b4297687f43bde99e100bb5ceb");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_Death_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_Death_01.prefab:60891177533b5934ea4d499152eef7c9");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_DefeatPlayer_01.prefab:c34d561cc66605d4b95fe836ccc523b0");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_EmoteResponse_01.prefab:32ba78c9a0364de48b8abf823ef603aa");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_01.prefab:49940cd11cadb5041b6431367c43d164");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_02.prefab:a384f835f507a0349b2ac39ef70e2e34");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_03.prefab:a146aca8a334e684c873d0f3da19a73e");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_05.prefab:587a74d18825d9a4bbdc6b0349cd9210");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_Idle_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_Idle_01.prefab:49fd34f7c4ba8f54e91406a4a38ac49c");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_Idle_03 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_Idle_03.prefab:5d0d8db1387ffdd458f6fad072550935");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_Intro_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_Intro_01.prefab:ecb6ded326712904889601681e0a5f60");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_IntroSpecial_Elise_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_IntroSpecial_Elise_01.prefab:54ea30eff03a410458187db325b3ba34");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Anubisath_Defender_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Anubisath_Defender_01.prefab:ba414294e85df514990c7a501dbb0ed7");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Enslaved_Guardian_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Enslaved_Guardian_01.prefab:12e1b3782b0072843858d63157d2b12a");

	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Pharaoh_Cat_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Pharaoh_Cat_01.prefab:0c73fa01c1b00d14f82f99c0e09039d3");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_01, VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_02, VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_03, VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_51h_Female_Anubisath_Idle_01, VO_ULDA_BOSS_51h_Female_Anubisath_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerAnubisathDefender_01, VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerEmbalmingRitual_01, VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerPsychopomp_01, VO_ULDA_BOSS_51h_Female_Anubisath_Death_01, VO_ULDA_BOSS_51h_Female_Anubisath_DefeatPlayer_01, VO_ULDA_BOSS_51h_Female_Anubisath_EmoteResponse_01, VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_01, VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_02, VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_03, VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_05,
			VO_ULDA_BOSS_51h_Female_Anubisath_Idle_01, VO_ULDA_BOSS_51h_Female_Anubisath_Idle_03, VO_ULDA_BOSS_51h_Female_Anubisath_Intro_01, VO_ULDA_BOSS_51h_Female_Anubisath_IntroSpecial_Elise_01, VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Anubisath_Defender_01, VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Enslaved_Guardian_01, VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Pharaoh_Cat_01
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
		m_introLine = VO_ULDA_BOSS_51h_Female_Anubisath_Intro_01;
		m_deathLine = VO_ULDA_BOSS_51h_Female_Anubisath_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_51h_Female_Anubisath_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START && cardId != "ULDA_Elise")
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
			case "ULD_186":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Pharaoh_Cat_01);
				break;
			case "ULD_138":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Anubisath_Defender_01);
				break;
			case "ULD_271":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Enslaved_Guardian_01);
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
			case "ULD_138":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerAnubisathDefender_01);
				break;
			case "ULD_265":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerEmbalmingRitual_01);
				break;
			case "ULD_268":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerPsychopomp_01);
				break;
			}
		}
	}
}
