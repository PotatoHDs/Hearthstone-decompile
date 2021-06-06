using System;

// Token: 0x020009E7 RID: 2535
public class ReactiveBoolOption : ReactiveOption<bool>
{
	// Token: 0x06008964 RID: 35172 RVA: 0x002C2068 File Offset: 0x002C0268
	private ReactiveBoolOption(Option val) : base(val)
	{
	}

	// Token: 0x06008965 RID: 35173 RVA: 0x002C2074 File Offset: 0x002C0274
	public static ReactiveBoolOption CreateInstance(Option opt)
	{
		ReactiveObject<bool> reactiveObject = ReactiveObject<bool>.GetExistingInstance(ReactiveOption<bool>.GetOptionId(opt));
		if (reactiveObject == null)
		{
			reactiveObject = new ReactiveBoolOption(opt);
		}
		return reactiveObject as ReactiveBoolOption;
	}

	// Token: 0x06008966 RID: 35174 RVA: 0x002C209D File Offset: 0x002C029D
	protected override bool DoFetchValue()
	{
		return Options.Get().GetBool(this.m_option);
	}

	// Token: 0x06008967 RID: 35175 RVA: 0x002C20AF File Offset: 0x002C02AF
	public override void Set(bool newValue)
	{
		Options.Get().SetBool(this.m_option, newValue);
	}
}
