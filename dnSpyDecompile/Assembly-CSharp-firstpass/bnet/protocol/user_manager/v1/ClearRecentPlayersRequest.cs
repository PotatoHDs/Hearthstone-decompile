using System;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	// Token: 0x020002F0 RID: 752
	public class ClearRecentPlayersRequest : IProtoBuf
	{
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06002CCE RID: 11470 RVA: 0x00099EA7 File Offset: 0x000980A7
		// (set) Token: 0x06002CCF RID: 11471 RVA: 0x00099EAF File Offset: 0x000980AF
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

		// Token: 0x06002CD0 RID: 11472 RVA: 0x00099EC2 File Offset: 0x000980C2
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x00099ECB File Offset: 0x000980CB
		// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x00099ED3 File Offset: 0x000980D3
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

		// Token: 0x06002CD3 RID: 11475 RVA: 0x00099EE3 File Offset: 0x000980E3
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x00099EEC File Offset: 0x000980EC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
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

		// Token: 0x06002CD5 RID: 11477 RVA: 0x00099F38 File Offset: 0x00098138
		public override bool Equals(object obj)
		{
			ClearRecentPlayersRequest clearRecentPlayersRequest = obj as ClearRecentPlayersRequest;
			return clearRecentPlayersRequest != null && this.HasAgentId == clearRecentPlayersRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(clearRecentPlayersRequest.AgentId)) && this.HasProgram == clearRecentPlayersRequest.HasProgram && (!this.HasProgram || this.Program.Equals(clearRecentPlayersRequest.Program));
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x00099FAB File Offset: 0x000981AB
		public static ClearRecentPlayersRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ClearRecentPlayersRequest>(bs, 0, -1);
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x00099FB5 File Offset: 0x000981B5
		public void Deserialize(Stream stream)
		{
			ClearRecentPlayersRequest.Deserialize(stream, this);
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x00099FBF File Offset: 0x000981BF
		public static ClearRecentPlayersRequest Deserialize(Stream stream, ClearRecentPlayersRequest instance)
		{
			return ClearRecentPlayersRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x00099FCC File Offset: 0x000981CC
		public static ClearRecentPlayersRequest DeserializeLengthDelimited(Stream stream)
		{
			ClearRecentPlayersRequest clearRecentPlayersRequest = new ClearRecentPlayersRequest();
			ClearRecentPlayersRequest.DeserializeLengthDelimited(stream, clearRecentPlayersRequest);
			return clearRecentPlayersRequest;
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x00099FE8 File Offset: 0x000981E8
		public static ClearRecentPlayersRequest DeserializeLengthDelimited(Stream stream, ClearRecentPlayersRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClearRecentPlayersRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x0009A010 File Offset: 0x00098210
		public static ClearRecentPlayersRequest Deserialize(Stream stream, ClearRecentPlayersRequest instance, long limit)
		{
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
					if (num != 16)
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
						instance.Program = ProtocolParser.ReadUInt32(stream);
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

		// Token: 0x06002CDD RID: 11485 RVA: 0x0009A0C2 File Offset: 0x000982C2
		public void Serialize(Stream stream)
		{
			ClearRecentPlayersRequest.Serialize(stream, this);
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x0009A0CC File Offset: 0x000982CC
		public static void Serialize(Stream stream, ClearRecentPlayersRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Program);
			}
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x0009A124 File Offset: 0x00098324
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Program);
			}
			return num;
		}

		// Token: 0x04001271 RID: 4721
		public bool HasAgentId;

		// Token: 0x04001272 RID: 4722
		private EntityId _AgentId;

		// Token: 0x04001273 RID: 4723
		public bool HasProgram;

		// Token: 0x04001274 RID: 4724
		private uint _Program;
	}
}
