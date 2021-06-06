using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000468 RID: 1128
	public class MemberRoleChangedNotification : IProtoBuf
	{
		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06004D53 RID: 19795 RVA: 0x000F00EF File Offset: 0x000EE2EF
		// (set) Token: 0x06004D54 RID: 19796 RVA: 0x000F00F7 File Offset: 0x000EE2F7
		public GameAccountHandle AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x000F010A File Offset: 0x000EE30A
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06004D56 RID: 19798 RVA: 0x000F0113 File Offset: 0x000EE313
		// (set) Token: 0x06004D57 RID: 19799 RVA: 0x000F011B File Offset: 0x000EE31B
		public GameAccountHandle SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = (value != null);
			}
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x000F012E File Offset: 0x000EE32E
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06004D59 RID: 19801 RVA: 0x000F0137 File Offset: 0x000EE337
		// (set) Token: 0x06004D5A RID: 19802 RVA: 0x000F013F File Offset: 0x000EE33F
		public ChannelId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x000F0152 File Offset: 0x000EE352
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06004D5C RID: 19804 RVA: 0x000F015B File Offset: 0x000EE35B
		// (set) Token: 0x06004D5D RID: 19805 RVA: 0x000F0163 File Offset: 0x000EE363
		public List<RoleAssignment> Assignment
		{
			get
			{
				return this._Assignment;
			}
			set
			{
				this._Assignment = value;
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06004D5E RID: 19806 RVA: 0x000F015B File Offset: 0x000EE35B
		public List<RoleAssignment> AssignmentList
		{
			get
			{
				return this._Assignment;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06004D5F RID: 19807 RVA: 0x000F016C File Offset: 0x000EE36C
		public int AssignmentCount
		{
			get
			{
				return this._Assignment.Count;
			}
		}

		// Token: 0x06004D60 RID: 19808 RVA: 0x000F0179 File Offset: 0x000EE379
		public void AddAssignment(RoleAssignment val)
		{
			this._Assignment.Add(val);
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x000F0187 File Offset: 0x000EE387
		public void ClearAssignment()
		{
			this._Assignment.Clear();
		}

		// Token: 0x06004D62 RID: 19810 RVA: 0x000F0194 File Offset: 0x000EE394
		public void SetAssignment(List<RoleAssignment> val)
		{
			this.Assignment = val;
		}

		// Token: 0x06004D63 RID: 19811 RVA: 0x000F01A0 File Offset: 0x000EE3A0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			foreach (RoleAssignment roleAssignment in this.Assignment)
			{
				num ^= roleAssignment.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x000F0244 File Offset: 0x000EE444
		public override bool Equals(object obj)
		{
			MemberRoleChangedNotification memberRoleChangedNotification = obj as MemberRoleChangedNotification;
			if (memberRoleChangedNotification == null)
			{
				return false;
			}
			if (this.HasAgentId != memberRoleChangedNotification.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(memberRoleChangedNotification.AgentId)))
			{
				return false;
			}
			if (this.HasSubscriberId != memberRoleChangedNotification.HasSubscriberId || (this.HasSubscriberId && !this.SubscriberId.Equals(memberRoleChangedNotification.SubscriberId)))
			{
				return false;
			}
			if (this.HasChannelId != memberRoleChangedNotification.HasChannelId || (this.HasChannelId && !this.ChannelId.Equals(memberRoleChangedNotification.ChannelId)))
			{
				return false;
			}
			if (this.Assignment.Count != memberRoleChangedNotification.Assignment.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Assignment.Count; i++)
			{
				if (!this.Assignment[i].Equals(memberRoleChangedNotification.Assignment[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06004D65 RID: 19813 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x000F0330 File Offset: 0x000EE530
		public static MemberRoleChangedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberRoleChangedNotification>(bs, 0, -1);
		}

		// Token: 0x06004D67 RID: 19815 RVA: 0x000F033A File Offset: 0x000EE53A
		public void Deserialize(Stream stream)
		{
			MemberRoleChangedNotification.Deserialize(stream, this);
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x000F0344 File Offset: 0x000EE544
		public static MemberRoleChangedNotification Deserialize(Stream stream, MemberRoleChangedNotification instance)
		{
			return MemberRoleChangedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x000F0350 File Offset: 0x000EE550
		public static MemberRoleChangedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberRoleChangedNotification memberRoleChangedNotification = new MemberRoleChangedNotification();
			MemberRoleChangedNotification.DeserializeLengthDelimited(stream, memberRoleChangedNotification);
			return memberRoleChangedNotification;
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x000F036C File Offset: 0x000EE56C
		public static MemberRoleChangedNotification DeserializeLengthDelimited(Stream stream, MemberRoleChangedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberRoleChangedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004D6B RID: 19819 RVA: 0x000F0394 File Offset: 0x000EE594
		public static MemberRoleChangedNotification Deserialize(Stream stream, MemberRoleChangedNotification instance, long limit)
		{
			if (instance.Assignment == null)
			{
				instance.Assignment = new List<RoleAssignment>();
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
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								if (instance.SubscriberId == null)
								{
									instance.SubscriberId = GameAccountHandle.DeserializeLengthDelimited(stream);
									continue;
								}
								GameAccountHandle.DeserializeLengthDelimited(stream, instance.SubscriberId);
								continue;
							}
						}
						else
						{
							if (instance.AgentId == null)
							{
								instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 34)
						{
							instance.Assignment.Add(RoleAssignment.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					else
					{
						if (instance.ChannelId == null)
						{
							instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
							continue;
						}
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
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

		// Token: 0x06004D6C RID: 19820 RVA: 0x000F04D7 File Offset: 0x000EE6D7
		public void Serialize(Stream stream)
		{
			MemberRoleChangedNotification.Serialize(stream, this);
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x000F04E0 File Offset: 0x000EE6E0
		public static void Serialize(Stream stream, MemberRoleChangedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.Assignment.Count > 0)
			{
				foreach (RoleAssignment roleAssignment in instance.Assignment)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, roleAssignment.GetSerializedSize());
					RoleAssignment.Serialize(stream, roleAssignment);
				}
			}
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x000F05DC File Offset: 0x000EE7DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize2 = this.SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize3 = this.ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.Assignment.Count > 0)
			{
				foreach (RoleAssignment roleAssignment in this.Assignment)
				{
					num += 1U;
					uint serializedSize4 = roleAssignment.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			return num;
		}

		// Token: 0x0400192A RID: 6442
		public bool HasAgentId;

		// Token: 0x0400192B RID: 6443
		private GameAccountHandle _AgentId;

		// Token: 0x0400192C RID: 6444
		public bool HasSubscriberId;

		// Token: 0x0400192D RID: 6445
		private GameAccountHandle _SubscriberId;

		// Token: 0x0400192E RID: 6446
		public bool HasChannelId;

		// Token: 0x0400192F RID: 6447
		private ChannelId _ChannelId;

		// Token: 0x04001930 RID: 6448
		private List<RoleAssignment> _Assignment = new List<RoleAssignment>();
	}
}
