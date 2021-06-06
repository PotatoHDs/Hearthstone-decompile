using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.whisper.v1.admin
{
	// Token: 0x020002EA RID: 746
	public class GetWhisperMessagesResponse : IProtoBuf
	{
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x000987E1 File Offset: 0x000969E1
		// (set) Token: 0x06002C46 RID: 11334 RVA: 0x000987E9 File Offset: 0x000969E9
		public List<AdminWhisper> Whisper
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

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000987E1 File Offset: 0x000969E1
		public List<AdminWhisper> WhisperList
		{
			get
			{
				return this._Whisper;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002C48 RID: 11336 RVA: 0x000987F2 File Offset: 0x000969F2
		public int WhisperCount
		{
			get
			{
				return this._Whisper.Count;
			}
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000987FF File Offset: 0x000969FF
		public void AddWhisper(AdminWhisper val)
		{
			this._Whisper.Add(val);
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x0009880D File Offset: 0x00096A0D
		public void ClearWhisper()
		{
			this._Whisper.Clear();
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x0009881A File Offset: 0x00096A1A
		public void SetWhisper(List<AdminWhisper> val)
		{
			this.Whisper = val;
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002C4C RID: 11340 RVA: 0x00098823 File Offset: 0x00096A23
		// (set) Token: 0x06002C4D RID: 11341 RVA: 0x0009882B File Offset: 0x00096A2B
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

		// Token: 0x06002C4E RID: 11342 RVA: 0x0009883B File Offset: 0x00096A3B
		public void SetContinuation(ulong val)
		{
			this.Continuation = val;
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x00098844 File Offset: 0x00096A44
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AdminWhisper adminWhisper in this.Whisper)
			{
				num ^= adminWhisper.GetHashCode();
			}
			if (this.HasContinuation)
			{
				num ^= this.Continuation.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000988C0 File Offset: 0x00096AC0
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

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002C51 RID: 11345 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x00098959 File Offset: 0x00096B59
		public static GetWhisperMessagesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetWhisperMessagesResponse>(bs, 0, -1);
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x00098963 File Offset: 0x00096B63
		public void Deserialize(Stream stream)
		{
			GetWhisperMessagesResponse.Deserialize(stream, this);
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x0009896D File Offset: 0x00096B6D
		public static GetWhisperMessagesResponse Deserialize(Stream stream, GetWhisperMessagesResponse instance)
		{
			return GetWhisperMessagesResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x00098978 File Offset: 0x00096B78
		public static GetWhisperMessagesResponse DeserializeLengthDelimited(Stream stream)
		{
			GetWhisperMessagesResponse getWhisperMessagesResponse = new GetWhisperMessagesResponse();
			GetWhisperMessagesResponse.DeserializeLengthDelimited(stream, getWhisperMessagesResponse);
			return getWhisperMessagesResponse;
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x00098994 File Offset: 0x00096B94
		public static GetWhisperMessagesResponse DeserializeLengthDelimited(Stream stream, GetWhisperMessagesResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetWhisperMessagesResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000989BC File Offset: 0x00096BBC
		public static GetWhisperMessagesResponse Deserialize(Stream stream, GetWhisperMessagesResponse instance, long limit)
		{
			if (instance.Whisper == null)
			{
				instance.Whisper = new List<AdminWhisper>();
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
					instance.Whisper.Add(AdminWhisper.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x00098A6C File Offset: 0x00096C6C
		public void Serialize(Stream stream)
		{
			GetWhisperMessagesResponse.Serialize(stream, this);
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x00098A78 File Offset: 0x00096C78
		public static void Serialize(Stream stream, GetWhisperMessagesResponse instance)
		{
			if (instance.Whisper.Count > 0)
			{
				foreach (AdminWhisper adminWhisper in instance.Whisper)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, adminWhisper.GetSerializedSize());
					AdminWhisper.Serialize(stream, adminWhisper);
				}
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x00098B0C File Offset: 0x00096D0C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Whisper.Count > 0)
			{
				foreach (AdminWhisper adminWhisper in this.Whisper)
				{
					num += 1U;
					uint serializedSize = adminWhisper.GetSerializedSize();
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

		// Token: 0x0400125D RID: 4701
		private List<AdminWhisper> _Whisper = new List<AdminWhisper>();

		// Token: 0x0400125E RID: 4702
		public bool HasContinuation;

		// Token: 0x0400125F RID: 4703
		private ulong _Continuation;
	}
}
