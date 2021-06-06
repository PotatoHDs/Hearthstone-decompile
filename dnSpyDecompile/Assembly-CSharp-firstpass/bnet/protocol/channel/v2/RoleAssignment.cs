using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000472 RID: 1138
	public class RoleAssignment : IProtoBuf
	{
		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06004E4F RID: 20047 RVA: 0x000F3090 File Offset: 0x000F1290
		// (set) Token: 0x06004E50 RID: 20048 RVA: 0x000F3098 File Offset: 0x000F1298
		public GameAccountHandle MemberId
		{
			get
			{
				return this._MemberId;
			}
			set
			{
				this._MemberId = value;
				this.HasMemberId = (value != null);
			}
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x000F30AB File Offset: 0x000F12AB
		public void SetMemberId(GameAccountHandle val)
		{
			this.MemberId = val;
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06004E52 RID: 20050 RVA: 0x000F30B4 File Offset: 0x000F12B4
		// (set) Token: 0x06004E53 RID: 20051 RVA: 0x000F30BC File Offset: 0x000F12BC
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

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06004E54 RID: 20052 RVA: 0x000F30B4 File Offset: 0x000F12B4
		public List<uint> RoleList
		{
			get
			{
				return this._Role;
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06004E55 RID: 20053 RVA: 0x000F30C5 File Offset: 0x000F12C5
		public int RoleCount
		{
			get
			{
				return this._Role.Count;
			}
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x000F30D2 File Offset: 0x000F12D2
		public void AddRole(uint val)
		{
			this._Role.Add(val);
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x000F30E0 File Offset: 0x000F12E0
		public void ClearRole()
		{
			this._Role.Clear();
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x000F30ED File Offset: 0x000F12ED
		public void SetRole(List<uint> val)
		{
			this.Role = val;
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x000F30F8 File Offset: 0x000F12F8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMemberId)
			{
				num ^= this.MemberId.GetHashCode();
			}
			foreach (uint num2 in this.Role)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004E5A RID: 20058 RVA: 0x000F3174 File Offset: 0x000F1374
		public override bool Equals(object obj)
		{
			RoleAssignment roleAssignment = obj as RoleAssignment;
			if (roleAssignment == null)
			{
				return false;
			}
			if (this.HasMemberId != roleAssignment.HasMemberId || (this.HasMemberId && !this.MemberId.Equals(roleAssignment.MemberId)))
			{
				return false;
			}
			if (this.Role.Count != roleAssignment.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Role.Count; i++)
			{
				if (!this.Role[i].Equals(roleAssignment.Role[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06004E5B RID: 20059 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x000F320D File Offset: 0x000F140D
		public static RoleAssignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RoleAssignment>(bs, 0, -1);
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x000F3217 File Offset: 0x000F1417
		public void Deserialize(Stream stream)
		{
			RoleAssignment.Deserialize(stream, this);
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x000F3221 File Offset: 0x000F1421
		public static RoleAssignment Deserialize(Stream stream, RoleAssignment instance)
		{
			return RoleAssignment.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x000F322C File Offset: 0x000F142C
		public static RoleAssignment DeserializeLengthDelimited(Stream stream)
		{
			RoleAssignment roleAssignment = new RoleAssignment();
			RoleAssignment.DeserializeLengthDelimited(stream, roleAssignment);
			return roleAssignment;
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x000F3248 File Offset: 0x000F1448
		public static RoleAssignment DeserializeLengthDelimited(Stream stream, RoleAssignment instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RoleAssignment.Deserialize(stream, instance, num);
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x000F3270 File Offset: 0x000F1470
		public static RoleAssignment Deserialize(Stream stream, RoleAssignment instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
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
				else if (instance.MemberId == null)
				{
					instance.MemberId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.MemberId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x000F336E File Offset: 0x000F156E
		public void Serialize(Stream stream)
		{
			RoleAssignment.Serialize(stream, this);
		}

		// Token: 0x06004E63 RID: 20067 RVA: 0x000F3378 File Offset: 0x000F1578
		public static void Serialize(Stream stream, RoleAssignment instance)
		{
			if (instance.HasMemberId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.MemberId);
			}
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
		}

		// Token: 0x06004E64 RID: 20068 RVA: 0x000F345C File Offset: 0x000F165C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMemberId)
			{
				num += 1U;
				uint serializedSize = this.MemberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
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
			return num;
		}

		// Token: 0x04001974 RID: 6516
		public bool HasMemberId;

		// Token: 0x04001975 RID: 6517
		private GameAccountHandle _MemberId;

		// Token: 0x04001976 RID: 6518
		private List<uint> _Role = new List<uint>();
	}
}
