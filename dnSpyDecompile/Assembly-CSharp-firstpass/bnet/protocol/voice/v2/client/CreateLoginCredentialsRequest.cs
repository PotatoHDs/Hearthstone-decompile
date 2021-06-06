using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.voice.v2.client
{
	// Token: 0x020002CB RID: 715
	public class CreateLoginCredentialsRequest : IProtoBuf
	{
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060029D9 RID: 10713 RVA: 0x000926A3 File Offset: 0x000908A3
		// (set) Token: 0x060029DA RID: 10714 RVA: 0x000926AB File Offset: 0x000908AB
		public AccountId AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				this._AccountId = value;
				this.HasAccountId = (value != null);
			}
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000926BE File Offset: 0x000908BE
		public void SetAccountId(AccountId val)
		{
			this.AccountId = val;
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x000926C8 File Offset: 0x000908C8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000926F8 File Offset: 0x000908F8
		public override bool Equals(object obj)
		{
			CreateLoginCredentialsRequest createLoginCredentialsRequest = obj as CreateLoginCredentialsRequest;
			return createLoginCredentialsRequest != null && this.HasAccountId == createLoginCredentialsRequest.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(createLoginCredentialsRequest.AccountId));
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060029DE RID: 10718 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x0009273D File Offset: 0x0009093D
		public static CreateLoginCredentialsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateLoginCredentialsRequest>(bs, 0, -1);
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x00092747 File Offset: 0x00090947
		public void Deserialize(Stream stream)
		{
			CreateLoginCredentialsRequest.Deserialize(stream, this);
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x00092751 File Offset: 0x00090951
		public static CreateLoginCredentialsRequest Deserialize(Stream stream, CreateLoginCredentialsRequest instance)
		{
			return CreateLoginCredentialsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x0009275C File Offset: 0x0009095C
		public static CreateLoginCredentialsRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateLoginCredentialsRequest createLoginCredentialsRequest = new CreateLoginCredentialsRequest();
			CreateLoginCredentialsRequest.DeserializeLengthDelimited(stream, createLoginCredentialsRequest);
			return createLoginCredentialsRequest;
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x00092778 File Offset: 0x00090978
		public static CreateLoginCredentialsRequest DeserializeLengthDelimited(Stream stream, CreateLoginCredentialsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateLoginCredentialsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000927A0 File Offset: 0x000909A0
		public static CreateLoginCredentialsRequest Deserialize(Stream stream, CreateLoginCredentialsRequest instance, long limit)
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
					if (instance.AccountId == null)
					{
						instance.AccountId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.AccountId);
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

		// Token: 0x060029E5 RID: 10725 RVA: 0x0009283A File Offset: 0x00090A3A
		public void Serialize(Stream stream)
		{
			CreateLoginCredentialsRequest.Serialize(stream, this);
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x00092843 File Offset: 0x00090A43
		public static void Serialize(Stream stream, CreateLoginCredentialsRequest instance)
		{
			if (instance.HasAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				AccountId.Serialize(stream, instance.AccountId);
			}
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x00092874 File Offset: 0x00090A74
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize = this.AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x040011DB RID: 4571
		public bool HasAccountId;

		// Token: 0x040011DC RID: 4572
		private AccountId _AccountId;
	}
}
