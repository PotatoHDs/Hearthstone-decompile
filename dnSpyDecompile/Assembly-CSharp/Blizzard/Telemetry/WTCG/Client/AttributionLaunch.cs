using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x0200120B RID: 4619
	public class AttributionLaunch : IProtoBuf
	{
		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x0600CF57 RID: 53079 RVA: 0x003DBB09 File Offset: 0x003D9D09
		// (set) Token: 0x0600CF58 RID: 53080 RVA: 0x003DBB11 File Offset: 0x003D9D11
		public string ApplicationId
		{
			get
			{
				return this._ApplicationId;
			}
			set
			{
				this._ApplicationId = value;
				this.HasApplicationId = (value != null);
			}
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x0600CF59 RID: 53081 RVA: 0x003DBB24 File Offset: 0x003D9D24
		// (set) Token: 0x0600CF5A RID: 53082 RVA: 0x003DBB2C File Offset: 0x003D9D2C
		public string DeviceType
		{
			get
			{
				return this._DeviceType;
			}
			set
			{
				this._DeviceType = value;
				this.HasDeviceType = (value != null);
			}
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x0600CF5B RID: 53083 RVA: 0x003DBB3F File Offset: 0x003D9D3F
		// (set) Token: 0x0600CF5C RID: 53084 RVA: 0x003DBB47 File Offset: 0x003D9D47
		public ulong FirstInstallDate
		{
			get
			{
				return this._FirstInstallDate;
			}
			set
			{
				this._FirstInstallDate = value;
				this.HasFirstInstallDate = true;
			}
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x0600CF5D RID: 53085 RVA: 0x003DBB57 File Offset: 0x003D9D57
		// (set) Token: 0x0600CF5E RID: 53086 RVA: 0x003DBB5F File Offset: 0x003D9D5F
		public string BundleId
		{
			get
			{
				return this._BundleId;
			}
			set
			{
				this._BundleId = value;
				this.HasBundleId = (value != null);
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x0600CF5F RID: 53087 RVA: 0x003DBB72 File Offset: 0x003D9D72
		// (set) Token: 0x0600CF60 RID: 53088 RVA: 0x003DBB7A File Offset: 0x003D9D7A
		public int Counter
		{
			get
			{
				return this._Counter;
			}
			set
			{
				this._Counter = value;
				this.HasCounter = true;
			}
		}

		// Token: 0x0600CF61 RID: 53089 RVA: 0x003DBB8C File Offset: 0x003D9D8C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasApplicationId)
			{
				num ^= this.ApplicationId.GetHashCode();
			}
			if (this.HasDeviceType)
			{
				num ^= this.DeviceType.GetHashCode();
			}
			if (this.HasFirstInstallDate)
			{
				num ^= this.FirstInstallDate.GetHashCode();
			}
			if (this.HasBundleId)
			{
				num ^= this.BundleId.GetHashCode();
			}
			if (this.HasCounter)
			{
				num ^= this.Counter.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CF62 RID: 53090 RVA: 0x003DBC1C File Offset: 0x003D9E1C
		public override bool Equals(object obj)
		{
			AttributionLaunch attributionLaunch = obj as AttributionLaunch;
			return attributionLaunch != null && this.HasApplicationId == attributionLaunch.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(attributionLaunch.ApplicationId)) && this.HasDeviceType == attributionLaunch.HasDeviceType && (!this.HasDeviceType || this.DeviceType.Equals(attributionLaunch.DeviceType)) && this.HasFirstInstallDate == attributionLaunch.HasFirstInstallDate && (!this.HasFirstInstallDate || this.FirstInstallDate.Equals(attributionLaunch.FirstInstallDate)) && this.HasBundleId == attributionLaunch.HasBundleId && (!this.HasBundleId || this.BundleId.Equals(attributionLaunch.BundleId)) && this.HasCounter == attributionLaunch.HasCounter && (!this.HasCounter || this.Counter.Equals(attributionLaunch.Counter));
		}

		// Token: 0x0600CF63 RID: 53091 RVA: 0x003DBD13 File Offset: 0x003D9F13
		public void Deserialize(Stream stream)
		{
			AttributionLaunch.Deserialize(stream, this);
		}

		// Token: 0x0600CF64 RID: 53092 RVA: 0x003DBD1D File Offset: 0x003D9F1D
		public static AttributionLaunch Deserialize(Stream stream, AttributionLaunch instance)
		{
			return AttributionLaunch.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CF65 RID: 53093 RVA: 0x003DBD28 File Offset: 0x003D9F28
		public static AttributionLaunch DeserializeLengthDelimited(Stream stream)
		{
			AttributionLaunch attributionLaunch = new AttributionLaunch();
			AttributionLaunch.DeserializeLengthDelimited(stream, attributionLaunch);
			return attributionLaunch;
		}

		// Token: 0x0600CF66 RID: 53094 RVA: 0x003DBD44 File Offset: 0x003D9F44
		public static AttributionLaunch DeserializeLengthDelimited(Stream stream, AttributionLaunch instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributionLaunch.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CF67 RID: 53095 RVA: 0x003DBD6C File Offset: 0x003D9F6C
		public static AttributionLaunch Deserialize(Stream stream, AttributionLaunch instance, long limit)
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
				else if (num == 8)
				{
					instance.Counter = (int)ProtocolParser.ReadUInt64(stream);
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
					case 100U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ApplicationId = ProtocolParser.ReadString(stream);
						}
						break;
					case 101U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeviceType = ProtocolParser.ReadString(stream);
						}
						break;
					case 102U:
						if (key.WireType == Wire.Varint)
						{
							instance.FirstInstallDate = ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 103U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.BundleId = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CF68 RID: 53096 RVA: 0x003DBE83 File Offset: 0x003DA083
		public void Serialize(Stream stream)
		{
			AttributionLaunch.Serialize(stream, this);
		}

		// Token: 0x0600CF69 RID: 53097 RVA: 0x003DBE8C File Offset: 0x003DA08C
		public static void Serialize(Stream stream, AttributionLaunch instance)
		{
			if (instance.HasApplicationId)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
			if (instance.HasDeviceType)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceType));
			}
			if (instance.HasFirstInstallDate)
			{
				stream.WriteByte(176);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt64(stream, instance.FirstInstallDate);
			}
			if (instance.HasBundleId)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BundleId));
			}
			if (instance.HasCounter)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Counter));
			}
		}

		// Token: 0x0600CF6A RID: 53098 RVA: 0x003DBF6C File Offset: 0x003DA16C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasApplicationId)
			{
				num += 2U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDeviceType)
			{
				num += 2U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.DeviceType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasFirstInstallDate)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64(this.FirstInstallDate);
			}
			if (this.HasBundleId)
			{
				num += 2U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.BundleId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasCounter)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Counter));
			}
			return num;
		}

		// Token: 0x0400A1D5 RID: 41429
		public bool HasApplicationId;

		// Token: 0x0400A1D6 RID: 41430
		private string _ApplicationId;

		// Token: 0x0400A1D7 RID: 41431
		public bool HasDeviceType;

		// Token: 0x0400A1D8 RID: 41432
		private string _DeviceType;

		// Token: 0x0400A1D9 RID: 41433
		public bool HasFirstInstallDate;

		// Token: 0x0400A1DA RID: 41434
		private ulong _FirstInstallDate;

		// Token: 0x0400A1DB RID: 41435
		public bool HasBundleId;

		// Token: 0x0400A1DC RID: 41436
		private string _BundleId;

		// Token: 0x0400A1DD RID: 41437
		public bool HasCounter;

		// Token: 0x0400A1DE RID: 41438
		private int _Counter;
	}
}
