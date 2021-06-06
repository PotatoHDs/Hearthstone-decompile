using System;
using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000524 RID: 1316
	public class AccountReference : IProtoBuf
	{
		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06005DF3 RID: 24051 RVA: 0x0011CC57 File Offset: 0x0011AE57
		// (set) Token: 0x06005DF4 RID: 24052 RVA: 0x0011CC5F File Offset: 0x0011AE5F
		public uint Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		// Token: 0x06005DF5 RID: 24053 RVA: 0x0011CC6F File Offset: 0x0011AE6F
		public void SetId(uint val)
		{
			this.Id = val;
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x06005DF6 RID: 24054 RVA: 0x0011CC78 File Offset: 0x0011AE78
		// (set) Token: 0x06005DF7 RID: 24055 RVA: 0x0011CC80 File Offset: 0x0011AE80
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				this._Email = value;
				this.HasEmail = (value != null);
			}
		}

		// Token: 0x06005DF8 RID: 24056 RVA: 0x0011CC93 File Offset: 0x0011AE93
		public void SetEmail(string val)
		{
			this.Email = val;
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06005DF9 RID: 24057 RVA: 0x0011CC9C File Offset: 0x0011AE9C
		// (set) Token: 0x06005DFA RID: 24058 RVA: 0x0011CCA4 File Offset: 0x0011AEA4
		public GameAccountHandle Handle
		{
			get
			{
				return this._Handle;
			}
			set
			{
				this._Handle = value;
				this.HasHandle = (value != null);
			}
		}

		// Token: 0x06005DFB RID: 24059 RVA: 0x0011CCB7 File Offset: 0x0011AEB7
		public void SetHandle(GameAccountHandle val)
		{
			this.Handle = val;
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06005DFC RID: 24060 RVA: 0x0011CCC0 File Offset: 0x0011AEC0
		// (set) Token: 0x06005DFD RID: 24061 RVA: 0x0011CCC8 File Offset: 0x0011AEC8
		public string BattleTag
		{
			get
			{
				return this._BattleTag;
			}
			set
			{
				this._BattleTag = value;
				this.HasBattleTag = (value != null);
			}
		}

		// Token: 0x06005DFE RID: 24062 RVA: 0x0011CCDB File Offset: 0x0011AEDB
		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06005DFF RID: 24063 RVA: 0x0011CCE4 File Offset: 0x0011AEE4
		// (set) Token: 0x06005E00 RID: 24064 RVA: 0x0011CCEC File Offset: 0x0011AEEC
		public uint Region
		{
			get
			{
				return this._Region;
			}
			set
			{
				this._Region = value;
				this.HasRegion = true;
			}
		}

		// Token: 0x06005E01 RID: 24065 RVA: 0x0011CCFC File Offset: 0x0011AEFC
		public void SetRegion(uint val)
		{
			this.Region = val;
		}

		// Token: 0x06005E02 RID: 24066 RVA: 0x0011CD08 File Offset: 0x0011AF08
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			if (this.HasEmail)
			{
				num ^= this.Email.GetHashCode();
			}
			if (this.HasHandle)
			{
				num ^= this.Handle.GetHashCode();
			}
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			if (this.HasRegion)
			{
				num ^= this.Region.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005E03 RID: 24067 RVA: 0x0011CD98 File Offset: 0x0011AF98
		public override bool Equals(object obj)
		{
			AccountReference accountReference = obj as AccountReference;
			return accountReference != null && this.HasId == accountReference.HasId && (!this.HasId || this.Id.Equals(accountReference.Id)) && this.HasEmail == accountReference.HasEmail && (!this.HasEmail || this.Email.Equals(accountReference.Email)) && this.HasHandle == accountReference.HasHandle && (!this.HasHandle || this.Handle.Equals(accountReference.Handle)) && this.HasBattleTag == accountReference.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(accountReference.BattleTag)) && this.HasRegion == accountReference.HasRegion && (!this.HasRegion || this.Region.Equals(accountReference.Region));
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06005E04 RID: 24068 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005E05 RID: 24069 RVA: 0x0011CE8F File Offset: 0x0011B08F
		public static AccountReference ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountReference>(bs, 0, -1);
		}

		// Token: 0x06005E06 RID: 24070 RVA: 0x0011CE99 File Offset: 0x0011B099
		public void Deserialize(Stream stream)
		{
			AccountReference.Deserialize(stream, this);
		}

		// Token: 0x06005E07 RID: 24071 RVA: 0x0011CEA3 File Offset: 0x0011B0A3
		public static AccountReference Deserialize(Stream stream, AccountReference instance)
		{
			return AccountReference.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005E08 RID: 24072 RVA: 0x0011CEB0 File Offset: 0x0011B0B0
		public static AccountReference DeserializeLengthDelimited(Stream stream)
		{
			AccountReference accountReference = new AccountReference();
			AccountReference.DeserializeLengthDelimited(stream, accountReference);
			return accountReference;
		}

		// Token: 0x06005E09 RID: 24073 RVA: 0x0011CECC File Offset: 0x0011B0CC
		public static AccountReference DeserializeLengthDelimited(Stream stream, AccountReference instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountReference.Deserialize(stream, instance, num);
		}

		// Token: 0x06005E0A RID: 24074 RVA: 0x0011CEF4 File Offset: 0x0011B0F4
		public static AccountReference Deserialize(Stream stream, AccountReference instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Region = 0U;
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
						if (num == 13)
						{
							instance.Id = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 18)
						{
							instance.Email = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 34)
						{
							instance.BattleTag = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 80)
						{
							instance.Region = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else
					{
						if (instance.Handle == null)
						{
							instance.Handle = GameAccountHandle.DeserializeLengthDelimited(stream);
							continue;
						}
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.Handle);
						continue;
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

		// Token: 0x06005E0B RID: 24075 RVA: 0x0011D003 File Offset: 0x0011B203
		public void Serialize(Stream stream)
		{
			AccountReference.Serialize(stream, this);
		}

		// Token: 0x06005E0C RID: 24076 RVA: 0x0011D00C File Offset: 0x0011B20C
		public static void Serialize(Stream stream, AccountReference instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Id);
			}
			if (instance.HasEmail)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Email));
			}
			if (instance.HasHandle)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Handle.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Handle);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasRegion)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
		}

		// Token: 0x06005E0D RID: 24077 RVA: 0x0011D0D4 File Offset: 0x0011B2D4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasEmail)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Email);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasHandle)
			{
				num += 1U;
				uint serializedSize = this.Handle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasBattleTag)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasRegion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Region);
			}
			return num;
		}

		// Token: 0x04001CE8 RID: 7400
		public bool HasId;

		// Token: 0x04001CE9 RID: 7401
		private uint _Id;

		// Token: 0x04001CEA RID: 7402
		public bool HasEmail;

		// Token: 0x04001CEB RID: 7403
		private string _Email;

		// Token: 0x04001CEC RID: 7404
		public bool HasHandle;

		// Token: 0x04001CED RID: 7405
		private GameAccountHandle _Handle;

		// Token: 0x04001CEE RID: 7406
		public bool HasBattleTag;

		// Token: 0x04001CEF RID: 7407
		private string _BattleTag;

		// Token: 0x04001CF0 RID: 7408
		public bool HasRegion;

		// Token: 0x04001CF1 RID: 7409
		private uint _Region;
	}
}
