using System;
using System.Collections.Generic;
using System.Linq;
using PegasusShared;

// Token: 0x0200093B RID: 2363
public static class UserAttentionManager
{
	// Token: 0x1400008E RID: 142
	// (add) Token: 0x060082B1 RID: 33457 RVA: 0x002A71A8 File Offset: 0x002A53A8
	// (remove) Token: 0x060082B2 RID: 33458 RVA: 0x002A71DC File Offset: 0x002A53DC
	public static event Action<UserAttentionBlocker> OnBlockingStart;

	// Token: 0x1400008F RID: 143
	// (add) Token: 0x060082B3 RID: 33459 RVA: 0x002A7210 File Offset: 0x002A5410
	// (remove) Token: 0x060082B4 RID: 33460 RVA: 0x002A7244 File Offset: 0x002A5444
	public static event Action OnBlockingEnd;

	// Token: 0x1700076D RID: 1901
	// (get) Token: 0x060082B5 RID: 33461 RVA: 0x002A7277 File Offset: 0x002A5477
	private static bool IsBlocked
	{
		get
		{
			return UserAttentionManager.s_blockedReasons > UserAttentionBlocker.NONE;
		}
	}

	// Token: 0x060082B6 RID: 33462 RVA: 0x002A7281 File Offset: 0x002A5481
	public static bool IsBlockedBy(UserAttentionBlocker attentionCategory)
	{
		return attentionCategory != UserAttentionBlocker.NONE && (UserAttentionManager.s_blockedReasons & attentionCategory) == attentionCategory;
	}

	// Token: 0x060082B7 RID: 33463 RVA: 0x002A7292 File Offset: 0x002A5492
	public static bool CanShowAttentionGrabber(string callerName)
	{
		return UserAttentionManager.CanShowAttentionGrabber(UserAttentionBlocker.NONE, callerName);
	}

	// Token: 0x060082B8 RID: 33464 RVA: 0x002A729C File Offset: 0x002A549C
	public static bool CanShowAttentionGrabber(UserAttentionBlocker attentionCategory, string callerName)
	{
		if ((UserAttentionManager.s_blockedReasons & ~attentionCategory) > UserAttentionBlocker.NONE)
		{
			IEnumerable<string> source = (from UserAttentionBlocker r in Enum.GetValues(typeof(UserAttentionBlocker))
			where r != attentionCategory && UserAttentionManager.IsBlockedBy(r)
			select r).Select(delegate(UserAttentionBlocker r)
			{
				UserAttentionBlocker userAttentionBlocker = r;
				return userAttentionBlocker.ToString();
			});
			string text = string.Join(", ", source.ToArray<string>());
			Log.UserAttention.Print("UserAttentionManager attention grabber [{0}] blocked by: {1}", new object[]
			{
				callerName,
				text
			});
			return false;
		}
		return true;
	}

	// Token: 0x060082B9 RID: 33465 RVA: 0x002A7344 File Offset: 0x002A5544
	public static void StartBlocking(UserAttentionBlocker attentionCategory)
	{
		if (UserAttentionManager.IsBlockedBy(attentionCategory))
		{
			return;
		}
		bool isBlocked = UserAttentionManager.IsBlocked;
		if (isBlocked)
		{
			string text = UserAttentionManager.DumpUserAttentionBlockers("StartBlocking");
			Error.AddDevFatal("UserAttentionBlocker.{0} already active, cannot StartBlocking {1}", new object[]
			{
				text,
				attentionCategory
			});
		}
		UserAttentionManager.s_blockedReasons |= attentionCategory;
		UserAttentionManager.DumpUserAttentionBlockers("StartBlocking[" + attentionCategory + "]");
		if (!isBlocked && UserAttentionManager.OnBlockingStart != null)
		{
			UserAttentionManager.OnBlockingStart(attentionCategory);
		}
	}

	// Token: 0x060082BA RID: 33466 RVA: 0x002A73C8 File Offset: 0x002A55C8
	public static void StopBlocking(UserAttentionBlocker attentionCategory)
	{
		bool isBlocked = UserAttentionManager.IsBlocked;
		UserAttentionManager.s_blockedReasons &= ~attentionCategory;
		if (isBlocked)
		{
			if (UserAttentionManager.s_blockedReasons == UserAttentionBlocker.NONE)
			{
				Log.UserAttention.Print("UserAttentionManager.StopBlocking[{0}] - all blockers cleared.", new object[]
				{
					attentionCategory
				});
				if (UserAttentionManager.OnBlockingEnd != null)
				{
					UserAttentionManager.OnBlockingEnd();
					return;
				}
			}
			else
			{
				Log.UserAttention.Print("UserAttentionManager.StopBlocking[{0}]", new object[]
				{
					attentionCategory
				});
			}
		}
	}

	// Token: 0x060082BB RID: 33467 RVA: 0x002A7440 File Offset: 0x002A5640
	public static AvailabilityBlockerReasons GetAvailabilityBlockerReason(bool isFriendlyChallenge)
	{
		if (SpectatorManager.Get().IsInSpectatorMode())
		{
			return AvailabilityBlockerReasons.SPECTATING_GAME;
		}
		if (GameMgr.Get().IsFindingGame())
		{
			bool flag = GameMgr.Get().GetNextGameType() == GameType.GT_RANKED;
			FormatType nextFormatType = GameMgr.Get().GetNextFormatType();
			bool flag2 = (nextFormatType == FormatType.FT_UNKNOWN) ? RankMgr.Get().IsLegendRank(FormatType.FT_STANDARD) : RankMgr.Get().IsLegendRank(nextFormatType);
			if (!flag || !flag2 || !isFriendlyChallenge)
			{
				return AvailabilityBlockerReasons.FINDING_GAME;
			}
		}
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FATAL_ERROR))
		{
			return AvailabilityBlockerReasons.HAS_FATAL_ERROR;
		}
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.LOGIN))
		{
			return AvailabilityBlockerReasons.LOGGING_IN;
		}
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.STARTUP))
		{
			return AvailabilityBlockerReasons.STARTING_UP;
		}
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.GAMEPLAY))
		{
			if (GameMgr.Get().IsSpectator() || GameMgr.Get().IsNextSpectator())
			{
				return AvailabilityBlockerReasons.SPECTATING_GAME;
			}
			if (GameMgr.Get().IsAI() || GameMgr.Get().IsNextAI())
			{
				return AvailabilityBlockerReasons.PLAYING_AI_GAME;
			}
			return AvailabilityBlockerReasons.PLAYING_NON_AI_GAME;
		}
		else
		{
			if (!GameUtils.AreAllTutorialsComplete())
			{
				return AvailabilityBlockerReasons.TUTORIALS_INCOMPLETE;
			}
			if (ShownUIMgr.Get().GetShownUI() == ShownUIMgr.UI_WINDOW.GENERAL_STORE)
			{
				return AvailabilityBlockerReasons.STORE_IS_SHOWN;
			}
			if (ShownUIMgr.Get().GetShownUI() == ShownUIMgr.UI_WINDOW.ARENA_STORE)
			{
				return AvailabilityBlockerReasons.STORE_IS_SHOWN;
			}
			if (TavernBrawlDisplay.Get() != null && TavernBrawlDisplay.Get().IsInDeckEditMode())
			{
				return AvailabilityBlockerReasons.EDITING_TAVERN_BRAWL;
			}
			if (NarrativeManager.Get() != null && NarrativeManager.Get().IsShowingBlockingDialog())
			{
				return AvailabilityBlockerReasons.IN_BLOCKING_NARRATIVE_DIALOG;
			}
			if (SetRotationManager.ShouldShowSetRotationIntro())
			{
				return AvailabilityBlockerReasons.SHOULD_BE_SHOWING_SET_ROTATION;
			}
			if (PopupDisplayManager.Get().IsShowing && !isFriendlyChallenge)
			{
				return AvailabilityBlockerReasons.POPUP_SHOWING;
			}
			if (DraftDisplay.Get() != null && DraftDisplay.Get().GetDraftMode() == DraftDisplay.DraftMode.IN_REWARDS && !isFriendlyChallenge)
			{
				return AvailabilityBlockerReasons.DRAFT_REWARDS_SHOWING;
			}
			return AvailabilityBlockerReasons.NONE;
		}
	}

	// Token: 0x1700076E RID: 1902
	// (get) Token: 0x060082BC RID: 33468 RVA: 0x002A75B8 File Offset: 0x002A57B8
	private static string CurrentActiveBlockersString
	{
		get
		{
			IEnumerable<string> source = (from UserAttentionBlocker r in Enum.GetValues(typeof(UserAttentionBlocker))
			where UserAttentionManager.IsBlockedBy(r)
			select r).Select(delegate(UserAttentionBlocker r)
			{
				UserAttentionBlocker userAttentionBlocker = r;
				return userAttentionBlocker.ToString();
			});
			return string.Join(", ", source.ToArray<string>());
		}
	}

	// Token: 0x060082BD RID: 33469 RVA: 0x002A7634 File Offset: 0x002A5834
	public static string DumpUserAttentionBlockers(string callerName)
	{
		string currentActiveBlockersString = UserAttentionManager.CurrentActiveBlockersString;
		Log.UserAttention.Print("UserAttentionManager:{0} current blockers: {1}", new object[]
		{
			callerName,
			currentActiveBlockersString
		});
		return currentActiveBlockersString;
	}

	// Token: 0x04006D81 RID: 28033
	private static UserAttentionBlocker s_blockedReasons;
}
