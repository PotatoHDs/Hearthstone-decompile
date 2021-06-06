using System.IO;

namespace PegasusGame
{
	public class MouseInfo : IProtoBuf
	{
		public int ArrowOrigin { get; set; }

		public int HeldCard { get; set; }

		public int OverCard { get; set; }

		public int X { get; set; }

		public int Y { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ArrowOrigin.GetHashCode() ^ HeldCard.GetHashCode() ^ OverCard.GetHashCode() ^ X.GetHashCode() ^ Y.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			MouseInfo mouseInfo = obj as MouseInfo;
			if (mouseInfo == null)
			{
				return false;
			}
			if (!ArrowOrigin.Equals(mouseInfo.ArrowOrigin))
			{
				return false;
			}
			if (!HeldCard.Equals(mouseInfo.HeldCard))
			{
				return false;
			}
			if (!OverCard.Equals(mouseInfo.OverCard))
			{
				return false;
			}
			if (!X.Equals(mouseInfo.X))
			{
				return false;
			}
			if (!Y.Equals(mouseInfo.Y))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MouseInfo Deserialize(Stream stream, MouseInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MouseInfo DeserializeLengthDelimited(Stream stream)
		{
			MouseInfo mouseInfo = new MouseInfo();
			DeserializeLengthDelimited(stream, mouseInfo);
			return mouseInfo;
		}

		public static MouseInfo DeserializeLengthDelimited(Stream stream, MouseInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MouseInfo Deserialize(Stream stream, MouseInfo instance, long limit)
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
					instance.ArrowOrigin = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.HeldCard = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.OverCard = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.X = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.Y = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, MouseInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ArrowOrigin);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.HeldCard);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.OverCard);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.X);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Y);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)ArrowOrigin) + ProtocolParser.SizeOfUInt64((ulong)HeldCard) + ProtocolParser.SizeOfUInt64((ulong)OverCard) + ProtocolParser.SizeOfUInt64((ulong)X) + ProtocolParser.SizeOfUInt64((ulong)Y) + 5;
		}
	}
}
