using System;
using System.IO;

namespace PegasusUtil
{
	public class GenericData : IProtoBuf
	{
		public uint TypeId { get; set; }

		public byte[] Data { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ TypeId.GetHashCode() ^ Data.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GenericData genericData = obj as GenericData;
			if (genericData == null)
			{
				return false;
			}
			if (!TypeId.Equals(genericData.TypeId))
			{
				return false;
			}
			if (!Data.Equals(genericData.Data))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GenericData Deserialize(Stream stream, GenericData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GenericData DeserializeLengthDelimited(Stream stream)
		{
			GenericData genericData = new GenericData();
			DeserializeLengthDelimited(stream, genericData);
			return genericData;
		}

		public static GenericData DeserializeLengthDelimited(Stream stream, GenericData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GenericData Deserialize(Stream stream, GenericData instance, long limit)
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
					instance.TypeId = ProtocolParser.ReadUInt32(stream);
					continue;
				case 18:
					instance.Data = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, GenericData instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.TypeId);
			if (instance.Data == null)
			{
				throw new ArgumentNullException("Data", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, instance.Data);
		}

		public uint GetSerializedSize()
		{
			return (uint)((int)(0 + ProtocolParser.SizeOfUInt32(TypeId)) + ((int)ProtocolParser.SizeOfUInt32(Data.Length) + Data.Length) + 2);
		}
	}
}
