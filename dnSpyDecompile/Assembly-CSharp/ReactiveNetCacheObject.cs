using System;

// Token: 0x020009E2 RID: 2530
public class ReactiveNetCacheObject<T> : ReactiveObject<T> where T : new()
{
	// Token: 0x06008949 RID: 35145 RVA: 0x002C1DD4 File Offset: 0x002BFFD4
	private ReactiveNetCacheObject()
	{
	}

	// Token: 0x0600894A RID: 35146 RVA: 0x002C1DDC File Offset: 0x002BFFDC
	public static ReactiveNetCacheObject<T> CreateInstance()
	{
		ReactiveObject<T> reactiveObject = ReactiveObject<T>.GetExistingInstance();
		if (reactiveObject == null)
		{
			reactiveObject = new ReactiveNetCacheObject<T>();
		}
		return reactiveObject as ReactiveNetCacheObject<T>;
	}

	// Token: 0x0600894B RID: 35147 RVA: 0x002C1E00 File Offset: 0x002C0000
	protected override T FetchValue()
	{
		NetCache netCache = NetCache.Get();
		if (netCache == null)
		{
			return default(T);
		}
		return netCache.GetNetObject<T>();
	}

	// Token: 0x0600894C RID: 35148 RVA: 0x002C1E28 File Offset: 0x002C0028
	protected override bool RegisterChangeCallback()
	{
		NetCache netCache = NetCache.Get();
		if (netCache == null)
		{
			return false;
		}
		netCache.RegisterUpdatedListener(typeof(T), new Action(base.OnObjectChanged));
		return true;
	}
}
