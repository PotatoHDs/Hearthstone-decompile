using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

// Token: 0x02000553 RID: 1363
public abstract class BoH_Uther_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x06004B40 RID: 19264 RVA: 0x0017BF3A File Offset: 0x0017A13A
	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.BOH;
	}

	// Token: 0x06004B41 RID: 19265 RVA: 0x0018EF80 File Offset: 0x0018D180
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			yield break;
		}
		yield break;
	}

	// Token: 0x06004B42 RID: 19266 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x06004B43 RID: 19267 RVA: 0x0018EF90 File Offset: 0x0018D190
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

	// Token: 0x06004B44 RID: 19268 RVA: 0x0017F615 File Offset: 0x0017D815
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_Mulligan);
		}
	}

	// Token: 0x06004B45 RID: 19269 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayPlotTwistVOLine()
	{
		return 1f;
	}

	// Token: 0x06004B46 RID: 19270 RVA: 0x0018F018 File Offset: 0x0018D218
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

	// Token: 0x06004B47 RID: 19271 RVA: 0x0018F084 File Offset: 0x0018D284
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

	// Token: 0x06004B48 RID: 19272 RVA: 0x0018F0BA File Offset: 0x0018D2BA
	public BoH_Uther_MissionEntity()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Uther_MissionEntity.s_booleanOptions);
	}

	// Token: 0x06004B49 RID: 19273 RVA: 0x0016DED1 File Offset: 0x0016C0D1
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			}
		};
	}

	// Token: 0x04003FA4 RID: 16292
	public bool m_MissionDisableAutomaticVO;

	// Token: 0x04003FA5 RID: 16293
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Uther_MissionEntity.InitBooleanOptions();

	// Token: 0x02001DA4 RID: 7588
	public static class MemberInfoGetting
	{
		// Token: 0x06010DDF RID: 69087 RVA: 0x00481530 File Offset: 0x0047F730
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}
}
