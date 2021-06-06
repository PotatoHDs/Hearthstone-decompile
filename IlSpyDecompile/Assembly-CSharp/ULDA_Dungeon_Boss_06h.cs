using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_06h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Livewire_Lance_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Livewire_Lance_01.prefab:6d8d09361821ecf46805a5e88a8a310f");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Mirage_Blade_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Mirage_Blade_01.prefab:c2b0b2d06c4b4ce49b52231c00faa5ef");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_DeathALT_01.prefab:d8b6b87063c1bed4e9a7301b1fe53799");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_DefeatPlayer_01.prefab:7efa6080bc4c581428d993ffb2930e3e");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_EmoteResponse_01.prefab:d112eb7ac63daef4a99d2c91093ece59");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_01.prefab:2835c6feeb8eb32488c1008ea5222033");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_02.prefab:01c74b291f9e0bc4caccf2efd3762a9a");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_03.prefab:7d41bc2ed6df25f4b98c1b721ea7daba");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_04.prefab:23dd9ecddfd00c345a534e3d0bed8b0d");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_05.prefab:060201ce2f8a3eb4c863349fc3e164b2");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_Idle_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_Idle_01.prefab:1f59eaa3f2cfcd94faca67a031f19350");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_Idle_02 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_Idle_02.prefab:9c5d3eaabd54195459184e8e7d5ef496");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_Idle_03 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_Idle_03.prefab:df2bf04f1d4043747a518c342df19865");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_Intro_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_Intro_01.prefab:0f58b798e775b5848a3e9b7f08322b74");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_IntroBrannRespose_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_IntroBrannRespose_01.prefab:565a41251ba668f409f1cdd1ad130bf3");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Armored_Goon_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Armored_Goon_01.prefab:25fdcf8825f977042b451106ff42a359");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Madam_Goya_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Madam_Goya_01.prefab:32053c00561edcc48b08919ba22e0a4c");

	private static readonly AssetReference VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Mischief_Maker_01 = new AssetReference("VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Mischief_Maker_01.prefab:0a55e9df35c37cc408ecfd161046dde3");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_01, VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_02, VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_03, VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_04, VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_06h_Male_Pandaren_Idle_01, VO_ULDA_BOSS_06h_Male_Pandaren_Idle_02, VO_ULDA_BOSS_06h_Male_Pandaren_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Livewire_Lance_01, VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Mirage_Blade_01, VO_ULDA_BOSS_06h_Male_Pandaren_DeathALT_01, VO_ULDA_BOSS_06h_Male_Pandaren_DefeatPlayer_01, VO_ULDA_BOSS_06h_Male_Pandaren_EmoteResponse_01, VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_01, VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_02, VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_03, VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_04, VO_ULDA_BOSS_06h_Male_Pandaren_HeroPower_05,
			VO_ULDA_BOSS_06h_Male_Pandaren_Idle_01, VO_ULDA_BOSS_06h_Male_Pandaren_Idle_02, VO_ULDA_BOSS_06h_Male_Pandaren_Idle_03, VO_ULDA_BOSS_06h_Male_Pandaren_Intro_01, VO_ULDA_BOSS_06h_Male_Pandaren_IntroBrannRespose_01, VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Armored_Goon_01, VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Madam_Goya_01, VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Mischief_Maker_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_06h_Male_Pandaren_Intro_01;
		m_deathLine = VO_ULDA_BOSS_06h_Male_Pandaren_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_06h_Male_Pandaren_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_06h_Male_Pandaren_IntroBrannRespose_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (missionEvent == 101)
		{
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerLines);
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
			case "ULD_709":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Armored_Goon_01);
				break;
			case "CFM_672":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Madam_Goya_01);
				break;
			case "ULD_229":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_06h_Male_Pandaren_PlayerTrigger_Mischief_Maker_01);
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
		if (!(cardId == "ULD_326t"))
		{
			if (cardId == "ULD_708")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Livewire_Lance_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_06h_Male_Pandaren_BossTrigger_Mirage_Blade_01);
		}
	}
}
