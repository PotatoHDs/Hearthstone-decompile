using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.user_manager.v1
{
	// Token: 0x020002F8 RID: 760
	public class BlockedPlayer : IProtoBuf
	{
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x0009BC77 File Offset: 0x00099E77
		// (set) Token: 0x06002D82 RID: 11650 RVA: 0x0009BC7F File Offset: 0x00099E7F
		public EntityId AccountId { get; set; }

		// Token: 0x06002D83 RID: 11651 RVA: 0x0009BC88 File Offset: 0x00099E88
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x0009BC91 File Offset: 0x00099E91
		// (set) Token: 0x06002D85 RID: 11653 RVA: 0x0009BC99 File Offset: 0x00099E99
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

		// Token: 0x06002D86 RID: 11654 RVA: 0x0009BCAC File Offset: 0x00099EAC
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x0009BCB5 File Offset: 0x00099EB5
		// (set) Token: 0x06002D88 RID: 11656 RVA: 0x0009BCBD File Offset: 0x00099EBD
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

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x0009BCB5 File Offset: 0x00099EB5
		public List<uint> RoleList
		{
			get
			{
				return this._Role;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x0009BCC6 File Offset: 0x00099EC6
		public int RoleCount
		{
			get
			{
				return this._Role.Count;
			}
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x0009BCD3 File Offset: 0x00099ED3
		public void AddRole(uint val)
		{
			this._Role.Add(val);
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x0009BCE1 File Offset: 0x00099EE1
		public void ClearRole()
		{
			this._Role.Clear();
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x0009BCEE File Offset: 0x00099EEE
		public void SetRole(List<uint> val)
		{
			this.Role = val;
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x0009BCF7 File Offset: 0x00099EF7
		// (set) Token: 0x06002D8F RID: 11663 RVA: 0x0009BCFF File Offset: 0x00099EFF
		public ulong Privileges
		{
			get
			{
				return this._Privileges;
			}
			set
			{
				this._Privileges = value;
				this.HasPrivileges = true;
			}
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x0009BD0F File Offset: 0x00099F0F
		public void SetPrivileges(ulong val)
		{
			this.Privileges = val;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x0009BD18 File Offset: 0x00099F18
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.AccountId.GetHashCode();
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			foreach (uint num2 in this.Role)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasPrivileges)
			{
				num ^= this.Privileges.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x0009BDB8 File Offset: 0x00099FB8
		public override bool Equals(object obj)
		{
			BlockedPlayer blockedPlayer = obj as BlockedPlayer;
			if (blockedPlayer == null)
			{
				return false;
			}
			if (!this.AccountId.Equals(blockedPlayer.AccountId))
			{
				return false;
			}
			if (this.HasName != blockedPlayer.HasName || (this.HasName && !this.Name.Equals(blockedPlayer.Name)))
			{
				return false;
			}
			if (this.Role.Count != blockedPlayer.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Role.Count; i++)
			{
				if (!this.Role[i].Equals(blockedPlayer.Role[i]))
				{
					return false;
				}
			}
			return this.HasPrivileges == blockedPlayer.HasPrivileges && (!this.HasPrivileges || this.Privileges.Equals(blockedPlayer.Privileges));
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002D93 RID: 11667 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x0009BE94 File Offset: 0x0009A094
		public static BlockedPlayer ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BlockedPlayer>(bs, 0, -1);
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x0009BE9E File Offset: 0x0009A09E
		public void Deserialize(Stream stream)
		{
			BlockedPlayer.Deserialize(stream, this);
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x0009BEA8 File Offset: 0x0009A0A8
		public static BlockedPlayer Deserialize(Stream stream, BlockedPlayer instance)
		{
			return BlockedPlayer.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x0009BEB4 File Offset: 0x0009A0B4
		public static BlockedPlayer DeserializeLengthDelimited(Stream stream)
		{
			BlockedPlayer blockedPlayer = new BlockedPlayer();
			BlockedPlayer.DeserializeLengthDelimited(stream, blockedPlayer);
			return blockedPlayer;
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x0009BED0 File Offset: 0x0009A0D0
		public static BlockedPlayer DeserializeLengthDelimited(Stream stream, BlockedPlayer instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BlockedPlayer.Deserialize(stream, instance, num);
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x0009BEF8 File Offset: 0x0009A0F8
		public static BlockedPlayer Deserialize(Stream stream, BlockedPlayer instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
			}
			instance.Privileges = 0UL;
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
						if (num != 10)
						{
							if (num == 18)
							{
								instance.Name = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.AccountId == null)
							{
								instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 32)
						{
							instance.Privileges = ProtocolParser.ReadUInt64(stream);
							continue;
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

		// Token: 0x06002D9A RID: 11674 RVA: 0x0009C03D File Offset: 0x0009A23D
		public void Serialize(Stream stream)
		{
			BlockedPlayer.Serialize(stream, this);
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x0009C048 File Offset: 0x0009A248
		public static void Serialize(Stream stream, BlockedPlayer instance)
		{
			if (instance.AccountId == null)
			{
				throw new ArgumentNullException("AccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
			EntityId.Serialize(stream, instance.AccountId);
			if (instance.HasName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(26);
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
			if (instance.HasPrivileges)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.Privileges);
			}
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x0009C180 File Offset: 0x0009A380
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.AccountId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
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
			if (this.HasPrivileges)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Privileges);
			}
			num += 1U;
			return num;
		}

		// Token: 0x04001294 RID: 4756
		public bool HasName;

		// Token: 0x04001295 RID: 4757
		private string _Name;

		// Token: 0x04001296 RID: 4758
		private List<uint> _Role = new List<uint>();

		// Token: 0x04001297 RID: 4759
		public bool HasPrivileges;

		// Token: 0x04001298 RID: 4760
		private ulong _Privileges;
	}
}
