using System;

public class ReactiveEnumOption<T> : ReactiveOption<T> where T : struct, IConvertible
{
	public ReactiveEnumOption(Option val)
		: base(val)
	{
		if (!typeof(T).IsEnum)
		{
			throw new Exception("T must be an enumerated type");
		}
	}

	public static ReactiveEnumOption<T> CreateInstance(Option opt)
	{
		ReactiveObject<T> reactiveObject = ReactiveObject<T>.GetExistingInstance(ReactiveOption<T>.GetOptionId(opt));
		if (reactiveObject == null)
		{
			reactiveObject = new ReactiveEnumOption<T>(opt);
		}
		return reactiveObject as ReactiveEnumOption<T>;
	}

	protected override T DoFetchValue()
	{
		return Options.Get().GetEnum<T>(m_option);
	}

	public override void Set(T newValue)
	{
		Options.Get().SetEnum(m_option, newValue);
	}
}
