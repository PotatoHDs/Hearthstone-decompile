using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000464 RID: 1124
	public class GetJoinVoiceTokenResponse : IProtoBuf
	{
		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06004CF2 RID: 19698 RVA: 0x000EEEAE File Offset: 0x000ED0AE
		// (set) Token: 0x06004CF3 RID: 19699 RVA: 0x000EEEB6 File Offset: 0x000ED0B6
		public string ChannelUri
		{
			get
			{
				return this._ChannelUri;
			}
			set
			{
				this._ChannelUri = value;
				this.HasChannelUri = (value != null);
			}
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x000EEEC9 File Offset: 0x000ED0C9
		public void SetChannelUri(string val)
		{
			this.ChannelUri = val;
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06004CF5 RID: 19701 RVA: 0x000EEED2 File Offset: 0x000ED0D2
		// (set) Token: 0x06004CF6 RID: 19702 RVA: 0x000EEEDA File Offset: 0x000ED0DA
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

		// Token: 0x06004CF7 RID: 19703 RVA: 0x000EEEED File Offset: 0x000ED0ED
		public void SetCredentials(VoiceCredentials val)
		{
			this.Credentials = val;
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x000EEEF8 File Offset: 0x000ED0F8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelUri)
			{
				num ^= this.ChannelUri.GetHashCode();
			}
			if (this.HasCredentials)
			{
				num ^= this.Credentials.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004CF9 RID: 19705 RVA: 0x000EEF40 File Offset: 0x000ED140
		public override bool Equals(object obj)
		{
			GetJoinVoiceTokenResponse getJoinVoiceTokenResponse = obj as GetJoinVoiceTokenResponse;
			return getJoinVoiceTokenResponse != null && this.HasChannelUri == getJoinVoiceTokenResponse.HasChannelUri && (!this.HasChannelUri || this.ChannelUri.Equals(getJoinVoiceTokenResponse.ChannelUri)) && this.HasCredentials == getJoinVoiceTokenResponse.HasCredentials && (!this.HasCredentials || this.Credentials.Equals(getJoinVoiceTokenResponse.Credentials));
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06004CFA RID: 19706 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x000EEFB0 File Offset: 0x000ED1B0
		public static GetJoinVoiceTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetJoinVoiceTokenResponse>(bs, 0, -1);
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x000EEFBA File Offset: 0x000ED1BA
		public void Deserialize(Stream stream)
		{
			GetJoinVoiceTokenResponse.Deserialize(stream, this);
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x000EEFC4 File Offset: 0x000ED1C4
		public static GetJoinVoiceTokenResponse Deserialize(Stream stream, GetJoinVoiceTokenResponse instance)
		{
			return GetJoinVoiceTokenResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x000EEFD0 File Offset: 0x000ED1D0
		public static GetJoinVoiceTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			GetJoinVoiceTokenResponse getJoinVoiceTokenResponse = new GetJoinVoiceTokenResponse();
			GetJoinVoiceTokenResponse.DeserializeLengthDelimited(stream, getJoinVoiceTokenResponse);
			return getJoinVoiceTokenResponse;
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x000EEFEC File Offset: 0x000ED1EC
		public static GetJoinVoiceTokenResponse DeserializeLengthDelimited(Stream stream, GetJoinVoiceTokenResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetJoinVoiceTokenResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x000EF014 File Offset: 0x000ED214
		public static GetJoinVoiceTokenResponse Deserialize(Stream stream, GetJoinVoiceTokenResponse instance, long limit)
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
					else if (instance.Credentials == null)
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
					instance.ChannelUri = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x000EF0C6 File Offset: 0x000ED2C6
		public void Serialize(Stream stream)
		{
			GetJoinVoiceTokenResponse.Serialize(stream, this);
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x000EF0D0 File Offset: 0x000ED2D0
		public static void Serialize(Stream stream, GetJoinVoiceTokenResponse instance)
		{
			if (instance.HasChannelUri)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelUri));
			}
			if (instance.HasCredentials)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Credentials.GetSerializedSize());
				VoiceCredentials.Serialize(stream, instance.Credentials);
			}
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x000EF130 File Offset: 0x000ED330
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelUri)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ChannelUri);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasCredentials)
			{
				num += 1U;
				uint serializedSize = this.Credentials.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x0400190C RID: 6412
		public bool HasChannelUri;

		// Token: 0x0400190D RID: 6413
		private string _ChannelUri;

		// Token: 0x0400190E RID: 6414
		public bool HasCredentials;

		// Token: 0x0400190F RID: 6415
		private VoiceCredentials _Credentials;
	}
}
