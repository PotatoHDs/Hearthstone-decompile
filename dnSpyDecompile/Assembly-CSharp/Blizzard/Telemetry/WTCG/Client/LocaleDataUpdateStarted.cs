using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011D7 RID: 4567
	public class LocaleDataUpdateStarted : IProtoBuf
	{
		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x0600CB7F RID: 52095 RVA: 0x003CDAB7 File Offset: 0x003CBCB7
		// (set) Token: 0x0600CB80 RID: 52096 RVA: 0x003CDABF File Offset: 0x003CBCBF
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

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x0600CB81 RID: 52097 RVA: 0x003CDAD2 File Offset: 0x003CBCD2
		// (set) Token: 0x0600CB82 RID: 52098 RVA: 0x003CDADA File Offset: 0x003CBCDA
		public string Locale
		{
			get
			{
				return this._Locale;
			}
			set
			{
				this._Locale = value;
				this.HasLocale = (value != null);
			}
		}

		// Token: 0x0600CB83 RID: 52099 RVA: 0x003CDAF0 File Offset: 0x003CBCF0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasLocale)
			{
				num ^= this.Locale.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CB84 RID: 52100 RVA: 0x003CDB38 File Offset: 0x003CBD38
		public override bool Equals(object obj)
		{
			LocaleDataUpdateStarted localeDataUpdateStarted = obj as LocaleDataUpdateStarted;
			return localeDataUpdateStarted != null && this.HasDeviceInfo == localeDataUpdateStarted.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(localeDataUpdateStarted.DeviceInfo)) && this.HasLocale == localeDataUpdateStarted.HasLocale && (!this.HasLocale || this.Locale.Equals(localeDataUpdateStarted.Locale));
		}

		// Token: 0x0600CB85 RID: 52101 RVA: 0x003CDBA8 File Offset: 0x003CBDA8
		public void Deserialize(Stream stream)
		{
			LocaleDataUpdateStarted.Deserialize(stream, this);
		}

		// Token: 0x0600CB86 RID: 52102 RVA: 0x003CDBB2 File Offset: 0x003CBDB2
		public static LocaleDataUpdateStarted Deserialize(Stream stream, LocaleDataUpdateStarted instance)
		{
			return LocaleDataUpdateStarted.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CB87 RID: 52103 RVA: 0x003CDBC0 File Offset: 0x003CBDC0
		public static LocaleDataUpdateStarted DeserializeLengthDelimited(Stream stream)
		{
			LocaleDataUpdateStarted localeDataUpdateStarted = new LocaleDataUpdateStarted();
			LocaleDataUpdateStarted.DeserializeLengthDelimited(stream, localeDataUpdateStarted);
			return localeDataUpdateStarted;
		}

		// Token: 0x0600CB88 RID: 52104 RVA: 0x003CDBDC File Offset: 0x003CBDDC
		public static LocaleDataUpdateStarted DeserializeLengthDelimited(Stream stream, LocaleDataUpdateStarted instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LocaleDataUpdateStarted.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CB89 RID: 52105 RVA: 0x003CDC04 File Offset: 0x003CBE04
		public static LocaleDataUpdateStarted Deserialize(Stream stream, LocaleDataUpdateStarted instance, long limit)
		{
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
				else if (num != 10)
				{
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Locale = ProtocolParser.ReadString(stream);
					}
				}
				else if (instance.DeviceInfo == null)
				{
					instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
				}
				else
				{
					DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CB8A RID: 52106 RVA: 0x003CDCB6 File Offset: 0x003CBEB6
		public void Serialize(Stream stream)
		{
			LocaleDataUpdateStarted.Serialize(stream, this);
		}

		// Token: 0x0600CB8B RID: 52107 RVA: 0x003CDCC0 File Offset: 0x003CBEC0
		public static void Serialize(Stream stream, LocaleDataUpdateStarted instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Locale));
			}
		}

		// Token: 0x0600CB8C RID: 52108 RVA: 0x003CDD20 File Offset: 0x003CBF20
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasLocale)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Locale);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x0400A03B RID: 41019
		public bool HasDeviceInfo;

		// Token: 0x0400A03C RID: 41020
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A03D RID: 41021
		public bool HasLocale;

		// Token: 0x0400A03E RID: 41022
		private string _Locale;
	}
}
