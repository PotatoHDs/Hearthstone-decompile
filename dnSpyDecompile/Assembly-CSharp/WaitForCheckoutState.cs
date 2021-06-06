using System;
using System.Text;
using Blizzard.T5.Jobs;

// Token: 0x020008D5 RID: 2261
public class WaitForCheckoutState : IJobDependency, IAsyncJobResult
{
	// Token: 0x06007D6C RID: 32108 RVA: 0x0028B98C File Offset: 0x00289B8C
	public WaitForCheckoutState(params HearthstoneCheckout.State[] allowListedStates)
	{
		if (allowListedStates == null)
		{
			Log.Jobs.PrintWarning("Cannot use WaitForCheckoutState with a null state array!  Ignoring dependency...", Array.Empty<object>());
			return;
		}
		if (allowListedStates.Length == 0)
		{
			Log.Jobs.PrintWarning("Cannot use WaitForCheckoutState with an empty state array!  Ignoring dependency...", Array.Empty<object>());
		}
		this.m_allowListedStates = allowListedStates;
	}

	// Token: 0x06007D6D RID: 32109 RVA: 0x0028B9CC File Offset: 0x00289BCC
	public bool IsReady()
	{
		if (this.m_allowListedStates == null || this.m_allowListedStates.Length == 0)
		{
			return true;
		}
		if (!HearthstoneServices.IsAvailable<HearthstoneCheckout>())
		{
			return false;
		}
		HearthstoneCheckout hearthstoneCheckout = HearthstoneServices.Get<HearthstoneCheckout>();
		return Array.IndexOf<HearthstoneCheckout.State>(this.m_allowListedStates, hearthstoneCheckout.CurrentState) >= 0;
	}

	// Token: 0x06007D6E RID: 32110 RVA: 0x0028BA14 File Offset: 0x00289C14
	public override string ToString()
	{
		if (string.IsNullOrEmpty(this.m_cachedToString))
		{
			if (WaitForCheckoutState.s_stateList == null)
			{
				WaitForCheckoutState.s_stateList = new StringBuilder();
			}
			else
			{
				WaitForCheckoutState.s_stateList.Length = 0;
			}
			if (this.m_allowListedStates == null || this.m_allowListedStates.Length == 0)
			{
				WaitForCheckoutState.s_stateList.Append("None");
			}
			else
			{
				WaitForCheckoutState.s_stateList.Append(this.m_allowListedStates[0]);
				for (int i = 1; i < this.m_allowListedStates.Length; i++)
				{
					WaitForCheckoutState.s_stateList.Append(", ");
					WaitForCheckoutState.s_stateList.Append(this.m_allowListedStates[i]);
				}
			}
			this.m_cachedToString = string.Format("WaitForCheckoutState: {0}", WaitForCheckoutState.s_stateList);
		}
		return this.m_cachedToString;
	}

	// Token: 0x040065B4 RID: 26036
	private HearthstoneCheckout.State[] m_allowListedStates;

	// Token: 0x040065B5 RID: 26037
	private static StringBuilder s_stateList;

	// Token: 0x040065B6 RID: 26038
	private string m_cachedToString;
}
