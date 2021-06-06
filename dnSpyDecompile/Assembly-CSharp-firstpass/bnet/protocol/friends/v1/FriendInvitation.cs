using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000431 RID: 1073
	public class FriendInvitation : IProtoBuf
	{
		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x0600483D RID: 18493 RVA: 0x000E1F02 File Offset: 0x000E0102
		// (set) Token: 0x0600483E RID: 18494 RVA: 0x000E1F0A File Offset: 0x000E010A
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

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x0600483F RID: 18495 RVA: 0x000E1F02 File Offset: 0x000E0102
		public List<uint> RoleList
		{
			get
			{
				return this._Role;
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06004840 RID: 18496 RVA: 0x000E1F13 File Offset: 0x000E0113
		public int RoleCount
		{
			get
			{
				return this._Role.Count;
			}
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x000E1F20 File Offset: 0x000E0120
		public void AddRole(uint val)
		{
			this._Role.Add(val);
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x000E1F2E File Offset: 0x000E012E
		public void ClearRole()
		{
			this._Role.Clear();
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x000E1F3B File Offset: 0x000E013B
		public void SetRole(List<uint> val)
		{
			this.Role = val;
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06004844 RID: 18500 RVA: 0x000E1F44 File Offset: 0x000E0144
		// (set) Token: 0x06004845 RID: 18501 RVA: 0x000E1F4C File Offset: 0x000E014C
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

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06004846 RID: 18502 RVA: 0x000E1F44 File Offset: 0x000E0144
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06004847 RID: 18503 RVA: 0x000E1F55 File Offset: 0x000E0155
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x000E1F62 File Offset: 0x000E0162
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x000E1F70 File Offset: 0x000E0170
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x000E1F7D File Offset: 0x000E017D
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x000E1F88 File Offset: 0x000E0188
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (uint num2 in this.Role)
			{
				num ^= num2.GetHashCode();
			}
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x000E2034 File Offset: 0x000E0234
		public override bool Equals(object obj)
		{
			FriendInvitation friendInvitation = obj as FriendInvitation;
			if (friendInvitation == null)
			{
				return false;
			}
			if (this.Role.Count != friendInvitation.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Role.Count; i++)
			{
				if (!this.Role[i].Equals(friendInvitation.Role[i]))
				{
					return false;
				}
			}
			if (this.Attribute.Count != friendInvitation.Attribute.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Attribute.Count; j++)
			{
				if (!this.Attribute[j].Equals(friendInvitation.Attribute[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x0600484D RID: 18509 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x000E20F3 File Offset: 0x000E02F3
		public static FriendInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendInvitation>(bs, 0, -1);
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x000E20FD File Offset: 0x000E02FD
		public void Deserialize(Stream stream)
		{
			FriendInvitation.Deserialize(stream, this);
		}

		// Token: 0x06004850 RID: 18512 RVA: 0x000E2107 File Offset: 0x000E0307
		public static FriendInvitation Deserialize(Stream stream, FriendInvitation instance)
		{
			return FriendInvitation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x000E2114 File Offset: 0x000E0314
		public static FriendInvitation DeserializeLengthDelimited(Stream stream)
		{
			FriendInvitation friendInvitation = new FriendInvitation();
			FriendInvitation.DeserializeLengthDelimited(stream, friendInvitation);
			return friendInvitation;
		}

		// Token: 0x06004852 RID: 18514 RVA: 0x000E2130 File Offset: 0x000E0330
		public static FriendInvitation DeserializeLengthDelimited(Stream stream, FriendInvitation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FriendInvitation.Deserialize(stream, instance, num);
		}

		// Token: 0x06004853 RID: 18515 RVA: 0x000E2158 File Offset: 0x000E0358
		public static FriendInvitation Deserialize(Stream stream, FriendInvitation instance, long limit)
		{
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
				else if (num != 18)
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
						instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					}
				}
				else
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
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004854 RID: 18516 RVA: 0x000E2257 File Offset: 0x000E0457
		public void Serialize(Stream stream)
		{
			FriendInvitation.Serialize(stream, this);
		}

		// Token: 0x06004855 RID: 18517 RVA: 0x000E2260 File Offset: 0x000E0460
		public static void Serialize(Stream stream, FriendInvitation instance)
		{
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(18);
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
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x06004856 RID: 18518 RVA: 0x000E2380 File Offset: 0x000E0580
		public uint GetSerializedSize()
		{
			uint num = 0U;
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
			return num;
		}

		// Token: 0x04001802 RID: 6146
		private List<uint> _Role = new List<uint>();

		// Token: 0x04001803 RID: 6147
		private List<Attribute> _Attribute = new List<Attribute>();
	}
}
