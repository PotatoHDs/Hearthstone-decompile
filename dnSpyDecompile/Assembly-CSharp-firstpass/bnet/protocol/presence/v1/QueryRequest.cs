using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x02000339 RID: 825
	public class QueryRequest : IProtoBuf
	{
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060032F2 RID: 13042 RVA: 0x000AA38F File Offset: 0x000A858F
		// (set) Token: 0x060032F3 RID: 13043 RVA: 0x000AA397 File Offset: 0x000A8597
		public EntityId EntityId { get; set; }

		// Token: 0x060032F4 RID: 13044 RVA: 0x000AA3A0 File Offset: 0x000A85A0
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x000AA3A9 File Offset: 0x000A85A9
		// (set) Token: 0x060032F6 RID: 13046 RVA: 0x000AA3B1 File Offset: 0x000A85B1
		public List<FieldKey> Key
		{
			get
			{
				return this._Key;
			}
			set
			{
				this._Key = value;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060032F7 RID: 13047 RVA: 0x000AA3A9 File Offset: 0x000A85A9
		public List<FieldKey> KeyList
		{
			get
			{
				return this._Key;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x000AA3BA File Offset: 0x000A85BA
		public int KeyCount
		{
			get
			{
				return this._Key.Count;
			}
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x000AA3C7 File Offset: 0x000A85C7
		public void AddKey(FieldKey val)
		{
			this._Key.Add(val);
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000AA3D5 File Offset: 0x000A85D5
		public void ClearKey()
		{
			this._Key.Clear();
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000AA3E2 File Offset: 0x000A85E2
		public void SetKey(List<FieldKey> val)
		{
			this.Key = val;
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x000AA3EB File Offset: 0x000A85EB
		// (set) Token: 0x060032FD RID: 13053 RVA: 0x000AA3F3 File Offset: 0x000A85F3
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

		// Token: 0x060032FE RID: 13054 RVA: 0x000AA406 File Offset: 0x000A8606
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000AA410 File Offset: 0x000A8610
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.EntityId.GetHashCode();
			foreach (FieldKey fieldKey in this.Key)
			{
				num ^= fieldKey.GetHashCode();
			}
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000AA498 File Offset: 0x000A8698
		public override bool Equals(object obj)
		{
			QueryRequest queryRequest = obj as QueryRequest;
			if (queryRequest == null)
			{
				return false;
			}
			if (!this.EntityId.Equals(queryRequest.EntityId))
			{
				return false;
			}
			if (this.Key.Count != queryRequest.Key.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Key.Count; i++)
			{
				if (!this.Key[i].Equals(queryRequest.Key[i]))
				{
					return false;
				}
			}
			return this.HasAgentId == queryRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(queryRequest.AgentId));
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000AA543 File Offset: 0x000A8743
		public static QueryRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueryRequest>(bs, 0, -1);
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000AA54D File Offset: 0x000A874D
		public void Deserialize(Stream stream)
		{
			QueryRequest.Deserialize(stream, this);
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000AA557 File Offset: 0x000A8757
		public static QueryRequest Deserialize(Stream stream, QueryRequest instance)
		{
			return QueryRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000AA564 File Offset: 0x000A8764
		public static QueryRequest DeserializeLengthDelimited(Stream stream)
		{
			QueryRequest queryRequest = new QueryRequest();
			QueryRequest.DeserializeLengthDelimited(stream, queryRequest);
			return queryRequest;
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000AA580 File Offset: 0x000A8780
		public static QueryRequest DeserializeLengthDelimited(Stream stream, QueryRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueryRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000AA5A8 File Offset: 0x000A87A8
		public static QueryRequest Deserialize(Stream stream, QueryRequest instance, long limit)
		{
			if (instance.Key == null)
			{
				instance.Key = new List<FieldKey>();
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
						else if (instance.AgentId == null)
						{
							instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
						}
					}
					else
					{
						instance.Key.Add(FieldKey.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.EntityId == null)
				{
					instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000AA6A8 File Offset: 0x000A88A8
		public void Serialize(Stream stream)
		{
			QueryRequest.Serialize(stream, this);
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000AA6B4 File Offset: 0x000A88B4
		public static void Serialize(Stream stream, QueryRequest instance)
		{
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
			if (instance.Key.Count > 0)
			{
				foreach (FieldKey fieldKey in instance.Key)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, fieldKey.GetSerializedSize());
					FieldKey.Serialize(stream, fieldKey);
				}
			}
			if (instance.HasAgentId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000AA794 File Offset: 0x000A8994
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.EntityId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.Key.Count > 0)
			{
				foreach (FieldKey fieldKey in this.Key)
				{
					num += 1U;
					uint serializedSize2 = fieldKey.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize3 = this.AgentId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			num += 1U;
			return num;
		}

		// Token: 0x040013EF RID: 5103
		private List<FieldKey> _Key = new List<FieldKey>();

		// Token: 0x040013F0 RID: 5104
		public bool HasAgentId;

		// Token: 0x040013F1 RID: 5105
		private EntityId _AgentId;
	}
}
