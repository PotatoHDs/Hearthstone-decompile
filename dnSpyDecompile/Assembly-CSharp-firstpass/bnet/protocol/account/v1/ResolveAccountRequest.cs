using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000506 RID: 1286
	public class ResolveAccountRequest : IProtoBuf
	{
		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06005B89 RID: 23433 RVA: 0x00116C83 File Offset: 0x00114E83
		// (set) Token: 0x06005B8A RID: 23434 RVA: 0x00116C8B File Offset: 0x00114E8B
		public AccountReference Ref
		{
			get
			{
				return this._Ref;
			}
			set
			{
				this._Ref = value;
				this.HasRef = (value != null);
			}
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x00116C9E File Offset: 0x00114E9E
		public void SetRef(AccountReference val)
		{
			this.Ref = val;
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06005B8C RID: 23436 RVA: 0x00116CA7 File Offset: 0x00114EA7
		// (set) Token: 0x06005B8D RID: 23437 RVA: 0x00116CAF File Offset: 0x00114EAF
		public bool FetchId
		{
			get
			{
				return this._FetchId;
			}
			set
			{
				this._FetchId = value;
				this.HasFetchId = true;
			}
		}

		// Token: 0x06005B8E RID: 23438 RVA: 0x00116CBF File Offset: 0x00114EBF
		public void SetFetchId(bool val)
		{
			this.FetchId = val;
		}

		// Token: 0x06005B8F RID: 23439 RVA: 0x00116CC8 File Offset: 0x00114EC8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRef)
			{
				num ^= this.Ref.GetHashCode();
			}
			if (this.HasFetchId)
			{
				num ^= this.FetchId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x00116D14 File Offset: 0x00114F14
		public override bool Equals(object obj)
		{
			ResolveAccountRequest resolveAccountRequest = obj as ResolveAccountRequest;
			return resolveAccountRequest != null && this.HasRef == resolveAccountRequest.HasRef && (!this.HasRef || this.Ref.Equals(resolveAccountRequest.Ref)) && this.HasFetchId == resolveAccountRequest.HasFetchId && (!this.HasFetchId || this.FetchId.Equals(resolveAccountRequest.FetchId));
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x06005B91 RID: 23441 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x00116D87 File Offset: 0x00114F87
		public static ResolveAccountRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ResolveAccountRequest>(bs, 0, -1);
		}

		// Token: 0x06005B93 RID: 23443 RVA: 0x00116D91 File Offset: 0x00114F91
		public void Deserialize(Stream stream)
		{
			ResolveAccountRequest.Deserialize(stream, this);
		}

		// Token: 0x06005B94 RID: 23444 RVA: 0x00116D9B File Offset: 0x00114F9B
		public static ResolveAccountRequest Deserialize(Stream stream, ResolveAccountRequest instance)
		{
			return ResolveAccountRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005B95 RID: 23445 RVA: 0x00116DA8 File Offset: 0x00114FA8
		public static ResolveAccountRequest DeserializeLengthDelimited(Stream stream)
		{
			ResolveAccountRequest resolveAccountRequest = new ResolveAccountRequest();
			ResolveAccountRequest.DeserializeLengthDelimited(stream, resolveAccountRequest);
			return resolveAccountRequest;
		}

		// Token: 0x06005B96 RID: 23446 RVA: 0x00116DC4 File Offset: 0x00114FC4
		public static ResolveAccountRequest DeserializeLengthDelimited(Stream stream, ResolveAccountRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ResolveAccountRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005B97 RID: 23447 RVA: 0x00116DEC File Offset: 0x00114FEC
		public static ResolveAccountRequest Deserialize(Stream stream, ResolveAccountRequest instance, long limit)
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
					if (num != 96)
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
						instance.FetchId = ProtocolParser.ReadBool(stream);
					}
				}
				else if (instance.Ref == null)
				{
					instance.Ref = AccountReference.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountReference.DeserializeLengthDelimited(stream, instance.Ref);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005B98 RID: 23448 RVA: 0x00116E9E File Offset: 0x0011509E
		public void Serialize(Stream stream)
		{
			ResolveAccountRequest.Serialize(stream, this);
		}

		// Token: 0x06005B99 RID: 23449 RVA: 0x00116EA8 File Offset: 0x001150A8
		public static void Serialize(Stream stream, ResolveAccountRequest instance)
		{
			if (instance.HasRef)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Ref.GetSerializedSize());
				AccountReference.Serialize(stream, instance.Ref);
			}
			if (instance.HasFetchId)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteBool(stream, instance.FetchId);
			}
		}

		// Token: 0x06005B9A RID: 23450 RVA: 0x00116F00 File Offset: 0x00115100
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRef)
			{
				num += 1U;
				uint serializedSize = this.Ref.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasFetchId)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001C6B RID: 7275
		public bool HasRef;

		// Token: 0x04001C6C RID: 7276
		private AccountReference _Ref;

		// Token: 0x04001C6D RID: 7277
		public bool HasFetchId;

		// Token: 0x04001C6E RID: 7278
		private bool _FetchId;
	}
}
