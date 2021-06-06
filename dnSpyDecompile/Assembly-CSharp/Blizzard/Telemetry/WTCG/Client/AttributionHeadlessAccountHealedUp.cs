using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001198 RID: 4504
	public class AttributionHeadlessAccountHealedUp : IProtoBuf
	{
		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x0600C6B4 RID: 50868 RVA: 0x003BC0CC File Offset: 0x003BA2CC
		// (set) Token: 0x0600C6B5 RID: 50869 RVA: 0x003BC0D4 File Offset: 0x003BA2D4
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

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x0600C6B6 RID: 50870 RVA: 0x003BC0E7 File Offset: 0x003BA2E7
		// (set) Token: 0x0600C6B7 RID: 50871 RVA: 0x003BC0EF File Offset: 0x003BA2EF
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

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x0600C6B8 RID: 50872 RVA: 0x003BC102 File Offset: 0x003BA302
		// (set) Token: 0x0600C6B9 RID: 50873 RVA: 0x003BC10A File Offset: 0x003BA30A
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

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x0600C6BA RID: 50874 RVA: 0x003BC11A File Offset: 0x003BA31A
		// (set) Token: 0x0600C6BB RID: 50875 RVA: 0x003BC122 File Offset: 0x003BA322
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

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x0600C6BC RID: 50876 RVA: 0x003BC135 File Offset: 0x003BA335
		// (set) Token: 0x0600C6BD RID: 50877 RVA: 0x003BC13D File Offset: 0x003BA33D
		public string TemporaryGameAccountId
		{
			get
			{
				return this._TemporaryGameAccountId;
			}
			set
			{
				this._TemporaryGameAccountId = value;
				this.HasTemporaryGameAccountId = (value != null);
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x0600C6BE RID: 50878 RVA: 0x003BC150 File Offset: 0x003BA350
		// (set) Token: 0x0600C6BF RID: 50879 RVA: 0x003BC158 File Offset: 0x003BA358
		public IdentifierInfo Identifier
		{
			get
			{
				return this._Identifier;
			}
			set
			{
				this._Identifier = value;
				this.HasIdentifier = (value != null);
			}
		}

		// Token: 0x0600C6C0 RID: 50880 RVA: 0x003BC16C File Offset: 0x003BA36C
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
			if (this.HasTemporaryGameAccountId)
			{
				num ^= this.TemporaryGameAccountId.GetHashCode();
			}
			if (this.HasIdentifier)
			{
				num ^= this.Identifier.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C6C1 RID: 50881 RVA: 0x003BC210 File Offset: 0x003BA410
		public override bool Equals(object obj)
		{
			AttributionHeadlessAccountHealedUp attributionHeadlessAccountHealedUp = obj as AttributionHeadlessAccountHealedUp;
			return attributionHeadlessAccountHealedUp != null && this.HasApplicationId == attributionHeadlessAccountHealedUp.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(attributionHeadlessAccountHealedUp.ApplicationId)) && this.HasDeviceType == attributionHeadlessAccountHealedUp.HasDeviceType && (!this.HasDeviceType || this.DeviceType.Equals(attributionHeadlessAccountHealedUp.DeviceType)) && this.HasFirstInstallDate == attributionHeadlessAccountHealedUp.HasFirstInstallDate && (!this.HasFirstInstallDate || this.FirstInstallDate.Equals(attributionHeadlessAccountHealedUp.FirstInstallDate)) && this.HasBundleId == attributionHeadlessAccountHealedUp.HasBundleId && (!this.HasBundleId || this.BundleId.Equals(attributionHeadlessAccountHealedUp.BundleId)) && this.HasTemporaryGameAccountId == attributionHeadlessAccountHealedUp.HasTemporaryGameAccountId && (!this.HasTemporaryGameAccountId || this.TemporaryGameAccountId.Equals(attributionHeadlessAccountHealedUp.TemporaryGameAccountId)) && this.HasIdentifier == attributionHeadlessAccountHealedUp.HasIdentifier && (!this.HasIdentifier || this.Identifier.Equals(attributionHeadlessAccountHealedUp.Identifier));
		}

		// Token: 0x0600C6C2 RID: 50882 RVA: 0x003BC32F File Offset: 0x003BA52F
		public void Deserialize(Stream stream)
		{
			AttributionHeadlessAccountHealedUp.Deserialize(stream, this);
		}

		// Token: 0x0600C6C3 RID: 50883 RVA: 0x003BC339 File Offset: 0x003BA539
		public static AttributionHeadlessAccountHealedUp Deserialize(Stream stream, AttributionHeadlessAccountHealedUp instance)
		{
			return AttributionHeadlessAccountHealedUp.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C6C4 RID: 50884 RVA: 0x003BC344 File Offset: 0x003BA544
		public static AttributionHeadlessAccountHealedUp DeserializeLengthDelimited(Stream stream)
		{
			AttributionHeadlessAccountHealedUp attributionHeadlessAccountHealedUp = new AttributionHeadlessAccountHealedUp();
			AttributionHeadlessAccountHealedUp.DeserializeLengthDelimited(stream, attributionHeadlessAccountHealedUp);
			return attributionHeadlessAccountHealedUp;
		}

		// Token: 0x0600C6C5 RID: 50885 RVA: 0x003BC360 File Offset: 0x003BA560
		public static AttributionHeadlessAccountHealedUp DeserializeLengthDelimited(Stream stream, AttributionHeadlessAccountHealedUp instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributionHeadlessAccountHealedUp.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C6C6 RID: 50886 RVA: 0x003BC388 File Offset: 0x003BA588
		public static AttributionHeadlessAccountHealedUp Deserialize(Stream stream, AttributionHeadlessAccountHealedUp instance, long limit)
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
				else if (num == 10)
				{
					instance.TemporaryGameAccountId = ProtocolParser.ReadString(stream);
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
						if (field != 1000U)
						{
							ProtocolParser.SkipKey(stream, key);
						}
						else if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.Identifier == null)
							{
								instance.Identifier = IdentifierInfo.DeserializeLengthDelimited(stream);
							}
							else
							{
								IdentifierInfo.DeserializeLengthDelimited(stream, instance.Identifier);
							}
						}
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

		// Token: 0x0600C6C7 RID: 50887 RVA: 0x003BC4E4 File Offset: 0x003BA6E4
		public void Serialize(Stream stream)
		{
			AttributionHeadlessAccountHealedUp.Serialize(stream, this);
		}

		// Token: 0x0600C6C8 RID: 50888 RVA: 0x003BC4F0 File Offset: 0x003BA6F0
		public static void Serialize(Stream stream, AttributionHeadlessAccountHealedUp instance)
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
			if (instance.HasTemporaryGameAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TemporaryGameAccountId));
			}
			if (instance.HasIdentifier)
			{
				stream.WriteByte(194);
				stream.WriteByte(62);
				ProtocolParser.WriteUInt32(stream, instance.Identifier.GetSerializedSize());
				IdentifierInfo.Serialize(stream, instance.Identifier);
			}
		}

		// Token: 0x0600C6C9 RID: 50889 RVA: 0x003BC614 File Offset: 0x003BA814
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
			if (this.HasTemporaryGameAccountId)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.TemporaryGameAccountId);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasIdentifier)
			{
				num += 2U;
				uint serializedSize = this.Identifier.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04009E27 RID: 40487
		public bool HasApplicationId;

		// Token: 0x04009E28 RID: 40488
		private string _ApplicationId;

		// Token: 0x04009E29 RID: 40489
		public bool HasDeviceType;

		// Token: 0x04009E2A RID: 40490
		private string _DeviceType;

		// Token: 0x04009E2B RID: 40491
		public bool HasFirstInstallDate;

		// Token: 0x04009E2C RID: 40492
		private ulong _FirstInstallDate;

		// Token: 0x04009E2D RID: 40493
		public bool HasBundleId;

		// Token: 0x04009E2E RID: 40494
		private string _BundleId;

		// Token: 0x04009E2F RID: 40495
		public bool HasTemporaryGameAccountId;

		// Token: 0x04009E30 RID: 40496
		private string _TemporaryGameAccountId;

		// Token: 0x04009E31 RID: 40497
		public bool HasIdentifier;

		// Token: 0x04009E32 RID: 40498
		private IdentifierInfo _Identifier;
	}
}
