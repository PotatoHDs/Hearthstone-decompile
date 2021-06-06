using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004D1 RID: 1233
	public class UpdateMemberStateNotification : IProtoBuf
	{
		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x060056A5 RID: 22181 RVA: 0x00109DCD File Offset: 0x00107FCD
		// (set) Token: 0x060056A6 RID: 22182 RVA: 0x00109DD5 File Offset: 0x00107FD5
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

		// Token: 0x060056A7 RID: 22183 RVA: 0x00109DE8 File Offset: 0x00107FE8
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x060056A8 RID: 22184 RVA: 0x00109DF1 File Offset: 0x00107FF1
		// (set) Token: 0x060056A9 RID: 22185 RVA: 0x00109DF9 File Offset: 0x00107FF9
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

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x060056AA RID: 22186 RVA: 0x00109DF1 File Offset: 0x00107FF1
		public List<Member> StateChangeList
		{
			get
			{
				return this._StateChange;
			}
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x060056AB RID: 22187 RVA: 0x00109E02 File Offset: 0x00108002
		public int StateChangeCount
		{
			get
			{
				return this._StateChange.Count;
			}
		}

		// Token: 0x060056AC RID: 22188 RVA: 0x00109E0F File Offset: 0x0010800F
		public void AddStateChange(Member val)
		{
			this._StateChange.Add(val);
		}

		// Token: 0x060056AD RID: 22189 RVA: 0x00109E1D File Offset: 0x0010801D
		public void ClearStateChange()
		{
			this._StateChange.Clear();
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x00109E2A File Offset: 0x0010802A
		public void SetStateChange(List<Member> val)
		{
			this.StateChange = val;
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x060056AF RID: 22191 RVA: 0x00109E33 File Offset: 0x00108033
		// (set) Token: 0x060056B0 RID: 22192 RVA: 0x00109E3B File Offset: 0x0010803B
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

		// Token: 0x060056B1 RID: 22193 RVA: 0x00109E4E File Offset: 0x0010804E
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x060056B2 RID: 22194 RVA: 0x00109E57 File Offset: 0x00108057
		// (set) Token: 0x060056B3 RID: 22195 RVA: 0x00109E5F File Offset: 0x0010805F
		public SubscriberId SubscriberId
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

		// Token: 0x060056B4 RID: 22196 RVA: 0x00109E72 File Offset: 0x00108072
		public void SetSubscriberId(SubscriberId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x00109E7C File Offset: 0x0010807C
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
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060056B6 RID: 22198 RVA: 0x00109F20 File Offset: 0x00108120
		public override bool Equals(object obj)
		{
			UpdateMemberStateNotification updateMemberStateNotification = obj as UpdateMemberStateNotification;
			if (updateMemberStateNotification == null)
			{
				return false;
			}
			if (this.HasAgentId != updateMemberStateNotification.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(updateMemberStateNotification.AgentId)))
			{
				return false;
			}
			if (this.StateChange.Count != updateMemberStateNotification.StateChange.Count)
			{
				return false;
			}
			for (int i = 0; i < this.StateChange.Count; i++)
			{
				if (!this.StateChange[i].Equals(updateMemberStateNotification.StateChange[i]))
				{
					return false;
				}
			}
			return this.HasChannelId == updateMemberStateNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(updateMemberStateNotification.ChannelId)) && this.HasSubscriberId == updateMemberStateNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(updateMemberStateNotification.SubscriberId));
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x060056B7 RID: 22199 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060056B8 RID: 22200 RVA: 0x0010A00C File Offset: 0x0010820C
		public static UpdateMemberStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateMemberStateNotification>(bs, 0, -1);
		}

		// Token: 0x060056B9 RID: 22201 RVA: 0x0010A016 File Offset: 0x00108216
		public void Deserialize(Stream stream)
		{
			UpdateMemberStateNotification.Deserialize(stream, this);
		}

		// Token: 0x060056BA RID: 22202 RVA: 0x0010A020 File Offset: 0x00108220
		public static UpdateMemberStateNotification Deserialize(Stream stream, UpdateMemberStateNotification instance)
		{
			return UpdateMemberStateNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060056BB RID: 22203 RVA: 0x0010A02C File Offset: 0x0010822C
		public static UpdateMemberStateNotification DeserializeLengthDelimited(Stream stream)
		{
			UpdateMemberStateNotification updateMemberStateNotification = new UpdateMemberStateNotification();
			UpdateMemberStateNotification.DeserializeLengthDelimited(stream, updateMemberStateNotification);
			return updateMemberStateNotification;
		}

		// Token: 0x060056BC RID: 22204 RVA: 0x0010A048 File Offset: 0x00108248
		public static UpdateMemberStateNotification DeserializeLengthDelimited(Stream stream, UpdateMemberStateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateMemberStateNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x060056BD RID: 22205 RVA: 0x0010A070 File Offset: 0x00108270
		public static UpdateMemberStateNotification Deserialize(Stream stream, UpdateMemberStateNotification instance, long limit)
		{
			if (instance.StateChange == null)
			{
				instance.StateChange = new List<Member>();
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
								instance.StateChange.Add(Member.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
						{
							if (instance.AgentId == null)
							{
								instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
							continue;
						}
					}
					else if (num != 34)
					{
						if (num == 42)
						{
							if (instance.SubscriberId == null)
							{
								instance.SubscriberId = SubscriberId.DeserializeLengthDelimited(stream);
								continue;
							}
							SubscriberId.DeserializeLengthDelimited(stream, instance.SubscriberId);
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

		// Token: 0x060056BE RID: 22206 RVA: 0x0010A1B0 File Offset: 0x001083B0
		public void Serialize(Stream stream)
		{
			UpdateMemberStateNotification.Serialize(stream, this);
		}

		// Token: 0x060056BF RID: 22207 RVA: 0x0010A1BC File Offset: 0x001083BC
		public static void Serialize(Stream stream, UpdateMemberStateNotification instance)
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
			if (instance.HasChannelId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				SubscriberId.Serialize(stream, instance.SubscriberId);
			}
		}

		// Token: 0x060056C0 RID: 22208 RVA: 0x0010A2B8 File Offset: 0x001084B8
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
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize3 = this.ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize4 = this.SubscriberId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}

		// Token: 0x04001B47 RID: 6983
		public bool HasAgentId;

		// Token: 0x04001B48 RID: 6984
		private EntityId _AgentId;

		// Token: 0x04001B49 RID: 6985
		private List<Member> _StateChange = new List<Member>();

		// Token: 0x04001B4A RID: 6986
		public bool HasChannelId;

		// Token: 0x04001B4B RID: 6987
		private ChannelId _ChannelId;

		// Token: 0x04001B4C RID: 6988
		public bool HasSubscriberId;

		// Token: 0x04001B4D RID: 6989
		private SubscriberId _SubscriberId;
	}
}
