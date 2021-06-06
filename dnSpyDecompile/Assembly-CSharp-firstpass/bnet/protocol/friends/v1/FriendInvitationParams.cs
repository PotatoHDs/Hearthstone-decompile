using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000433 RID: 1075
	public class FriendInvitationParams : IProtoBuf
	{
		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x0600487B RID: 18555 RVA: 0x000E2B37 File Offset: 0x000E0D37
		// (set) Token: 0x0600487C RID: 18556 RVA: 0x000E2B3F File Offset: 0x000E0D3F
		public string TargetEmail
		{
			get
			{
				return this._TargetEmail;
			}
			set
			{
				this._TargetEmail = value;
				this.HasTargetEmail = (value != null);
			}
		}

		// Token: 0x0600487D RID: 18557 RVA: 0x000E2B52 File Offset: 0x000E0D52
		public void SetTargetEmail(string val)
		{
			this.TargetEmail = val;
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x0600487E RID: 18558 RVA: 0x000E2B5B File Offset: 0x000E0D5B
		// (set) Token: 0x0600487F RID: 18559 RVA: 0x000E2B63 File Offset: 0x000E0D63
		public string TargetBattleTag
		{
			get
			{
				return this._TargetBattleTag;
			}
			set
			{
				this._TargetBattleTag = value;
				this.HasTargetBattleTag = (value != null);
			}
		}

		// Token: 0x06004880 RID: 18560 RVA: 0x000E2B76 File Offset: 0x000E0D76
		public void SetTargetBattleTag(string val)
		{
			this.TargetBattleTag = val;
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06004881 RID: 18561 RVA: 0x000E2B7F File Offset: 0x000E0D7F
		// (set) Token: 0x06004882 RID: 18562 RVA: 0x000E2B87 File Offset: 0x000E0D87
		public List<uint> Role
		{
			get
			{
				return this._Role;
			}
			set
			{
				this._Role = value;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06004883 RID: 18563 RVA: 0x000E2B7F File Offset: 0x000E0D7F
		public List<uint> RoleList
		{
			get
			{
				return this._Role;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06004884 RID: 18564 RVA: 0x000E2B90 File Offset: 0x000E0D90
		public int RoleCount
		{
			get
			{
				return this._Role.Count;
			}
		}

		// Token: 0x06004885 RID: 18565 RVA: 0x000E2B9D File Offset: 0x000E0D9D
		public void AddRole(uint val)
		{
			this._Role.Add(val);
		}

		// Token: 0x06004886 RID: 18566 RVA: 0x000E2BAB File Offset: 0x000E0DAB
		public void ClearRole()
		{
			this._Role.Clear();
		}

		// Token: 0x06004887 RID: 18567 RVA: 0x000E2BB8 File Offset: 0x000E0DB8
		public void SetRole(List<uint> val)
		{
			this.Role = val;
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06004888 RID: 18568 RVA: 0x000E2BC1 File Offset: 0x000E0DC1
		// (set) Token: 0x06004889 RID: 18569 RVA: 0x000E2BC9 File Offset: 0x000E0DC9
		public List<Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x0600488A RID: 18570 RVA: 0x000E2BC1 File Offset: 0x000E0DC1
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x0600488B RID: 18571 RVA: 0x000E2BD2 File Offset: 0x000E0DD2
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x0600488C RID: 18572 RVA: 0x000E2BDF File Offset: 0x000E0DDF
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x0600488D RID: 18573 RVA: 0x000E2BED File Offset: 0x000E0DED
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x0600488E RID: 18574 RVA: 0x000E2BFA File Offset: 0x000E0DFA
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x0600488F RID: 18575 RVA: 0x000E2C03 File Offset: 0x000E0E03
		// (set) Token: 0x06004890 RID: 18576 RVA: 0x000E2C0B File Offset: 0x000E0E0B
		public string TargetName
		{
			get
			{
				return this._TargetName;
			}
			set
			{
				this._TargetName = value;
				this.HasTargetName = (value != null);
			}
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x000E2C1E File Offset: 0x000E0E1E
		public void SetTargetName(string val)
		{
			this.TargetName = val;
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06004892 RID: 18578 RVA: 0x000E2C27 File Offset: 0x000E0E27
		// (set) Token: 0x06004893 RID: 18579 RVA: 0x000E2C2F File Offset: 0x000E0E2F
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

		// Token: 0x06004894 RID: 18580 RVA: 0x000E2C3F File Offset: 0x000E0E3F
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x06004895 RID: 18581 RVA: 0x000E2C48 File Offset: 0x000E0E48
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTargetEmail)
			{
				num ^= this.TargetEmail.GetHashCode();
			}
			if (this.HasTargetBattleTag)
			{
				num ^= this.TargetBattleTag.GetHashCode();
			}
			foreach (uint num2 in this.Role)
			{
				num ^= num2.GetHashCode();
			}
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasTargetName)
			{
				num ^= this.TargetName.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004896 RID: 18582 RVA: 0x000E2D50 File Offset: 0x000E0F50
		public override bool Equals(object obj)
		{
			FriendInvitationParams friendInvitationParams = obj as FriendInvitationParams;
			if (friendInvitationParams == null)
			{
				return false;
			}
			if (this.HasTargetEmail != friendInvitationParams.HasTargetEmail || (this.HasTargetEmail && !this.TargetEmail.Equals(friendInvitationParams.TargetEmail)))
			{
				return false;
			}
			if (this.HasTargetBattleTag != friendInvitationParams.HasTargetBattleTag || (this.HasTargetBattleTag && !this.TargetBattleTag.Equals(friendInvitationParams.TargetBattleTag)))
			{
				return false;
			}
			if (this.Role.Count != friendInvitationParams.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Role.Count; i++)
			{
				if (!this.Role[i].Equals(friendInvitationParams.Role[i]))
				{
					return false;
				}
			}
			if (this.Attribute.Count != friendInvitationParams.Attribute.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Attribute.Count; j++)
			{
				if (!this.Attribute[j].Equals(friendInvitationParams.Attribute[j]))
				{
					return false;
				}
			}
			return this.HasTargetName == friendInvitationParams.HasTargetName && (!this.HasTargetName || this.TargetName.Equals(friendInvitationParams.TargetName)) && this.HasProgram == friendInvitationParams.HasProgram && (!this.HasProgram || this.Program.Equals(friendInvitationParams.Program));
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06004897 RID: 18583 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004898 RID: 18584 RVA: 0x000E2EBE File Offset: 0x000E10BE
		public static FriendInvitationParams ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendInvitationParams>(bs, 0, -1);
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x000E2EC8 File Offset: 0x000E10C8
		public void Deserialize(Stream stream)
		{
			FriendInvitationParams.Deserialize(stream, this);
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x000E2ED2 File Offset: 0x000E10D2
		public static FriendInvitationParams Deserialize(Stream stream, FriendInvitationParams instance)
		{
			return FriendInvitationParams.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x000E2EE0 File Offset: 0x000E10E0
		public static FriendInvitationParams DeserializeLengthDelimited(Stream stream)
		{
			FriendInvitationParams friendInvitationParams = new FriendInvitationParams();
			FriendInvitationParams.DeserializeLengthDelimited(stream, friendInvitationParams);
			return friendInvitationParams;
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x000E2EFC File Offset: 0x000E10FC
		public static FriendInvitationParams DeserializeLengthDelimited(Stream stream, FriendInvitationParams instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FriendInvitationParams.Deserialize(stream, instance, num);
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x000E2F24 File Offset: 0x000E1124
		public static FriendInvitationParams Deserialize(Stream stream, FriendInvitationParams instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
			}
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
					if (num <= 50)
					{
						if (num == 10)
						{
							instance.TargetEmail = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 18)
						{
							instance.TargetBattleTag = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 50)
						{
							long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
							num2 += stream.Position;
							while (stream.Position < num2)
							{
								instance.Role.Add(ProtocolParser.ReadUInt32(stream));
							}
							if (stream.Position != num2)
							{
								throw new ProtocolBufferException("Read too many bytes in packed data");
							}
							continue;
						}
					}
					else
					{
						if (num == 66)
						{
							instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 74)
						{
							instance.TargetName = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 85)
						{
							instance.Program = binaryReader.ReadUInt32();
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

		// Token: 0x0600489E RID: 18590 RVA: 0x000E3097 File Offset: 0x000E1297
		public void Serialize(Stream stream)
		{
			FriendInvitationParams.Serialize(stream, this);
		}

		// Token: 0x0600489F RID: 18591 RVA: 0x000E30A0 File Offset: 0x000E12A0
		public static void Serialize(Stream stream, FriendInvitationParams instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasTargetEmail)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TargetEmail));
			}
			if (instance.HasTargetBattleTag)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TargetBattleTag));
			}
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(50);
				uint num = 0U;
				foreach (uint val in instance.Role)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.Role)
				{
					ProtocolParser.WriteUInt32(stream, val2);
				}
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(66);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasTargetName)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TargetName));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(85);
				binaryWriter.Write(instance.Program);
			}
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x000E3258 File Offset: 0x000E1458
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTargetEmail)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.TargetEmail);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasTargetBattleTag)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.TargetBattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.Role.Count > 0)
			{
				num += 1U;
				uint num2 = num;
				foreach (uint val in this.Role)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasTargetName)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.TargetName);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x0400180F RID: 6159
		public bool HasTargetEmail;

		// Token: 0x04001810 RID: 6160
		private string _TargetEmail;

		// Token: 0x04001811 RID: 6161
		public bool HasTargetBattleTag;

		// Token: 0x04001812 RID: 6162
		private string _TargetBattleTag;

		// Token: 0x04001813 RID: 6163
		private List<uint> _Role = new List<uint>();

		// Token: 0x04001814 RID: 6164
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x04001815 RID: 6165
		public bool HasTargetName;

		// Token: 0x04001816 RID: 6166
		private string _TargetName;

		// Token: 0x04001817 RID: 6167
		public bool HasProgram;

		// Token: 0x04001818 RID: 6168
		private uint _Program;
	}
}
