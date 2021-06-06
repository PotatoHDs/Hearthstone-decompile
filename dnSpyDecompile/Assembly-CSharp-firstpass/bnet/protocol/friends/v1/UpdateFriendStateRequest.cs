using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000425 RID: 1061
	public class UpdateFriendStateRequest : IProtoBuf
	{
		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x060046FF RID: 18175 RVA: 0x000DE2FF File Offset: 0x000DC4FF
		// (set) Token: 0x06004700 RID: 18176 RVA: 0x000DE307 File Offset: 0x000DC507
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

		// Token: 0x06004701 RID: 18177 RVA: 0x000DE31A File Offset: 0x000DC51A
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06004702 RID: 18178 RVA: 0x000DE323 File Offset: 0x000DC523
		// (set) Token: 0x06004703 RID: 18179 RVA: 0x000DE32B File Offset: 0x000DC52B
		public EntityId TargetId { get; set; }

		// Token: 0x06004704 RID: 18180 RVA: 0x000DE334 File Offset: 0x000DC534
		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06004705 RID: 18181 RVA: 0x000DE33D File Offset: 0x000DC53D
		// (set) Token: 0x06004706 RID: 18182 RVA: 0x000DE345 File Offset: 0x000DC545
		public List<Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06004707 RID: 18183 RVA: 0x000DE33D File Offset: 0x000DC53D
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06004708 RID: 18184 RVA: 0x000DE34E File Offset: 0x000DC54E
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x000DE35B File Offset: 0x000DC55B
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x000DE369 File Offset: 0x000DC569
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x000DE376 File Offset: 0x000DC576
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x000DE380 File Offset: 0x000DC580
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.TargetId.GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x000DE408 File Offset: 0x000DC608
		public override bool Equals(object obj)
		{
			UpdateFriendStateRequest updateFriendStateRequest = obj as UpdateFriendStateRequest;
			if (updateFriendStateRequest == null)
			{
				return false;
			}
			if (this.HasAgentId != updateFriendStateRequest.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(updateFriendStateRequest.AgentId)))
			{
				return false;
			}
			if (!this.TargetId.Equals(updateFriendStateRequest.TargetId))
			{
				return false;
			}
			if (this.Attribute.Count != updateFriendStateRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(updateFriendStateRequest.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x0600470E RID: 18190 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x000DE4B3 File Offset: 0x000DC6B3
		public static UpdateFriendStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateFriendStateRequest>(bs, 0, -1);
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x000DE4BD File Offset: 0x000DC6BD
		public void Deserialize(Stream stream)
		{
			UpdateFriendStateRequest.Deserialize(stream, this);
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x000DE4C7 File Offset: 0x000DC6C7
		public static UpdateFriendStateRequest Deserialize(Stream stream, UpdateFriendStateRequest instance)
		{
			return UpdateFriendStateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x000DE4D4 File Offset: 0x000DC6D4
		public static UpdateFriendStateRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateFriendStateRequest updateFriendStateRequest = new UpdateFriendStateRequest();
			UpdateFriendStateRequest.DeserializeLengthDelimited(stream, updateFriendStateRequest);
			return updateFriendStateRequest;
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x000DE4F0 File Offset: 0x000DC6F0
		public static UpdateFriendStateRequest DeserializeLengthDelimited(Stream stream, UpdateFriendStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateFriendStateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x000DE518 File Offset: 0x000DC718
		public static UpdateFriendStateRequest Deserialize(Stream stream, UpdateFriendStateRequest instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
							instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
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

		// Token: 0x06004715 RID: 18197 RVA: 0x000DE618 File Offset: 0x000DC818
		public void Serialize(Stream stream)
		{
			UpdateFriendStateRequest.Serialize(stream, this);
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x000DE624 File Offset: 0x000DC824
		public static void Serialize(Stream stream, UpdateFriendStateRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.TargetId == null)
			{
				throw new ArgumentNullException("TargetId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
			EntityId.Serialize(stream, instance.TargetId);
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x000DE704 File Offset: 0x000DC904
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.TargetId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize3 = attribute.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x040017B4 RID: 6068
		public bool HasAgentId;

		// Token: 0x040017B5 RID: 6069
		private EntityId _AgentId;

		// Token: 0x040017B7 RID: 6071
		private List<Attribute> _Attribute = new List<Attribute>();
	}
}
