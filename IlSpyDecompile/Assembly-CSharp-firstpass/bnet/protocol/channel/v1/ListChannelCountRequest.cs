using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class ListChannelCountRequest : IProtoBuf
	{
		public EntityId MemberId { get; set; }

		public uint ServiceType { get; set; }

		public bool IsInitialized => true;

		public void SetMemberId(EntityId val)
		{
			MemberId = val;
		}

		public void SetServiceType(uint val)
		{
			ServiceType = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ MemberId.GetHashCode() ^ ServiceType.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ListChannelCountRequest listChannelCountRequest = obj as ListChannelCountRequest;
			if (listChannelCountRequest == null)
			{
				return false;
			}
			if (!MemberId.Equals(listChannelCountRequest.MemberId))
			{
				return false;
			}
			if (!ServiceType.Equals(listChannelCountRequest.ServiceType))
			{
				return false;
			}
			return true;
		}

		public static ListChannelCountRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListChannelCountRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ListChannelCountRequest Deserialize(Stream stream, ListChannelCountRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ListChannelCountRequest DeserializeLengthDelimited(Stream stream)
		{
			ListChannelCountRequest listChannelCountRequest = new ListChannelCountRequest();
			DeserializeLengthDelimited(stream, listChannelCountRequest);
			return listChannelCountRequest;
		}

		public static ListChannelCountRequest DeserializeLengthDelimited(Stream stream, ListChannelCountRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ListChannelCountRequest Deserialize(Stream stream, ListChannelCountRequest instance, long limit)
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
					if (instance.MemberId == null)
					{
						instance.MemberId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.MemberId);
					}
					continue;
				case 16:
					instance.ServiceType = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, ListChannelCountRequest instance)
		{
			if (instance.MemberId == null)
			{
				throw new ArgumentNullException("MemberId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
			EntityId.Serialize(stream, instance.MemberId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.ServiceType);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = MemberId.GetSerializedSize();
			return 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + ProtocolParser.SizeOfUInt32(ServiceType) + 2;
		}
	}
}
