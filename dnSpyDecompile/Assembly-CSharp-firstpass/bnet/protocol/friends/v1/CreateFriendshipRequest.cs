using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000428 RID: 1064
	public class CreateFriendshipRequest : IProtoBuf
	{
		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x0600473D RID: 18237 RVA: 0x000DECEB File Offset: 0x000DCEEB
		// (set) Token: 0x0600473E RID: 18238 RVA: 0x000DECF3 File Offset: 0x000DCEF3
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

		// Token: 0x0600473F RID: 18239 RVA: 0x000DED06 File Offset: 0x000DCF06
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06004740 RID: 18240 RVA: 0x000DED0F File Offset: 0x000DCF0F
		// (set) Token: 0x06004741 RID: 18241 RVA: 0x000DED17 File Offset: 0x000DCF17
		public EntityId TargetId
		{
			get
			{
				return this._TargetId;
			}
			set
			{
				this._TargetId = value;
				this.HasTargetId = (value != null);
			}
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x000DED2A File Offset: 0x000DCF2A
		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06004743 RID: 18243 RVA: 0x000DED33 File Offset: 0x000DCF33
		// (set) Token: 0x06004744 RID: 18244 RVA: 0x000DED3B File Offset: 0x000DCF3B
		public List<uint> Role
		{
			get
			{
				return this._Role;
			}
			set
			{
				this._Role = value;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06004745 RID: 18245 RVA: 0x000DED33 File Offset: 0x000DCF33
		public List<uint> RoleList
		{
			get
			{
				return this._Role;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06004746 RID: 18246 RVA: 0x000DED44 File Offset: 0x000DCF44
		public int RoleCount
		{
			get
			{
				return this._Role.Count;
			}
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x000DED51 File Offset: 0x000DCF51
		public void AddRole(uint val)
		{
			this._Role.Add(val);
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x000DED5F File Offset: 0x000DCF5F
		public void ClearRole()
		{
			this._Role.Clear();
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x000DED6C File Offset: 0x000DCF6C
		public void SetRole(List<uint> val)
		{
			this.Role = val;
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x000DED78 File Offset: 0x000DCF78
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasTargetId)
			{
				num ^= this.TargetId.GetHashCode();
			}
			foreach (uint num2 in this.Role)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x000DEE08 File Offset: 0x000DD008
		public override bool Equals(object obj)
		{
			CreateFriendshipRequest createFriendshipRequest = obj as CreateFriendshipRequest;
			if (createFriendshipRequest == null)
			{
				return false;
			}
			if (this.HasAgentId != createFriendshipRequest.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(createFriendshipRequest.AgentId)))
			{
				return false;
			}
			if (this.HasTargetId != createFriendshipRequest.HasTargetId || (this.HasTargetId && !this.TargetId.Equals(createFriendshipRequest.TargetId)))
			{
				return false;
			}
			if (this.Role.Count != createFriendshipRequest.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Role.Count; i++)
			{
				if (!this.Role[i].Equals(createFriendshipRequest.Role[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x0600474C RID: 18252 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x000DEECC File Offset: 0x000DD0CC
		public static CreateFriendshipRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateFriendshipRequest>(bs, 0, -1);
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x000DEED6 File Offset: 0x000DD0D6
		public void Deserialize(Stream stream)
		{
			CreateFriendshipRequest.Deserialize(stream, this);
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x000DEEE0 File Offset: 0x000DD0E0
		public static CreateFriendshipRequest Deserialize(Stream stream, CreateFriendshipRequest instance)
		{
			return CreateFriendshipRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x000DEEEC File Offset: 0x000DD0EC
		public static CreateFriendshipRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateFriendshipRequest createFriendshipRequest = new CreateFriendshipRequest();
			CreateFriendshipRequest.DeserializeLengthDelimited(stream, createFriendshipRequest);
			return createFriendshipRequest;
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x000DEF08 File Offset: 0x000DD108
		public static CreateFriendshipRequest DeserializeLengthDelimited(Stream stream, CreateFriendshipRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateFriendshipRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x000DEF30 File Offset: 0x000DD130
		public static CreateFriendshipRequest Deserialize(Stream stream, CreateFriendshipRequest instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
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
								instance.Role.Add(ProtocolParser.ReadUInt32(stream));
							}
							if (stream.Position != num2)
							{
								throw new ProtocolBufferException("Read too many bytes in packed data");
							}
						}
					}
					else if (instance.TargetId == null)
					{
						instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
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

		// Token: 0x06004753 RID: 18259 RVA: 0x000DF061 File Offset: 0x000DD261
		public void Serialize(Stream stream)
		{
			CreateFriendshipRequest.Serialize(stream, this);
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x000DF06C File Offset: 0x000DD26C
		public static void Serialize(Stream stream, CreateFriendshipRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasTargetId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				EntityId.Serialize(stream, instance.TargetId);
			}
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(26);
				uint num = 0U;
				foreach (uint val in instance.Role)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.Role)
				{
					ProtocolParser.WriteUInt32(stream, val2);
				}
			}
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x000DF17C File Offset: 0x000DD37C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasTargetId)
			{
				num += 1U;
				uint serializedSize2 = this.TargetId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.Role.Count > 0)
			{
				num += 1U;
				uint num2 = num;
				foreach (uint val in this.Role)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			return num;
		}

		// Token: 0x040017BB RID: 6075
		public bool HasAgentId;

		// Token: 0x040017BC RID: 6076
		private EntityId _AgentId;

		// Token: 0x040017BD RID: 6077
		public bool HasTargetId;

		// Token: 0x040017BE RID: 6078
		private EntityId _TargetId;

		// Token: 0x040017BF RID: 6079
		private List<uint> _Role = new List<uint>();
	}
}
