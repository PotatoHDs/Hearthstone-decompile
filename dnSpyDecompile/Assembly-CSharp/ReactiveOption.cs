using System;
using System.Collections.Generic;

// Token: 0x020009E6 RID: 2534
public abstract class ReactiveOption<T> : ReactiveObject<T>
{
	// Token: 0x0600895D RID: 35165
	public abstract void Set(T value);

	// Token: 0x0600895E RID: 35166 RVA: 0x002C1FBA File Offset: 0x002C01BA
	protected ReactiveOption(Option opt) : base(ReactiveOption<T>.GetOptionId(opt))
	{
		this.m_option = opt;
	}

	// Token: 0x0600895F RID: 35167 RVA: 0x002C1FD0 File Offset: 0x002C01D0
	protected override T FetchValue()
	{
		if (this.m_option == Option.INVALID)
		{
			return default(T);
		}
		return this.DoFetchValue();
	}

	// Token: 0x06008960 RID: 35168 RVA: 0x002C1FF5 File Offset: 0x002C01F5
	protected override bool RegisterChangeCallback()
	{
		if (this.m_option == Option.INVALID)
		{
			return false;
		}
		Options.Get().RegisterChangedListener(this.m_option, delegate(Option option, object value, bool existed, object data)
		{
			base.OnObjectChanged();
		});
		return true;
	}

	// Token: 0x06008961 RID: 35169 RVA: 0x002C2020 File Offset: 0x002C0220
	protected static Guid GetOptionId(Option opt)
	{
		if (ReactiveOption<T>.s_guids == null)
		{
			ReactiveOption<T>.s_guids = new Dictionary<Option, Guid>();
		}
		Guid guid;
		if (!ReactiveOption<T>.s_guids.TryGetValue(opt, out guid))
		{
			guid = Guid.NewGuid();
			ReactiveOption<T>.s_guids.Add(opt, guid);
		}
		return guid;
	}

	// Token: 0x06008962 RID: 35170
	protected abstract T DoFetchValue();

	// Token: 0x04007340 RID: 29504
	private static Dictionary<Option, Guid> s_guids;

	// Token: 0x04007341 RID: 29505
	protected Option m_option;
}
