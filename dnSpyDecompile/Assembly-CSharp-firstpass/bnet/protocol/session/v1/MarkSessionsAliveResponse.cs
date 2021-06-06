using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.session.v1
{
	// Token: 0x0200030E RID: 782
	public class MarkSessionsAliveResponse : IProtoBuf
	{
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002F6C RID: 12140 RVA: 0x000A0DB7 File Offset: 0x0009EFB7
		// (set) Token: 0x06002F6D RID: 12141 RVA: 0x000A0DBF File Offset: 0x0009EFBF
		public List<SessionIdentifier> FailedSession
		{
			get
			{
				return this._FailedSession;
			}
			set
			{
				this._FailedSession = value;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002F6E RID: 12142 RVA: 0x000A0DB7 File Offset: 0x0009EFB7
		public List<SessionIdentifier> FailedSessionList
		{
			get
			{
				return this._FailedSession;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002F6F RID: 12143 RVA: 0x000A0DC8 File Offset: 0x0009EFC8
		public int FailedSessionCount
		{
			get
			{
				return this._FailedSession.Count;
			}
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x000A0DD5 File Offset: 0x0009EFD5
		public void AddFailedSession(SessionIdentifier val)
		{
			this._FailedSession.Add(val);
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x000A0DE3 File Offset: 0x0009EFE3
		public void ClearFailedSession()
		{
			this._FailedSession.Clear();
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x000A0DF0 File Offset: 0x0009EFF0
		public void SetFailedSession(List<SessionIdentifier> val)
		{
			this.FailedSession = val;
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x000A0DFC File Offset: 0x0009EFFC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (SessionIdentifier sessionIdentifier in this.FailedSession)
			{
				num ^= sessionIdentifier.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000A0E60 File Offset: 0x0009F060
		public override bool Equals(object obj)
		{
			MarkSessionsAliveResponse markSessionsAliveResponse = obj as MarkSessionsAliveResponse;
			if (markSessionsAliveResponse == null)
			{
				return false;
			}
			if (this.FailedSession.Count != markSessionsAliveResponse.FailedSession.Count)
			{
				return false;
			}
			for (int i = 0; i < this.FailedSession.Count; i++)
			{
				if (!this.FailedSession[i].Equals(markSessionsAliveResponse.FailedSession[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002F75 RID: 12149 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x000A0ECB File Offset: 0x0009F0CB
		public static MarkSessionsAliveResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MarkSessionsAliveResponse>(bs, 0, -1);
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000A0ED5 File Offset: 0x0009F0D5
		public void Deserialize(Stream stream)
		{
			MarkSessionsAliveResponse.Deserialize(stream, this);
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x000A0EDF File Offset: 0x0009F0DF
		public static MarkSessionsAliveResponse Deserialize(Stream stream, MarkSessionsAliveResponse instance)
		{
			return MarkSessionsAliveResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x000A0EEC File Offset: 0x0009F0EC
		public static MarkSessionsAliveResponse DeserializeLengthDelimited(Stream stream)
		{
			MarkSessionsAliveResponse markSessionsAliveResponse = new MarkSessionsAliveResponse();
			MarkSessionsAliveResponse.DeserializeLengthDelimited(stream, markSessionsAliveResponse);
			return markSessionsAliveResponse;
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x000A0F08 File Offset: 0x0009F108
		public static MarkSessionsAliveResponse DeserializeLengthDelimited(Stream stream, MarkSessionsAliveResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MarkSessionsAliveResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000A0F30 File Offset: 0x0009F130
		public static MarkSessionsAliveResponse Deserialize(Stream stream, MarkSessionsAliveResponse instance, long limit)
		{
			if (instance.FailedSession == null)
			{
				instance.FailedSession = new List<SessionIdentifier>();
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
				else if (num == 10)
				{
					instance.FailedSession.Add(SessionIdentifier.DeserializeLengthDelimited(stream));
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

		// Token: 0x06002F7C RID: 12156 RVA: 0x000A0FC8 File Offset: 0x0009F1C8
		public void Serialize(Stream stream)
		{
			MarkSessionsAliveResponse.Serialize(stream, this);
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000A0FD4 File Offset: 0x0009F1D4
		public static void Serialize(Stream stream, MarkSessionsAliveResponse instance)
		{
			if (instance.FailedSession.Count > 0)
			{
				foreach (SessionIdentifier sessionIdentifier in instance.FailedSession)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, sessionIdentifier.GetSerializedSize());
					SessionIdentifier.Serialize(stream, sessionIdentifier);
				}
			}
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000A104C File Offset: 0x0009F24C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.FailedSession.Count > 0)
			{
				foreach (SessionIdentifier sessionIdentifier in this.FailedSession)
				{
					num += 1U;
					uint serializedSize = sessionIdentifier.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x0400130C RID: 4876
		private List<SessionIdentifier> _FailedSession = new List<SessionIdentifier>();
	}
}
