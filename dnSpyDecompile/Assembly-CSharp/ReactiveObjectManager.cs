using System;
using System.Collections.Generic;

// Token: 0x020009E5 RID: 2533
public class ReactiveObjectManager
{
	// Token: 0x06008959 RID: 35161 RVA: 0x002C1F61 File Offset: 0x002C0161
	public static ReactiveObjectManager Get()
	{
		if (ReactiveObjectManager.s_instance == null)
		{
			ReactiveObjectManager.s_instance = new ReactiveObjectManager();
		}
		return ReactiveObjectManager.s_instance;
	}

	// Token: 0x0600895A RID: 35162 RVA: 0x002C1F79 File Offset: 0x002C0179
	public void RegisterReactiveObject(ReactiveObject robj, Guid id)
	{
		this.m_entries.Add(id, robj);
	}

	// Token: 0x0600895B RID: 35163 RVA: 0x002C1F88 File Offset: 0x002C0188
	public ReactiveObject GetReactiveObjectById(Guid id)
	{
		ReactiveObject result = null;
		this.m_entries.TryGetValue(id, out result);
		return result;
	}

	// Token: 0x0600895C RID: 35164 RVA: 0x002C1FA7 File Offset: 0x002C01A7
	private ReactiveObjectManager()
	{
		this.m_entries = new Dictionary<Guid, ReactiveObject>();
	}

	// Token: 0x0400733E RID: 29502
	private static ReactiveObjectManager s_instance;

	// Token: 0x0400733F RID: 29503
	private Dictionary<Guid, ReactiveObject> m_entries;
}
