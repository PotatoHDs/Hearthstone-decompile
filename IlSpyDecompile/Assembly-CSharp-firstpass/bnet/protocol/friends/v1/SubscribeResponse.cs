using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v1
{
	public class SubscribeResponse : IProtoBuf
	{
		public bool HasMaxFriends;

		private uint _MaxFriends;

		public bool HasMaxReceivedInvitations;

		private uint _MaxReceivedInvitations;

		public bool HasMaxSentInvitations;

		private uint _MaxSentInvitations;

		private List<Role> _Role = new List<Role>();

		private List<Friend> _Friends = new List<Friend>();

		private List<ReceivedInvitation> _ReceivedInvitations = new List<ReceivedInvitation>();

		private List<SentInvitation> _SentInvitations = new List<SentInvitation>();

		public uint MaxFriends
		{
			get
			{
				return _MaxFriends;
			}
			set
			{
				_MaxFriends = value;
				HasMaxFriends = true;
			}
		}

		public uint MaxReceivedInvitations
		{
			get
			{
				return _MaxReceivedInvitations;
			}
			set
			{
				_MaxReceivedInvitations = value;
				HasMaxReceivedInvitations = true;
			}
		}

		public uint MaxSentInvitations
		{
			get
			{
				return _MaxSentInvitations;
			}
			set
			{
				_MaxSentInvitations = value;
				HasMaxSentInvitations = true;
			}
		}

		public List<Role> Role
		{
			get
			{
				return _Role;
			}
			set
			{
				_Role = value;
			}
		}

		public List<Role> RoleList => _Role;

		public int RoleCount => _Role.Count;

		public List<Friend> Friends
		{
			get
			{
				return _Friends;
			}
			set
			{
				_Friends = value;
			}
		}

		public List<Friend> FriendsList => _Friends;

		public int FriendsCount => _Friends.Count;

		public List<ReceivedInvitation> ReceivedInvitations
		{
			get
			{
				return _ReceivedInvitations;
			}
			set
			{
				_ReceivedInvitations = value;
			}
		}

		public List<ReceivedInvitation> ReceivedInvitationsList => _ReceivedInvitations;

		public int ReceivedInvitationsCount => _ReceivedInvitations.Count;

		public List<SentInvitation> SentInvitations
		{
			get
			{
				return _SentInvitations;
			}
			set
			{
				_SentInvitations = value;
			}
		}

		public List<SentInvitation> SentInvitationsList => _SentInvitations;

		public int SentInvitationsCount => _SentInvitations.Count;

		public bool IsInitialized => true;

		public void SetMaxFriends(uint val)
		{
			MaxFriends = val;
		}

		public void SetMaxReceivedInvitations(uint val)
		{
			MaxReceivedInvitations = val;
		}

		public void SetMaxSentInvitations(uint val)
		{
			MaxSentInvitations = val;
		}

		public void AddRole(Role val)
		{
			_Role.Add(val);
		}

		public void ClearRole()
		{
			_Role.Clear();
		}

		public void SetRole(List<Role> val)
		{
			Role = val;
		}

		public void AddFriends(Friend val)
		{
			_Friends.Add(val);
		}

		public void ClearFriends()
		{
			_Friends.Clear();
		}

		public void SetFriends(List<Friend> val)
		{
			Friends = val;
		}

		public void AddReceivedInvitations(ReceivedInvitation val)
		{
			_ReceivedInvitations.Add(val);
		}

		public void ClearReceivedInvitations()
		{
			_ReceivedInvitations.Clear();
		}

		public void SetReceivedInvitations(List<ReceivedInvitation> val)
		{
			ReceivedInvitations = val;
		}

		public void AddSentInvitations(SentInvitation val)
		{
			_SentInvitations.Add(val);
		}

		public void ClearSentInvitations()
		{
			_SentInvitations.Clear();
		}

		public void SetSentInvitations(List<SentInvitation> val)
		{
			SentInvitations = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMaxFriends)
			{
				num ^= MaxFriends.GetHashCode();
			}
			if (HasMaxReceivedInvitations)
			{
				num ^= MaxReceivedInvitations.GetHashCode();
			}
			if (HasMaxSentInvitations)
			{
				num ^= MaxSentInvitations.GetHashCode();
			}
			foreach (Role item in Role)
			{
				num ^= item.GetHashCode();
			}
			foreach (Friend friend in Friends)
			{
				num ^= friend.GetHashCode();
			}
			foreach (ReceivedInvitation receivedInvitation in ReceivedInvitations)
			{
				num ^= receivedInvitation.GetHashCode();
			}
			foreach (SentInvitation sentInvitation in SentInvitations)
			{
				num ^= sentInvitation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			if (subscribeResponse == null)
			{
				return false;
			}
			if (HasMaxFriends != subscribeResponse.HasMaxFriends || (HasMaxFriends && !MaxFriends.Equals(subscribeResponse.MaxFriends)))
			{
				return false;
			}
			if (HasMaxReceivedInvitations != subscribeResponse.HasMaxReceivedInvitations || (HasMaxReceivedInvitations && !MaxReceivedInvitations.Equals(subscribeResponse.MaxReceivedInvitations)))
			{
				return false;
			}
			if (HasMaxSentInvitations != subscribeResponse.HasMaxSentInvitations || (HasMaxSentInvitations && !MaxSentInvitations.Equals(subscribeResponse.MaxSentInvitations)))
			{
				return false;
			}
			if (Role.Count != subscribeResponse.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < Role.Count; i++)
			{
				if (!Role[i].Equals(subscribeResponse.Role[i]))
				{
					return false;
				}
			}
			if (Friends.Count != subscribeResponse.Friends.Count)
			{
				return false;
			}
			for (int j = 0; j < Friends.Count; j++)
			{
				if (!Friends[j].Equals(subscribeResponse.Friends[j]))
				{
					return false;
				}
			}
			if (ReceivedInvitations.Count != subscribeResponse.ReceivedInvitations.Count)
			{
				return false;
			}
			for (int k = 0; k < ReceivedInvitations.Count; k++)
			{
				if (!ReceivedInvitations[k].Equals(subscribeResponse.ReceivedInvitations[k]))
				{
					return false;
				}
			}
			if (SentInvitations.Count != subscribeResponse.SentInvitations.Count)
			{
				return false;
			}
			for (int l = 0; l < SentInvitations.Count; l++)
			{
				if (!SentInvitations[l].Equals(subscribeResponse.SentInvitations[l]))
				{
					return false;
				}
			}
			return true;
		}

		public static SubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<Role>();
			}
			if (instance.Friends == null)
			{
				instance.Friends = new List<Friend>();
			}
			if (instance.ReceivedInvitations == null)
			{
				instance.ReceivedInvitations = new List<ReceivedInvitation>();
			}
			if (instance.SentInvitations == null)
			{
				instance.SentInvitations = new List<SentInvitation>();
			}
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 8:
					instance.MaxFriends = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.MaxReceivedInvitations = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.MaxSentInvitations = ProtocolParser.ReadUInt32(stream);
					continue;
				case 34:
					instance.Role.Add(bnet.protocol.Role.DeserializeLengthDelimited(stream));
					continue;
				case 42:
					instance.Friends.Add(Friend.DeserializeLengthDelimited(stream));
					continue;
				case 58:
					instance.ReceivedInvitations.Add(ReceivedInvitation.DeserializeLengthDelimited(stream));
					continue;
				case 66:
					instance.SentInvitations.Add(SentInvitation.DeserializeLengthDelimited(stream));
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.HasMaxFriends)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.MaxFriends);
			}
			if (instance.HasMaxReceivedInvitations)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.MaxReceivedInvitations);
			}
			if (instance.HasMaxSentInvitations)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.MaxSentInvitations);
			}
			if (instance.Role.Count > 0)
			{
				foreach (Role item in instance.Role)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.Role.Serialize(stream, item);
				}
			}
			if (instance.Friends.Count > 0)
			{
				foreach (Friend friend in instance.Friends)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, friend.GetSerializedSize());
					Friend.Serialize(stream, friend);
				}
			}
			if (instance.ReceivedInvitations.Count > 0)
			{
				foreach (ReceivedInvitation receivedInvitation in instance.ReceivedInvitations)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, receivedInvitation.GetSerializedSize());
					ReceivedInvitation.Serialize(stream, receivedInvitation);
				}
			}
			if (instance.SentInvitations.Count <= 0)
			{
				return;
			}
			foreach (SentInvitation sentInvitation in instance.SentInvitations)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, sentInvitation.GetSerializedSize());
				SentInvitation.Serialize(stream, sentInvitation);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMaxFriends)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MaxFriends);
			}
			if (HasMaxReceivedInvitations)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MaxReceivedInvitations);
			}
			if (HasMaxSentInvitations)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MaxSentInvitations);
			}
			if (Role.Count > 0)
			{
				foreach (Role item in Role)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (Friends.Count > 0)
			{
				foreach (Friend friend in Friends)
				{
					num++;
					uint serializedSize2 = friend.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (ReceivedInvitations.Count > 0)
			{
				foreach (ReceivedInvitation receivedInvitation in ReceivedInvitations)
				{
					num++;
					uint serializedSize3 = receivedInvitation.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (SentInvitations.Count > 0)
			{
				foreach (SentInvitation sentInvitation in SentInvitations)
				{
					num++;
					uint serializedSize4 = sentInvitation.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
				return num;
			}
			return num;
		}
	}
}
