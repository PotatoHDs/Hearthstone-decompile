using System;
using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	// Token: 0x0200001D RID: 29
	public class CheckOutOfFSGResponse : IProtoBuf
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000687E File Offset: 0x00004A7E
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00006886 File Offset: 0x00004A86
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000688F File Offset: 0x00004A8F
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00006897 File Offset: 0x00004A97
		public long FsgId { get; set; }

		// Token: 0x06000146 RID: 326 RVA: 0x000068A0 File Offset: 0x00004AA0
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ErrorCode.GetHashCode() ^ this.FsgId.GetHashCode();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000068DC File Offset: 0x00004ADC
		public override bool Equals(object obj)
		{
			CheckOutOfFSGResponse checkOutOfFSGResponse = obj as CheckOutOfFSGResponse;
			return checkOutOfFSGResponse != null && this.ErrorCode.Equals(checkOutOfFSGResponse.ErrorCode) && this.FsgId.Equals(checkOutOfFSGResponse.FsgId);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006931 File Offset: 0x00004B31
		public void Deserialize(Stream stream)
		{
			CheckOutOfFSGResponse.Deserialize(stream, this);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000693B File Offset: 0x00004B3B
		public static CheckOutOfFSGResponse Deserialize(Stream stream, CheckOutOfFSGResponse instance)
		{
			return CheckOutOfFSGResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006948 File Offset: 0x00004B48
		public static CheckOutOfFSGResponse DeserializeLengthDelimited(Stream stream)
		{
			CheckOutOfFSGResponse checkOutOfFSGResponse = new CheckOutOfFSGResponse();
			CheckOutOfFSGResponse.DeserializeLengthDelimited(stream, checkOutOfFSGResponse);
			return checkOutOfFSGResponse;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006964 File Offset: 0x00004B64
		public static CheckOutOfFSGResponse DeserializeLengthDelimited(Stream stream, CheckOutOfFSGResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CheckOutOfFSGResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000698C File Offset: 0x00004B8C
		public static CheckOutOfFSGResponse Deserialize(Stream stream, CheckOutOfFSGResponse instance, long limit)
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
				else if (num != 8)
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
						instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006A24 File Offset: 0x00004C24
		public void Serialize(Stream stream)
		{
			CheckOutOfFSGResponse.Serialize(stream, this);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006A2D File Offset: 0x00004C2D
		public static void Serialize(Stream stream, CheckOutOfFSGResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006A57 File Offset: 0x00004C57
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode)) + ProtocolParser.SizeOfUInt64((ulong)this.FsgId) + 2U;
		}

		// Token: 0x02000553 RID: 1363
		public enum PacketID
		{
			// Token: 0x04001E18 RID: 7704
			ID = 506
		}
	}
}
