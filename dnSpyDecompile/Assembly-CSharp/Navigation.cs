using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.Login;
using UnityEngine;

// Token: 0x020005F7 RID: 1527
public static class Navigation
{
	// Token: 0x17000514 RID: 1300
	// (get) Token: 0x06005308 RID: 21256 RVA: 0x001B222B File Offset: 0x001B042B
	// (set) Token: 0x06005309 RID: 21257 RVA: 0x001B223D File Offset: 0x001B043D
	public static bool NAVIGATION_DEBUG
	{
		get
		{
			return Vars.Key("Application.Navigation.Debug").GetBool(false);
		}
		set
		{
			Vars.Key("Application.Navigation.Debug").Set(value.ToString(), false);
		}
	}

	// Token: 0x0600530A RID: 21258 RVA: 0x001B2256 File Offset: 0x001B0456
	public static void Clear()
	{
		Navigation.m_history.Clear();
		if (Navigation.NAVIGATION_DEBUG)
		{
			Navigation.DumpStack();
		}
	}

	// Token: 0x17000515 RID: 1301
	// (get) Token: 0x0600530B RID: 21259 RVA: 0x001B226E File Offset: 0x001B046E
	public static bool IsEmpty
	{
		get
		{
			return Navigation.m_history.Count == 0;
		}
	}

	// Token: 0x17000516 RID: 1302
	// (get) Token: 0x0600530C RID: 21260 RVA: 0x001B227D File Offset: 0x001B047D
	public static bool CanGoBack
	{
		get
		{
			return !Navigation.IsEmpty && Navigation.CanNavigate();
		}
	}

	// Token: 0x0600530D RID: 21261 RVA: 0x001B2290 File Offset: 0x001B0490
	public static bool GoBack()
	{
		if (!Navigation.CanGoBack)
		{
			return false;
		}
		Navigation.NavigateBackHandler navigateBackHandler = Navigation.m_history.Peek();
		if (navigateBackHandler())
		{
			if (Navigation.m_history.Count > 0 && navigateBackHandler == Navigation.m_history.Peek())
			{
				Navigation.m_history.Pop();
			}
			else if (Navigation.m_history.Contains(navigateBackHandler))
			{
				Log.All.PrintWarning("Navigation tried to remove handler and failed, but the handler exists further down the stack! Perhaps something went wrong, like a new scene added itself to the top of the stack in its Awake? Handler to remove: {0}", new object[]
				{
					Navigation.StackEntryToString(navigateBackHandler)
				});
			}
			if (Navigation.NAVIGATION_DEBUG)
			{
				Navigation.DumpStack();
			}
			return true;
		}
		return false;
	}

	// Token: 0x0600530E RID: 21262 RVA: 0x001B231F File Offset: 0x001B051F
	public static void Push(Navigation.NavigateBackHandler handler)
	{
		if (handler == null)
		{
			return;
		}
		Navigation.m_history.Push(handler);
		if (Navigation.NAVIGATION_DEBUG)
		{
			Navigation.DumpStack();
		}
	}

	// Token: 0x0600530F RID: 21263 RVA: 0x001B233C File Offset: 0x001B053C
	public static void PushUnique(Navigation.NavigateBackHandler handler)
	{
		if (handler == null)
		{
			return;
		}
		if (Navigation.m_history.Contains(handler))
		{
			return;
		}
		if (!handler.Method.IsStatic)
		{
			Debug.LogWarningFormat("Navigation.PushUnique called for non-static method! - {0}", new object[]
			{
				handler.Method.Name
			});
		}
		Navigation.m_history.Push(handler);
		if (Navigation.NAVIGATION_DEBUG)
		{
			Navigation.DumpStack();
		}
	}

	// Token: 0x06005310 RID: 21264 RVA: 0x001B23A0 File Offset: 0x001B05A0
	public static void PushIfNotOnTop(Navigation.NavigateBackHandler handler)
	{
		if (handler == null)
		{
			return;
		}
		if (Navigation.m_history.Count > 0 && Navigation.m_history.Peek() == handler)
		{
			if (Navigation.NAVIGATION_DEBUG)
			{
				Debug.LogFormat("Navigation - Did not push {0}, it already exists on the top of the stack!", new object[]
				{
					Navigation.StackEntryToString(handler)
				});
			}
			return;
		}
		Navigation.m_history.Push(handler);
		if (Navigation.NAVIGATION_DEBUG)
		{
			Navigation.DumpStack();
		}
	}

	// Token: 0x06005311 RID: 21265 RVA: 0x001B2408 File Offset: 0x001B0608
	public static void Pop()
	{
		if (Navigation.IsEmpty || !Navigation.CanNavigate())
		{
			return;
		}
		Navigation.m_history.Pop();
		if (Navigation.NAVIGATION_DEBUG)
		{
			Navigation.DumpStack();
		}
	}

	// Token: 0x06005312 RID: 21266 RVA: 0x001B2430 File Offset: 0x001B0630
	public static bool RemoveHandler(Navigation.NavigateBackHandler handler)
	{
		if (Navigation.IsEmpty)
		{
			return false;
		}
		bool flag = Navigation.m_history.Contains(handler);
		if (flag)
		{
			Navigation.m_history = new Stack<Navigation.NavigateBackHandler>((from h in Navigation.m_history
			where h != handler
			select h).Reverse<Navigation.NavigateBackHandler>());
		}
		if (Navigation.NAVIGATION_DEBUG)
		{
			Navigation.DumpStack();
		}
		return flag;
	}

	// Token: 0x06005313 RID: 21267 RVA: 0x001B2497 File Offset: 0x001B0697
	public static bool BackStackContainsHandler(Navigation.NavigateBackHandler handler)
	{
		return Navigation.m_history.Contains(handler);
	}

	// Token: 0x06005314 RID: 21268 RVA: 0x001B24A4 File Offset: 0x001B06A4
	public static void PushBlockBackingOut()
	{
		Navigation.Push(new Navigation.NavigateBackHandler(Navigation.BlockBackingOut));
	}

	// Token: 0x06005315 RID: 21269 RVA: 0x001B24B7 File Offset: 0x001B06B7
	public static void PopBlockBackingOut()
	{
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(Navigation.BlockBackingOut));
	}

	// Token: 0x06005316 RID: 21270 RVA: 0x0001FA65 File Offset: 0x0001DC65
	private static bool BlockBackingOut()
	{
		return false;
	}

	// Token: 0x06005317 RID: 21271 RVA: 0x001B24CC File Offset: 0x001B06CC
	private static bool CanNavigate()
	{
		if (GameUtils.IsAnyTransitionActive() && !Navigation.IsWebLoginActive())
		{
			return false;
		}
		FindGameState findGameState = GameMgr.Get().GetFindGameState();
		return findGameState - FindGameState.CLIENT_STARTED > 2 && findGameState - FindGameState.BNET_QUEUE_CANCELED > 4;
	}

	// Token: 0x06005318 RID: 21272 RVA: 0x001B2503 File Offset: 0x001B0703
	private static bool IsWebLoginActive()
	{
		return WebAuthDisplay.IsWebLoginCanvasActive();
	}

	// Token: 0x17000517 RID: 1303
	// (get) Token: 0x06005319 RID: 21273 RVA: 0x001B250C File Offset: 0x001B070C
	public static string StackDumpString
	{
		get
		{
			int count = 0;
			return string.Join("\n", Navigation.m_history.Select(delegate(Navigation.NavigateBackHandler entry)
			{
				string format = "{0}: {1}";
				int count = count;
				count++;
				return string.Format(format, count, Navigation.StackEntryToString(entry));
			}).ToArray<string>());
		}
	}

	// Token: 0x0600531A RID: 21274 RVA: 0x001B254C File Offset: 0x001B074C
	private static string StackEntryToString(Navigation.NavigateBackHandler entry)
	{
		return string.Format("{0}.{1} Target={2}", entry.Method.DeclaringType, entry.Method.Name, (entry == null || entry.Target == null) ? (entry.Method.IsStatic ? "<static>" : "null") : entry.Target.ToString());
	}

	// Token: 0x0600531B RID: 21275 RVA: 0x001B25AC File Offset: 0x001B07AC
	public static void DumpStack()
	{
		Debug.Log(string.Format("Navigation Stack Dump (count: {0})\n", Navigation.m_history.Count));
		int num = 0;
		foreach (Navigation.NavigateBackHandler entry in Navigation.m_history)
		{
			Debug.Log(string.Format("{0}: {1}\n", num, Navigation.StackEntryToString(entry)));
			num++;
		}
	}

	// Token: 0x0600531C RID: 21276 RVA: 0x001B2638 File Offset: 0x001B0838
	public static void GoBackUntilOnNavigateBackCalled(Navigation.NavigateBackHandler handler)
	{
		while (Navigation.BackStackContainsHandler(handler) && Navigation.GoBack())
		{
		}
	}

	// Token: 0x040049C9 RID: 18889
	private static Stack<Navigation.NavigateBackHandler> m_history = new Stack<Navigation.NavigateBackHandler>();

	// Token: 0x02002027 RID: 8231
	// (Invoke) Token: 0x06011C46 RID: 72774
	public delegate bool NavigateBackHandler();
}
