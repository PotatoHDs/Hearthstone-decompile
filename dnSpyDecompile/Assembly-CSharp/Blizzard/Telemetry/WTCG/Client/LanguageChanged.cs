using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011D2 RID: 4562
	public class LanguageChanged : IProtoBuf
	{
		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x0600CB26 RID: 52006 RVA: 0x003CC81A File Offset: 0x003CAA1A
		// (set) Token: 0x0600CB27 RID: 52007 RVA: 0x003CC822 File Offset: 0x003CAA22
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

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x0600CB28 RID: 52008 RVA: 0x003CC835 File Offset: 0x003CAA35
		// (set) Token: 0x0600CB29 RID: 52009 RVA: 0x003CC83D File Offset: 0x003CAA3D
		public string PreviousLanguage
		{
			get
			{
				return this._PreviousLanguage;
			}
			set
			{
				this._PreviousLanguage = value;
				this.HasPreviousLanguage = (value != null);
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x0600CB2A RID: 52010 RVA: 0x003CC850 File Offset: 0x003CAA50
		// (set) Token: 0x0600CB2B RID: 52011 RVA: 0x003CC858 File Offset: 0x003CAA58
		public string NextLanguage
		{
			get
			{
				return this._NextLanguage;
			}
			set
			{
				this._NextLanguage = value;
				this.HasNextLanguage = (value != null);
			}
		}

		// Token: 0x0600CB2C RID: 52012 RVA: 0x003CC86C File Offset: 0x003CAA6C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasPreviousLanguage)
			{
				num ^= this.PreviousLanguage.GetHashCode();
			}
			if (this.HasNextLanguage)
			{
				num ^= this.NextLanguage.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CB2D RID: 52013 RVA: 0x003CC8C8 File Offset: 0x003CAAC8
		public override bool Equals(object obj)
		{
			LanguageChanged languageChanged = obj as LanguageChanged;
			return languageChanged != null && this.HasDeviceInfo == languageChanged.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(languageChanged.DeviceInfo)) && this.HasPreviousLanguage == languageChanged.HasPreviousLanguage && (!this.HasPreviousLanguage || this.PreviousLanguage.Equals(languageChanged.PreviousLanguage)) && this.HasNextLanguage == languageChanged.HasNextLanguage && (!this.HasNextLanguage || this.NextLanguage.Equals(languageChanged.NextLanguage));
		}

		// Token: 0x0600CB2E RID: 52014 RVA: 0x003CC963 File Offset: 0x003CAB63
		public void Deserialize(Stream stream)
		{
			LanguageChanged.Deserialize(stream, this);
		}

		// Token: 0x0600CB2F RID: 52015 RVA: 0x003CC96D File Offset: 0x003CAB6D
		public static LanguageChanged Deserialize(Stream stream, LanguageChanged instance)
		{
			return LanguageChanged.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CB30 RID: 52016 RVA: 0x003CC978 File Offset: 0x003CAB78
		public static LanguageChanged DeserializeLengthDelimited(Stream stream)
		{
			LanguageChanged languageChanged = new LanguageChanged();
			LanguageChanged.DeserializeLengthDelimited(stream, languageChanged);
			return languageChanged;
		}

		// Token: 0x0600CB31 RID: 52017 RVA: 0x003CC994 File Offset: 0x003CAB94
		public static LanguageChanged DeserializeLengthDelimited(Stream stream, LanguageChanged instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LanguageChanged.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CB32 RID: 52018 RVA: 0x003CC9BC File Offset: 0x003CABBC
		public static LanguageChanged Deserialize(Stream stream, LanguageChanged instance, long limit)
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
						if (num != 26)
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
							instance.NextLanguage = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.PreviousLanguage = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CB33 RID: 52019 RVA: 0x003CCA8A File Offset: 0x003CAC8A
		public void Serialize(Stream stream)
		{
			LanguageChanged.Serialize(stream, this);
		}

		// Token: 0x0600CB34 RID: 52020 RVA: 0x003CCA94 File Offset: 0x003CAC94
		public static void Serialize(Stream stream, LanguageChanged instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasPreviousLanguage)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PreviousLanguage));
			}
			if (instance.HasNextLanguage)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.NextLanguage));
			}
		}

		// Token: 0x0600CB35 RID: 52021 RVA: 0x003CCB1C File Offset: 0x003CAD1C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasPreviousLanguage)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.PreviousLanguage);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasNextLanguage)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.NextLanguage);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x0400A019 RID: 40985
		public bool HasDeviceInfo;

		// Token: 0x0400A01A RID: 40986
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A01B RID: 40987
		public bool HasPreviousLanguage;

		// Token: 0x0400A01C RID: 40988
		private string _PreviousLanguage;

		// Token: 0x0400A01D RID: 40989
		public bool HasNextLanguage;

		// Token: 0x0400A01E RID: 40990
		private string _NextLanguage;
	}
}
