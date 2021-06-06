using System;

// Token: 0x020009EC RID: 2540
public class ReactiveStringOption : ReactiveOption<string>
{
	// Token: 0x06008978 RID: 35192 RVA: 0x002C2222 File Offset: 0x002C0422
	public ReactiveStringOption(Option val) : base(val)
	{
	}

	// Token: 0x06008979 RID: 35193 RVA: 0x002C222C File Offset: 0x002C042C
	public static ReactiveStringOption CreateInstance(Option opt)
	{
		ReactiveObject<string> reactiveObject = ReactiveObject<string>.GetExistingInstance(ReactiveOption<string>.GetOptionId(opt));
		if (reactiveObject == null)
		{
			reactiveObject = new ReactiveStringOption(opt);
		}
		return reactiveObject as ReactiveStringOption;
	}

	// Token: 0x0600897A RID: 35194 RVA: 0x002C2255 File Offset: 0x002C0455
	protected override string DoFetchValue()
	{
		return Options.Get().GetString(this.m_option);
	}

	// Token: 0x0600897B RID: 35195 RVA: 0x002C2267 File Offset: 0x002C0467
	public override void Set(string newValue)
	{
		Options.Get().SetString(this.m_option, newValue);
	}
}
