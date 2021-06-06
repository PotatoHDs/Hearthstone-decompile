using System;

// Token: 0x020009E9 RID: 2537
public class ReactiveIntOption : ReactiveOption<int>
{
	// Token: 0x0600896C RID: 35180 RVA: 0x002C211A File Offset: 0x002C031A
	public ReactiveIntOption(Option val) : base(val)
	{
	}

	// Token: 0x0600896D RID: 35181 RVA: 0x002C2124 File Offset: 0x002C0324
	public static ReactiveIntOption CreateInstance(Option opt)
	{
		ReactiveObject<int> reactiveObject = ReactiveObject<int>.GetExistingInstance(ReactiveOption<int>.GetOptionId(opt));
		if (reactiveObject == null)
		{
			reactiveObject = new ReactiveIntOption(opt);
		}
		return reactiveObject as ReactiveIntOption;
	}

	// Token: 0x0600896E RID: 35182 RVA: 0x002C214D File Offset: 0x002C034D
	protected override int DoFetchValue()
	{
		return Options.Get().GetInt(this.m_option);
	}

	// Token: 0x0600896F RID: 35183 RVA: 0x002C215F File Offset: 0x002C035F
	public override void Set(int newValue)
	{
		Options.Get().SetInt(this.m_option, newValue);
	}
}
