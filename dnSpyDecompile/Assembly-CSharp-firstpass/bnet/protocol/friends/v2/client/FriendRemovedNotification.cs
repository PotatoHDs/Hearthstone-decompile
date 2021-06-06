using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x020003F3 RID: 1011
	public class FriendRemovedNotification : IProtoBuf
	{
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06004300 RID: 17152 RVA: 0x000D45A7 File Offset: 0x000D27A7
		// (set) Token: 0x06004301 RID: 17153 RVA: 0x000D45AF File Offset: 0x000D27AF
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

		// Token: 0x06004302 RID: 17154 RVA: 0x000D45BF File Offset: 0x000D27BF
		public void SetAgentAccountId(ulong val)
		{
			this.AgentAccountId = val;
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x000D45C8 File Offset: 0x000D27C8
		// (set) Token: 0x06004304 RID: 17156 RVA: 0x000D45D0 File Offset: 0x000D27D0
		public List<RemovedFriendAssignment> Assignment
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

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06004305 RID: 17157 RVA: 0x000D45C8 File Offset: 0x000D27C8
		public List<RemovedFriendAssignment> AssignmentList
		{
			get
			{
				return this._Assignment;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06004306 RID: 17158 RVA: 0x000D45D9 File Offset: 0x000D27D9
		public int AssignmentCount
		{
			get
			{
				return this._Assignment.Count;
			}
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x000D45E6 File Offset: 0x000D27E6
		public void AddAssignment(RemovedFriendAssignment val)
		{
			this._Assignment.Add(val);
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x000D45F4 File Offset: 0x000D27F4
		public void ClearAssignment()
		{
			this._Assignment.Clear();
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x000D4601 File Offset: 0x000D2801
		public void SetAssignment(List<RemovedFriendAssignment> val)
		{
			this.Assignment = val;
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x000D460C File Offset: 0x000D280C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentAccountId)
			{
				num ^= this.AgentAccountId.GetHashCode();
			}
			foreach (RemovedFriendAssignment removedFriendAssignment in this.Assignment)
			{
				num ^= removedFriendAssignment.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x000D4688 File Offset: 0x000D2888
		public override bool Equals(object obj)
		{
			FriendRemovedNotification friendRemovedNotification = obj as FriendRemovedNotification;
			if (friendRemovedNotification == null)
			{
				return false;
			}
			if (this.HasAgentAccountId != friendRemovedNotification.HasAgentAccountId || (this.HasAgentAccountId && !this.AgentAccountId.Equals(friendRemovedNotification.AgentAccountId)))
			{
				return false;
			}
			if (this.Assignment.Count != friendRemovedNotification.Assignment.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Assignment.Count; i++)
			{
				if (!this.Assignment[i].Equals(friendRemovedNotification.Assignment[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x0600430C RID: 17164 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x000D4721 File Offset: 0x000D2921
		public static FriendRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendRemovedNotification>(bs, 0, -1);
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x000D472B File Offset: 0x000D292B
		public void Deserialize(Stream stream)
		{
			FriendRemovedNotification.Deserialize(stream, this);
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x000D4735 File Offset: 0x000D2935
		public static FriendRemovedNotification Deserialize(Stream stream, FriendRemovedNotification instance)
		{
			return FriendRemovedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004310 RID: 17168 RVA: 0x000D4740 File Offset: 0x000D2940
		public static FriendRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			FriendRemovedNotification friendRemovedNotification = new FriendRemovedNotification();
			FriendRemovedNotification.DeserializeLengthDelimited(stream, friendRemovedNotification);
			return friendRemovedNotification;
		}

		// Token: 0x06004311 RID: 17169 RVA: 0x000D475C File Offset: 0x000D295C
		public static FriendRemovedNotification DeserializeLengthDelimited(Stream stream, FriendRemovedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FriendRemovedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x000D4784 File Offset: 0x000D2984
		public static FriendRemovedNotification Deserialize(Stream stream, FriendRemovedNotification instance, long limit)
		{
			if (instance.Assignment == null)
			{
				instance.Assignment = new List<RemovedFriendAssignment>();
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
						instance.Assignment.Add(RemovedFriendAssignment.DeserializeLengthDelimited(stream));
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

		// Token: 0x06004313 RID: 17171 RVA: 0x000D4833 File Offset: 0x000D2A33
		public void Serialize(Stream stream)
		{
			FriendRemovedNotification.Serialize(stream, this);
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x000D483C File Offset: 0x000D2A3C
		public static void Serialize(Stream stream, FriendRemovedNotification instance)
		{
			if (instance.HasAgentAccountId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.AgentAccountId);
			}
			if (instance.Assignment.Count > 0)
			{
				foreach (RemovedFriendAssignment removedFriendAssignment in instance.Assignment)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, removedFriendAssignment.GetSerializedSize());
					RemovedFriendAssignment.Serialize(stream, removedFriendAssignment);
				}
			}
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x000D48CC File Offset: 0x000D2ACC
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
				foreach (RemovedFriendAssignment removedFriendAssignment in this.Assignment)
				{
					num += 1U;
					uint serializedSize = removedFriendAssignment.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040016F6 RID: 5878
		public bool HasAgentAccountId;

		// Token: 0x040016F7 RID: 5879
		private ulong _AgentAccountId;

		// Token: 0x040016F8 RID: 5880
		private List<RemovedFriendAssignment> _Assignment = new List<RemovedFriendAssignment>();
	}
}
