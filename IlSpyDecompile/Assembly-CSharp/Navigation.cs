using System.Collections.Generic;
using System.Linq;
using Hearthstone.Login;
using UnityEngine;

public static class Navigation
{
	public delegate bool NavigateBackHandler();

	private static Stack<NavigateBackHandler> m_history = new Stack<NavigateBackHandler>();

	public static bool NAVIGATION_DEBUG
	{
		get
		{
			return Vars.Key("Application.Navigation.Debug").GetBool(def: false);
		}
		set
		{
			Vars.Key("Application.Navigation.Debug").Set(value.ToString(), permanent: false);
		}
	}

	public static bool IsEmpty => m_history.Count == 0;

	public static bool CanGoBack
	{
		get
		{
			if (IsEmpty || !CanNavigate())
			{
				return false;
			}
			return true;
		}
	}

	public static string StackDumpString
	{
		get
		{
			int count = 0;
			return string.Join("\n", m_history.Select((NavigateBackHandler entry) => $"{count++}: {StackEntryToString(entry)}").ToArray());
		}
	}

	public static void Clear()
	{
		m_history.Clear();
		if (NAVIGATION_DEBUG)
		{
			DumpStack();
		}
	}

	public static bool GoBack()
	{
		if (!CanGoBack)
		{
			return false;
		}
		NavigateBackHandler navigateBackHandler = m_history.Peek();
		if (navigateBackHandler())
		{
			if (m_history.Count > 0 && navigateBackHandler == m_history.Peek())
			{
				m_history.Pop();
			}
			else if (m_history.Contains(navigateBackHandler))
			{
				Log.All.PrintWarning("Navigation tried to remove handler and failed, but the handler exists further down the stack! Perhaps something went wrong, like a new scene added itself to the top of the stack in its Awake? Handler to remove: {0}", StackEntryToString(navigateBackHandler));
			}
			if (NAVIGATION_DEBUG)
			{
				DumpStack();
			}
			return true;
		}
		return false;
	}

	public static void Push(NavigateBackHandler handler)
	{
		if (handler != null)
		{
			m_history.Push(handler);
			if (NAVIGATION_DEBUG)
			{
				DumpStack();
			}
		}
	}

	public static void PushUnique(NavigateBackHandler handler)
	{
		if (handler != null && !m_history.Contains(handler))
		{
			if (!handler.Method.IsStatic)
			{
				Debug.LogWarningFormat("Navigation.PushUnique called for non-static method! - {0}", handler.Method.Name);
			}
			m_history.Push(handler);
			if (NAVIGATION_DEBUG)
			{
				DumpStack();
			}
		}
	}

	public static void PushIfNotOnTop(NavigateBackHandler handler)
	{
		if (handler == null)
		{
			return;
		}
		if (m_history.Count > 0 && m_history.Peek() == handler)
		{
			if (NAVIGATION_DEBUG)
			{
				Debug.LogFormat("Navigation - Did not push {0}, it already exists on the top of the stack!", StackEntryToString(handler));
			}
		}
		else
		{
			m_history.Push(handler);
			if (NAVIGATION_DEBUG)
			{
				DumpStack();
			}
		}
	}

	public static void Pop()
	{
		if (!IsEmpty && CanNavigate())
		{
			m_history.Pop();
			if (NAVIGATION_DEBUG)
			{
				DumpStack();
			}
		}
	}

	public static bool RemoveHandler(NavigateBackHandler handler)
	{
		if (IsEmpty)
		{
			return false;
		}
		bool num = m_history.Contains(handler);
		if (num)
		{
			m_history = new Stack<NavigateBackHandler>(m_history.Where((NavigateBackHandler h) => h != handler).Reverse());
		}
		if (NAVIGATION_DEBUG)
		{
			DumpStack();
		}
		return num;
	}

	public static bool BackStackContainsHandler(NavigateBackHandler handler)
	{
		return m_history.Contains(handler);
	}

	public static void PushBlockBackingOut()
	{
		Push(BlockBackingOut);
	}

	public static void PopBlockBackingOut()
	{
		RemoveHandler(BlockBackingOut);
	}

	private static bool BlockBackingOut()
	{
		return false;
	}

	private static bool CanNavigate()
	{
		if (GameUtils.IsAnyTransitionActive() && !IsWebLoginActive())
		{
			return false;
		}
		FindGameState findGameState = GameMgr.Get().GetFindGameState();
		if ((uint)(findGameState - 1) <= 2u || (uint)(findGameState - 7) <= 4u)
		{
			return false;
		}
		return true;
	}

	private static bool IsWebLoginActive()
	{
		return WebAuthDisplay.IsWebLoginCanvasActive();
	}

	private static string StackEntryToString(NavigateBackHandler entry)
	{
		return string.Format("{0}.{1} Target={2}", entry.Method.DeclaringType, entry.Method.Name, (entry != null && entry.Target != null) ? entry.Target.ToString() : (entry.Method.IsStatic ? "<static>" : "null"));
	}

	public static void DumpStack()
	{
		Debug.Log($"Navigation Stack Dump (count: {m_history.Count})\n");
		int num = 0;
		foreach (NavigateBackHandler item in m_history)
		{
			Debug.Log($"{num}: {StackEntryToString(item)}\n");
			num++;
		}
	}

	public static void GoBackUntilOnNavigateBackCalled(NavigateBackHandler handler)
	{
		while (BackStackContainsHandler(handler) && GoBack())
		{
		}
	}
}
