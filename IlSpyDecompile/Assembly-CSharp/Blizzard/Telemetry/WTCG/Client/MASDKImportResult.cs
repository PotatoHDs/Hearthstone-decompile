using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class MASDKImportResult : IProtoBuf
	{
		public enum ImportResult
		{
			SUCCESS,
			FAILURE
		}

		public enum ImportType
		{
			GUEST_ACCOUNT_ID,
			AUTH_TOKEN
		}

		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasResult;

		private ImportResult _Result;

		public bool HasImportType_;

		private ImportType _ImportType_;

		public bool HasErrorCode;

		private int _ErrorCode;

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

		public ImportResult Result
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

		public ImportType ImportType_
		{
			get
			{
				return _ImportType_;
			}
			set
			{
				_ImportType_ = value;
				HasImportType_ = true;
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
			if (HasImportType_)
			{
				num ^= ImportType_.GetHashCode();
			}
			if (HasErrorCode)
			{
				num ^= ErrorCode.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MASDKImportResult mASDKImportResult = obj as MASDKImportResult;
			if (mASDKImportResult == null)
			{
				return false;
			}
			if (HasPlayer != mASDKImportResult.HasPlayer || (HasPlayer && !Player.Equals(mASDKImportResult.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != mASDKImportResult.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(mASDKImportResult.DeviceInfo)))
			{
				return false;
			}
			if (HasResult != mASDKImportResult.HasResult || (HasResult && !Result.Equals(mASDKImportResult.Result)))
			{
				return false;
			}
			if (HasImportType_ != mASDKImportResult.HasImportType_ || (HasImportType_ && !ImportType_.Equals(mASDKImportResult.ImportType_)))
			{
				return false;
			}
			if (HasErrorCode != mASDKImportResult.HasErrorCode || (HasErrorCode && !ErrorCode.Equals(mASDKImportResult.ErrorCode)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MASDKImportResult Deserialize(Stream stream, MASDKImportResult instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MASDKImportResult DeserializeLengthDelimited(Stream stream)
		{
			MASDKImportResult mASDKImportResult = new MASDKImportResult();
			DeserializeLengthDelimited(stream, mASDKImportResult);
			return mASDKImportResult;
		}

		public static MASDKImportResult DeserializeLengthDelimited(Stream stream, MASDKImportResult instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MASDKImportResult Deserialize(Stream stream, MASDKImportResult instance, long limit)
		{
			instance.Result = ImportResult.SUCCESS;
			instance.ImportType_ = ImportType.GUEST_ACCOUNT_ID;
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
					instance.Result = (ImportResult)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.ImportType_ = (ImportType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.ErrorCode = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, MASDKImportResult instance)
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
			if (instance.HasImportType_)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ImportType_);
			}
			if (instance.HasErrorCode)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
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
			if (HasImportType_)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ImportType_);
			}
			if (HasErrorCode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode);
			}
			return num;
		}
	}
}
