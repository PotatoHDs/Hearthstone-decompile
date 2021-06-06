using System;
using System.IO;

namespace bnet.protocol.games.v2
{
	// Token: 0x0200036C RID: 876
	public class FindGameRequestId : IProtoBuf
	{
		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06003783 RID: 14211 RVA: 0x000B65F2 File Offset: 0x000B47F2
		// (set) Token: 0x06003784 RID: 14212 RVA: 0x000B65FA File Offset: 0x000B47FA
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

		// Token: 0x06003785 RID: 14213 RVA: 0x000B660A File Offset: 0x000B480A
		public void SetId(ulong val)
		{
			this.Id = val;
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000B6614 File Offset: 0x000B4814
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x000B6648 File Offset: 0x000B4848
		public override bool Equals(object obj)
		{
			FindGameRequestId findGameRequestId = obj as FindGameRequestId;
			return findGameRequestId != null && this.HasId == findGameRequestId.HasId && (!this.HasId || this.Id.Equals(findGameRequestId.Id));
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06003788 RID: 14216 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x000B6690 File Offset: 0x000B4890
		public static FindGameRequestId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindGameRequestId>(bs, 0, -1);
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000B669A File Offset: 0x000B489A
		public void Deserialize(Stream stream)
		{
			FindGameRequestId.Deserialize(stream, this);
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000B66A4 File Offset: 0x000B48A4
		public static FindGameRequestId Deserialize(Stream stream, FindGameRequestId instance)
		{
			return FindGameRequestId.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000B66B0 File Offset: 0x000B48B0
		public static FindGameRequestId DeserializeLengthDelimited(Stream stream)
		{
			FindGameRequestId findGameRequestId = new FindGameRequestId();
			FindGameRequestId.DeserializeLengthDelimited(stream, findGameRequestId);
			return findGameRequestId;
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000B66CC File Offset: 0x000B48CC
		public static FindGameRequestId DeserializeLengthDelimited(Stream stream, FindGameRequestId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FindGameRequestId.Deserialize(stream, instance, num);
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000B66F4 File Offset: 0x000B48F4
		public static FindGameRequestId Deserialize(Stream stream, FindGameRequestId instance, long limit)
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

		// Token: 0x0600378F RID: 14223 RVA: 0x000B677B File Offset: 0x000B497B
		public void Serialize(Stream stream)
		{
			FindGameRequestId.Serialize(stream, this);
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000B6784 File Offset: 0x000B4984
		public static void Serialize(Stream stream, FindGameRequestId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.Id);
			}
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x000B67B4 File Offset: 0x000B49B4
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

		// Token: 0x040014BF RID: 5311
		public bool HasId;

		// Token: 0x040014C0 RID: 5312
		private ulong _Id;
	}
}
