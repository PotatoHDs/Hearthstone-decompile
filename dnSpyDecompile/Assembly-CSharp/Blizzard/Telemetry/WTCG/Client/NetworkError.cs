using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011E0 RID: 4576
	public class NetworkError : IProtoBuf
	{
		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x0600CC1E RID: 52254 RVA: 0x003CFD0E File Offset: 0x003CDF0E
		// (set) Token: 0x0600CC1F RID: 52255 RVA: 0x003CFD16 File Offset: 0x003CDF16
		public DeviceInfo DeviceInfo
		{
			get
			{
				return this._DeviceInfo;
			}
			set
			{
				this._DeviceInfo = value;
				this.HasDeviceInfo = (value != null);
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x0600CC20 RID: 52256 RVA: 0x003CFD29 File Offset: 0x003CDF29
		// (set) Token: 0x0600CC21 RID: 52257 RVA: 0x003CFD31 File Offset: 0x003CDF31
		public NetworkError.ErrorType ErrorType_
		{
			get
			{
				return this._ErrorType_;
			}
			set
			{
				this._ErrorType_ = value;
				this.HasErrorType_ = true;
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x0600CC22 RID: 52258 RVA: 0x003CFD41 File Offset: 0x003CDF41
		// (set) Token: 0x0600CC23 RID: 52259 RVA: 0x003CFD49 File Offset: 0x003CDF49
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
				this.HasDescription = (value != null);
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x0600CC24 RID: 52260 RVA: 0x003CFD5C File Offset: 0x003CDF5C
		// (set) Token: 0x0600CC25 RID: 52261 RVA: 0x003CFD64 File Offset: 0x003CDF64
		public int ErrorCode
		{
			get
			{
				return this._ErrorCode;
			}
			set
			{
				this._ErrorCode = value;
				this.HasErrorCode = true;
			}
		}

		// Token: 0x0600CC26 RID: 52262 RVA: 0x003CFD74 File Offset: 0x003CDF74
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasErrorType_)
			{
				num ^= this.ErrorType_.GetHashCode();
			}
			if (this.HasDescription)
			{
				num ^= this.Description.GetHashCode();
			}
			if (this.HasErrorCode)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CC27 RID: 52263 RVA: 0x003CFDF4 File Offset: 0x003CDFF4
		public override bool Equals(object obj)
		{
			NetworkError networkError = obj as NetworkError;
			return networkError != null && this.HasDeviceInfo == networkError.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(networkError.DeviceInfo)) && this.HasErrorType_ == networkError.HasErrorType_ && (!this.HasErrorType_ || this.ErrorType_.Equals(networkError.ErrorType_)) && this.HasDescription == networkError.HasDescription && (!this.HasDescription || this.Description.Equals(networkError.Description)) && this.HasErrorCode == networkError.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(networkError.ErrorCode));
		}

		// Token: 0x0600CC28 RID: 52264 RVA: 0x003CFECB File Offset: 0x003CE0CB
		public void Deserialize(Stream stream)
		{
			NetworkError.Deserialize(stream, this);
		}

		// Token: 0x0600CC29 RID: 52265 RVA: 0x003CFED5 File Offset: 0x003CE0D5
		public static NetworkError Deserialize(Stream stream, NetworkError instance)
		{
			return NetworkError.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CC2A RID: 52266 RVA: 0x003CFEE0 File Offset: 0x003CE0E0
		public static NetworkError DeserializeLengthDelimited(Stream stream)
		{
			NetworkError networkError = new NetworkError();
			NetworkError.DeserializeLengthDelimited(stream, networkError);
			return networkError;
		}

		// Token: 0x0600CC2B RID: 52267 RVA: 0x003CFEFC File Offset: 0x003CE0FC
		public static NetworkError DeserializeLengthDelimited(Stream stream, NetworkError instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return NetworkError.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CC2C RID: 52268 RVA: 0x003CFF24 File Offset: 0x003CE124
		public static NetworkError Deserialize(Stream stream, NetworkError instance, long limit)
		{
			instance.ErrorType_ = NetworkError.ErrorType.PRIVATE_SERVER;
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					if (num <= 16)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.ErrorType_ = (NetworkError.ErrorType)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.DeviceInfo == null)
							{
								instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
								continue;
							}
							DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Description = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 32)
						{
							instance.ErrorCode = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CC2D RID: 52269 RVA: 0x003D0018 File Offset: 0x003CE218
		public void Serialize(Stream stream)
		{
			NetworkError.Serialize(stream, this);
		}

		// Token: 0x0600CC2E RID: 52270 RVA: 0x003D0024 File Offset: 0x003CE224
		public static void Serialize(Stream stream, NetworkError instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasErrorType_)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorType_));
			}
			if (instance.HasDescription)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Description));
			}
			if (instance.HasErrorCode)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			}
		}

		// Token: 0x0600CC2F RID: 52271 RVA: 0x003D00C0 File Offset: 0x003CE2C0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasErrorType_)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorType_));
			}
			if (this.HasDescription)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Description);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasErrorCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			}
			return num;
		}

		// Token: 0x0400A077 RID: 41079
		public bool HasDeviceInfo;

		// Token: 0x0400A078 RID: 41080
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A079 RID: 41081
		public bool HasErrorType_;

		// Token: 0x0400A07A RID: 41082
		private NetworkError.ErrorType _ErrorType_;

		// Token: 0x0400A07B RID: 41083
		public bool HasDescription;

		// Token: 0x0400A07C RID: 41084
		private string _Description;

		// Token: 0x0400A07D RID: 41085
		public bool HasErrorCode;

		// Token: 0x0400A07E RID: 41086
		private int _ErrorCode;

		// Token: 0x02002949 RID: 10569
		public enum ErrorType
		{
			// Token: 0x0400FC7C RID: 64636
			PRIVATE_SERVER = 1,
			// Token: 0x0400FC7D RID: 64637
			SERVICE_UNAVAILABLE,
			// Token: 0x0400FC7E RID: 64638
			PEER_UNAVAILABLE,
			// Token: 0x0400FC7F RID: 64639
			TIMEOUT_DEFERRED_RESPONSE,
			// Token: 0x0400FC80 RID: 64640
			TIMEOUT_NOT_DEFERRED_RESPONSE,
			// Token: 0x0400FC81 RID: 64641
			REQUEST_ERROR,
			// Token: 0x0400FC82 RID: 64642
			OTHER_UNKNOWN
		}
	}
}
