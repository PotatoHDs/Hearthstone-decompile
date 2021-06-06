using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

// Token: 0x02000521 RID: 1313
public abstract class BoH_Garrosh_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x0600474D RID: 18253 RVA: 0x0017BF3A File Offset: 0x0017A13A
	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.BOH;
	}

	// Token: 0x0600474E RID: 18254 RVA: 0x0017F580 File Offset: 0x0017D780
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			yield break;
		}
		yield break;
	}

	// Token: 0x0600474F RID: 18255 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x06004750 RID: 18256 RVA: 0x0017F590 File Offset: 0x0017D790
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
		if (this.m_MissionDisableAutomaticVO)
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

	// Token: 0x06004751 RID: 18257 RVA: 0x0017F615 File Offset: 0x0017D815
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_Mulligan);
		}
	}

	// Token: 0x06004752 RID: 18258 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayPlotTwistVOLine()
	{
		return 1f;
	}

	// Token: 0x06004753 RID: 18259 RVA: 0x0017F62C File Offset: 0x0017D82C
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

	// Token: 0x06004754 RID: 18260 RVA: 0x0017F698 File Offset: 0x0017D898
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

	// Token: 0x04003ABE RID: 15038
	public bool m_MissionDisableAutomaticVO;

	// Token: 0x02001C6C RID: 7276
	public static class MemberInfoGetting
	{
		// Token: 0x060106A0 RID: 67232 RVA: 0x00481530 File Offset: 0x0047F730
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}
}
