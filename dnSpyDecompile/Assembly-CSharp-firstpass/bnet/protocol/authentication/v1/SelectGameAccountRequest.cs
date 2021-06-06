using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004F8 RID: 1272
	public class SelectGameAccountRequest : IProtoBuf
	{
		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06005A64 RID: 23140 RVA: 0x00113FC3 File Offset: 0x001121C3
		// (set) Token: 0x06005A65 RID: 23141 RVA: 0x00113FCB File Offset: 0x001121CB
		public EntityId GameAccountId { get; set; }

		// Token: 0x06005A66 RID: 23142 RVA: 0x00113FD4 File Offset: 0x001121D4
		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x00113FDD File Offset: 0x001121DD
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.GameAccountId.GetHashCode();
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x00113FF8 File Offset: 0x001121F8
		public override bool Equals(object obj)
		{
			SelectGameAccountRequest selectGameAccountRequest = obj as SelectGameAccountRequest;
			return selectGameAccountRequest != null && this.GameAccountId.Equals(selectGameAccountRequest.GameAccountId);
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06005A69 RID: 23145 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x00114027 File Offset: 0x00112227
		public static SelectGameAccountRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SelectGameAccountRequest>(bs, 0, -1);
		}

		// Token: 0x06005A6B RID: 23147 RVA: 0x00114031 File Offset: 0x00112231
		public void Deserialize(Stream stream)
		{
			SelectGameAccountRequest.Deserialize(stream, this);
		}

		// Token: 0x06005A6C RID: 23148 RVA: 0x0011403B File Offset: 0x0011223B
		public static SelectGameAccountRequest Deserialize(Stream stream, SelectGameAccountRequest instance)
		{
			return SelectGameAccountRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005A6D RID: 23149 RVA: 0x00114048 File Offset: 0x00112248
		public static SelectGameAccountRequest DeserializeLengthDelimited(Stream stream)
		{
			SelectGameAccountRequest selectGameAccountRequest = new SelectGameAccountRequest();
			SelectGameAccountRequest.DeserializeLengthDelimited(stream, selectGameAccountRequest);
			return selectGameAccountRequest;
		}

		// Token: 0x06005A6E RID: 23150 RVA: 0x00114064 File Offset: 0x00112264
		public static SelectGameAccountRequest DeserializeLengthDelimited(Stream stream, SelectGameAccountRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SelectGameAccountRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005A6F RID: 23151 RVA: 0x0011408C File Offset: 0x0011228C
		public static SelectGameAccountRequest Deserialize(Stream stream, SelectGameAccountRequest instance, long limit)
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
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
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

		// Token: 0x06005A70 RID: 23152 RVA: 0x00114126 File Offset: 0x00112326
		public void Serialize(Stream stream)
		{
			SelectGameAccountRequest.Serialize(stream, this);
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x0011412F File Offset: 0x0011232F
		public static void Serialize(Stream stream, SelectGameAccountRequest instance)
		{
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			EntityId.Serialize(stream, instance.GameAccountId);
		}

		// Token: 0x06005A72 RID: 23154 RVA: 0x00114170 File Offset: 0x00112370
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameAccountId.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1U;
		}
	}
}
