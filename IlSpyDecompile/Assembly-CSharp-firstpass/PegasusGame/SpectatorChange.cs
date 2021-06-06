using System;
using System.IO;
using PegasusShared;

namespace PegasusGame
{
	public class SpectatorChange : IProtoBuf
	{
		public BnetId GameAccountId { get; set; }

		public bool IsRemoved { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ GameAccountId.GetHashCode() ^ IsRemoved.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SpectatorChange spectatorChange = obj as SpectatorChange;
			if (spectatorChange == null)
			{
				return false;
			}
			if (!GameAccountId.Equals(spectatorChange.GameAccountId))
			{
				return false;
			}
			if (!IsRemoved.Equals(spectatorChange.IsRemoved))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SpectatorChange Deserialize(Stream stream, SpectatorChange instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SpectatorChange DeserializeLengthDelimited(Stream stream)
		{
			SpectatorChange spectatorChange = new SpectatorChange();
			DeserializeLengthDelimited(stream, spectatorChange);
			return spectatorChange;
		}

		public static SpectatorChange DeserializeLengthDelimited(Stream stream, SpectatorChange instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SpectatorChange Deserialize(Stream stream, SpectatorChange instance, long limit)
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
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
					continue;
				case 16:
					instance.IsRemoved = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, SpectatorChange instance)
		{
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.GameAccountId);
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.IsRemoved);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = GameAccountId.GetSerializedSize();
			return 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1 + 2;
		}
	}
}
