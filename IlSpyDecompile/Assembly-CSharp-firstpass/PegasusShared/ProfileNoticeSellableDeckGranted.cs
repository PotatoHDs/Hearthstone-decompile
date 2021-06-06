using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeSellableDeckGranted : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 28
		}

		public bool HasSellableDeckId;

		private int _SellableDeckId;

		public bool HasWasDeckGranted;

		private bool _WasDeckGranted;

		public bool HasPlayerDeckId;

		private long _PlayerDeckId;

		public int SellableDeckId
		{
			get
			{
				return _SellableDeckId;
			}
			set
			{
				_SellableDeckId = value;
				HasSellableDeckId = true;
			}
		}

		public bool WasDeckGranted
		{
			get
			{
				return _WasDeckGranted;
			}
			set
			{
				_WasDeckGranted = value;
				HasWasDeckGranted = true;
			}
		}

		public long PlayerDeckId
		{
			get
			{
				return _PlayerDeckId;
			}
			set
			{
				_PlayerDeckId = value;
				HasPlayerDeckId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSellableDeckId)
			{
				num ^= SellableDeckId.GetHashCode();
			}
			if (HasWasDeckGranted)
			{
				num ^= WasDeckGranted.GetHashCode();
			}
			if (HasPlayerDeckId)
			{
				num ^= PlayerDeckId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeSellableDeckGranted profileNoticeSellableDeckGranted = obj as ProfileNoticeSellableDeckGranted;
			if (profileNoticeSellableDeckGranted == null)
			{
				return false;
			}
			if (HasSellableDeckId != profileNoticeSellableDeckGranted.HasSellableDeckId || (HasSellableDeckId && !SellableDeckId.Equals(profileNoticeSellableDeckGranted.SellableDeckId)))
			{
				return false;
			}
			if (HasWasDeckGranted != profileNoticeSellableDeckGranted.HasWasDeckGranted || (HasWasDeckGranted && !WasDeckGranted.Equals(profileNoticeSellableDeckGranted.WasDeckGranted)))
			{
				return false;
			}
			if (HasPlayerDeckId != profileNoticeSellableDeckGranted.HasPlayerDeckId || (HasPlayerDeckId && !PlayerDeckId.Equals(profileNoticeSellableDeckGranted.PlayerDeckId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeSellableDeckGranted Deserialize(Stream stream, ProfileNoticeSellableDeckGranted instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeSellableDeckGranted DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeSellableDeckGranted profileNoticeSellableDeckGranted = new ProfileNoticeSellableDeckGranted();
			DeserializeLengthDelimited(stream, profileNoticeSellableDeckGranted);
			return profileNoticeSellableDeckGranted;
		}

		public static ProfileNoticeSellableDeckGranted DeserializeLengthDelimited(Stream stream, ProfileNoticeSellableDeckGranted instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeSellableDeckGranted Deserialize(Stream stream, ProfileNoticeSellableDeckGranted instance, long limit)
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
					instance.SellableDeckId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.WasDeckGranted = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.PlayerDeckId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeSellableDeckGranted instance)
		{
			if (instance.HasSellableDeckId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SellableDeckId);
			}
			if (instance.HasWasDeckGranted)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.WasDeckGranted);
			}
			if (instance.HasPlayerDeckId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerDeckId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSellableDeckId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SellableDeckId);
			}
			if (HasWasDeckGranted)
			{
				num++;
				num++;
			}
			if (HasPlayerDeckId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PlayerDeckId);
			}
			return num;
		}
	}
}
