using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x02000338 RID: 824
	public class UpdateRequest : IProtoBuf
	{
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060032D5 RID: 13013 RVA: 0x000A9E0C File Offset: 0x000A800C
		// (set) Token: 0x060032D6 RID: 13014 RVA: 0x000A9E14 File Offset: 0x000A8014
		public EntityId EntityId { get; set; }

		// Token: 0x060032D7 RID: 13015 RVA: 0x000A9E1D File Offset: 0x000A801D
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x000A9E26 File Offset: 0x000A8026
		// (set) Token: 0x060032D9 RID: 13017 RVA: 0x000A9E2E File Offset: 0x000A802E
		public List<FieldOperation> FieldOperation
		{
			get
			{
				return this._FieldOperation;
			}
			set
			{
				this._FieldOperation = value;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x000A9E26 File Offset: 0x000A8026
		public List<FieldOperation> FieldOperationList
		{
			get
			{
				return this._FieldOperation;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060032DB RID: 13019 RVA: 0x000A9E37 File Offset: 0x000A8037
		public int FieldOperationCount
		{
			get
			{
				return this._FieldOperation.Count;
			}
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000A9E44 File Offset: 0x000A8044
		public void AddFieldOperation(FieldOperation val)
		{
			this._FieldOperation.Add(val);
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000A9E52 File Offset: 0x000A8052
		public void ClearFieldOperation()
		{
			this._FieldOperation.Clear();
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000A9E5F File Offset: 0x000A805F
		public void SetFieldOperation(List<FieldOperation> val)
		{
			this.FieldOperation = val;
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x000A9E68 File Offset: 0x000A8068
		// (set) Token: 0x060032E0 RID: 13024 RVA: 0x000A9E70 File Offset: 0x000A8070
		public bool NoCreate
		{
			get
			{
				return this._NoCreate;
			}
			set
			{
				this._NoCreate = value;
				this.HasNoCreate = true;
			}
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000A9E80 File Offset: 0x000A8080
		public void SetNoCreate(bool val)
		{
			this.NoCreate = val;
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060032E2 RID: 13026 RVA: 0x000A9E89 File Offset: 0x000A8089
		// (set) Token: 0x060032E3 RID: 13027 RVA: 0x000A9E91 File Offset: 0x000A8091
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

		// Token: 0x060032E4 RID: 13028 RVA: 0x000A9EA4 File Offset: 0x000A80A4
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000A9EB0 File Offset: 0x000A80B0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.EntityId.GetHashCode();
			foreach (FieldOperation fieldOperation in this.FieldOperation)
			{
				num ^= fieldOperation.GetHashCode();
			}
			if (this.HasNoCreate)
			{
				num ^= this.NoCreate.GetHashCode();
			}
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000A9F50 File Offset: 0x000A8150
		public override bool Equals(object obj)
		{
			UpdateRequest updateRequest = obj as UpdateRequest;
			if (updateRequest == null)
			{
				return false;
			}
			if (!this.EntityId.Equals(updateRequest.EntityId))
			{
				return false;
			}
			if (this.FieldOperation.Count != updateRequest.FieldOperation.Count)
			{
				return false;
			}
			for (int i = 0; i < this.FieldOperation.Count; i++)
			{
				if (!this.FieldOperation[i].Equals(updateRequest.FieldOperation[i]))
				{
					return false;
				}
			}
			return this.HasNoCreate == updateRequest.HasNoCreate && (!this.HasNoCreate || this.NoCreate.Equals(updateRequest.NoCreate)) && this.HasAgentId == updateRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(updateRequest.AgentId));
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060032E7 RID: 13031 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000AA029 File Offset: 0x000A8229
		public static UpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateRequest>(bs, 0, -1);
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000AA033 File Offset: 0x000A8233
		public void Deserialize(Stream stream)
		{
			UpdateRequest.Deserialize(stream, this);
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000AA03D File Offset: 0x000A823D
		public static UpdateRequest Deserialize(Stream stream, UpdateRequest instance)
		{
			return UpdateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000AA048 File Offset: 0x000A8248
		public static UpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			UpdateRequest.DeserializeLengthDelimited(stream, updateRequest);
			return updateRequest;
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000AA064 File Offset: 0x000A8264
		public static UpdateRequest DeserializeLengthDelimited(Stream stream, UpdateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000AA08C File Offset: 0x000A828C
		public static UpdateRequest Deserialize(Stream stream, UpdateRequest instance, long limit)
		{
			if (instance.FieldOperation == null)
			{
				instance.FieldOperation = new List<FieldOperation>();
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
								instance.FieldOperation.Add(bnet.protocol.presence.v1.FieldOperation.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
						{
							if (instance.EntityId == null)
							{
								instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.NoCreate = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 34)
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

		// Token: 0x060032EE RID: 13038 RVA: 0x000AA1AF File Offset: 0x000A83AF
		public void Serialize(Stream stream)
		{
			UpdateRequest.Serialize(stream, this);
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000AA1B8 File Offset: 0x000A83B8
		public static void Serialize(Stream stream, UpdateRequest instance)
		{
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
			if (instance.FieldOperation.Count > 0)
			{
				foreach (FieldOperation fieldOperation in instance.FieldOperation)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, fieldOperation.GetSerializedSize());
					bnet.protocol.presence.v1.FieldOperation.Serialize(stream, fieldOperation);
				}
			}
			if (instance.HasNoCreate)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.NoCreate);
			}
			if (instance.HasAgentId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000AA2B4 File Offset: 0x000A84B4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.EntityId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.FieldOperation.Count > 0)
			{
				foreach (FieldOperation fieldOperation in this.FieldOperation)
				{
					num += 1U;
					uint serializedSize2 = fieldOperation.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasNoCreate)
			{
				num += 1U;
				num += 1U;
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

		// Token: 0x040013E9 RID: 5097
		private List<FieldOperation> _FieldOperation = new List<FieldOperation>();

		// Token: 0x040013EA RID: 5098
		public bool HasNoCreate;

		// Token: 0x040013EB RID: 5099
		private bool _NoCreate;

		// Token: 0x040013EC RID: 5100
		public bool HasAgentId;

		// Token: 0x040013ED RID: 5101
		private EntityId _AgentId;
	}
}
