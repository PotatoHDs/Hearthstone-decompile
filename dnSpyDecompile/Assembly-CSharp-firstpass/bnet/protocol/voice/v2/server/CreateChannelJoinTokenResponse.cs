using System;
using System.IO;

namespace bnet.protocol.voice.v2.server
{
	// Token: 0x020002C6 RID: 710
	public class CreateChannelJoinTokenResponse : IProtoBuf
	{
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x00091912 File Offset: 0x0008FB12
		// (set) Token: 0x0600297D RID: 10621 RVA: 0x0009191A File Offset: 0x0008FB1A
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

		// Token: 0x0600297E RID: 10622 RVA: 0x0009192D File Offset: 0x0008FB2D
		public void SetCredentials(VoiceCredentials val)
		{
			this.Credentials = val;
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x00091938 File Offset: 0x0008FB38
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCredentials)
			{
				num ^= this.Credentials.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x00091968 File Offset: 0x0008FB68
		public override bool Equals(object obj)
		{
			CreateChannelJoinTokenResponse createChannelJoinTokenResponse = obj as CreateChannelJoinTokenResponse;
			return createChannelJoinTokenResponse != null && this.HasCredentials == createChannelJoinTokenResponse.HasCredentials && (!this.HasCredentials || this.Credentials.Equals(createChannelJoinTokenResponse.Credentials));
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000919AD File Offset: 0x0008FBAD
		public static CreateChannelJoinTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelJoinTokenResponse>(bs, 0, -1);
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000919B7 File Offset: 0x0008FBB7
		public void Deserialize(Stream stream)
		{
			CreateChannelJoinTokenResponse.Deserialize(stream, this);
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x000919C1 File Offset: 0x0008FBC1
		public static CreateChannelJoinTokenResponse Deserialize(Stream stream, CreateChannelJoinTokenResponse instance)
		{
			return CreateChannelJoinTokenResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x000919CC File Offset: 0x0008FBCC
		public static CreateChannelJoinTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelJoinTokenResponse createChannelJoinTokenResponse = new CreateChannelJoinTokenResponse();
			CreateChannelJoinTokenResponse.DeserializeLengthDelimited(stream, createChannelJoinTokenResponse);
			return createChannelJoinTokenResponse;
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x000919E8 File Offset: 0x0008FBE8
		public static CreateChannelJoinTokenResponse DeserializeLengthDelimited(Stream stream, CreateChannelJoinTokenResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateChannelJoinTokenResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x00091A10 File Offset: 0x0008FC10
		public static CreateChannelJoinTokenResponse Deserialize(Stream stream, CreateChannelJoinTokenResponse instance, long limit)
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

		// Token: 0x06002988 RID: 10632 RVA: 0x00091AAA File Offset: 0x0008FCAA
		public void Serialize(Stream stream)
		{
			CreateChannelJoinTokenResponse.Serialize(stream, this);
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x00091AB3 File Offset: 0x0008FCB3
		public static void Serialize(Stream stream, CreateChannelJoinTokenResponse instance)
		{
			if (instance.HasCredentials)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Credentials.GetSerializedSize());
				VoiceCredentials.Serialize(stream, instance.Credentials);
			}
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x00091AE4 File Offset: 0x0008FCE4
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

		// Token: 0x040011CC RID: 4556
		public bool HasCredentials;

		// Token: 0x040011CD RID: 4557
		private VoiceCredentials _Credentials;
	}
}
