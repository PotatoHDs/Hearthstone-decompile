using System;
using System.IO;
using System.Text;

namespace bnet.protocol.session.v1
{
	// Token: 0x02000308 RID: 776
	public class CreateSessionResponse : IProtoBuf
	{
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002EEF RID: 12015 RVA: 0x0009F9C0 File Offset: 0x0009DBC0
		// (set) Token: 0x06002EF0 RID: 12016 RVA: 0x0009F9C8 File Offset: 0x0009DBC8
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

		// Token: 0x06002EF1 RID: 12017 RVA: 0x0009F9DB File Offset: 0x0009DBDB
		public void SetSessionId(string val)
		{
			this.SessionId = val;
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x0009F9E4 File Offset: 0x0009DBE4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSessionId)
			{
				num ^= this.SessionId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x0009FA14 File Offset: 0x0009DC14
		public override bool Equals(object obj)
		{
			CreateSessionResponse createSessionResponse = obj as CreateSessionResponse;
			return createSessionResponse != null && this.HasSessionId == createSessionResponse.HasSessionId && (!this.HasSessionId || this.SessionId.Equals(createSessionResponse.SessionId));
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002EF4 RID: 12020 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x0009FA59 File Offset: 0x0009DC59
		public static CreateSessionResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateSessionResponse>(bs, 0, -1);
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x0009FA63 File Offset: 0x0009DC63
		public void Deserialize(Stream stream)
		{
			CreateSessionResponse.Deserialize(stream, this);
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x0009FA6D File Offset: 0x0009DC6D
		public static CreateSessionResponse Deserialize(Stream stream, CreateSessionResponse instance)
		{
			return CreateSessionResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x0009FA78 File Offset: 0x0009DC78
		public static CreateSessionResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateSessionResponse createSessionResponse = new CreateSessionResponse();
			CreateSessionResponse.DeserializeLengthDelimited(stream, createSessionResponse);
			return createSessionResponse;
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x0009FA94 File Offset: 0x0009DC94
		public static CreateSessionResponse DeserializeLengthDelimited(Stream stream, CreateSessionResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateSessionResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x0009FABC File Offset: 0x0009DCBC
		public static CreateSessionResponse Deserialize(Stream stream, CreateSessionResponse instance, long limit)
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
					instance.SessionId = ProtocolParser.ReadString(stream);
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

		// Token: 0x06002EFB RID: 12027 RVA: 0x0009FB3C File Offset: 0x0009DD3C
		public void Serialize(Stream stream)
		{
			CreateSessionResponse.Serialize(stream, this);
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x0009FB45 File Offset: 0x0009DD45
		public static void Serialize(Stream stream, CreateSessionResponse instance)
		{
			if (instance.HasSessionId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SessionId));
			}
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x0009FB70 File Offset: 0x0009DD70
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSessionId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SessionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040012F4 RID: 4852
		public bool HasSessionId;

		// Token: 0x040012F5 RID: 4853
		private string _SessionId;
	}
}
