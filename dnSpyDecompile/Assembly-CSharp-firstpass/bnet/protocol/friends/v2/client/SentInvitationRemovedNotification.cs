using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x020003F7 RID: 1015
	public class SentInvitationRemovedNotification : IProtoBuf
	{
		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x0600435C RID: 17244 RVA: 0x000D54B7 File Offset: 0x000D36B7
		// (set) Token: 0x0600435D RID: 17245 RVA: 0x000D54BF File Offset: 0x000D36BF
		public ulong AgentAccountId
		{
			get
			{
				return this._AgentAccountId;
			}
			set
			{
				this._AgentAccountId = value;
				this.HasAgentAccountId = true;
			}
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x000D54CF File Offset: 0x000D36CF
		public void SetAgentAccountId(ulong val)
		{
			this.AgentAccountId = val;
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x0600435F RID: 17247 RVA: 0x000D54D8 File Offset: 0x000D36D8
		// (set) Token: 0x06004360 RID: 17248 RVA: 0x000D54E0 File Offset: 0x000D36E0
		public List<RemovedInvitationAssignment> Assignment
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

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06004361 RID: 17249 RVA: 0x000D54D8 File Offset: 0x000D36D8
		public List<RemovedInvitationAssignment> AssignmentList
		{
			get
			{
				return this._Assignment;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06004362 RID: 17250 RVA: 0x000D54E9 File Offset: 0x000D36E9
		public int AssignmentCount
		{
			get
			{
				return this._Assignment.Count;
			}
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x000D54F6 File Offset: 0x000D36F6
		public void AddAssignment(RemovedInvitationAssignment val)
		{
			this._Assignment.Add(val);
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x000D5504 File Offset: 0x000D3704
		public void ClearAssignment()
		{
			this._Assignment.Clear();
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x000D5511 File Offset: 0x000D3711
		public void SetAssignment(List<RemovedInvitationAssignment> val)
		{
			this.Assignment = val;
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x000D551C File Offset: 0x000D371C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentAccountId)
			{
				num ^= this.AgentAccountId.GetHashCode();
			}
			foreach (RemovedInvitationAssignment removedInvitationAssignment in this.Assignment)
			{
				num ^= removedInvitationAssignment.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x000D5598 File Offset: 0x000D3798
		public override bool Equals(object obj)
		{
			SentInvitationRemovedNotification sentInvitationRemovedNotification = obj as SentInvitationRemovedNotification;
			if (sentInvitationRemovedNotification == null)
			{
				return false;
			}
			if (this.HasAgentAccountId != sentInvitationRemovedNotification.HasAgentAccountId || (this.HasAgentAccountId && !this.AgentAccountId.Equals(sentInvitationRemovedNotification.AgentAccountId)))
			{
				return false;
			}
			if (this.Assignment.Count != sentInvitationRemovedNotification.Assignment.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Assignment.Count; i++)
			{
				if (!this.Assignment[i].Equals(sentInvitationRemovedNotification.Assignment[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06004368 RID: 17256 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x000D5631 File Offset: 0x000D3831
		public static SentInvitationRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SentInvitationRemovedNotification>(bs, 0, -1);
		}

		// Token: 0x0600436A RID: 17258 RVA: 0x000D563B File Offset: 0x000D383B
		public void Deserialize(Stream stream)
		{
			SentInvitationRemovedNotification.Deserialize(stream, this);
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x000D5645 File Offset: 0x000D3845
		public static SentInvitationRemovedNotification Deserialize(Stream stream, SentInvitationRemovedNotification instance)
		{
			return SentInvitationRemovedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600436C RID: 17260 RVA: 0x000D5650 File Offset: 0x000D3850
		public static SentInvitationRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			SentInvitationRemovedNotification sentInvitationRemovedNotification = new SentInvitationRemovedNotification();
			SentInvitationRemovedNotification.DeserializeLengthDelimited(stream, sentInvitationRemovedNotification);
			return sentInvitationRemovedNotification;
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x000D566C File Offset: 0x000D386C
		public static SentInvitationRemovedNotification DeserializeLengthDelimited(Stream stream, SentInvitationRemovedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SentInvitationRemovedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x000D5694 File Offset: 0x000D3894
		public static SentInvitationRemovedNotification Deserialize(Stream stream, SentInvitationRemovedNotification instance, long limit)
		{
			if (instance.Assignment == null)
			{
				instance.Assignment = new List<RemovedInvitationAssignment>();
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
				else if (num != 8)
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
						instance.Assignment.Add(RemovedInvitationAssignment.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.AgentAccountId = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x000D5743 File Offset: 0x000D3943
		public void Serialize(Stream stream)
		{
			SentInvitationRemovedNotification.Serialize(stream, this);
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x000D574C File Offset: 0x000D394C
		public static void Serialize(Stream stream, SentInvitationRemovedNotification instance)
		{
			if (instance.HasAgentAccountId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.AgentAccountId);
			}
			if (instance.Assignment.Count > 0)
			{
				foreach (RemovedInvitationAssignment removedInvitationAssignment in instance.Assignment)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, removedInvitationAssignment.GetSerializedSize());
					RemovedInvitationAssignment.Serialize(stream, removedInvitationAssignment);
				}
			}
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x000D57DC File Offset: 0x000D39DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentAccountId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.AgentAccountId);
			}
			if (this.Assignment.Count > 0)
			{
				foreach (RemovedInvitationAssignment removedInvitationAssignment in this.Assignment)
				{
					num += 1U;
					uint serializedSize = removedInvitationAssignment.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001702 RID: 5890
		public bool HasAgentAccountId;

		// Token: 0x04001703 RID: 5891
		private ulong _AgentAccountId;

		// Token: 0x04001704 RID: 5892
		private List<RemovedInvitationAssignment> _Assignment = new List<RemovedInvitationAssignment>();
	}
}
