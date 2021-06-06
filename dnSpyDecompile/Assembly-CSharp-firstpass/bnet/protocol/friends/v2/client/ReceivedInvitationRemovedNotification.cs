using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x020003F5 RID: 1013
	public class ReceivedInvitationRemovedNotification : IProtoBuf
	{
		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x0600432E RID: 17198 RVA: 0x000D4D2F File Offset: 0x000D2F2F
		// (set) Token: 0x0600432F RID: 17199 RVA: 0x000D4D37 File Offset: 0x000D2F37
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

		// Token: 0x06004330 RID: 17200 RVA: 0x000D4D47 File Offset: 0x000D2F47
		public void SetAgentAccountId(ulong val)
		{
			this.AgentAccountId = val;
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06004331 RID: 17201 RVA: 0x000D4D50 File Offset: 0x000D2F50
		// (set) Token: 0x06004332 RID: 17202 RVA: 0x000D4D58 File Offset: 0x000D2F58
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

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06004333 RID: 17203 RVA: 0x000D4D50 File Offset: 0x000D2F50
		public List<RemovedInvitationAssignment> AssignmentList
		{
			get
			{
				return this._Assignment;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06004334 RID: 17204 RVA: 0x000D4D61 File Offset: 0x000D2F61
		public int AssignmentCount
		{
			get
			{
				return this._Assignment.Count;
			}
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x000D4D6E File Offset: 0x000D2F6E
		public void AddAssignment(RemovedInvitationAssignment val)
		{
			this._Assignment.Add(val);
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x000D4D7C File Offset: 0x000D2F7C
		public void ClearAssignment()
		{
			this._Assignment.Clear();
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x000D4D89 File Offset: 0x000D2F89
		public void SetAssignment(List<RemovedInvitationAssignment> val)
		{
			this.Assignment = val;
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x000D4D94 File Offset: 0x000D2F94
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

		// Token: 0x06004339 RID: 17209 RVA: 0x000D4E10 File Offset: 0x000D3010
		public override bool Equals(object obj)
		{
			ReceivedInvitationRemovedNotification receivedInvitationRemovedNotification = obj as ReceivedInvitationRemovedNotification;
			if (receivedInvitationRemovedNotification == null)
			{
				return false;
			}
			if (this.HasAgentAccountId != receivedInvitationRemovedNotification.HasAgentAccountId || (this.HasAgentAccountId && !this.AgentAccountId.Equals(receivedInvitationRemovedNotification.AgentAccountId)))
			{
				return false;
			}
			if (this.Assignment.Count != receivedInvitationRemovedNotification.Assignment.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Assignment.Count; i++)
			{
				if (!this.Assignment[i].Equals(receivedInvitationRemovedNotification.Assignment[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x0600433A RID: 17210 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x000D4EA9 File Offset: 0x000D30A9
		public static ReceivedInvitationRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReceivedInvitationRemovedNotification>(bs, 0, -1);
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x000D4EB3 File Offset: 0x000D30B3
		public void Deserialize(Stream stream)
		{
			ReceivedInvitationRemovedNotification.Deserialize(stream, this);
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x000D4EBD File Offset: 0x000D30BD
		public static ReceivedInvitationRemovedNotification Deserialize(Stream stream, ReceivedInvitationRemovedNotification instance)
		{
			return ReceivedInvitationRemovedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x000D4EC8 File Offset: 0x000D30C8
		public static ReceivedInvitationRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			ReceivedInvitationRemovedNotification receivedInvitationRemovedNotification = new ReceivedInvitationRemovedNotification();
			ReceivedInvitationRemovedNotification.DeserializeLengthDelimited(stream, receivedInvitationRemovedNotification);
			return receivedInvitationRemovedNotification;
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x000D4EE4 File Offset: 0x000D30E4
		public static ReceivedInvitationRemovedNotification DeserializeLengthDelimited(Stream stream, ReceivedInvitationRemovedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ReceivedInvitationRemovedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x000D4F0C File Offset: 0x000D310C
		public static ReceivedInvitationRemovedNotification Deserialize(Stream stream, ReceivedInvitationRemovedNotification instance, long limit)
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

		// Token: 0x06004341 RID: 17217 RVA: 0x000D4FBB File Offset: 0x000D31BB
		public void Serialize(Stream stream)
		{
			ReceivedInvitationRemovedNotification.Serialize(stream, this);
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x000D4FC4 File Offset: 0x000D31C4
		public static void Serialize(Stream stream, ReceivedInvitationRemovedNotification instance)
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

		// Token: 0x06004343 RID: 17219 RVA: 0x000D5054 File Offset: 0x000D3254
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

		// Token: 0x040016FC RID: 5884
		public bool HasAgentAccountId;

		// Token: 0x040016FD RID: 5885
		private ulong _AgentAccountId;

		// Token: 0x040016FE RID: 5886
		private List<RemovedInvitationAssignment> _Assignment = new List<RemovedInvitationAssignment>();
	}
}
