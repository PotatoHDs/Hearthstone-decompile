using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011B2 RID: 4530
	public class DataUpdateProgress : IProtoBuf
	{
		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x0600C8AE RID: 51374 RVA: 0x003C374F File Offset: 0x003C194F
		// (set) Token: 0x0600C8AF RID: 51375 RVA: 0x003C3757 File Offset: 0x003C1957
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

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x0600C8B0 RID: 51376 RVA: 0x003C376A File Offset: 0x003C196A
		// (set) Token: 0x0600C8B1 RID: 51377 RVA: 0x003C3772 File Offset: 0x003C1972
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

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x0600C8B2 RID: 51378 RVA: 0x003C3782 File Offset: 0x003C1982
		// (set) Token: 0x0600C8B3 RID: 51379 RVA: 0x003C378A File Offset: 0x003C198A
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

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x0600C8B4 RID: 51380 RVA: 0x003C379A File Offset: 0x003C199A
		// (set) Token: 0x0600C8B5 RID: 51381 RVA: 0x003C37A2 File Offset: 0x003C19A2
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

		// Token: 0x0600C8B6 RID: 51382 RVA: 0x003C37B4 File Offset: 0x003C19B4
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
			return num;
		}

		// Token: 0x0600C8B7 RID: 51383 RVA: 0x003C3830 File Offset: 0x003C1A30
		public override bool Equals(object obj)
		{
			DataUpdateProgress dataUpdateProgress = obj as DataUpdateProgress;
			return dataUpdateProgress != null && this.HasDeviceInfo == dataUpdateProgress.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(dataUpdateProgress.DeviceInfo)) && this.HasDuration == dataUpdateProgress.HasDuration && (!this.HasDuration || this.Duration.Equals(dataUpdateProgress.Duration)) && this.HasRealDownloadBytes == dataUpdateProgress.HasRealDownloadBytes && (!this.HasRealDownloadBytes || this.RealDownloadBytes.Equals(dataUpdateProgress.RealDownloadBytes)) && this.HasExpectedDownloadBytes == dataUpdateProgress.HasExpectedDownloadBytes && (!this.HasExpectedDownloadBytes || this.ExpectedDownloadBytes.Equals(dataUpdateProgress.ExpectedDownloadBytes));
		}

		// Token: 0x0600C8B8 RID: 51384 RVA: 0x003C38FF File Offset: 0x003C1AFF
		public void Deserialize(Stream stream)
		{
			DataUpdateProgress.Deserialize(stream, this);
		}

		// Token: 0x0600C8B9 RID: 51385 RVA: 0x003C3909 File Offset: 0x003C1B09
		public static DataUpdateProgress Deserialize(Stream stream, DataUpdateProgress instance)
		{
			return DataUpdateProgress.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C8BA RID: 51386 RVA: 0x003C3914 File Offset: 0x003C1B14
		public static DataUpdateProgress DeserializeLengthDelimited(Stream stream)
		{
			DataUpdateProgress dataUpdateProgress = new DataUpdateProgress();
			DataUpdateProgress.DeserializeLengthDelimited(stream, dataUpdateProgress);
			return dataUpdateProgress;
		}

		// Token: 0x0600C8BB RID: 51387 RVA: 0x003C3930 File Offset: 0x003C1B30
		public static DataUpdateProgress DeserializeLengthDelimited(Stream stream, DataUpdateProgress instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DataUpdateProgress.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C8BC RID: 51388 RVA: 0x003C3958 File Offset: 0x003C1B58
		public static DataUpdateProgress Deserialize(Stream stream, DataUpdateProgress instance, long limit)
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

		// Token: 0x0600C8BD RID: 51389 RVA: 0x003C3A4A File Offset: 0x003C1C4A
		public void Serialize(Stream stream)
		{
			DataUpdateProgress.Serialize(stream, this);
		}

		// Token: 0x0600C8BE RID: 51390 RVA: 0x003C3A54 File Offset: 0x003C1C54
		public static void Serialize(Stream stream, DataUpdateProgress instance)
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
		}

		// Token: 0x0600C8BF RID: 51391 RVA: 0x003C3AEC File Offset: 0x003C1CEC
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
			return num;
		}

		// Token: 0x04009F01 RID: 40705
		public bool HasDeviceInfo;

		// Token: 0x04009F02 RID: 40706
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F03 RID: 40707
		public bool HasDuration;

		// Token: 0x04009F04 RID: 40708
		private float _Duration;

		// Token: 0x04009F05 RID: 40709
		public bool HasRealDownloadBytes;

		// Token: 0x04009F06 RID: 40710
		private long _RealDownloadBytes;

		// Token: 0x04009F07 RID: 40711
		public bool HasExpectedDownloadBytes;

		// Token: 0x04009F08 RID: 40712
		private long _ExpectedDownloadBytes;
	}
}
