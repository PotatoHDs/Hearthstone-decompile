using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	public class ChannelCount : IProtoBuf
	{
		public bool HasChannelId;

		private EntityId _ChannelId;

		public bool HasChannelType;

		private string _ChannelType;

		public bool HasChannelName;

		private string _ChannelName;

		public EntityId ChannelId
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

		public string ChannelType
		{
			get
			{
				return _ChannelType;
			}
			set
			{
				_ChannelType = value;
				HasChannelType = value != null;
			}
		}

		public string ChannelName
		{
			get
			{
				return _ChannelName;
			}
			set
			{
				_ChannelName = value;
				HasChannelName = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetChannelId(EntityId val)
		{
			ChannelId = val;
		}

		public void SetChannelType(string val)
		{
			ChannelType = val;
		}

		public void SetChannelName(string val)
		{
			ChannelName = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasChannelType)
			{
				num ^= ChannelType.GetHashCode();
			}
			if (HasChannelName)
			{
				num ^= ChannelName.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelCount channelCount = obj as ChannelCount;
			if (channelCount == null)
			{
				return false;
			}
			if (HasChannelId != channelCount.HasChannelId || (HasChannelId && !ChannelId.Equals(channelCount.ChannelId)))
			{
				return false;
			}
			if (HasChannelType != channelCount.HasChannelType || (HasChannelType && !ChannelType.Equals(channelCount.ChannelType)))
			{
				return false;
			}
			if (HasChannelName != channelCount.HasChannelName || (HasChannelName && !ChannelName.Equals(channelCount.ChannelName)))
			{
				return false;
			}
			return true;
		}

		public static ChannelCount ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelCount>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelCount Deserialize(Stream stream, ChannelCount instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelCount DeserializeLengthDelimited(Stream stream)
		{
			ChannelCount channelCount = new ChannelCount();
			DeserializeLengthDelimited(stream, channelCount);
			return channelCount;
		}

		public static ChannelCount DeserializeLengthDelimited(Stream stream, ChannelCount instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelCount Deserialize(Stream stream, ChannelCount instance, long limit)
		{
			instance.ChannelType = "default";
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
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 18:
					instance.ChannelType = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.ChannelName = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ChannelCount instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasChannelType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelType));
			}
			if (instance.HasChannelName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelName));
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
			if (HasChannelType)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ChannelType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasChannelName)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ChannelName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
