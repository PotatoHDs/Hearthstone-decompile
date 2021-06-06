using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200038E RID: 910
	public class GetFindGameRequestsRequest : IProtoBuf
	{
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06003A38 RID: 14904 RVA: 0x000BCE7F File Offset: 0x000BB07F
		// (set) Token: 0x06003A39 RID: 14905 RVA: 0x000BCE87 File Offset: 0x000BB087
		public ulong FactoryId
		{
			get
			{
				return this._FactoryId;
			}
			set
			{
				this._FactoryId = value;
				this.HasFactoryId = true;
			}
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x000BCE97 File Offset: 0x000BB097
		public void SetFactoryId(ulong val)
		{
			this.FactoryId = val;
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06003A3B RID: 14907 RVA: 0x000BCEA0 File Offset: 0x000BB0A0
		// (set) Token: 0x06003A3C RID: 14908 RVA: 0x000BCEA8 File Offset: 0x000BB0A8
		public uint NumPlayers
		{
			get
			{
				return this._NumPlayers;
			}
			set
			{
				this._NumPlayers = value;
				this.HasNumPlayers = true;
			}
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x000BCEB8 File Offset: 0x000BB0B8
		public void SetNumPlayers(uint val)
		{
			this.NumPlayers = val;
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x000BCEC4 File Offset: 0x000BB0C4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFactoryId)
			{
				num ^= this.FactoryId.GetHashCode();
			}
			if (this.HasNumPlayers)
			{
				num ^= this.NumPlayers.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x000BCF10 File Offset: 0x000BB110
		public override bool Equals(object obj)
		{
			GetFindGameRequestsRequest getFindGameRequestsRequest = obj as GetFindGameRequestsRequest;
			return getFindGameRequestsRequest != null && this.HasFactoryId == getFindGameRequestsRequest.HasFactoryId && (!this.HasFactoryId || this.FactoryId.Equals(getFindGameRequestsRequest.FactoryId)) && this.HasNumPlayers == getFindGameRequestsRequest.HasNumPlayers && (!this.HasNumPlayers || this.NumPlayers.Equals(getFindGameRequestsRequest.NumPlayers));
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06003A40 RID: 14912 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x000BCF86 File Offset: 0x000BB186
		public static GetFindGameRequestsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFindGameRequestsRequest>(bs, 0, -1);
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x000BCF90 File Offset: 0x000BB190
		public void Deserialize(Stream stream)
		{
			GetFindGameRequestsRequest.Deserialize(stream, this);
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x000BCF9A File Offset: 0x000BB19A
		public static GetFindGameRequestsRequest Deserialize(Stream stream, GetFindGameRequestsRequest instance)
		{
			return GetFindGameRequestsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x000BCFA8 File Offset: 0x000BB1A8
		public static GetFindGameRequestsRequest DeserializeLengthDelimited(Stream stream)
		{
			GetFindGameRequestsRequest getFindGameRequestsRequest = new GetFindGameRequestsRequest();
			GetFindGameRequestsRequest.DeserializeLengthDelimited(stream, getFindGameRequestsRequest);
			return getFindGameRequestsRequest;
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x000BCFC4 File Offset: 0x000BB1C4
		public static GetFindGameRequestsRequest DeserializeLengthDelimited(Stream stream, GetFindGameRequestsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFindGameRequestsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x000BCFEC File Offset: 0x000BB1EC
		public static GetFindGameRequestsRequest Deserialize(Stream stream, GetFindGameRequestsRequest instance, long limit)
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
				else if (num != 9)
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
						instance.NumPlayers = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.FactoryId = binaryReader.ReadUInt64();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x000BD08B File Offset: 0x000BB28B
		public void Serialize(Stream stream)
		{
			GetFindGameRequestsRequest.Serialize(stream, this);
		}

		// Token: 0x06003A48 RID: 14920 RVA: 0x000BD094 File Offset: 0x000BB294
		public static void Serialize(Stream stream, GetFindGameRequestsRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasFactoryId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.FactoryId);
			}
			if (instance.HasNumPlayers)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.NumPlayers);
			}
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x000BD0E0 File Offset: 0x000BB2E0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFactoryId)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasNumPlayers)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.NumPlayers);
			}
			return num;
		}

		// Token: 0x04001533 RID: 5427
		public bool HasFactoryId;

		// Token: 0x04001534 RID: 5428
		private ulong _FactoryId;

		// Token: 0x04001535 RID: 5429
		public bool HasNumPlayers;

		// Token: 0x04001536 RID: 5430
		private uint _NumPlayers;
	}
}
