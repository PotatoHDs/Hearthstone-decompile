using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x02000490 RID: 1168
	public class MemberRemovedNotification : IProtoBuf
	{
		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06005142 RID: 20802 RVA: 0x000FBFC7 File Offset: 0x000FA1C7
		// (set) Token: 0x06005143 RID: 20803 RVA: 0x000FBFCF File Offset: 0x000FA1CF
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

		// Token: 0x06005144 RID: 20804 RVA: 0x000FBFE2 File Offset: 0x000FA1E2
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06005145 RID: 20805 RVA: 0x000FBFEB File Offset: 0x000FA1EB
		// (set) Token: 0x06005146 RID: 20806 RVA: 0x000FBFF3 File Offset: 0x000FA1F3
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

		// Token: 0x06005147 RID: 20807 RVA: 0x000FC006 File Offset: 0x000FA206
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06005148 RID: 20808 RVA: 0x000FC00F File Offset: 0x000FA20F
		// (set) Token: 0x06005149 RID: 20809 RVA: 0x000FC017 File Offset: 0x000FA217
		public uint Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		// Token: 0x0600514A RID: 20810 RVA: 0x000FC027 File Offset: 0x000FA227
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x0600514B RID: 20811 RVA: 0x000FC030 File Offset: 0x000FA230
		// (set) Token: 0x0600514C RID: 20812 RVA: 0x000FC038 File Offset: 0x000FA238
		public List<GameAccountHandle> MemberId
		{
			get
			{
				return this._MemberId;
			}
			set
			{
				this._MemberId = value;
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x0600514D RID: 20813 RVA: 0x000FC030 File Offset: 0x000FA230
		public List<GameAccountHandle> MemberIdList
		{
			get
			{
				return this._MemberId;
			}
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x0600514E RID: 20814 RVA: 0x000FC041 File Offset: 0x000FA241
		public int MemberIdCount
		{
			get
			{
				return this._MemberId.Count;
			}
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x000FC04E File Offset: 0x000FA24E
		public void AddMemberId(GameAccountHandle val)
		{
			this._MemberId.Add(val);
		}

		// Token: 0x06005150 RID: 20816 RVA: 0x000FC05C File Offset: 0x000FA25C
		public void ClearMemberId()
		{
			this._MemberId.Clear();
		}

		// Token: 0x06005151 RID: 20817 RVA: 0x000FC069 File Offset: 0x000FA269
		public void SetMemberId(List<GameAccountHandle> val)
		{
			this.MemberId = val;
		}

		// Token: 0x06005152 RID: 20818 RVA: 0x000FC074 File Offset: 0x000FA274
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
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			foreach (GameAccountHandle gameAccountHandle in this.MemberId)
			{
				num ^= gameAccountHandle.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005153 RID: 20819 RVA: 0x000FC11C File Offset: 0x000FA31C
		public override bool Equals(object obj)
		{
			MemberRemovedNotification memberRemovedNotification = obj as MemberRemovedNotification;
			if (memberRemovedNotification == null)
			{
				return false;
			}
			if (this.HasAgentId != memberRemovedNotification.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(memberRemovedNotification.AgentId)))
			{
				return false;
			}
			if (this.HasChannelId != memberRemovedNotification.HasChannelId || (this.HasChannelId && !this.ChannelId.Equals(memberRemovedNotification.ChannelId)))
			{
				return false;
			}
			if (this.HasReason != memberRemovedNotification.HasReason || (this.HasReason && !this.Reason.Equals(memberRemovedNotification.Reason)))
			{
				return false;
			}
			if (this.MemberId.Count != memberRemovedNotification.MemberId.Count)
			{
				return false;
			}
			for (int i = 0; i < this.MemberId.Count; i++)
			{
				if (!this.MemberId[i].Equals(memberRemovedNotification.MemberId[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06005154 RID: 20820 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x000FC20B File Offset: 0x000FA40B
		public static MemberRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberRemovedNotification>(bs, 0, -1);
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x000FC215 File Offset: 0x000FA415
		public void Deserialize(Stream stream)
		{
			MemberRemovedNotification.Deserialize(stream, this);
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x000FC21F File Offset: 0x000FA41F
		public static MemberRemovedNotification Deserialize(Stream stream, MemberRemovedNotification instance)
		{
			return MemberRemovedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x000FC22C File Offset: 0x000FA42C
		public static MemberRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberRemovedNotification memberRemovedNotification = new MemberRemovedNotification();
			MemberRemovedNotification.DeserializeLengthDelimited(stream, memberRemovedNotification);
			return memberRemovedNotification;
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x000FC248 File Offset: 0x000FA448
		public static MemberRemovedNotification DeserializeLengthDelimited(Stream stream, MemberRemovedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberRemovedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600515A RID: 20826 RVA: 0x000FC270 File Offset: 0x000FA470
		public static MemberRemovedNotification Deserialize(Stream stream, MemberRemovedNotification instance, long limit)
		{
			if (instance.MemberId == null)
			{
				instance.MemberId = new List<GameAccountHandle>();
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
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num == 26)
							{
								if (instance.ChannelId == null)
								{
									instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
									continue;
								}
								ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
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
					else
					{
						if (num == 32)
						{
							instance.Reason = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 42)
						{
							instance.MemberId.Add(GameAccountHandle.DeserializeLengthDelimited(stream));
							continue;
						}
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

		// Token: 0x0600515B RID: 20827 RVA: 0x000FC393 File Offset: 0x000FA593
		public void Serialize(Stream stream)
		{
			MemberRemovedNotification.Serialize(stream, this);
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x000FC39C File Offset: 0x000FA59C
		public static void Serialize(Stream stream, MemberRemovedNotification instance)
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
			if (instance.HasReason)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.MemberId.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in instance.MemberId)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, gameAccountHandle.GetSerializedSize());
					GameAccountHandle.Serialize(stream, gameAccountHandle);
				}
			}
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x000FC488 File Offset: 0x000FA688
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
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			if (this.MemberId.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in this.MemberId)
				{
					num += 1U;
					uint serializedSize3 = gameAccountHandle.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num;
		}

		// Token: 0x04001A2A RID: 6698
		public bool HasAgentId;

		// Token: 0x04001A2B RID: 6699
		private GameAccountHandle _AgentId;

		// Token: 0x04001A2C RID: 6700
		public bool HasChannelId;

		// Token: 0x04001A2D RID: 6701
		private ChannelId _ChannelId;

		// Token: 0x04001A2E RID: 6702
		public bool HasReason;

		// Token: 0x04001A2F RID: 6703
		private uint _Reason;

		// Token: 0x04001A30 RID: 6704
		private List<GameAccountHandle> _MemberId = new List<GameAccountHandle>();
	}
}
