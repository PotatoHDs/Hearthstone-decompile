using System;
using System.IO;

namespace bnet.protocol.voice.v2.client
{
	// Token: 0x020002CC RID: 716
	public class CreateLoginCredentialsResponse : IProtoBuf
	{
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060029E9 RID: 10729 RVA: 0x000928A7 File Offset: 0x00090AA7
		// (set) Token: 0x060029EA RID: 10730 RVA: 0x000928AF File Offset: 0x00090AAF
		public VoiceCredentials Credentials
		{
			get
			{
				return this._Credentials;
			}
			set
			{
				this._Credentials = value;
				this.HasCredentials = (value != null);
			}
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000928C2 File Offset: 0x00090AC2
		public void SetCredentials(VoiceCredentials val)
		{
			this.Credentials = val;
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000928CC File Offset: 0x00090ACC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCredentials)
			{
				num ^= this.Credentials.GetHashCode();
			}
			return num;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000928FC File Offset: 0x00090AFC
		public override bool Equals(object obj)
		{
			CreateLoginCredentialsResponse createLoginCredentialsResponse = obj as CreateLoginCredentialsResponse;
			return createLoginCredentialsResponse != null && this.HasCredentials == createLoginCredentialsResponse.HasCredentials && (!this.HasCredentials || this.Credentials.Equals(createLoginCredentialsResponse.Credentials));
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060029EE RID: 10734 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x00092941 File Offset: 0x00090B41
		public static CreateLoginCredentialsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateLoginCredentialsResponse>(bs, 0, -1);
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x0009294B File Offset: 0x00090B4B
		public void Deserialize(Stream stream)
		{
			CreateLoginCredentialsResponse.Deserialize(stream, this);
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x00092955 File Offset: 0x00090B55
		public static CreateLoginCredentialsResponse Deserialize(Stream stream, CreateLoginCredentialsResponse instance)
		{
			return CreateLoginCredentialsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x00092960 File Offset: 0x00090B60
		public static CreateLoginCredentialsResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateLoginCredentialsResponse createLoginCredentialsResponse = new CreateLoginCredentialsResponse();
			CreateLoginCredentialsResponse.DeserializeLengthDelimited(stream, createLoginCredentialsResponse);
			return createLoginCredentialsResponse;
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x0009297C File Offset: 0x00090B7C
		public static CreateLoginCredentialsResponse DeserializeLengthDelimited(Stream stream, CreateLoginCredentialsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateLoginCredentialsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000929A4 File Offset: 0x00090BA4
		public static CreateLoginCredentialsResponse Deserialize(Stream stream, CreateLoginCredentialsResponse instance, long limit)
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
					if (instance.Credentials == null)
					{
						instance.Credentials = VoiceCredentials.DeserializeLengthDelimited(stream);
					}
					else
					{
						VoiceCredentials.DeserializeLengthDelimited(stream, instance.Credentials);
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

		// Token: 0x060029F5 RID: 10741 RVA: 0x00092A3E File Offset: 0x00090C3E
		public void Serialize(Stream stream)
		{
			CreateLoginCredentialsResponse.Serialize(stream, this);
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x00092A47 File Offset: 0x00090C47
		public static void Serialize(Stream stream, CreateLoginCredentialsResponse instance)
		{
			if (instance.HasCredentials)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Credentials.GetSerializedSize());
				VoiceCredentials.Serialize(stream, instance.Credentials);
			}
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x00092A78 File Offset: 0x00090C78
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCredentials)
			{
				num += 1U;
				uint serializedSize = this.Credentials.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x040011DD RID: 4573
		public bool HasCredentials;

		// Token: 0x040011DE RID: 4574
		private VoiceCredentials _Credentials;
	}
}
