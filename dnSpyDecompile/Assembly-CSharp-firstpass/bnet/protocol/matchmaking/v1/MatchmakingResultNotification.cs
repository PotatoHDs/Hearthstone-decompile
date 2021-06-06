using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003ED RID: 1005
	public class MatchmakingResultNotification : IProtoBuf
	{
		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x000D2FC6 File Offset: 0x000D11C6
		// (set) Token: 0x0600427B RID: 17019 RVA: 0x000D2FCE File Offset: 0x000D11CE
		public RequestId RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
				this.HasRequestId = (value != null);
			}
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x000D2FE1 File Offset: 0x000D11E1
		public void SetRequestId(RequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x0600427D RID: 17021 RVA: 0x000D2FEA File Offset: 0x000D11EA
		// (set) Token: 0x0600427E RID: 17022 RVA: 0x000D2FF2 File Offset: 0x000D11F2
		public uint Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				this._Result = value;
				this.HasResult = true;
			}
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x000D3002 File Offset: 0x000D1202
		public void SetResult(uint val)
		{
			this.Result = val;
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06004280 RID: 17024 RVA: 0x000D300B File Offset: 0x000D120B
		// (set) Token: 0x06004281 RID: 17025 RVA: 0x000D3013 File Offset: 0x000D1213
		public ConnectInfo ConnectInfo
		{
			get
			{
				return this._ConnectInfo;
			}
			set
			{
				this._ConnectInfo = value;
				this.HasConnectInfo = (value != null);
			}
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x000D3026 File Offset: 0x000D1226
		public void SetConnectInfo(ConnectInfo val)
		{
			this.ConnectInfo = val;
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06004283 RID: 17027 RVA: 0x000D302F File Offset: 0x000D122F
		// (set) Token: 0x06004284 RID: 17028 RVA: 0x000D3037 File Offset: 0x000D1237
		public GameHandle GameHandle
		{
			get
			{
				return this._GameHandle;
			}
			set
			{
				this._GameHandle = value;
				this.HasGameHandle = (value != null);
			}
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x000D304A File Offset: 0x000D124A
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x000D3054 File Offset: 0x000D1254
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			if (this.HasResult)
			{
				num ^= this.Result.GetHashCode();
			}
			if (this.HasConnectInfo)
			{
				num ^= this.ConnectInfo.GetHashCode();
			}
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x000D30CC File Offset: 0x000D12CC
		public override bool Equals(object obj)
		{
			MatchmakingResultNotification matchmakingResultNotification = obj as MatchmakingResultNotification;
			return matchmakingResultNotification != null && this.HasRequestId == matchmakingResultNotification.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(matchmakingResultNotification.RequestId)) && this.HasResult == matchmakingResultNotification.HasResult && (!this.HasResult || this.Result.Equals(matchmakingResultNotification.Result)) && this.HasConnectInfo == matchmakingResultNotification.HasConnectInfo && (!this.HasConnectInfo || this.ConnectInfo.Equals(matchmakingResultNotification.ConnectInfo)) && this.HasGameHandle == matchmakingResultNotification.HasGameHandle && (!this.HasGameHandle || this.GameHandle.Equals(matchmakingResultNotification.GameHandle));
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06004288 RID: 17032 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x000D3195 File Offset: 0x000D1395
		public static MatchmakingResultNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MatchmakingResultNotification>(bs, 0, -1);
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x000D319F File Offset: 0x000D139F
		public void Deserialize(Stream stream)
		{
			MatchmakingResultNotification.Deserialize(stream, this);
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x000D31A9 File Offset: 0x000D13A9
		public static MatchmakingResultNotification Deserialize(Stream stream, MatchmakingResultNotification instance)
		{
			return MatchmakingResultNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x000D31B4 File Offset: 0x000D13B4
		public static MatchmakingResultNotification DeserializeLengthDelimited(Stream stream)
		{
			MatchmakingResultNotification matchmakingResultNotification = new MatchmakingResultNotification();
			MatchmakingResultNotification.DeserializeLengthDelimited(stream, matchmakingResultNotification);
			return matchmakingResultNotification;
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x000D31D0 File Offset: 0x000D13D0
		public static MatchmakingResultNotification DeserializeLengthDelimited(Stream stream, MatchmakingResultNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MatchmakingResultNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x000D31F8 File Offset: 0x000D13F8
		public static MatchmakingResultNotification Deserialize(Stream stream, MatchmakingResultNotification instance, long limit)
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
				else
				{
					if (num <= 16)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.Result = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (instance.RequestId == null)
							{
								instance.RequestId = RequestId.DeserializeLengthDelimited(stream);
								continue;
							}
							RequestId.DeserializeLengthDelimited(stream, instance.RequestId);
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 34)
						{
							if (instance.GameHandle == null)
							{
								instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
							continue;
						}
					}
					else
					{
						if (instance.ConnectInfo == null)
						{
							instance.ConnectInfo = ConnectInfo.DeserializeLengthDelimited(stream);
							continue;
						}
						ConnectInfo.DeserializeLengthDelimited(stream, instance.ConnectInfo);
						continue;
					}
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

		// Token: 0x0600428F RID: 17039 RVA: 0x000D3320 File Offset: 0x000D1520
		public void Serialize(Stream stream)
		{
			MatchmakingResultNotification.Serialize(stream, this);
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x000D332C File Offset: 0x000D152C
		public static void Serialize(Stream stream, MatchmakingResultNotification instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasResult)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Result);
			}
			if (instance.HasConnectInfo)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ConnectInfo.GetSerializedSize());
				ConnectInfo.Serialize(stream, instance.ConnectInfo);
			}
			if (instance.HasGameHandle)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x000D33DC File Offset: 0x000D15DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestId)
			{
				num += 1U;
				uint serializedSize = this.RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasResult)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Result);
			}
			if (this.HasConnectInfo)
			{
				num += 1U;
				uint serializedSize2 = this.ConnectInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize3 = this.GameHandle.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x040016D8 RID: 5848
		public bool HasRequestId;

		// Token: 0x040016D9 RID: 5849
		private RequestId _RequestId;

		// Token: 0x040016DA RID: 5850
		public bool HasResult;

		// Token: 0x040016DB RID: 5851
		private uint _Result;

		// Token: 0x040016DC RID: 5852
		public bool HasConnectInfo;

		// Token: 0x040016DD RID: 5853
		private ConnectInfo _ConnectInfo;

		// Token: 0x040016DE RID: 5854
		public bool HasGameHandle;

		// Token: 0x040016DF RID: 5855
		private GameHandle _GameHandle;
	}
}
