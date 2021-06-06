using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000515 RID: 1301
	public class GetGameSessionInfoResponse : IProtoBuf
	{
		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06005CBB RID: 23739 RVA: 0x00119C8B File Offset: 0x00117E8B
		// (set) Token: 0x06005CBC RID: 23740 RVA: 0x00119C93 File Offset: 0x00117E93
		public GameSessionInfo SessionInfo
		{
			get
			{
				return this._SessionInfo;
			}
			set
			{
				this._SessionInfo = value;
				this.HasSessionInfo = (value != null);
			}
		}

		// Token: 0x06005CBD RID: 23741 RVA: 0x00119CA6 File Offset: 0x00117EA6
		public void SetSessionInfo(GameSessionInfo val)
		{
			this.SessionInfo = val;
		}

		// Token: 0x06005CBE RID: 23742 RVA: 0x00119CB0 File Offset: 0x00117EB0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSessionInfo)
			{
				num ^= this.SessionInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005CBF RID: 23743 RVA: 0x00119CE0 File Offset: 0x00117EE0
		public override bool Equals(object obj)
		{
			GetGameSessionInfoResponse getGameSessionInfoResponse = obj as GetGameSessionInfoResponse;
			return getGameSessionInfoResponse != null && this.HasSessionInfo == getGameSessionInfoResponse.HasSessionInfo && (!this.HasSessionInfo || this.SessionInfo.Equals(getGameSessionInfoResponse.SessionInfo));
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06005CC0 RID: 23744 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005CC1 RID: 23745 RVA: 0x00119D25 File Offset: 0x00117F25
		public static GetGameSessionInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameSessionInfoResponse>(bs, 0, -1);
		}

		// Token: 0x06005CC2 RID: 23746 RVA: 0x00119D2F File Offset: 0x00117F2F
		public void Deserialize(Stream stream)
		{
			GetGameSessionInfoResponse.Deserialize(stream, this);
		}

		// Token: 0x06005CC3 RID: 23747 RVA: 0x00119D39 File Offset: 0x00117F39
		public static GetGameSessionInfoResponse Deserialize(Stream stream, GetGameSessionInfoResponse instance)
		{
			return GetGameSessionInfoResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005CC4 RID: 23748 RVA: 0x00119D44 File Offset: 0x00117F44
		public static GetGameSessionInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGameSessionInfoResponse getGameSessionInfoResponse = new GetGameSessionInfoResponse();
			GetGameSessionInfoResponse.DeserializeLengthDelimited(stream, getGameSessionInfoResponse);
			return getGameSessionInfoResponse;
		}

		// Token: 0x06005CC5 RID: 23749 RVA: 0x00119D60 File Offset: 0x00117F60
		public static GetGameSessionInfoResponse DeserializeLengthDelimited(Stream stream, GetGameSessionInfoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameSessionInfoResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005CC6 RID: 23750 RVA: 0x00119D88 File Offset: 0x00117F88
		public static GetGameSessionInfoResponse Deserialize(Stream stream, GetGameSessionInfoResponse instance, long limit)
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
				else if (num == 18)
				{
					if (instance.SessionInfo == null)
					{
						instance.SessionInfo = GameSessionInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameSessionInfo.DeserializeLengthDelimited(stream, instance.SessionInfo);
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

		// Token: 0x06005CC7 RID: 23751 RVA: 0x00119E22 File Offset: 0x00118022
		public void Serialize(Stream stream)
		{
			GetGameSessionInfoResponse.Serialize(stream, this);
		}

		// Token: 0x06005CC8 RID: 23752 RVA: 0x00119E2B File Offset: 0x0011802B
		public static void Serialize(Stream stream, GetGameSessionInfoResponse instance)
		{
			if (instance.HasSessionInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SessionInfo.GetSerializedSize());
				GameSessionInfo.Serialize(stream, instance.SessionInfo);
			}
		}

		// Token: 0x06005CC9 RID: 23753 RVA: 0x00119E5C File Offset: 0x0011805C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSessionInfo)
			{
				num += 1U;
				uint serializedSize = this.SessionInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001CAA RID: 7338
		public bool HasSessionInfo;

		// Token: 0x04001CAB RID: 7339
		private GameSessionInfo _SessionInfo;
	}
}
