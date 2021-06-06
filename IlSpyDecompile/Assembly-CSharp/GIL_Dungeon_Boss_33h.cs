using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_33h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_WeaponKillLines = new List<string> { "VO_GILA_BOSS_33h_Male_Mech_EventWeaponKill_01.prefab:37a31f0f76dfdf64ea7758fe65286dcf", "VO_GILA_BOSS_33h_Male_Mech_EventWeaponKill_02.prefab:f031f0ad30432914faebc7910e4b8dfc", "VO_GILA_BOSS_33h_Male_Mech_EventWeaponKill_03.prefab:088df2cc43594914f978f3cd6ffe4936" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_33h_Male_Mech_Intro_01.prefab:0511c22148db56a448c2e55a8930a644", "VO_GILA_BOSS_33h_Male_Mech_EmoteResponse_01.prefab:e76887ef139b3454f952a8d666c627ef", "VO_GILA_BOSS_33h_Male_Mech_Death_01.prefab:88c829f9fdf7791458a9ff6540f55b8a", "VO_GILA_BOSS_33h_Male_Mech_HeroPower_01.prefab:82aaf3e22b386c14b97f9ce229d06df1", "VO_GILA_BOSS_33h_Male_Mech_HeroPower_02.prefab:f8a6c89b84d011044b93570ca4e706b7", "VO_GILA_BOSS_33h_Male_Mech_HeroPower_03.prefab:ea6cc1b345bc2b54196d189c4923d4eb", "VO_GILA_BOSS_33h_Male_Mech_HeroPower_04.prefab:72bfd202931ce5346bec2e2e18460aa8", "VO_GILA_BOSS_33h_Male_Mech_EventWeaponKill_01.prefab:37a31f0f76dfdf64ea7758fe65286dcf", "VO_GILA_BOSS_33h_Male_Mech_EventWeaponKill_02.prefab:f031f0ad30432914faebc7910e4b8dfc", "VO_GILA_BOSS_33h_Male_Mech_EventWeaponKill_03.prefab:088df2cc43594914f978f3cd6ffe4936",
			"VO_GILA_BOSS_33h_Male_Mech_Attack_01.prefab:004b23d48756f1c4faf510e064804b5a", "VO_GILA_BOSS_33h_Male_Mech_EventPlayHarvestGolem_01.prefab:2ffa7bb43bb5bc34ca7b76f0a8f05e8f"
		})
		{
			PreloadSound(item);
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { "VO_GILA_BOSS_33h_Male_Mech_HeroPower_01.prefab:82aaf3e22b386c14b97f9ce229d06df1", "VO_GILA_BOSS_33h_Male_Mech_HeroPower_02.prefab:f8a6c89b84d011044b93570ca4e706b7", "VO_GILA_BOSS_33h_Male_Mech_HeroPower_03.prefab:ea6cc1b345bc2b54196d189c4923d4eb", "VO_GILA_BOSS_33h_Male_Mech_HeroPower_04.prefab:72bfd202931ce5346bec2e2e18460aa8" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_33h_Male_Mech_Death_01.prefab:88c829f9fdf7791458a9ff6540f55b8a";
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_33h_Male_Mech_Intro_01.prefab:0511c22148db56a448c2e55a8930a644", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_33h_Male_Mech_EmoteResponse_01.prefab:e76887ef139b3454f952a8d666c627ef", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "EX1_556")
			{
				yield return PlayEasterEggLine(actor, "VO_GILA_BOSS_33h_Male_Mech_EventPlayHarvestGolem_01.prefab:2ffa7bb43bb5bc34ca7b76f0a8f05e8f");
			}
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
			yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_33h_Male_Mech_Attack_01.prefab:004b23d48756f1c4faf510e064804b5a");
			break;
		case 102:
		{
			string text = PopRandomLineWithChance(m_WeaponKillLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
			break;
		}
		}
	}
}
