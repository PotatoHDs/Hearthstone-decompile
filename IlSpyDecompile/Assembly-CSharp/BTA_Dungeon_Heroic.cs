using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Dungeon_Heroic : BTA_MissionEntity_Heroic
{
	public List<string> m_BossVOLines = new List<string>();

	public List<string> m_PlayerVOLines = new List<string>();

	public bool m_DisableIdle;

	private static readonly AssetReference Karnuk_Demon_Hunter_Popup_BrassRing = new AssetReference("Karnuk_Demon_Hunter_Popup_BrassRing.prefab:af78f17e1126eef41b6700cad3d1bccb");

	public static readonly AssetReference KarnukBrassRing = new AssetReference("Karnuk_Outcast_Popup_BrassRing.prefab:d097e6294875881488492604e9320e64");

	public static readonly AssetReference KarnukBrassRingDemonHunter = new AssetReference("Karnuk_Demon_Hunter_Popup_BrassRing.prefab:af78f17e1126eef41b6700cad3d1bccb");

	public static readonly AssetReference ShaljaBrassRing = new AssetReference("Shalja_Outcast_Popup_BrassRing.prefab:0425972e057e448458abedcc24797c3a");

	public static readonly AssetReference ShaljaBrassRingDemonHunter = new AssetReference("Shalja_Demon_Hunter_Popup_BrassRing.prefab:08f4bb41a6104a94ca96bb8003fa826f");

	public static readonly AssetReference BaduuBrassRing = new AssetReference("Baduu_Outcast_Popup_BrassRing.prefab:9202d8afcf6e80542ae9dafd691df43f");

	public static readonly AssetReference SklibbBrassRing = new AssetReference("Sklibb_Outcast_Popup_BrassRing.prefab:ec8003f5e3c1c564cb20b106672a8ed4");

	public static readonly AssetReference SklibbBrassRingDemonHunter = new AssetReference("Sklibb_Demon_Hunter_Popup_BrassRing.prefab:6bf5ceddde5f11347bb7df1c1266fb20");

	public static readonly AssetReference IllidanBrassRing = new AssetReference("DemonHunter_Illidan_Popup_BrassRing.prefab:8c007b8e8be417c4fbd9738960e6f7f0");

	public static readonly AssetReference ArannaBrassRing = new AssetReference("Aranna_Explorer_Popup_Banner.prefab:2d1aaedce4ece664680073bf82f191d6");

	public static readonly AssetReference ArannaBrassRingInTraining = new AssetReference("Aranna_Training_Popup_BrassRing.prefab:d2b86b1c51e1f734daee22d98b4abdcf");

	public static readonly AssetReference ArannaBrassRingDemonHunter = new AssetReference("Aranna_Demon_Hunter_Popup_BrassRing.prefab:57c34d7d7bffe1849a85ffbcf95cda3a");

	public string m_introLine;

	public string m_deathLine;

	public string m_standardEmoteResponseLine;

	public List<string> m_BossIdleLines;

	public List<string> m_BossIdleLinesCopy;

	private int m_PlayPlayerVOLineIndex;

	private int m_PlayBossVOLineIndex;

	public int TurnOfPlotTwistLastPlayed;

	public static BTA_Dungeon_Heroic InstantiateBTADungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		switch (opposingHeroCardID)
		{
		case "BTA_BOSS_18h":
			return new BTA_Fight_18();
		case "BTA_BOSS_19h":
			return new BTA_Fight_20();
		case "BTA_BOSS_20h":
			return new BTA_Fight_19();
		case "BTA_BOSS_21h":
			return new BTA_Fight_21();
		case "BTA_BOSS_22h":
			return new BTA_Fight_22();
		case "BTA_BOSS_23h":
			return new BTA_Fight_23();
		case "BTA_BOSS_24h":
			return new BTA_Fight_24();
		case "BTA_BOSS_25h":
			return new BTA_Fight_25();
		case "BTA_BOSS_26h":
			return new BTA_Fight_26();
		default:
			Log.All.PrintError("BTA_Dungeon.InstantiateBTADungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", opposingHeroCardID);
			return new BTA_Dungeon_Heroic();
		}
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>();
		m_PlayerVOLines = new List<string>(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public virtual List<string> GetIdleLines()
	{
		return new List<string>();
	}

	public void SetBossVOLines(List<string> VOLines)
	{
		m_BossVOLines = new List<string>(VOLines);
	}

	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	protected virtual IEnumerable OnBossHeroPowerPlayed(Entity entity)
	{
		float chanceToPlay = ChanceToPlayBossHeroPowerVOLine();
		float chanceRoll = Random.Range(0f, 1f);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (chanceToPlay < chanceRoll)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (enemyActor == null)
		{
			yield break;
		}
		List<string> bossHeroPowerRandomLines = GetBossHeroPowerRandomLines();
		string text = "";
		while (bossHeroPowerRandomLines.Count > 0)
		{
			int index = Random.Range(0, bossHeroPowerRandomLines.Count);
			text = bossHeroPowerRandomLines[index];
			bossHeroPowerRandomLines.RemoveAt(index);
			if (!NotificationManager.Get().HasSoundPlayedThisSession(text))
			{
				break;
			}
		}
		if (text == "")
		{
			yield return null;
		}
		yield return PlayLineAlways(enemyActor, m_PlayerVOLines[m_PlayPlayerVOLineIndex]);
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		if (!m_enemySpeaking && entity.GetCardType() != 0 && entity.GetCardType() == TAG_CARDTYPE.HERO_POWER && entity.GetControllerSide() == Player.Side.OPPOSING)
		{
			yield return OnBossHeroPowerPlayed(entity);
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		while (m_enemySpeaking)
		{
			yield return null;
		}
		yield return WaitForEntitySoundToFinish(entity);
		entity.GetCardId();
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (!m_enemySpeaking && !string.IsNullOrEmpty(m_deathLine) && gameResult == TAG_PLAYSTATE.WON)
		{
			if (GetShouldSuppressDeathTextBubble())
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_deathLine, Notification.SpeechBubbleDirection.None, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_deathLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
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

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = null;
		m_deathLine = null;
		m_standardEmoteResponseLine = null;
		m_BossIdleLines = new List<string>(GetIdleLines());
		m_BossIdleLinesCopy = new List<string>(GetIdleLines());
	}

	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(busy: true);
			while (m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(busy: false);
			yield break;
		}
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		Random.Range(0f, 1f);
		GetTag(GAME_TAG.TURN);
		GameState.Get().GetGameEntity().GetTag(GAME_TAG.EXTRA_TURNS_TAKEN_THIS_GAME);
		switch (missionEvent)
		{
		case 1000:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			if (m_PlayPlayerVOLineIndex + 1 >= m_PlayerVOLines.Count)
			{
				m_PlayPlayerVOLineIndex = 0;
			}
			else
			{
				m_PlayPlayerVOLineIndex++;
			}
			SceneDebugger.Get().AddMessage(m_PlayerVOLines[m_PlayPlayerVOLineIndex]);
			yield return PlayBossLine(actor, m_PlayerVOLines[m_PlayPlayerVOLineIndex]);
			break;
		case 1001:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			SceneDebugger.Get().AddMessage(m_PlayerVOLines[m_PlayPlayerVOLineIndex]);
			yield return PlayBossLine(actor, m_PlayerVOLines[m_PlayPlayerVOLineIndex]);
			break;
		case 1002:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			if (m_PlayBossVOLineIndex + 1 >= m_BossVOLines.Count)
			{
				m_PlayBossVOLineIndex = 0;
			}
			else
			{
				m_PlayBossVOLineIndex++;
			}
			SceneDebugger.Get().AddMessage(m_BossVOLines[m_PlayBossVOLineIndex]);
			yield return PlayBossLine(enemyActor, m_BossVOLines[m_PlayBossVOLineIndex]);
			break;
		case 1003:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			SceneDebugger.Get().AddMessage(m_BossVOLines[m_PlayBossVOLineIndex]);
			yield return PlayBossLine(enemyActor, m_BossVOLines[m_PlayBossVOLineIndex]);
			break;
		case 1010:
			if (m_forceAlwaysPlayLine)
			{
				m_forceAlwaysPlayLine = false;
			}
			else
			{
				m_forceAlwaysPlayLine = true;
			}
			break;
		case 1011:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			foreach (string bossVOLine in m_BossVOLines)
			{
				SceneDebugger.Get().AddMessage(bossVOLine);
				yield return PlayLineAlways(enemyActor, bossVOLine);
			}
			foreach (string playerVOLine in m_PlayerVOLines)
			{
				SceneDebugger.Get().AddMessage(playerVOLine);
				yield return PlayLineAlways(enemyActor, playerVOLine);
			}
			break;
		case 1012:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			foreach (string bossVOLine2 in m_BossVOLines)
			{
				SceneDebugger.Get().AddMessage(bossVOLine2);
				yield return PlayLineAlways(enemyActor, bossVOLine2);
			}
			break;
		case 1013:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			foreach (string playerVOLine2 in m_PlayerVOLines)
			{
				SceneDebugger.Get().AddMessage(playerVOLine2);
				yield return PlayLineAlways(enemyActor, playerVOLine2);
			}
			break;
		case 58023:
		{
			SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
			GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
			SceneMgr.Get().SetNextMode(postGameSceneMode);
			break;
		}
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		GameMgr.Get().GetMissionId();
		_ = 2;
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	public virtual float GetThinkEmoteBossThinkChancePercentage()
	{
		return 0.25f;
	}

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

	public IEnumerator PlayRandomLineAlways(Actor actor, List<string> lines)
	{
		string text = PopRandomLine(lines);
		if (text != null)
		{
			yield return PlayBossLine(actor, text);
		}
	}

	public IEnumerator PlayRandomLineAlways(string actor, List<string> lines)
	{
		string text = PopRandomLine(lines);
		if (text != null)
		{
			yield return PlayBossLine(actor, text);
		}
	}

	public IEnumerator PlayAndRemoveRandomLineOnlyOnceWithBrassRing(Actor actor, AssetReference brassRingBackup, List<string> lines)
	{
		if (actor != null)
		{
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, lines);
		}
		else if (brassRingBackup != null)
		{
			yield return PlayAndRemoveRandomLineOnlyOnce(brassRingBackup, lines);
		}
	}

	protected IEnumerator PlayLineAlwaysWithBrassRing(Actor actor, AssetReference brassRingBackup, string line, float duration = 2.5f)
	{
		if (actor != null)
		{
			yield return PlayLineAlways(actor, line);
		}
		else if (brassRingBackup != null)
		{
			yield return PlayLineAlways(brassRingBackup, line);
		}
	}

	public IEnumerator PlayLineInOrderOnceWithBrassRing(Actor actor, AssetReference brassRingBackup, List<string> lines)
	{
		if (actor != null)
		{
			yield return PlayLineInOrderOnce(actor, lines);
		}
		else if (brassRingBackup != null)
		{
			yield return PlayLineInOrderOnce(brassRingBackup, lines);
		}
	}

	public override void OnPlayThinkEmote()
	{
		if (m_DisableIdle || m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide() || currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		float thinkEmoteBossThinkChancePercentage = GetThinkEmoteBossThinkChancePercentage();
		float num = Random.Range(0f, 1f);
		if (thinkEmoteBossThinkChancePercentage > num && m_BossIdleLines != null && m_BossIdleLines.Count != 0)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			string line = PopRandomLine(m_BossIdleLinesCopy);
			if (m_BossIdleLinesCopy.Count == 0)
			{
				m_BossIdleLinesCopy = new List<string>(GetIdleLines());
			}
			Gameplay.Get().StartCoroutine(PlayBossLine(actor, line));
			return;
		}
		EmoteType emoteType = EmoteType.THINK1;
		switch (Random.Range(1, 4))
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
		GameState.Get().GetCurrentPlayer().GetHeroCard()
			.PlayEmote(emoteType);
	}
}
