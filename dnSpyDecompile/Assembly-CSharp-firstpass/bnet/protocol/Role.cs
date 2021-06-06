using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	// Token: 0x020002AC RID: 684
	public class Role : IProtoBuf
	{
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x0008BD6C File Offset: 0x00089F6C
		// (set) Token: 0x0600277D RID: 10109 RVA: 0x0008BD74 File Offset: 0x00089F74
		public uint Id { get; set; }

		// Token: 0x0600277E RID: 10110 RVA: 0x0008BD7D File Offset: 0x00089F7D
		public void SetId(uint val)
		{
			this.Id = val;
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x0008BD86 File Offset: 0x00089F86
		// (set) Token: 0x06002780 RID: 10112 RVA: 0x0008BD8E File Offset: 0x00089F8E
		public string Name { get; set; }

		// Token: 0x06002781 RID: 10113 RVA: 0x0008BD97 File Offset: 0x00089F97
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x0008BDA0 File Offset: 0x00089FA0
		// (set) Token: 0x06002783 RID: 10115 RVA: 0x0008BDA8 File Offset: 0x00089FA8
		public List<string> Privilege
		{
			get
			{
				return this._Privilege;
			}
			set
			{
				this._Privilege = value;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06002784 RID: 10116 RVA: 0x0008BDA0 File Offset: 0x00089FA0
		public List<string> PrivilegeList
		{
			get
			{
				return this._Privilege;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002785 RID: 10117 RVA: 0x0008BDB1 File Offset: 0x00089FB1
		public int PrivilegeCount
		{
			get
			{
				return this._Privilege.Count;
			}
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0008BDBE File Offset: 0x00089FBE
		public void AddPrivilege(string val)
		{
			this._Privilege.Add(val);
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x0008BDCC File Offset: 0x00089FCC
		public void ClearPrivilege()
		{
			this._Privilege.Clear();
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0008BDD9 File Offset: 0x00089FD9
		public void SetPrivilege(List<string> val)
		{
			this.Privilege = val;
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x0008BDE2 File Offset: 0x00089FE2
		// (set) Token: 0x0600278A RID: 10122 RVA: 0x0008BDEA File Offset: 0x00089FEA
		public List<uint> AssignableRole
		{
			get
			{
				return this._AssignableRole;
			}
			set
			{
				this._AssignableRole = value;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x0008BDE2 File Offset: 0x00089FE2
		public List<uint> AssignableRoleList
		{
			get
			{
				return this._AssignableRole;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x0008BDF3 File Offset: 0x00089FF3
		public int AssignableRoleCount
		{
			get
			{
				return this._AssignableRole.Count;
			}
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x0008BE00 File Offset: 0x0008A000
		public void AddAssignableRole(uint val)
		{
			this._AssignableRole.Add(val);
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x0008BE0E File Offset: 0x0008A00E
		public void ClearAssignableRole()
		{
			this._AssignableRole.Clear();
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x0008BE1B File Offset: 0x0008A01B
		public void SetAssignableRole(List<uint> val)
		{
			this.AssignableRole = val;
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002790 RID: 10128 RVA: 0x0008BE24 File Offset: 0x0008A024
		// (set) Token: 0x06002791 RID: 10129 RVA: 0x0008BE2C File Offset: 0x0008A02C
		public bool Required
		{
			get
			{
				return this._Required;
			}
			set
			{
				this._Required = value;
				this.HasRequired = true;
			}
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x0008BE3C File Offset: 0x0008A03C
		public void SetRequired(bool val)
		{
			this.Required = val;
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x0008BE45 File Offset: 0x0008A045
		// (set) Token: 0x06002794 RID: 10132 RVA: 0x0008BE4D File Offset: 0x0008A04D
		public bool Unique
		{
			get
			{
				return this._Unique;
			}
			set
			{
				this._Unique = value;
				this.HasUnique = true;
			}
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x0008BE5D File Offset: 0x0008A05D
		public void SetUnique(bool val)
		{
			this.Unique = val;
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002796 RID: 10134 RVA: 0x0008BE66 File Offset: 0x0008A066
		// (set) Token: 0x06002797 RID: 10135 RVA: 0x0008BE6E File Offset: 0x0008A06E
		public uint RelegationRole
		{
			get
			{
				return this._RelegationRole;
			}
			set
			{
				this._RelegationRole = value;
				this.HasRelegationRole = true;
			}
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x0008BE7E File Offset: 0x0008A07E
		public void SetRelegationRole(uint val)
		{
			this.RelegationRole = val;
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002799 RID: 10137 RVA: 0x0008BE87 File Offset: 0x0008A087
		// (set) Token: 0x0600279A RID: 10138 RVA: 0x0008BE8F File Offset: 0x0008A08F
		public List<uint> KickableRole
		{
			get
			{
				return this._KickableRole;
			}
			set
			{
				this._KickableRole = value;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x0008BE87 File Offset: 0x0008A087
		public List<uint> KickableRoleList
		{
			get
			{
				return this._KickableRole;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x0008BE98 File Offset: 0x0008A098
		public int KickableRoleCount
		{
			get
			{
				return this._KickableRole.Count;
			}
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x0008BEA5 File Offset: 0x0008A0A5
		public void AddKickableRole(uint val)
		{
			this._KickableRole.Add(val);
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x0008BEB3 File Offset: 0x0008A0B3
		public void ClearKickableRole()
		{
			this._KickableRole.Clear();
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x0008BEC0 File Offset: 0x0008A0C0
		public void SetKickableRole(List<uint> val)
		{
			this.KickableRole = val;
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x0008BEC9 File Offset: 0x0008A0C9
		// (set) Token: 0x060027A1 RID: 10145 RVA: 0x0008BED1 File Offset: 0x0008A0D1
		public List<uint> RemovableRole
		{
			get
			{
				return this._RemovableRole;
			}
			set
			{
				this._RemovableRole = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060027A2 RID: 10146 RVA: 0x0008BEC9 File Offset: 0x0008A0C9
		public List<uint> RemovableRoleList
		{
			get
			{
				return this._RemovableRole;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060027A3 RID: 10147 RVA: 0x0008BEDA File Offset: 0x0008A0DA
		public int RemovableRoleCount
		{
			get
			{
				return this._RemovableRole.Count;
			}
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x0008BEE7 File Offset: 0x0008A0E7
		public void AddRemovableRole(uint val)
		{
			this._RemovableRole.Add(val);
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x0008BEF5 File Offset: 0x0008A0F5
		public void ClearRemovableRole()
		{
			this._RemovableRole.Clear();
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x0008BF02 File Offset: 0x0008A102
		public void SetRemovableRole(List<uint> val)
		{
			this.RemovableRole = val;
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x0008BF0C File Offset: 0x0008A10C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			num ^= this.Name.GetHashCode();
			foreach (string text in this.Privilege)
			{
				num ^= text.GetHashCode();
			}
			foreach (uint num2 in this.AssignableRole)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasRequired)
			{
				num ^= this.Required.GetHashCode();
			}
			if (this.HasUnique)
			{
				num ^= this.Unique.GetHashCode();
			}
			if (this.HasRelegationRole)
			{
				num ^= this.RelegationRole.GetHashCode();
			}
			foreach (uint num3 in this.KickableRole)
			{
				num ^= num3.GetHashCode();
			}
			foreach (uint num4 in this.RemovableRole)
			{
				num ^= num4.GetHashCode();
			}
			return num;
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x0008C0B0 File Offset: 0x0008A2B0
		public override bool Equals(object obj)
		{
			Role role = obj as Role;
			if (role == null)
			{
				return false;
			}
			if (!this.Id.Equals(role.Id))
			{
				return false;
			}
			if (!this.Name.Equals(role.Name))
			{
				return false;
			}
			if (this.Privilege.Count != role.Privilege.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Privilege.Count; i++)
			{
				if (!this.Privilege[i].Equals(role.Privilege[i]))
				{
					return false;
				}
			}
			if (this.AssignableRole.Count != role.AssignableRole.Count)
			{
				return false;
			}
			for (int j = 0; j < this.AssignableRole.Count; j++)
			{
				if (!this.AssignableRole[j].Equals(role.AssignableRole[j]))
				{
					return false;
				}
			}
			if (this.HasRequired != role.HasRequired || (this.HasRequired && !this.Required.Equals(role.Required)))
			{
				return false;
			}
			if (this.HasUnique != role.HasUnique || (this.HasUnique && !this.Unique.Equals(role.Unique)))
			{
				return false;
			}
			if (this.HasRelegationRole != role.HasRelegationRole || (this.HasRelegationRole && !this.RelegationRole.Equals(role.RelegationRole)))
			{
				return false;
			}
			if (this.KickableRole.Count != role.KickableRole.Count)
			{
				return false;
			}
			for (int k = 0; k < this.KickableRole.Count; k++)
			{
				if (!this.KickableRole[k].Equals(role.KickableRole[k]))
				{
					return false;
				}
			}
			if (this.RemovableRole.Count != role.RemovableRole.Count)
			{
				return false;
			}
			for (int l = 0; l < this.RemovableRole.Count; l++)
			{
				if (!this.RemovableRole[l].Equals(role.RemovableRole[l]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060027A9 RID: 10153 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x0008C2DC File Offset: 0x0008A4DC
		public static Role ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Role>(bs, 0, -1);
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x0008C2E6 File Offset: 0x0008A4E6
		public void Deserialize(Stream stream)
		{
			Role.Deserialize(stream, this);
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x0008C2F0 File Offset: 0x0008A4F0
		public static Role Deserialize(Stream stream, Role instance)
		{
			return Role.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x0008C2FC File Offset: 0x0008A4FC
		public static Role DeserializeLengthDelimited(Stream stream)
		{
			Role role = new Role();
			Role.DeserializeLengthDelimited(stream, role);
			return role;
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x0008C318 File Offset: 0x0008A518
		public static Role DeserializeLengthDelimited(Stream stream, Role instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Role.Deserialize(stream, instance, num);
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x0008C340 File Offset: 0x0008A540
		public static Role Deserialize(Stream stream, Role instance, long limit)
		{
			if (instance.Privilege == null)
			{
				instance.Privilege = new List<string>();
			}
			if (instance.AssignableRole == null)
			{
				instance.AssignableRole = new List<uint>();
			}
			if (instance.KickableRole == null)
			{
				instance.KickableRole = new List<uint>();
			}
			if (instance.RemovableRole == null)
			{
				instance.RemovableRole = new List<uint>();
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
					if (num <= 34)
					{
						if (num <= 18)
						{
							if (num == 8)
							{
								instance.Id = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 18)
							{
								instance.Name = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (num == 26)
							{
								instance.Privilege.Add(ProtocolParser.ReadString(stream));
								continue;
							}
							if (num == 34)
							{
								long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
								num2 += stream.Position;
								while (stream.Position < num2)
								{
									instance.AssignableRole.Add(ProtocolParser.ReadUInt32(stream));
								}
								if (stream.Position != num2)
								{
									throw new ProtocolBufferException("Read too many bytes in packed data");
								}
								continue;
							}
						}
					}
					else if (num <= 48)
					{
						if (num == 40)
						{
							instance.Required = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 48)
						{
							instance.Unique = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.RelegationRole = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num != 74)
						{
							if (num == 82)
							{
								long num3 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
								num3 += stream.Position;
								while (stream.Position < num3)
								{
									instance.RemovableRole.Add(ProtocolParser.ReadUInt32(stream));
								}
								if (stream.Position != num3)
								{
									throw new ProtocolBufferException("Read too many bytes in packed data");
								}
								continue;
							}
						}
						else
						{
							long num4 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
							num4 += stream.Position;
							while (stream.Position < num4)
							{
								instance.KickableRole.Add(ProtocolParser.ReadUInt32(stream));
							}
							if (stream.Position != num4)
							{
								throw new ProtocolBufferException("Read too many bytes in packed data");
							}
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

		// Token: 0x060027B0 RID: 10160 RVA: 0x0008C59E File Offset: 0x0008A79E
		public void Serialize(Stream stream)
		{
			Role.Serialize(stream, this);
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x0008C5A8 File Offset: 0x0008A7A8
		public static void Serialize(Stream stream, Role instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Id);
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.Privilege.Count > 0)
			{
				foreach (string s in instance.Privilege)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
			if (instance.AssignableRole.Count > 0)
			{
				stream.WriteByte(34);
				uint num = 0U;
				foreach (uint val in instance.AssignableRole)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.AssignableRole)
				{
					ProtocolParser.WriteUInt32(stream, val2);
				}
			}
			if (instance.HasRequired)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.Required);
			}
			if (instance.HasUnique)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.Unique);
			}
			if (instance.HasRelegationRole)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt32(stream, instance.RelegationRole);
			}
			if (instance.KickableRole.Count > 0)
			{
				stream.WriteByte(74);
				uint num2 = 0U;
				foreach (uint val3 in instance.KickableRole)
				{
					num2 += ProtocolParser.SizeOfUInt32(val3);
				}
				ProtocolParser.WriteUInt32(stream, num2);
				foreach (uint val4 in instance.KickableRole)
				{
					ProtocolParser.WriteUInt32(stream, val4);
				}
			}
			if (instance.RemovableRole.Count > 0)
			{
				stream.WriteByte(82);
				uint num3 = 0U;
				foreach (uint val5 in instance.RemovableRole)
				{
					num3 += ProtocolParser.SizeOfUInt32(val5);
				}
				ProtocolParser.WriteUInt32(stream, num3);
				foreach (uint val6 in instance.RemovableRole)
				{
					ProtocolParser.WriteUInt32(stream, val6);
				}
			}
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x0008C8C4 File Offset: 0x0008AAC4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt32(this.Id);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.Privilege.Count > 0)
			{
				foreach (string s in this.Privilege)
				{
					num += 1U;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			if (this.AssignableRole.Count > 0)
			{
				num += 1U;
				uint num2 = num;
				foreach (uint val in this.AssignableRole)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (this.HasRequired)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasUnique)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasRelegationRole)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.RelegationRole);
			}
			if (this.KickableRole.Count > 0)
			{
				num += 1U;
				uint num3 = num;
				foreach (uint val2 in this.KickableRole)
				{
					num += ProtocolParser.SizeOfUInt32(val2);
				}
				num += ProtocolParser.SizeOfUInt32(num - num3);
			}
			if (this.RemovableRole.Count > 0)
			{
				num += 1U;
				uint num4 = num;
				foreach (uint val3 in this.RemovableRole)
				{
					num += ProtocolParser.SizeOfUInt32(val3);
				}
				num += ProtocolParser.SizeOfUInt32(num - num4);
			}
			num += 2U;
			return num;
		}

		// Token: 0x0400113B RID: 4411
		private List<string> _Privilege = new List<string>();

		// Token: 0x0400113C RID: 4412
		private List<uint> _AssignableRole = new List<uint>();

		// Token: 0x0400113D RID: 4413
		public bool HasRequired;

		// Token: 0x0400113E RID: 4414
		private bool _Required;

		// Token: 0x0400113F RID: 4415
		public bool HasUnique;

		// Token: 0x04001140 RID: 4416
		private bool _Unique;

		// Token: 0x04001141 RID: 4417
		public bool HasRelegationRole;

		// Token: 0x04001142 RID: 4418
		private uint _RelegationRole;

		// Token: 0x04001143 RID: 4419
		private List<uint> _KickableRole = new List<uint>();

		// Token: 0x04001144 RID: 4420
		private List<uint> _RemovableRole = new List<uint>();
	}
}
