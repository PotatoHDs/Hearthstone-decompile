using System;
using System.IO;

namespace PegasusShared
{
	public class FSGPatron : IProtoBuf
	{
		public BnetId GameAccount { get; set; }

		public BnetId BnetAccount { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ GameAccount.GetHashCode() ^ BnetAccount.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			FSGPatron fSGPatron = obj as FSGPatron;
			if (fSGPatron == null)
			{
				return false;
			}
			if (!GameAccount.Equals(fSGPatron.GameAccount))
			{
				return false;
			}
			if (!BnetAccount.Equals(fSGPatron.BnetAccount))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FSGPatron Deserialize(Stream stream, FSGPatron instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FSGPatron DeserializeLengthDelimited(Stream stream)
		{
			FSGPatron fSGPatron = new FSGPatron();
			DeserializeLengthDelimited(stream, fSGPatron);
			return fSGPatron;
		}

		public static FSGPatron DeserializeLengthDelimited(Stream stream, FSGPatron instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FSGPatron Deserialize(Stream stream, FSGPatron instance, long limit)
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
					if (instance.GameAccount == null)
					{
						instance.GameAccount = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.GameAccount);
					}
					continue;
				case 18:
					if (instance.BnetAccount == null)
					{
						instance.BnetAccount = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.BnetAccount);
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

		public static void Serialize(Stream stream, FSGPatron instance)
		{
			if (instance.GameAccount == null)
			{
				throw new ArgumentNullException("GameAccount", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
			BnetId.Serialize(stream, instance.GameAccount);
			if (instance.BnetAccount == null)
			{
				throw new ArgumentNullException("BnetAccount", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.BnetAccount.GetSerializedSize());
			BnetId.Serialize(stream, instance.BnetAccount);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = GameAccount.GetSerializedSize();
			uint num = 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = BnetAccount.GetSerializedSize();
			return num + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 2;
		}
	}
}
