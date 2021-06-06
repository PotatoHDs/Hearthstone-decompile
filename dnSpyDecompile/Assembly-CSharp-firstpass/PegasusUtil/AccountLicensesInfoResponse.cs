using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000C4 RID: 196
	public class AccountLicensesInfoResponse : IProtoBuf
	{
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00031DD0 File Offset: 0x0002FFD0
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x00031DD8 File Offset: 0x0002FFD8
		public List<AccountLicenseInfo> List
		{
			get
			{
				return this._List;
			}
			set
			{
				this._List = value;
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00031DE4 File Offset: 0x0002FFE4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AccountLicenseInfo accountLicenseInfo in this.List)
			{
				num ^= accountLicenseInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00031E48 File Offset: 0x00030048
		public override bool Equals(object obj)
		{
			AccountLicensesInfoResponse accountLicensesInfoResponse = obj as AccountLicensesInfoResponse;
			if (accountLicensesInfoResponse == null)
			{
				return false;
			}
			if (this.List.Count != accountLicensesInfoResponse.List.Count)
			{
				return false;
			}
			for (int i = 0; i < this.List.Count; i++)
			{
				if (!this.List[i].Equals(accountLicensesInfoResponse.List[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00031EB3 File Offset: 0x000300B3
		public void Deserialize(Stream stream)
		{
			AccountLicensesInfoResponse.Deserialize(stream, this);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00031EBD File Offset: 0x000300BD
		public static AccountLicensesInfoResponse Deserialize(Stream stream, AccountLicensesInfoResponse instance)
		{
			return AccountLicensesInfoResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00031EC8 File Offset: 0x000300C8
		public static AccountLicensesInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			AccountLicensesInfoResponse accountLicensesInfoResponse = new AccountLicensesInfoResponse();
			AccountLicensesInfoResponse.DeserializeLengthDelimited(stream, accountLicensesInfoResponse);
			return accountLicensesInfoResponse;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00031EE4 File Offset: 0x000300E4
		public static AccountLicensesInfoResponse DeserializeLengthDelimited(Stream stream, AccountLicensesInfoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountLicensesInfoResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00031F0C File Offset: 0x0003010C
		public static AccountLicensesInfoResponse Deserialize(Stream stream, AccountLicensesInfoResponse instance, long limit)
		{
			if (instance.List == null)
			{
				instance.List = new List<AccountLicenseInfo>();
			}
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
					instance.List.Add(AccountLicenseInfo.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000D7F RID: 3455 RVA: 0x00031FA4 File Offset: 0x000301A4
		public void Serialize(Stream stream)
		{
			AccountLicensesInfoResponse.Serialize(stream, this);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00031FB0 File Offset: 0x000301B0
		public static void Serialize(Stream stream, AccountLicensesInfoResponse instance)
		{
			if (instance.List.Count > 0)
			{
				foreach (AccountLicenseInfo accountLicenseInfo in instance.List)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, accountLicenseInfo.GetSerializedSize());
					AccountLicenseInfo.Serialize(stream, accountLicenseInfo);
				}
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00032028 File Offset: 0x00030228
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.List.Count > 0)
			{
				foreach (AccountLicenseInfo accountLicenseInfo in this.List)
				{
					num += 1U;
					uint serializedSize = accountLicenseInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x0400049A RID: 1178
		private List<AccountLicenseInfo> _List = new List<AccountLicenseInfo>();

		// Token: 0x020005D3 RID: 1491
		public enum PacketID
		{
			// Token: 0x04001FBF RID: 8127
			ID = 325
		}
	}
}
