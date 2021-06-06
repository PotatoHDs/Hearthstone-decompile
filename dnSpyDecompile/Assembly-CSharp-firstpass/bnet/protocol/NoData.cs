using System;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x020002B2 RID: 690
	public class NoData : IProtoBuf
	{
		// Token: 0x0600282F RID: 10287 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x0008E109 File Offset: 0x0008C309
		public override bool Equals(object obj)
		{
			return obj is NoData;
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002831 RID: 10289 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x0008E116 File Offset: 0x0008C316
		public static NoData ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<NoData>(bs, 0, -1);
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x0008E120 File Offset: 0x0008C320
		public void Deserialize(Stream stream)
		{
			NoData.Deserialize(stream, this);
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x0008E12A File Offset: 0x0008C32A
		public static NoData Deserialize(Stream stream, NoData instance)
		{
			return NoData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x0008E138 File Offset: 0x0008C338
		public static NoData DeserializeLengthDelimited(Stream stream)
		{
			NoData noData = new NoData();
			NoData.DeserializeLengthDelimited(stream, noData);
			return noData;
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x0008E154 File Offset: 0x0008C354
		public static NoData DeserializeLengthDelimited(Stream stream, NoData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return NoData.Deserialize(stream, instance, num);
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x0008E17C File Offset: 0x0008C37C
		public static NoData Deserialize(Stream stream, NoData instance, long limit)
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

		// Token: 0x06002838 RID: 10296 RVA: 0x0008E1E9 File Offset: 0x0008C3E9
		public void Serialize(Stream stream)
		{
			NoData.Serialize(stream, this);
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, NoData instance)
		{
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}
	}
}
