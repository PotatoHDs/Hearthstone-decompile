using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011DC RID: 4572
	public class MASDKImportResult : IProtoBuf
	{
		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x0600CBD2 RID: 52178 RVA: 0x003CEBD1 File Offset: 0x003CCDD1
		// (set) Token: 0x0600CBD3 RID: 52179 RVA: 0x003CEBD9 File Offset: 0x003CCDD9
		public Player Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
				this.HasPlayer = (value != null);
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x0600CBD4 RID: 52180 RVA: 0x003CEBEC File Offset: 0x003CCDEC
		// (set) Token: 0x0600CBD5 RID: 52181 RVA: 0x003CEBF4 File Offset: 0x003CCDF4
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

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x0600CBD6 RID: 52182 RVA: 0x003CEC07 File Offset: 0x003CCE07
		// (set) Token: 0x0600CBD7 RID: 52183 RVA: 0x003CEC0F File Offset: 0x003CCE0F
		public MASDKImportResult.ImportResult Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				this._Result = value;
				this.HasResult = true;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x0600CBD8 RID: 52184 RVA: 0x003CEC1F File Offset: 0x003CCE1F
		// (set) Token: 0x0600CBD9 RID: 52185 RVA: 0x003CEC27 File Offset: 0x003CCE27
		public MASDKImportResult.ImportType ImportType_
		{
			get
			{
				return this._ImportType_;
			}
			set
			{
				this._ImportType_ = value;
				this.HasImportType_ = true;
			}
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x0600CBDA RID: 52186 RVA: 0x003CEC37 File Offset: 0x003CCE37
		// (set) Token: 0x0600CBDB RID: 52187 RVA: 0x003CEC3F File Offset: 0x003CCE3F
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

		// Token: 0x0600CBDC RID: 52188 RVA: 0x003CEC50 File Offset: 0x003CCE50
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasResult)
			{
				num ^= this.Result.GetHashCode();
			}
			if (this.HasImportType_)
			{
				num ^= this.ImportType_.GetHashCode();
			}
			if (this.HasErrorCode)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CBDD RID: 52189 RVA: 0x003CECF0 File Offset: 0x003CCEF0
		public override bool Equals(object obj)
		{
			MASDKImportResult masdkimportResult = obj as MASDKImportResult;
			return masdkimportResult != null && this.HasPlayer == masdkimportResult.HasPlayer && (!this.HasPlayer || this.Player.Equals(masdkimportResult.Player)) && this.HasDeviceInfo == masdkimportResult.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(masdkimportResult.DeviceInfo)) && this.HasResult == masdkimportResult.HasResult && (!this.HasResult || this.Result.Equals(masdkimportResult.Result)) && this.HasImportType_ == masdkimportResult.HasImportType_ && (!this.HasImportType_ || this.ImportType_.Equals(masdkimportResult.ImportType_)) && this.HasErrorCode == masdkimportResult.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(masdkimportResult.ErrorCode));
		}

		// Token: 0x0600CBDE RID: 52190 RVA: 0x003CEE00 File Offset: 0x003CD000
		public void Deserialize(Stream stream)
		{
			MASDKImportResult.Deserialize(stream, this);
		}

		// Token: 0x0600CBDF RID: 52191 RVA: 0x003CEE0A File Offset: 0x003CD00A
		public static MASDKImportResult Deserialize(Stream stream, MASDKImportResult instance)
		{
			return MASDKImportResult.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CBE0 RID: 52192 RVA: 0x003CEE18 File Offset: 0x003CD018
		public static MASDKImportResult DeserializeLengthDelimited(Stream stream)
		{
			MASDKImportResult masdkimportResult = new MASDKImportResult();
			MASDKImportResult.DeserializeLengthDelimited(stream, masdkimportResult);
			return masdkimportResult;
		}

		// Token: 0x0600CBE1 RID: 52193 RVA: 0x003CEE34 File Offset: 0x003CD034
		public static MASDKImportResult DeserializeLengthDelimited(Stream stream, MASDKImportResult instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MASDKImportResult.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CBE2 RID: 52194 RVA: 0x003CEE5C File Offset: 0x003CD05C
		public static MASDKImportResult Deserialize(Stream stream, MASDKImportResult instance, long limit)
		{
			instance.Result = MASDKImportResult.ImportResult.SUCCESS;
			instance.ImportType_ = MASDKImportResult.ImportType.GUEST_ACCOUNT_ID;
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
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
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
							if (instance.Player == null)
							{
								instance.Player = Player.DeserializeLengthDelimited(stream);
								continue;
							}
							Player.DeserializeLengthDelimited(stream, instance.Player);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Result = (MASDKImportResult.ImportResult)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.ImportType_ = (MASDKImportResult.ImportType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
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

		// Token: 0x0600CBE3 RID: 52195 RVA: 0x003CEF91 File Offset: 0x003CD191
		public void Serialize(Stream stream)
		{
			MASDKImportResult.Serialize(stream, this);
		}

		// Token: 0x0600CBE4 RID: 52196 RVA: 0x003CEF9C File Offset: 0x003CD19C
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
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Result));
			}
			if (instance.HasImportType_)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ImportType_));
			}
			if (instance.HasErrorCode)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			}
		}

		// Token: 0x0600CBE5 RID: 52197 RVA: 0x003CF05C File Offset: 0x003CD25C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize2 = this.DeviceInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasResult)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Result));
			}
			if (this.HasImportType_)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ImportType_));
			}
			if (this.HasErrorCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			}
			return num;
		}

		// Token: 0x0400A057 RID: 41047
		public bool HasPlayer;

		// Token: 0x0400A058 RID: 41048
		private Player _Player;

		// Token: 0x0400A059 RID: 41049
		public bool HasDeviceInfo;

		// Token: 0x0400A05A RID: 41050
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A05B RID: 41051
		public bool HasResult;

		// Token: 0x0400A05C RID: 41052
		private MASDKImportResult.ImportResult _Result;

		// Token: 0x0400A05D RID: 41053
		public bool HasImportType_;

		// Token: 0x0400A05E RID: 41054
		private MASDKImportResult.ImportType _ImportType_;

		// Token: 0x0400A05F RID: 41055
		public bool HasErrorCode;

		// Token: 0x0400A060 RID: 41056
		private int _ErrorCode;

		// Token: 0x02002947 RID: 10567
		public enum ImportResult
		{
			// Token: 0x0400FC76 RID: 64630
			SUCCESS,
			// Token: 0x0400FC77 RID: 64631
			FAILURE
		}

		// Token: 0x02002948 RID: 10568
		public enum ImportType
		{
			// Token: 0x0400FC79 RID: 64633
			GUEST_ACCOUNT_ID,
			// Token: 0x0400FC7A RID: 64634
			AUTH_TOKEN
		}
	}
}
