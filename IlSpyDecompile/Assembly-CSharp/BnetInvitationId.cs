public class BnetInvitationId
{
	private ulong m_val;

	public BnetInvitationId(ulong val)
	{
		m_val = val;
	}

	public ulong GetVal()
	{
		return m_val;
	}

	public void SetVal(ulong val)
	{
		m_val = val;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		BnetInvitationId bnetInvitationId = obj as BnetInvitationId;
		if ((object)bnetInvitationId == null)
		{
			return false;
		}
		return m_val == bnetInvitationId.m_val;
	}

	public bool Equals(BnetInvitationId other)
	{
		if ((object)other == null)
		{
			return false;
		}
		return m_val == other.m_val;
	}

	public override int GetHashCode()
	{
		return m_val.GetHashCode();
	}

	public static bool operator ==(BnetInvitationId a, BnetInvitationId b)
	{
		if ((object)a == b)
		{
			return true;
		}
		if ((object)a == null || (object)b == null)
		{
			return false;
		}
		return a.m_val == b.m_val;
	}

	public static bool operator !=(BnetInvitationId a, BnetInvitationId b)
	{
		return !(a == b);
	}

	public override string ToString()
	{
		return m_val.ToString();
	}
}
