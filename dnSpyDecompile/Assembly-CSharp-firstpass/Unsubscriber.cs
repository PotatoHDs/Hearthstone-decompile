using System;
using System.Collections.Generic;
using agent;
using UnityEngine;

// Token: 0x02000004 RID: 4
internal class Unsubscriber<ICallbackHandler> : IDisposable
{
	// Token: 0x06000007 RID: 7 RVA: 0x00002188 File Offset: 0x00000388
	internal Unsubscriber(agent.ICallbackHandler listener)
	{
		this._listener = listener;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002198 File Offset: 0x00000398
	public void Dispose()
	{
		List<agent.ICallbackHandler> s_listeners = AgentEmbeddedAPI.s_listeners;
		lock (s_listeners)
		{
			int count = AgentEmbeddedAPI.s_listeners.Count;
			AgentEmbeddedAPI.s_listeners.Remove(this._listener);
			int count2 = AgentEmbeddedAPI.s_listeners.Count;
			if (count == count2)
			{
				Debug.LogFormat("Attempted to remove an AgentEmbeddedAPI listener that is not Subscribed. {0} listeners currently Subscribed", new object[]
				{
					count2
				});
			}
			else
			{
				Debug.LogFormat("Remove a Subscribed AgentEmbeddedAPI listener. {0} listeners still Subscribed", new object[]
				{
					count2
				});
			}
		}
	}

	// Token: 0x0400000A RID: 10
	private agent.ICallbackHandler _listener;
}
