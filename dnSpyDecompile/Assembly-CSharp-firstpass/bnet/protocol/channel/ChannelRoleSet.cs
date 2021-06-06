using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.channel
{
	// Token: 0x02000449 RID: 1097
	public class ChannelRoleSet : IProtoBuf
	{
		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06004AB1 RID: 19121 RVA: 0x000E8D14 File Offset: 0x000E6F14
		// (set) Token: 0x06004AB2 RID: 19122 RVA: 0x000E8D1C File Offset: 0x000E6F1C
		public List<ChannelRole> Role
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

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06004AB3 RID: 19123 RVA: 0x000E8D14 File Offset: 0x000E6F14
		public List<ChannelRole> RoleList
		{
			get
			{
				return this._Role;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06004AB4 RID: 19124 RVA: 0x000E8D25 File Offset: 0x000E6F25
		public int RoleCount
		{
			get
			{
				return this._Role.Count;
			}
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x000E8D32 File Offset: 0x000E6F32
		public void AddRole(ChannelRole val)
		{
			this._Role.Add(val);
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x000E8D40 File Offset: 0x000E6F40
		public void ClearRole()
		{
			this._Role.Clear();
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x000E8D4D File Offset: 0x000E6F4D
		public void SetRole(List<ChannelRole> val)
		{
			this.Role = val;
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06004AB8 RID: 19128 RVA: 0x000E8D56 File Offset: 0x000E6F56
		// (set) Token: 0x06004AB9 RID: 19129 RVA: 0x000E8D5E File Offset: 0x000E6F5E
		public List<uint> DefaultRole
		{
			get
			{
				return this._DefaultRole;
			}
			set
			{
				this._DefaultRole = value;
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06004ABA RID: 19130 RVA: 0x000E8D56 File Offset: 0x000E6F56
		public List<uint> DefaultRoleList
		{
			get
			{
				return this._DefaultRole;
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06004ABB RID: 19131 RVA: 0x000E8D67 File Offset: 0x000E6F67
		public int DefaultRoleCount
		{
			get
			{
				return this._DefaultRole.Count;
			}
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x000E8D74 File Offset: 0x000E6F74
		public void AddDefaultRole(uint val)
		{
			this._DefaultRole.Add(val);
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x000E8D82 File Offset: 0x000E6F82
		public void ClearDefaultRole()
		{
			this._DefaultRole.Clear();
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x000E8D8F File Offset: 0x000E6F8F
		public void SetDefaultRole(List<uint> val)
		{
			this.DefaultRole = val;
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06004ABF RID: 19135 RVA: 0x000E8D98 File Offset: 0x000E6F98
		// (set) Token: 0x06004AC0 RID: 19136 RVA: 0x000E8DA0 File Offset: 0x000E6FA0
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

		// Token: 0x06004AC1 RID: 19137 RVA: 0x000E8DB3 File Offset: 0x000E6FB3
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x000E8DBC File Offset: 0x000E6FBC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ChannelRole channelRole in this.Role)
			{
				num ^= channelRole.GetHashCode();
			}
			foreach (uint num2 in this.DefaultRole)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x000E8E7C File Offset: 0x000E707C
		public override bool Equals(object obj)
		{
			ChannelRoleSet channelRoleSet = obj as ChannelRoleSet;
			if (channelRoleSet == null)
			{
				return false;
			}
			if (this.Role.Count != channelRoleSet.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Role.Count; i++)
			{
				if (!this.Role[i].Equals(channelRoleSet.Role[i]))
				{
					return false;
				}
			}
			if (this.DefaultRole.Count != channelRoleSet.DefaultRole.Count)
			{
				return false;
			}
			for (int j = 0; j < this.DefaultRole.Count; j++)
			{
				if (!this.DefaultRole[j].Equals(channelRoleSet.DefaultRole[j]))
				{
					return false;
				}
			}
			return this.HasName == channelRoleSet.HasName && (!this.HasName || this.Name.Equals(channelRoleSet.Name));
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06004AC4 RID: 19140 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x000E8F66 File Offset: 0x000E7166
		public static ChannelRoleSet ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelRoleSet>(bs, 0, -1);
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x000E8F70 File Offset: 0x000E7170
		public void Deserialize(Stream stream)
		{
			ChannelRoleSet.Deserialize(stream, this);
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x000E8F7A File Offset: 0x000E717A
		public static ChannelRoleSet Deserialize(Stream stream, ChannelRoleSet instance)
		{
			return ChannelRoleSet.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x000E8F88 File Offset: 0x000E7188
		public static ChannelRoleSet DeserializeLengthDelimited(Stream stream)
		{
			ChannelRoleSet channelRoleSet = new ChannelRoleSet();
			ChannelRoleSet.DeserializeLengthDelimited(stream, channelRoleSet);
			return channelRoleSet;
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x000E8FA4 File Offset: 0x000E71A4
		public static ChannelRoleSet DeserializeLengthDelimited(Stream stream, ChannelRoleSet instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelRoleSet.Deserialize(stream, instance, num);
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x000E8FCC File Offset: 0x000E71CC
		public static ChannelRoleSet Deserialize(Stream stream, ChannelRoleSet instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<ChannelRole>();
			}
			if (instance.DefaultRole == null)
			{
				instance.DefaultRole = new List<uint>();
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.Name = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
						num2 += stream.Position;
						while (stream.Position < num2)
						{
							instance.DefaultRole.Add(ProtocolParser.ReadUInt32(stream));
						}
						if (stream.Position != num2)
						{
							throw new ProtocolBufferException("Read too many bytes in packed data");
						}
					}
				}
				else
				{
					instance.Role.Add(ChannelRole.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x000E90E1 File Offset: 0x000E72E1
		public void Serialize(Stream stream)
		{
			ChannelRoleSet.Serialize(stream, this);
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x000E90EC File Offset: 0x000E72EC
		public static void Serialize(Stream stream, ChannelRoleSet instance)
		{
			if (instance.Role.Count > 0)
			{
				foreach (ChannelRole channelRole in instance.Role)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, channelRole.GetSerializedSize());
					ChannelRole.Serialize(stream, channelRole);
				}
			}
			if (instance.DefaultRole.Count > 0)
			{
				stream.WriteByte(18);
				uint num = 0U;
				foreach (uint val in instance.DefaultRole)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.DefaultRole)
				{
					ProtocolParser.WriteUInt32(stream, val2);
				}
			}
			if (instance.HasName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x000E9234 File Offset: 0x000E7434
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Role.Count > 0)
			{
				foreach (ChannelRole channelRole in this.Role)
				{
					num += 1U;
					uint serializedSize = channelRole.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.DefaultRole.Count > 0)
			{
				num += 1U;
				uint num2 = num;
				foreach (uint val in this.DefaultRole)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04001884 RID: 6276
		private List<ChannelRole> _Role = new List<ChannelRole>();

		// Token: 0x04001885 RID: 6277
		private List<uint> _DefaultRole = new List<uint>();

		// Token: 0x04001886 RID: 6278
		public bool HasName;

		// Token: 0x04001887 RID: 6279
		private string _Name;
	}
}
