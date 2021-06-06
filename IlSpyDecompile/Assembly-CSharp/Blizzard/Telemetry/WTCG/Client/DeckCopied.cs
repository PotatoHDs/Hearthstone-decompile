using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class DeckCopied : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasDeckId;

		private long _DeckId;

		public bool HasDeckHash;

		private string _DeckHash;

		public Player Player
		{
			get
			{
				return _Player;
			}
			set
			{
				_Player = value;
				HasPlayer = value != null;
			}
		}

		public DeviceInfo DeviceInfo
		{
			get
			{
				return _DeviceInfo;
			}
			set
			{
				_DeviceInfo = value;
				HasDeviceInfo = value != null;
			}
		}

		public long DeckId
		{
			get
			{
				return _DeckId;
			}
			set
			{
				_DeckId = value;
				HasDeckId = true;
			}
		}

		public string DeckHash
		{
			get
			{
				return _DeckHash;
			}
			set
			{
				_DeckHash = value;
				HasDeckHash = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasDeckId)
			{
				num ^= DeckId.GetHashCode();
			}
			if (HasDeckHash)
			{
				num ^= DeckHash.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DeckCopied deckCopied = obj as DeckCopied;
			if (deckCopied == null)
			{
				return false;
			}
			if (HasPlayer != deckCopied.HasPlayer || (HasPlayer && !Player.Equals(deckCopied.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != deckCopied.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(deckCopied.DeviceInfo)))
			{
				return false;
			}
			if (HasDeckId != deckCopied.HasDeckId || (HasDeckId && !DeckId.Equals(deckCopied.DeckId)))
			{
				return false;
			}
			if (HasDeckHash != deckCopied.HasDeckHash || (HasDeckHash && !DeckHash.Equals(deckCopied.DeckHash)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeckCopied Deserialize(Stream stream, DeckCopied instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckCopied DeserializeLengthDelimited(Stream stream)
		{
			DeckCopied deckCopied = new DeckCopied();
			DeserializeLengthDelimited(stream, deckCopied);
			return deckCopied;
		}

		public static DeckCopied DeserializeLengthDelimited(Stream stream, DeckCopied instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckCopied Deserialize(Stream stream, DeckCopied instance, long limit)
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
					if (instance.Player == null)
					{
						instance.Player = Player.DeserializeLengthDelimited(stream);
					}
					else
					{
						Player.DeserializeLengthDelimited(stream, instance.Player);
					}
					continue;
				case 18:
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 24:
					instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.DeckHash = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, DeckCopied instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasDeckId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			}
			if (instance.HasDeckHash)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeckHash));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPlayer)
			{
				num++;
				uint serializedSize = Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize2 = DeviceInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasDeckId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeckId);
			}
			if (HasDeckHash)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(DeckHash);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
