using System;

// Token: 0x020009ED RID: 2541
public class ReactiveEnumOption<T> : ReactiveOption<T> where T : struct, IConvertible
{
	// Token: 0x0600897C RID: 35196 RVA: 0x002C227A File Offset: 0x002C047A
	public ReactiveEnumOption(Option val) : base(val)
	{
		if (!typeof(T).IsEnum)
		{
			throw new Exception("T must be an enumerated type");
		}
	}

	// Token: 0x0600897D RID: 35197 RVA: 0x002C22A0 File Offset: 0x002C04A0
	public static ReactiveEnumOption<T> CreateInstance(Option opt)
	{
		ReactiveObject<T> reactiveObject = ReactiveObject<T>.GetExistingInstance(ReactiveOption<T>.GetOptionId(opt));
		if (reactiveObject == null)
		{
			reactiveObject = new ReactiveEnumOption<T>(opt);
		}
		return reactiveObject as ReactiveEnumOption<T>;
	}

	// Token: 0x0600897E RID: 35198 RVA: 0x002C22C9 File Offset: 0x002C04C9
	protected override T DoFetchValue()
	{
		return Options.Get().GetEnum<T>(this.m_option);
	}

	// Token: 0x0600897F RID: 35199 RVA: 0x002C22DB File Offset: 0x002C04DB
	public override void Set(T newValue)
	{
		Options.Get().SetEnum<T>(this.m_option, newValue);
	}
}
