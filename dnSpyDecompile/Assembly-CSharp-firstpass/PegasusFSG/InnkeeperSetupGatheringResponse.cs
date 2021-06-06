using System;
using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	// Token: 0x0200001F RID: 31
	public class InnkeeperSetupGatheringResponse : IProtoBuf
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006F9F File Offset: 0x0000519F
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00006FA7 File Offset: 0x000051A7
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006FB0 File Offset: 0x000051B0
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00006FB8 File Offset: 0x000051B8
		public long FsgId { get; set; }

		// Token: 0x06000168 RID: 360 RVA: 0x00006FC4 File Offset: 0x000051C4
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ErrorCode.GetHashCode() ^ this.FsgId.GetHashCode();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007000 File Offset: 0x00005200
		public override bool Equals(object obj)
		{
			InnkeeperSetupGatheringResponse innkeeperSetupGatheringResponse = obj as InnkeeperSetupGatheringResponse;
			return innkeeperSetupGatheringResponse != null && this.ErrorCode.Equals(innkeeperSetupGatheringResponse.ErrorCode) && this.FsgId.Equals(innkeeperSetupGatheringResponse.FsgId);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007055 File Offset: 0x00005255
		public void Deserialize(Stream stream)
		{
			InnkeeperSetupGatheringResponse.Deserialize(stream, this);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000705F File Offset: 0x0000525F
		public static InnkeeperSetupGatheringResponse Deserialize(Stream stream, InnkeeperSetupGatheringResponse instance)
		{
			return InnkeeperSetupGatheringResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000706C File Offset: 0x0000526C
		public static InnkeeperSetupGatheringResponse DeserializeLengthDelimited(Stream stream)
		{
			InnkeeperSetupGatheringResponse innkeeperSetupGatheringResponse = new InnkeeperSetupGatheringResponse();
			InnkeeperSetupGatheringResponse.DeserializeLengthDelimited(stream, innkeeperSetupGatheringResponse);
			return innkeeperSetupGatheringResponse;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007088 File Offset: 0x00005288
		public static InnkeeperSetupGatheringResponse DeserializeLengthDelimited(Stream stream, InnkeeperSetupGatheringResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InnkeeperSetupGatheringResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000070B0 File Offset: 0x000052B0
		public static InnkeeperSetupGatheringResponse Deserialize(Stream stream, InnkeeperSetupGatheringResponse instance, long limit)
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

		// Token: 0x0600016F RID: 367 RVA: 0x00007148 File Offset: 0x00005348
		public void Serialize(Stream stream)
		{
			InnkeeperSetupGatheringResponse.Serialize(stream, this);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007151 File Offset: 0x00005351
		public static void Serialize(Stream stream, InnkeeperSetupGatheringResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000717B File Offset: 0x0000537B
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode)) + ProtocolParser.SizeOfUInt64((ulong)this.FsgId) + 2U;
		}

		// Token: 0x02000555 RID: 1365
		public enum PacketID
		{
			// Token: 0x04001E1D RID: 7709
			ID = 508
		}
	}
}
