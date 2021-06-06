using System;
using System.Collections.Generic;

public class ReactiveObjectManager
{
	private static ReactiveObjectManager s_instance;

	private Dictionary<Guid, ReactiveObject> m_entries;

	public static ReactiveObjectManager Get()
	{
		if (s_instance == null)
		{
			s_instance = new ReactiveObjectManager();
		}
		return s_instance;
	}

	public void RegisterReactiveObject(ReactiveObject robj, Guid id)
	{
		m_entries.Add(id, robj);
	}

	public ReactiveObject GetReactiveObjectById(Guid id)
	{
		ReactiveObject value = null;
		m_entries.TryGetValue(id, out value);
		return value;
	}

	private ReactiveObjectManager()
	{
		m_entries = new Dictionary<Guid, ReactiveObject>();
	}
}
