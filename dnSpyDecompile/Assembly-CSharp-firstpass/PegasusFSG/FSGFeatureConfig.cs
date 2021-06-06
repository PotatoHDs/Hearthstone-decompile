using System;
using System.IO;

namespace PegasusFSG
{
	// Token: 0x02000022 RID: 34
	public class FSGFeatureConfig : IProtoBuf
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00007511 File Offset: 0x00005711
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00007519 File Offset: 0x00005719
		public bool Gps
		{
			get
			{
				return this._Gps;
			}
			set
			{
				this._Gps = value;
				this.HasGps = true;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00007529 File Offset: 0x00005729
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00007531 File Offset: 0x00005731
		public bool Wifi
		{
			get
			{
				return this._Wifi;
			}
			set
			{
				this._Wifi = value;
				this.HasWifi = true;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00007541 File Offset: 0x00005741
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00007549 File Offset: 0x00005749
		public bool AutoCheckin
		{
			get
			{
				return this._AutoCheckin;
			}
			set
			{
				this._AutoCheckin = value;
				this.HasAutoCheckin = true;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007559 File Offset: 0x00005759
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00007561 File Offset: 0x00005761
		public long MaxAccuracy
		{
			get
			{
				return this._MaxAccuracy;
			}
			set
			{
				this._MaxAccuracy = value;
				this.HasMaxAccuracy = true;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007574 File Offset: 0x00005774
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGps)
			{
				num ^= this.Gps.GetHashCode();
			}
			if (this.HasWifi)
			{
				num ^= this.Wifi.GetHashCode();
			}
			if (this.HasAutoCheckin)
			{
				num ^= this.AutoCheckin.GetHashCode();
			}
			if (this.HasMaxAccuracy)
			{
				num ^= this.MaxAccuracy.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000075F4 File Offset: 0x000057F4
		public override bool Equals(object obj)
		{
			FSGFeatureConfig fsgfeatureConfig = obj as FSGFeatureConfig;
			return fsgfeatureConfig != null && this.HasGps == fsgfeatureConfig.HasGps && (!this.HasGps || this.Gps.Equals(fsgfeatureConfig.Gps)) && this.HasWifi == fsgfeatureConfig.HasWifi && (!this.HasWifi || this.Wifi.Equals(fsgfeatureConfig.Wifi)) && this.HasAutoCheckin == fsgfeatureConfig.HasAutoCheckin && (!this.HasAutoCheckin || this.AutoCheckin.Equals(fsgfeatureConfig.AutoCheckin)) && this.HasMaxAccuracy == fsgfeatureConfig.HasMaxAccuracy && (!this.HasMaxAccuracy || this.MaxAccuracy.Equals(fsgfeatureConfig.MaxAccuracy));
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000076C6 File Offset: 0x000058C6
		public void Deserialize(Stream stream)
		{
			FSGFeatureConfig.Deserialize(stream, this);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000076D0 File Offset: 0x000058D0
		public static FSGFeatureConfig Deserialize(Stream stream, FSGFeatureConfig instance)
		{
			return FSGFeatureConfig.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000076DC File Offset: 0x000058DC
		public static FSGFeatureConfig DeserializeLengthDelimited(Stream stream)
		{
			FSGFeatureConfig fsgfeatureConfig = new FSGFeatureConfig();
			FSGFeatureConfig.DeserializeLengthDelimited(stream, fsgfeatureConfig);
			return fsgfeatureConfig;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000076F8 File Offset: 0x000058F8
		public static FSGFeatureConfig DeserializeLengthDelimited(Stream stream, FSGFeatureConfig instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FSGFeatureConfig.Deserialize(stream, instance, num);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007720 File Offset: 0x00005920
		public static FSGFeatureConfig Deserialize(Stream stream, FSGFeatureConfig instance, long limit)
		{
			instance.Gps = true;
			instance.Wifi = true;
			instance.AutoCheckin = true;
			instance.MaxAccuracy = 200L;
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
						if (num == 8)
						{
							instance.Gps = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Wifi = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.AutoCheckin = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 32)
						{
							instance.MaxAccuracy = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600019C RID: 412 RVA: 0x00007811 File Offset: 0x00005A11
		public void Serialize(Stream stream)
		{
			FSGFeatureConfig.Serialize(stream, this);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000781C File Offset: 0x00005A1C
		public static void Serialize(Stream stream, FSGFeatureConfig instance)
		{
			if (instance.HasGps)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.Gps);
			}
			if (instance.HasWifi)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Wifi);
			}
			if (instance.HasAutoCheckin)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.AutoCheckin);
			}
			if (instance.HasMaxAccuracy)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxAccuracy);
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007898 File Offset: 0x00005A98
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGps)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasWifi)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasAutoCheckin)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasMaxAccuracy)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.MaxAccuracy);
			}
			return num;
		}

		// Token: 0x0400006B RID: 107
		public bool HasGps;

		// Token: 0x0400006C RID: 108
		private bool _Gps;

		// Token: 0x0400006D RID: 109
		public bool HasWifi;

		// Token: 0x0400006E RID: 110
		private bool _Wifi;

		// Token: 0x0400006F RID: 111
		public bool HasAutoCheckin;

		// Token: 0x04000070 RID: 112
		private bool _AutoCheckin;

		// Token: 0x04000071 RID: 113
		public bool HasMaxAccuracy;

		// Token: 0x04000072 RID: 114
		private long _MaxAccuracy;

		// Token: 0x02000558 RID: 1368
		public enum PacketID
		{
			// Token: 0x04001E25 RID: 7717
			ID = 511
		}
	}
}
