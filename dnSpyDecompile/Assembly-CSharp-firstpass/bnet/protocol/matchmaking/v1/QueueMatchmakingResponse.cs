using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003E4 RID: 996
	public class QueueMatchmakingResponse : IProtoBuf
	{
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x060041B0 RID: 16816 RVA: 0x000D0D4A File Offset: 0x000CEF4A
		// (set) Token: 0x060041B1 RID: 16817 RVA: 0x000D0D52 File Offset: 0x000CEF52
		public RequestId RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
				this.HasRequestId = (value != null);
			}
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x000D0D65 File Offset: 0x000CEF65
		public void SetRequestId(RequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x000D0D70 File Offset: 0x000CEF70
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x000D0DA0 File Offset: 0x000CEFA0
		public override bool Equals(object obj)
		{
			QueueMatchmakingResponse queueMatchmakingResponse = obj as QueueMatchmakingResponse;
			return queueMatchmakingResponse != null && this.HasRequestId == queueMatchmakingResponse.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(queueMatchmakingResponse.RequestId));
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x060041B5 RID: 16821 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x000D0DE5 File Offset: 0x000CEFE5
		public static QueueMatchmakingResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueMatchmakingResponse>(bs, 0, -1);
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x000D0DEF File Offset: 0x000CEFEF
		public void Deserialize(Stream stream)
		{
			QueueMatchmakingResponse.Deserialize(stream, this);
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x000D0DF9 File Offset: 0x000CEFF9
		public static QueueMatchmakingResponse Deserialize(Stream stream, QueueMatchmakingResponse instance)
		{
			return QueueMatchmakingResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x000D0E04 File Offset: 0x000CF004
		public static QueueMatchmakingResponse DeserializeLengthDelimited(Stream stream)
		{
			QueueMatchmakingResponse queueMatchmakingResponse = new QueueMatchmakingResponse();
			QueueMatchmakingResponse.DeserializeLengthDelimited(stream, queueMatchmakingResponse);
			return queueMatchmakingResponse;
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x000D0E20 File Offset: 0x000CF020
		public static QueueMatchmakingResponse DeserializeLengthDelimited(Stream stream, QueueMatchmakingResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueueMatchmakingResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x000D0E48 File Offset: 0x000CF048
		public static QueueMatchmakingResponse Deserialize(Stream stream, QueueMatchmakingResponse instance, long limit)
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
				else if (num == 10)
				{
					if (instance.RequestId == null)
					{
						instance.RequestId = RequestId.DeserializeLengthDelimited(stream);
					}
					else
					{
						RequestId.DeserializeLengthDelimited(stream, instance.RequestId);
					}
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

		// Token: 0x060041BC RID: 16828 RVA: 0x000D0EE2 File Offset: 0x000CF0E2
		public void Serialize(Stream stream)
		{
			QueueMatchmakingResponse.Serialize(stream, this);
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x000D0EEB File Offset: 0x000CF0EB
		public static void Serialize(Stream stream, QueueMatchmakingResponse instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x000D0F1C File Offset: 0x000CF11C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestId)
			{
				num += 1U;
				uint serializedSize = this.RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x040016AE RID: 5806
		public bool HasRequestId;

		// Token: 0x040016AF RID: 5807
		private RequestId _RequestId;
	}
}
