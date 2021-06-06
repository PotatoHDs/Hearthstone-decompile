using System;

// Token: 0x020009EB RID: 2539
public class ReactiveULongOption : ReactiveOption<ulong>
{
	// Token: 0x06008974 RID: 35188 RVA: 0x002C21CA File Offset: 0x002C03CA
	public ReactiveULongOption(Option val) : base(val)
	{
	}

	// Token: 0x06008975 RID: 35189 RVA: 0x002C21D4 File Offset: 0x002C03D4
	public static ReactiveULongOption CreateInstance(Option opt)
	{
		ReactiveObject<ulong> reactiveObject = ReactiveObject<ulong>.GetExistingInstance(ReactiveOption<ulong>.GetOptionId(opt));
		if (reactiveObject == null)
		{
			reactiveObject = new ReactiveULongOption(opt);
		}
		return reactiveObject as ReactiveULongOption;
	}

	// Token: 0x06008976 RID: 35190 RVA: 0x002C21FD File Offset: 0x002C03FD
	protected override ulong DoFetchValue()
	{
		return Options.Get().GetULong(this.m_option);
	}

	// Token: 0x06008977 RID: 35191 RVA: 0x002C220F File Offset: 0x002C040F
	public override void Set(ulong newValue)
	{
		Options.Get().SetULong(this.m_option, newValue);
	}
}
