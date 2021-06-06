using System;
using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	public class PatronCheckedOutOfFSG : IProtoBuf
	{
		public enum PacketID
		{
			ID = 510,
			System = 3
		}

		public FSGPatron Patron { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Patron.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			PatronCheckedOutOfFSG patronCheckedOutOfFSG = obj as PatronCheckedOutOfFSG;
			if (patronCheckedOutOfFSG == null)
			{
				return false;
			}
			if (!Patron.Equals(patronCheckedOutOfFSG.Patron))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PatronCheckedOutOfFSG Deserialize(Stream stream, PatronCheckedOutOfFSG instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PatronCheckedOutOfFSG DeserializeLengthDelimited(Stream stream)
		{
			PatronCheckedOutOfFSG patronCheckedOutOfFSG = new PatronCheckedOutOfFSG();
			DeserializeLengthDelimited(stream, patronCheckedOutOfFSG);
			return patronCheckedOutOfFSG;
		}

		public static PatronCheckedOutOfFSG DeserializeLengthDelimited(Stream stream, PatronCheckedOutOfFSG instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PatronCheckedOutOfFSG Deserialize(Stream stream, PatronCheckedOutOfFSG instance, long limit)
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
					if (instance.Patron == null)
					{
						instance.Patron = FSGPatron.DeserializeLengthDelimited(stream);
					}
					else
					{
						FSGPatron.DeserializeLengthDelimited(stream, instance.Patron);
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

		public static void Serialize(Stream stream, PatronCheckedOutOfFSG instance)
		{
			if (instance.Patron == null)
			{
				throw new ArgumentNullException("Patron", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Patron.GetSerializedSize());
			FSGPatron.Serialize(stream, instance.Patron);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = Patron.GetSerializedSize();
			return 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1;
		}
	}
}
