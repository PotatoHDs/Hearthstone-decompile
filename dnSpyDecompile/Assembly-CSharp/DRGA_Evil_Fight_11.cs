using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D9 RID: 1241
public class DRGA_Evil_Fight_11 : DRGA_Dungeon
{
	// Token: 0x0600427F RID: 17023 RVA: 0x001650B4 File Offset: 0x001632B4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_Death_Reno_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_Death_Reno_Alt_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_01_Reno_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_02_Reno_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_03_Reno_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_04_Reno_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_05_Reno_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_BossAttack_Reno_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_BossStart_Reno_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_EmoteResponse_Reno_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Idle_01_Reno_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Idle_02_Reno_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Idle_03_Reno_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_01_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_02_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_03_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_04_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_05_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_06a_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_06b_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_01a_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_01b_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_02_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_03_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_04_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Victory_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_Anduin_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_Sylvanas_a_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_Sylvanas_b_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_Death_Anduin_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_01_Anduin_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_02_Anduin_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_03_Anduin_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_BossAttack_Anduin_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_BossStart_Anduin_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_EmoteResponse_Anduin_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Idle_01_Anduin_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Idle_02_Anduin_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_Death_Sylvanas_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_HeroPower_01_Sylvanas_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_HeroPower_02_Sylvanas_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_HeroPower_03_Sylvanas_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_BossAttack_Sylvanas_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_BossStart_Sylvanas_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_EmoteResponse_Sylvanas_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Idle_01_Sylvanas_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Idle_02_Sylvanas_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Idle_03_Sylvanas_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_01_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_02_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_03_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_04_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_05_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_06_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_Gala_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_NoGala_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_01_Galakrond_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_02_Galakrond_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_03_Galakrond_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_04_Galakrond_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_05_Galakrond_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_Misc_05_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_01_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_02_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_03_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_04_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Laugh_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_01_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_02_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_03_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01,
			DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStartHeroic_01,
			DRGA_Evil_Fight_11.VO_DRG_610_Male_Dragon_Play_01,
			DRGA_Evil_Fight_11.VO_DRG_600_Male_Dragon_Start_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004280 RID: 17024 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004281 RID: 17025 RVA: 0x001655E8 File Offset: 0x001637E8
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_Death_Sylvanas_01;
	}

	// Token: 0x06004282 RID: 17026 RVA: 0x00165600 File Offset: 0x00163800
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_BossStart_Reno_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			return;
		}
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		if (cardId == "DRGA_BOSS_01h4")
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_EmoteResponse_Reno_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (cardId == "DRGA_BOSS_37h")
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_EmoteResponse_Anduin_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (cardId == "DRGA_BOSS_38h")
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_EmoteResponse_Sylvanas_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_EmoteResponse_Reno_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
	}

	// Token: 0x06004283 RID: 17027 RVA: 0x00165793 File Offset: 0x00163993
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield break;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		string enemyHeroCardID = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		switch (missionEvent)
		{
		case 100:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_AnduinBossHeroPower);
			goto IL_D50;
		case 101:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_BossAttack_Anduin_01, 2.5f);
			goto IL_D50;
		case 102:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_BossStart_Anduin_01, 2.5f);
			goto IL_D50;
		case 103:
			yield return base.PlayLineInOrderOnce(friendlyActor, this.m_Galakrond_Awesome);
			goto IL_D50;
		case 104:
			yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01, 2.5f);
			goto IL_D50;
		case 105:
		case 108:
		case 111:
		case 117:
		case 118:
		case 119:
		case 120:
		case 121:
		case 122:
		case 123:
		case 124:
		case 125:
		case 127:
		case 128:
		case 129:
		case 130:
		case 131:
		case 135:
			break;
		case 106:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(friendlyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_D50;
		case 107:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_Death_Anduin_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_D50;
		case 109:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_Death_Reno_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_D50;
		case 110:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_BossAttack_Reno_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_D50;
		case 112:
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_Death_Sylvanas_01, 2.5f);
			goto IL_D50;
		case 113:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_BossAttack_Sylvanas_01, 2.5f);
			goto IL_D50;
		case 114:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_missionEventTriggerRenoHeroPower);
			goto IL_D50;
		case 115:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_missionEventTriggerAnduinHeroPower);
			goto IL_D50;
		case 116:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_missionEventTriggerSylvanasHeroPower);
			goto IL_D50;
		case 126:
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_Misc_05_01, 2.5f);
			goto IL_D50;
		case 132:
			if (this.m_Galakrond)
			{
				yield return base.PlayLineOnlyOnce(friendlyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_Gala_01, 2.5f);
			}
			if (!this.m_Galakrond)
			{
				yield return base.PlayLineOnlyOnce(friendlyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_NoGala_01, 2.5f);
				goto IL_D50;
			}
			goto IL_D50;
		case 133:
			if (this.m_Galakrond)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_VO_BossHasGalakrond);
				goto IL_D50;
			}
			goto IL_D50;
		case 134:
			yield return base.PlayLineInOrderOnce(friendlyActor, this.m_InvokeGalakrond);
			goto IL_D50;
		case 136:
			yield return base.PlayLineInOrderOnce(friendlyActor, this.m_VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_ExpositionLines);
			goto IL_D50;
		case 137:
			yield return base.PlayAndRemoveRandomLineOnlyOnceWithBrassRing(base.GetFriendlyActorByCardId("DRGA_005"), null, this.m_Galakrond_Devastation);
			goto IL_D50;
		case 138:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_BossStart_Anduin_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_D50;
		case 139:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_BossStart_Sylvanas_01, 2.5f);
			GameState.Get().SetBusy(false);
			yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_Sylvanas_a_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_Sylvanas_b_01, 2.5f);
			goto IL_D50;
		case 140:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_D50;
		case 141:
			this.m_Galakrond = true;
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("DRGA_005"), null, DRGA_Evil_Fight_11.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01, 2.5f);
			if (enemyHeroCardID == "DRGA_BOSS_01h4")
			{
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01, 2.5f);
				goto IL_D50;
			}
			goto IL_D50;
		case 142:
			GameState.Get().SetBusy(true);
			this.m_Galakrond = true;
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("DRGA_005"), null, DRGA_Evil_Fight_11.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_D50;
		case 143:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_Anduin_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_D50;
		case 144:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("DRGA_005"), null, DRGA_Evil_Fight_11.VO_DRG_610_Male_Dragon_Play_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_Death_Reno_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_D50;
		case 145:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("DRGA_005"), null, DRGA_Evil_Fight_11.VO_DRG_600_Male_Dragon_Start_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_Death_Anduin_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_D50;
		default:
			if (missionEvent == 199)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_RenoHeroPower);
				goto IL_D50;
			}
			if (missionEvent == 250)
			{
				int num = base.GetTag(GAME_TAG.TURN) - GameState.Get().GetGameEntity().GetTag(GAME_TAG.EXTRA_TURNS_TAKEN_THIS_GAME);
				if (this.m_TurnOfGalakrondLine >= num)
				{
					goto IL_D50;
				}
				this.m_TurnOfGalakrondLine = num;
				if (this.m_GalakrondFirstAttack)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_02_Galakrond_01, 2.5f);
					this.m_GalakrondFirstAttack = false;
					GameState.Get().SetBusy(false);
					goto IL_D50;
				}
				if (this.m_GalakrondAnduinFirstAttack && enemyHeroCardID == "DRGA_BOSS_37h")
				{
					yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_06a_01, 2.5f);
					yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("DRGA_005"), null, DRGA_Evil_Fight_11.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Laugh_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_06b_01, 2.5f);
					this.m_GalakrondAnduinFirstAttack = false;
					goto IL_D50;
				}
				if (enemyHeroCardID == "DRGA_BOSS_37h" || enemyHeroCardID == "DRGA_BOSS_01h4")
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_Galakrond_Awesome);
					goto IL_D50;
				}
				if (!(enemyHeroCardID == "DRGA_BOSS_38h"))
				{
					goto IL_D50;
				}
				if (this.m_GalakrondLategameFirstAttack)
				{
					yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_01a_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_01b_01, 2.5f);
					this.m_GalakrondLategameFirstAttack = false;
					goto IL_D50;
				}
				yield return base.PlayLineInOrderOnce(friendlyActor, this.m_Galakrond_Lategame);
				goto IL_D50;
			}
			break;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_D50:
		yield break;
	}

	// Token: 0x06004284 RID: 17028 RVA: 0x001657A9 File Offset: 0x001639A9
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		yield break;
	}

	// Token: 0x06004285 RID: 17029 RVA: 0x001657BF File Offset: 0x001639BF
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		yield break;
	}

	// Token: 0x06004286 RID: 17030 RVA: 0x001657D8 File Offset: 0x001639D8
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		float thinkEmoteBossThinkChancePercentage = this.GetThinkEmoteBossThinkChancePercentage();
		float num = UnityEngine.Random.Range(0f, 1f);
		if (thinkEmoteBossThinkChancePercentage > num)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
			if (cardId == "DRGA_BOSS_01h4")
			{
				string line = base.PopRandomLine(this.m_RenoIdleLinesCopy);
				if (this.m_RenoIdleLinesCopy.Count == 0)
				{
					this.m_RenoIdleLinesCopy = new List<string>(DRGA_Evil_Fight_11.m_RenoIdleLines);
				}
				Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
				return;
			}
			if (cardId == "DRGA_BOSS_37h")
			{
				string line2 = base.PopRandomLine(this.m_AnduinIdleLinesCopy);
				if (this.m_AnduinIdleLinesCopy.Count == 0)
				{
					this.m_AnduinIdleLinesCopy = new List<string>(DRGA_Evil_Fight_11.m_AnduinIdleLines);
				}
				Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line2, 2.5f));
				return;
			}
			if (cardId == "DRGA_BOSS_38h")
			{
				string line3 = base.PopRandomLine(this.m_SylvanasIdleLinesCopy);
				if (this.m_SylvanasIdleLinesCopy.Count == 0)
				{
					this.m_SylvanasIdleLinesCopy = new List<string>(DRGA_Evil_Fight_11.m_SylvanasIdleLines);
				}
				Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line3, 2.5f));
				return;
			}
		}
		else
		{
			EmoteType emoteType = EmoteType.THINK1;
			switch (UnityEngine.Random.Range(1, 4))
			{
			case 1:
				emoteType = EmoteType.THINK1;
				break;
			case 2:
				emoteType = EmoteType.THINK2;
				break;
			case 3:
				emoteType = EmoteType.THINK3;
				break;
			}
			GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType);
		}
	}

	// Token: 0x04003274 RID: 12916
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_Death_Reno_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_Death_Reno_01.prefab:77f38ef3df3289f408454040b4ecae62");

	// Token: 0x04003275 RID: 12917
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_Death_Reno_Alt_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_Death_Reno_Alt_01.prefab:19f80b1782bc2824bbe12d1689f405d5");

	// Token: 0x04003276 RID: 12918
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_01_Reno_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_01_Reno_01.prefab:e5c37c5b5fff8b548ac5b9f14914efa6");

	// Token: 0x04003277 RID: 12919
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_02_Reno_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_02_Reno_01.prefab:4a98f209662f2e94398e72af80852789");

	// Token: 0x04003278 RID: 12920
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_03_Reno_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_03_Reno_01.prefab:895da455c05a5be4888b93d183468ebd");

	// Token: 0x04003279 RID: 12921
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_04_Reno_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_04_Reno_01.prefab:d89aa8ac4f866ef40b06a949830cfff3");

	// Token: 0x0400327A RID: 12922
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_05_Reno_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_05_Reno_01.prefab:3c617a2310ed4af4dab5d45730d9d8d2");

	// Token: 0x0400327B RID: 12923
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_BossAttack_Reno_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_BossAttack_Reno_01.prefab:321e819cd8b3b9541910c49db7f31d5a");

	// Token: 0x0400327C RID: 12924
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_BossStart_Reno_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_BossStart_Reno_01.prefab:7f767113c34f56f439cd4006881a9480");

	// Token: 0x0400327D RID: 12925
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_EmoteResponse_Reno_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_EmoteResponse_Reno_01.prefab:af0b69c14b688fb45b62cbf43cd2e3f8");

	// Token: 0x0400327E RID: 12926
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Idle_01_Reno_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Idle_01_Reno_01.prefab:eeac0c3fb63d72747ab45ff2415badbe");

	// Token: 0x0400327F RID: 12927
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Idle_02_Reno_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Idle_02_Reno_01.prefab:61d8549a5c5144e4dbe2c21de591bb74");

	// Token: 0x04003280 RID: 12928
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Idle_03_Reno_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Idle_03_Reno_01.prefab:983f39cbca91e5e49a53f520663b68e1");

	// Token: 0x04003281 RID: 12929
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_01_01.prefab:5bf1baf4a4278384b9b548cf8451f9ab");

	// Token: 0x04003282 RID: 12930
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_02_01.prefab:f5582d9e476adc543a16ef1d59fcfb1a");

	// Token: 0x04003283 RID: 12931
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_03_01.prefab:b63c1dfc1cdd7d941b698f3f728bed91");

	// Token: 0x04003284 RID: 12932
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_04_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_04_01.prefab:61ede9459db3a4741955c11c4b4f1377");

	// Token: 0x04003285 RID: 12933
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_05_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_05_01.prefab:0c7cc25ad2bfbb84da564b8939916340");

	// Token: 0x04003286 RID: 12934
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_06a_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_06a_01.prefab:f3fceb65864d6344bab7a9092c457812");

	// Token: 0x04003287 RID: 12935
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_06b_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_06b_01.prefab:d4d657e9db014314caab2f40636ff0b4");

	// Token: 0x04003288 RID: 12936
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01.prefab:97d4176bf290fb64a9f7aba668c6d013");

	// Token: 0x04003289 RID: 12937
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_01a_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_01a_01.prefab:86b37070de8df5342b1ad83c47667576");

	// Token: 0x0400328A RID: 12938
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_01b_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_01b_01.prefab:23922063ad1675c41bfe9e6f3b6f6859");

	// Token: 0x0400328B RID: 12939
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_02_01.prefab:72eae8a8b874bd14d81b3627af18c847");

	// Token: 0x0400328C RID: 12940
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_03_01.prefab:22aa8be74f27779419b2335966330876");

	// Token: 0x0400328D RID: 12941
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_04_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_04_01.prefab:6c217ab12ba5dd942a3e92b7794660e2");

	// Token: 0x0400328E RID: 12942
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Victory_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Victory_01.prefab:11c0fff12681abf4cbb2163e95ea19f1");

	// Token: 0x0400328F RID: 12943
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_01.prefab:c46f24e0b34808142807111a15344873");

	// Token: 0x04003290 RID: 12944
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_Anduin_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_Anduin_01.prefab:9d2bb802ee0746f408d562ae7e13f46a");

	// Token: 0x04003291 RID: 12945
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_Sylvanas_a_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_Sylvanas_a_01.prefab:be943a6d259b41d4884b2dc351e051ad");

	// Token: 0x04003292 RID: 12946
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_Sylvanas_b_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_PlayerStart_Sylvanas_b_01.prefab:d85c0941095876b409f50a479ee61c91");

	// Token: 0x04003293 RID: 12947
	private static readonly AssetReference VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_Death_Anduin_01 = new AssetReference("VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_Death_Anduin_01.prefab:d12f0c859628d464ea2d58a8a4576dac");

	// Token: 0x04003294 RID: 12948
	private static readonly AssetReference VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_01_Anduin_01 = new AssetReference("VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_01_Anduin_01.prefab:1f3e63cf82062164fa69b4dc1e5f29bd");

	// Token: 0x04003295 RID: 12949
	private static readonly AssetReference VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_02_Anduin_01 = new AssetReference("VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_02_Anduin_01.prefab:e98a1c3e21706ab46ab16ea5a2952164");

	// Token: 0x04003296 RID: 12950
	private static readonly AssetReference VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_03_Anduin_01 = new AssetReference("VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_03_Anduin_01.prefab:63ada6ddec7cb3f48b2d28fa19cd97e9");

	// Token: 0x04003297 RID: 12951
	private static readonly AssetReference VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_BossAttack_Anduin_01 = new AssetReference("VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_BossAttack_Anduin_01.prefab:de8612b7bf980be4490b519a6e359eae");

	// Token: 0x04003298 RID: 12952
	private static readonly AssetReference VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_BossStart_Anduin_01 = new AssetReference("VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_BossStart_Anduin_01.prefab:cf2f6ca5ebc64044daae9078d6ad5f25");

	// Token: 0x04003299 RID: 12953
	private static readonly AssetReference VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_EmoteResponse_Anduin_01 = new AssetReference("VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_EmoteResponse_Anduin_01.prefab:8cfbed66c07551a429ad4fd6e5a37e5a");

	// Token: 0x0400329A RID: 12954
	private static readonly AssetReference VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Idle_01_Anduin_01 = new AssetReference("VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Idle_01_Anduin_01.prefab:102e893c5422414409995141dc5176b9");

	// Token: 0x0400329B RID: 12955
	private static readonly AssetReference VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Idle_02_Anduin_01 = new AssetReference("VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Idle_02_Anduin_01.prefab:e67929bb380e8c34ba3be8657580ffbd");

	// Token: 0x0400329C RID: 12956
	private static readonly AssetReference VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_Death_Sylvanas_01 = new AssetReference("VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_Death_Sylvanas_01.prefab:920f5f25f7526b64ebc1874ce03a069c");

	// Token: 0x0400329D RID: 12957
	private static readonly AssetReference VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_HeroPower_01_Sylvanas_01 = new AssetReference("VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_HeroPower_01_Sylvanas_01.prefab:66770ca958cfb614e9a0928ccbf67f7e");

	// Token: 0x0400329E RID: 12958
	private static readonly AssetReference VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_HeroPower_02_Sylvanas_01 = new AssetReference("VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_HeroPower_02_Sylvanas_01.prefab:49bdc2d40ab6e2848b8c2cd454d9af84");

	// Token: 0x0400329F RID: 12959
	private static readonly AssetReference VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_HeroPower_03_Sylvanas_01 = new AssetReference("VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_HeroPower_03_Sylvanas_01.prefab:eae26bd4bab21fe4084c86deb1dab642");

	// Token: 0x040032A0 RID: 12960
	private static readonly AssetReference VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_BossAttack_Sylvanas_01 = new AssetReference("VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_BossAttack_Sylvanas_01.prefab:64fcde735b952f8468eb1ba72cc7d8ba");

	// Token: 0x040032A1 RID: 12961
	private static readonly AssetReference VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_BossStart_Sylvanas_01 = new AssetReference("VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_BossStart_Sylvanas_01.prefab:a3f5e8bdcf9f74f4f88117fd307e93a6");

	// Token: 0x040032A2 RID: 12962
	private static readonly AssetReference VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_EmoteResponse_Sylvanas_01 = new AssetReference("VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_EmoteResponse_Sylvanas_01.prefab:778129426d2e1ef43b9767467af3e3f4");

	// Token: 0x040032A3 RID: 12963
	private static readonly AssetReference VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Idle_01_Sylvanas_01 = new AssetReference("VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Idle_01_Sylvanas_01.prefab:2302a2610659ed94f92e2d046280fc72");

	// Token: 0x040032A4 RID: 12964
	private static readonly AssetReference VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Idle_02_Sylvanas_01 = new AssetReference("VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Idle_02_Sylvanas_01.prefab:6cd88de1fd763ae4981a59e5c0dba454");

	// Token: 0x040032A5 RID: 12965
	private static readonly AssetReference VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Idle_03_Sylvanas_01 = new AssetReference("VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Idle_03_Sylvanas_01.prefab:19c19254823851c45a78e9c91aa79c71");

	// Token: 0x040032A6 RID: 12966
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_01_01.prefab:eb9c22f23e619604b9de2e8cbf35486f");

	// Token: 0x040032A7 RID: 12967
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_02_01.prefab:f13d2d8751b90254bab7a94dd70d67db");

	// Token: 0x040032A8 RID: 12968
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_03_01.prefab:f9b97fd400ffca44db629c80efcb3895");

	// Token: 0x040032A9 RID: 12969
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_04_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_04_01.prefab:32a4b7c99f3aae145b2a414c984f355b");

	// Token: 0x040032AA RID: 12970
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_05_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_05_01.prefab:3b0a5793ab112f54599e953587948acf");

	// Token: 0x040032AB RID: 12971
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_06_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_06_01.prefab:ca6c35bf0c1d5d140aafc25c11c78cc7");

	// Token: 0x040032AC RID: 12972
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_Gala_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_Gala_01.prefab:861be9550341a2b47b1383d91e916507");

	// Token: 0x040032AD RID: 12973
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_NoGala_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_NoGala_01.prefab:4500792ba86ca0143b8d0040571c2d35");

	// Token: 0x040032AE RID: 12974
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_01_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_01_Galakrond_01.prefab:fef0bd9eb9481db43bc33e8639f61bc6");

	// Token: 0x040032AF RID: 12975
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_02_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_02_Galakrond_01.prefab:ce67c7d371bd22d4b9e346aa1583df7f");

	// Token: 0x040032B0 RID: 12976
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_03_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_03_Galakrond_01.prefab:7ac14feda2477984aa143219f855979f");

	// Token: 0x040032B1 RID: 12977
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_04_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_04_Galakrond_01.prefab:194b03d8ed0c58f4b9c685c397dc6965");

	// Token: 0x040032B2 RID: 12978
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_05_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_05_Galakrond_01.prefab:b3b2591977dbf4f47a76584b055f4869");

	// Token: 0x040032B3 RID: 12979
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_Misc_05_01.prefab:9d551762e7ecc1845842217663425566");

	// Token: 0x040032B4 RID: 12980
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01.prefab:c1442324e17668d4abfbd46d6eed287a");

	// Token: 0x040032B5 RID: 12981
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_01_01.prefab:42615239b9fb58b4c8b518c764b63137");

	// Token: 0x040032B6 RID: 12982
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_02_01.prefab:d13f509fccbb2474bbf7bc5a276d7a4d");

	// Token: 0x040032B7 RID: 12983
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_03_01.prefab:a998f80b9b268df4fb86df39a37f992e");

	// Token: 0x040032B8 RID: 12984
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_04_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_04_01.prefab:a998749fb50c6264a9f178edb21d5b7f");

	// Token: 0x040032B9 RID: 12985
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Laugh_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Laugh_01.prefab:09c06754b47077445a8b9dd2c0d9b83a");

	// Token: 0x040032BA RID: 12986
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_01_01.prefab:a60ff4ac653d29d4aaa283131c7424e3");

	// Token: 0x040032BB RID: 12987
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_02_01.prefab:76009e7b08ac2414a885b15b03901408");

	// Token: 0x040032BC RID: 12988
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_03_01.prefab:c107dd155cf1fb2409f9bc203d836dd7");

	// Token: 0x040032BD RID: 12989
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01.prefab:fbfe70bab9271e746adc673ebe4e8ab4");

	// Token: 0x040032BE RID: 12990
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStartHeroic_01.prefab:659d57c7389ab52448ac1a90cb4b47a6");

	// Token: 0x040032BF RID: 12991
	private static readonly AssetReference VO_DRG_610_Male_Dragon_Play_01 = new AssetReference("VO_DRG_610_Male_Dragon_Play_01.prefab:c81c587900090674aa26f25a03fb3479");

	// Token: 0x040032C0 RID: 12992
	private static readonly AssetReference VO_DRG_600_Male_Dragon_Start_01 = new AssetReference("VO_DRG_600_Male_Dragon_Start_01.prefab:d3bc566b53d56454097f27ca04ce28f4");

	// Token: 0x040032C1 RID: 12993
	private List<string> m_AnduinBossHeroPower = new List<string>
	{
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_01_Anduin_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_02_Anduin_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_03_Anduin_01
	};

	// Token: 0x040032C2 RID: 12994
	private List<string> m_Galakrond_Awesome = new List<string>
	{
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_01_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_02_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_03_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_04_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Awesome_05_01
	};

	// Token: 0x040032C3 RID: 12995
	private List<string> m_Galakrond_Devastation = new List<string>
	{
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_01_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_02_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_03_01
	};

	// Token: 0x040032C4 RID: 12996
	private List<string> m_Galakrond_Lategame = new List<string>
	{
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_02_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_03_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_LateGame_04_01
	};

	// Token: 0x040032C5 RID: 12997
	private List<string> m_missionEventTriggerRenoHeroPower = new List<string>
	{
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_01_Reno_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_02_Reno_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_03_Reno_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_04_Reno_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_05_Reno_01
	};

	// Token: 0x040032C6 RID: 12998
	private List<string> m_missionEventTriggerAnduinHeroPower = new List<string>
	{
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_01_Anduin_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_02_Anduin_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Boss_HeroPower_03_Anduin_01
	};

	// Token: 0x040032C7 RID: 12999
	private List<string> m_missionEventTriggerSylvanasHeroPower = new List<string>
	{
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_HeroPower_01_Sylvanas_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_HeroPower_02_Sylvanas_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Boss_HeroPower_03_Sylvanas_01
	};

	// Token: 0x040032C8 RID: 13000
	private List<string> m_InvokeGalakrond = new List<string>
	{
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_01_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_02_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_03_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_04_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_05_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_06_01
	};

	// Token: 0x040032C9 RID: 13001
	private static List<string> m_AnduinIdleLines = new List<string>
	{
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Idle_01_Anduin_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_37h_Male_Human_Evil_Fight_11_Idle_02_Anduin_01
	};

	// Token: 0x040032CA RID: 13002
	private List<string> m_AnduinIdleLinesCopy = new List<string>(DRGA_Evil_Fight_11.m_AnduinIdleLines);

	// Token: 0x040032CB RID: 13003
	private static List<string> m_SylvanasIdleLines = new List<string>
	{
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Idle_01_Sylvanas_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Idle_02_Sylvanas_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_38h_Female_Forsaken_Evil_Fight_11_Idle_03_Sylvanas_01
	};

	// Token: 0x040032CC RID: 13004
	private List<string> m_SylvanasIdleLinesCopy = new List<string>(DRGA_Evil_Fight_11.m_SylvanasIdleLines);

	// Token: 0x040032CD RID: 13005
	private static List<string> m_RenoIdleLines = new List<string>
	{
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Idle_01_Reno_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Idle_02_Reno_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Idle_03_Reno_01
	};

	// Token: 0x040032CE RID: 13006
	private List<string> m_RenoIdleLinesCopy = new List<string>(DRGA_Evil_Fight_11.m_RenoIdleLines);

	// Token: 0x040032CF RID: 13007
	private List<string> m_VO_BossHasGalakrond = new List<string>
	{
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_01_Galakrond_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_02_Galakrond_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_03_Galakrond_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_04_Galakrond_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_05_Galakrond_01
	};

	// Token: 0x040032D0 RID: 13008
	private List<string> m_VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_ExpositionLines = new List<string>
	{
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_01_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_02_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_03_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_04_01
	};

	// Token: 0x040032D1 RID: 13009
	private List<string> m_RenoHeroPower = new List<string>
	{
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_01_Reno_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_04_Reno_01,
		DRGA_Evil_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_11_Boss_HeroPower_05_Reno_01
	};

	// Token: 0x040032D2 RID: 13010
	public int m_TurnOfGalakrondLine;

	// Token: 0x040032D3 RID: 13011
	public bool m_GalakrondFirstAttack = true;

	// Token: 0x040032D4 RID: 13012
	public bool m_GalakrondAnduinFirstAttack = true;

	// Token: 0x040032D5 RID: 13013
	public bool m_GalakrondLategameFirstAttack = true;

	// Token: 0x040032D6 RID: 13014
	private HashSet<string> m_playedLines = new HashSet<string>();
}
