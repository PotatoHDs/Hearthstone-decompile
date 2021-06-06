using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001197 RID: 4503
	public class AttributionHeadlessAccountCreated : IProtoBuf
	{
		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x0600C69F RID: 50847 RVA: 0x003BBB54 File Offset: 0x003B9D54
		// (set) Token: 0x0600C6A0 RID: 50848 RVA: 0x003BBB5C File Offset: 0x003B9D5C
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

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x0600C6A1 RID: 50849 RVA: 0x003BBB6F File Offset: 0x003B9D6F
		// (set) Token: 0x0600C6A2 RID: 50850 RVA: 0x003BBB77 File Offset: 0x003B9D77
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

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x0600C6A3 RID: 50851 RVA: 0x003BBB8A File Offset: 0x003B9D8A
		// (set) Token: 0x0600C6A4 RID: 50852 RVA: 0x003BBB92 File Offset: 0x003B9D92
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

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x0600C6A5 RID: 50853 RVA: 0x003BBBA2 File Offset: 0x003B9DA2
		// (set) Token: 0x0600C6A6 RID: 50854 RVA: 0x003BBBAA File Offset: 0x003B9DAA
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

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x0600C6A7 RID: 50855 RVA: 0x003BBBBD File Offset: 0x003B9DBD
		// (set) Token: 0x0600C6A8 RID: 50856 RVA: 0x003BBBC5 File Offset: 0x003B9DC5
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

		// Token: 0x0600C6A9 RID: 50857 RVA: 0x003BBBD8 File Offset: 0x003B9DD8
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
			if (this.HasIdentifier)
			{
				num ^= this.Identifier.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C6AA RID: 50858 RVA: 0x003BBC64 File Offset: 0x003B9E64
		public override bool Equals(object obj)
		{
			AttributionHeadlessAccountCreated attributionHeadlessAccountCreated = obj as AttributionHeadlessAccountCreated;
			return attributionHeadlessAccountCreated != null && this.HasApplicationId == attributionHeadlessAccountCreated.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(attributionHeadlessAccountCreated.ApplicationId)) && this.HasDeviceType == attributionHeadlessAccountCreated.HasDeviceType && (!this.HasDeviceType || this.DeviceType.Equals(attributionHeadlessAccountCreated.DeviceType)) && this.HasFirstInstallDate == attributionHeadlessAccountCreated.HasFirstInstallDate && (!this.HasFirstInstallDate || this.FirstInstallDate.Equals(attributionHeadlessAccountCreated.FirstInstallDate)) && this.HasBundleId == attributionHeadlessAccountCreated.HasBundleId && (!this.HasBundleId || this.BundleId.Equals(attributionHeadlessAccountCreated.BundleId)) && this.HasIdentifier == attributionHeadlessAccountCreated.HasIdentifier && (!this.HasIdentifier || this.Identifier.Equals(attributionHeadlessAccountCreated.Identifier));
		}

		// Token: 0x0600C6AB RID: 50859 RVA: 0x003BBD58 File Offset: 0x003B9F58
		public void Deserialize(Stream stream)
		{
			AttributionHeadlessAccountCreated.Deserialize(stream, this);
		}

		// Token: 0x0600C6AC RID: 50860 RVA: 0x003BBD62 File Offset: 0x003B9F62
		public static AttributionHeadlessAccountCreated Deserialize(Stream stream, AttributionHeadlessAccountCreated instance)
		{
			return AttributionHeadlessAccountCreated.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C6AD RID: 50861 RVA: 0x003BBD70 File Offset: 0x003B9F70
		public static AttributionHeadlessAccountCreated DeserializeLengthDelimited(Stream stream)
		{
			AttributionHeadlessAccountCreated attributionHeadlessAccountCreated = new AttributionHeadlessAccountCreated();
			AttributionHeadlessAccountCreated.DeserializeLengthDelimited(stream, attributionHeadlessAccountCreated);
			return attributionHeadlessAccountCreated;
		}

		// Token: 0x0600C6AE RID: 50862 RVA: 0x003BBD8C File Offset: 0x003B9F8C
		public static AttributionHeadlessAccountCreated DeserializeLengthDelimited(Stream stream, AttributionHeadlessAccountCreated instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributionHeadlessAccountCreated.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C6AF RID: 50863 RVA: 0x003BBDB4 File Offset: 0x003B9FB4
		public static AttributionHeadlessAccountCreated Deserialize(Stream stream, AttributionHeadlessAccountCreated instance, long limit)
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

		// Token: 0x0600C6B0 RID: 50864 RVA: 0x003BBEFD File Offset: 0x003BA0FD
		public void Serialize(Stream stream)
		{
			AttributionHeadlessAccountCreated.Serialize(stream, this);
		}

		// Token: 0x0600C6B1 RID: 50865 RVA: 0x003BBF08 File Offset: 0x003BA108
		public static void Serialize(Stream stream, AttributionHeadlessAccountCreated instance)
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
			if (instance.HasIdentifier)
			{
				stream.WriteByte(194);
				stream.WriteByte(62);
				ProtocolParser.WriteUInt32(stream, instance.Identifier.GetSerializedSize());
				IdentifierInfo.Serialize(stream, instance.Identifier);
			}
		}

		// Token: 0x0600C6B2 RID: 50866 RVA: 0x003BC004 File Offset: 0x003BA204
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
			if (this.HasIdentifier)
			{
				num += 2U;
				uint serializedSize = this.Identifier.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04009E1D RID: 40477
		public bool HasApplicationId;

		// Token: 0x04009E1E RID: 40478
		private string _ApplicationId;

		// Token: 0x04009E1F RID: 40479
		public bool HasDeviceType;

		// Token: 0x04009E20 RID: 40480
		private string _DeviceType;

		// Token: 0x04009E21 RID: 40481
		public bool HasFirstInstallDate;

		// Token: 0x04009E22 RID: 40482
		private ulong _FirstInstallDate;

		// Token: 0x04009E23 RID: 40483
		public bool HasBundleId;

		// Token: 0x04009E24 RID: 40484
		private string _BundleId;

		// Token: 0x04009E25 RID: 40485
		public bool HasIdentifier;

		// Token: 0x04009E26 RID: 40486
		private IdentifierInfo _Identifier;
	}
}
