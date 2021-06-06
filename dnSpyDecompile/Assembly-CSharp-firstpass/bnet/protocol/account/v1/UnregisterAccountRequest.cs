using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000505 RID: 1285
	public class UnregisterAccountRequest : IProtoBuf
	{
		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06005B79 RID: 23417 RVA: 0x00116A7F File Offset: 0x00114C7F
		// (set) Token: 0x06005B7A RID: 23418 RVA: 0x00116A87 File Offset: 0x00114C87
		public Identity Identity
		{
			get
			{
				return this._Identity;
			}
			set
			{
				this._Identity = value;
				this.HasIdentity = (value != null);
			}
		}

		// Token: 0x06005B7B RID: 23419 RVA: 0x00116A9A File Offset: 0x00114C9A
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x00116AA4 File Offset: 0x00114CA4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x00116AD4 File Offset: 0x00114CD4
		public override bool Equals(object obj)
		{
			UnregisterAccountRequest unregisterAccountRequest = obj as UnregisterAccountRequest;
			return unregisterAccountRequest != null && this.HasIdentity == unregisterAccountRequest.HasIdentity && (!this.HasIdentity || this.Identity.Equals(unregisterAccountRequest.Identity));
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x06005B7E RID: 23422 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005B7F RID: 23423 RVA: 0x00116B19 File Offset: 0x00114D19
		public static UnregisterAccountRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnregisterAccountRequest>(bs, 0, -1);
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x00116B23 File Offset: 0x00114D23
		public void Deserialize(Stream stream)
		{
			UnregisterAccountRequest.Deserialize(stream, this);
		}

		// Token: 0x06005B81 RID: 23425 RVA: 0x00116B2D File Offset: 0x00114D2D
		public static UnregisterAccountRequest Deserialize(Stream stream, UnregisterAccountRequest instance)
		{
			return UnregisterAccountRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005B82 RID: 23426 RVA: 0x00116B38 File Offset: 0x00114D38
		public static UnregisterAccountRequest DeserializeLengthDelimited(Stream stream)
		{
			UnregisterAccountRequest unregisterAccountRequest = new UnregisterAccountRequest();
			UnregisterAccountRequest.DeserializeLengthDelimited(stream, unregisterAccountRequest);
			return unregisterAccountRequest;
		}

		// Token: 0x06005B83 RID: 23427 RVA: 0x00116B54 File Offset: 0x00114D54
		public static UnregisterAccountRequest DeserializeLengthDelimited(Stream stream, UnregisterAccountRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnregisterAccountRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x00116B7C File Offset: 0x00114D7C
		public static UnregisterAccountRequest Deserialize(Stream stream, UnregisterAccountRequest instance, long limit)
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
					if (instance.Identity == null)
					{
						instance.Identity = Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						Identity.DeserializeLengthDelimited(stream, instance.Identity);
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

		// Token: 0x06005B85 RID: 23429 RVA: 0x00116C16 File Offset: 0x00114E16
		public void Serialize(Stream stream)
		{
			UnregisterAccountRequest.Serialize(stream, this);
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x00116C1F File Offset: 0x00114E1F
		public static void Serialize(Stream stream, UnregisterAccountRequest instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
		}

		// Token: 0x06005B87 RID: 23431 RVA: 0x00116C50 File Offset: 0x00114E50
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIdentity)
			{
				num += 1U;
				uint serializedSize = this.Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001C69 RID: 7273
		public bool HasIdentity;

		// Token: 0x04001C6A RID: 7274
		private Identity _Identity;
	}
}
