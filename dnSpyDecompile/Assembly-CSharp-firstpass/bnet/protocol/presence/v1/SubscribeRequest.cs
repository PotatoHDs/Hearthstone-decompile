using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x02000335 RID: 821
	public class SubscribeRequest : IProtoBuf
	{
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x000A917B File Offset: 0x000A737B
		// (set) Token: 0x0600328C RID: 12940 RVA: 0x000A9183 File Offset: 0x000A7383
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

		// Token: 0x0600328D RID: 12941 RVA: 0x000A9196 File Offset: 0x000A7396
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x000A919F File Offset: 0x000A739F
		// (set) Token: 0x0600328F RID: 12943 RVA: 0x000A91A7 File Offset: 0x000A73A7
		public EntityId EntityId { get; set; }

		// Token: 0x06003290 RID: 12944 RVA: 0x000A91B0 File Offset: 0x000A73B0
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06003291 RID: 12945 RVA: 0x000A91B9 File Offset: 0x000A73B9
		// (set) Token: 0x06003292 RID: 12946 RVA: 0x000A91C1 File Offset: 0x000A73C1
		public ulong ObjectId { get; set; }

		// Token: 0x06003293 RID: 12947 RVA: 0x000A91CA File Offset: 0x000A73CA
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06003294 RID: 12948 RVA: 0x000A91D3 File Offset: 0x000A73D3
		// (set) Token: 0x06003295 RID: 12949 RVA: 0x000A91DB File Offset: 0x000A73DB
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

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x000A91D3 File Offset: 0x000A73D3
		public List<uint> ProgramList
		{
			get
			{
				return this._Program;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06003297 RID: 12951 RVA: 0x000A91E4 File Offset: 0x000A73E4
		public int ProgramCount
		{
			get
			{
				return this._Program.Count;
			}
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000A91F1 File Offset: 0x000A73F1
		public void AddProgram(uint val)
		{
			this._Program.Add(val);
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x000A91FF File Offset: 0x000A73FF
		public void ClearProgram()
		{
			this._Program.Clear();
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x000A920C File Offset: 0x000A740C
		public void SetProgram(List<uint> val)
		{
			this.Program = val;
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x000A9215 File Offset: 0x000A7415
		// (set) Token: 0x0600329C RID: 12956 RVA: 0x000A921D File Offset: 0x000A741D
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

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x0600329D RID: 12957 RVA: 0x000A9215 File Offset: 0x000A7415
		public List<FieldKey> KeyList
		{
			get
			{
				return this._Key;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x0600329E RID: 12958 RVA: 0x000A9226 File Offset: 0x000A7426
		public int KeyCount
		{
			get
			{
				return this._Key.Count;
			}
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x000A9233 File Offset: 0x000A7433
		public void AddKey(FieldKey val)
		{
			this._Key.Add(val);
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x000A9241 File Offset: 0x000A7441
		public void ClearKey()
		{
			this._Key.Clear();
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x000A924E File Offset: 0x000A744E
		public void SetKey(List<FieldKey> val)
		{
			this.Key = val;
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000A9258 File Offset: 0x000A7458
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.EntityId.GetHashCode();
			num ^= this.ObjectId.GetHashCode();
			foreach (uint num2 in this.Program)
			{
				num ^= num2.GetHashCode();
			}
			foreach (FieldKey fieldKey in this.Key)
			{
				num ^= fieldKey.GetHashCode();
			}
			return num;
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x000A9338 File Offset: 0x000A7538
		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			if (subscribeRequest == null)
			{
				return false;
			}
			if (this.HasAgentId != subscribeRequest.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(subscribeRequest.AgentId)))
			{
				return false;
			}
			if (!this.EntityId.Equals(subscribeRequest.EntityId))
			{
				return false;
			}
			if (!this.ObjectId.Equals(subscribeRequest.ObjectId))
			{
				return false;
			}
			if (this.Program.Count != subscribeRequest.Program.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Program.Count; i++)
			{
				if (!this.Program[i].Equals(subscribeRequest.Program[i]))
				{
					return false;
				}
			}
			if (this.Key.Count != subscribeRequest.Key.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Key.Count; j++)
			{
				if (!this.Key[j].Equals(subscribeRequest.Key[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x000A9455 File Offset: 0x000A7655
		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000A945F File Offset: 0x000A765F
		public void Deserialize(Stream stream)
		{
			SubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x000A9469 File Offset: 0x000A7669
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return SubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000A9474 File Offset: 0x000A7674
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			SubscribeRequest.DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000A9490 File Offset: 0x000A7690
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x000A94B8 File Offset: 0x000A76B8
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
							if (instance.AgentId == null)
							{
								instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.ObjectId = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 37)
						{
							instance.Program.Add(binaryReader.ReadUInt32());
							continue;
						}
						if (num == 50)
						{
							instance.Key.Add(FieldKey.DeserializeLengthDelimited(stream));
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

		// Token: 0x060032AB RID: 12971 RVA: 0x000A9613 File Offset: 0x000A7813
		public void Serialize(Stream stream)
		{
			SubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000A961C File Offset: 0x000A781C
		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			if (instance.Program.Count > 0)
			{
				foreach (uint value in instance.Program)
				{
					stream.WriteByte(37);
					binaryWriter.Write(value);
				}
			}
			if (instance.Key.Count > 0)
			{
				foreach (FieldKey fieldKey in instance.Key)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, fieldKey.GetSerializedSize());
					FieldKey.Serialize(stream, fieldKey);
				}
			}
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000A9774 File Offset: 0x000A7974
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.EntityId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			num += ProtocolParser.SizeOfUInt64(this.ObjectId);
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
			num += 2U;
			return num;
		}

		// Token: 0x040013DC RID: 5084
		public bool HasAgentId;

		// Token: 0x040013DD RID: 5085
		private EntityId _AgentId;

		// Token: 0x040013E0 RID: 5088
		private List<uint> _Program = new List<uint>();

		// Token: 0x040013E1 RID: 5089
		private List<FieldKey> _Key = new List<FieldKey>();
	}
}
