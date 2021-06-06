using System;
using System.IO;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002E0 RID: 736
	public class SendWhisperResponse : IProtoBuf
	{
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x0009619A File Offset: 0x0009439A
		// (set) Token: 0x06002B68 RID: 11112 RVA: 0x000961A2 File Offset: 0x000943A2
		public Whisper Whisper
		{
			get
			{
				return this._Whisper;
			}
			set
			{
				this._Whisper = value;
				this.HasWhisper = (value != null);
			}
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000961B5 File Offset: 0x000943B5
		public void SetWhisper(Whisper val)
		{
			this.Whisper = val;
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000961C0 File Offset: 0x000943C0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasWhisper)
			{
				num ^= this.Whisper.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x000961F0 File Offset: 0x000943F0
		public override bool Equals(object obj)
		{
			SendWhisperResponse sendWhisperResponse = obj as SendWhisperResponse;
			return sendWhisperResponse != null && this.HasWhisper == sendWhisperResponse.HasWhisper && (!this.HasWhisper || this.Whisper.Equals(sendWhisperResponse.Whisper));
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x00096235 File Offset: 0x00094435
		public static SendWhisperResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendWhisperResponse>(bs, 0, -1);
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x0009623F File Offset: 0x0009443F
		public void Deserialize(Stream stream)
		{
			SendWhisperResponse.Deserialize(stream, this);
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x00096249 File Offset: 0x00094449
		public static SendWhisperResponse Deserialize(Stream stream, SendWhisperResponse instance)
		{
			return SendWhisperResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x00096254 File Offset: 0x00094454
		public static SendWhisperResponse DeserializeLengthDelimited(Stream stream)
		{
			SendWhisperResponse sendWhisperResponse = new SendWhisperResponse();
			SendWhisperResponse.DeserializeLengthDelimited(stream, sendWhisperResponse);
			return sendWhisperResponse;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x00096270 File Offset: 0x00094470
		public static SendWhisperResponse DeserializeLengthDelimited(Stream stream, SendWhisperResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendWhisperResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x00096298 File Offset: 0x00094498
		public static SendWhisperResponse Deserialize(Stream stream, SendWhisperResponse instance, long limit)
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
					if (instance.Whisper == null)
					{
						instance.Whisper = Whisper.DeserializeLengthDelimited(stream);
					}
					else
					{
						Whisper.DeserializeLengthDelimited(stream, instance.Whisper);
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

		// Token: 0x06002B73 RID: 11123 RVA: 0x00096332 File Offset: 0x00094532
		public void Serialize(Stream stream)
		{
			SendWhisperResponse.Serialize(stream, this);
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0009633B File Offset: 0x0009453B
		public static void Serialize(Stream stream, SendWhisperResponse instance)
		{
			if (instance.HasWhisper)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Whisper.GetSerializedSize());
				Whisper.Serialize(stream, instance.Whisper);
			}
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x0009636C File Offset: 0x0009456C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasWhisper)
			{
				num += 1U;
				uint serializedSize = this.Whisper.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001227 RID: 4647
		public bool HasWhisper;

		// Token: 0x04001228 RID: 4648
		private Whisper _Whisper;
	}
}
