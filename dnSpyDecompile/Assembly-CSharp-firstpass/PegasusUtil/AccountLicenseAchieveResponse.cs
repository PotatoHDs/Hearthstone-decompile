using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000BC RID: 188
	public class AccountLicenseAchieveResponse : IProtoBuf
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x000302E3 File Offset: 0x0002E4E3
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x000302EB File Offset: 0x0002E4EB
		public int Achieve { get; set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x000302F4 File Offset: 0x0002E4F4
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x000302FC File Offset: 0x0002E4FC
		public AccountLicenseAchieveResponse.Result Result_ { get; set; }

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00030308 File Offset: 0x0002E508
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Achieve.GetHashCode() ^ this.Result_.GetHashCode();
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00030344 File Offset: 0x0002E544
		public override bool Equals(object obj)
		{
			AccountLicenseAchieveResponse accountLicenseAchieveResponse = obj as AccountLicenseAchieveResponse;
			return accountLicenseAchieveResponse != null && this.Achieve.Equals(accountLicenseAchieveResponse.Achieve) && this.Result_.Equals(accountLicenseAchieveResponse.Result_);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00030399 File Offset: 0x0002E599
		public void Deserialize(Stream stream)
		{
			AccountLicenseAchieveResponse.Deserialize(stream, this);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x000303A3 File Offset: 0x0002E5A3
		public static AccountLicenseAchieveResponse Deserialize(Stream stream, AccountLicenseAchieveResponse instance)
		{
			return AccountLicenseAchieveResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000303B0 File Offset: 0x0002E5B0
		public static AccountLicenseAchieveResponse DeserializeLengthDelimited(Stream stream)
		{
			AccountLicenseAchieveResponse accountLicenseAchieveResponse = new AccountLicenseAchieveResponse();
			AccountLicenseAchieveResponse.DeserializeLengthDelimited(stream, accountLicenseAchieveResponse);
			return accountLicenseAchieveResponse;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000303CC File Offset: 0x0002E5CC
		public static AccountLicenseAchieveResponse DeserializeLengthDelimited(Stream stream, AccountLicenseAchieveResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountLicenseAchieveResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000303F4 File Offset: 0x0002E5F4
		public static AccountLicenseAchieveResponse Deserialize(Stream stream, AccountLicenseAchieveResponse instance, long limit)
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
						instance.Result_ = (AccountLicenseAchieveResponse.Result)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Achieve = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0003048D File Offset: 0x0002E68D
		public void Serialize(Stream stream)
		{
			AccountLicenseAchieveResponse.Serialize(stream, this);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00030496 File Offset: 0x0002E696
		public static void Serialize(Stream stream, AccountLicenseAchieveResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Achieve));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Result_));
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000304C1 File Offset: 0x0002E6C1
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Achieve)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Result_)) + 2U;
		}

		// Token: 0x020005C9 RID: 1481
		public enum PacketID
		{
			// Token: 0x04001FA6 RID: 8102
			ID = 311
		}

		// Token: 0x020005CA RID: 1482
		public enum Result
		{
			// Token: 0x04001FA8 RID: 8104
			INVALID_ACHIEVE = 1,
			// Token: 0x04001FA9 RID: 8105
			NOT_ACTIVE,
			// Token: 0x04001FAA RID: 8106
			IN_PROGRESS,
			// Token: 0x04001FAB RID: 8107
			COMPLETE,
			// Token: 0x04001FAC RID: 8108
			STATUS_UNKNOWN
		}
	}
}
