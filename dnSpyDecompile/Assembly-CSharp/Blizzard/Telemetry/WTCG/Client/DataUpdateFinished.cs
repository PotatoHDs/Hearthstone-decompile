using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011B1 RID: 4529
	public class DataUpdateFinished : IProtoBuf
	{
		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x0600C89B RID: 51355 RVA: 0x003C333A File Offset: 0x003C153A
		// (set) Token: 0x0600C89C RID: 51356 RVA: 0x003C3342 File Offset: 0x003C1542
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

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x0600C89D RID: 51357 RVA: 0x003C3355 File Offset: 0x003C1555
		// (set) Token: 0x0600C89E RID: 51358 RVA: 0x003C335D File Offset: 0x003C155D
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

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x0600C89F RID: 51359 RVA: 0x003C336D File Offset: 0x003C156D
		// (set) Token: 0x0600C8A0 RID: 51360 RVA: 0x003C3375 File Offset: 0x003C1575
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

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x0600C8A1 RID: 51361 RVA: 0x003C3385 File Offset: 0x003C1585
		// (set) Token: 0x0600C8A2 RID: 51362 RVA: 0x003C338D File Offset: 0x003C158D
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

		// Token: 0x0600C8A3 RID: 51363 RVA: 0x003C33A0 File Offset: 0x003C15A0
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

		// Token: 0x0600C8A4 RID: 51364 RVA: 0x003C341C File Offset: 0x003C161C
		public override bool Equals(object obj)
		{
			DataUpdateFinished dataUpdateFinished = obj as DataUpdateFinished;
			return dataUpdateFinished != null && this.HasDeviceInfo == dataUpdateFinished.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(dataUpdateFinished.DeviceInfo)) && this.HasDuration == dataUpdateFinished.HasDuration && (!this.HasDuration || this.Duration.Equals(dataUpdateFinished.Duration)) && this.HasRealDownloadBytes == dataUpdateFinished.HasRealDownloadBytes && (!this.HasRealDownloadBytes || this.RealDownloadBytes.Equals(dataUpdateFinished.RealDownloadBytes)) && this.HasExpectedDownloadBytes == dataUpdateFinished.HasExpectedDownloadBytes && (!this.HasExpectedDownloadBytes || this.ExpectedDownloadBytes.Equals(dataUpdateFinished.ExpectedDownloadBytes));
		}

		// Token: 0x0600C8A5 RID: 51365 RVA: 0x003C34EB File Offset: 0x003C16EB
		public void Deserialize(Stream stream)
		{
			DataUpdateFinished.Deserialize(stream, this);
		}

		// Token: 0x0600C8A6 RID: 51366 RVA: 0x003C34F5 File Offset: 0x003C16F5
		public static DataUpdateFinished Deserialize(Stream stream, DataUpdateFinished instance)
		{
			return DataUpdateFinished.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C8A7 RID: 51367 RVA: 0x003C3500 File Offset: 0x003C1700
		public static DataUpdateFinished DeserializeLengthDelimited(Stream stream)
		{
			DataUpdateFinished dataUpdateFinished = new DataUpdateFinished();
			DataUpdateFinished.DeserializeLengthDelimited(stream, dataUpdateFinished);
			return dataUpdateFinished;
		}

		// Token: 0x0600C8A8 RID: 51368 RVA: 0x003C351C File Offset: 0x003C171C
		public static DataUpdateFinished DeserializeLengthDelimited(Stream stream, DataUpdateFinished instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DataUpdateFinished.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C8A9 RID: 51369 RVA: 0x003C3544 File Offset: 0x003C1744
		public static DataUpdateFinished Deserialize(Stream stream, DataUpdateFinished instance, long limit)
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

		// Token: 0x0600C8AA RID: 51370 RVA: 0x003C3636 File Offset: 0x003C1836
		public void Serialize(Stream stream)
		{
			DataUpdateFinished.Serialize(stream, this);
		}

		// Token: 0x0600C8AB RID: 51371 RVA: 0x003C3640 File Offset: 0x003C1840
		public static void Serialize(Stream stream, DataUpdateFinished instance)
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

		// Token: 0x0600C8AC RID: 51372 RVA: 0x003C36D8 File Offset: 0x003C18D8
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

		// Token: 0x04009EF9 RID: 40697
		public bool HasDeviceInfo;

		// Token: 0x04009EFA RID: 40698
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009EFB RID: 40699
		public bool HasDuration;

		// Token: 0x04009EFC RID: 40700
		private float _Duration;

		// Token: 0x04009EFD RID: 40701
		public bool HasRealDownloadBytes;

		// Token: 0x04009EFE RID: 40702
		private long _RealDownloadBytes;

		// Token: 0x04009EFF RID: 40703
		public bool HasExpectedDownloadBytes;

		// Token: 0x04009F00 RID: 40704
		private long _ExpectedDownloadBytes;
	}
}
