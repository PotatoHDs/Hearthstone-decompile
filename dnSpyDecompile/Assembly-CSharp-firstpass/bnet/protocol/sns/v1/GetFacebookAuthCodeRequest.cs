using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.sns.v1
{
	// Token: 0x020002F9 RID: 761
	public class GetFacebookAuthCodeRequest : IProtoBuf
	{
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002D9E RID: 11678 RVA: 0x0009C26B File Offset: 0x0009A46B
		// (set) Token: 0x06002D9F RID: 11679 RVA: 0x0009C273 File Offset: 0x0009A473
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

		// Token: 0x06002DA0 RID: 11680 RVA: 0x0009C286 File Offset: 0x0009A486
		public void SetAccount(AccountId val)
		{
			this.Account = val;
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x0009C290 File Offset: 0x0009A490
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccount)
			{
				num ^= this.Account.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x0009C2C0 File Offset: 0x0009A4C0
		public override bool Equals(object obj)
		{
			GetFacebookAuthCodeRequest getFacebookAuthCodeRequest = obj as GetFacebookAuthCodeRequest;
			return getFacebookAuthCodeRequest != null && this.HasAccount == getFacebookAuthCodeRequest.HasAccount && (!this.HasAccount || this.Account.Equals(getFacebookAuthCodeRequest.Account));
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002DA3 RID: 11683 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x0009C305 File Offset: 0x0009A505
		public static GetFacebookAuthCodeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFacebookAuthCodeRequest>(bs, 0, -1);
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x0009C30F File Offset: 0x0009A50F
		public void Deserialize(Stream stream)
		{
			GetFacebookAuthCodeRequest.Deserialize(stream, this);
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x0009C319 File Offset: 0x0009A519
		public static GetFacebookAuthCodeRequest Deserialize(Stream stream, GetFacebookAuthCodeRequest instance)
		{
			return GetFacebookAuthCodeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x0009C324 File Offset: 0x0009A524
		public static GetFacebookAuthCodeRequest DeserializeLengthDelimited(Stream stream)
		{
			GetFacebookAuthCodeRequest getFacebookAuthCodeRequest = new GetFacebookAuthCodeRequest();
			GetFacebookAuthCodeRequest.DeserializeLengthDelimited(stream, getFacebookAuthCodeRequest);
			return getFacebookAuthCodeRequest;
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x0009C340 File Offset: 0x0009A540
		public static GetFacebookAuthCodeRequest DeserializeLengthDelimited(Stream stream, GetFacebookAuthCodeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFacebookAuthCodeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x0009C368 File Offset: 0x0009A568
		public static GetFacebookAuthCodeRequest Deserialize(Stream stream, GetFacebookAuthCodeRequest instance, long limit)
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

		// Token: 0x06002DAA RID: 11690 RVA: 0x0009C402 File Offset: 0x0009A602
		public void Serialize(Stream stream)
		{
			GetFacebookAuthCodeRequest.Serialize(stream, this);
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x0009C40B File Offset: 0x0009A60B
		public static void Serialize(Stream stream, GetFacebookAuthCodeRequest instance)
		{
			if (instance.HasAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
				AccountId.Serialize(stream, instance.Account);
			}
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x0009C43C File Offset: 0x0009A63C
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

		// Token: 0x04001299 RID: 4761
		public bool HasAccount;

		// Token: 0x0400129A RID: 4762
		private AccountId _Account;
	}
}
