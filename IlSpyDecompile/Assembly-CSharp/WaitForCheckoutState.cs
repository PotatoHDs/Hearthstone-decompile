using System;
using System.Text;
using Blizzard.T5.Jobs;

public class WaitForCheckoutState : IJobDependency, IAsyncJobResult
{
	private HearthstoneCheckout.State[] m_allowListedStates;

	private static StringBuilder s_stateList;

	private string m_cachedToString;

	public WaitForCheckoutState(params HearthstoneCheckout.State[] allowListedStates)
	{
		if (allowListedStates == null)
		{
			Log.Jobs.PrintWarning("Cannot use WaitForCheckoutState with a null state array!  Ignoring dependency...");
			return;
		}
		if (allowListedStates.Length == 0)
		{
			Log.Jobs.PrintWarning("Cannot use WaitForCheckoutState with an empty state array!  Ignoring dependency...");
		}
		m_allowListedStates = allowListedStates;
	}

	public bool IsReady()
	{
		if (m_allowListedStates == null || m_allowListedStates.Length == 0)
		{
			return true;
		}
		if (!HearthstoneServices.IsAvailable<HearthstoneCheckout>())
		{
			return false;
		}
		HearthstoneCheckout hearthstoneCheckout = HearthstoneServices.Get<HearthstoneCheckout>();
		return Array.IndexOf(m_allowListedStates, hearthstoneCheckout.CurrentState) >= 0;
	}

	public override string ToString()
	{
		if (string.IsNullOrEmpty(m_cachedToString))
		{
			if (s_stateList == null)
			{
				s_stateList = new StringBuilder();
			}
			else
			{
				s_stateList.Length = 0;
			}
			if (m_allowListedStates == null || m_allowListedStates.Length == 0)
			{
				s_stateList.Append("None");
			}
			else
			{
				s_stateList.Append(m_allowListedStates[0]);
				for (int i = 1; i < m_allowListedStates.Length; i++)
				{
					s_stateList.Append(", ");
					s_stateList.Append(m_allowListedStates[i]);
				}
			}
			m_cachedToString = $"WaitForCheckoutState: {s_stateList}";
		}
		return m_cachedToString;
	}
}
