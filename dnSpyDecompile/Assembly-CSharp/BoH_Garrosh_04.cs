using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200051B RID: 1307
public class BoH_Garrosh_04 : BoH_Garrosh_Dungeon
{
	// Token: 0x060046D5 RID: 18133 RVA: 0x0017D8A0 File Offset: 0x0017BAA0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Garrosh_04.VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4Death_01,
			BoH_Garrosh_04.VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4EmoteResponse_01,
			BoH_Garrosh_04.VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_01,
			BoH_Garrosh_04.VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_02,
			BoH_Garrosh_04.VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_03,
			BoH_Garrosh_04.VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4Loss_01,
			BoH_Garrosh_04.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeA_01,
			BoH_Garrosh_04.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeB_01,
			BoH_Garrosh_04.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeC_01,
			BoH_Garrosh_04.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Intro_01,
			BoH_Garrosh_04.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Victory_01,
			BoH_Garrosh_04.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4ExchangeC_01,
			BoH_Garrosh_04.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_01,
			BoH_Garrosh_04.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_02,
			BoH_Garrosh_04.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_03
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060046D6 RID: 18134 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060046D7 RID: 18135 RVA: 0x0017D9F4 File Offset: 0x0017BBF4
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Garrosh_04.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060046D8 RID: 18136 RVA: 0x0017DA03 File Offset: 0x0017BC03
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPowerLines;
	}

	// Token: 0x060046D9 RID: 18137 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060046DA RID: 18138 RVA: 0x0017DA0B File Offset: 0x0017BC0B
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Garrosh_04.VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4EmoteResponse_01;
	}

	// Token: 0x060046DB RID: 18139 RVA: 0x0017DA24 File Offset: 0x0017BC24
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060046DC RID: 18140 RVA: 0x0017DAA8 File Offset: 0x0017BCA8
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 501)
		{
			if (missionEvent != 504)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Garrosh_04.VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(BoH_Garrosh_04.ThrallBrassRing, BoH_Garrosh_04.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Garrosh_04.ThrallBrassRing, BoH_Garrosh_04.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_02, 2.5f);
			yield return base.PlayLineAlways(BoH_Garrosh_04.ThrallBrassRing, BoH_Garrosh_04.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_03, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_04.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x060046DD RID: 18141 RVA: 0x0017DABE File Offset: 0x0017BCBE
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x060046DE RID: 18142 RVA: 0x0017DAD4 File Offset: 0x0017BCD4
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x060046DF RID: 18143 RVA: 0x0017DAEA File Offset: 0x0017BCEA
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn != 3)
			{
				if (turn == 7)
				{
					yield return base.PlayLineAlways(actor, BoH_Garrosh_04.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(BoH_Garrosh_04.ThrallBrassRing, BoH_Garrosh_04.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4ExchangeC_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BoH_Garrosh_04.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Garrosh_04.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060046E0 RID: 18144 RVA: 0x0017D1BC File Offset: 0x0017B3BC
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BRMAdventure);
	}

	// Token: 0x04003A41 RID: 14913
	private static readonly AssetReference VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4Death_01 = new AssetReference("VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4Death_01.prefab:bfa5b4579b1945b1a0beba43c0bcb993");

	// Token: 0x04003A42 RID: 14914
	private static readonly AssetReference VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4EmoteResponse_01 = new AssetReference("VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4EmoteResponse_01.prefab:66222a97e5eb40f4a5d5a700da9d71d6");

	// Token: 0x04003A43 RID: 14915
	private static readonly AssetReference VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_01 = new AssetReference("VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_01.prefab:ed3c00e07654463286fa69563f566695");

	// Token: 0x04003A44 RID: 14916
	private static readonly AssetReference VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_02 = new AssetReference("VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_02.prefab:4f3a87b7dd6d43178a416c61eea55821");

	// Token: 0x04003A45 RID: 14917
	private static readonly AssetReference VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_03 = new AssetReference("VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_03.prefab:87afd0aa3ed94c9abf14a340502b2690");

	// Token: 0x04003A46 RID: 14918
	private static readonly AssetReference VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4Loss_01 = new AssetReference("VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4Loss_01.prefab:1de1f7f8705644c499d73059cf8d6e6c");

	// Token: 0x04003A47 RID: 14919
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeA_01.prefab:2c7ead4c7d2299f4fa0d9777633a973c");

	// Token: 0x04003A48 RID: 14920
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeB_01.prefab:012246e019320354288264519deffaf2");

	// Token: 0x04003A49 RID: 14921
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4ExchangeC_01.prefab:8f17be315bfaae7429fe82fcf776ea7a");

	// Token: 0x04003A4A RID: 14922
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Intro_01.prefab:e784b22048a1082498984a2ed37ddb42");

	// Token: 0x04003A4B RID: 14923
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission4Victory_01.prefab:3119bee73eaea8b4b9efa03b5ce1b8c8");

	// Token: 0x04003A4C RID: 14924
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4ExchangeC_01.prefab:5eef752e55d6aea4080aeac7e0aeb67c");

	// Token: 0x04003A4D RID: 14925
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_01.prefab:cf21762ad6240494c8097a3c816d661f");

	// Token: 0x04003A4E RID: 14926
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_02.prefab:0ed5fe130a8c147408b9cad50624ee34");

	// Token: 0x04003A4F RID: 14927
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission4Victory_03.prefab:cb890dc97b3acd743b7c6cc4db678a6b");

	// Token: 0x04003A50 RID: 14928
	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	// Token: 0x04003A51 RID: 14929
	private List<string> m_VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPowerLines = new List<string>
	{
		BoH_Garrosh_04.VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_01,
		BoH_Garrosh_04.VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_02,
		BoH_Garrosh_04.VO_Story_Hero_FireElemental_Male_Elemental_Story_Garrosh_Mission4HeroPower_03
	};

	// Token: 0x04003A52 RID: 14930
	private HashSet<string> m_playedLines = new HashSet<string>();
}
