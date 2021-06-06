using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

// Token: 0x02000506 RID: 1286
public abstract class BTA_MissionEntity_Heroic : GenericDungeonMissionEntity
{
	// Token: 0x06004541 RID: 17729 RVA: 0x00176F35 File Offset: 0x00175135
	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.BTA_HEROIC;
	}

	// Token: 0x06004542 RID: 17730 RVA: 0x00176F3C File Offset: 0x0017513C
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			yield break;
		}
		yield break;
	}

	// Token: 0x06004543 RID: 17731 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x06004544 RID: 17732 RVA: 0x00176F4C File Offset: 0x0017514C
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

	// Token: 0x06004545 RID: 17733 RVA: 0x00176BF4 File Offset: 0x00174DF4
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHMulligan);
		}
	}

	// Token: 0x06004546 RID: 17734 RVA: 0x00176FC8 File Offset: 0x001751C8
	public int GetDefeatedBossCountForFinalBoss()
	{
		int missionId = GameMgr.Get().GetMissionId();
		if (missionId == 3432 || missionId == 3437)
		{
			return 0;
		}
		return 7;
	}

	// Token: 0x06004547 RID: 17735 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayPlotTwistVOLine()
	{
		return 1f;
	}

	// Token: 0x06004548 RID: 17736 RVA: 0x00176C37 File Offset: 0x00174E37
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}

	// Token: 0x06004549 RID: 17737 RVA: 0x00176FF4 File Offset: 0x001751F4
	public static bool GetIsFirstBoss()
	{
		int @int = Options.Get().GetInt(Option.SELECTED_ADVENTURE);
		AdventureModeDbId int2 = (AdventureModeDbId)Options.Get().GetInt(Option.SELECTED_ADVENTURE_MODE);
		AdventureDataDbfRecord adventureDataRecord = BTA_MissionEntity_Heroic.GetAdventureDataRecord(@int, (int)int2);
		if (adventureDataRecord == null)
		{
			return true;
		}
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out num);
		return num == 0L;
	}

	// Token: 0x0600454A RID: 17738 RVA: 0x00177044 File Offset: 0x00175244
	public static AdventureDataDbfRecord GetAdventureDataRecord(int adventureId, int modeId)
	{
		foreach (AdventureDataDbfRecord adventureDataDbfRecord in GameDbf.AdventureData.GetRecords())
		{
			if (adventureDataDbfRecord.AdventureId == adventureId && adventureDataDbfRecord.ModeId == modeId)
			{
				return adventureDataDbfRecord;
			}
		}
		return null;
	}

	// Token: 0x0600454B RID: 17739 RVA: 0x001770B0 File Offset: 0x001752B0
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return this.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600454C RID: 17740 RVA: 0x001770CD File Offset: 0x001752CD
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(string actor, List<string> lines)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return this.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600454D RID: 17741 RVA: 0x001770EA File Offset: 0x001752EA
	protected new IEnumerator PlayLineOnlyOnce(Actor speaker, string line, float duration = 2.5f)
	{
		yield return base.PlayLine(speaker, line, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), duration);
		yield break;
	}

	// Token: 0x0600454E RID: 17742 RVA: 0x0017710E File Offset: 0x0017530E
	protected new IEnumerator PlayLineOnlyOnce(string speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), duration);
		yield break;
	}

	// Token: 0x0600454F RID: 17743 RVA: 0x00177132 File Offset: 0x00175332
	protected new IEnumerator PlayLine(string speaker, string line, MissionEntity.ShouldPlay shouldPlay, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, shouldPlay, Vector3.zero, Notification.SpeechBubbleDirection.None, duration, false);
		yield break;
	}

	// Token: 0x06004550 RID: 17744 RVA: 0x0017715E File Offset: 0x0017535E
	protected IEnumerator PlayLineWithUnderlay(Actor speaker, string line, string underlay, float duration = 2.5f)
	{
		base.PlaySound(underlay, 1f, true, false);
		yield return base.PlayLine(speaker, line, new MissionEntity.ShouldPlay(base.InternalShouldPlayBossLine), duration);
		yield break;
	}

	// Token: 0x06004551 RID: 17745 RVA: 0x0017718C File Offset: 0x0017538C
	protected new IEnumerator PlayLine(string speaker, string line, MissionEntity.ShouldPlay shouldPlay, Vector3 quotePosition, Notification.SpeechBubbleDirection direction, float duration, bool persistCharacter = false)
	{
		if (this.m_enemySpeaking)
		{
			yield break;
		}
		this.m_enemySpeaking = true;
		if (this.m_forceAlwaysPlayLine)
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlayBigCharacterQuoteAndWait(speaker, line, quotePosition, direction, duration, 1f, true, false, persistCharacter));
		}
		else if (shouldPlay() == MissionEntity.ShouldPlayValue.Always)
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlayBigCharacterQuoteAndWait(speaker, line, quotePosition, direction, duration, 1f, true, false, persistCharacter));
		}
		else if (shouldPlay() == MissionEntity.ShouldPlayValue.Once)
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce(speaker, line, duration, 1f, false, persistCharacter));
		}
		NotificationManager.Get().ForceAddSoundToPlayedList(line);
		this.m_enemySpeaking = false;
		yield break;
	}

	// Token: 0x06004552 RID: 17746 RVA: 0x001771DB File Offset: 0x001753DB
	protected IEnumerator PlayLine(Actor speaker, string line, MissionEntity.ShouldPlay shouldPlay, Vector3 quotePosition, Notification.SpeechBubbleDirection direction, float duration, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection speakerDirection = base.GetDirection(speaker);
		if (this.m_forceAlwaysPlayLine)
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlayLine(speaker, line, shouldPlay, duration));
		}
		if (shouldPlay() == MissionEntity.ShouldPlayValue.Always)
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlaySoundAndBlockSpeech(line, speakerDirection, speaker, duration, 1f, true, false, 0f));
		}
		else if (shouldPlay() == MissionEntity.ShouldPlayValue.Once)
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlaySoundAndBlockSpeechOnce(line, speakerDirection, speaker, duration, 1f, true, false, 0f));
		}
		NotificationManager.Get().ForceAddSoundToPlayedList(line);
		yield break;
	}

	// Token: 0x06004553 RID: 17747 RVA: 0x00177208 File Offset: 0x00175408
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

	// Token: 0x06004554 RID: 17748 RVA: 0x0017723E File Offset: 0x0017543E
	protected IEnumerator PlayLineAlways(Actor speaker, string line, Notification.SpeechBubbleDirection direction, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(base.InternalShouldPlayBossLine), duration, direction);
		yield break;
	}

	// Token: 0x06004555 RID: 17749 RVA: 0x0017726A File Offset: 0x0017546A
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines, Notification.SpeechBubbleDirection direction)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return this.PlayLineOnlyOnce(actor, text, direction, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004556 RID: 17750 RVA: 0x0017728E File Offset: 0x0017548E
	protected IEnumerator PlayLineOnlyOnce(Actor speaker, string line, Notification.SpeechBubbleDirection direction, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), duration, direction);
		yield break;
	}

	// Token: 0x06004557 RID: 17751 RVA: 0x001772BA File Offset: 0x001754BA
	protected IEnumerator PlayLine(Actor speaker, string line, MissionEntity.ShouldPlay shouldPlay, float duration, Notification.SpeechBubbleDirection direction)
	{
		if (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.m_enemySpeaking = true;
		if (this.m_forceAlwaysPlayLine)
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlaySoundAndBlockSpeech(line, direction, speaker, duration, 1f, true, false, 0f));
		}
		if (shouldPlay() == MissionEntity.ShouldPlayValue.Always)
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlaySoundAndBlockSpeech(line, direction, speaker, duration, 1f, true, false, 0f));
		}
		else if (shouldPlay() == MissionEntity.ShouldPlayValue.Once)
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlaySoundAndBlockSpeechOnce(line, direction, speaker, duration, 1f, true, false, 0f));
		}
		NotificationManager.Get().ForceAddSoundToPlayedList(line);
		this.m_enemySpeaking = false;
		yield break;
	}

	// Token: 0x06004558 RID: 17752 RVA: 0x001772EE File Offset: 0x001754EE
	protected IEnumerator PlayBossLine(Actor speaker, string line, Notification.SpeechBubbleDirection direction, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(base.InternalShouldPlayBossLine), duration, direction);
		yield break;
	}

	// Token: 0x0400382F RID: 14383
	private HashSet<string> m_InOrderPlayedLines = new HashSet<string>();

	// Token: 0x02001BBA RID: 7098
	public static class MemberInfoGetting
	{
		// Token: 0x06010285 RID: 66181 RVA: 0x00481530 File Offset: 0x0047F730
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}
}
