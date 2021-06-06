using System;
using agent;
using UnityEngine;

internal class Unsubscriber<ICallbackHandler> : IDisposable
{
	private agent.ICallbackHandler _listener;

	internal Unsubscriber(agent.ICallbackHandler listener)
	{
		_listener = listener;
	}

	public void Dispose()
	{
		lock (AgentEmbeddedAPI.s_listeners)
		{
			int count = AgentEmbeddedAPI.s_listeners.Count;
			AgentEmbeddedAPI.s_listeners.Remove(_listener);
			int count2 = AgentEmbeddedAPI.s_listeners.Count;
			if (count == count2)
			{
				Debug.LogFormat("Attempted to remove an AgentEmbeddedAPI listener that is not Subscribed. {0} listeners currently Subscribed", count2);
			}
			else
			{
				Debug.LogFormat("Remove a Subscribed AgentEmbeddedAPI listener. {0} listeners still Subscribed", count2);
			}
		}
	}
}
