using System.IO;

namespace bnet.protocol.channel
{
	public class ChannelRole : IProtoBuf
	{
		public bool HasId;

		private uint _Id;

		public bool HasState;

		private RoleState _State;

		public bool HasPrivilege;

		private ChannelPrivilegeSet _Privilege;

		public uint Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = true;
			}
		}

		public RoleState State
		{
			get
			{
				return _State;
			}
			set
			{
				_State = value;
				HasState = value != null;
			}
		}

		public ChannelPrivilegeSet Privilege
		{
			get
			{
				return _Privilege;
			}
			set
			{
				_Privilege = value;
				HasPrivilege = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetId(uint val)
		{
			Id = val;
		}

		public void SetState(RoleState val)
		{
			State = val;
		}

		public void SetPrivilege(ChannelPrivilegeSet val)
		{
			Privilege = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			if (HasState)
			{
				num ^= State.GetHashCode();
			}
			if (HasPrivilege)
			{
				num ^= Privilege.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelRole channelRole = obj as ChannelRole;
			if (channelRole == null)
			{
				return false;
			}
			if (HasId != channelRole.HasId || (HasId && !Id.Equals(channelRole.Id)))
			{
				return false;
			}
			if (HasState != channelRole.HasState || (HasState && !State.Equals(channelRole.State)))
			{
				return false;
			}
			if (HasPrivilege != channelRole.HasPrivilege || (HasPrivilege && !Privilege.Equals(channelRole.Privilege)))
			{
				return false;
			}
			return true;
		}

		public static ChannelRole ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelRole>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelRole Deserialize(Stream stream, ChannelRole instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelRole DeserializeLengthDelimited(Stream stream)
		{
			ChannelRole channelRole = new ChannelRole();
			DeserializeLengthDelimited(stream, channelRole);
			return channelRole;
		}

		public static ChannelRole DeserializeLengthDelimited(Stream stream, ChannelRole instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelRole Deserialize(Stream stream, ChannelRole instance, long limit)
		{
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
					instance.Id = ProtocolParser.ReadUInt32(stream);
					continue;
				case 18:
					if (instance.State == null)
					{
						instance.State = RoleState.DeserializeLengthDelimited(stream);
					}
					else
					{
						RoleState.DeserializeLengthDelimited(stream, instance.State);
					}
					continue;
				case 26:
					if (instance.Privilege == null)
					{
						instance.Privilege = ChannelPrivilegeSet.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelPrivilegeSet.DeserializeLengthDelimited(stream, instance.Privilege);
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

		public static void Serialize(Stream stream, ChannelRole instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.Id);
			}
			if (instance.HasState)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				RoleState.Serialize(stream, instance.State);
			}
			if (instance.HasPrivilege)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Privilege.GetSerializedSize());
				ChannelPrivilegeSet.Serialize(stream, instance.Privilege);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Id);
			}
			if (HasState)
			{
				num++;
				uint serializedSize = State.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasPrivilege)
			{
				num++;
				uint serializedSize2 = Privilege.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
