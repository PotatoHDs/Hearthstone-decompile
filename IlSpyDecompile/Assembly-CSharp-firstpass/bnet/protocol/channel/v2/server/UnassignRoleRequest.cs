using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	public class UnassignRoleRequest : IProtoBuf
	{
		public bool HasChannelId;

		private bnet.protocol.channel.v1.ChannelId _ChannelId;

		public bool HasAssignment;

		private RoleAssignment _Assignment;

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

		public RoleAssignment Assignment
		{
			get
			{
				return _Assignment;
			}
			set
			{
				_Assignment = value;
				HasAssignment = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetChannelId(bnet.protocol.channel.v1.ChannelId val)
		{
			ChannelId = val;
		}

		public void SetAssignment(RoleAssignment val)
		{
			Assignment = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasAssignment)
			{
				num ^= Assignment.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UnassignRoleRequest unassignRoleRequest = obj as UnassignRoleRequest;
			if (unassignRoleRequest == null)
			{
				return false;
			}
			if (HasChannelId != unassignRoleRequest.HasChannelId || (HasChannelId && !ChannelId.Equals(unassignRoleRequest.ChannelId)))
			{
				return false;
			}
			if (HasAssignment != unassignRoleRequest.HasAssignment || (HasAssignment && !Assignment.Equals(unassignRoleRequest.Assignment)))
			{
				return false;
			}
			return true;
		}

		public static UnassignRoleRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnassignRoleRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UnassignRoleRequest Deserialize(Stream stream, UnassignRoleRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UnassignRoleRequest DeserializeLengthDelimited(Stream stream)
		{
			UnassignRoleRequest unassignRoleRequest = new UnassignRoleRequest();
			DeserializeLengthDelimited(stream, unassignRoleRequest);
			return unassignRoleRequest;
		}

		public static UnassignRoleRequest DeserializeLengthDelimited(Stream stream, UnassignRoleRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UnassignRoleRequest Deserialize(Stream stream, UnassignRoleRequest instance, long limit)
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
				case 10:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = bnet.protocol.channel.v1.ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.channel.v1.ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 18:
					if (instance.Assignment == null)
					{
						instance.Assignment = RoleAssignment.DeserializeLengthDelimited(stream);
					}
					else
					{
						RoleAssignment.DeserializeLengthDelimited(stream, instance.Assignment);
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

		public static void Serialize(Stream stream, UnassignRoleRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				bnet.protocol.channel.v1.ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasAssignment)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Assignment.GetSerializedSize());
				RoleAssignment.Serialize(stream, instance.Assignment);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasChannelId)
			{
				num++;
				uint serializedSize = ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasAssignment)
			{
				num++;
				uint serializedSize2 = Assignment.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
