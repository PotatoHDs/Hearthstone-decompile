public class ReactiveLongOption : ReactiveOption<long>
{
	public ReactiveLongOption(Option val)
		: base(val)
	{
	}

	public static ReactiveLongOption CreateInstance(Option opt)
	{
		ReactiveObject<long> reactiveObject = ReactiveObject<long>.GetExistingInstance(ReactiveOption<long>.GetOptionId(opt));
		if (reactiveObject == null)
		{
			reactiveObject = new ReactiveLongOption(opt);
		}
		return reactiveObject as ReactiveLongOption;
	}

	protected override long DoFetchValue()
	{
		return Options.Get().GetLong(m_option);
	}

	public override void Set(long newValue)
	{
		Options.Get().SetLong(m_option, newValue);
	}
}
