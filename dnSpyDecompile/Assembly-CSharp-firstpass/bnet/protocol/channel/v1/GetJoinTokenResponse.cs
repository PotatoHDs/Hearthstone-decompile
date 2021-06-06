using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004DE RID: 1246
	public class GetJoinTokenResponse : IProtoBuf
	{
		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x060057EE RID: 22510 RVA: 0x0010D942 File Offset: 0x0010BB42
		// (set) Token: 0x060057EF RID: 22511 RVA: 0x0010D94A File Offset: 0x0010BB4A
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

		// Token: 0x060057F0 RID: 22512 RVA: 0x0010D95D File Offset: 0x0010BB5D
		public void SetChannelUri(string val)
		{
			this.ChannelUri = val;
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x060057F1 RID: 22513 RVA: 0x0010D966 File Offset: 0x0010BB66
		// (set) Token: 0x060057F2 RID: 22514 RVA: 0x0010D96E File Offset: 0x0010BB6E
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

		// Token: 0x060057F3 RID: 22515 RVA: 0x0010D981 File Offset: 0x0010BB81
		public void SetCredentials(VoiceCredentials val)
		{
			this.Credentials = val;
		}

		// Token: 0x060057F4 RID: 22516 RVA: 0x0010D98C File Offset: 0x0010BB8C
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

		// Token: 0x060057F5 RID: 22517 RVA: 0x0010D9D4 File Offset: 0x0010BBD4
		public override bool Equals(object obj)
		{
			GetJoinTokenResponse getJoinTokenResponse = obj as GetJoinTokenResponse;
			return getJoinTokenResponse != null && this.HasChannelUri == getJoinTokenResponse.HasChannelUri && (!this.HasChannelUri || this.ChannelUri.Equals(getJoinTokenResponse.ChannelUri)) && this.HasCredentials == getJoinTokenResponse.HasCredentials && (!this.HasCredentials || this.Credentials.Equals(getJoinTokenResponse.Credentials));
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x060057F6 RID: 22518 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060057F7 RID: 22519 RVA: 0x0010DA44 File Offset: 0x0010BC44
		public static GetJoinTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetJoinTokenResponse>(bs, 0, -1);
		}

		// Token: 0x060057F8 RID: 22520 RVA: 0x0010DA4E File Offset: 0x0010BC4E
		public void Deserialize(Stream stream)
		{
			GetJoinTokenResponse.Deserialize(stream, this);
		}

		// Token: 0x060057F9 RID: 22521 RVA: 0x0010DA58 File Offset: 0x0010BC58
		public static GetJoinTokenResponse Deserialize(Stream stream, GetJoinTokenResponse instance)
		{
			return GetJoinTokenResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060057FA RID: 22522 RVA: 0x0010DA64 File Offset: 0x0010BC64
		public static GetJoinTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			GetJoinTokenResponse getJoinTokenResponse = new GetJoinTokenResponse();
			GetJoinTokenResponse.DeserializeLengthDelimited(stream, getJoinTokenResponse);
			return getJoinTokenResponse;
		}

		// Token: 0x060057FB RID: 22523 RVA: 0x0010DA80 File Offset: 0x0010BC80
		public static GetJoinTokenResponse DeserializeLengthDelimited(Stream stream, GetJoinTokenResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetJoinTokenResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060057FC RID: 22524 RVA: 0x0010DAA8 File Offset: 0x0010BCA8
		public static GetJoinTokenResponse Deserialize(Stream stream, GetJoinTokenResponse instance, long limit)
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

		// Token: 0x060057FD RID: 22525 RVA: 0x0010DB5A File Offset: 0x0010BD5A
		public void Serialize(Stream stream)
		{
			GetJoinTokenResponse.Serialize(stream, this);
		}

		// Token: 0x060057FE RID: 22526 RVA: 0x0010DB64 File Offset: 0x0010BD64
		public static void Serialize(Stream stream, GetJoinTokenResponse instance)
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

		// Token: 0x060057FF RID: 22527 RVA: 0x0010DBC4 File Offset: 0x0010BDC4
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

		// Token: 0x04001B93 RID: 7059
		public bool HasChannelUri;

		// Token: 0x04001B94 RID: 7060
		private string _ChannelUri;

		// Token: 0x04001B95 RID: 7061
		public bool HasCredentials;

		// Token: 0x04001B96 RID: 7062
		private VoiceCredentials _Credentials;
	}
}
