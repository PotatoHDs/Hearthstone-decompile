using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_30h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFirefly_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFirefly_01.prefab:7f4b2bcd0a897154781b45da0945666f");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamestrike_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamestrike_01.prefab:90d1ffbe274c5c74d896cf53c8cc4e8b");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamewaker_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamewaker_01.prefab:be690996d7ba7114893560caf4a89f4b");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_Death_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_Death_01.prefab:786ab936edab4ca4f9ec25ef1043ad5f");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_DefeatPlayer_01.prefab:195ab34934b1e4c4190b00a57628d8b4");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_EmoteResponse_01.prefab:388d6dc2e5873f34baef5edbf2f4e45c");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_01.prefab:d3da526643216d3469cb4affd279b486");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_03.prefab:4d51832d05d371e47b6858dac38b0b7b");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_04.prefab:efc05fffb80f51c4bb1d0e9422cbeb9e");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_05.prefab:6b91551cde887824a88ad5bb7d0339ae");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_01.prefab:8564c53c5c5ded248b8f960b770edc3d");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_02 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_02.prefab:ae9737b3f3f15444c9bf8aace76dc766");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_03 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_03.prefab:4e5103210c9e69140a919c27c17ed2ad");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_Intro_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_Intro_01.prefab:c0cb28dc94ff7af4a8d1a72181307912");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Fireball_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Fireball_01.prefab:8dd701ead2a23324ab6cb65105829b13");

	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Pharaoh_Cat_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Pharaoh_Cat_01.prefab:cbadc943d6f8d4847869588fc6b42dd9");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_01, VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_03, VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_04, VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_01, VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_02, VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFirefly_01, VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamestrike_01, VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamewaker_01, VO_ULDA_BOSS_30h_Female_NefersetTolvir_Death_01, VO_ULDA_BOSS_30h_Female_NefersetTolvir_DefeatPlayer_01, VO_ULDA_BOSS_30h_Female_NefersetTolvir_EmoteResponse_01, VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_01, VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_03, VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_04, VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_05,
			VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_01, VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_02, VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_03, VO_ULDA_BOSS_30h_Female_NefersetTolvir_Intro_01, VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Fireball_01, VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Pharaoh_Cat_01
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

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_30h_Female_NefersetTolvir_Intro_01;
		m_deathLine = VO_ULDA_BOSS_30h_Female_NefersetTolvir_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_30h_Female_NefersetTolvir_EmoteResponse_01;
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
		if (!(cardId == "ULD_186"))
		{
			if (cardId == "CS2_029")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Fireball_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Pharaoh_Cat_01);
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
			case "UNG_809":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFirefly_01);
				break;
			case "CS2_032":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamestrike_01);
				break;
			case "BRM_002":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamewaker_01);
				break;
			}
		}
	}
}
