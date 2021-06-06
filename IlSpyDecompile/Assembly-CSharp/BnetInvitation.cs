using bgs;
using bgs.types;

public class BnetInvitation
{
	private BnetInvitationId m_id;

	private BnetEntityId m_inviterId;

	private string m_inviterName;

	private BnetEntityId m_inviteeId;

	private string m_inviteeName;

	private string m_message;

	private ulong m_creationTimeMicrosec;

	private ulong m_expirationTimeMicrosec;

	public static BnetInvitation CreateFromFriendsUpdate(FriendsUpdate src)
	{
		BnetInvitation bnetInvitation = new BnetInvitation();
		bnetInvitation.m_id = new BnetInvitationId(src.long1);
		if (src.entity1 != null)
		{
			bnetInvitation.m_inviterId = src.entity1.Clone();
		}
		if (src.entity2 != null)
		{
			bnetInvitation.m_inviteeId = src.entity2.Clone();
		}
		bnetInvitation.m_inviterName = src.string1;
		bnetInvitation.m_inviteeName = src.string2;
		bnetInvitation.m_message = src.string3;
		bnetInvitation.m_creationTimeMicrosec = src.long2;
		bnetInvitation.m_expirationTimeMicrosec = src.long3;
		return bnetInvitation;
	}

	public BnetInvitationId GetId()
	{
		return m_id;
	}

	public void SetId(BnetInvitationId id)
	{
		m_id = id;
	}

	public BnetEntityId GetInviterId()
	{
		return m_inviterId;
	}

	public void SetInviterId(BnetEntityId id)
	{
		m_inviterId = id;
	}

	public string GetInviterName()
	{
		return m_inviterName;
	}

	public void SetInviterName(string name)
	{
		m_inviterName = name;
	}

	public BnetEntityId GetInviteeId()
	{
		return m_inviteeId;
	}

	public void SetInviteeId(BnetEntityId id)
	{
		m_inviteeId = id;
	}

	public string GetInviteeName()
	{
		return m_inviteeName;
	}

	public void SetInviteeName(string name)
	{
		m_inviteeName = name;
	}

	public string GetMessage()
	{
		return m_message;
	}

	public void SetMessage(string message)
	{
		m_message = message;
	}

	public ulong GetCreationTimeMicrosec()
	{
		return m_creationTimeMicrosec;
	}

	public void SetCreationTimeMicrosec(ulong microsec)
	{
		m_creationTimeMicrosec = microsec;
	}

	public ulong GetExpirationTimeMicrosec()
	{
		return m_expirationTimeMicrosec;
	}

	public void SetExpirationTimeMicroSec(ulong microsec)
	{
		m_expirationTimeMicrosec = microsec;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		BnetInvitation bnetInvitation = obj as BnetInvitation;
		if ((object)bnetInvitation == null)
		{
			return false;
		}
		return m_id.Equals(bnetInvitation.m_id);
	}

	public bool Equals(BnetInvitationId other)
	{
		if ((object)other == null)
		{
			return false;
		}
		return m_id.Equals(other);
	}

	public override int GetHashCode()
	{
		return m_id.GetHashCode();
	}

	public static bool operator ==(BnetInvitation a, BnetInvitation b)
	{
		if ((object)a == b)
		{
			return true;
		}
		if ((object)a == null || (object)b == null)
		{
			return false;
		}
		return a.m_id == b.m_id;
	}

	public static bool operator !=(BnetInvitation a, BnetInvitation b)
	{
		return !(a == b);
	}

	public override string ToString()
	{
		if (m_id == null)
		{
			return "UNKNOWN INVITATION";
		}
		return $"[id={m_id} inviterId={m_inviterId} inviterName={m_inviterName} inviteeId={m_inviteeId} inviteeName={m_inviteeName} message={m_message}]";
	}
}
