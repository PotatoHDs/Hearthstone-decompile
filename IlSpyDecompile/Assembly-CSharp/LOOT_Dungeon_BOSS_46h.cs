using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_46h : LOOT_Dungeon
{
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_46h_Male_Murloc_Intro_01.prefab:71c2c439e7f1f3e42be72c6b02d4a013", "VO_LOOTA_BOSS_46h_Male_Murloc_EmoteResponse_01.prefab:00a42aa4cdea4954f968e90a4c2f2758", "VO_LOOTA_BOSS_46h_Male_Murloc_HeroPower1_01.prefab:1d626b91bc1b5e7498592d47eb2a66ed", "VO_LOOTA_BOSS_46h_Male_Murloc_HeroPower2_01.prefab:dfb57d97d355c9242945f43abc4ce81d", "VO_LOOTA_BOSS_46h_Male_Murloc_HeroPower3_01.prefab:b1e1098bc4695444bbfabf587395db28", "VO_LOOTA_BOSS_46h_Male_Murloc_Death_01.prefab:3cc732e6f7fd04b459e5dece964b7eba", "VO_LOOTA_BOSS_46h_Male_Murloc_DefeatPlayer_01.prefab:e24d2745d8495864a83101211a805f6b" })
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
		return new List<string> { "VO_LOOTA_BOSS_46h_Male_Murloc_HeroPower1_01.prefab:1d626b91bc1b5e7498592d47eb2a66ed", "VO_LOOTA_BOSS_46h_Male_Murloc_HeroPower2_01.prefab:dfb57d97d355c9242945f43abc4ce81d", "VO_LOOTA_BOSS_46h_Male_Murloc_HeroPower3_01.prefab:b1e1098bc4695444bbfabf587395db28" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_46h_Male_Murloc_Death_01.prefab:3cc732e6f7fd04b459e5dece964b7eba";
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_46h_Male_Murloc_Intro_01.prefab:71c2c439e7f1f3e42be72c6b02d4a013", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_46h_Male_Murloc_EmoteResponse_01.prefab:00a42aa4cdea4954f968e90a4c2f2758", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		yield return PlayLoyalSideKickBetrayal(missionEvent);
	}
}
