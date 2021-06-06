using System;

// Token: 0x020009E8 RID: 2536
public class ReactiveFloatOption : ReactiveOption<float>
{
	// Token: 0x06008968 RID: 35176 RVA: 0x002C20C2 File Offset: 0x002C02C2
	public ReactiveFloatOption(Option val) : base(val)
	{
	}

	// Token: 0x06008969 RID: 35177 RVA: 0x002C20CC File Offset: 0x002C02CC
	public static ReactiveFloatOption CreateInstance(Option opt)
	{
		ReactiveObject<float> reactiveObject = ReactiveObject<float>.GetExistingInstance(ReactiveOption<float>.GetOptionId(opt));
		if (reactiveObject == null)
		{
			reactiveObject = new ReactiveFloatOption(opt);
		}
		return reactiveObject as ReactiveFloatOption;
	}

	// Token: 0x0600896A RID: 35178 RVA: 0x002C20F5 File Offset: 0x002C02F5
	protected override float DoFetchValue()
	{
		return Options.Get().GetFloat(this.m_option);
	}

	// Token: 0x0600896B RID: 35179 RVA: 0x002C2107 File Offset: 0x002C0307
	public override void Set(float newValue)
	{
		Options.Get().SetFloat(this.m_option, newValue);
	}
}
