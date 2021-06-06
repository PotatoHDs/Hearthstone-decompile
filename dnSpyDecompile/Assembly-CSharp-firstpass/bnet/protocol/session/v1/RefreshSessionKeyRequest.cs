using System;
using System.IO;

namespace bnet.protocol.session.v1
{
	// Token: 0x02000313 RID: 787
	public class RefreshSessionKeyRequest : IProtoBuf
	{
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002FC6 RID: 12230 RVA: 0x000A1A88 File Offset: 0x0009FC88
		// (set) Token: 0x06002FC7 RID: 12231 RVA: 0x000A1A90 File Offset: 0x0009FC90
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

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000A1AA3 File Offset: 0x0009FCA3
		public void SetSessionKey(byte[] val)
		{
			this.SessionKey = val;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000A1AAC File Offset: 0x0009FCAC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSessionKey)
			{
				num ^= this.SessionKey.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000A1ADC File Offset: 0x0009FCDC
		public override bool Equals(object obj)
		{
			RefreshSessionKeyRequest refreshSessionKeyRequest = obj as RefreshSessionKeyRequest;
			return refreshSessionKeyRequest != null && this.HasSessionKey == refreshSessionKeyRequest.HasSessionKey && (!this.HasSessionKey || this.SessionKey.Equals(refreshSessionKeyRequest.SessionKey));
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002FCB RID: 12235 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000A1B21 File Offset: 0x0009FD21
		public static RefreshSessionKeyRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RefreshSessionKeyRequest>(bs, 0, -1);
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000A1B2B File Offset: 0x0009FD2B
		public void Deserialize(Stream stream)
		{
			RefreshSessionKeyRequest.Deserialize(stream, this);
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000A1B35 File Offset: 0x0009FD35
		public static RefreshSessionKeyRequest Deserialize(Stream stream, RefreshSessionKeyRequest instance)
		{
			return RefreshSessionKeyRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000A1B40 File Offset: 0x0009FD40
		public static RefreshSessionKeyRequest DeserializeLengthDelimited(Stream stream)
		{
			RefreshSessionKeyRequest refreshSessionKeyRequest = new RefreshSessionKeyRequest();
			RefreshSessionKeyRequest.DeserializeLengthDelimited(stream, refreshSessionKeyRequest);
			return refreshSessionKeyRequest;
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x000A1B5C File Offset: 0x0009FD5C
		public static RefreshSessionKeyRequest DeserializeLengthDelimited(Stream stream, RefreshSessionKeyRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RefreshSessionKeyRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x000A1B84 File Offset: 0x0009FD84
		public static RefreshSessionKeyRequest Deserialize(Stream stream, RefreshSessionKeyRequest instance, long limit)
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
					instance.SessionKey = ProtocolParser.ReadBytes(stream);
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

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000A1C04 File Offset: 0x0009FE04
		public void Serialize(Stream stream)
		{
			RefreshSessionKeyRequest.Serialize(stream, this);
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000A1C0D File Offset: 0x0009FE0D
		public static void Serialize(Stream stream, RefreshSessionKeyRequest instance)
		{
			if (instance.HasSessionKey)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.SessionKey);
			}
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x000A1C2C File Offset: 0x0009FE2C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSessionKey)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.SessionKey.Length) + (uint)this.SessionKey.Length;
			}
			return num;
		}

		// Token: 0x04001319 RID: 4889
		public bool HasSessionKey;

		// Token: 0x0400131A RID: 4890
		private byte[] _SessionKey;
	}
}
