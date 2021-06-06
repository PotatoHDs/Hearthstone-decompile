using System;

// Token: 0x020009EA RID: 2538
public class ReactiveLongOption : ReactiveOption<long>
{
	// Token: 0x06008970 RID: 35184 RVA: 0x002C2172 File Offset: 0x002C0372
	public ReactiveLongOption(Option val) : base(val)
	{
	}

	// Token: 0x06008971 RID: 35185 RVA: 0x002C217C File Offset: 0x002C037C
	public static ReactiveLongOption CreateInstance(Option opt)
	{
		ReactiveObject<long> reactiveObject = ReactiveObject<long>.GetExistingInstance(ReactiveOption<long>.GetOptionId(opt));
		if (reactiveObject == null)
		{
			reactiveObject = new ReactiveLongOption(opt);
		}
		return reactiveObject as ReactiveLongOption;
	}

	// Token: 0x06008972 RID: 35186 RVA: 0x002C21A5 File Offset: 0x002C03A5
	protected override long DoFetchValue()
	{
		return Options.Get().GetLong(this.m_option);
	}

	// Token: 0x06008973 RID: 35187 RVA: 0x002C21B7 File Offset: 0x002C03B7
	public override void Set(long newValue)
	{
		Options.Get().SetLong(this.m_option, newValue);
	}
}
