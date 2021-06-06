using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011B0 RID: 4528
	public class DataUpdateFailed : IProtoBuf
	{
		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x0600C886 RID: 51334 RVA: 0x003C2E75 File Offset: 0x003C1075
		// (set) Token: 0x0600C887 RID: 51335 RVA: 0x003C2E7D File Offset: 0x003C107D
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

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x0600C888 RID: 51336 RVA: 0x003C2E90 File Offset: 0x003C1090
		// (set) Token: 0x0600C889 RID: 51337 RVA: 0x003C2E98 File Offset: 0x003C1098
		public float Duration
		{
			get
			{
				return this._Duration;
			}
			set
			{
				this._Duration = value;
				this.HasDuration = true;
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x0600C88A RID: 51338 RVA: 0x003C2EA8 File Offset: 0x003C10A8
		// (set) Token: 0x0600C88B RID: 51339 RVA: 0x003C2EB0 File Offset: 0x003C10B0
		public long RealDownloadBytes
		{
			get
			{
				return this._RealDownloadBytes;
			}
			set
			{
				this._RealDownloadBytes = value;
				this.HasRealDownloadBytes = true;
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x0600C88C RID: 51340 RVA: 0x003C2EC0 File Offset: 0x003C10C0
		// (set) Token: 0x0600C88D RID: 51341 RVA: 0x003C2EC8 File Offset: 0x003C10C8
		public long ExpectedDownloadBytes
		{
			get
			{
				return this._ExpectedDownloadBytes;
			}
			set
			{
				this._ExpectedDownloadBytes = value;
				this.HasExpectedDownloadBytes = true;
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x0600C88E RID: 51342 RVA: 0x003C2ED8 File Offset: 0x003C10D8
		// (set) Token: 0x0600C88F RID: 51343 RVA: 0x003C2EE0 File Offset: 0x003C10E0
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

		// Token: 0x0600C890 RID: 51344 RVA: 0x003C2EF0 File Offset: 0x003C10F0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasDuration)
			{
				num ^= this.Duration.GetHashCode();
			}
			if (this.HasRealDownloadBytes)
			{
				num ^= this.RealDownloadBytes.GetHashCode();
			}
			if (this.HasExpectedDownloadBytes)
			{
				num ^= this.ExpectedDownloadBytes.GetHashCode();
			}
			if (this.HasErrorCode)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C891 RID: 51345 RVA: 0x003C2F84 File Offset: 0x003C1184
		public override bool Equals(object obj)
		{
			DataUpdateFailed dataUpdateFailed = obj as DataUpdateFailed;
			return dataUpdateFailed != null && this.HasDeviceInfo == dataUpdateFailed.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(dataUpdateFailed.DeviceInfo)) && this.HasDuration == dataUpdateFailed.HasDuration && (!this.HasDuration || this.Duration.Equals(dataUpdateFailed.Duration)) && this.HasRealDownloadBytes == dataUpdateFailed.HasRealDownloadBytes && (!this.HasRealDownloadBytes || this.RealDownloadBytes.Equals(dataUpdateFailed.RealDownloadBytes)) && this.HasExpectedDownloadBytes == dataUpdateFailed.HasExpectedDownloadBytes && (!this.HasExpectedDownloadBytes || this.ExpectedDownloadBytes.Equals(dataUpdateFailed.ExpectedDownloadBytes)) && this.HasErrorCode == dataUpdateFailed.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(dataUpdateFailed.ErrorCode));
		}

		// Token: 0x0600C892 RID: 51346 RVA: 0x003C3081 File Offset: 0x003C1281
		public void Deserialize(Stream stream)
		{
			DataUpdateFailed.Deserialize(stream, this);
		}

		// Token: 0x0600C893 RID: 51347 RVA: 0x003C308B File Offset: 0x003C128B
		public static DataUpdateFailed Deserialize(Stream stream, DataUpdateFailed instance)
		{
			return DataUpdateFailed.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C894 RID: 51348 RVA: 0x003C3098 File Offset: 0x003C1298
		public static DataUpdateFailed DeserializeLengthDelimited(Stream stream)
		{
			DataUpdateFailed dataUpdateFailed = new DataUpdateFailed();
			DataUpdateFailed.DeserializeLengthDelimited(stream, dataUpdateFailed);
			return dataUpdateFailed;
		}

		// Token: 0x0600C895 RID: 51349 RVA: 0x003C30B4 File Offset: 0x003C12B4
		public static DataUpdateFailed DeserializeLengthDelimited(Stream stream, DataUpdateFailed instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DataUpdateFailed.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C896 RID: 51350 RVA: 0x003C30DC File Offset: 0x003C12DC
		public static DataUpdateFailed Deserialize(Stream stream, DataUpdateFailed instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num <= 21)
					{
						if (num != 10)
						{
							if (num == 21)
							{
								instance.Duration = binaryReader.ReadSingle();
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
						if (num == 24)
						{
							instance.RealDownloadBytes = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.ExpectedDownloadBytes = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600C897 RID: 51351 RVA: 0x003C31E8 File Offset: 0x003C13E8
		public void Serialize(Stream stream)
		{
			DataUpdateFailed.Serialize(stream, this);
		}

		// Token: 0x0600C898 RID: 51352 RVA: 0x003C31F4 File Offset: 0x003C13F4
		public static void Serialize(Stream stream, DataUpdateFailed instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasDuration)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Duration);
			}
			if (instance.HasRealDownloadBytes)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RealDownloadBytes);
			}
			if (instance.HasExpectedDownloadBytes)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ExpectedDownloadBytes);
			}
			if (instance.HasErrorCode)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			}
		}

		// Token: 0x0600C899 RID: 51353 RVA: 0x003C32A8 File Offset: 0x003C14A8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasDuration)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasRealDownloadBytes)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.RealDownloadBytes);
			}
			if (this.HasExpectedDownloadBytes)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ExpectedDownloadBytes);
			}
			if (this.HasErrorCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			}
			return num;
		}

		// Token: 0x04009EEF RID: 40687
		public bool HasDeviceInfo;

		// Token: 0x04009EF0 RID: 40688
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009EF1 RID: 40689
		public bool HasDuration;

		// Token: 0x04009EF2 RID: 40690
		private float _Duration;

		// Token: 0x04009EF3 RID: 40691
		public bool HasRealDownloadBytes;

		// Token: 0x04009EF4 RID: 40692
		private long _RealDownloadBytes;

		// Token: 0x04009EF5 RID: 40693
		public bool HasExpectedDownloadBytes;

		// Token: 0x04009EF6 RID: 40694
		private long _ExpectedDownloadBytes;

		// Token: 0x04009EF7 RID: 40695
		public bool HasErrorCode;

		// Token: 0x04009EF8 RID: 40696
		private int _ErrorCode;
	}
}
