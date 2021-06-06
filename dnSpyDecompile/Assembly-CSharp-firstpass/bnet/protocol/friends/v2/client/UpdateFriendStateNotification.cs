using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x020003F8 RID: 1016
	public class UpdateFriendStateNotification : IProtoBuf
	{
		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06004373 RID: 17267 RVA: 0x000D587B File Offset: 0x000D3A7B
		// (set) Token: 0x06004374 RID: 17268 RVA: 0x000D5883 File Offset: 0x000D3A83
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

		// Token: 0x06004375 RID: 17269 RVA: 0x000D5893 File Offset: 0x000D3A93
		public void SetAgentAccountId(ulong val)
		{
			this.AgentAccountId = val;
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06004376 RID: 17270 RVA: 0x000D589C File Offset: 0x000D3A9C
		// (set) Token: 0x06004377 RID: 17271 RVA: 0x000D58A4 File Offset: 0x000D3AA4
		public List<FriendStateAssignment> Assignment
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

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06004378 RID: 17272 RVA: 0x000D589C File Offset: 0x000D3A9C
		public List<FriendStateAssignment> AssignmentList
		{
			get
			{
				return this._Assignment;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06004379 RID: 17273 RVA: 0x000D58AD File Offset: 0x000D3AAD
		public int AssignmentCount
		{
			get
			{
				return this._Assignment.Count;
			}
		}

		// Token: 0x0600437A RID: 17274 RVA: 0x000D58BA File Offset: 0x000D3ABA
		public void AddAssignment(FriendStateAssignment val)
		{
			this._Assignment.Add(val);
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x000D58C8 File Offset: 0x000D3AC8
		public void ClearAssignment()
		{
			this._Assignment.Clear();
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x000D58D5 File Offset: 0x000D3AD5
		public void SetAssignment(List<FriendStateAssignment> val)
		{
			this.Assignment = val;
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x000D58E0 File Offset: 0x000D3AE0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentAccountId)
			{
				num ^= this.AgentAccountId.GetHashCode();
			}
			foreach (FriendStateAssignment friendStateAssignment in this.Assignment)
			{
				num ^= friendStateAssignment.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x000D595C File Offset: 0x000D3B5C
		public override bool Equals(object obj)
		{
			UpdateFriendStateNotification updateFriendStateNotification = obj as UpdateFriendStateNotification;
			if (updateFriendStateNotification == null)
			{
				return false;
			}
			if (this.HasAgentAccountId != updateFriendStateNotification.HasAgentAccountId || (this.HasAgentAccountId && !this.AgentAccountId.Equals(updateFriendStateNotification.AgentAccountId)))
			{
				return false;
			}
			if (this.Assignment.Count != updateFriendStateNotification.Assignment.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Assignment.Count; i++)
			{
				if (!this.Assignment[i].Equals(updateFriendStateNotification.Assignment[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x0600437F RID: 17279 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x000D59F5 File Offset: 0x000D3BF5
		public static UpdateFriendStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateFriendStateNotification>(bs, 0, -1);
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x000D59FF File Offset: 0x000D3BFF
		public void Deserialize(Stream stream)
		{
			UpdateFriendStateNotification.Deserialize(stream, this);
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x000D5A09 File Offset: 0x000D3C09
		public static UpdateFriendStateNotification Deserialize(Stream stream, UpdateFriendStateNotification instance)
		{
			return UpdateFriendStateNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x000D5A14 File Offset: 0x000D3C14
		public static UpdateFriendStateNotification DeserializeLengthDelimited(Stream stream)
		{
			UpdateFriendStateNotification updateFriendStateNotification = new UpdateFriendStateNotification();
			UpdateFriendStateNotification.DeserializeLengthDelimited(stream, updateFriendStateNotification);
			return updateFriendStateNotification;
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x000D5A30 File Offset: 0x000D3C30
		public static UpdateFriendStateNotification DeserializeLengthDelimited(Stream stream, UpdateFriendStateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateFriendStateNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x000D5A58 File Offset: 0x000D3C58
		public static UpdateFriendStateNotification Deserialize(Stream stream, UpdateFriendStateNotification instance, long limit)
		{
			if (instance.Assignment == null)
			{
				instance.Assignment = new List<FriendStateAssignment>();
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
						instance.Assignment.Add(FriendStateAssignment.DeserializeLengthDelimited(stream));
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

		// Token: 0x06004386 RID: 17286 RVA: 0x000D5B07 File Offset: 0x000D3D07
		public void Serialize(Stream stream)
		{
			UpdateFriendStateNotification.Serialize(stream, this);
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x000D5B10 File Offset: 0x000D3D10
		public static void Serialize(Stream stream, UpdateFriendStateNotification instance)
		{
			if (instance.HasAgentAccountId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.AgentAccountId);
			}
			if (instance.Assignment.Count > 0)
			{
				foreach (FriendStateAssignment friendStateAssignment in instance.Assignment)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, friendStateAssignment.GetSerializedSize());
					FriendStateAssignment.Serialize(stream, friendStateAssignment);
				}
			}
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x000D5BA0 File Offset: 0x000D3DA0
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
				foreach (FriendStateAssignment friendStateAssignment in this.Assignment)
				{
					num += 1U;
					uint serializedSize = friendStateAssignment.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001705 RID: 5893
		public bool HasAgentAccountId;

		// Token: 0x04001706 RID: 5894
		private ulong _AgentAccountId;

		// Token: 0x04001707 RID: 5895
		private List<FriendStateAssignment> _Assignment = new List<FriendStateAssignment>();
	}
}
