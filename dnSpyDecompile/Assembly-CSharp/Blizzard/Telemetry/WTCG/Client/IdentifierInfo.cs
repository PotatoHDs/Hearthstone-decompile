using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011C6 RID: 4550
	public class IdentifierInfo : IProtoBuf
	{
		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x0600CA38 RID: 51768 RVA: 0x003C914E File Offset: 0x003C734E
		// (set) Token: 0x0600CA39 RID: 51769 RVA: 0x003C9156 File Offset: 0x003C7356
		public string AppInstallId
		{
			get
			{
				return this._AppInstallId;
			}
			set
			{
				this._AppInstallId = value;
				this.HasAppInstallId = (value != null);
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x0600CA3A RID: 51770 RVA: 0x003C9169 File Offset: 0x003C7369
		// (set) Token: 0x0600CA3B RID: 51771 RVA: 0x003C9171 File Offset: 0x003C7371
		public string DeviceId
		{
			get
			{
				return this._DeviceId;
			}
			set
			{
				this._DeviceId = value;
				this.HasDeviceId = (value != null);
			}
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x0600CA3C RID: 51772 RVA: 0x003C9184 File Offset: 0x003C7384
		// (set) Token: 0x0600CA3D RID: 51773 RVA: 0x003C918C File Offset: 0x003C738C
		public string AdvertisingId
		{
			get
			{
				return this._AdvertisingId;
			}
			set
			{
				this._AdvertisingId = value;
				this.HasAdvertisingId = (value != null);
			}
		}

		// Token: 0x0600CA3E RID: 51774 RVA: 0x003C91A0 File Offset: 0x003C73A0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAppInstallId)
			{
				num ^= this.AppInstallId.GetHashCode();
			}
			if (this.HasDeviceId)
			{
				num ^= this.DeviceId.GetHashCode();
			}
			if (this.HasAdvertisingId)
			{
				num ^= this.AdvertisingId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CA3F RID: 51775 RVA: 0x003C91FC File Offset: 0x003C73FC
		public override bool Equals(object obj)
		{
			IdentifierInfo identifierInfo = obj as IdentifierInfo;
			return identifierInfo != null && this.HasAppInstallId == identifierInfo.HasAppInstallId && (!this.HasAppInstallId || this.AppInstallId.Equals(identifierInfo.AppInstallId)) && this.HasDeviceId == identifierInfo.HasDeviceId && (!this.HasDeviceId || this.DeviceId.Equals(identifierInfo.DeviceId)) && this.HasAdvertisingId == identifierInfo.HasAdvertisingId && (!this.HasAdvertisingId || this.AdvertisingId.Equals(identifierInfo.AdvertisingId));
		}

		// Token: 0x0600CA40 RID: 51776 RVA: 0x003C9297 File Offset: 0x003C7497
		public void Deserialize(Stream stream)
		{
			IdentifierInfo.Deserialize(stream, this);
		}

		// Token: 0x0600CA41 RID: 51777 RVA: 0x003C92A1 File Offset: 0x003C74A1
		public static IdentifierInfo Deserialize(Stream stream, IdentifierInfo instance)
		{
			return IdentifierInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CA42 RID: 51778 RVA: 0x003C92AC File Offset: 0x003C74AC
		public static IdentifierInfo DeserializeLengthDelimited(Stream stream)
		{
			IdentifierInfo identifierInfo = new IdentifierInfo();
			IdentifierInfo.DeserializeLengthDelimited(stream, identifierInfo);
			return identifierInfo;
		}

		// Token: 0x0600CA43 RID: 51779 RVA: 0x003C92C8 File Offset: 0x003C74C8
		public static IdentifierInfo DeserializeLengthDelimited(Stream stream, IdentifierInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return IdentifierInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CA44 RID: 51780 RVA: 0x003C92F0 File Offset: 0x003C74F0
		public static IdentifierInfo Deserialize(Stream stream, IdentifierInfo instance, long limit)
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
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					switch (field)
					{
					case 101U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.AppInstallId = ProtocolParser.ReadString(stream);
						}
						break;
					case 102U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeviceId = ProtocolParser.ReadString(stream);
						}
						break;
					case 103U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.AdvertisingId = ProtocolParser.ReadString(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CA45 RID: 51781 RVA: 0x003C93D1 File Offset: 0x003C75D1
		public void Serialize(Stream stream)
		{
			IdentifierInfo.Serialize(stream, this);
		}

		// Token: 0x0600CA46 RID: 51782 RVA: 0x003C93DC File Offset: 0x003C75DC
		public static void Serialize(Stream stream, IdentifierInfo instance)
		{
			if (instance.HasAppInstallId)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AppInstallId));
			}
			if (instance.HasDeviceId)
			{
				stream.WriteByte(178);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceId));
			}
			if (instance.HasAdvertisingId)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AdvertisingId));
			}
		}

		// Token: 0x0600CA47 RID: 51783 RVA: 0x003C947C File Offset: 0x003C767C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAppInstallId)
			{
				num += 2U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.AppInstallId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDeviceId)
			{
				num += 2U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.DeviceId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasAdvertisingId)
			{
				num += 2U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.AdvertisingId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x04009FAF RID: 40879
		public bool HasAppInstallId;

		// Token: 0x04009FB0 RID: 40880
		private string _AppInstallId;

		// Token: 0x04009FB1 RID: 40881
		public bool HasDeviceId;

		// Token: 0x04009FB2 RID: 40882
		private string _DeviceId;

		// Token: 0x04009FB3 RID: 40883
		public bool HasAdvertisingId;

		// Token: 0x04009FB4 RID: 40884
		private string _AdvertisingId;
	}
}
