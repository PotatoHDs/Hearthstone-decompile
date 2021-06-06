using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	// Token: 0x0200030A RID: 778
	public class DestroySessionRequest : IProtoBuf
	{
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002F15 RID: 12053 RVA: 0x0009FF72 File Offset: 0x0009E172
		// (set) Token: 0x06002F16 RID: 12054 RVA: 0x0009FF7A File Offset: 0x0009E17A
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

		// Token: 0x06002F17 RID: 12055 RVA: 0x0009FF8D File Offset: 0x0009E18D
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x0009FF96 File Offset: 0x0009E196
		// (set) Token: 0x06002F19 RID: 12057 RVA: 0x0009FF9E File Offset: 0x0009E19E
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

		// Token: 0x06002F1A RID: 12058 RVA: 0x0009FFB1 File Offset: 0x0009E1B1
		public void SetSessionId(string val)
		{
			this.SessionId = val;
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x0009FFBC File Offset: 0x0009E1BC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			if (this.HasSessionId)
			{
				num ^= this.SessionId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000A0004 File Offset: 0x0009E204
		public override bool Equals(object obj)
		{
			DestroySessionRequest destroySessionRequest = obj as DestroySessionRequest;
			return destroySessionRequest != null && this.HasIdentity == destroySessionRequest.HasIdentity && (!this.HasIdentity || this.Identity.Equals(destroySessionRequest.Identity)) && this.HasSessionId == destroySessionRequest.HasSessionId && (!this.HasSessionId || this.SessionId.Equals(destroySessionRequest.SessionId));
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002F1D RID: 12061 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000A0074 File Offset: 0x0009E274
		public static DestroySessionRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DestroySessionRequest>(bs, 0, -1);
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000A007E File Offset: 0x0009E27E
		public void Deserialize(Stream stream)
		{
			DestroySessionRequest.Deserialize(stream, this);
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000A0088 File Offset: 0x0009E288
		public static DestroySessionRequest Deserialize(Stream stream, DestroySessionRequest instance)
		{
			return DestroySessionRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x000A0094 File Offset: 0x0009E294
		public static DestroySessionRequest DeserializeLengthDelimited(Stream stream)
		{
			DestroySessionRequest destroySessionRequest = new DestroySessionRequest();
			DestroySessionRequest.DeserializeLengthDelimited(stream, destroySessionRequest);
			return destroySessionRequest;
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x000A00B0 File Offset: 0x0009E2B0
		public static DestroySessionRequest DeserializeLengthDelimited(Stream stream, DestroySessionRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DestroySessionRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000A00D8 File Offset: 0x0009E2D8
		public static DestroySessionRequest Deserialize(Stream stream, DestroySessionRequest instance, long limit)
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
					else
					{
						instance.SessionId = ProtocolParser.ReadString(stream);
					}
				}
				else if (instance.Identity == null)
				{
					instance.Identity = Identity.DeserializeLengthDelimited(stream);
				}
				else
				{
					Identity.DeserializeLengthDelimited(stream, instance.Identity);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000A018A File Offset: 0x0009E38A
		public void Serialize(Stream stream)
		{
			DestroySessionRequest.Serialize(stream, this);
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000A0194 File Offset: 0x0009E394
		public static void Serialize(Stream stream, DestroySessionRequest instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
			if (instance.HasSessionId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SessionId));
			}
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x000A01F4 File Offset: 0x0009E3F4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIdentity)
			{
				num += 1U;
				uint serializedSize = this.Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSessionId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SessionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040012FC RID: 4860
		public bool HasIdentity;

		// Token: 0x040012FD RID: 4861
		private Identity _Identity;

		// Token: 0x040012FE RID: 4862
		public bool HasSessionId;

		// Token: 0x040012FF RID: 4863
		private string _SessionId;
	}
}
