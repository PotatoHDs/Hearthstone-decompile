using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	public class MemberAddedNotification : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasChannelId;

		private bnet.protocol.channel.v1.ChannelId _ChannelId;

		private List<Member> _Member = new List<Member>();

		public GameAccountHandle AgentId
		{
			get
			{
				return _AgentId;
			}
			set
			{
				_AgentId = value;
				HasAgentId = value != null;
			}
		}

		public bnet.protocol.channel.v1.ChannelId ChannelId
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

		public bool IsInitialized => true;

		public void SetAgentId(GameAccountHandle val)
		{
			AgentId = val;
		}

		public void SetChannelId(bnet.protocol.channel.v1.ChannelId val)
		{
			ChannelId = val;
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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			foreach (Member item in Member)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MemberAddedNotification memberAddedNotification = obj as MemberAddedNotification;
			if (memberAddedNotification == null)
			{
				return false;
			}
			if (HasAgentId != memberAddedNotification.HasAgentId || (HasAgentId && !AgentId.Equals(memberAddedNotification.AgentId)))
			{
				return false;
			}
			if (HasChannelId != memberAddedNotification.HasChannelId || (HasChannelId && !ChannelId.Equals(memberAddedNotification.ChannelId)))
			{
				return false;
			}
			if (Member.Count != memberAddedNotification.Member.Count)
			{
				return false;
			}
			for (int i = 0; i < Member.Count; i++)
			{
				if (!Member[i].Equals(memberAddedNotification.Member[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static MemberAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberAddedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MemberAddedNotification Deserialize(Stream stream, MemberAddedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MemberAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberAddedNotification memberAddedNotification = new MemberAddedNotification();
			DeserializeLengthDelimited(stream, memberAddedNotification);
			return memberAddedNotification;
		}

		public static MemberAddedNotification DeserializeLengthDelimited(Stream stream, MemberAddedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MemberAddedNotification Deserialize(Stream stream, MemberAddedNotification instance, long limit)
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
					if (instance.AgentId == null)
					{
						instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 26:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = bnet.protocol.channel.v1.ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.channel.v1.ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 34:
					instance.Member.Add(bnet.protocol.channel.v2.Member.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, MemberAddedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				bnet.protocol.channel.v1.ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.Member.Count <= 0)
			{
				return;
			}
			foreach (Member item in instance.Member)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.channel.v2.Member.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAgentId)
			{
				num++;
				uint serializedSize = AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasChannelId)
			{
				num++;
				uint serializedSize2 = ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (Member.Count > 0)
			{
				foreach (Member item in Member)
				{
					num++;
					uint serializedSize3 = item.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
				return num;
			}
			return num;
		}
	}
}
