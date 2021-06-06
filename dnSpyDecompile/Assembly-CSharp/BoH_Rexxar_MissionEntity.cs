using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

// Token: 0x0200053F RID: 1343
public abstract class BoH_Rexxar_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x0600499E RID: 18846 RVA: 0x0017BF3A File Offset: 0x0017A13A
	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.BOH;
	}

	// Token: 0x0600499F RID: 18847 RVA: 0x00188AA8 File Offset: 0x00186CA8
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			yield break;
		}
		yield break;
	}

	// Token: 0x060049A0 RID: 18848 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x060049A1 RID: 18849 RVA: 0x00188AB8 File Offset: 0x00186CB8
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

	// Token: 0x060049A2 RID: 18850 RVA: 0x0017F615 File Offset: 0x0017D815
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_Mulligan);
		}
	}

	// Token: 0x060049A3 RID: 18851 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayPlotTwistVOLine()
	{
		return 1f;
	}

	// Token: 0x060049A4 RID: 18852 RVA: 0x00188B34 File Offset: 0x00186D34
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

	// Token: 0x060049A5 RID: 18853 RVA: 0x00188BA0 File Offset: 0x00186DA0
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

	// Token: 0x02001D23 RID: 7459
	public static class MemberInfoGetting
	{
		// Token: 0x06010ADF RID: 68319 RVA: 0x00481530 File Offset: 0x0047F730
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}
}
