using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x02000491 RID: 1169
	public class MemberStateChangedNotification : IProtoBuf
	{
		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x0600515F RID: 20831 RVA: 0x000FC56F File Offset: 0x000FA76F
		// (set) Token: 0x06005160 RID: 20832 RVA: 0x000FC577 File Offset: 0x000FA777
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

		// Token: 0x06005161 RID: 20833 RVA: 0x000FC58A File Offset: 0x000FA78A
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06005162 RID: 20834 RVA: 0x000FC593 File Offset: 0x000FA793
		// (set) Token: 0x06005163 RID: 20835 RVA: 0x000FC59B File Offset: 0x000FA79B
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

		// Token: 0x06005164 RID: 20836 RVA: 0x000FC5AE File Offset: 0x000FA7AE
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06005165 RID: 20837 RVA: 0x000FC5B7 File Offset: 0x000FA7B7
		// (set) Token: 0x06005166 RID: 20838 RVA: 0x000FC5BF File Offset: 0x000FA7BF
		public List<MemberStateAssignment> Assignment
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

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06005167 RID: 20839 RVA: 0x000FC5B7 File Offset: 0x000FA7B7
		public List<MemberStateAssignment> AssignmentList
		{
			get
			{
				return this._Assignment;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06005168 RID: 20840 RVA: 0x000FC5C8 File Offset: 0x000FA7C8
		public int AssignmentCount
		{
			get
			{
				return this._Assignment.Count;
			}
		}

		// Token: 0x06005169 RID: 20841 RVA: 0x000FC5D5 File Offset: 0x000FA7D5
		public void AddAssignment(MemberStateAssignment val)
		{
			this._Assignment.Add(val);
		}

		// Token: 0x0600516A RID: 20842 RVA: 0x000FC5E3 File Offset: 0x000FA7E3
		public void ClearAssignment()
		{
			this._Assignment.Clear();
		}

		// Token: 0x0600516B RID: 20843 RVA: 0x000FC5F0 File Offset: 0x000FA7F0
		public void SetAssignment(List<MemberStateAssignment> val)
		{
			this.Assignment = val;
		}

		// Token: 0x0600516C RID: 20844 RVA: 0x000FC5FC File Offset: 0x000FA7FC
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
			foreach (MemberStateAssignment memberStateAssignment in this.Assignment)
			{
				num ^= memberStateAssignment.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x000FC68C File Offset: 0x000FA88C
		public override bool Equals(object obj)
		{
			MemberStateChangedNotification memberStateChangedNotification = obj as MemberStateChangedNotification;
			if (memberStateChangedNotification == null)
			{
				return false;
			}
			if (this.HasAgentId != memberStateChangedNotification.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(memberStateChangedNotification.AgentId)))
			{
				return false;
			}
			if (this.HasChannelId != memberStateChangedNotification.HasChannelId || (this.HasChannelId && !this.ChannelId.Equals(memberStateChangedNotification.ChannelId)))
			{
				return false;
			}
			if (this.Assignment.Count != memberStateChangedNotification.Assignment.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Assignment.Count; i++)
			{
				if (!this.Assignment[i].Equals(memberStateChangedNotification.Assignment[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x0600516E RID: 20846 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x000FC74D File Offset: 0x000FA94D
		public static MemberStateChangedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberStateChangedNotification>(bs, 0, -1);
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x000FC757 File Offset: 0x000FA957
		public void Deserialize(Stream stream)
		{
			MemberStateChangedNotification.Deserialize(stream, this);
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x000FC761 File Offset: 0x000FA961
		public static MemberStateChangedNotification Deserialize(Stream stream, MemberStateChangedNotification instance)
		{
			return MemberStateChangedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x000FC76C File Offset: 0x000FA96C
		public static MemberStateChangedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberStateChangedNotification memberStateChangedNotification = new MemberStateChangedNotification();
			MemberStateChangedNotification.DeserializeLengthDelimited(stream, memberStateChangedNotification);
			return memberStateChangedNotification;
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x000FC788 File Offset: 0x000FA988
		public static MemberStateChangedNotification DeserializeLengthDelimited(Stream stream, MemberStateChangedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberStateChangedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x000FC7B0 File Offset: 0x000FA9B0
		public static MemberStateChangedNotification Deserialize(Stream stream, MemberStateChangedNotification instance, long limit)
		{
			if (instance.Assignment == null)
			{
				instance.Assignment = new List<MemberStateAssignment>();
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
							instance.Assignment.Add(MemberStateAssignment.DeserializeLengthDelimited(stream));
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

		// Token: 0x06005175 RID: 20853 RVA: 0x000FC8B0 File Offset: 0x000FAAB0
		public void Serialize(Stream stream)
		{
			MemberStateChangedNotification.Serialize(stream, this);
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x000FC8BC File Offset: 0x000FAABC
		public static void Serialize(Stream stream, MemberStateChangedNotification instance)
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
				foreach (MemberStateAssignment memberStateAssignment in instance.Assignment)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, memberStateAssignment.GetSerializedSize());
					MemberStateAssignment.Serialize(stream, memberStateAssignment);
				}
			}
		}

		// Token: 0x06005177 RID: 20855 RVA: 0x000FC98C File Offset: 0x000FAB8C
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
				foreach (MemberStateAssignment memberStateAssignment in this.Assignment)
				{
					num += 1U;
					uint serializedSize3 = memberStateAssignment.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num;
		}

		// Token: 0x04001A31 RID: 6705
		public bool HasAgentId;

		// Token: 0x04001A32 RID: 6706
		private GameAccountHandle _AgentId;

		// Token: 0x04001A33 RID: 6707
		public bool HasChannelId;

		// Token: 0x04001A34 RID: 6708
		private ChannelId _ChannelId;

		// Token: 0x04001A35 RID: 6709
		private List<MemberStateAssignment> _Assignment = new List<MemberStateAssignment>();
	}
}
