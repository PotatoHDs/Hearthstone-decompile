using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011D5 RID: 4565
	public class LocaleDataUpdateFailed : IProtoBuf
	{
		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x0600CB57 RID: 52055 RVA: 0x003CD1DC File Offset: 0x003CB3DC
		// (set) Token: 0x0600CB58 RID: 52056 RVA: 0x003CD1E4 File Offset: 0x003CB3E4
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

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x0600CB59 RID: 52057 RVA: 0x003CD1F7 File Offset: 0x003CB3F7
		// (set) Token: 0x0600CB5A RID: 52058 RVA: 0x003CD1FF File Offset: 0x003CB3FF
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

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x0600CB5B RID: 52059 RVA: 0x003CD20F File Offset: 0x003CB40F
		// (set) Token: 0x0600CB5C RID: 52060 RVA: 0x003CD217 File Offset: 0x003CB417
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

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x0600CB5D RID: 52061 RVA: 0x003CD227 File Offset: 0x003CB427
		// (set) Token: 0x0600CB5E RID: 52062 RVA: 0x003CD22F File Offset: 0x003CB42F
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

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x0600CB5F RID: 52063 RVA: 0x003CD23F File Offset: 0x003CB43F
		// (set) Token: 0x0600CB60 RID: 52064 RVA: 0x003CD247 File Offset: 0x003CB447
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

		// Token: 0x0600CB61 RID: 52065 RVA: 0x003CD258 File Offset: 0x003CB458
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

		// Token: 0x0600CB62 RID: 52066 RVA: 0x003CD2EC File Offset: 0x003CB4EC
		public override bool Equals(object obj)
		{
			LocaleDataUpdateFailed localeDataUpdateFailed = obj as LocaleDataUpdateFailed;
			return localeDataUpdateFailed != null && this.HasDeviceInfo == localeDataUpdateFailed.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(localeDataUpdateFailed.DeviceInfo)) && this.HasDuration == localeDataUpdateFailed.HasDuration && (!this.HasDuration || this.Duration.Equals(localeDataUpdateFailed.Duration)) && this.HasRealDownloadBytes == localeDataUpdateFailed.HasRealDownloadBytes && (!this.HasRealDownloadBytes || this.RealDownloadBytes.Equals(localeDataUpdateFailed.RealDownloadBytes)) && this.HasExpectedDownloadBytes == localeDataUpdateFailed.HasExpectedDownloadBytes && (!this.HasExpectedDownloadBytes || this.ExpectedDownloadBytes.Equals(localeDataUpdateFailed.ExpectedDownloadBytes)) && this.HasErrorCode == localeDataUpdateFailed.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(localeDataUpdateFailed.ErrorCode));
		}

		// Token: 0x0600CB63 RID: 52067 RVA: 0x003CD3E9 File Offset: 0x003CB5E9
		public void Deserialize(Stream stream)
		{
			LocaleDataUpdateFailed.Deserialize(stream, this);
		}

		// Token: 0x0600CB64 RID: 52068 RVA: 0x003CD3F3 File Offset: 0x003CB5F3
		public static LocaleDataUpdateFailed Deserialize(Stream stream, LocaleDataUpdateFailed instance)
		{
			return LocaleDataUpdateFailed.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CB65 RID: 52069 RVA: 0x003CD400 File Offset: 0x003CB600
		public static LocaleDataUpdateFailed DeserializeLengthDelimited(Stream stream)
		{
			LocaleDataUpdateFailed localeDataUpdateFailed = new LocaleDataUpdateFailed();
			LocaleDataUpdateFailed.DeserializeLengthDelimited(stream, localeDataUpdateFailed);
			return localeDataUpdateFailed;
		}

		// Token: 0x0600CB66 RID: 52070 RVA: 0x003CD41C File Offset: 0x003CB61C
		public static LocaleDataUpdateFailed DeserializeLengthDelimited(Stream stream, LocaleDataUpdateFailed instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LocaleDataUpdateFailed.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CB67 RID: 52071 RVA: 0x003CD444 File Offset: 0x003CB644
		public static LocaleDataUpdateFailed Deserialize(Stream stream, LocaleDataUpdateFailed instance, long limit)
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

		// Token: 0x0600CB68 RID: 52072 RVA: 0x003CD550 File Offset: 0x003CB750
		public void Serialize(Stream stream)
		{
			LocaleDataUpdateFailed.Serialize(stream, this);
		}

		// Token: 0x0600CB69 RID: 52073 RVA: 0x003CD55C File Offset: 0x003CB75C
		public static void Serialize(Stream stream, LocaleDataUpdateFailed instance)
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

		// Token: 0x0600CB6A RID: 52074 RVA: 0x003CD610 File Offset: 0x003CB810
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

		// Token: 0x0400A029 RID: 41001
		public bool HasDeviceInfo;

		// Token: 0x0400A02A RID: 41002
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A02B RID: 41003
		public bool HasDuration;

		// Token: 0x0400A02C RID: 41004
		private float _Duration;

		// Token: 0x0400A02D RID: 41005
		public bool HasRealDownloadBytes;

		// Token: 0x0400A02E RID: 41006
		private long _RealDownloadBytes;

		// Token: 0x0400A02F RID: 41007
		public bool HasExpectedDownloadBytes;

		// Token: 0x0400A030 RID: 41008
		private long _ExpectedDownloadBytes;

		// Token: 0x0400A031 RID: 41009
		public bool HasErrorCode;

		// Token: 0x0400A032 RID: 41010
		private int _ErrorCode;
	}
}
