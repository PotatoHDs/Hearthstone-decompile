using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004C9 RID: 1225
	public class UpdateMemberStateRequest : IProtoBuf
	{
		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x060055C8 RID: 21960 RVA: 0x00107266 File Offset: 0x00105466
		// (set) Token: 0x060055C9 RID: 21961 RVA: 0x0010726E File Offset: 0x0010546E
		public EntityId AgentId
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

		// Token: 0x060055CA RID: 21962 RVA: 0x00107281 File Offset: 0x00105481
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x060055CB RID: 21963 RVA: 0x0010728A File Offset: 0x0010548A
		// (set) Token: 0x060055CC RID: 21964 RVA: 0x00107292 File Offset: 0x00105492
		public List<Member> StateChange
		{
			get
			{
				return this._StateChange;
			}
			set
			{
				this._StateChange = value;
			}
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x060055CD RID: 21965 RVA: 0x0010728A File Offset: 0x0010548A
		public List<Member> StateChangeList
		{
			get
			{
				return this._StateChange;
			}
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x060055CE RID: 21966 RVA: 0x0010729B File Offset: 0x0010549B
		public int StateChangeCount
		{
			get
			{
				return this._StateChange.Count;
			}
		}

		// Token: 0x060055CF RID: 21967 RVA: 0x001072A8 File Offset: 0x001054A8
		public void AddStateChange(Member val)
		{
			this._StateChange.Add(val);
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x001072B6 File Offset: 0x001054B6
		public void ClearStateChange()
		{
			this._StateChange.Clear();
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x001072C3 File Offset: 0x001054C3
		public void SetStateChange(List<Member> val)
		{
			this.StateChange = val;
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x060055D2 RID: 21970 RVA: 0x001072CC File Offset: 0x001054CC
		// (set) Token: 0x060055D3 RID: 21971 RVA: 0x001072D4 File Offset: 0x001054D4
		public List<uint> RemovedRole
		{
			get
			{
				return this._RemovedRole;
			}
			set
			{
				this._RemovedRole = value;
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x060055D4 RID: 21972 RVA: 0x001072CC File Offset: 0x001054CC
		public List<uint> RemovedRoleList
		{
			get
			{
				return this._RemovedRole;
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x060055D5 RID: 21973 RVA: 0x001072DD File Offset: 0x001054DD
		public int RemovedRoleCount
		{
			get
			{
				return this._RemovedRole.Count;
			}
		}

		// Token: 0x060055D6 RID: 21974 RVA: 0x001072EA File Offset: 0x001054EA
		public void AddRemovedRole(uint val)
		{
			this._RemovedRole.Add(val);
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x001072F8 File Offset: 0x001054F8
		public void ClearRemovedRole()
		{
			this._RemovedRole.Clear();
		}

		// Token: 0x060055D8 RID: 21976 RVA: 0x00107305 File Offset: 0x00105505
		public void SetRemovedRole(List<uint> val)
		{
			this.RemovedRole = val;
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x00107310 File Offset: 0x00105510
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			foreach (Member member in this.StateChange)
			{
				num ^= member.GetHashCode();
			}
			foreach (uint num2 in this.RemovedRole)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x001073D0 File Offset: 0x001055D0
		public override bool Equals(object obj)
		{
			UpdateMemberStateRequest updateMemberStateRequest = obj as UpdateMemberStateRequest;
			if (updateMemberStateRequest == null)
			{
				return false;
			}
			if (this.HasAgentId != updateMemberStateRequest.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(updateMemberStateRequest.AgentId)))
			{
				return false;
			}
			if (this.StateChange.Count != updateMemberStateRequest.StateChange.Count)
			{
				return false;
			}
			for (int i = 0; i < this.StateChange.Count; i++)
			{
				if (!this.StateChange[i].Equals(updateMemberStateRequest.StateChange[i]))
				{
					return false;
				}
			}
			if (this.RemovedRole.Count != updateMemberStateRequest.RemovedRole.Count)
			{
				return false;
			}
			for (int j = 0; j < this.RemovedRole.Count; j++)
			{
				if (!this.RemovedRole[j].Equals(updateMemberStateRequest.RemovedRole[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x060055DB RID: 21979 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060055DC RID: 21980 RVA: 0x001074BA File Offset: 0x001056BA
		public static UpdateMemberStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateMemberStateRequest>(bs, 0, -1);
		}

		// Token: 0x060055DD RID: 21981 RVA: 0x001074C4 File Offset: 0x001056C4
		public void Deserialize(Stream stream)
		{
			UpdateMemberStateRequest.Deserialize(stream, this);
		}

		// Token: 0x060055DE RID: 21982 RVA: 0x001074CE File Offset: 0x001056CE
		public static UpdateMemberStateRequest Deserialize(Stream stream, UpdateMemberStateRequest instance)
		{
			return UpdateMemberStateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060055DF RID: 21983 RVA: 0x001074DC File Offset: 0x001056DC
		public static UpdateMemberStateRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateMemberStateRequest updateMemberStateRequest = new UpdateMemberStateRequest();
			UpdateMemberStateRequest.DeserializeLengthDelimited(stream, updateMemberStateRequest);
			return updateMemberStateRequest;
		}

		// Token: 0x060055E0 RID: 21984 RVA: 0x001074F8 File Offset: 0x001056F8
		public static UpdateMemberStateRequest DeserializeLengthDelimited(Stream stream, UpdateMemberStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateMemberStateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x00107520 File Offset: 0x00105720
		public static UpdateMemberStateRequest Deserialize(Stream stream, UpdateMemberStateRequest instance, long limit)
		{
			if (instance.StateChange == null)
			{
				instance.StateChange = new List<Member>();
			}
			if (instance.RemovedRole == null)
			{
				instance.RemovedRole = new List<uint>();
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
							long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
							num2 += stream.Position;
							while (stream.Position < num2)
							{
								instance.RemovedRole.Add(ProtocolParser.ReadUInt32(stream));
							}
							if (stream.Position != num2)
							{
								throw new ProtocolBufferException("Read too many bytes in packed data");
							}
						}
					}
					else
					{
						instance.StateChange.Add(Member.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x0010764C File Offset: 0x0010584C
		public void Serialize(Stream stream)
		{
			UpdateMemberStateRequest.Serialize(stream, this);
		}

		// Token: 0x060055E3 RID: 21987 RVA: 0x00107658 File Offset: 0x00105858
		public static void Serialize(Stream stream, UpdateMemberStateRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.StateChange.Count > 0)
			{
				foreach (Member member in instance.StateChange)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, member.GetSerializedSize());
					Member.Serialize(stream, member);
				}
			}
			if (instance.RemovedRole.Count > 0)
			{
				stream.WriteByte(26);
				uint num = 0U;
				foreach (uint val in instance.RemovedRole)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.RemovedRole)
				{
					ProtocolParser.WriteUInt32(stream, val2);
				}
			}
		}

		// Token: 0x060055E4 RID: 21988 RVA: 0x001077A8 File Offset: 0x001059A8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.StateChange.Count > 0)
			{
				foreach (Member member in this.StateChange)
				{
					num += 1U;
					uint serializedSize2 = member.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.RemovedRole.Count > 0)
			{
				num += 1U;
				uint num2 = num;
				foreach (uint val in this.RemovedRole)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			return num;
		}

		// Token: 0x04001B0A RID: 6922
		public bool HasAgentId;

		// Token: 0x04001B0B RID: 6923
		private EntityId _AgentId;

		// Token: 0x04001B0C RID: 6924
		private List<Member> _StateChange = new List<Member>();

		// Token: 0x04001B0D RID: 6925
		private List<uint> _RemovedRole = new List<uint>();
	}
}
