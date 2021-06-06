using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000CC RID: 204
	public class UpdateAccountLicensesResponse : IProtoBuf
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0003368B File Offset: 0x0003188B
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x00033693 File Offset: 0x00031893
		public bool FixedLicenseSuccess { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0003369C File Offset: 0x0003189C
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x000336A4 File Offset: 0x000318A4
		public bool ConsumableLicenseSuccess { get; set; }

		// Token: 0x06000DF6 RID: 3574 RVA: 0x000336B0 File Offset: 0x000318B0
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.FixedLicenseSuccess.GetHashCode() ^ this.ConsumableLicenseSuccess.GetHashCode();
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x000336E8 File Offset: 0x000318E8
		public override bool Equals(object obj)
		{
			UpdateAccountLicensesResponse updateAccountLicensesResponse = obj as UpdateAccountLicensesResponse;
			return updateAccountLicensesResponse != null && this.FixedLicenseSuccess.Equals(updateAccountLicensesResponse.FixedLicenseSuccess) && this.ConsumableLicenseSuccess.Equals(updateAccountLicensesResponse.ConsumableLicenseSuccess);
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00033732 File Offset: 0x00031932
		public void Deserialize(Stream stream)
		{
			UpdateAccountLicensesResponse.Deserialize(stream, this);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003373C File Offset: 0x0003193C
		public static UpdateAccountLicensesResponse Deserialize(Stream stream, UpdateAccountLicensesResponse instance)
		{
			return UpdateAccountLicensesResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00033748 File Offset: 0x00031948
		public static UpdateAccountLicensesResponse DeserializeLengthDelimited(Stream stream)
		{
			UpdateAccountLicensesResponse updateAccountLicensesResponse = new UpdateAccountLicensesResponse();
			UpdateAccountLicensesResponse.DeserializeLengthDelimited(stream, updateAccountLicensesResponse);
			return updateAccountLicensesResponse;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00033764 File Offset: 0x00031964
		public static UpdateAccountLicensesResponse DeserializeLengthDelimited(Stream stream, UpdateAccountLicensesResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateAccountLicensesResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0003378C File Offset: 0x0003198C
		public static UpdateAccountLicensesResponse Deserialize(Stream stream, UpdateAccountLicensesResponse instance, long limit)
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
						instance.ConsumableLicenseSuccess = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.FixedLicenseSuccess = ProtocolParser.ReadBool(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00033823 File Offset: 0x00031A23
		public void Serialize(Stream stream)
		{
			UpdateAccountLicensesResponse.Serialize(stream, this);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0003382C File Offset: 0x00031A2C
		public static void Serialize(Stream stream, UpdateAccountLicensesResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.FixedLicenseSuccess);
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.ConsumableLicenseSuccess);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00033855 File Offset: 0x00031A55
		public uint GetSerializedSize()
		{
			return 0U + 1U + 1U + 2U;
		}

		// Token: 0x020005DB RID: 1499
		public enum PacketID
		{
			// Token: 0x04001FDC RID: 8156
			ID = 331
		}
	}
}
