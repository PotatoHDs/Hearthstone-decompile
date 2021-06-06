using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

// Token: 0x02000505 RID: 1285
public abstract class BTA_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x06004529 RID: 17705 RVA: 0x00176B60 File Offset: 0x00174D60
	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.BTA;
	}

	// Token: 0x0600452A RID: 17706 RVA: 0x00176B67 File Offset: 0x00174D67
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			yield break;
		}
		yield break;
	}

	// Token: 0x0600452B RID: 17707 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x0600452C RID: 17708 RVA: 0x00176B78 File Offset: 0x00174D78
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

	// Token: 0x0600452D RID: 17709 RVA: 0x00176BF4 File Offset: 0x00174DF4
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHMulligan);
		}
	}

	// Token: 0x0600452E RID: 17710 RVA: 0x00176C0C File Offset: 0x00174E0C
	public int GetDefeatedBossCountForFinalBoss()
	{
		int missionId = GameMgr.Get().GetMissionId();
		if (missionId == 3432 || missionId == 3437)
		{
			return 0;
		}
		return 7;
	}

	// Token: 0x0600452F RID: 17711 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayPlotTwistVOLine()
	{
		return 1f;
	}

	// Token: 0x06004530 RID: 17712 RVA: 0x00176C37 File Offset: 0x00174E37
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}

	// Token: 0x06004531 RID: 17713 RVA: 0x00176C4C File Offset: 0x00174E4C
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

	// Token: 0x06004532 RID: 17714 RVA: 0x00176CB8 File Offset: 0x00174EB8
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return this.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004533 RID: 17715 RVA: 0x00176CD5 File Offset: 0x00174ED5
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(string actor, List<string> lines)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return this.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004534 RID: 17716 RVA: 0x00176CF2 File Offset: 0x00174EF2
	protected new IEnumerator PlayLineOnlyOnce(Actor speaker, string line, float duration = 2.5f)
	{
		yield return base.PlayLine(speaker, line, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), duration);
		yield break;
	}

	// Token: 0x06004535 RID: 17717 RVA: 0x00176D16 File Offset: 0x00174F16
	protected new IEnumerator PlayLineOnlyOnce(string speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), duration);
		yield break;
	}

	// Token: 0x06004536 RID: 17718 RVA: 0x00176D3A File Offset: 0x00174F3A
	protected new IEnumerator PlayLine(string speaker, string line, MissionEntity.ShouldPlay shouldPlay, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, shouldPlay, Vector3.zero, Notification.SpeechBubbleDirection.None, duration, false);
		yield break;
	}

	// Token: 0x06004537 RID: 17719 RVA: 0x00176D66 File Offset: 0x00174F66
	protected IEnumerator PlayLineWithUnderlay(Actor speaker, string line, string underlay, float duration = 2.5f)
	{
		base.PlaySound(underlay, 1f, true, false);
		yield return base.PlayLine(speaker, line, new MissionEntity.ShouldPlay(base.InternalShouldPlayBossLine), duration);
		yield break;
	}

	// Token: 0x06004538 RID: 17720 RVA: 0x00176D94 File Offset: 0x00174F94
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

	// Token: 0x06004539 RID: 17721 RVA: 0x00176DE3 File Offset: 0x00174FE3
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

	// Token: 0x0600453A RID: 17722 RVA: 0x00176E10 File Offset: 0x00175010
	protected string PopRandomLine(List<string> lines)
	{
		if (lines == null || lines.Count == 0)
		{
			return null;
		}
		string text = lines[UnityEngine.Random.Range(0, lines.Count)];
		lines.Remove(text);
		return text;
	}

	// Token: 0x0600453B RID: 17723 RVA: 0x00176E46 File Offset: 0x00175046
	protected IEnumerator PlayLineAlways(Actor speaker, string line, Notification.SpeechBubbleDirection direction, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(base.InternalShouldPlayBossLine), duration, direction);
		yield break;
	}

	// Token: 0x0600453C RID: 17724 RVA: 0x00176E72 File Offset: 0x00175072
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines, Notification.SpeechBubbleDirection direction)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return this.PlayLineOnlyOnce(actor, text, direction, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600453D RID: 17725 RVA: 0x00176E96 File Offset: 0x00175096
	protected IEnumerator PlayLineOnlyOnce(Actor speaker, string line, Notification.SpeechBubbleDirection direction, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), duration, direction);
		yield break;
	}

	// Token: 0x0600453E RID: 17726 RVA: 0x00176EC2 File Offset: 0x001750C2
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

	// Token: 0x0600453F RID: 17727 RVA: 0x00176EF6 File Offset: 0x001750F6
	protected IEnumerator PlayBossLine(Actor speaker, string line, Notification.SpeechBubbleDirection direction, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(base.InternalShouldPlayBossLine), duration, direction);
		yield break;
	}

	// Token: 0x0400382E RID: 14382
	private HashSet<string> m_InOrderPlayedLines = new HashSet<string>();

	// Token: 0x02001BAB RID: 7083
	public static class MemberInfoGetting
	{
		// Token: 0x06010230 RID: 66096 RVA: 0x00481530 File Offset: 0x0047F730
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}
}
