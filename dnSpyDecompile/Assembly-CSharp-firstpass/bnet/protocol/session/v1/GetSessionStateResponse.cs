using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	// Token: 0x02000310 RID: 784
	public class GetSessionStateResponse : IProtoBuf
	{
		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002F93 RID: 12179 RVA: 0x000A139B File Offset: 0x0009F59B
		// (set) Token: 0x06002F94 RID: 12180 RVA: 0x000A13A3 File Offset: 0x0009F5A3
		public GameAccountHandle Handle
		{
			get
			{
				return this._Handle;
			}
			set
			{
				this._Handle = value;
				this.HasHandle = (value != null);
			}
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000A13B6 File Offset: 0x0009F5B6
		public void SetHandle(GameAccountHandle val)
		{
			this.Handle = val;
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x000A13BF File Offset: 0x0009F5BF
		// (set) Token: 0x06002F97 RID: 12183 RVA: 0x000A13C7 File Offset: 0x0009F5C7
		public SessionState Session
		{
			get
			{
				return this._Session;
			}
			set
			{
				this._Session = value;
				this.HasSession = (value != null);
			}
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000A13DA File Offset: 0x0009F5DA
		public void SetSession(SessionState val)
		{
			this.Session = val;
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x000A13E4 File Offset: 0x0009F5E4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasHandle)
			{
				num ^= this.Handle.GetHashCode();
			}
			if (this.HasSession)
			{
				num ^= this.Session.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000A142C File Offset: 0x0009F62C
		public override bool Equals(object obj)
		{
			GetSessionStateResponse getSessionStateResponse = obj as GetSessionStateResponse;
			return getSessionStateResponse != null && this.HasHandle == getSessionStateResponse.HasHandle && (!this.HasHandle || this.Handle.Equals(getSessionStateResponse.Handle)) && this.HasSession == getSessionStateResponse.HasSession && (!this.HasSession || this.Session.Equals(getSessionStateResponse.Session));
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002F9B RID: 12187 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000A149C File Offset: 0x0009F69C
		public static GetSessionStateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetSessionStateResponse>(bs, 0, -1);
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000A14A6 File Offset: 0x0009F6A6
		public void Deserialize(Stream stream)
		{
			GetSessionStateResponse.Deserialize(stream, this);
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000A14B0 File Offset: 0x0009F6B0
		public static GetSessionStateResponse Deserialize(Stream stream, GetSessionStateResponse instance)
		{
			return GetSessionStateResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000A14BC File Offset: 0x0009F6BC
		public static GetSessionStateResponse DeserializeLengthDelimited(Stream stream)
		{
			GetSessionStateResponse getSessionStateResponse = new GetSessionStateResponse();
			GetSessionStateResponse.DeserializeLengthDelimited(stream, getSessionStateResponse);
			return getSessionStateResponse;
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000A14D8 File Offset: 0x0009F6D8
		public static GetSessionStateResponse DeserializeLengthDelimited(Stream stream, GetSessionStateResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetSessionStateResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000A1500 File Offset: 0x0009F700
		public static GetSessionStateResponse Deserialize(Stream stream, GetSessionStateResponse instance, long limit)
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
					else if (instance.Session == null)
					{
						instance.Session = SessionState.DeserializeLengthDelimited(stream);
					}
					else
					{
						SessionState.DeserializeLengthDelimited(stream, instance.Session);
					}
				}
				else if (instance.Handle == null)
				{
					instance.Handle = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.Handle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000A15D2 File Offset: 0x0009F7D2
		public void Serialize(Stream stream)
		{
			GetSessionStateResponse.Serialize(stream, this);
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000A15DC File Offset: 0x0009F7DC
		public static void Serialize(Stream stream, GetSessionStateResponse instance)
		{
			if (instance.HasHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Handle.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Handle);
			}
			if (instance.HasSession)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Session.GetSerializedSize());
				SessionState.Serialize(stream, instance.Session);
			}
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000A1644 File Offset: 0x0009F844
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasHandle)
			{
				num += 1U;
				uint serializedSize = this.Handle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSession)
			{
				num += 1U;
				uint serializedSize2 = this.Session.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001311 RID: 4881
		public bool HasHandle;

		// Token: 0x04001312 RID: 4882
		private GameAccountHandle _Handle;

		// Token: 0x04001313 RID: 4883
		public bool HasSession;

		// Token: 0x04001314 RID: 4884
		private SessionState _Session;
	}
}
