public class ReactiveFloatOption : ReactiveOption<float>
{
	public ReactiveFloatOption(Option val)
		: base(val)
	{
	}

	public static ReactiveFloatOption CreateInstance(Option opt)
	{
		ReactiveObject<float> reactiveObject = ReactiveObject<float>.GetExistingInstance(ReactiveOption<float>.GetOptionId(opt));
		if (reactiveObject == null)
		{
			reactiveObject = new ReactiveFloatOption(opt);
		}
		return reactiveObject as ReactiveFloatOption;
	}

	protected override float DoFetchValue()
	{
		return Options.Get().GetFloat(m_option);
	}

	public override void Set(float newValue)
	{
		Options.Get().SetFloat(m_option, newValue);
	}
}
