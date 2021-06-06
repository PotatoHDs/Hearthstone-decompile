using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

// Token: 0x020005C2 RID: 1474
public abstract class BTB_RumbleDome_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x06005138 RID: 20792 RVA: 0x0017935C File Offset: 0x0017755C
	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.BTP;
	}

	// Token: 0x06005139 RID: 20793 RVA: 0x001AB8D0 File Offset: 0x001A9AD0
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			yield break;
		}
		yield break;
	}

	// Token: 0x0600513A RID: 20794 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x0600513B RID: 20795 RVA: 0x001AB8E0 File Offset: 0x001A9AE0
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

	// Token: 0x0600513C RID: 20796 RVA: 0x0015F6C4 File Offset: 0x0015D8C4
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ULDMulligan);
		}
	}

	// Token: 0x0600513D RID: 20797 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayPlotTwistVOLine()
	{
		return 1f;
	}

	// Token: 0x0600513E RID: 20798 RVA: 0x001AB95C File Offset: 0x001A9B5C
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

	// Token: 0x0600513F RID: 20799 RVA: 0x001AB9C8 File Offset: 0x001A9BC8
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

	// Token: 0x02001FDB RID: 8155
	public static class MemberInfoGetting
	{
		// Token: 0x06011AA3 RID: 72355 RVA: 0x00481530 File Offset: 0x0047F730
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}
}
