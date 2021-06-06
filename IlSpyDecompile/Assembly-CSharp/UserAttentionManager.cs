using System;
using System.Collections.Generic;
using System.Linq;
using PegasusShared;

public static class UserAttentionManager
{
	private static UserAttentionBlocker s_blockedReasons;

	private static bool IsBlocked => s_blockedReasons != UserAttentionBlocker.NONE;

	private static string CurrentActiveBlockersString
	{
		get
		{
			IEnumerable<string> source = (from UserAttentionBlocker r in Enum.GetValues(typeof(UserAttentionBlocker))
				where IsBlockedBy(r)
				select r).Select(delegate(UserAttentionBlocker r)
			{
				UserAttentionBlocker userAttentionBlocker = r;
				return userAttentionBlocker.ToString();
			});
			return string.Join(", ", source.ToArray());
		}
	}

	public static event Action<UserAttentionBlocker> OnBlockingStart;

	public static event Action OnBlockingEnd;

	public static bool IsBlockedBy(UserAttentionBlocker attentionCategory)
	{
		if (attentionCategory != 0)
		{
			return (s_blockedReasons & attentionCategory) == attentionCategory;
		}
		return false;
	}

	public static bool CanShowAttentionGrabber(string callerName)
	{
		return CanShowAttentionGrabber(UserAttentionBlocker.NONE, callerName);
	}

	public static bool CanShowAttentionGrabber(UserAttentionBlocker attentionCategory, string callerName)
	{
		if ((s_blockedReasons & ~attentionCategory) != 0)
		{
			IEnumerable<string> source = (from UserAttentionBlocker r in Enum.GetValues(typeof(UserAttentionBlocker))
				where r != attentionCategory && IsBlockedBy(r)
				select r).Select(delegate(UserAttentionBlocker r)
			{
				UserAttentionBlocker userAttentionBlocker = r;
				return userAttentionBlocker.ToString();
			});
			string text = string.Join(", ", source.ToArray());
			Log.UserAttention.Print("UserAttentionManager attention grabber [{0}] blocked by: {1}", callerName, text);
			return false;
		}
		return true;
	}

	public static void StartBlocking(UserAttentionBlocker attentionCategory)
	{
		if (!IsBlockedBy(attentionCategory))
		{
			bool isBlocked = IsBlocked;
			if (isBlocked)
			{
				string text = DumpUserAttentionBlockers("StartBlocking");
				Error.AddDevFatal("UserAttentionBlocker.{0} already active, cannot StartBlocking {1}", text, attentionCategory);
			}
			s_blockedReasons |= attentionCategory;
			DumpUserAttentionBlockers(string.Concat("StartBlocking[", attentionCategory, "]"));
			if (!isBlocked && UserAttentionManager.OnBlockingStart != null)
			{
				UserAttentionManager.OnBlockingStart(attentionCategory);
			}
		}
	}

	public static void StopBlocking(UserAttentionBlocker attentionCategory)
	{
		bool isBlocked = IsBlocked;
		s_blockedReasons &= ~attentionCategory;
		if (!isBlocked)
		{
			return;
		}
		if (s_blockedReasons == UserAttentionBlocker.NONE)
		{
			Log.UserAttention.Print("UserAttentionManager.StopBlocking[{0}] - all blockers cleared.", attentionCategory);
			if (UserAttentionManager.OnBlockingEnd != null)
			{
				UserAttentionManager.OnBlockingEnd();
			}
		}
		else
		{
			Log.UserAttention.Print("UserAttentionManager.StopBlocking[{0}]", attentionCategory);
		}
	}

	public static AvailabilityBlockerReasons GetAvailabilityBlockerReason(bool isFriendlyChallenge)
	{
		if (SpectatorManager.Get().IsInSpectatorMode())
		{
			return AvailabilityBlockerReasons.SPECTATING_GAME;
		}
		if (GameMgr.Get().IsFindingGame())
		{
			bool num = GameMgr.Get().GetNextGameType() == GameType.GT_RANKED;
			FormatType nextFormatType = GameMgr.Get().GetNextFormatType();
			bool flag = ((nextFormatType == FormatType.FT_UNKNOWN) ? RankMgr.Get().IsLegendRank(FormatType.FT_STANDARD) : RankMgr.Get().IsLegendRank(nextFormatType));
			if (!num || !flag || !isFriendlyChallenge)
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

	public static string DumpUserAttentionBlockers(string callerName)
	{
		string currentActiveBlockersString = CurrentActiveBlockersString;
		Log.UserAttention.Print("UserAttentionManager:{0} current blockers: {1}", callerName, currentActiveBlockersString);
		return currentActiveBlockersString;
	}
}
