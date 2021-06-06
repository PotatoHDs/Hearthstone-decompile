using System;
using System.Collections.Generic;

public abstract class ReactiveOption<T> : ReactiveObject<T>
{
	private static Dictionary<Option, Guid> s_guids;

	protected Option m_option;

	public abstract void Set(T value);

	protected ReactiveOption(Option opt)
		: base(GetOptionId(opt))
	{
		m_option = opt;
	}

	protected override T FetchValue()
	{
		if (m_option == Option.INVALID)
		{
			return default(T);
		}
		return DoFetchValue();
	}

	protected override bool RegisterChangeCallback()
	{
		if (m_option == Option.INVALID)
		{
			return false;
		}
		Options.Get().RegisterChangedListener(m_option, delegate
		{
			OnObjectChanged();
		});
		return true;
	}

	protected static Guid GetOptionId(Option opt)
	{
		if (s_guids == null)
		{
			s_guids = new Dictionary<Option, Guid>();
		}
		if (!s_guids.TryGetValue(opt, out var value))
		{
			value = Guid.NewGuid();
			s_guids.Add(opt, value);
		}
		return value;
	}

	protected abstract T DoFetchValue();
}
