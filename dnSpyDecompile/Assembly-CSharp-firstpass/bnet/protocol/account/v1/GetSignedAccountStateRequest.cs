using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200050E RID: 1294
	public class GetSignedAccountStateRequest : IProtoBuf
	{
		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x06005C2C RID: 23596 RVA: 0x00118602 File Offset: 0x00116802
		// (set) Token: 0x06005C2D RID: 23597 RVA: 0x0011860A File Offset: 0x0011680A
		public AccountId Account
		{
			get
			{
				return this._Account;
			}
			set
			{
				this._Account = value;
				this.HasAccount = (value != null);
			}
		}

		// Token: 0x06005C2E RID: 23598 RVA: 0x0011861D File Offset: 0x0011681D
		public void SetAccount(AccountId val)
		{
			this.Account = val;
		}

		// Token: 0x06005C2F RID: 23599 RVA: 0x00118628 File Offset: 0x00116828
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccount)
			{
				num ^= this.Account.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005C30 RID: 23600 RVA: 0x00118658 File Offset: 0x00116858
		public override bool Equals(object obj)
		{
			GetSignedAccountStateRequest getSignedAccountStateRequest = obj as GetSignedAccountStateRequest;
			return getSignedAccountStateRequest != null && this.HasAccount == getSignedAccountStateRequest.HasAccount && (!this.HasAccount || this.Account.Equals(getSignedAccountStateRequest.Account));
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06005C31 RID: 23601 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005C32 RID: 23602 RVA: 0x0011869D File Offset: 0x0011689D
		public static GetSignedAccountStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetSignedAccountStateRequest>(bs, 0, -1);
		}

		// Token: 0x06005C33 RID: 23603 RVA: 0x001186A7 File Offset: 0x001168A7
		public void Deserialize(Stream stream)
		{
			GetSignedAccountStateRequest.Deserialize(stream, this);
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x001186B1 File Offset: 0x001168B1
		public static GetSignedAccountStateRequest Deserialize(Stream stream, GetSignedAccountStateRequest instance)
		{
			return GetSignedAccountStateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005C35 RID: 23605 RVA: 0x001186BC File Offset: 0x001168BC
		public static GetSignedAccountStateRequest DeserializeLengthDelimited(Stream stream)
		{
			GetSignedAccountStateRequest getSignedAccountStateRequest = new GetSignedAccountStateRequest();
			GetSignedAccountStateRequest.DeserializeLengthDelimited(stream, getSignedAccountStateRequest);
			return getSignedAccountStateRequest;
		}

		// Token: 0x06005C36 RID: 23606 RVA: 0x001186D8 File Offset: 0x001168D8
		public static GetSignedAccountStateRequest DeserializeLengthDelimited(Stream stream, GetSignedAccountStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetSignedAccountStateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005C37 RID: 23607 RVA: 0x00118700 File Offset: 0x00116900
		public static GetSignedAccountStateRequest Deserialize(Stream stream, GetSignedAccountStateRequest instance, long limit)
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
				else if (num == 10)
				{
					if (instance.Account == null)
					{
						instance.Account = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.Account);
					}
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

		// Token: 0x06005C38 RID: 23608 RVA: 0x0011879A File Offset: 0x0011699A
		public void Serialize(Stream stream)
		{
			GetSignedAccountStateRequest.Serialize(stream, this);
		}

		// Token: 0x06005C39 RID: 23609 RVA: 0x001187A3 File Offset: 0x001169A3
		public static void Serialize(Stream stream, GetSignedAccountStateRequest instance)
		{
			if (instance.HasAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
				AccountId.Serialize(stream, instance.Account);
			}
		}

		// Token: 0x06005C3A RID: 23610 RVA: 0x001187D4 File Offset: 0x001169D4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccount)
			{
				num += 1U;
				uint serializedSize = this.Account.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001C8B RID: 7307
		public bool HasAccount;

		// Token: 0x04001C8C RID: 7308
		private AccountId _Account;
	}
}
