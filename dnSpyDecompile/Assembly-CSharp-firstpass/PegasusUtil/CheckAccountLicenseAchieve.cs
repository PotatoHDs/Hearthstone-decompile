using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000070 RID: 112
	public class CheckAccountLicenseAchieve : IProtoBuf
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001A55E File Offset: 0x0001875E
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0001A566 File Offset: 0x00018766
		public int Achieve { get; set; }

		// Token: 0x06000714 RID: 1812 RVA: 0x0001A570 File Offset: 0x00018770
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Achieve.GetHashCode();
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001A598 File Offset: 0x00018798
		public override bool Equals(object obj)
		{
			CheckAccountLicenseAchieve checkAccountLicenseAchieve = obj as CheckAccountLicenseAchieve;
			return checkAccountLicenseAchieve != null && this.Achieve.Equals(checkAccountLicenseAchieve.Achieve);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001A5CA File Offset: 0x000187CA
		public void Deserialize(Stream stream)
		{
			CheckAccountLicenseAchieve.Deserialize(stream, this);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001A5D4 File Offset: 0x000187D4
		public static CheckAccountLicenseAchieve Deserialize(Stream stream, CheckAccountLicenseAchieve instance)
		{
			return CheckAccountLicenseAchieve.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001A5E0 File Offset: 0x000187E0
		public static CheckAccountLicenseAchieve DeserializeLengthDelimited(Stream stream)
		{
			CheckAccountLicenseAchieve checkAccountLicenseAchieve = new CheckAccountLicenseAchieve();
			CheckAccountLicenseAchieve.DeserializeLengthDelimited(stream, checkAccountLicenseAchieve);
			return checkAccountLicenseAchieve;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001A5FC File Offset: 0x000187FC
		public static CheckAccountLicenseAchieve DeserializeLengthDelimited(Stream stream, CheckAccountLicenseAchieve instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CheckAccountLicenseAchieve.Deserialize(stream, instance, num);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001A624 File Offset: 0x00018824
		public static CheckAccountLicenseAchieve Deserialize(Stream stream, CheckAccountLicenseAchieve instance, long limit)
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
				else if (num == 8)
				{
					instance.Achieve = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600071B RID: 1819 RVA: 0x0001A6A4 File Offset: 0x000188A4
		public void Serialize(Stream stream)
		{
			CheckAccountLicenseAchieve.Serialize(stream, this);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001A6AD File Offset: 0x000188AD
		public static void Serialize(Stream stream, CheckAccountLicenseAchieve instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Achieve));
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001A6C3 File Offset: 0x000188C3
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Achieve)) + 1U;
		}

		// Token: 0x02000582 RID: 1410
		public enum PacketID
		{
			// Token: 0x04001EE2 RID: 7906
			ID = 297,
			// Token: 0x04001EE3 RID: 7907
			System = 1
		}
	}
}
