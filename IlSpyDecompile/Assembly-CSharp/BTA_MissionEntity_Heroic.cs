using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public abstract class BTA_MissionEntity_Heroic : GenericDungeonMissionEntity
{
	public static class MemberInfoGetting
	{
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}

	private HashSet<string> m_InOrderPlayedLines = new HashSet<string>();

	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.BTA_HEROIC;
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
		}
	}

	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	public override void OnPlayThinkEmote()
	{
		if (m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (currentPlayer.IsFriendlySide() && !currentPlayer.GetHeroCard().HasActiveEmoteSound())
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
			GameState.Get().GetCurrentPlayer().GetHeroCard()
				.PlayEmote(emoteType);
		}
	}

	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHMulligan);
		}
	}

	public int GetDefeatedBossCountForFinalBoss()
	{
		int missionId = GameMgr.Get().GetMissionId();
		if (missionId == 3432 || missionId == 3437)
		{
			return 0;
		}
		return 7;
	}

	protected virtual float ChanceToPlayPlotTwistVOLine()
	{
		return 1f;
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}

	public static bool GetIsFirstBoss()
	{
		int @int = Options.Get().GetInt(Option.SELECTED_ADVENTURE);
		AdventureModeDbId int2 = (AdventureModeDbId)Options.Get().GetInt(Option.SELECTED_ADVENTURE_MODE);
		AdventureDataDbfRecord adventureDataRecord = GetAdventureDataRecord(@int, (int)int2);
		if (adventureDataRecord == null)
		{
			return true;
		}
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out long value);
		if (value == 0L)
		{
			return true;
		}
		return false;
	}

	public static AdventureDataDbfRecord GetAdventureDataRecord(int adventureId, int modeId)
	{
		foreach (AdventureDataDbfRecord record in GameDbf.AdventureData.GetRecords())
		{
			if (record.AdventureId == adventureId && record.ModeId == modeId)
			{
				return record;
			}
		}
		return null;
	}

	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines)
	{
		string text = PopRandomLine(lines);
		if (text != null)
		{
			yield return PlayLineOnlyOnce(actor, text);
		}
	}

	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(string actor, List<string> lines)
	{
		string text = PopRandomLine(lines);
		if (text != null)
		{
			yield return PlayLineOnlyOnce(actor, text);
		}
	}

	protected new IEnumerator PlayLineOnlyOnce(Actor speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, base.InternalShouldPlayOnlyOnce, duration);
	}

	protected new IEnumerator PlayLineOnlyOnce(string speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, base.InternalShouldPlayOnlyOnce, duration);
	}

	protected new IEnumerator PlayLine(string speaker, string line, ShouldPlay shouldPlay, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, shouldPlay, Vector3.zero, Notification.SpeechBubbleDirection.None, duration);
	}

	protected IEnumerator PlayLineWithUnderlay(Actor speaker, string line, string underlay, float duration = 2.5f)
	{
		PlaySound(underlay);
		yield return PlayLine(speaker, line, base.InternalShouldPlayBossLine, duration);
	}

	protected new IEnumerator PlayLine(string speaker, string line, ShouldPlay shouldPlay, Vector3 quotePosition, Notification.SpeechBubbleDirection direction, float duration, bool persistCharacter = false)
	{
		if (!m_enemySpeaking)
		{
			m_enemySpeaking = true;
			if (m_forceAlwaysPlayLine)
			{
				yield return GameEntity.Coroutines.StartCoroutine(PlayBigCharacterQuoteAndWait(speaker, line, quotePosition, direction, duration, 1f, allowRepeatDuringSession: true, delayCardSoundSpells: false, persistCharacter));
			}
			else if (shouldPlay() == ShouldPlayValue.Always)
			{
				yield return GameEntity.Coroutines.StartCoroutine(PlayBigCharacterQuoteAndWait(speaker, line, quotePosition, direction, duration, 1f, allowRepeatDuringSession: true, delayCardSoundSpells: false, persistCharacter));
			}
			else if (shouldPlay() == ShouldPlayValue.Once)
			{
				yield return GameEntity.Coroutines.StartCoroutine(PlayBigCharacterQuoteAndWaitOnce(speaker, line, duration, 1f, delayCardSoundSpells: false, persistCharacter));
			}
			NotificationManager.Get().ForceAddSoundToPlayedList(line);
			m_enemySpeaking = false;
		}
	}

	protected IEnumerator PlayLine(Actor speaker, string line, ShouldPlay shouldPlay, Vector3 quotePosition, Notification.SpeechBubbleDirection direction, float duration, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection speakerDirection = GetDirection(speaker);
		if (m_forceAlwaysPlayLine)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlayLine(speaker, line, shouldPlay, duration));
		}
		if (shouldPlay() == ShouldPlayValue.Always)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndBlockSpeech(line, speakerDirection, speaker, duration));
		}
		else if (shouldPlay() == ShouldPlayValue.Once)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndBlockSpeechOnce(line, speakerDirection, speaker, duration));
		}
		NotificationManager.Get().ForceAddSoundToPlayedList(line);
	}

	protected string PopRandomLine(List<string> lines)
	{
		if (lines.Count == 0 || lines == null)
		{
			return null;
		}
		string text = lines[UnityEngine.Random.Range(0, lines.Count)];
		lines.Remove(text);
		return text;
	}

	protected IEnumerator PlayLineAlways(Actor speaker, string line, Notification.SpeechBubbleDirection direction, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, base.InternalShouldPlayBossLine, duration, direction);
	}

	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines, Notification.SpeechBubbleDirection direction)
	{
		string text = PopRandomLine(lines);
		if (text != null)
		{
			yield return PlayLineOnlyOnce(actor, text, direction);
		}
	}

	protected IEnumerator PlayLineOnlyOnce(Actor speaker, string line, Notification.SpeechBubbleDirection direction, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, base.InternalShouldPlayOnlyOnce, duration, direction);
	}

	protected IEnumerator PlayLine(Actor speaker, string line, ShouldPlay shouldPlay, float duration, Notification.SpeechBubbleDirection direction)
	{
		if (m_enemySpeaking)
		{
			yield return null;
		}
		m_enemySpeaking = true;
		if (m_forceAlwaysPlayLine)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndBlockSpeech(line, direction, speaker, duration));
		}
		if (shouldPlay() == ShouldPlayValue.Always)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndBlockSpeech(line, direction, speaker, duration));
		}
		else if (shouldPlay() == ShouldPlayValue.Once)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndBlockSpeechOnce(line, direction, speaker, duration));
		}
		NotificationManager.Get().ForceAddSoundToPlayedList(line);
		m_enemySpeaking = false;
	}

	protected IEnumerator PlayBossLine(Actor speaker, string line, Notification.SpeechBubbleDirection direction, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, base.InternalShouldPlayBossLine, duration, direction);
	}
}
