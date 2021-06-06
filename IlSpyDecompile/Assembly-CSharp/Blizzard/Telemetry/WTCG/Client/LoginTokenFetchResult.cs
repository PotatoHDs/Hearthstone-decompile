using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class LoginTokenFetchResult : IProtoBuf
	{
		public enum TokenFetchResult
		{
			SUCCESS,
			CANCELED,
			FAILURE
		}

		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasResult;

		private TokenFetchResult _Result;

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

		public TokenFetchResult Result
		{
			get
			{
				return _Result;
			}
			set
			{
				_Result = value;
				HasResult = true;
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
			if (HasResult)
			{
				num ^= Result.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			LoginTokenFetchResult loginTokenFetchResult = obj as LoginTokenFetchResult;
			if (loginTokenFetchResult == null)
			{
				return false;
			}
			if (HasPlayer != loginTokenFetchResult.HasPlayer || (HasPlayer && !Player.Equals(loginTokenFetchResult.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != loginTokenFetchResult.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(loginTokenFetchResult.DeviceInfo)))
			{
				return false;
			}
			if (HasResult != loginTokenFetchResult.HasResult || (HasResult && !Result.Equals(loginTokenFetchResult.Result)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static LoginTokenFetchResult Deserialize(Stream stream, LoginTokenFetchResult instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static LoginTokenFetchResult DeserializeLengthDelimited(Stream stream)
		{
			LoginTokenFetchResult loginTokenFetchResult = new LoginTokenFetchResult();
			DeserializeLengthDelimited(stream, loginTokenFetchResult);
			return loginTokenFetchResult;
		}

		public static LoginTokenFetchResult DeserializeLengthDelimited(Stream stream, LoginTokenFetchResult instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static LoginTokenFetchResult Deserialize(Stream stream, LoginTokenFetchResult instance, long limit)
		{
			instance.Result = TokenFetchResult.SUCCESS;
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
					instance.Result = (TokenFetchResult)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, LoginTokenFetchResult instance)
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
			if (instance.HasResult)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Result);
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
			if (HasResult)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Result);
			}
			return num;
		}
	}
}
