using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003D7 RID: 983
	public class RequestId : IProtoBuf
	{
		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06004087 RID: 16519 RVA: 0x000CDA93 File Offset: 0x000CBC93
		// (set) Token: 0x06004088 RID: 16520 RVA: 0x000CDA9B File Offset: 0x000CBC9B
		public ulong Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x000CDAAB File Offset: 0x000CBCAB
		public void SetId(ulong val)
		{
			this.Id = val;
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x000CDAB4 File Offset: 0x000CBCB4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x000CDAE8 File Offset: 0x000CBCE8
		public override bool Equals(object obj)
		{
			RequestId requestId = obj as RequestId;
			return requestId != null && this.HasId == requestId.HasId && (!this.HasId || this.Id.Equals(requestId.Id));
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x0600408C RID: 16524 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x000CDB30 File Offset: 0x000CBD30
		public static RequestId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RequestId>(bs, 0, -1);
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x000CDB3A File Offset: 0x000CBD3A
		public void Deserialize(Stream stream)
		{
			RequestId.Deserialize(stream, this);
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x000CDB44 File Offset: 0x000CBD44
		public static RequestId Deserialize(Stream stream, RequestId instance)
		{
			return RequestId.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x000CDB50 File Offset: 0x000CBD50
		public static RequestId DeserializeLengthDelimited(Stream stream)
		{
			RequestId requestId = new RequestId();
			RequestId.DeserializeLengthDelimited(stream, requestId);
			return requestId;
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x000CDB6C File Offset: 0x000CBD6C
		public static RequestId DeserializeLengthDelimited(Stream stream, RequestId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RequestId.Deserialize(stream, instance, num);
		}

		// Token: 0x06004092 RID: 16530 RVA: 0x000CDB94 File Offset: 0x000CBD94
		public static RequestId Deserialize(Stream stream, RequestId instance, long limit)
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
				else if (num == 9)
				{
					instance.Id = binaryReader.ReadUInt64();
				}
				else
				{
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

		// Token: 0x06004093 RID: 16531 RVA: 0x000CDC1B File Offset: 0x000CBE1B
		public void Serialize(Stream stream)
		{
			RequestId.Serialize(stream, this);
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x000CDC24 File Offset: 0x000CBE24
		public static void Serialize(Stream stream, RequestId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.Id);
			}
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x000CDC54 File Offset: 0x000CBE54
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x04001676 RID: 5750
		public bool HasId;

		// Token: 0x04001677 RID: 5751
		private ulong _Id;
	}
}
