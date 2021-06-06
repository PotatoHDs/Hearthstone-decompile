public class ReactiveIntOption : ReactiveOption<int>
{
	public ReactiveIntOption(Option val)
		: base(val)
	{
	}

	public static ReactiveIntOption CreateInstance(Option opt)
	{
		ReactiveObject<int> reactiveObject = ReactiveObject<int>.GetExistingInstance(ReactiveOption<int>.GetOptionId(opt));
		if (reactiveObject == null)
		{
			reactiveObject = new ReactiveIntOption(opt);
		}
		return reactiveObject as ReactiveIntOption;
	}

	protected override int DoFetchValue()
	{
		return Options.Get().GetInt(m_option);
	}

	public override void Set(int newValue)
	{
		Options.Get().SetInt(m_option, newValue);
	}
}
