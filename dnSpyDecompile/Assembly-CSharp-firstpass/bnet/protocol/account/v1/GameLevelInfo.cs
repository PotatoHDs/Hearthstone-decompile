using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000530 RID: 1328
	public class GameLevelInfo : IProtoBuf
	{
		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06005F73 RID: 24435 RVA: 0x0012140F File Offset: 0x0011F60F
		// (set) Token: 0x06005F74 RID: 24436 RVA: 0x00121417 File Offset: 0x0011F617
		public bool IsTrial
		{
			get
			{
				return this._IsTrial;
			}
			set
			{
				this._IsTrial = value;
				this.HasIsTrial = true;
			}
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x00121427 File Offset: 0x0011F627
		public void SetIsTrial(bool val)
		{
			this.IsTrial = val;
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06005F76 RID: 24438 RVA: 0x00121430 File Offset: 0x0011F630
		// (set) Token: 0x06005F77 RID: 24439 RVA: 0x00121438 File Offset: 0x0011F638
		public bool IsLifetime
		{
			get
			{
				return this._IsLifetime;
			}
			set
			{
				this._IsLifetime = value;
				this.HasIsLifetime = true;
			}
		}

		// Token: 0x06005F78 RID: 24440 RVA: 0x00121448 File Offset: 0x0011F648
		public void SetIsLifetime(bool val)
		{
			this.IsLifetime = val;
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06005F79 RID: 24441 RVA: 0x00121451 File Offset: 0x0011F651
		// (set) Token: 0x06005F7A RID: 24442 RVA: 0x00121459 File Offset: 0x0011F659
		public bool IsRestricted
		{
			get
			{
				return this._IsRestricted;
			}
			set
			{
				this._IsRestricted = value;
				this.HasIsRestricted = true;
			}
		}

		// Token: 0x06005F7B RID: 24443 RVA: 0x00121469 File Offset: 0x0011F669
		public void SetIsRestricted(bool val)
		{
			this.IsRestricted = val;
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06005F7C RID: 24444 RVA: 0x00121472 File Offset: 0x0011F672
		// (set) Token: 0x06005F7D RID: 24445 RVA: 0x0012147A File Offset: 0x0011F67A
		public bool IsBeta
		{
			get
			{
				return this._IsBeta;
			}
			set
			{
				this._IsBeta = value;
				this.HasIsBeta = true;
			}
		}

		// Token: 0x06005F7E RID: 24446 RVA: 0x0012148A File Offset: 0x0011F68A
		public void SetIsBeta(bool val)
		{
			this.IsBeta = val;
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06005F7F RID: 24447 RVA: 0x00121493 File Offset: 0x0011F693
		// (set) Token: 0x06005F80 RID: 24448 RVA: 0x0012149B File Offset: 0x0011F69B
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		// Token: 0x06005F81 RID: 24449 RVA: 0x001214AE File Offset: 0x0011F6AE
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06005F82 RID: 24450 RVA: 0x001214B7 File Offset: 0x0011F6B7
		// (set) Token: 0x06005F83 RID: 24451 RVA: 0x001214BF File Offset: 0x0011F6BF
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x06005F84 RID: 24452 RVA: 0x001214CF File Offset: 0x0011F6CF
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06005F85 RID: 24453 RVA: 0x001214D8 File Offset: 0x0011F6D8
		// (set) Token: 0x06005F86 RID: 24454 RVA: 0x001214E0 File Offset: 0x0011F6E0
		public List<AccountLicense> Licenses
		{
			get
			{
				return this._Licenses;
			}
			set
			{
				this._Licenses = value;
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06005F87 RID: 24455 RVA: 0x001214D8 File Offset: 0x0011F6D8
		public List<AccountLicense> LicensesList
		{
			get
			{
				return this._Licenses;
			}
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06005F88 RID: 24456 RVA: 0x001214E9 File Offset: 0x0011F6E9
		public int LicensesCount
		{
			get
			{
				return this._Licenses.Count;
			}
		}

		// Token: 0x06005F89 RID: 24457 RVA: 0x001214F6 File Offset: 0x0011F6F6
		public void AddLicenses(AccountLicense val)
		{
			this._Licenses.Add(val);
		}

		// Token: 0x06005F8A RID: 24458 RVA: 0x00121504 File Offset: 0x0011F704
		public void ClearLicenses()
		{
			this._Licenses.Clear();
		}

		// Token: 0x06005F8B RID: 24459 RVA: 0x00121511 File Offset: 0x0011F711
		public void SetLicenses(List<AccountLicense> val)
		{
			this.Licenses = val;
		}

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06005F8C RID: 24460 RVA: 0x0012151A File Offset: 0x0011F71A
		// (set) Token: 0x06005F8D RID: 24461 RVA: 0x00121522 File Offset: 0x0011F722
		public uint RealmPermissions
		{
			get
			{
				return this._RealmPermissions;
			}
			set
			{
				this._RealmPermissions = value;
				this.HasRealmPermissions = true;
			}
		}

		// Token: 0x06005F8E RID: 24462 RVA: 0x00121532 File Offset: 0x0011F732
		public void SetRealmPermissions(uint val)
		{
			this.RealmPermissions = val;
		}

		// Token: 0x06005F8F RID: 24463 RVA: 0x0012153C File Offset: 0x0011F73C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIsTrial)
			{
				num ^= this.IsTrial.GetHashCode();
			}
			if (this.HasIsLifetime)
			{
				num ^= this.IsLifetime.GetHashCode();
			}
			if (this.HasIsRestricted)
			{
				num ^= this.IsRestricted.GetHashCode();
			}
			if (this.HasIsBeta)
			{
				num ^= this.IsBeta.GetHashCode();
			}
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			foreach (AccountLicense accountLicense in this.Licenses)
			{
				num ^= accountLicense.GetHashCode();
			}
			if (this.HasRealmPermissions)
			{
				num ^= this.RealmPermissions.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005F90 RID: 24464 RVA: 0x0012164C File Offset: 0x0011F84C
		public override bool Equals(object obj)
		{
			GameLevelInfo gameLevelInfo = obj as GameLevelInfo;
			if (gameLevelInfo == null)
			{
				return false;
			}
			if (this.HasIsTrial != gameLevelInfo.HasIsTrial || (this.HasIsTrial && !this.IsTrial.Equals(gameLevelInfo.IsTrial)))
			{
				return false;
			}
			if (this.HasIsLifetime != gameLevelInfo.HasIsLifetime || (this.HasIsLifetime && !this.IsLifetime.Equals(gameLevelInfo.IsLifetime)))
			{
				return false;
			}
			if (this.HasIsRestricted != gameLevelInfo.HasIsRestricted || (this.HasIsRestricted && !this.IsRestricted.Equals(gameLevelInfo.IsRestricted)))
			{
				return false;
			}
			if (this.HasIsBeta != gameLevelInfo.HasIsBeta || (this.HasIsBeta && !this.IsBeta.Equals(gameLevelInfo.IsBeta)))
			{
				return false;
			}
			if (this.HasName != gameLevelInfo.HasName || (this.HasName && !this.Name.Equals(gameLevelInfo.Name)))
			{
				return false;
			}
			if (this.HasProgram != gameLevelInfo.HasProgram || (this.HasProgram && !this.Program.Equals(gameLevelInfo.Program)))
			{
				return false;
			}
			if (this.Licenses.Count != gameLevelInfo.Licenses.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Licenses.Count; i++)
			{
				if (!this.Licenses[i].Equals(gameLevelInfo.Licenses[i]))
				{
					return false;
				}
			}
			return this.HasRealmPermissions == gameLevelInfo.HasRealmPermissions && (!this.HasRealmPermissions || this.RealmPermissions.Equals(gameLevelInfo.RealmPermissions));
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06005F91 RID: 24465 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005F92 RID: 24466 RVA: 0x001217F6 File Offset: 0x0011F9F6
		public static GameLevelInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameLevelInfo>(bs, 0, -1);
		}

		// Token: 0x06005F93 RID: 24467 RVA: 0x00121800 File Offset: 0x0011FA00
		public void Deserialize(Stream stream)
		{
			GameLevelInfo.Deserialize(stream, this);
		}

		// Token: 0x06005F94 RID: 24468 RVA: 0x0012180A File Offset: 0x0011FA0A
		public static GameLevelInfo Deserialize(Stream stream, GameLevelInfo instance)
		{
			return GameLevelInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005F95 RID: 24469 RVA: 0x00121818 File Offset: 0x0011FA18
		public static GameLevelInfo DeserializeLengthDelimited(Stream stream)
		{
			GameLevelInfo gameLevelInfo = new GameLevelInfo();
			GameLevelInfo.DeserializeLengthDelimited(stream, gameLevelInfo);
			return gameLevelInfo;
		}

		// Token: 0x06005F96 RID: 24470 RVA: 0x00121834 File Offset: 0x0011FA34
		public static GameLevelInfo DeserializeLengthDelimited(Stream stream, GameLevelInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameLevelInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06005F97 RID: 24471 RVA: 0x0012185C File Offset: 0x0011FA5C
		public static GameLevelInfo Deserialize(Stream stream, GameLevelInfo instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Licenses == null)
			{
				instance.Licenses = new List<AccountLicense>();
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
					if (num <= 56)
					{
						if (num <= 40)
						{
							if (num == 32)
							{
								instance.IsTrial = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 40)
							{
								instance.IsLifetime = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (num == 48)
							{
								instance.IsRestricted = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 56)
							{
								instance.IsBeta = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
					}
					else if (num <= 77)
					{
						if (num == 66)
						{
							instance.Name = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 77)
						{
							instance.Program = binaryReader.ReadUInt32();
							continue;
						}
					}
					else
					{
						if (num == 82)
						{
							instance.Licenses.Add(AccountLicense.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 88)
						{
							instance.RealmPermissions = ProtocolParser.ReadUInt32(stream);
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

		// Token: 0x06005F98 RID: 24472 RVA: 0x001219C7 File Offset: 0x0011FBC7
		public void Serialize(Stream stream)
		{
			GameLevelInfo.Serialize(stream, this);
		}

		// Token: 0x06005F99 RID: 24473 RVA: 0x001219D0 File Offset: 0x0011FBD0
		public static void Serialize(Stream stream, GameLevelInfo instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasIsTrial)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsTrial);
			}
			if (instance.HasIsLifetime)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.IsLifetime);
			}
			if (instance.HasIsRestricted)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.IsRestricted);
			}
			if (instance.HasIsBeta)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.IsBeta);
			}
			if (instance.HasName)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(77);
				binaryWriter.Write(instance.Program);
			}
			if (instance.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in instance.Licenses)
				{
					stream.WriteByte(82);
					ProtocolParser.WriteUInt32(stream, accountLicense.GetSerializedSize());
					AccountLicense.Serialize(stream, accountLicense);
				}
			}
			if (instance.HasRealmPermissions)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt32(stream, instance.RealmPermissions);
			}
		}

		// Token: 0x06005F9A RID: 24474 RVA: 0x00121B1C File Offset: 0x0011FD1C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIsTrial)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsLifetime)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsRestricted)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsBeta)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in this.Licenses)
				{
					num += 1U;
					uint serializedSize = accountLicense.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasRealmPermissions)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.RealmPermissions);
			}
			return num;
		}

		// Token: 0x04001D6A RID: 7530
		public bool HasIsTrial;

		// Token: 0x04001D6B RID: 7531
		private bool _IsTrial;

		// Token: 0x04001D6C RID: 7532
		public bool HasIsLifetime;

		// Token: 0x04001D6D RID: 7533
		private bool _IsLifetime;

		// Token: 0x04001D6E RID: 7534
		public bool HasIsRestricted;

		// Token: 0x04001D6F RID: 7535
		private bool _IsRestricted;

		// Token: 0x04001D70 RID: 7536
		public bool HasIsBeta;

		// Token: 0x04001D71 RID: 7537
		private bool _IsBeta;

		// Token: 0x04001D72 RID: 7538
		public bool HasName;

		// Token: 0x04001D73 RID: 7539
		private string _Name;

		// Token: 0x04001D74 RID: 7540
		public bool HasProgram;

		// Token: 0x04001D75 RID: 7541
		private uint _Program;

		// Token: 0x04001D76 RID: 7542
		private List<AccountLicense> _Licenses = new List<AccountLicense>();

		// Token: 0x04001D77 RID: 7543
		public bool HasRealmPermissions;

		// Token: 0x04001D78 RID: 7544
		private uint _RealmPermissions;
	}
}
