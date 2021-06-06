using System;
using System.IO;

namespace bnet.protocol.presence.v1
{
	public class Field : IProtoBuf
	{
		public FieldKey Key { get; set; }

		public Variant Value { get; set; }

		public bool IsInitialized => true;

		public void SetKey(FieldKey val)
		{
			Key = val;
		}

		public void SetValue(Variant val)
		{
			Value = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Key.GetHashCode() ^ Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Field field = obj as Field;
			if (field == null)
			{
				return false;
			}
			if (!Key.Equals(field.Key))
			{
				return false;
			}
			if (!Value.Equals(field.Value))
			{
				return false;
			}
			return true;
		}

		public static Field ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Field>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Field Deserialize(Stream stream, Field instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Field DeserializeLengthDelimited(Stream stream)
		{
			Field field = new Field();
			DeserializeLengthDelimited(stream, field);
			return field;
		}

		public static Field DeserializeLengthDelimited(Stream stream, Field instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Field Deserialize(Stream stream, Field instance, long limit)
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
					if (instance.Key == null)
					{
						instance.Key = FieldKey.DeserializeLengthDelimited(stream);
					}
					else
					{
						FieldKey.DeserializeLengthDelimited(stream, instance.Key);
					}
					continue;
				case 18:
					if (instance.Value == null)
					{
						instance.Value = Variant.DeserializeLengthDelimited(stream);
					}
					else
					{
						Variant.DeserializeLengthDelimited(stream, instance.Value);
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

		public static void Serialize(Stream stream, Field instance)
		{
			if (instance.Key == null)
			{
				throw new ArgumentNullException("Key", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Key.GetSerializedSize());
			FieldKey.Serialize(stream, instance.Key);
			if (instance.Value == null)
			{
				throw new ArgumentNullException("Value", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Value.GetSerializedSize());
			Variant.Serialize(stream, instance.Value);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = Key.GetSerializedSize();
			uint num = 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = Value.GetSerializedSize();
			return num + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 2;
		}
	}
}
