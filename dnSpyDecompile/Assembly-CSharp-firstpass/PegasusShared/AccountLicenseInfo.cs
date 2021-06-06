using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000145 RID: 325
	public class AccountLicenseInfo : IProtoBuf
	{
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0004951E File Offset: 0x0004771E
		// (set) Token: 0x06001572 RID: 5490 RVA: 0x00049526 File Offset: 0x00047726
		public long License { get; set; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0004952F File Offset: 0x0004772F
		// (set) Token: 0x06001574 RID: 5492 RVA: 0x00049537 File Offset: 0x00047737
		public ulong Flags_ { get; set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x00049540 File Offset: 0x00047740
		// (set) Token: 0x06001576 RID: 5494 RVA: 0x00049548 File Offset: 0x00047748
		public long CasId { get; set; }

		// Token: 0x06001577 RID: 5495 RVA: 0x00049554 File Offset: 0x00047754
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.License.GetHashCode() ^ this.Flags_.GetHashCode() ^ this.CasId.GetHashCode();
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0004959C File Offset: 0x0004779C
		public override bool Equals(object obj)
		{
			AccountLicenseInfo accountLicenseInfo = obj as AccountLicenseInfo;
			return accountLicenseInfo != null && this.License.Equals(accountLicenseInfo.License) && this.Flags_.Equals(accountLicenseInfo.Flags_) && this.CasId.Equals(accountLicenseInfo.CasId);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x000495FE File Offset: 0x000477FE
		public void Deserialize(Stream stream)
		{
			AccountLicenseInfo.Deserialize(stream, this);
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00049608 File Offset: 0x00047808
		public static AccountLicenseInfo Deserialize(Stream stream, AccountLicenseInfo instance)
		{
			return AccountLicenseInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x00049614 File Offset: 0x00047814
		public static AccountLicenseInfo DeserializeLengthDelimited(Stream stream)
		{
			AccountLicenseInfo accountLicenseInfo = new AccountLicenseInfo();
			AccountLicenseInfo.DeserializeLengthDelimited(stream, accountLicenseInfo);
			return accountLicenseInfo;
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00049630 File Offset: 0x00047830
		public static AccountLicenseInfo DeserializeLengthDelimited(Stream stream, AccountLicenseInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountLicenseInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x00049658 File Offset: 0x00047858
		public static AccountLicenseInfo Deserialize(Stream stream, AccountLicenseInfo instance, long limit)
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
						if (num != 24)
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
							instance.CasId = (long)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.Flags_ = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.License = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00049705 File Offset: 0x00047905
		public void Serialize(Stream stream)
		{
			AccountLicenseInfo.Serialize(stream, this);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0004970E File Offset: 0x0004790E
		public static void Serialize(Stream stream, AccountLicenseInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.License);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.Flags_);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CasId);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0004974B File Offset: 0x0004794B
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)this.License) + ProtocolParser.SizeOfUInt64(this.Flags_) + ProtocolParser.SizeOfUInt64((ulong)this.CasId) + 3U;
		}

		// Token: 0x02000639 RID: 1593
		public enum Flags
		{
			// Token: 0x040020E4 RID: 8420
			OWNED = 1
		}
	}
}
