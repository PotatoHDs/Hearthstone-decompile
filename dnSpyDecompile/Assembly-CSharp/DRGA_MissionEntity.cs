using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

// Token: 0x020004E7 RID: 1255
public abstract class DRGA_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x0600433B RID: 17211 RVA: 0x0016C876 File Offset: 0x0016AA76
	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.DRAGONS;
	}

	// Token: 0x0600433C RID: 17212 RVA: 0x0016C87D File Offset: 0x0016AA7D
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			yield break;
		}
		yield break;
	}

	// Token: 0x0600433D RID: 17213 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x0600433E RID: 17214 RVA: 0x0016C88C File Offset: 0x0016AA8C
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

	// Token: 0x0600433F RID: 17215 RVA: 0x0015F6C4 File Offset: 0x0015D8C4
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ULDMulligan);
		}
	}

	// Token: 0x06004340 RID: 17216 RVA: 0x0016C908 File Offset: 0x0016AB08
	public int GetDefeatedBossCountForFinalBoss()
	{
		int missionId = GameMgr.Get().GetMissionId();
		if (missionId == 3432 || missionId == 3437)
		{
			return 0;
		}
		return 7;
	}

	// Token: 0x06004341 RID: 17217 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayPlotTwistVOLine()
	{
		return 1f;
	}

	// Token: 0x06004342 RID: 17218 RVA: 0x0016C933 File Offset: 0x0016AB33
	public override void StartGameplaySoundtracks()
	{
		if (GameUtils.GetDefeatedBossCount() == this.GetDefeatedBossCountForFinalBoss())
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ULDFinalBoss);
			return;
		}
		base.StartGameplaySoundtracks();
	}

	// Token: 0x06004343 RID: 17219 RVA: 0x0016C95C File Offset: 0x0016AB5C
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

	// Token: 0x06004344 RID: 17220 RVA: 0x0016C9C8 File Offset: 0x0016ABC8
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

	// Token: 0x02001B1B RID: 6939
	public static class MemberInfoGetting
	{
		// Token: 0x0600FEC9 RID: 65225 RVA: 0x00481530 File Offset: 0x0047F730
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}
}
