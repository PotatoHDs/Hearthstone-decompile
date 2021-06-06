using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	public class MemberRemovedNotification : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasChannelId;

		private bnet.protocol.channel.v1.ChannelId _ChannelId;

		public bool HasReason;

		private uint _Reason;

		private List<GameAccountHandle> _MemberId = new List<GameAccountHandle>();

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

		public uint Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = true;
			}
		}

		public List<GameAccountHandle> MemberId
		{
			get
			{
				return _MemberId;
			}
			set
			{
				_MemberId = value;
			}
		}

		public List<GameAccountHandle> MemberIdList => _MemberId;

		public int MemberIdCount => _MemberId.Count;

		public bool IsInitialized => true;

		public void SetAgentId(GameAccountHandle val)
		{
			AgentId = val;
		}

		public void SetChannelId(bnet.protocol.channel.v1.ChannelId val)
		{
			ChannelId = val;
		}

		public void SetReason(uint val)
		{
			Reason = val;
		}

		public void AddMemberId(GameAccountHandle val)
		{
			_MemberId.Add(val);
		}

		public void ClearMemberId()
		{
			_MemberId.Clear();
		}

		public void SetMemberId(List<GameAccountHandle> val)
		{
			MemberId = val;
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
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			foreach (GameAccountHandle item in MemberId)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MemberRemovedNotification memberRemovedNotification = obj as MemberRemovedNotification;
			if (memberRemovedNotification == null)
			{
				return false;
			}
			if (HasAgentId != memberRemovedNotification.HasAgentId || (HasAgentId && !AgentId.Equals(memberRemovedNotification.AgentId)))
			{
				return false;
			}
			if (HasChannelId != memberRemovedNotification.HasChannelId || (HasChannelId && !ChannelId.Equals(memberRemovedNotification.ChannelId)))
			{
				return false;
			}
			if (HasReason != memberRemovedNotification.HasReason || (HasReason && !Reason.Equals(memberRemovedNotification.Reason)))
			{
				return false;
			}
			if (MemberId.Count != memberRemovedNotification.MemberId.Count)
			{
				return false;
			}
			for (int i = 0; i < MemberId.Count; i++)
			{
				if (!MemberId[i].Equals(memberRemovedNotification.MemberId[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static MemberRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberRemovedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MemberRemovedNotification Deserialize(Stream stream, MemberRemovedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MemberRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberRemovedNotification memberRemovedNotification = new MemberRemovedNotification();
			DeserializeLengthDelimited(stream, memberRemovedNotification);
			return memberRemovedNotification;
		}

		public static MemberRemovedNotification DeserializeLengthDelimited(Stream stream, MemberRemovedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MemberRemovedNotification Deserialize(Stream stream, MemberRemovedNotification instance, long limit)
		{
			if (instance.MemberId == null)
			{
				instance.MemberId = new List<GameAccountHandle>();
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
				case 32:
					instance.Reason = ProtocolParser.ReadUInt32(stream);
					continue;
				case 42:
					instance.MemberId.Add(GameAccountHandle.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, MemberRemovedNotification instance)
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
			if (instance.HasReason)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.MemberId.Count <= 0)
			{
				return;
			}
			foreach (GameAccountHandle item in instance.MemberId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				GameAccountHandle.Serialize(stream, item);
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
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Reason);
			}
			if (MemberId.Count > 0)
			{
				foreach (GameAccountHandle item in MemberId)
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
