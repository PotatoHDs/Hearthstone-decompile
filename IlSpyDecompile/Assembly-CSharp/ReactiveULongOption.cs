public class ReactiveULongOption : ReactiveOption<ulong>
{
	public ReactiveULongOption(Option val)
		: base(val)
	{
	}

	public static ReactiveULongOption CreateInstance(Option opt)
	{
		ReactiveObject<ulong> reactiveObject = ReactiveObject<ulong>.GetExistingInstance(ReactiveOption<ulong>.GetOptionId(opt));
		if (reactiveObject == null)
		{
			reactiveObject = new ReactiveULongOption(opt);
		}
		return reactiveObject as ReactiveULongOption;
	}

	protected override ulong DoFetchValue()
	{
		return Options.Get().GetULong(m_option);
	}

	public override void Set(ulong newValue)
	{
		Options.Get().SetULong(m_option, newValue);
	}
}
