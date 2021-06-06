using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x02000492 RID: 1170
	public class MemberRoleChangedNotification : IProtoBuf
	{
		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06005179 RID: 20857 RVA: 0x000FCA5B File Offset: 0x000FAC5B
		// (set) Token: 0x0600517A RID: 20858 RVA: 0x000FCA63 File Offset: 0x000FAC63
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

		// Token: 0x0600517B RID: 20859 RVA: 0x000FCA76 File Offset: 0x000FAC76
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x0600517C RID: 20860 RVA: 0x000FCA7F File Offset: 0x000FAC7F
		// (set) Token: 0x0600517D RID: 20861 RVA: 0x000FCA87 File Offset: 0x000FAC87
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

		// Token: 0x0600517E RID: 20862 RVA: 0x000FCA9A File Offset: 0x000FAC9A
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x0600517F RID: 20863 RVA: 0x000FCAA3 File Offset: 0x000FACA3
		// (set) Token: 0x06005180 RID: 20864 RVA: 0x000FCAAB File Offset: 0x000FACAB
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

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06005181 RID: 20865 RVA: 0x000FCAA3 File Offset: 0x000FACA3
		public List<RoleAssignment> AssignmentList
		{
			get
			{
				return this._Assignment;
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06005182 RID: 20866 RVA: 0x000FCAB4 File Offset: 0x000FACB4
		public int AssignmentCount
		{
			get
			{
				return this._Assignment.Count;
			}
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x000FCAC1 File Offset: 0x000FACC1
		public void AddAssignment(RoleAssignment val)
		{
			this._Assignment.Add(val);
		}

		// Token: 0x06005184 RID: 20868 RVA: 0x000FCACF File Offset: 0x000FACCF
		public void ClearAssignment()
		{
			this._Assignment.Clear();
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x000FCADC File Offset: 0x000FACDC
		public void SetAssignment(List<RoleAssignment> val)
		{
			this.Assignment = val;
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x000FCAE8 File Offset: 0x000FACE8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
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

		// Token: 0x06005187 RID: 20871 RVA: 0x000FCB78 File Offset: 0x000FAD78
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

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06005188 RID: 20872 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x000FCC39 File Offset: 0x000FAE39
		public static MemberRoleChangedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberRoleChangedNotification>(bs, 0, -1);
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x000FCC43 File Offset: 0x000FAE43
		public void Deserialize(Stream stream)
		{
			MemberRoleChangedNotification.Deserialize(stream, this);
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x000FCC4D File Offset: 0x000FAE4D
		public static MemberRoleChangedNotification Deserialize(Stream stream, MemberRoleChangedNotification instance)
		{
			return MemberRoleChangedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x000FCC58 File Offset: 0x000FAE58
		public static MemberRoleChangedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberRoleChangedNotification memberRoleChangedNotification = new MemberRoleChangedNotification();
			MemberRoleChangedNotification.DeserializeLengthDelimited(stream, memberRoleChangedNotification);
			return memberRoleChangedNotification;
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x000FCC74 File Offset: 0x000FAE74
		public static MemberRoleChangedNotification DeserializeLengthDelimited(Stream stream, MemberRoleChangedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberRoleChangedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x000FCC9C File Offset: 0x000FAE9C
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
				else if (num != 10)
				{
					if (num != 26)
					{
						if (num != 42)
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
							instance.Assignment.Add(RoleAssignment.DeserializeLengthDelimited(stream));
						}
					}
					else if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x000FCD9C File Offset: 0x000FAF9C
		public void Serialize(Stream stream)
		{
			MemberRoleChangedNotification.Serialize(stream, this);
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x000FCDA8 File Offset: 0x000FAFA8
		public static void Serialize(Stream stream, MemberRoleChangedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
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
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, roleAssignment.GetSerializedSize());
					RoleAssignment.Serialize(stream, roleAssignment);
				}
			}
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x000FCE78 File Offset: 0x000FB078
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize2 = this.ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.Assignment.Count > 0)
			{
				foreach (RoleAssignment roleAssignment in this.Assignment)
				{
					num += 1U;
					uint serializedSize3 = roleAssignment.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num;
		}

		// Token: 0x04001A36 RID: 6710
		public bool HasAgentId;

		// Token: 0x04001A37 RID: 6711
		private GameAccountHandle _AgentId;

		// Token: 0x04001A38 RID: 6712
		public bool HasChannelId;

		// Token: 0x04001A39 RID: 6713
		private ChannelId _ChannelId;

		// Token: 0x04001A3A RID: 6714
		private List<RoleAssignment> _Assignment = new List<RoleAssignment>();
	}
}
