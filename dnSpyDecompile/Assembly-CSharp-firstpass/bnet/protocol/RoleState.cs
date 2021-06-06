using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	// Token: 0x020002AD RID: 685
	public class RoleState : IProtoBuf
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060027B4 RID: 10164 RVA: 0x0008CB14 File Offset: 0x0008AD14
		// (set) Token: 0x060027B5 RID: 10165 RVA: 0x0008CB1C File Offset: 0x0008AD1C
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

		// Token: 0x060027B6 RID: 10166 RVA: 0x0008CB2F File Offset: 0x0008AD2F
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x0008CB38 File Offset: 0x0008AD38
		// (set) Token: 0x060027B8 RID: 10168 RVA: 0x0008CB40 File Offset: 0x0008AD40
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

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x0008CB38 File Offset: 0x0008AD38
		public List<uint> AssignableRoleList
		{
			get
			{
				return this._AssignableRole;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060027BA RID: 10170 RVA: 0x0008CB49 File Offset: 0x0008AD49
		public int AssignableRoleCount
		{
			get
			{
				return this._AssignableRole.Count;
			}
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x0008CB56 File Offset: 0x0008AD56
		public void AddAssignableRole(uint val)
		{
			this._AssignableRole.Add(val);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x0008CB64 File Offset: 0x0008AD64
		public void ClearAssignableRole()
		{
			this._AssignableRole.Clear();
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x0008CB71 File Offset: 0x0008AD71
		public void SetAssignableRole(List<uint> val)
		{
			this.AssignableRole = val;
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060027BE RID: 10174 RVA: 0x0008CB7A File Offset: 0x0008AD7A
		// (set) Token: 0x060027BF RID: 10175 RVA: 0x0008CB82 File Offset: 0x0008AD82
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

		// Token: 0x060027C0 RID: 10176 RVA: 0x0008CB92 File Offset: 0x0008AD92
		public void SetRequired(bool val)
		{
			this.Required = val;
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x0008CB9B File Offset: 0x0008AD9B
		// (set) Token: 0x060027C2 RID: 10178 RVA: 0x0008CBA3 File Offset: 0x0008ADA3
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

		// Token: 0x060027C3 RID: 10179 RVA: 0x0008CBB3 File Offset: 0x0008ADB3
		public void SetUnique(bool val)
		{
			this.Unique = val;
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x0008CBBC File Offset: 0x0008ADBC
		// (set) Token: 0x060027C5 RID: 10181 RVA: 0x0008CBC4 File Offset: 0x0008ADC4
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

		// Token: 0x060027C6 RID: 10182 RVA: 0x0008CBD4 File Offset: 0x0008ADD4
		public void SetRelegationRole(uint val)
		{
			this.RelegationRole = val;
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x0008CBDD File Offset: 0x0008ADDD
		// (set) Token: 0x060027C8 RID: 10184 RVA: 0x0008CBE5 File Offset: 0x0008ADE5
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

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x0008CBDD File Offset: 0x0008ADDD
		public List<uint> KickableRoleList
		{
			get
			{
				return this._KickableRole;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060027CA RID: 10186 RVA: 0x0008CBEE File Offset: 0x0008ADEE
		public int KickableRoleCount
		{
			get
			{
				return this._KickableRole.Count;
			}
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x0008CBFB File Offset: 0x0008ADFB
		public void AddKickableRole(uint val)
		{
			this._KickableRole.Add(val);
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x0008CC09 File Offset: 0x0008AE09
		public void ClearKickableRole()
		{
			this._KickableRole.Clear();
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x0008CC16 File Offset: 0x0008AE16
		public void SetKickableRole(List<uint> val)
		{
			this.KickableRole = val;
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060027CE RID: 10190 RVA: 0x0008CC1F File Offset: 0x0008AE1F
		// (set) Token: 0x060027CF RID: 10191 RVA: 0x0008CC27 File Offset: 0x0008AE27
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

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x0008CC1F File Offset: 0x0008AE1F
		public List<uint> RemovableRoleList
		{
			get
			{
				return this._RemovableRole;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060027D1 RID: 10193 RVA: 0x0008CC30 File Offset: 0x0008AE30
		public int RemovableRoleCount
		{
			get
			{
				return this._RemovableRole.Count;
			}
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x0008CC3D File Offset: 0x0008AE3D
		public void AddRemovableRole(uint val)
		{
			this._RemovableRole.Add(val);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x0008CC4B File Offset: 0x0008AE4B
		public void ClearRemovableRole()
		{
			this._RemovableRole.Clear();
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x0008CC58 File Offset: 0x0008AE58
		public void SetRemovableRole(List<uint> val)
		{
			this.RemovableRole = val;
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060027D5 RID: 10197 RVA: 0x0008CC61 File Offset: 0x0008AE61
		// (set) Token: 0x060027D6 RID: 10198 RVA: 0x0008CC69 File Offset: 0x0008AE69
		public List<uint> MentionableRole
		{
			get
			{
				return this._MentionableRole;
			}
			set
			{
				this._MentionableRole = value;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060027D7 RID: 10199 RVA: 0x0008CC61 File Offset: 0x0008AE61
		public List<uint> MentionableRoleList
		{
			get
			{
				return this._MentionableRole;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x0008CC72 File Offset: 0x0008AE72
		public int MentionableRoleCount
		{
			get
			{
				return this._MentionableRole.Count;
			}
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x0008CC7F File Offset: 0x0008AE7F
		public void AddMentionableRole(uint val)
		{
			this._MentionableRole.Add(val);
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x0008CC8D File Offset: 0x0008AE8D
		public void ClearMentionableRole()
		{
			this._MentionableRole.Clear();
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x0008CC9A File Offset: 0x0008AE9A
		public void SetMentionableRole(List<uint> val)
		{
			this.MentionableRole = val;
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x0008CCA4 File Offset: 0x0008AEA4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
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
			foreach (uint num5 in this.MentionableRole)
			{
				num ^= num5.GetHashCode();
			}
			return num;
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x0008CE3C File Offset: 0x0008B03C
		public override bool Equals(object obj)
		{
			RoleState roleState = obj as RoleState;
			if (roleState == null)
			{
				return false;
			}
			if (this.HasName != roleState.HasName || (this.HasName && !this.Name.Equals(roleState.Name)))
			{
				return false;
			}
			if (this.AssignableRole.Count != roleState.AssignableRole.Count)
			{
				return false;
			}
			for (int i = 0; i < this.AssignableRole.Count; i++)
			{
				if (!this.AssignableRole[i].Equals(roleState.AssignableRole[i]))
				{
					return false;
				}
			}
			if (this.HasRequired != roleState.HasRequired || (this.HasRequired && !this.Required.Equals(roleState.Required)))
			{
				return false;
			}
			if (this.HasUnique != roleState.HasUnique || (this.HasUnique && !this.Unique.Equals(roleState.Unique)))
			{
				return false;
			}
			if (this.HasRelegationRole != roleState.HasRelegationRole || (this.HasRelegationRole && !this.RelegationRole.Equals(roleState.RelegationRole)))
			{
				return false;
			}
			if (this.KickableRole.Count != roleState.KickableRole.Count)
			{
				return false;
			}
			for (int j = 0; j < this.KickableRole.Count; j++)
			{
				if (!this.KickableRole[j].Equals(roleState.KickableRole[j]))
				{
					return false;
				}
			}
			if (this.RemovableRole.Count != roleState.RemovableRole.Count)
			{
				return false;
			}
			for (int k = 0; k < this.RemovableRole.Count; k++)
			{
				if (!this.RemovableRole[k].Equals(roleState.RemovableRole[k]))
				{
					return false;
				}
			}
			if (this.MentionableRole.Count != roleState.MentionableRole.Count)
			{
				return false;
			}
			for (int l = 0; l < this.MentionableRole.Count; l++)
			{
				if (!this.MentionableRole[l].Equals(roleState.MentionableRole[l]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x0008D06D File Offset: 0x0008B26D
		public static RoleState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RoleState>(bs, 0, -1);
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x0008D077 File Offset: 0x0008B277
		public void Deserialize(Stream stream)
		{
			RoleState.Deserialize(stream, this);
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x0008D081 File Offset: 0x0008B281
		public static RoleState Deserialize(Stream stream, RoleState instance)
		{
			return RoleState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x0008D08C File Offset: 0x0008B28C
		public static RoleState DeserializeLengthDelimited(Stream stream)
		{
			RoleState roleState = new RoleState();
			RoleState.DeserializeLengthDelimited(stream, roleState);
			return roleState;
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x0008D0A8 File Offset: 0x0008B2A8
		public static RoleState DeserializeLengthDelimited(Stream stream, RoleState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RoleState.Deserialize(stream, instance, num);
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x0008D0D0 File Offset: 0x0008B2D0
		public static RoleState Deserialize(Stream stream, RoleState instance, long limit)
		{
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
			if (instance.MentionableRole == null)
			{
				instance.MentionableRole = new List<uint>();
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
					if (num <= 48)
					{
						if (num <= 34)
						{
							if (num == 18)
							{
								instance.Name = ProtocolParser.ReadString(stream);
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
						else
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
					}
					else if (num <= 74)
					{
						if (num == 56)
						{
							instance.RelegationRole = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 74)
						{
							long num3 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
							num3 += stream.Position;
							while (stream.Position < num3)
							{
								instance.KickableRole.Add(ProtocolParser.ReadUInt32(stream));
							}
							if (stream.Position != num3)
							{
								throw new ProtocolBufferException("Read too many bytes in packed data");
							}
							continue;
						}
					}
					else if (num != 82)
					{
						if (num == 90)
						{
							long num4 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
							num4 += stream.Position;
							while (stream.Position < num4)
							{
								instance.MentionableRole.Add(ProtocolParser.ReadUInt32(stream));
							}
							if (stream.Position != num4)
							{
								throw new ProtocolBufferException("Read too many bytes in packed data");
							}
							continue;
						}
					}
					else
					{
						long num5 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
						num5 += stream.Position;
						while (stream.Position < num5)
						{
							instance.RemovableRole.Add(ProtocolParser.ReadUInt32(stream));
						}
						if (stream.Position != num5)
						{
							throw new ProtocolBufferException("Read too many bytes in packed data");
						}
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

		// Token: 0x060027E5 RID: 10213 RVA: 0x0008D34F File Offset: 0x0008B54F
		public void Serialize(Stream stream)
		{
			RoleState.Serialize(stream, this);
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x0008D358 File Offset: 0x0008B558
		public static void Serialize(Stream stream, RoleState instance)
		{
			if (instance.HasName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
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
			if (instance.MentionableRole.Count > 0)
			{
				stream.WriteByte(90);
				uint num4 = 0U;
				foreach (uint val7 in instance.MentionableRole)
				{
					num4 += ProtocolParser.SizeOfUInt32(val7);
				}
				ProtocolParser.WriteUInt32(stream, num4);
				foreach (uint val8 in instance.MentionableRole)
				{
					ProtocolParser.WriteUInt32(stream, val8);
				}
			}
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x0008D698 File Offset: 0x0008B898
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
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
			if (this.MentionableRole.Count > 0)
			{
				num += 1U;
				uint num5 = num;
				foreach (uint val4 in this.MentionableRole)
				{
					num += ProtocolParser.SizeOfUInt32(val4);
				}
				num += ProtocolParser.SizeOfUInt32(num - num5);
			}
			return num;
		}

		// Token: 0x04001145 RID: 4421
		public bool HasName;

		// Token: 0x04001146 RID: 4422
		private string _Name;

		// Token: 0x04001147 RID: 4423
		private List<uint> _AssignableRole = new List<uint>();

		// Token: 0x04001148 RID: 4424
		public bool HasRequired;

		// Token: 0x04001149 RID: 4425
		private bool _Required;

		// Token: 0x0400114A RID: 4426
		public bool HasUnique;

		// Token: 0x0400114B RID: 4427
		private bool _Unique;

		// Token: 0x0400114C RID: 4428
		public bool HasRelegationRole;

		// Token: 0x0400114D RID: 4429
		private uint _RelegationRole;

		// Token: 0x0400114E RID: 4430
		private List<uint> _KickableRole = new List<uint>();

		// Token: 0x0400114F RID: 4431
		private List<uint> _RemovableRole = new List<uint>();

		// Token: 0x04001150 RID: 4432
		private List<uint> _MentionableRole = new List<uint>();
	}
}
