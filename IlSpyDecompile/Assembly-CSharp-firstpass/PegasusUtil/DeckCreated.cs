using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class DeckCreated : IProtoBuf
	{
		public enum PacketID
		{
			ID = 217
		}

		public bool HasRequestId;

		private int _RequestId;

		public DeckInfo Info { get; set; }

		public int RequestId
		{
			get
			{
				return _RequestId;
			}
			set
			{
				_RequestId = value;
				HasRequestId = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Info.GetHashCode();
			if (HasRequestId)
			{
				hashCode ^= RequestId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DeckCreated deckCreated = obj as DeckCreated;
			if (deckCreated == null)
			{
				return false;
			}
			if (!Info.Equals(deckCreated.Info))
			{
				return false;
			}
			if (HasRequestId != deckCreated.HasRequestId || (HasRequestId && !RequestId.Equals(deckCreated.RequestId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeckCreated Deserialize(Stream stream, DeckCreated instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckCreated DeserializeLengthDelimited(Stream stream)
		{
			DeckCreated deckCreated = new DeckCreated();
			DeserializeLengthDelimited(stream, deckCreated);
			return deckCreated;
		}

		public static DeckCreated DeserializeLengthDelimited(Stream stream, DeckCreated instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckCreated Deserialize(Stream stream, DeckCreated instance, long limit)
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
					if (instance.Info == null)
					{
						instance.Info = DeckInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeckInfo.DeserializeLengthDelimited(stream, instance.Info);
					}
					continue;
				case 16:
					instance.RequestId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DeckCreated instance)
		{
			if (instance.Info == null)
			{
				throw new ArgumentNullException("Info", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Info.GetSerializedSize());
			DeckInfo.Serialize(stream, instance.Info);
			if (instance.HasRequestId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RequestId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Info.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasRequestId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RequestId);
			}
			return num + 1;
		}
	}
}
