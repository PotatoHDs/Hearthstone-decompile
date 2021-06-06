using System.IO;

namespace PegasusShared
{
	public class Vector2 : IProtoBuf
	{
		public float X { get; set; }

		public float Y { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ X.GetHashCode() ^ Y.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Vector2 vector = obj as Vector2;
			if (vector == null)
			{
				return false;
			}
			if (!X.Equals(vector.X))
			{
				return false;
			}
			if (!Y.Equals(vector.Y))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Vector2 Deserialize(Stream stream, Vector2 instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Vector2 DeserializeLengthDelimited(Stream stream)
		{
			Vector2 vector = new Vector2();
			DeserializeLengthDelimited(stream, vector);
			return vector;
		}

		public static Vector2 DeserializeLengthDelimited(Stream stream, Vector2 instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Vector2 Deserialize(Stream stream, Vector2 instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.X = 0f;
			instance.Y = 0f;
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
				case 13:
					instance.X = binaryReader.ReadSingle();
					continue;
				case 21:
					instance.Y = binaryReader.ReadSingle();
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

		public static void Serialize(Stream stream, Vector2 instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.X);
			stream.WriteByte(21);
			binaryWriter.Write(instance.Y);
		}

		public uint GetSerializedSize()
		{
			return 10u;
		}
	}
}
