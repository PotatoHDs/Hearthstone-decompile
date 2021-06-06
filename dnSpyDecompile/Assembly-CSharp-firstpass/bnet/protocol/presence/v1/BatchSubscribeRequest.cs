using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x0200033C RID: 828
	public class BatchSubscribeRequest : IProtoBuf
	{
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06003333 RID: 13107 RVA: 0x000AAE1B File Offset: 0x000A901B
		// (set) Token: 0x06003334 RID: 13108 RVA: 0x000AAE23 File Offset: 0x000A9023
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

		// Token: 0x06003335 RID: 13109 RVA: 0x000AAE36 File Offset: 0x000A9036
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x000AAE3F File Offset: 0x000A903F
		// (set) Token: 0x06003337 RID: 13111 RVA: 0x000AAE47 File Offset: 0x000A9047
		public List<EntityId> EntityId
		{
			get
			{
				return this._EntityId;
			}
			set
			{
				this._EntityId = value;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x000AAE3F File Offset: 0x000A903F
		public List<EntityId> EntityIdList
		{
			get
			{
				return this._EntityId;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06003339 RID: 13113 RVA: 0x000AAE50 File Offset: 0x000A9050
		public int EntityIdCount
		{
			get
			{
				return this._EntityId.Count;
			}
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000AAE5D File Offset: 0x000A905D
		public void AddEntityId(EntityId val)
		{
			this._EntityId.Add(val);
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x000AAE6B File Offset: 0x000A906B
		public void ClearEntityId()
		{
			this._EntityId.Clear();
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000AAE78 File Offset: 0x000A9078
		public void SetEntityId(List<EntityId> val)
		{
			this.EntityId = val;
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x000AAE81 File Offset: 0x000A9081
		// (set) Token: 0x0600333E RID: 13118 RVA: 0x000AAE89 File Offset: 0x000A9089
		public List<uint> Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000AAE81 File Offset: 0x000A9081
		public List<uint> ProgramList
		{
			get
			{
				return this._Program;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x000AAE92 File Offset: 0x000A9092
		public int ProgramCount
		{
			get
			{
				return this._Program.Count;
			}
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000AAE9F File Offset: 0x000A909F
		public void AddProgram(uint val)
		{
			this._Program.Add(val);
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000AAEAD File Offset: 0x000A90AD
		public void ClearProgram()
		{
			this._Program.Clear();
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000AAEBA File Offset: 0x000A90BA
		public void SetProgram(List<uint> val)
		{
			this.Program = val;
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06003344 RID: 13124 RVA: 0x000AAEC3 File Offset: 0x000A90C3
		// (set) Token: 0x06003345 RID: 13125 RVA: 0x000AAECB File Offset: 0x000A90CB
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

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06003346 RID: 13126 RVA: 0x000AAEC3 File Offset: 0x000A90C3
		public List<FieldKey> KeyList
		{
			get
			{
				return this._Key;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x000AAED4 File Offset: 0x000A90D4
		public int KeyCount
		{
			get
			{
				return this._Key.Count;
			}
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000AAEE1 File Offset: 0x000A90E1
		public void AddKey(FieldKey val)
		{
			this._Key.Add(val);
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000AAEEF File Offset: 0x000A90EF
		public void ClearKey()
		{
			this._Key.Clear();
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000AAEFC File Offset: 0x000A90FC
		public void SetKey(List<FieldKey> val)
		{
			this.Key = val;
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x0600334B RID: 13131 RVA: 0x000AAF05 File Offset: 0x000A9105
		// (set) Token: 0x0600334C RID: 13132 RVA: 0x000AAF0D File Offset: 0x000A910D
		public ulong ObjectId
		{
			get
			{
				return this._ObjectId;
			}
			set
			{
				this._ObjectId = value;
				this.HasObjectId = true;
			}
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000AAF1D File Offset: 0x000A911D
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000AAF28 File Offset: 0x000A9128
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			foreach (EntityId entityId in this.EntityId)
			{
				num ^= entityId.GetHashCode();
			}
			foreach (uint num2 in this.Program)
			{
				num ^= num2.GetHashCode();
			}
			foreach (FieldKey fieldKey in this.Key)
			{
				num ^= fieldKey.GetHashCode();
			}
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000AB048 File Offset: 0x000A9248
		public override bool Equals(object obj)
		{
			BatchSubscribeRequest batchSubscribeRequest = obj as BatchSubscribeRequest;
			if (batchSubscribeRequest == null)
			{
				return false;
			}
			if (this.HasAgentId != batchSubscribeRequest.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(batchSubscribeRequest.AgentId)))
			{
				return false;
			}
			if (this.EntityId.Count != batchSubscribeRequest.EntityId.Count)
			{
				return false;
			}
			for (int i = 0; i < this.EntityId.Count; i++)
			{
				if (!this.EntityId[i].Equals(batchSubscribeRequest.EntityId[i]))
				{
					return false;
				}
			}
			if (this.Program.Count != batchSubscribeRequest.Program.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Program.Count; j++)
			{
				if (!this.Program[j].Equals(batchSubscribeRequest.Program[j]))
				{
					return false;
				}
			}
			if (this.Key.Count != batchSubscribeRequest.Key.Count)
			{
				return false;
			}
			for (int k = 0; k < this.Key.Count; k++)
			{
				if (!this.Key[k].Equals(batchSubscribeRequest.Key[k]))
				{
					return false;
				}
			}
			return this.HasObjectId == batchSubscribeRequest.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(batchSubscribeRequest.ObjectId));
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000AB1B8 File Offset: 0x000A93B8
		public static BatchSubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BatchSubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000AB1C2 File Offset: 0x000A93C2
		public void Deserialize(Stream stream)
		{
			BatchSubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000AB1CC File Offset: 0x000A93CC
		public static BatchSubscribeRequest Deserialize(Stream stream, BatchSubscribeRequest instance)
		{
			return BatchSubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000AB1D8 File Offset: 0x000A93D8
		public static BatchSubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			BatchSubscribeRequest batchSubscribeRequest = new BatchSubscribeRequest();
			BatchSubscribeRequest.DeserializeLengthDelimited(stream, batchSubscribeRequest);
			return batchSubscribeRequest;
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000AB1F4 File Offset: 0x000A93F4
		public static BatchSubscribeRequest DeserializeLengthDelimited(Stream stream, BatchSubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BatchSubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x000AB21C File Offset: 0x000A941C
		public static BatchSubscribeRequest Deserialize(Stream stream, BatchSubscribeRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.EntityId == null)
			{
				instance.EntityId = new List<EntityId>();
			}
			if (instance.Program == null)
			{
				instance.Program = new List<uint>();
			}
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
				else
				{
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.EntityId.Add(bnet.protocol.EntityId.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
						{
							if (instance.AgentId == null)
							{
								instance.AgentId = bnet.protocol.EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							bnet.protocol.EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
							continue;
						}
					}
					else
					{
						if (num == 29)
						{
							instance.Program.Add(binaryReader.ReadUInt32());
							continue;
						}
						if (num == 34)
						{
							instance.Key.Add(FieldKey.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 40)
						{
							instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06003357 RID: 13143 RVA: 0x000AB372 File Offset: 0x000A9572
		public void Serialize(Stream stream)
		{
			BatchSubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x000AB37C File Offset: 0x000A957C
		public static void Serialize(Stream stream, BatchSubscribeRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				bnet.protocol.EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.EntityId.Count > 0)
			{
				foreach (EntityId entityId in instance.EntityId)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, entityId.GetSerializedSize());
					bnet.protocol.EntityId.Serialize(stream, entityId);
				}
			}
			if (instance.Program.Count > 0)
			{
				foreach (uint value in instance.Program)
				{
					stream.WriteByte(29);
					binaryWriter.Write(value);
				}
			}
			if (instance.Key.Count > 0)
			{
				foreach (FieldKey fieldKey in instance.Key)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, fieldKey.GetSerializedSize());
					FieldKey.Serialize(stream, fieldKey);
				}
			}
			if (instance.HasObjectId)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x000AB504 File Offset: 0x000A9704
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.EntityId.Count > 0)
			{
				foreach (EntityId entityId in this.EntityId)
				{
					num += 1U;
					uint serializedSize2 = entityId.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.Program.Count > 0)
			{
				foreach (uint num2 in this.Program)
				{
					num += 1U;
					num += 4U;
				}
			}
			if (this.Key.Count > 0)
			{
				foreach (FieldKey fieldKey in this.Key)
				{
					num += 1U;
					uint serializedSize3 = fieldKey.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.HasObjectId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			return num;
		}

		// Token: 0x040013F6 RID: 5110
		public bool HasAgentId;

		// Token: 0x040013F7 RID: 5111
		private EntityId _AgentId;

		// Token: 0x040013F8 RID: 5112
		private List<EntityId> _EntityId = new List<EntityId>();

		// Token: 0x040013F9 RID: 5113
		private List<uint> _Program = new List<uint>();

		// Token: 0x040013FA RID: 5114
		private List<FieldKey> _Key = new List<FieldKey>();

		// Token: 0x040013FB RID: 5115
		public bool HasObjectId;

		// Token: 0x040013FC RID: 5116
		private ulong _ObjectId;
	}
}
