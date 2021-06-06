using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x0200119A RID: 4506
	public class AttributionLogin : IProtoBuf
	{
		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x0600C6E2 RID: 50914 RVA: 0x003BCCEC File Offset: 0x003BAEEC
		// (set) Token: 0x0600C6E3 RID: 50915 RVA: 0x003BCCF4 File Offset: 0x003BAEF4
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

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x0600C6E4 RID: 50916 RVA: 0x003BCD07 File Offset: 0x003BAF07
		// (set) Token: 0x0600C6E5 RID: 50917 RVA: 0x003BCD0F File Offset: 0x003BAF0F
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

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x0600C6E6 RID: 50918 RVA: 0x003BCD22 File Offset: 0x003BAF22
		// (set) Token: 0x0600C6E7 RID: 50919 RVA: 0x003BCD2A File Offset: 0x003BAF2A
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

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x0600C6E8 RID: 50920 RVA: 0x003BCD3A File Offset: 0x003BAF3A
		// (set) Token: 0x0600C6E9 RID: 50921 RVA: 0x003BCD42 File Offset: 0x003BAF42
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

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x0600C6EA RID: 50922 RVA: 0x003BCD55 File Offset: 0x003BAF55
		// (set) Token: 0x0600C6EB RID: 50923 RVA: 0x003BCD5D File Offset: 0x003BAF5D
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

		// Token: 0x0600C6EC RID: 50924 RVA: 0x003BCD70 File Offset: 0x003BAF70
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

		// Token: 0x0600C6ED RID: 50925 RVA: 0x003BCDFC File Offset: 0x003BAFFC
		public override bool Equals(object obj)
		{
			AttributionLogin attributionLogin = obj as AttributionLogin;
			return attributionLogin != null && this.HasApplicationId == attributionLogin.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(attributionLogin.ApplicationId)) && this.HasDeviceType == attributionLogin.HasDeviceType && (!this.HasDeviceType || this.DeviceType.Equals(attributionLogin.DeviceType)) && this.HasFirstInstallDate == attributionLogin.HasFirstInstallDate && (!this.HasFirstInstallDate || this.FirstInstallDate.Equals(attributionLogin.FirstInstallDate)) && this.HasBundleId == attributionLogin.HasBundleId && (!this.HasBundleId || this.BundleId.Equals(attributionLogin.BundleId)) && this.HasIdentifier == attributionLogin.HasIdentifier && (!this.HasIdentifier || this.Identifier.Equals(attributionLogin.Identifier));
		}

		// Token: 0x0600C6EE RID: 50926 RVA: 0x003BCEF0 File Offset: 0x003BB0F0
		public void Deserialize(Stream stream)
		{
			AttributionLogin.Deserialize(stream, this);
		}

		// Token: 0x0600C6EF RID: 50927 RVA: 0x003BCEFA File Offset: 0x003BB0FA
		public static AttributionLogin Deserialize(Stream stream, AttributionLogin instance)
		{
			return AttributionLogin.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C6F0 RID: 50928 RVA: 0x003BCF08 File Offset: 0x003BB108
		public static AttributionLogin DeserializeLengthDelimited(Stream stream)
		{
			AttributionLogin attributionLogin = new AttributionLogin();
			AttributionLogin.DeserializeLengthDelimited(stream, attributionLogin);
			return attributionLogin;
		}

		// Token: 0x0600C6F1 RID: 50929 RVA: 0x003BCF24 File Offset: 0x003BB124
		public static AttributionLogin DeserializeLengthDelimited(Stream stream, AttributionLogin instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributionLogin.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C6F2 RID: 50930 RVA: 0x003BCF4C File Offset: 0x003BB14C
		public static AttributionLogin Deserialize(Stream stream, AttributionLogin instance, long limit)
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

		// Token: 0x0600C6F3 RID: 50931 RVA: 0x003BD095 File Offset: 0x003BB295
		public void Serialize(Stream stream)
		{
			AttributionLogin.Serialize(stream, this);
		}

		// Token: 0x0600C6F4 RID: 50932 RVA: 0x003BD0A0 File Offset: 0x003BB2A0
		public static void Serialize(Stream stream, AttributionLogin instance)
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

		// Token: 0x0600C6F5 RID: 50933 RVA: 0x003BD19C File Offset: 0x003BB39C
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

		// Token: 0x04009E3F RID: 40511
		public bool HasApplicationId;

		// Token: 0x04009E40 RID: 40512
		private string _ApplicationId;

		// Token: 0x04009E41 RID: 40513
		public bool HasDeviceType;

		// Token: 0x04009E42 RID: 40514
		private string _DeviceType;

		// Token: 0x04009E43 RID: 40515
		public bool HasFirstInstallDate;

		// Token: 0x04009E44 RID: 40516
		private ulong _FirstInstallDate;

		// Token: 0x04009E45 RID: 40517
		public bool HasBundleId;

		// Token: 0x04009E46 RID: 40518
		private string _BundleId;

		// Token: 0x04009E47 RID: 40519
		public bool HasIdentifier;

		// Token: 0x04009E48 RID: 40520
		private IdentifierInfo _Identifier;
	}
}
