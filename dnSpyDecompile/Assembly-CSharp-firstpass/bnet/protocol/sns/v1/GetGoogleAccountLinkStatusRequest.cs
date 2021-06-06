using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.sns.v1
{
	// Token: 0x02000302 RID: 770
	public class GetGoogleAccountLinkStatusRequest : IProtoBuf
	{
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x0009DB50 File Offset: 0x0009BD50
		// (set) Token: 0x06002E4A RID: 11850 RVA: 0x0009DB58 File Offset: 0x0009BD58
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

		// Token: 0x06002E4B RID: 11851 RVA: 0x0009DB6B File Offset: 0x0009BD6B
		public void SetAccount(AccountId val)
		{
			this.Account = val;
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x0009DB74 File Offset: 0x0009BD74
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccount)
			{
				num ^= this.Account.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x0009DBA4 File Offset: 0x0009BDA4
		public override bool Equals(object obj)
		{
			GetGoogleAccountLinkStatusRequest getGoogleAccountLinkStatusRequest = obj as GetGoogleAccountLinkStatusRequest;
			return getGoogleAccountLinkStatusRequest != null && this.HasAccount == getGoogleAccountLinkStatusRequest.HasAccount && (!this.HasAccount || this.Account.Equals(getGoogleAccountLinkStatusRequest.Account));
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x0009DBE9 File Offset: 0x0009BDE9
		public static GetGoogleAccountLinkStatusRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGoogleAccountLinkStatusRequest>(bs, 0, -1);
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x0009DBF3 File Offset: 0x0009BDF3
		public void Deserialize(Stream stream)
		{
			GetGoogleAccountLinkStatusRequest.Deserialize(stream, this);
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x0009DBFD File Offset: 0x0009BDFD
		public static GetGoogleAccountLinkStatusRequest Deserialize(Stream stream, GetGoogleAccountLinkStatusRequest instance)
		{
			return GetGoogleAccountLinkStatusRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x0009DC08 File Offset: 0x0009BE08
		public static GetGoogleAccountLinkStatusRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGoogleAccountLinkStatusRequest getGoogleAccountLinkStatusRequest = new GetGoogleAccountLinkStatusRequest();
			GetGoogleAccountLinkStatusRequest.DeserializeLengthDelimited(stream, getGoogleAccountLinkStatusRequest);
			return getGoogleAccountLinkStatusRequest;
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x0009DC24 File Offset: 0x0009BE24
		public static GetGoogleAccountLinkStatusRequest DeserializeLengthDelimited(Stream stream, GetGoogleAccountLinkStatusRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGoogleAccountLinkStatusRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x0009DC4C File Offset: 0x0009BE4C
		public static GetGoogleAccountLinkStatusRequest Deserialize(Stream stream, GetGoogleAccountLinkStatusRequest instance, long limit)
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

		// Token: 0x06002E55 RID: 11861 RVA: 0x0009DCE6 File Offset: 0x0009BEE6
		public void Serialize(Stream stream)
		{
			GetGoogleAccountLinkStatusRequest.Serialize(stream, this);
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x0009DCEF File Offset: 0x0009BEEF
		public static void Serialize(Stream stream, GetGoogleAccountLinkStatusRequest instance)
		{
			if (instance.HasAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
				AccountId.Serialize(stream, instance.Account);
			}
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x0009DD20 File Offset: 0x0009BF20
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

		// Token: 0x040012BD RID: 4797
		public bool HasAccount;

		// Token: 0x040012BE RID: 4798
		private AccountId _Account;
	}
}
