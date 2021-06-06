using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002E5 RID: 741
	public class GetWhisperMessagesResponse : IProtoBuf
	{
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x00097151 File Offset: 0x00095351
		// (set) Token: 0x06002BCA RID: 11210 RVA: 0x00097159 File Offset: 0x00095359
		public List<Whisper> Whisper
		{
			get
			{
				return this._Whisper;
			}
			set
			{
				this._Whisper = value;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002BCB RID: 11211 RVA: 0x00097151 File Offset: 0x00095351
		public List<Whisper> WhisperList
		{
			get
			{
				return this._Whisper;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x00097162 File Offset: 0x00095362
		public int WhisperCount
		{
			get
			{
				return this._Whisper.Count;
			}
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x0009716F File Offset: 0x0009536F
		public void AddWhisper(Whisper val)
		{
			this._Whisper.Add(val);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x0009717D File Offset: 0x0009537D
		public void ClearWhisper()
		{
			this._Whisper.Clear();
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x0009718A File Offset: 0x0009538A
		public void SetWhisper(List<Whisper> val)
		{
			this.Whisper = val;
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x00097193 File Offset: 0x00095393
		// (set) Token: 0x06002BD1 RID: 11217 RVA: 0x0009719B File Offset: 0x0009539B
		public ulong Continuation
		{
			get
			{
				return this._Continuation;
			}
			set
			{
				this._Continuation = value;
				this.HasContinuation = true;
			}
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000971AB File Offset: 0x000953AB
		public void SetContinuation(ulong val)
		{
			this.Continuation = val;
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000971B4 File Offset: 0x000953B4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Whisper whisper in this.Whisper)
			{
				num ^= whisper.GetHashCode();
			}
			if (this.HasContinuation)
			{
				num ^= this.Continuation.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x00097230 File Offset: 0x00095430
		public override bool Equals(object obj)
		{
			GetWhisperMessagesResponse getWhisperMessagesResponse = obj as GetWhisperMessagesResponse;
			if (getWhisperMessagesResponse == null)
			{
				return false;
			}
			if (this.Whisper.Count != getWhisperMessagesResponse.Whisper.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Whisper.Count; i++)
			{
				if (!this.Whisper[i].Equals(getWhisperMessagesResponse.Whisper[i]))
				{
					return false;
				}
			}
			return this.HasContinuation == getWhisperMessagesResponse.HasContinuation && (!this.HasContinuation || this.Continuation.Equals(getWhisperMessagesResponse.Continuation));
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000972C9 File Offset: 0x000954C9
		public static GetWhisperMessagesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetWhisperMessagesResponse>(bs, 0, -1);
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000972D3 File Offset: 0x000954D3
		public void Deserialize(Stream stream)
		{
			GetWhisperMessagesResponse.Deserialize(stream, this);
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000972DD File Offset: 0x000954DD
		public static GetWhisperMessagesResponse Deserialize(Stream stream, GetWhisperMessagesResponse instance)
		{
			return GetWhisperMessagesResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000972E8 File Offset: 0x000954E8
		public static GetWhisperMessagesResponse DeserializeLengthDelimited(Stream stream)
		{
			GetWhisperMessagesResponse getWhisperMessagesResponse = new GetWhisperMessagesResponse();
			GetWhisperMessagesResponse.DeserializeLengthDelimited(stream, getWhisperMessagesResponse);
			return getWhisperMessagesResponse;
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x00097304 File Offset: 0x00095504
		public static GetWhisperMessagesResponse DeserializeLengthDelimited(Stream stream, GetWhisperMessagesResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetWhisperMessagesResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x0009732C File Offset: 0x0009552C
		public static GetWhisperMessagesResponse Deserialize(Stream stream, GetWhisperMessagesResponse instance, long limit)
		{
			if (instance.Whisper == null)
			{
				instance.Whisper = new List<Whisper>();
			}
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
					if (num != 16)
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
						instance.Continuation = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Whisper.Add(bnet.protocol.whisper.v1.Whisper.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000973DC File Offset: 0x000955DC
		public void Serialize(Stream stream)
		{
			GetWhisperMessagesResponse.Serialize(stream, this);
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000973E8 File Offset: 0x000955E8
		public static void Serialize(Stream stream, GetWhisperMessagesResponse instance)
		{
			if (instance.Whisper.Count > 0)
			{
				foreach (Whisper whisper in instance.Whisper)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, whisper.GetSerializedSize());
					bnet.protocol.whisper.v1.Whisper.Serialize(stream, whisper);
				}
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x0009747C File Offset: 0x0009567C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Whisper.Count > 0)
			{
				foreach (Whisper whisper in this.Whisper)
				{
					num += 1U;
					uint serializedSize = whisper.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasContinuation)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Continuation);
			}
			return num;
		}

		// Token: 0x0400123D RID: 4669
		private List<Whisper> _Whisper = new List<Whisper>();

		// Token: 0x0400123E RID: 4670
		public bool HasContinuation;

		// Token: 0x0400123F RID: 4671
		private ulong _Continuation;
	}
}
