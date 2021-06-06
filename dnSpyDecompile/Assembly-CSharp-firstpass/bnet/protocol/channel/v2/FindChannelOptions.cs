using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000479 RID: 1145
	public class FindChannelOptions : IProtoBuf
	{
		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06004EFE RID: 20222 RVA: 0x000F5143 File Offset: 0x000F3343
		// (set) Token: 0x06004EFF RID: 20223 RVA: 0x000F514B File Offset: 0x000F334B
		public UniqueChannelType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = (value != null);
			}
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x000F515E File Offset: 0x000F335E
		public void SetType(UniqueChannelType val)
		{
			this.Type = val;
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06004F01 RID: 20225 RVA: 0x000F5167 File Offset: 0x000F3367
		// (set) Token: 0x06004F02 RID: 20226 RVA: 0x000F516F File Offset: 0x000F336F
		public string Identity
		{
			get
			{
				return this._Identity;
			}
			set
			{
				this._Identity = value;
				this.HasIdentity = (value != null);
			}
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x000F5182 File Offset: 0x000F3382
		public void SetIdentity(string val)
		{
			this.Identity = val;
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06004F04 RID: 20228 RVA: 0x000F518B File Offset: 0x000F338B
		// (set) Token: 0x06004F05 RID: 20229 RVA: 0x000F5193 File Offset: 0x000F3393
		public uint Locale
		{
			get
			{
				return this._Locale;
			}
			set
			{
				this._Locale = value;
				this.HasLocale = true;
			}
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x000F51A3 File Offset: 0x000F33A3
		public void SetLocale(uint val)
		{
			this.Locale = val;
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06004F07 RID: 20231 RVA: 0x000F51AC File Offset: 0x000F33AC
		// (set) Token: 0x06004F08 RID: 20232 RVA: 0x000F51B4 File Offset: 0x000F33B4
		public List<bnet.protocol.v2.Attribute> SearchAttribute
		{
			get
			{
				return this._SearchAttribute;
			}
			set
			{
				this._SearchAttribute = value;
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06004F09 RID: 20233 RVA: 0x000F51AC File Offset: 0x000F33AC
		public List<bnet.protocol.v2.Attribute> SearchAttributeList
		{
			get
			{
				return this._SearchAttribute;
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06004F0A RID: 20234 RVA: 0x000F51BD File Offset: 0x000F33BD
		public int SearchAttributeCount
		{
			get
			{
				return this._SearchAttribute.Count;
			}
		}

		// Token: 0x06004F0B RID: 20235 RVA: 0x000F51CA File Offset: 0x000F33CA
		public void AddSearchAttribute(bnet.protocol.v2.Attribute val)
		{
			this._SearchAttribute.Add(val);
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x000F51D8 File Offset: 0x000F33D8
		public void ClearSearchAttribute()
		{
			this._SearchAttribute.Clear();
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x000F51E5 File Offset: 0x000F33E5
		public void SetSearchAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.SearchAttribute = val;
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06004F0E RID: 20238 RVA: 0x000F51EE File Offset: 0x000F33EE
		// (set) Token: 0x06004F0F RID: 20239 RVA: 0x000F51F6 File Offset: 0x000F33F6
		public List<AccountId> Reservation
		{
			get
			{
				return this._Reservation;
			}
			set
			{
				this._Reservation = value;
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06004F10 RID: 20240 RVA: 0x000F51EE File Offset: 0x000F33EE
		public List<AccountId> ReservationList
		{
			get
			{
				return this._Reservation;
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06004F11 RID: 20241 RVA: 0x000F51FF File Offset: 0x000F33FF
		public int ReservationCount
		{
			get
			{
				return this._Reservation.Count;
			}
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x000F520C File Offset: 0x000F340C
		public void AddReservation(AccountId val)
		{
			this._Reservation.Add(val);
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x000F521A File Offset: 0x000F341A
		public void ClearReservation()
		{
			this._Reservation.Clear();
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x000F5227 File Offset: 0x000F3427
		public void SetReservation(List<AccountId> val)
		{
			this.Reservation = val;
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x000F5230 File Offset: 0x000F3430
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			if (this.HasLocale)
			{
				num ^= this.Locale.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.SearchAttribute)
			{
				num ^= attribute.GetHashCode();
			}
			foreach (AccountId accountId in this.Reservation)
			{
				num ^= accountId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x000F5320 File Offset: 0x000F3520
		public override bool Equals(object obj)
		{
			FindChannelOptions findChannelOptions = obj as FindChannelOptions;
			if (findChannelOptions == null)
			{
				return false;
			}
			if (this.HasType != findChannelOptions.HasType || (this.HasType && !this.Type.Equals(findChannelOptions.Type)))
			{
				return false;
			}
			if (this.HasIdentity != findChannelOptions.HasIdentity || (this.HasIdentity && !this.Identity.Equals(findChannelOptions.Identity)))
			{
				return false;
			}
			if (this.HasLocale != findChannelOptions.HasLocale || (this.HasLocale && !this.Locale.Equals(findChannelOptions.Locale)))
			{
				return false;
			}
			if (this.SearchAttribute.Count != findChannelOptions.SearchAttribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.SearchAttribute.Count; i++)
			{
				if (!this.SearchAttribute[i].Equals(findChannelOptions.SearchAttribute[i]))
				{
					return false;
				}
			}
			if (this.Reservation.Count != findChannelOptions.Reservation.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Reservation.Count; j++)
			{
				if (!this.Reservation[j].Equals(findChannelOptions.Reservation[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06004F17 RID: 20247 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x000F5460 File Offset: 0x000F3660
		public static FindChannelOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindChannelOptions>(bs, 0, -1);
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x000F546A File Offset: 0x000F366A
		public void Deserialize(Stream stream)
		{
			FindChannelOptions.Deserialize(stream, this);
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x000F5474 File Offset: 0x000F3674
		public static FindChannelOptions Deserialize(Stream stream, FindChannelOptions instance)
		{
			return FindChannelOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x000F5480 File Offset: 0x000F3680
		public static FindChannelOptions DeserializeLengthDelimited(Stream stream)
		{
			FindChannelOptions findChannelOptions = new FindChannelOptions();
			FindChannelOptions.DeserializeLengthDelimited(stream, findChannelOptions);
			return findChannelOptions;
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x000F549C File Offset: 0x000F369C
		public static FindChannelOptions DeserializeLengthDelimited(Stream stream, FindChannelOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FindChannelOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x000F54C4 File Offset: 0x000F36C4
		public static FindChannelOptions Deserialize(Stream stream, FindChannelOptions instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.SearchAttribute == null)
			{
				instance.SearchAttribute = new List<bnet.protocol.v2.Attribute>();
			}
			if (instance.Reservation == null)
			{
				instance.Reservation = new List<AccountId>();
			}
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
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.Identity = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.Type == null)
							{
								instance.Type = UniqueChannelType.DeserializeLengthDelimited(stream);
								continue;
							}
							UniqueChannelType.DeserializeLengthDelimited(stream, instance.Type);
							continue;
						}
					}
					else
					{
						if (num == 29)
						{
							instance.Locale = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 34)
						{
							instance.SearchAttribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 50)
						{
							instance.Reservation.Add(AccountId.DeserializeLengthDelimited(stream));
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

		// Token: 0x06004F1E RID: 20254 RVA: 0x000F5602 File Offset: 0x000F3802
		public void Serialize(Stream stream)
		{
			FindChannelOptions.Serialize(stream, this);
		}

		// Token: 0x06004F1F RID: 20255 RVA: 0x000F560C File Offset: 0x000F380C
		public static void Serialize(Stream stream, FindChannelOptions instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Type.GetSerializedSize());
				UniqueChannelType.Serialize(stream, instance.Type);
			}
			if (instance.HasIdentity)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Identity));
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Locale);
			}
			if (instance.SearchAttribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.SearchAttribute)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.Reservation.Count > 0)
			{
				foreach (AccountId accountId in instance.Reservation)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, accountId.GetSerializedSize());
					AccountId.Serialize(stream, accountId);
				}
			}
		}

		// Token: 0x06004F20 RID: 20256 RVA: 0x000F5760 File Offset: 0x000F3960
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasType)
			{
				num += 1U;
				uint serializedSize = this.Type.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasIdentity)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Identity);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasLocale)
			{
				num += 1U;
				num += 4U;
			}
			if (this.SearchAttribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.SearchAttribute)
				{
					num += 1U;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.Reservation.Count > 0)
			{
				foreach (AccountId accountId in this.Reservation)
				{
					num += 1U;
					uint serializedSize3 = accountId.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num;
		}

		// Token: 0x04001996 RID: 6550
		public bool HasType;

		// Token: 0x04001997 RID: 6551
		private UniqueChannelType _Type;

		// Token: 0x04001998 RID: 6552
		public bool HasIdentity;

		// Token: 0x04001999 RID: 6553
		private string _Identity;

		// Token: 0x0400199A RID: 6554
		public bool HasLocale;

		// Token: 0x0400199B RID: 6555
		private uint _Locale;

		// Token: 0x0400199C RID: 6556
		private List<bnet.protocol.v2.Attribute> _SearchAttribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x0400199D RID: 6557
		private List<AccountId> _Reservation = new List<AccountId>();
	}
}
