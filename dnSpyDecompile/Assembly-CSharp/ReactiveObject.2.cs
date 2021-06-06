using System;

// Token: 0x020009E4 RID: 2532
public abstract class ReactiveObject<T> : ReactiveObject
{
	// Token: 0x170007BB RID: 1979
	// (get) Token: 0x0600894D RID: 35149 RVA: 0x002C1E5D File Offset: 0x002C005D
	public T Value
	{
		get
		{
			if (!this.m_init)
			{
				this.Init();
			}
			return this.m_value;
		}
	}

	// Token: 0x0600894E RID: 35150 RVA: 0x002C1E73 File Offset: 0x002C0073
	public void Init()
	{
		if (!this.m_init)
		{
			this.SetValue(this.FetchValue());
		}
		if (!this.m_registeredCallbacks)
		{
			this.RegisterCallbacks();
		}
	}

	// Token: 0x0600894F RID: 35151 RVA: 0x002C1E97 File Offset: 0x002C0097
	protected ReactiveObject() : this(ReactiveObject<T>.GetId())
	{
	}

	// Token: 0x06008950 RID: 35152 RVA: 0x002C1EA4 File Offset: 0x002C00A4
	protected ReactiveObject(Guid guid)
	{
		ReactiveObjectManager.Get().RegisterReactiveObject(this, guid);
	}

	// Token: 0x06008951 RID: 35153 RVA: 0x002C1EB8 File Offset: 0x002C00B8
	protected static ReactiveObject<T> GetExistingInstance()
	{
		return ReactiveObject<T>.GetExistingInstance(ReactiveObject<T>.GetId());
	}

	// Token: 0x06008952 RID: 35154 RVA: 0x002C1EC4 File Offset: 0x002C00C4
	protected static ReactiveObject<T> GetExistingInstance(Guid guid)
	{
		return ReactiveObjectManager.Get().GetReactiveObjectById(guid) as ReactiveObject<T>;
	}

	// Token: 0x06008953 RID: 35155
	protected abstract T FetchValue();

	// Token: 0x06008954 RID: 35156
	protected abstract bool RegisterChangeCallback();

	// Token: 0x06008955 RID: 35157 RVA: 0x002C1ED8 File Offset: 0x002C00D8
	protected void OnObjectChanged()
	{
		T value = this.FetchValue();
		this.SetValue(value);
	}

	// Token: 0x06008956 RID: 35158 RVA: 0x002C1EF3 File Offset: 0x002C00F3
	protected static Guid GetId()
	{
		if (ReactiveObject<T>.s_guid == Guid.Empty)
		{
			ReactiveObject<T>.s_guid = Guid.NewGuid();
		}
		return ReactiveObject<T>.s_guid;
	}

	// Token: 0x06008957 RID: 35159 RVA: 0x002C1F15 File Offset: 0x002C0115
	private void SetValue(T val)
	{
		this.m_value = val;
		if (this.m_value != null && !this.m_init)
		{
			this.m_init = true;
		}
		if (!this.m_registeredCallbacks)
		{
			this.RegisterCallbacks();
		}
	}

	// Token: 0x06008958 RID: 35160 RVA: 0x002C1F48 File Offset: 0x002C0148
	private void RegisterCallbacks()
	{
		if (!this.m_registeredCallbacks && this.RegisterChangeCallback())
		{
			this.m_registeredCallbacks = true;
		}
	}

	// Token: 0x0400733A RID: 29498
	private static Guid s_guid;

	// Token: 0x0400733B RID: 29499
	private T m_value;

	// Token: 0x0400733C RID: 29500
	private bool m_init;

	// Token: 0x0400733D RID: 29501
	private bool m_registeredCallbacks;
}
