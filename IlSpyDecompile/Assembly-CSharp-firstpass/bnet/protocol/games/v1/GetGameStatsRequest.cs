using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class GetGameStatsRequest : IProtoBuf
	{
		public ulong FactoryId { get; set; }

		public AttributeFilter Filter { get; set; }

		public bool IsInitialized => true;

		public void SetFactoryId(ulong val)
		{
			FactoryId = val;
		}

		public void SetFilter(AttributeFilter val)
		{
			Filter = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ FactoryId.GetHashCode() ^ Filter.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GetGameStatsRequest getGameStatsRequest = obj as GetGameStatsRequest;
			if (getGameStatsRequest == null)
			{
				return false;
			}
			if (!FactoryId.Equals(getGameStatsRequest.FactoryId))
			{
				return false;
			}
			if (!Filter.Equals(getGameStatsRequest.Filter))
			{
				return false;
			}
			return true;
		}

		public static GetGameStatsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameStatsRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGameStatsRequest Deserialize(Stream stream, GetGameStatsRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGameStatsRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGameStatsRequest getGameStatsRequest = new GetGameStatsRequest();
			DeserializeLengthDelimited(stream, getGameStatsRequest);
			return getGameStatsRequest;
		}

		public static GetGameStatsRequest DeserializeLengthDelimited(Stream stream, GetGameStatsRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGameStatsRequest Deserialize(Stream stream, GetGameStatsRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 9:
					instance.FactoryId = binaryReader.ReadUInt64();
					continue;
				case 18:
					if (instance.Filter == null)
					{
						instance.Filter = AttributeFilter.DeserializeLengthDelimited(stream);
					}
					else
					{
						AttributeFilter.DeserializeLengthDelimited(stream, instance.Filter);
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

		public static void Serialize(Stream stream, GetGameStatsRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.FactoryId);
			if (instance.Filter == null)
			{
				throw new ArgumentNullException("Filter", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Filter.GetSerializedSize());
			AttributeFilter.Serialize(stream, instance.Filter);
		}

		public uint GetSerializedSize()
		{
			int num = 0 + 8;
			uint serializedSize = Filter.GetSerializedSize();
			return (uint)(num + (int)(serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 2);
		}
	}
}
