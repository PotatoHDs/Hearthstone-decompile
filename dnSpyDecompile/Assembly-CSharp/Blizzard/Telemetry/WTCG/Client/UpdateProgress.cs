using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x0200120D RID: 4621
	public class UpdateProgress : IProtoBuf
	{
		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x0600CF77 RID: 53111 RVA: 0x003DC112 File Offset: 0x003DA312
		// (set) Token: 0x0600CF78 RID: 53112 RVA: 0x003DC11A File Offset: 0x003DA31A
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

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x0600CF79 RID: 53113 RVA: 0x003DC12D File Offset: 0x003DA32D
		// (set) Token: 0x0600CF7A RID: 53114 RVA: 0x003DC135 File Offset: 0x003DA335
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

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x0600CF7B RID: 53115 RVA: 0x003DC145 File Offset: 0x003DA345
		// (set) Token: 0x0600CF7C RID: 53116 RVA: 0x003DC14D File Offset: 0x003DA34D
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

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x0600CF7D RID: 53117 RVA: 0x003DC15D File Offset: 0x003DA35D
		// (set) Token: 0x0600CF7E RID: 53118 RVA: 0x003DC165 File Offset: 0x003DA365
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

		// Token: 0x0600CF7F RID: 53119 RVA: 0x003DC178 File Offset: 0x003DA378
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

		// Token: 0x0600CF80 RID: 53120 RVA: 0x003DC1F4 File Offset: 0x003DA3F4
		public override bool Equals(object obj)
		{
			UpdateProgress updateProgress = obj as UpdateProgress;
			return updateProgress != null && this.HasDeviceInfo == updateProgress.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(updateProgress.DeviceInfo)) && this.HasDuration == updateProgress.HasDuration && (!this.HasDuration || this.Duration.Equals(updateProgress.Duration)) && this.HasRealDownloadBytes == updateProgress.HasRealDownloadBytes && (!this.HasRealDownloadBytes || this.RealDownloadBytes.Equals(updateProgress.RealDownloadBytes)) && this.HasExpectedDownloadBytes == updateProgress.HasExpectedDownloadBytes && (!this.HasExpectedDownloadBytes || this.ExpectedDownloadBytes.Equals(updateProgress.ExpectedDownloadBytes));
		}

		// Token: 0x0600CF81 RID: 53121 RVA: 0x003DC2C3 File Offset: 0x003DA4C3
		public void Deserialize(Stream stream)
		{
			UpdateProgress.Deserialize(stream, this);
		}

		// Token: 0x0600CF82 RID: 53122 RVA: 0x003DC2CD File Offset: 0x003DA4CD
		public static UpdateProgress Deserialize(Stream stream, UpdateProgress instance)
		{
			return UpdateProgress.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CF83 RID: 53123 RVA: 0x003DC2D8 File Offset: 0x003DA4D8
		public static UpdateProgress DeserializeLengthDelimited(Stream stream)
		{
			UpdateProgress updateProgress = new UpdateProgress();
			UpdateProgress.DeserializeLengthDelimited(stream, updateProgress);
			return updateProgress;
		}

		// Token: 0x0600CF84 RID: 53124 RVA: 0x003DC2F4 File Offset: 0x003DA4F4
		public static UpdateProgress DeserializeLengthDelimited(Stream stream, UpdateProgress instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateProgress.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CF85 RID: 53125 RVA: 0x003DC31C File Offset: 0x003DA51C
		public static UpdateProgress Deserialize(Stream stream, UpdateProgress instance, long limit)
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

		// Token: 0x0600CF86 RID: 53126 RVA: 0x003DC40E File Offset: 0x003DA60E
		public void Serialize(Stream stream)
		{
			UpdateProgress.Serialize(stream, this);
		}

		// Token: 0x0600CF87 RID: 53127 RVA: 0x003DC418 File Offset: 0x003DA618
		public static void Serialize(Stream stream, UpdateProgress instance)
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

		// Token: 0x0600CF88 RID: 53128 RVA: 0x003DC4B0 File Offset: 0x003DA6B0
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

		// Token: 0x0400A1DF RID: 41439
		public bool HasDeviceInfo;

		// Token: 0x0400A1E0 RID: 41440
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A1E1 RID: 41441
		public bool HasDuration;

		// Token: 0x0400A1E2 RID: 41442
		private float _Duration;

		// Token: 0x0400A1E3 RID: 41443
		public bool HasRealDownloadBytes;

		// Token: 0x0400A1E4 RID: 41444
		private long _RealDownloadBytes;

		// Token: 0x0400A1E5 RID: 41445
		public bool HasExpectedDownloadBytes;

		// Token: 0x0400A1E6 RID: 41446
		private long _ExpectedDownloadBytes;
	}
}
