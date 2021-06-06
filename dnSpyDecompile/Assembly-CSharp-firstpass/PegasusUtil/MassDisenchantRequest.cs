using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200006A RID: 106
	public class MassDisenchantRequest : IProtoBuf
	{
		// Token: 0x060006B2 RID: 1714 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00019639 File Offset: 0x00017839
		public override bool Equals(object obj)
		{
			return obj is MassDisenchantRequest;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00019646 File Offset: 0x00017846
		public void Deserialize(Stream stream)
		{
			MassDisenchantRequest.Deserialize(stream, this);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00019650 File Offset: 0x00017850
		public static MassDisenchantRequest Deserialize(Stream stream, MassDisenchantRequest instance)
		{
			return MassDisenchantRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001965C File Offset: 0x0001785C
		public static MassDisenchantRequest DeserializeLengthDelimited(Stream stream)
		{
			MassDisenchantRequest massDisenchantRequest = new MassDisenchantRequest();
			MassDisenchantRequest.DeserializeLengthDelimited(stream, massDisenchantRequest);
			return massDisenchantRequest;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00019678 File Offset: 0x00017878
		public static MassDisenchantRequest DeserializeLengthDelimited(Stream stream, MassDisenchantRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MassDisenchantRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000196A0 File Offset: 0x000178A0
		public static MassDisenchantRequest Deserialize(Stream stream, MassDisenchantRequest instance, long limit)
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

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001970D File Offset: 0x0001790D
		public void Serialize(Stream stream)
		{
			MassDisenchantRequest.Serialize(stream, this);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, MassDisenchantRequest instance)
		{
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x0200057D RID: 1405
		public enum PacketID
		{
			// Token: 0x04001ED3 RID: 7891
			ID = 268,
			// Token: 0x04001ED4 RID: 7892
			System = 0
		}
	}
}
