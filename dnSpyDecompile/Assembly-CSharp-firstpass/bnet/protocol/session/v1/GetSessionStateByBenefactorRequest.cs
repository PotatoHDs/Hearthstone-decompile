using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	// Token: 0x0200030B RID: 779
	public class GetSessionStateByBenefactorRequest : IProtoBuf
	{
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002F28 RID: 12072 RVA: 0x000A024F File Offset: 0x0009E44F
		// (set) Token: 0x06002F29 RID: 12073 RVA: 0x000A0257 File Offset: 0x0009E457
		public GameAccountHandle BenefactorHandle
		{
			get
			{
				return this._BenefactorHandle;
			}
			set
			{
				this._BenefactorHandle = value;
				this.HasBenefactorHandle = (value != null);
			}
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x000A026A File Offset: 0x0009E46A
		public void SetBenefactorHandle(GameAccountHandle val)
		{
			this.BenefactorHandle = val;
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x000A0273 File Offset: 0x0009E473
		// (set) Token: 0x06002F2C RID: 12076 RVA: 0x000A027B File Offset: 0x0009E47B
		public bool IncludeBillingDisabled
		{
			get
			{
				return this._IncludeBillingDisabled;
			}
			set
			{
				this._IncludeBillingDisabled = value;
				this.HasIncludeBillingDisabled = true;
			}
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x000A028B File Offset: 0x0009E48B
		public void SetIncludeBillingDisabled(bool val)
		{
			this.IncludeBillingDisabled = val;
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x000A0294 File Offset: 0x0009E494
		// (set) Token: 0x06002F2F RID: 12079 RVA: 0x000A029C File Offset: 0x0009E49C
		public string BenefactorUuid
		{
			get
			{
				return this._BenefactorUuid;
			}
			set
			{
				this._BenefactorUuid = value;
				this.HasBenefactorUuid = (value != null);
			}
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x000A02AF File Offset: 0x0009E4AF
		public void SetBenefactorUuid(string val)
		{
			this.BenefactorUuid = val;
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x000A02B8 File Offset: 0x0009E4B8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBenefactorHandle)
			{
				num ^= this.BenefactorHandle.GetHashCode();
			}
			if (this.HasIncludeBillingDisabled)
			{
				num ^= this.IncludeBillingDisabled.GetHashCode();
			}
			if (this.HasBenefactorUuid)
			{
				num ^= this.BenefactorUuid.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000A0318 File Offset: 0x0009E518
		public override bool Equals(object obj)
		{
			GetSessionStateByBenefactorRequest getSessionStateByBenefactorRequest = obj as GetSessionStateByBenefactorRequest;
			return getSessionStateByBenefactorRequest != null && this.HasBenefactorHandle == getSessionStateByBenefactorRequest.HasBenefactorHandle && (!this.HasBenefactorHandle || this.BenefactorHandle.Equals(getSessionStateByBenefactorRequest.BenefactorHandle)) && this.HasIncludeBillingDisabled == getSessionStateByBenefactorRequest.HasIncludeBillingDisabled && (!this.HasIncludeBillingDisabled || this.IncludeBillingDisabled.Equals(getSessionStateByBenefactorRequest.IncludeBillingDisabled)) && this.HasBenefactorUuid == getSessionStateByBenefactorRequest.HasBenefactorUuid && (!this.HasBenefactorUuid || this.BenefactorUuid.Equals(getSessionStateByBenefactorRequest.BenefactorUuid));
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x000A03B6 File Offset: 0x0009E5B6
		public static GetSessionStateByBenefactorRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetSessionStateByBenefactorRequest>(bs, 0, -1);
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000A03C0 File Offset: 0x0009E5C0
		public void Deserialize(Stream stream)
		{
			GetSessionStateByBenefactorRequest.Deserialize(stream, this);
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x000A03CA File Offset: 0x0009E5CA
		public static GetSessionStateByBenefactorRequest Deserialize(Stream stream, GetSessionStateByBenefactorRequest instance)
		{
			return GetSessionStateByBenefactorRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x000A03D8 File Offset: 0x0009E5D8
		public static GetSessionStateByBenefactorRequest DeserializeLengthDelimited(Stream stream)
		{
			GetSessionStateByBenefactorRequest getSessionStateByBenefactorRequest = new GetSessionStateByBenefactorRequest();
			GetSessionStateByBenefactorRequest.DeserializeLengthDelimited(stream, getSessionStateByBenefactorRequest);
			return getSessionStateByBenefactorRequest;
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x000A03F4 File Offset: 0x0009E5F4
		public static GetSessionStateByBenefactorRequest DeserializeLengthDelimited(Stream stream, GetSessionStateByBenefactorRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetSessionStateByBenefactorRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x000A041C File Offset: 0x0009E61C
		public static GetSessionStateByBenefactorRequest Deserialize(Stream stream, GetSessionStateByBenefactorRequest instance, long limit)
		{
			instance.IncludeBillingDisabled = false;
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
				else if (num != 10)
				{
					if (num != 16)
					{
						if (num != 26)
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
							instance.BenefactorUuid = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.IncludeBillingDisabled = ProtocolParser.ReadBool(stream);
					}
				}
				else if (instance.BenefactorHandle == null)
				{
					instance.BenefactorHandle = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.BenefactorHandle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x000A04F1 File Offset: 0x0009E6F1
		public void Serialize(Stream stream)
		{
			GetSessionStateByBenefactorRequest.Serialize(stream, this);
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000A04FC File Offset: 0x0009E6FC
		public static void Serialize(Stream stream, GetSessionStateByBenefactorRequest instance)
		{
			if (instance.HasBenefactorHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.BenefactorHandle.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.BenefactorHandle);
			}
			if (instance.HasIncludeBillingDisabled)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.IncludeBillingDisabled);
			}
			if (instance.HasBenefactorUuid)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BenefactorUuid));
			}
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x000A0578 File Offset: 0x0009E778
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasBenefactorHandle)
			{
				num += 1U;
				uint serializedSize = this.BenefactorHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasIncludeBillingDisabled)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasBenefactorUuid)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.BenefactorUuid);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04001300 RID: 4864
		public bool HasBenefactorHandle;

		// Token: 0x04001301 RID: 4865
		private GameAccountHandle _BenefactorHandle;

		// Token: 0x04001302 RID: 4866
		public bool HasIncludeBillingDisabled;

		// Token: 0x04001303 RID: 4867
		private bool _IncludeBillingDisabled;

		// Token: 0x04001304 RID: 4868
		public bool HasBenefactorUuid;

		// Token: 0x04001305 RID: 4869
		private string _BenefactorUuid;
	}
}
