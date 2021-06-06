using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000068 RID: 104
	public class UpdateAccountLicenses : IProtoBuf
	{
		// Token: 0x0600069A RID: 1690 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x000193E3 File Offset: 0x000175E3
		public override bool Equals(object obj)
		{
			return obj is UpdateAccountLicenses;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000193F0 File Offset: 0x000175F0
		public void Deserialize(Stream stream)
		{
			UpdateAccountLicenses.Deserialize(stream, this);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x000193FA File Offset: 0x000175FA
		public static UpdateAccountLicenses Deserialize(Stream stream, UpdateAccountLicenses instance)
		{
			return UpdateAccountLicenses.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00019408 File Offset: 0x00017608
		public static UpdateAccountLicenses DeserializeLengthDelimited(Stream stream)
		{
			UpdateAccountLicenses updateAccountLicenses = new UpdateAccountLicenses();
			UpdateAccountLicenses.DeserializeLengthDelimited(stream, updateAccountLicenses);
			return updateAccountLicenses;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00019424 File Offset: 0x00017624
		public static UpdateAccountLicenses DeserializeLengthDelimited(Stream stream, UpdateAccountLicenses instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateAccountLicenses.Deserialize(stream, instance, num);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001944C File Offset: 0x0001764C
		public static UpdateAccountLicenses Deserialize(Stream stream, UpdateAccountLicenses instance, long limit)
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

		// Token: 0x060006A1 RID: 1697 RVA: 0x000194B9 File Offset: 0x000176B9
		public void Serialize(Stream stream)
		{
			UpdateAccountLicenses.Serialize(stream, this);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, UpdateAccountLicenses instance)
		{
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x0200057B RID: 1403
		public enum PacketID
		{
			// Token: 0x04001ECD RID: 7885
			ID = 276,
			// Token: 0x04001ECE RID: 7886
			System = 1
		}
	}
}
