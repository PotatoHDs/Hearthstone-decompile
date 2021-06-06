using System;

public interface ReactiveObject
{
}
public abstract class ReactiveObject<T> : ReactiveObject
{
	private static Guid s_guid;

	private T m_value;

	private bool m_init;

	private bool m_registeredCallbacks;

	public T Value
	{
		get
		{
			if (!m_init)
			{
				Init();
			}
			return m_value;
		}
	}

	public void Init()
	{
		if (!m_init)
		{
			SetValue(FetchValue());
		}
		if (!m_registeredCallbacks)
		{
			RegisterCallbacks();
		}
	}

	protected ReactiveObject()
		: this(GetId())
	{
	}

	protected ReactiveObject(Guid guid)
	{
		ReactiveObjectManager.Get().RegisterReactiveObject(this, guid);
	}

	protected static ReactiveObject<T> GetExistingInstance()
	{
		return GetExistingInstance(GetId());
	}

	protected static ReactiveObject<T> GetExistingInstance(Guid guid)
	{
		return ReactiveObjectManager.Get().GetReactiveObjectById(guid) as ReactiveObject<T>;
	}

	protected abstract T FetchValue();

	protected abstract bool RegisterChangeCallback();

	protected void OnObjectChanged()
	{
		T value = FetchValue();
		SetValue(value);
	}

	protected static Guid GetId()
	{
		if (s_guid == Guid.Empty)
		{
			s_guid = Guid.NewGuid();
		}
		return s_guid;
	}

	private void SetValue(T val)
	{
		m_value = val;
		if (m_value != null && !m_init)
		{
			m_init = true;
		}
		if (!m_registeredCallbacks)
		{
			RegisterCallbacks();
		}
	}

	private void RegisterCallbacks()
	{
		if (!m_registeredCallbacks && RegisterChangeCallback())
		{
			m_registeredCallbacks = true;
		}
	}
}
