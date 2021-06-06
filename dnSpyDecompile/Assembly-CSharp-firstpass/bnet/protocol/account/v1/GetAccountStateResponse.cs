using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200050D RID: 1293
	public class GetAccountStateResponse : IProtoBuf
	{
		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06005C19 RID: 23577 RVA: 0x00118301 File Offset: 0x00116501
		// (set) Token: 0x06005C1A RID: 23578 RVA: 0x00118309 File Offset: 0x00116509
		public AccountState State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
				this.HasState = (value != null);
			}
		}

		// Token: 0x06005C1B RID: 23579 RVA: 0x0011831C File Offset: 0x0011651C
		public void SetState(AccountState val)
		{
			this.State = val;
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06005C1C RID: 23580 RVA: 0x00118325 File Offset: 0x00116525
		// (set) Token: 0x06005C1D RID: 23581 RVA: 0x0011832D File Offset: 0x0011652D
		public AccountFieldTags Tags
		{
			get
			{
				return this._Tags;
			}
			set
			{
				this._Tags = value;
				this.HasTags = (value != null);
			}
		}

		// Token: 0x06005C1E RID: 23582 RVA: 0x00118340 File Offset: 0x00116540
		public void SetTags(AccountFieldTags val)
		{
			this.Tags = val;
		}

		// Token: 0x06005C1F RID: 23583 RVA: 0x0011834C File Offset: 0x0011654C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasState)
			{
				num ^= this.State.GetHashCode();
			}
			if (this.HasTags)
			{
				num ^= this.Tags.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005C20 RID: 23584 RVA: 0x00118394 File Offset: 0x00116594
		public override bool Equals(object obj)
		{
			GetAccountStateResponse getAccountStateResponse = obj as GetAccountStateResponse;
			return getAccountStateResponse != null && this.HasState == getAccountStateResponse.HasState && (!this.HasState || this.State.Equals(getAccountStateResponse.State)) && this.HasTags == getAccountStateResponse.HasTags && (!this.HasTags || this.Tags.Equals(getAccountStateResponse.Tags));
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x06005C21 RID: 23585 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005C22 RID: 23586 RVA: 0x00118404 File Offset: 0x00116604
		public static GetAccountStateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAccountStateResponse>(bs, 0, -1);
		}

		// Token: 0x06005C23 RID: 23587 RVA: 0x0011840E File Offset: 0x0011660E
		public void Deserialize(Stream stream)
		{
			GetAccountStateResponse.Deserialize(stream, this);
		}

		// Token: 0x06005C24 RID: 23588 RVA: 0x00118418 File Offset: 0x00116618
		public static GetAccountStateResponse Deserialize(Stream stream, GetAccountStateResponse instance)
		{
			return GetAccountStateResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005C25 RID: 23589 RVA: 0x00118424 File Offset: 0x00116624
		public static GetAccountStateResponse DeserializeLengthDelimited(Stream stream)
		{
			GetAccountStateResponse getAccountStateResponse = new GetAccountStateResponse();
			GetAccountStateResponse.DeserializeLengthDelimited(stream, getAccountStateResponse);
			return getAccountStateResponse;
		}

		// Token: 0x06005C26 RID: 23590 RVA: 0x00118440 File Offset: 0x00116640
		public static GetAccountStateResponse DeserializeLengthDelimited(Stream stream, GetAccountStateResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAccountStateResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005C27 RID: 23591 RVA: 0x00118468 File Offset: 0x00116668
		public static GetAccountStateResponse Deserialize(Stream stream, GetAccountStateResponse instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Tags == null)
					{
						instance.Tags = AccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountFieldTags.DeserializeLengthDelimited(stream, instance.Tags);
					}
				}
				else if (instance.State == null)
				{
					instance.State = AccountState.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountState.DeserializeLengthDelimited(stream, instance.State);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005C28 RID: 23592 RVA: 0x0011853A File Offset: 0x0011673A
		public void Serialize(Stream stream)
		{
			GetAccountStateResponse.Serialize(stream, this);
		}

		// Token: 0x06005C29 RID: 23593 RVA: 0x00118544 File Offset: 0x00116744
		public static void Serialize(Stream stream, GetAccountStateResponse instance)
		{
			if (instance.HasState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				AccountState.Serialize(stream, instance.State);
			}
			if (instance.HasTags)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Tags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.Tags);
			}
		}

		// Token: 0x06005C2A RID: 23594 RVA: 0x001185AC File Offset: 0x001167AC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasState)
			{
				num += 1U;
				uint serializedSize = this.State.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasTags)
			{
				num += 1U;
				uint serializedSize2 = this.Tags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001C87 RID: 7303
		public bool HasState;

		// Token: 0x04001C88 RID: 7304
		private AccountState _State;

		// Token: 0x04001C89 RID: 7305
		public bool HasTags;

		// Token: 0x04001C8A RID: 7306
		private AccountFieldTags _Tags;
	}
}
