using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class MASDKAuthResult : IProtoBuf
	{
		public enum AuthResult
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

		private AuthResult _Result;

		public bool HasErrorCode;

		private int _ErrorCode;

		public bool HasSource;

		private string _Source;

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

		public AuthResult Result
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

		public int ErrorCode
		{
			get
			{
				return _ErrorCode;
			}
			set
			{
				_ErrorCode = value;
				HasErrorCode = true;
			}
		}

		public string Source
		{
			get
			{
				return _Source;
			}
			set
			{
				_Source = value;
				HasSource = value != null;
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
			if (HasErrorCode)
			{
				num ^= ErrorCode.GetHashCode();
			}
			if (HasSource)
			{
				num ^= Source.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MASDKAuthResult mASDKAuthResult = obj as MASDKAuthResult;
			if (mASDKAuthResult == null)
			{
				return false;
			}
			if (HasPlayer != mASDKAuthResult.HasPlayer || (HasPlayer && !Player.Equals(mASDKAuthResult.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != mASDKAuthResult.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(mASDKAuthResult.DeviceInfo)))
			{
				return false;
			}
			if (HasResult != mASDKAuthResult.HasResult || (HasResult && !Result.Equals(mASDKAuthResult.Result)))
			{
				return false;
			}
			if (HasErrorCode != mASDKAuthResult.HasErrorCode || (HasErrorCode && !ErrorCode.Equals(mASDKAuthResult.ErrorCode)))
			{
				return false;
			}
			if (HasSource != mASDKAuthResult.HasSource || (HasSource && !Source.Equals(mASDKAuthResult.Source)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MASDKAuthResult Deserialize(Stream stream, MASDKAuthResult instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MASDKAuthResult DeserializeLengthDelimited(Stream stream)
		{
			MASDKAuthResult mASDKAuthResult = new MASDKAuthResult();
			DeserializeLengthDelimited(stream, mASDKAuthResult);
			return mASDKAuthResult;
		}

		public static MASDKAuthResult DeserializeLengthDelimited(Stream stream, MASDKAuthResult instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MASDKAuthResult Deserialize(Stream stream, MASDKAuthResult instance, long limit)
		{
			instance.Result = AuthResult.SUCCESS;
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
					instance.Result = (AuthResult)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.ErrorCode = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					instance.Source = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, MASDKAuthResult instance)
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
			if (instance.HasErrorCode)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
			}
			if (instance.HasSource)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Source));
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
			if (HasErrorCode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode);
			}
			if (HasSource)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Source);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
