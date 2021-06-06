using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1.Types;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000541 RID: 1345
	public class AccountRestriction : IProtoBuf
	{
		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06006119 RID: 24857 RVA: 0x00125C56 File Offset: 0x00123E56
		// (set) Token: 0x0600611A RID: 24858 RVA: 0x00125C5E File Offset: 0x00123E5E
		public uint RestrictionId
		{
			get
			{
				return this._RestrictionId;
			}
			set
			{
				this._RestrictionId = value;
				this.HasRestrictionId = true;
			}
		}

		// Token: 0x0600611B RID: 24859 RVA: 0x00125C6E File Offset: 0x00123E6E
		public void SetRestrictionId(uint val)
		{
			this.RestrictionId = val;
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x0600611C RID: 24860 RVA: 0x00125C77 File Offset: 0x00123E77
		// (set) Token: 0x0600611D RID: 24861 RVA: 0x00125C7F File Offset: 0x00123E7F
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

		// Token: 0x0600611E RID: 24862 RVA: 0x00125C8F File Offset: 0x00123E8F
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x0600611F RID: 24863 RVA: 0x00125C98 File Offset: 0x00123E98
		// (set) Token: 0x06006120 RID: 24864 RVA: 0x00125CA0 File Offset: 0x00123EA0
		public RestrictionType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = true;
			}
		}

		// Token: 0x06006121 RID: 24865 RVA: 0x00125CB0 File Offset: 0x00123EB0
		public void SetType(RestrictionType val)
		{
			this.Type = val;
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06006122 RID: 24866 RVA: 0x00125CB9 File Offset: 0x00123EB9
		// (set) Token: 0x06006123 RID: 24867 RVA: 0x00125CC1 File Offset: 0x00123EC1
		public List<uint> Platform
		{
			get
			{
				return this._Platform;
			}
			set
			{
				this._Platform = value;
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06006124 RID: 24868 RVA: 0x00125CB9 File Offset: 0x00123EB9
		public List<uint> PlatformList
		{
			get
			{
				return this._Platform;
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06006125 RID: 24869 RVA: 0x00125CCA File Offset: 0x00123ECA
		public int PlatformCount
		{
			get
			{
				return this._Platform.Count;
			}
		}

		// Token: 0x06006126 RID: 24870 RVA: 0x00125CD7 File Offset: 0x00123ED7
		public void AddPlatform(uint val)
		{
			this._Platform.Add(val);
		}

		// Token: 0x06006127 RID: 24871 RVA: 0x00125CE5 File Offset: 0x00123EE5
		public void ClearPlatform()
		{
			this._Platform.Clear();
		}

		// Token: 0x06006128 RID: 24872 RVA: 0x00125CF2 File Offset: 0x00123EF2
		public void SetPlatform(List<uint> val)
		{
			this.Platform = val;
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06006129 RID: 24873 RVA: 0x00125CFB File Offset: 0x00123EFB
		// (set) Token: 0x0600612A RID: 24874 RVA: 0x00125D03 File Offset: 0x00123F03
		public ulong ExpireTime
		{
			get
			{
				return this._ExpireTime;
			}
			set
			{
				this._ExpireTime = value;
				this.HasExpireTime = true;
			}
		}

		// Token: 0x0600612B RID: 24875 RVA: 0x00125D13 File Offset: 0x00123F13
		public void SetExpireTime(ulong val)
		{
			this.ExpireTime = val;
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x0600612C RID: 24876 RVA: 0x00125D1C File Offset: 0x00123F1C
		// (set) Token: 0x0600612D RID: 24877 RVA: 0x00125D24 File Offset: 0x00123F24
		public ulong CreatedTime
		{
			get
			{
				return this._CreatedTime;
			}
			set
			{
				this._CreatedTime = value;
				this.HasCreatedTime = true;
			}
		}

		// Token: 0x0600612E RID: 24878 RVA: 0x00125D34 File Offset: 0x00123F34
		public void SetCreatedTime(ulong val)
		{
			this.CreatedTime = val;
		}

		// Token: 0x0600612F RID: 24879 RVA: 0x00125D40 File Offset: 0x00123F40
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRestrictionId)
			{
				num ^= this.RestrictionId.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			foreach (uint num2 in this.Platform)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasExpireTime)
			{
				num ^= this.ExpireTime.GetHashCode();
			}
			if (this.HasCreatedTime)
			{
				num ^= this.CreatedTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x06006130 RID: 24880 RVA: 0x00125E2C File Offset: 0x0012402C
		public override bool Equals(object obj)
		{
			AccountRestriction accountRestriction = obj as AccountRestriction;
			if (accountRestriction == null)
			{
				return false;
			}
			if (this.HasRestrictionId != accountRestriction.HasRestrictionId || (this.HasRestrictionId && !this.RestrictionId.Equals(accountRestriction.RestrictionId)))
			{
				return false;
			}
			if (this.HasProgram != accountRestriction.HasProgram || (this.HasProgram && !this.Program.Equals(accountRestriction.Program)))
			{
				return false;
			}
			if (this.HasType != accountRestriction.HasType || (this.HasType && !this.Type.Equals(accountRestriction.Type)))
			{
				return false;
			}
			if (this.Platform.Count != accountRestriction.Platform.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Platform.Count; i++)
			{
				if (!this.Platform[i].Equals(accountRestriction.Platform[i]))
				{
					return false;
				}
			}
			return this.HasExpireTime == accountRestriction.HasExpireTime && (!this.HasExpireTime || this.ExpireTime.Equals(accountRestriction.ExpireTime)) && this.HasCreatedTime == accountRestriction.HasCreatedTime && (!this.HasCreatedTime || this.CreatedTime.Equals(accountRestriction.CreatedTime));
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06006131 RID: 24881 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006132 RID: 24882 RVA: 0x00125F8D File Offset: 0x0012418D
		public static AccountRestriction ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountRestriction>(bs, 0, -1);
		}

		// Token: 0x06006133 RID: 24883 RVA: 0x00125F97 File Offset: 0x00124197
		public void Deserialize(Stream stream)
		{
			AccountRestriction.Deserialize(stream, this);
		}

		// Token: 0x06006134 RID: 24884 RVA: 0x00125FA1 File Offset: 0x001241A1
		public static AccountRestriction Deserialize(Stream stream, AccountRestriction instance)
		{
			return AccountRestriction.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06006135 RID: 24885 RVA: 0x00125FAC File Offset: 0x001241AC
		public static AccountRestriction DeserializeLengthDelimited(Stream stream)
		{
			AccountRestriction accountRestriction = new AccountRestriction();
			AccountRestriction.DeserializeLengthDelimited(stream, accountRestriction);
			return accountRestriction;
		}

		// Token: 0x06006136 RID: 24886 RVA: 0x00125FC8 File Offset: 0x001241C8
		public static AccountRestriction DeserializeLengthDelimited(Stream stream, AccountRestriction instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountRestriction.Deserialize(stream, instance, num);
		}

		// Token: 0x06006137 RID: 24887 RVA: 0x00125FF0 File Offset: 0x001241F0
		public static AccountRestriction Deserialize(Stream stream, AccountRestriction instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Type = RestrictionType.UNKNOWN;
			if (instance.Platform == null)
			{
				instance.Platform = new List<uint>();
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
					if (num <= 24)
					{
						if (num == 8)
						{
							instance.RestrictionId = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 21)
						{
							instance.Program = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 24)
						{
							instance.Type = (RestrictionType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 37)
						{
							instance.Platform.Add(binaryReader.ReadUInt32());
							continue;
						}
						if (num == 40)
						{
							instance.ExpireTime = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.CreatedTime = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06006138 RID: 24888 RVA: 0x00126113 File Offset: 0x00124313
		public void Serialize(Stream stream)
		{
			AccountRestriction.Serialize(stream, this);
		}

		// Token: 0x06006139 RID: 24889 RVA: 0x0012611C File Offset: 0x0012431C
		public static void Serialize(Stream stream, AccountRestriction instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasRestrictionId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.RestrictionId);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasType)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Type));
			}
			if (instance.Platform.Count > 0)
			{
				foreach (uint value in instance.Platform)
				{
					stream.WriteByte(37);
					binaryWriter.Write(value);
				}
			}
			if (instance.HasExpireTime)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.ExpireTime);
			}
			if (instance.HasCreatedTime)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.CreatedTime);
			}
		}

		// Token: 0x0600613A RID: 24890 RVA: 0x00126218 File Offset: 0x00124418
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRestrictionId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.RestrictionId);
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Type));
			}
			if (this.Platform.Count > 0)
			{
				foreach (uint num2 in this.Platform)
				{
					num += 1U;
					num += 4U;
				}
			}
			if (this.HasExpireTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ExpireTime);
			}
			if (this.HasCreatedTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.CreatedTime);
			}
			return num;
		}

		// Token: 0x04001DDA RID: 7642
		public bool HasRestrictionId;

		// Token: 0x04001DDB RID: 7643
		private uint _RestrictionId;

		// Token: 0x04001DDC RID: 7644
		public bool HasProgram;

		// Token: 0x04001DDD RID: 7645
		private uint _Program;

		// Token: 0x04001DDE RID: 7646
		public bool HasType;

		// Token: 0x04001DDF RID: 7647
		private RestrictionType _Type;

		// Token: 0x04001DE0 RID: 7648
		private List<uint> _Platform = new List<uint>();

		// Token: 0x04001DE1 RID: 7649
		public bool HasExpireTime;

		// Token: 0x04001DE2 RID: 7650
		private ulong _ExpireTime;

		// Token: 0x04001DE3 RID: 7651
		public bool HasCreatedTime;

		// Token: 0x04001DE4 RID: 7652
		private ulong _CreatedTime;
	}
}
