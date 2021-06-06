using System.IO;

namespace PegasusUtil
{
	public class SetFavoriteCoinResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 610
		}

		public bool HasSuccess;

		private bool _Success;

		public bool HasCoinId;

		private int _CoinId;

		public bool Success
		{
			get
			{
				return _Success;
			}
			set
			{
				_Success = value;
				HasSuccess = true;
			}
		}

		public int CoinId
		{
			get
			{
				return _CoinId;
			}
			set
			{
				_CoinId = value;
				HasCoinId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSuccess)
			{
				num ^= Success.GetHashCode();
			}
			if (HasCoinId)
			{
				num ^= CoinId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SetFavoriteCoinResponse setFavoriteCoinResponse = obj as SetFavoriteCoinResponse;
			if (setFavoriteCoinResponse == null)
			{
				return false;
			}
			if (HasSuccess != setFavoriteCoinResponse.HasSuccess || (HasSuccess && !Success.Equals(setFavoriteCoinResponse.Success)))
			{
				return false;
			}
			if (HasCoinId != setFavoriteCoinResponse.HasCoinId || (HasCoinId && !CoinId.Equals(setFavoriteCoinResponse.CoinId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SetFavoriteCoinResponse Deserialize(Stream stream, SetFavoriteCoinResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SetFavoriteCoinResponse DeserializeLengthDelimited(Stream stream)
		{
			SetFavoriteCoinResponse setFavoriteCoinResponse = new SetFavoriteCoinResponse();
			DeserializeLengthDelimited(stream, setFavoriteCoinResponse);
			return setFavoriteCoinResponse;
		}

		public static SetFavoriteCoinResponse DeserializeLengthDelimited(Stream stream, SetFavoriteCoinResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SetFavoriteCoinResponse Deserialize(Stream stream, SetFavoriteCoinResponse instance, long limit)
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
					instance.Success = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.CoinId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SetFavoriteCoinResponse instance)
		{
			if (instance.HasSuccess)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.Success);
			}
			if (instance.HasCoinId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CoinId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSuccess)
			{
				num++;
				num++;
			}
			if (HasCoinId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CoinId);
			}
			return num;
		}
	}
}
