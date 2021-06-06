using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001196 RID: 4502
	public class AttributionGameRoundStart : IProtoBuf
	{
		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x0600C688 RID: 50824 RVA: 0x003BB553 File Offset: 0x003B9753
		// (set) Token: 0x0600C689 RID: 50825 RVA: 0x003BB55B File Offset: 0x003B975B
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

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x0600C68A RID: 50826 RVA: 0x003BB56E File Offset: 0x003B976E
		// (set) Token: 0x0600C68B RID: 50827 RVA: 0x003BB576 File Offset: 0x003B9776
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

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x0600C68C RID: 50828 RVA: 0x003BB589 File Offset: 0x003B9789
		// (set) Token: 0x0600C68D RID: 50829 RVA: 0x003BB591 File Offset: 0x003B9791
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

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x0600C68E RID: 50830 RVA: 0x003BB5A1 File Offset: 0x003B97A1
		// (set) Token: 0x0600C68F RID: 50831 RVA: 0x003BB5A9 File Offset: 0x003B97A9
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

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x0600C690 RID: 50832 RVA: 0x003BB5BC File Offset: 0x003B97BC
		// (set) Token: 0x0600C691 RID: 50833 RVA: 0x003BB5C4 File Offset: 0x003B97C4
		public string GameMode
		{
			get
			{
				return this._GameMode;
			}
			set
			{
				this._GameMode = value;
				this.HasGameMode = (value != null);
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x0600C692 RID: 50834 RVA: 0x003BB5D7 File Offset: 0x003B97D7
		// (set) Token: 0x0600C693 RID: 50835 RVA: 0x003BB5DF File Offset: 0x003B97DF
		public FormatType FormatType
		{
			get
			{
				return this._FormatType;
			}
			set
			{
				this._FormatType = value;
				this.HasFormatType = true;
			}
		}

		// Token: 0x0600C694 RID: 50836 RVA: 0x003BB5F0 File Offset: 0x003B97F0
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
			if (this.HasGameMode)
			{
				num ^= this.GameMode.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C695 RID: 50837 RVA: 0x003BB69C File Offset: 0x003B989C
		public override bool Equals(object obj)
		{
			AttributionGameRoundStart attributionGameRoundStart = obj as AttributionGameRoundStart;
			return attributionGameRoundStart != null && this.HasApplicationId == attributionGameRoundStart.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(attributionGameRoundStart.ApplicationId)) && this.HasDeviceType == attributionGameRoundStart.HasDeviceType && (!this.HasDeviceType || this.DeviceType.Equals(attributionGameRoundStart.DeviceType)) && this.HasFirstInstallDate == attributionGameRoundStart.HasFirstInstallDate && (!this.HasFirstInstallDate || this.FirstInstallDate.Equals(attributionGameRoundStart.FirstInstallDate)) && this.HasBundleId == attributionGameRoundStart.HasBundleId && (!this.HasBundleId || this.BundleId.Equals(attributionGameRoundStart.BundleId)) && this.HasGameMode == attributionGameRoundStart.HasGameMode && (!this.HasGameMode || this.GameMode.Equals(attributionGameRoundStart.GameMode)) && this.HasFormatType == attributionGameRoundStart.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(attributionGameRoundStart.FormatType));
		}

		// Token: 0x0600C696 RID: 50838 RVA: 0x003BB7C9 File Offset: 0x003B99C9
		public void Deserialize(Stream stream)
		{
			AttributionGameRoundStart.Deserialize(stream, this);
		}

		// Token: 0x0600C697 RID: 50839 RVA: 0x003BB7D3 File Offset: 0x003B99D3
		public static AttributionGameRoundStart Deserialize(Stream stream, AttributionGameRoundStart instance)
		{
			return AttributionGameRoundStart.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C698 RID: 50840 RVA: 0x003BB7E0 File Offset: 0x003B99E0
		public static AttributionGameRoundStart DeserializeLengthDelimited(Stream stream)
		{
			AttributionGameRoundStart attributionGameRoundStart = new AttributionGameRoundStart();
			AttributionGameRoundStart.DeserializeLengthDelimited(stream, attributionGameRoundStart);
			return attributionGameRoundStart;
		}

		// Token: 0x0600C699 RID: 50841 RVA: 0x003BB7FC File Offset: 0x003B99FC
		public static AttributionGameRoundStart DeserializeLengthDelimited(Stream stream, AttributionGameRoundStart instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributionGameRoundStart.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C69A RID: 50842 RVA: 0x003BB824 File Offset: 0x003B9A24
		public static AttributionGameRoundStart Deserialize(Stream stream, AttributionGameRoundStart instance, long limit)
		{
			instance.FormatType = FormatType.FT_UNKNOWN;
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
					if (num != 32)
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
					else
					{
						instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.GameMode = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C69B RID: 50843 RVA: 0x003BB958 File Offset: 0x003B9B58
		public void Serialize(Stream stream)
		{
			AttributionGameRoundStart.Serialize(stream, this);
		}

		// Token: 0x0600C69C RID: 50844 RVA: 0x003BB964 File Offset: 0x003B9B64
		public static void Serialize(Stream stream, AttributionGameRoundStart instance)
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
			if (instance.HasGameMode)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GameMode));
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
		}

		// Token: 0x0600C69D RID: 50845 RVA: 0x003BBA6C File Offset: 0x003B9C6C
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
			if (this.HasGameMode)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.GameMode);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasFormatType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			}
			return num;
		}

		// Token: 0x04009E11 RID: 40465
		public bool HasApplicationId;

		// Token: 0x04009E12 RID: 40466
		private string _ApplicationId;

		// Token: 0x04009E13 RID: 40467
		public bool HasDeviceType;

		// Token: 0x04009E14 RID: 40468
		private string _DeviceType;

		// Token: 0x04009E15 RID: 40469
		public bool HasFirstInstallDate;

		// Token: 0x04009E16 RID: 40470
		private ulong _FirstInstallDate;

		// Token: 0x04009E17 RID: 40471
		public bool HasBundleId;

		// Token: 0x04009E18 RID: 40472
		private string _BundleId;

		// Token: 0x04009E19 RID: 40473
		public bool HasGameMode;

		// Token: 0x04009E1A RID: 40474
		private string _GameMode;

		// Token: 0x04009E1B RID: 40475
		public bool HasFormatType;

		// Token: 0x04009E1C RID: 40476
		private FormatType _FormatType;
	}
}
