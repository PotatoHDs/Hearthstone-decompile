using System;
using System.IO;
using System.Text;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x02000361 RID: 865
	public class GetAllValuesForAttributeRequest : IProtoBuf
	{
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060036A8 RID: 13992 RVA: 0x000B44EF File Offset: 0x000B26EF
		// (set) Token: 0x060036A9 RID: 13993 RVA: 0x000B44F7 File Offset: 0x000B26F7
		public string AttributeKey
		{
			get
			{
				return this._AttributeKey;
			}
			set
			{
				this._AttributeKey = value;
				this.HasAttributeKey = (value != null);
			}
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000B450A File Offset: 0x000B270A
		public void SetAttributeKey(string val)
		{
			this.AttributeKey = val;
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060036AB RID: 13995 RVA: 0x000B4513 File Offset: 0x000B2713
		// (set) Token: 0x060036AC RID: 13996 RVA: 0x000B451B File Offset: 0x000B271B
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

		// Token: 0x060036AD RID: 13997 RVA: 0x000B452E File Offset: 0x000B272E
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060036AE RID: 13998 RVA: 0x000B4537 File Offset: 0x000B2737
		// (set) Token: 0x060036AF RID: 13999 RVA: 0x000B453F File Offset: 0x000B273F
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000B454F File Offset: 0x000B274F
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000B4558 File Offset: 0x000B2758
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAttributeKey)
			{
				num ^= this.AttributeKey.GetHashCode();
			}
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000B45B8 File Offset: 0x000B27B8
		public override bool Equals(object obj)
		{
			GetAllValuesForAttributeRequest getAllValuesForAttributeRequest = obj as GetAllValuesForAttributeRequest;
			return getAllValuesForAttributeRequest != null && this.HasAttributeKey == getAllValuesForAttributeRequest.HasAttributeKey && (!this.HasAttributeKey || this.AttributeKey.Equals(getAllValuesForAttributeRequest.AttributeKey)) && this.HasAgentId == getAllValuesForAttributeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(getAllValuesForAttributeRequest.AgentId)) && this.HasProgram == getAllValuesForAttributeRequest.HasProgram && (!this.HasProgram || this.Program.Equals(getAllValuesForAttributeRequest.Program));
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060036B3 RID: 14003 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x000B4656 File Offset: 0x000B2856
		public static GetAllValuesForAttributeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAllValuesForAttributeRequest>(bs, 0, -1);
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x000B4660 File Offset: 0x000B2860
		public void Deserialize(Stream stream)
		{
			GetAllValuesForAttributeRequest.Deserialize(stream, this);
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x000B466A File Offset: 0x000B286A
		public static GetAllValuesForAttributeRequest Deserialize(Stream stream, GetAllValuesForAttributeRequest instance)
		{
			return GetAllValuesForAttributeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x000B4678 File Offset: 0x000B2878
		public static GetAllValuesForAttributeRequest DeserializeLengthDelimited(Stream stream)
		{
			GetAllValuesForAttributeRequest getAllValuesForAttributeRequest = new GetAllValuesForAttributeRequest();
			GetAllValuesForAttributeRequest.DeserializeLengthDelimited(stream, getAllValuesForAttributeRequest);
			return getAllValuesForAttributeRequest;
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x000B4694 File Offset: 0x000B2894
		public static GetAllValuesForAttributeRequest DeserializeLengthDelimited(Stream stream, GetAllValuesForAttributeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAllValuesForAttributeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x000B46BC File Offset: 0x000B28BC
		public static GetAllValuesForAttributeRequest Deserialize(Stream stream, GetAllValuesForAttributeRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
						if (num != 45)
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
							instance.Program = binaryReader.ReadUInt32();
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
				else
				{
					instance.AttributeKey = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060036BA RID: 14010 RVA: 0x000B4791 File Offset: 0x000B2991
		public void Serialize(Stream stream)
		{
			GetAllValuesForAttributeRequest.Serialize(stream, this);
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x000B479C File Offset: 0x000B299C
		public static void Serialize(Stream stream, GetAllValuesForAttributeRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAttributeKey)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AttributeKey));
			}
			if (instance.HasAgentId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Program);
			}
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x000B4820 File Offset: 0x000B2A20
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAttributeKey)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.AttributeKey);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x0400149C RID: 5276
		public bool HasAttributeKey;

		// Token: 0x0400149D RID: 5277
		private string _AttributeKey;

		// Token: 0x0400149E RID: 5278
		public bool HasAgentId;

		// Token: 0x0400149F RID: 5279
		private EntityId _AgentId;

		// Token: 0x040014A0 RID: 5280
		public bool HasProgram;

		// Token: 0x040014A1 RID: 5281
		private uint _Program;
	}
}
