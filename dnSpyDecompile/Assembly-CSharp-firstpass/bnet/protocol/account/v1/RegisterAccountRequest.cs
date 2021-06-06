using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000504 RID: 1284
	public class RegisterAccountRequest : IProtoBuf
	{
		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06005B69 RID: 23401 RVA: 0x00116879 File Offset: 0x00114A79
		// (set) Token: 0x06005B6A RID: 23402 RVA: 0x00116881 File Offset: 0x00114A81
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

		// Token: 0x06005B6B RID: 23403 RVA: 0x00116894 File Offset: 0x00114A94
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x06005B6C RID: 23404 RVA: 0x001168A0 File Offset: 0x00114AA0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005B6D RID: 23405 RVA: 0x001168D0 File Offset: 0x00114AD0
		public override bool Equals(object obj)
		{
			RegisterAccountRequest registerAccountRequest = obj as RegisterAccountRequest;
			return registerAccountRequest != null && this.HasIdentity == registerAccountRequest.HasIdentity && (!this.HasIdentity || this.Identity.Equals(registerAccountRequest.Identity));
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x06005B6E RID: 23406 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005B6F RID: 23407 RVA: 0x00116915 File Offset: 0x00114B15
		public static RegisterAccountRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterAccountRequest>(bs, 0, -1);
		}

		// Token: 0x06005B70 RID: 23408 RVA: 0x0011691F File Offset: 0x00114B1F
		public void Deserialize(Stream stream)
		{
			RegisterAccountRequest.Deserialize(stream, this);
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x00116929 File Offset: 0x00114B29
		public static RegisterAccountRequest Deserialize(Stream stream, RegisterAccountRequest instance)
		{
			return RegisterAccountRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005B72 RID: 23410 RVA: 0x00116934 File Offset: 0x00114B34
		public static RegisterAccountRequest DeserializeLengthDelimited(Stream stream)
		{
			RegisterAccountRequest registerAccountRequest = new RegisterAccountRequest();
			RegisterAccountRequest.DeserializeLengthDelimited(stream, registerAccountRequest);
			return registerAccountRequest;
		}

		// Token: 0x06005B73 RID: 23411 RVA: 0x00116950 File Offset: 0x00114B50
		public static RegisterAccountRequest DeserializeLengthDelimited(Stream stream, RegisterAccountRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegisterAccountRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005B74 RID: 23412 RVA: 0x00116978 File Offset: 0x00114B78
		public static RegisterAccountRequest Deserialize(Stream stream, RegisterAccountRequest instance, long limit)
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

		// Token: 0x06005B75 RID: 23413 RVA: 0x00116A12 File Offset: 0x00114C12
		public void Serialize(Stream stream)
		{
			RegisterAccountRequest.Serialize(stream, this);
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x00116A1B File Offset: 0x00114C1B
		public static void Serialize(Stream stream, RegisterAccountRequest instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x00116A4C File Offset: 0x00114C4C
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

		// Token: 0x04001C67 RID: 7271
		public bool HasIdentity;

		// Token: 0x04001C68 RID: 7272
		private Identity _Identity;
	}
}
