using System;
using System.IO;

namespace bnet.protocol.session.v1
{
	// Token: 0x02000314 RID: 788
	public class RefreshSessionKeyResponse : IProtoBuf
	{
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x000A1C61 File Offset: 0x0009FE61
		// (set) Token: 0x06002FD7 RID: 12247 RVA: 0x000A1C69 File Offset: 0x0009FE69
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

		// Token: 0x06002FD8 RID: 12248 RVA: 0x000A1C7C File Offset: 0x0009FE7C
		public void SetSessionKey(byte[] val)
		{
			this.SessionKey = val;
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000A1C88 File Offset: 0x0009FE88
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSessionKey)
			{
				num ^= this.SessionKey.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000A1CB8 File Offset: 0x0009FEB8
		public override bool Equals(object obj)
		{
			RefreshSessionKeyResponse refreshSessionKeyResponse = obj as RefreshSessionKeyResponse;
			return refreshSessionKeyResponse != null && this.HasSessionKey == refreshSessionKeyResponse.HasSessionKey && (!this.HasSessionKey || this.SessionKey.Equals(refreshSessionKeyResponse.SessionKey));
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000A1CFD File Offset: 0x0009FEFD
		public static RefreshSessionKeyResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RefreshSessionKeyResponse>(bs, 0, -1);
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000A1D07 File Offset: 0x0009FF07
		public void Deserialize(Stream stream)
		{
			RefreshSessionKeyResponse.Deserialize(stream, this);
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000A1D11 File Offset: 0x0009FF11
		public static RefreshSessionKeyResponse Deserialize(Stream stream, RefreshSessionKeyResponse instance)
		{
			return RefreshSessionKeyResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000A1D1C File Offset: 0x0009FF1C
		public static RefreshSessionKeyResponse DeserializeLengthDelimited(Stream stream)
		{
			RefreshSessionKeyResponse refreshSessionKeyResponse = new RefreshSessionKeyResponse();
			RefreshSessionKeyResponse.DeserializeLengthDelimited(stream, refreshSessionKeyResponse);
			return refreshSessionKeyResponse;
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000A1D38 File Offset: 0x0009FF38
		public static RefreshSessionKeyResponse DeserializeLengthDelimited(Stream stream, RefreshSessionKeyResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RefreshSessionKeyResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000A1D60 File Offset: 0x0009FF60
		public static RefreshSessionKeyResponse Deserialize(Stream stream, RefreshSessionKeyResponse instance, long limit)
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

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000A1DE0 File Offset: 0x0009FFE0
		public void Serialize(Stream stream)
		{
			RefreshSessionKeyResponse.Serialize(stream, this);
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000A1DE9 File Offset: 0x0009FFE9
		public static void Serialize(Stream stream, RefreshSessionKeyResponse instance)
		{
			if (instance.HasSessionKey)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.SessionKey);
			}
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000A1E08 File Offset: 0x000A0008
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

		// Token: 0x0400131B RID: 4891
		public bool HasSessionKey;

		// Token: 0x0400131C RID: 4892
		private byte[] _SessionKey;
	}
}
