using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011D6 RID: 4566
	public class LocaleDataUpdateFinished : IProtoBuf
	{
		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x0600CB6C RID: 52076 RVA: 0x003CD6A2 File Offset: 0x003CB8A2
		// (set) Token: 0x0600CB6D RID: 52077 RVA: 0x003CD6AA File Offset: 0x003CB8AA
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

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x0600CB6E RID: 52078 RVA: 0x003CD6BD File Offset: 0x003CB8BD
		// (set) Token: 0x0600CB6F RID: 52079 RVA: 0x003CD6C5 File Offset: 0x003CB8C5
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

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x0600CB70 RID: 52080 RVA: 0x003CD6D5 File Offset: 0x003CB8D5
		// (set) Token: 0x0600CB71 RID: 52081 RVA: 0x003CD6DD File Offset: 0x003CB8DD
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

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x0600CB72 RID: 52082 RVA: 0x003CD6ED File Offset: 0x003CB8ED
		// (set) Token: 0x0600CB73 RID: 52083 RVA: 0x003CD6F5 File Offset: 0x003CB8F5
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

		// Token: 0x0600CB74 RID: 52084 RVA: 0x003CD708 File Offset: 0x003CB908
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

		// Token: 0x0600CB75 RID: 52085 RVA: 0x003CD784 File Offset: 0x003CB984
		public override bool Equals(object obj)
		{
			LocaleDataUpdateFinished localeDataUpdateFinished = obj as LocaleDataUpdateFinished;
			return localeDataUpdateFinished != null && this.HasDeviceInfo == localeDataUpdateFinished.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(localeDataUpdateFinished.DeviceInfo)) && this.HasDuration == localeDataUpdateFinished.HasDuration && (!this.HasDuration || this.Duration.Equals(localeDataUpdateFinished.Duration)) && this.HasRealDownloadBytes == localeDataUpdateFinished.HasRealDownloadBytes && (!this.HasRealDownloadBytes || this.RealDownloadBytes.Equals(localeDataUpdateFinished.RealDownloadBytes)) && this.HasExpectedDownloadBytes == localeDataUpdateFinished.HasExpectedDownloadBytes && (!this.HasExpectedDownloadBytes || this.ExpectedDownloadBytes.Equals(localeDataUpdateFinished.ExpectedDownloadBytes));
		}

		// Token: 0x0600CB76 RID: 52086 RVA: 0x003CD853 File Offset: 0x003CBA53
		public void Deserialize(Stream stream)
		{
			LocaleDataUpdateFinished.Deserialize(stream, this);
		}

		// Token: 0x0600CB77 RID: 52087 RVA: 0x003CD85D File Offset: 0x003CBA5D
		public static LocaleDataUpdateFinished Deserialize(Stream stream, LocaleDataUpdateFinished instance)
		{
			return LocaleDataUpdateFinished.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CB78 RID: 52088 RVA: 0x003CD868 File Offset: 0x003CBA68
		public static LocaleDataUpdateFinished DeserializeLengthDelimited(Stream stream)
		{
			LocaleDataUpdateFinished localeDataUpdateFinished = new LocaleDataUpdateFinished();
			LocaleDataUpdateFinished.DeserializeLengthDelimited(stream, localeDataUpdateFinished);
			return localeDataUpdateFinished;
		}

		// Token: 0x0600CB79 RID: 52089 RVA: 0x003CD884 File Offset: 0x003CBA84
		public static LocaleDataUpdateFinished DeserializeLengthDelimited(Stream stream, LocaleDataUpdateFinished instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LocaleDataUpdateFinished.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CB7A RID: 52090 RVA: 0x003CD8AC File Offset: 0x003CBAAC
		public static LocaleDataUpdateFinished Deserialize(Stream stream, LocaleDataUpdateFinished instance, long limit)
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

		// Token: 0x0600CB7B RID: 52091 RVA: 0x003CD99E File Offset: 0x003CBB9E
		public void Serialize(Stream stream)
		{
			LocaleDataUpdateFinished.Serialize(stream, this);
		}

		// Token: 0x0600CB7C RID: 52092 RVA: 0x003CD9A8 File Offset: 0x003CBBA8
		public static void Serialize(Stream stream, LocaleDataUpdateFinished instance)
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

		// Token: 0x0600CB7D RID: 52093 RVA: 0x003CDA40 File Offset: 0x003CBC40
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

		// Token: 0x0400A033 RID: 41011
		public bool HasDeviceInfo;

		// Token: 0x0400A034 RID: 41012
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A035 RID: 41013
		public bool HasDuration;

		// Token: 0x0400A036 RID: 41014
		private float _Duration;

		// Token: 0x0400A037 RID: 41015
		public bool HasRealDownloadBytes;

		// Token: 0x0400A038 RID: 41016
		private long _RealDownloadBytes;

		// Token: 0x0400A039 RID: 41017
		public bool HasExpectedDownloadBytes;

		// Token: 0x0400A03A RID: 41018
		private long _ExpectedDownloadBytes;
	}
}
