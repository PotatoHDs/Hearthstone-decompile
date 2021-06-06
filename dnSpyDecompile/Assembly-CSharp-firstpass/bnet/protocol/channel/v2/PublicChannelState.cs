using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200047C RID: 1148
	public class PublicChannelState : IProtoBuf
	{
		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06004F56 RID: 20310 RVA: 0x000F6217 File Offset: 0x000F4417
		// (set) Token: 0x06004F57 RID: 20311 RVA: 0x000F621F File Offset: 0x000F441F
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

		// Token: 0x06004F58 RID: 20312 RVA: 0x000F6232 File Offset: 0x000F4432
		public void SetIdentity(string val)
		{
			this.Identity = val;
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06004F59 RID: 20313 RVA: 0x000F623B File Offset: 0x000F443B
		// (set) Token: 0x06004F5A RID: 20314 RVA: 0x000F6243 File Offset: 0x000F4443
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

		// Token: 0x06004F5B RID: 20315 RVA: 0x000F6253 File Offset: 0x000F4453
		public void SetLocale(uint val)
		{
			this.Locale = val;
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06004F5C RID: 20316 RVA: 0x000F625C File Offset: 0x000F445C
		// (set) Token: 0x06004F5D RID: 20317 RVA: 0x000F6264 File Offset: 0x000F4464
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

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06004F5E RID: 20318 RVA: 0x000F625C File Offset: 0x000F445C
		public List<bnet.protocol.v2.Attribute> SearchAttributeList
		{
			get
			{
				return this._SearchAttribute;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06004F5F RID: 20319 RVA: 0x000F626D File Offset: 0x000F446D
		public int SearchAttributeCount
		{
			get
			{
				return this._SearchAttribute.Count;
			}
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x000F627A File Offset: 0x000F447A
		public void AddSearchAttribute(bnet.protocol.v2.Attribute val)
		{
			this._SearchAttribute.Add(val);
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x000F6288 File Offset: 0x000F4488
		public void ClearSearchAttribute()
		{
			this._SearchAttribute.Clear();
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x000F6295 File Offset: 0x000F4495
		public void SetSearchAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.SearchAttribute = val;
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06004F63 RID: 20323 RVA: 0x000F629E File Offset: 0x000F449E
		// (set) Token: 0x06004F64 RID: 20324 RVA: 0x000F62A6 File Offset: 0x000F44A6
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

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06004F65 RID: 20325 RVA: 0x000F629E File Offset: 0x000F449E
		public List<AccountId> ReservationList
		{
			get
			{
				return this._Reservation;
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06004F66 RID: 20326 RVA: 0x000F62AF File Offset: 0x000F44AF
		public int ReservationCount
		{
			get
			{
				return this._Reservation.Count;
			}
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x000F62BC File Offset: 0x000F44BC
		public void AddReservation(AccountId val)
		{
			this._Reservation.Add(val);
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x000F62CA File Offset: 0x000F44CA
		public void ClearReservation()
		{
			this._Reservation.Clear();
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x000F62D7 File Offset: 0x000F44D7
		public void SetReservation(List<AccountId> val)
		{
			this.Reservation = val;
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x000F62E0 File Offset: 0x000F44E0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
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

		// Token: 0x06004F6B RID: 20331 RVA: 0x000F63B8 File Offset: 0x000F45B8
		public override bool Equals(object obj)
		{
			PublicChannelState publicChannelState = obj as PublicChannelState;
			if (publicChannelState == null)
			{
				return false;
			}
			if (this.HasIdentity != publicChannelState.HasIdentity || (this.HasIdentity && !this.Identity.Equals(publicChannelState.Identity)))
			{
				return false;
			}
			if (this.HasLocale != publicChannelState.HasLocale || (this.HasLocale && !this.Locale.Equals(publicChannelState.Locale)))
			{
				return false;
			}
			if (this.SearchAttribute.Count != publicChannelState.SearchAttribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.SearchAttribute.Count; i++)
			{
				if (!this.SearchAttribute[i].Equals(publicChannelState.SearchAttribute[i]))
				{
					return false;
				}
			}
			if (this.Reservation.Count != publicChannelState.Reservation.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Reservation.Count; j++)
			{
				if (!this.Reservation[j].Equals(publicChannelState.Reservation[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06004F6C RID: 20332 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x000F64CD File Offset: 0x000F46CD
		public static PublicChannelState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PublicChannelState>(bs, 0, -1);
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x000F64D7 File Offset: 0x000F46D7
		public void Deserialize(Stream stream)
		{
			PublicChannelState.Deserialize(stream, this);
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x000F64E1 File Offset: 0x000F46E1
		public static PublicChannelState Deserialize(Stream stream, PublicChannelState instance)
		{
			return PublicChannelState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004F70 RID: 20336 RVA: 0x000F64EC File Offset: 0x000F46EC
		public static PublicChannelState DeserializeLengthDelimited(Stream stream)
		{
			PublicChannelState publicChannelState = new PublicChannelState();
			PublicChannelState.DeserializeLengthDelimited(stream, publicChannelState);
			return publicChannelState;
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x000F6508 File Offset: 0x000F4708
		public static PublicChannelState DeserializeLengthDelimited(Stream stream, PublicChannelState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PublicChannelState.Deserialize(stream, instance, num);
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x000F6530 File Offset: 0x000F4730
		public static PublicChannelState Deserialize(Stream stream, PublicChannelState instance, long limit)
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
					if (num <= 21)
					{
						if (num == 10)
						{
							instance.Identity = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 21)
						{
							instance.Locale = binaryReader.ReadUInt32();
							continue;
						}
					}
					else
					{
						if (num == 26)
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

		// Token: 0x06004F73 RID: 20339 RVA: 0x000F6638 File Offset: 0x000F4838
		public void Serialize(Stream stream)
		{
			PublicChannelState.Serialize(stream, this);
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x000F6644 File Offset: 0x000F4844
		public static void Serialize(Stream stream, PublicChannelState instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Identity));
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Locale);
			}
			if (instance.SearchAttribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.SearchAttribute)
				{
					stream.WriteByte(26);
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

		// Token: 0x06004F75 RID: 20341 RVA: 0x000F676C File Offset: 0x000F496C
		public uint GetSerializedSize()
		{
			uint num = 0U;
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
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.Reservation.Count > 0)
			{
				foreach (AccountId accountId in this.Reservation)
				{
					num += 1U;
					uint serializedSize2 = accountId.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x040019A8 RID: 6568
		public bool HasIdentity;

		// Token: 0x040019A9 RID: 6569
		private string _Identity;

		// Token: 0x040019AA RID: 6570
		public bool HasLocale;

		// Token: 0x040019AB RID: 6571
		private uint _Locale;

		// Token: 0x040019AC RID: 6572
		private List<bnet.protocol.v2.Attribute> _SearchAttribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x040019AD RID: 6573
		private List<AccountId> _Reservation = new List<AccountId>();
	}
}
