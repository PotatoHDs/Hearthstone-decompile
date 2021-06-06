using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000507 RID: 1287
	public class ResolveAccountResponse : IProtoBuf
	{
		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06005B9C RID: 23452 RVA: 0x00116F43 File Offset: 0x00115143
		// (set) Token: 0x06005B9D RID: 23453 RVA: 0x00116F4B File Offset: 0x0011514B
		public AccountId Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = (value != null);
			}
		}

		// Token: 0x06005B9E RID: 23454 RVA: 0x00116F5E File Offset: 0x0011515E
		public void SetId(AccountId val)
		{
			this.Id = val;
		}

		// Token: 0x06005B9F RID: 23455 RVA: 0x00116F68 File Offset: 0x00115168
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005BA0 RID: 23456 RVA: 0x00116F98 File Offset: 0x00115198
		public override bool Equals(object obj)
		{
			ResolveAccountResponse resolveAccountResponse = obj as ResolveAccountResponse;
			return resolveAccountResponse != null && this.HasId == resolveAccountResponse.HasId && (!this.HasId || this.Id.Equals(resolveAccountResponse.Id));
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06005BA1 RID: 23457 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005BA2 RID: 23458 RVA: 0x00116FDD File Offset: 0x001151DD
		public static ResolveAccountResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ResolveAccountResponse>(bs, 0, -1);
		}

		// Token: 0x06005BA3 RID: 23459 RVA: 0x00116FE7 File Offset: 0x001151E7
		public void Deserialize(Stream stream)
		{
			ResolveAccountResponse.Deserialize(stream, this);
		}

		// Token: 0x06005BA4 RID: 23460 RVA: 0x00116FF1 File Offset: 0x001151F1
		public static ResolveAccountResponse Deserialize(Stream stream, ResolveAccountResponse instance)
		{
			return ResolveAccountResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005BA5 RID: 23461 RVA: 0x00116FFC File Offset: 0x001151FC
		public static ResolveAccountResponse DeserializeLengthDelimited(Stream stream)
		{
			ResolveAccountResponse resolveAccountResponse = new ResolveAccountResponse();
			ResolveAccountResponse.DeserializeLengthDelimited(stream, resolveAccountResponse);
			return resolveAccountResponse;
		}

		// Token: 0x06005BA6 RID: 23462 RVA: 0x00117018 File Offset: 0x00115218
		public static ResolveAccountResponse DeserializeLengthDelimited(Stream stream, ResolveAccountResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ResolveAccountResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005BA7 RID: 23463 RVA: 0x00117040 File Offset: 0x00115240
		public static ResolveAccountResponse Deserialize(Stream stream, ResolveAccountResponse instance, long limit)
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
				else if (num == 98)
				{
					if (instance.Id == null)
					{
						instance.Id = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.Id);
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

		// Token: 0x06005BA8 RID: 23464 RVA: 0x001170DA File Offset: 0x001152DA
		public void Serialize(Stream stream)
		{
			ResolveAccountResponse.Serialize(stream, this);
		}

		// Token: 0x06005BA9 RID: 23465 RVA: 0x001170E3 File Offset: 0x001152E3
		public static void Serialize(Stream stream, ResolveAccountResponse instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(98);
				ProtocolParser.WriteUInt32(stream, instance.Id.GetSerializedSize());
				AccountId.Serialize(stream, instance.Id);
			}
		}

		// Token: 0x06005BAA RID: 23466 RVA: 0x00117114 File Offset: 0x00115314
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				uint serializedSize = this.Id.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001C6F RID: 7279
		public bool HasId;

		// Token: 0x04001C70 RID: 7280
		private AccountId _Id;
	}
}
