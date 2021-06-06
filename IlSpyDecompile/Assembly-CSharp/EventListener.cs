public class EventListener<Delegate>
{
	protected Delegate m_callback;

	protected object m_userData;

	public override bool Equals(object obj)
	{
		EventListener<Delegate> eventListener = obj as EventListener<Delegate>;
		if (eventListener == null)
		{
			return base.Equals(obj);
		}
		if (m_callback.Equals(eventListener.m_callback))
		{
			return m_userData == eventListener.m_userData;
		}
		return false;
	}

	public override int GetHashCode()
	{
		int num = 23;
		if (m_callback != null)
		{
			num = num * 17 + m_callback.GetHashCode();
		}
		if (m_userData != null)
		{
			num = num * 17 + m_userData.GetHashCode();
		}
		return num;
	}

	public EventListener()
	{
	}

	public EventListener(Delegate callback, object userData)
	{
		m_callback = callback;
		m_userData = userData;
	}

	public Delegate GetCallback()
	{
		return m_callback;
	}

	public void SetCallback(Delegate callback)
	{
		m_callback = callback;
	}

	public object GetUserData()
	{
		return m_userData;
	}

	public void SetUserData(object userData)
	{
		m_userData = userData;
	}
}
