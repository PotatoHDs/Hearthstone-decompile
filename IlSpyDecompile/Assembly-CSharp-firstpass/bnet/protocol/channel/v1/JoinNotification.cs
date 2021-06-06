using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	public class JoinNotification : IProtoBuf
	{
		public bool HasSelf;

		private Member _Self;

		private List<Member> _Member = new List<Member>();

		public bool HasChannelId;

		private ChannelId _ChannelId;

		public bool HasSubscriberId;

		private SubscriberId _SubscriberId;

		public bool HasPresenceSubscriberId;

		private AccountId _PresenceSubscriberId;

		public Member Self
		{
			get
			{
				return _Self;
			}
			set
			{
				_Self = value;
				HasSelf = value != null;
			}
		}

		public List<Member> Member
		{
			get
			{
				return _Member;
			}
			set
			{
				_Member = value;
			}
		}

		public List<Member> MemberList => _Member;

		public int MemberCount => _Member.Count;

		public ChannelState ChannelState { get; set; }

		public ChannelId ChannelId
		{
			get
			{
				return _ChannelId;
			}
			set
			{
				_ChannelId = value;
				HasChannelId = value != null;
			}
		}

		public SubscriberId SubscriberId
		{
			get
			{
				return _SubscriberId;
			}
			set
			{
				_SubscriberId = value;
				HasSubscriberId = value != null;
			}
		}

		public AccountId PresenceSubscriberId
		{
			get
			{
				return _PresenceSubscriberId;
			}
			set
			{
				_PresenceSubscriberId = value;
				HasPresenceSubscriberId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetSelf(Member val)
		{
			Self = val;
		}

		public void AddMember(Member val)
		{
			_Member.Add(val);
		}

		public void ClearMember()
		{
			_Member.Clear();
		}

		public void SetMember(List<Member> val)
		{
			Member = val;
		}

		public void SetChannelState(ChannelState val)
		{
			ChannelState = val;
		}

		public void SetChannelId(ChannelId val)
		{
			ChannelId = val;
		}

		public void SetSubscriberId(SubscriberId val)
		{
			SubscriberId = val;
		}

		public void SetPresenceSubscriberId(AccountId val)
		{
			PresenceSubscriberId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSelf)
			{
				num ^= Self.GetHashCode();
			}
			foreach (Member item in Member)
			{
				num ^= item.GetHashCode();
			}
			num ^= ChannelState.GetHashCode();
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasSubscriberId)
			{
				num ^= SubscriberId.GetHashCode();
			}
			if (HasPresenceSubscriberId)
			{
				num ^= PresenceSubscriberId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			JoinNotification joinNotification = obj as JoinNotification;
			if (joinNotification == null)
			{
				return false;
			}
			if (HasSelf != joinNotification.HasSelf || (HasSelf && !Self.Equals(joinNotification.Self)))
			{
				return false;
			}
			if (Member.Count != joinNotification.Member.Count)
			{
				return false;
			}
			for (int i = 0; i < Member.Count; i++)
			{
				if (!Member[i].Equals(joinNotification.Member[i]))
				{
					return false;
				}
			}
			if (!ChannelState.Equals(joinNotification.ChannelState))
			{
				return false;
			}
			if (HasChannelId != joinNotification.HasChannelId || (HasChannelId && !ChannelId.Equals(joinNotification.ChannelId)))
			{
				return false;
			}
			if (HasSubscriberId != joinNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(joinNotification.SubscriberId)))
			{
				return false;
			}
			if (HasPresenceSubscriberId != joinNotification.HasPresenceSubscriberId || (HasPresenceSubscriberId && !PresenceSubscriberId.Equals(joinNotification.PresenceSubscriberId)))
			{
				return false;
			}
			return true;
		}

		public static JoinNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static JoinNotification Deserialize(Stream stream, JoinNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static JoinNotification DeserializeLengthDelimited(Stream stream)
		{
			JoinNotification joinNotification = new JoinNotification();
			DeserializeLengthDelimited(stream, joinNotification);
			return joinNotification;
		}

		public static JoinNotification DeserializeLengthDelimited(Stream stream, JoinNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static JoinNotification Deserialize(Stream stream, JoinNotification instance, long limit)
		{
			if (instance.Member == null)
			{
				instance.Member = new List<Member>();
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
				case 10:
					if (instance.Self == null)
					{
						instance.Self = bnet.protocol.channel.v1.Member.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.channel.v1.Member.DeserializeLengthDelimited(stream, instance.Self);
					}
					continue;
				case 18:
					instance.Member.Add(bnet.protocol.channel.v1.Member.DeserializeLengthDelimited(stream));
					continue;
				case 26:
					if (instance.ChannelState == null)
					{
						instance.ChannelState = ChannelState.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelState.DeserializeLengthDelimited(stream, instance.ChannelState);
					}
					continue;
				case 34:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 42:
					if (instance.SubscriberId == null)
					{
						instance.SubscriberId = SubscriberId.DeserializeLengthDelimited(stream);
					}
					else
					{
						SubscriberId.DeserializeLengthDelimited(stream, instance.SubscriberId);
					}
					continue;
				case 50:
					if (instance.PresenceSubscriberId == null)
					{
						instance.PresenceSubscriberId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.PresenceSubscriberId);
					}
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

		public static void Serialize(Stream stream, JoinNotification instance)
		{
			if (instance.HasSelf)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Self.GetSerializedSize());
				bnet.protocol.channel.v1.Member.Serialize(stream, instance.Self);
			}
			if (instance.Member.Count > 0)
			{
				foreach (Member item in instance.Member)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.channel.v1.Member.Serialize(stream, item);
				}
			}
			if (instance.ChannelState == null)
			{
				throw new ArgumentNullException("ChannelState", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.ChannelState.GetSerializedSize());
			ChannelState.Serialize(stream, instance.ChannelState);
			if (instance.HasChannelId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				SubscriberId.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasPresenceSubscriberId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.PresenceSubscriberId.GetSerializedSize());
				AccountId.Serialize(stream, instance.PresenceSubscriberId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSelf)
			{
				num++;
				uint serializedSize = Self.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (Member.Count > 0)
			{
				foreach (Member item in Member)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			uint serializedSize3 = ChannelState.GetSerializedSize();
			num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			if (HasChannelId)
			{
				num++;
				uint serializedSize4 = ChannelId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasSubscriberId)
			{
				num++;
				uint serializedSize5 = SubscriberId.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (HasPresenceSubscriberId)
			{
				num++;
				uint serializedSize6 = PresenceSubscriberId.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			return num + 1;
		}
	}
}
