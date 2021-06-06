using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.session.v1
{
	// Token: 0x0200030D RID: 781
	public class MarkSessionsAliveRequest : IProtoBuf
	{
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x000A0A9B File Offset: 0x0009EC9B
		// (set) Token: 0x06002F59 RID: 12121 RVA: 0x000A0AA3 File Offset: 0x0009ECA3
		public List<SessionIdentifier> Session
		{
			get
			{
				return this._Session;
			}
			set
			{
				this._Session = value;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002F5A RID: 12122 RVA: 0x000A0A9B File Offset: 0x0009EC9B
		public List<SessionIdentifier> SessionList
		{
			get
			{
				return this._Session;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002F5B RID: 12123 RVA: 0x000A0AAC File Offset: 0x0009ECAC
		public int SessionCount
		{
			get
			{
				return this._Session.Count;
			}
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000A0AB9 File Offset: 0x0009ECB9
		public void AddSession(SessionIdentifier val)
		{
			this._Session.Add(val);
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000A0AC7 File Offset: 0x0009ECC7
		public void ClearSession()
		{
			this._Session.Clear();
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000A0AD4 File Offset: 0x0009ECD4
		public void SetSession(List<SessionIdentifier> val)
		{
			this.Session = val;
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x000A0AE0 File Offset: 0x0009ECE0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (SessionIdentifier sessionIdentifier in this.Session)
			{
				num ^= sessionIdentifier.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x000A0B44 File Offset: 0x0009ED44
		public override bool Equals(object obj)
		{
			MarkSessionsAliveRequest markSessionsAliveRequest = obj as MarkSessionsAliveRequest;
			if (markSessionsAliveRequest == null)
			{
				return false;
			}
			if (this.Session.Count != markSessionsAliveRequest.Session.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Session.Count; i++)
			{
				if (!this.Session[i].Equals(markSessionsAliveRequest.Session[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002F61 RID: 12129 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000A0BAF File Offset: 0x0009EDAF
		public static MarkSessionsAliveRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MarkSessionsAliveRequest>(bs, 0, -1);
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x000A0BB9 File Offset: 0x0009EDB9
		public void Deserialize(Stream stream)
		{
			MarkSessionsAliveRequest.Deserialize(stream, this);
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x000A0BC3 File Offset: 0x0009EDC3
		public static MarkSessionsAliveRequest Deserialize(Stream stream, MarkSessionsAliveRequest instance)
		{
			return MarkSessionsAliveRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000A0BD0 File Offset: 0x0009EDD0
		public static MarkSessionsAliveRequest DeserializeLengthDelimited(Stream stream)
		{
			MarkSessionsAliveRequest markSessionsAliveRequest = new MarkSessionsAliveRequest();
			MarkSessionsAliveRequest.DeserializeLengthDelimited(stream, markSessionsAliveRequest);
			return markSessionsAliveRequest;
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x000A0BEC File Offset: 0x0009EDEC
		public static MarkSessionsAliveRequest DeserializeLengthDelimited(Stream stream, MarkSessionsAliveRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MarkSessionsAliveRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x000A0C14 File Offset: 0x0009EE14
		public static MarkSessionsAliveRequest Deserialize(Stream stream, MarkSessionsAliveRequest instance, long limit)
		{
			if (instance.Session == null)
			{
				instance.Session = new List<SessionIdentifier>();
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
					instance.Session.Add(SessionIdentifier.DeserializeLengthDelimited(stream));
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

		// Token: 0x06002F68 RID: 12136 RVA: 0x000A0CAC File Offset: 0x0009EEAC
		public void Serialize(Stream stream)
		{
			MarkSessionsAliveRequest.Serialize(stream, this);
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x000A0CB8 File Offset: 0x0009EEB8
		public static void Serialize(Stream stream, MarkSessionsAliveRequest instance)
		{
			if (instance.Session.Count > 0)
			{
				foreach (SessionIdentifier sessionIdentifier in instance.Session)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, sessionIdentifier.GetSerializedSize());
					SessionIdentifier.Serialize(stream, sessionIdentifier);
				}
			}
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x000A0D30 File Offset: 0x0009EF30
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Session.Count > 0)
			{
				foreach (SessionIdentifier sessionIdentifier in this.Session)
				{
					num += 1U;
					uint serializedSize = sessionIdentifier.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x0400130B RID: 4875
		private List<SessionIdentifier> _Session = new List<SessionIdentifier>();
	}
}
