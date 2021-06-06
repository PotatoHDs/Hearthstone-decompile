using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	// Token: 0x02000315 RID: 789
	public class SessionCreatedNotification : IProtoBuf
	{
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x000A1E3D File Offset: 0x000A003D
		// (set) Token: 0x06002FE7 RID: 12263 RVA: 0x000A1E45 File Offset: 0x000A0045
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

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000A1E58 File Offset: 0x000A0058
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x000A1E61 File Offset: 0x000A0061
		// (set) Token: 0x06002FEA RID: 12266 RVA: 0x000A1E69 File Offset: 0x000A0069
		public uint Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000A1E79 File Offset: 0x000A0079
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002FEC RID: 12268 RVA: 0x000A1E82 File Offset: 0x000A0082
		// (set) Token: 0x06002FED RID: 12269 RVA: 0x000A1E8A File Offset: 0x000A008A
		public string SessionId
		{
			get
			{
				return this._SessionId;
			}
			set
			{
				this._SessionId = value;
				this.HasSessionId = (value != null);
			}
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000A1E9D File Offset: 0x000A009D
		public void SetSessionId(string val)
		{
			this.SessionId = val;
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002FEF RID: 12271 RVA: 0x000A1EA6 File Offset: 0x000A00A6
		// (set) Token: 0x06002FF0 RID: 12272 RVA: 0x000A1EAE File Offset: 0x000A00AE
		public byte[] SessionKey
		{
			get
			{
				return this._SessionKey;
			}
			set
			{
				this._SessionKey = value;
				this.HasSessionKey = (value != null);
			}
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000A1EC1 File Offset: 0x000A00C1
		public void SetSessionKey(byte[] val)
		{
			this.SessionKey = val;
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002FF2 RID: 12274 RVA: 0x000A1ECA File Offset: 0x000A00CA
		// (set) Token: 0x06002FF3 RID: 12275 RVA: 0x000A1ED2 File Offset: 0x000A00D2
		public string ClientId
		{
			get
			{
				return this._ClientId;
			}
			set
			{
				this._ClientId = value;
				this.HasClientId = (value != null);
			}
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000A1EE5 File Offset: 0x000A00E5
		public void SetClientId(string val)
		{
			this.ClientId = val;
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000A1EF0 File Offset: 0x000A00F0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			if (this.HasSessionId)
			{
				num ^= this.SessionId.GetHashCode();
			}
			if (this.HasSessionKey)
			{
				num ^= this.SessionKey.GetHashCode();
			}
			if (this.HasClientId)
			{
				num ^= this.ClientId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000A1F7C File Offset: 0x000A017C
		public override bool Equals(object obj)
		{
			SessionCreatedNotification sessionCreatedNotification = obj as SessionCreatedNotification;
			return sessionCreatedNotification != null && this.HasIdentity == sessionCreatedNotification.HasIdentity && (!this.HasIdentity || this.Identity.Equals(sessionCreatedNotification.Identity)) && this.HasReason == sessionCreatedNotification.HasReason && (!this.HasReason || this.Reason.Equals(sessionCreatedNotification.Reason)) && this.HasSessionId == sessionCreatedNotification.HasSessionId && (!this.HasSessionId || this.SessionId.Equals(sessionCreatedNotification.SessionId)) && this.HasSessionKey == sessionCreatedNotification.HasSessionKey && (!this.HasSessionKey || this.SessionKey.Equals(sessionCreatedNotification.SessionKey)) && this.HasClientId == sessionCreatedNotification.HasClientId && (!this.HasClientId || this.ClientId.Equals(sessionCreatedNotification.ClientId));
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002FF7 RID: 12279 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000A2070 File Offset: 0x000A0270
		public static SessionCreatedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SessionCreatedNotification>(bs, 0, -1);
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000A207A File Offset: 0x000A027A
		public void Deserialize(Stream stream)
		{
			SessionCreatedNotification.Deserialize(stream, this);
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000A2084 File Offset: 0x000A0284
		public static SessionCreatedNotification Deserialize(Stream stream, SessionCreatedNotification instance)
		{
			return SessionCreatedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000A2090 File Offset: 0x000A0290
		public static SessionCreatedNotification DeserializeLengthDelimited(Stream stream)
		{
			SessionCreatedNotification sessionCreatedNotification = new SessionCreatedNotification();
			SessionCreatedNotification.DeserializeLengthDelimited(stream, sessionCreatedNotification);
			return sessionCreatedNotification;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000A20AC File Offset: 0x000A02AC
		public static SessionCreatedNotification DeserializeLengthDelimited(Stream stream, SessionCreatedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SessionCreatedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000A20D4 File Offset: 0x000A02D4
		public static SessionCreatedNotification Deserialize(Stream stream, SessionCreatedNotification instance, long limit)
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
				else
				{
					if (num <= 16)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.Reason = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (instance.Identity == null)
							{
								instance.Identity = Identity.DeserializeLengthDelimited(stream);
								continue;
							}
							Identity.DeserializeLengthDelimited(stream, instance.Identity);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.SessionId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.SessionKey = ProtocolParser.ReadBytes(stream);
							continue;
						}
						if (num == 42)
						{
							instance.ClientId = ProtocolParser.ReadString(stream);
							continue;
						}
					}
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

		// Token: 0x06002FFE RID: 12286 RVA: 0x000A21D8 File Offset: 0x000A03D8
		public void Serialize(Stream stream)
		{
			SessionCreatedNotification.Serialize(stream, this);
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000A21E4 File Offset: 0x000A03E4
		public static void Serialize(Stream stream, SessionCreatedNotification instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.HasSessionId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SessionId));
			}
			if (instance.HasSessionKey)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, instance.SessionKey);
			}
			if (instance.HasClientId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000A22A4 File Offset: 0x000A04A4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIdentity)
			{
				num += 1U;
				uint serializedSize = this.Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			if (this.HasSessionId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SessionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasSessionKey)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.SessionKey.Length) + (uint)this.SessionKey.Length;
			}
			if (this.HasClientId)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x0400131D RID: 4893
		public bool HasIdentity;

		// Token: 0x0400131E RID: 4894
		private Identity _Identity;

		// Token: 0x0400131F RID: 4895
		public bool HasReason;

		// Token: 0x04001320 RID: 4896
		private uint _Reason;

		// Token: 0x04001321 RID: 4897
		public bool HasSessionId;

		// Token: 0x04001322 RID: 4898
		private string _SessionId;

		// Token: 0x04001323 RID: 4899
		public bool HasSessionKey;

		// Token: 0x04001324 RID: 4900
		private byte[] _SessionKey;

		// Token: 0x04001325 RID: 4901
		public bool HasClientId;

		// Token: 0x04001326 RID: 4902
		private string _ClientId;
	}
}
