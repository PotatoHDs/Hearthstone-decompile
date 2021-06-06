using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class DraftRetired : IProtoBuf
	{
		public enum PacketID
		{
			ID = 247
		}

		public long DeckId { get; set; }

		public RewardChest Chest { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ DeckId.GetHashCode() ^ Chest.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DraftRetired draftRetired = obj as DraftRetired;
			if (draftRetired == null)
			{
				return false;
			}
			if (!DeckId.Equals(draftRetired.DeckId))
			{
				return false;
			}
			if (!Chest.Equals(draftRetired.Chest))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DraftRetired Deserialize(Stream stream, DraftRetired instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DraftRetired DeserializeLengthDelimited(Stream stream)
		{
			DraftRetired draftRetired = new DraftRetired();
			DeserializeLengthDelimited(stream, draftRetired);
			return draftRetired;
		}

		public static DraftRetired DeserializeLengthDelimited(Stream stream, DraftRetired instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DraftRetired Deserialize(Stream stream, DraftRetired instance, long limit)
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
					instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.Chest == null)
					{
						instance.Chest = RewardChest.DeserializeLengthDelimited(stream);
					}
					else
					{
						RewardChest.DeserializeLengthDelimited(stream, instance.Chest);
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

		public static void Serialize(Stream stream, DraftRetired instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			if (instance.Chest == null)
			{
				throw new ArgumentNullException("Chest", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Chest.GetSerializedSize());
			RewardChest.Serialize(stream, instance.Chest);
		}

		public uint GetSerializedSize()
		{
			uint num = 0 + ProtocolParser.SizeOfUInt64((ulong)DeckId);
			uint serializedSize = Chest.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 2;
		}
	}
}
