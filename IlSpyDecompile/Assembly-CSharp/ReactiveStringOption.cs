public class ReactiveStringOption : ReactiveOption<string>
{
	public ReactiveStringOption(Option val)
		: base(val)
	{
	}

	public static ReactiveStringOption CreateInstance(Option opt)
	{
		ReactiveObject<string> reactiveObject = ReactiveObject<string>.GetExistingInstance(ReactiveOption<string>.GetOptionId(opt));
		if (reactiveObject == null)
		{
			reactiveObject = new ReactiveStringOption(opt);
		}
		return reactiveObject as ReactiveStringOption;
	}

	protected override string DoFetchValue()
	{
		return Options.Get().GetString(m_option);
	}

	public override void Set(string newValue)
	{
		Options.Get().SetString(m_option, newValue);
	}
}
