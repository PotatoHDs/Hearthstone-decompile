using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000593 RID: 1427
public class RP_Dungeon : RP_MissionEntity
{
	// Token: 0x06004F35 RID: 20277 RVA: 0x001A0560 File Offset: 0x0019E760
	public static RP_Dungeon InstantiateRPDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		switch (GameMgr.Get().GetMissionId())
		{
		case 3540:
			return new RP_Fight_01();
		case 3541:
			return new RP_Fight_02();
		case 3543:
			return new RP_Fight_03();
		}
		Log.All.PrintError("DRGA_Dungeon.InstantiateDRGADungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new RP_Dungeon();
	}

	// Token: 0x06004F36 RID: 20278 RVA: 0x001A05D0 File Offset: 0x0019E7D0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>();
		this.m_PlayerVOLines = new List<string>(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004F37 RID: 20279 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x06004F38 RID: 20280 RVA: 0x001A0638 File Offset: 0x0019E838
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x06004F39 RID: 20281 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06004F3A RID: 20282 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x06004F3B RID: 20283 RVA: 0x001A0648 File Offset: 0x0019E848
	protected virtual void OnBossHeroPowerPlayed(Entity entity)
	{
		float num = this.ChanceToPlayBossHeroPowerVOLine();
		float num2 = UnityEngine.Random.Range(0f, 1f);
		if (this.m_enemySpeaking)
		{
			return;
		}
		if (num < num2)
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (actor == null)
		{
			return;
		}
		List<string> bossHeroPowerRandomLines = this.GetBossHeroPowerRandomLines();
		string text = "";
		while (bossHeroPowerRandomLines.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, bossHeroPowerRandomLines.Count);
			text = bossHeroPowerRandomLines[index];
			bossHeroPowerRandomLines.RemoveAt(index);
			if (!NotificationManager.Get().HasSoundPlayedThisSession(text))
			{
				break;
			}
		}
		if (text == "")
		{
			return;
		}
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce(text, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
	}

	// Token: 0x06004F3C RID: 20284 RVA: 0x001A071C File Offset: 0x0019E91C
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		if (this.m_enemySpeaking)
		{
			yield break;
		}
		if (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield break;
		}
		if (entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		if (entity.GetControllerSide() == Player.Side.OPPOSING)
		{
			this.OnBossHeroPowerPlayed(entity);
		}
		yield break;
	}

	// Token: 0x06004F3D RID: 20285 RVA: 0x001A0732 File Offset: 0x0019E932
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		yield break;
	}

	// Token: 0x06004F3E RID: 20286 RVA: 0x001A0748 File Offset: 0x0019E948
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (!this.m_enemySpeaking && !string.IsNullOrEmpty(this.m_deathLine) && gameResult == TAG_PLAYSTATE.WON)
		{
			if (this.GetShouldSuppressDeathTextBubble())
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_deathLine, Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_deathLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
	}

	// Token: 0x06004F3F RID: 20287 RVA: 0x001A07E8 File Offset: 0x0019E9E8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004F40 RID: 20288 RVA: 0x001A0871 File Offset: 0x0019EA71
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = null;
		this.m_deathLine = null;
		this.m_standardEmoteResponseLine = null;
		this.m_BossIdleLines = new List<string>(this.GetIdleLines());
		this.m_BossIdleLinesCopy = new List<string>(this.GetIdleLines());
	}

	// Token: 0x06004F41 RID: 20289 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06004F42 RID: 20290 RVA: 0x001A08B0 File Offset: 0x0019EAB0
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		switch (missionEvent)
		{
		case 1000:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			if (this.m_PlayPlayerVOLineIndex + 1 >= this.m_PlayerVOLines.Count)
			{
				this.m_PlayPlayerVOLineIndex = 0;
			}
			else
			{
				this.m_PlayPlayerVOLineIndex++;
			}
			SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
			yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
			break;
		case 1001:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
			yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
			break;
		case 1002:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			if (this.m_PlayBossVOLineIndex + 1 >= this.m_BossVOLines.Count)
			{
				this.m_PlayBossVOLineIndex = 0;
			}
			else
			{
				this.m_PlayBossVOLineIndex++;
			}
			SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
			yield return base.PlayBossLine(actor2, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
			break;
		case 1003:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
			yield return base.PlayBossLine(actor2, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
			break;
		default:
			if (missionEvent != 1010)
			{
				if (missionEvent != 58023)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
					GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
					SceneMgr.Get().SetNextMode(postGameSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
				}
			}
			else if (this.m_forceAlwaysPlayLine)
			{
				this.m_forceAlwaysPlayLine = false;
			}
			else
			{
				this.m_forceAlwaysPlayLine = true;
			}
			break;
		}
		yield break;
	}

	// Token: 0x06004F43 RID: 20291 RVA: 0x00160DDC File Offset: 0x0015EFDC
	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	// Token: 0x06004F44 RID: 20292 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossThinkChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004F45 RID: 20293 RVA: 0x001A08C8 File Offset: 0x0019EAC8
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
		float thinkEmoteBossThinkChancePercentage = this.GetThinkEmoteBossThinkChancePercentage();
		float num = UnityEngine.Random.Range(0f, 1f);
		if (thinkEmoteBossThinkChancePercentage > num && this.m_BossIdleLines != null && this.m_BossIdleLines.Count != 0)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
			string line = base.PopRandomLine(this.m_BossIdleLinesCopy);
			if (this.m_BossIdleLinesCopy.Count == 0)
			{
				this.m_BossIdleLinesCopy = new List<string>(this.GetIdleLines());
			}
			Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
			return;
		}
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

	// Token: 0x06004F46 RID: 20294 RVA: 0x001A09D2 File Offset: 0x0019EBD2
	public IEnumerator PlayAndRemoveRandomLineOnlyOnceWithBrassRing(Actor actor, AssetReference brassRingBackup, List<string> lines)
	{
		if (actor != null)
		{
			yield return this.PlayAndRemoveRandomLineOnlyOnce(actor, lines);
		}
		else if (brassRingBackup != null)
		{
			yield return this.PlayAndRemoveRandomLineOnlyOnce(brassRingBackup, lines);
		}
		yield break;
	}

	// Token: 0x06004F47 RID: 20295 RVA: 0x001A09F6 File Offset: 0x0019EBF6
	protected IEnumerator PlayLineAlwaysWithBrassRing(Actor actor, AssetReference brassRingBackup, string line, float duration = 2.5f)
	{
		if (actor != null)
		{
			yield return base.PlayLineAlways(actor, line, 2.5f);
		}
		else if (brassRingBackup != null)
		{
			yield return base.PlayLineAlways(brassRingBackup, line, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004F48 RID: 20296 RVA: 0x001A0A1A File Offset: 0x0019EC1A
	public IEnumerator PlayLineInOrderOnceWithBrassRing(Actor actor, AssetReference brassRingBackup, List<string> lines)
	{
		if (actor != null)
		{
			yield return base.PlayLineInOrderOnce(actor, lines);
		}
		else if (brassRingBackup != null)
		{
			yield return base.PlayLineInOrderOnce(brassRingBackup, lines);
		}
		yield break;
	}

	// Token: 0x06004F49 RID: 20297 RVA: 0x001A0A3E File Offset: 0x0019EC3E
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004F4A RID: 20298 RVA: 0x001A0A5B File Offset: 0x0019EC5B
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(string actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004F4B RID: 20299 RVA: 0x001A0A78 File Offset: 0x0019EC78
	public IEnumerator PlayRandomLineAlways(Actor actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004F4C RID: 20300 RVA: 0x001A0A95 File Offset: 0x0019EC95
	public IEnumerator PlayRandomLineAlways(string actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004F4D RID: 20301 RVA: 0x001A0AB4 File Offset: 0x0019ECB4
	protected Actor GetEnemyActorByCardId(string cardId)
	{
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		foreach (Card card in opposingSidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == opposingSidePlayer.GetPlayerId() && entity.GetCardId() == cardId)
			{
				return entity.GetCard().GetActor();
			}
		}
		return null;
	}

	// Token: 0x06004F4E RID: 20302 RVA: 0x001A0B44 File Offset: 0x0019ED44
	protected Actor GetFriendlyActorByCardId(string cardId)
	{
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		foreach (Card card in friendlySidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == friendlySidePlayer.GetPlayerId() && entity.GetCardId() == cardId)
			{
				return entity.GetCard().GetActor();
			}
		}
		return null;
	}

	// Token: 0x06004F4F RID: 20303 RVA: 0x00176BF4 File Offset: 0x00174DF4
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHMulligan);
		}
	}

	// Token: 0x06004F50 RID: 20304 RVA: 0x00177A80 File Offset: 0x00175C80
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHPrologue);
	}

	// Token: 0x04004554 RID: 17748
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x04004555 RID: 17749
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x04004556 RID: 17750
	public bool m_Heroic;

	// Token: 0x04004557 RID: 17751
	public bool m_Galakrond;

	// Token: 0x04004558 RID: 17752
	public static readonly AssetReference BrannBrassRing = new AssetReference("BrannBronzebeard_BrassRing_Quote.prefab:d1f8af47f0917e94289b63f3a42e52f7");

	// Token: 0x04004559 RID: 17753
	public static readonly AssetReference EliseBrassRing = new AssetReference("EliseStarseeker_BrassRing_Quote.prefab:7176acaa6d28fa447adbafde663037d3");

	// Token: 0x0400455A RID: 17754
	public static readonly AssetReference FinleyBrassRing = new AssetReference("SirFinley_BrassRing_Quote.prefab:5f94953d717142446b348e4d2f3a4ca8");

	// Token: 0x0400455B RID: 17755
	public static readonly AssetReference RenoBrassRing = new AssetReference("RenoJackson_BrassRing_Quote.prefab:74a27d2f94ef83744a0a8357dbac2e43");

	// Token: 0x0400455C RID: 17756
	public static readonly AssetReference RafaamBrassRing = new AssetReference("Rafaam_BrassRing_Quote.prefab:2d6ab3cc1d153ed4886ff98e47d129c6");

	// Token: 0x0400455D RID: 17757
	public string m_introLine;

	// Token: 0x0400455E RID: 17758
	public string m_deathLine;

	// Token: 0x0400455F RID: 17759
	public string m_standardEmoteResponseLine;

	// Token: 0x04004560 RID: 17760
	public List<string> m_BossIdleLines;

	// Token: 0x04004561 RID: 17761
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x04004562 RID: 17762
	private int m_PlayPlayerVOLineIndex;

	// Token: 0x04004563 RID: 17763
	private int m_PlayBossVOLineIndex;

	// Token: 0x04004564 RID: 17764
	public int TurnOfPlotTwistLastPlayed;
}
