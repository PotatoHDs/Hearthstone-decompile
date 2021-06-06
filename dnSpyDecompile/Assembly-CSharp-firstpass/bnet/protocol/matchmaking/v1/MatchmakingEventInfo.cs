using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003CB RID: 971
	public class MatchmakingEventInfo : IProtoBuf
	{
		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06003F80 RID: 16256 RVA: 0x000CB027 File Offset: 0x000C9227
		// (set) Token: 0x06003F81 RID: 16257 RVA: 0x000CB02F File Offset: 0x000C922F
		public List<Player> Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06003F82 RID: 16258 RVA: 0x000CB027 File Offset: 0x000C9227
		public List<Player> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06003F83 RID: 16259 RVA: 0x000CB038 File Offset: 0x000C9238
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x000CB045 File Offset: 0x000C9245
		public void AddPlayer(Player val)
		{
			this._Player.Add(val);
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x000CB053 File Offset: 0x000C9253
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x000CB060 File Offset: 0x000C9260
		public void SetPlayer(List<Player> val)
		{
			this.Player = val;
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06003F87 RID: 16263 RVA: 0x000CB069 File Offset: 0x000C9269
		// (set) Token: 0x06003F88 RID: 16264 RVA: 0x000CB071 File Offset: 0x000C9271
		public List<RequestId> RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06003F89 RID: 16265 RVA: 0x000CB069 File Offset: 0x000C9269
		public List<RequestId> RequestIdList
		{
			get
			{
				return this._RequestId;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x000CB07A File Offset: 0x000C927A
		public int RequestIdCount
		{
			get
			{
				return this._RequestId.Count;
			}
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x000CB087 File Offset: 0x000C9287
		public void AddRequestId(RequestId val)
		{
			this._RequestId.Add(val);
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x000CB095 File Offset: 0x000C9295
		public void ClearRequestId()
		{
			this._RequestId.Clear();
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x000CB0A2 File Offset: 0x000C92A2
		public void SetRequestId(List<RequestId> val)
		{
			this.RequestId = val;
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x000CB0AC File Offset: 0x000C92AC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Player player in this.Player)
			{
				num ^= player.GetHashCode();
			}
			foreach (RequestId requestId in this.RequestId)
			{
				num ^= requestId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x000CB154 File Offset: 0x000C9354
		public override bool Equals(object obj)
		{
			MatchmakingEventInfo matchmakingEventInfo = obj as MatchmakingEventInfo;
			if (matchmakingEventInfo == null)
			{
				return false;
			}
			if (this.Player.Count != matchmakingEventInfo.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Player.Count; i++)
			{
				if (!this.Player[i].Equals(matchmakingEventInfo.Player[i]))
				{
					return false;
				}
			}
			if (this.RequestId.Count != matchmakingEventInfo.RequestId.Count)
			{
				return false;
			}
			for (int j = 0; j < this.RequestId.Count; j++)
			{
				if (!this.RequestId[j].Equals(matchmakingEventInfo.RequestId[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06003F90 RID: 16272 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x000CB210 File Offset: 0x000C9410
		public static MatchmakingEventInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MatchmakingEventInfo>(bs, 0, -1);
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x000CB21A File Offset: 0x000C941A
		public void Deserialize(Stream stream)
		{
			MatchmakingEventInfo.Deserialize(stream, this);
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x000CB224 File Offset: 0x000C9424
		public static MatchmakingEventInfo Deserialize(Stream stream, MatchmakingEventInfo instance)
		{
			return MatchmakingEventInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x000CB230 File Offset: 0x000C9430
		public static MatchmakingEventInfo DeserializeLengthDelimited(Stream stream)
		{
			MatchmakingEventInfo matchmakingEventInfo = new MatchmakingEventInfo();
			MatchmakingEventInfo.DeserializeLengthDelimited(stream, matchmakingEventInfo);
			return matchmakingEventInfo;
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x000CB24C File Offset: 0x000C944C
		public static MatchmakingEventInfo DeserializeLengthDelimited(Stream stream, MatchmakingEventInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MatchmakingEventInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x000CB274 File Offset: 0x000C9474
		public static MatchmakingEventInfo Deserialize(Stream stream, MatchmakingEventInfo instance, long limit)
		{
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
			}
			if (instance.RequestId == null)
			{
				instance.RequestId = new List<RequestId>();
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
					if (num != 18)
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
						instance.RequestId.Add(bnet.protocol.matchmaking.v1.RequestId.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Player.Add(bnet.protocol.matchmaking.v1.Player.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x000CB33C File Offset: 0x000C953C
		public void Serialize(Stream stream)
		{
			MatchmakingEventInfo.Serialize(stream, this);
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x000CB348 File Offset: 0x000C9548
		public static void Serialize(Stream stream, MatchmakingEventInfo instance)
		{
			if (instance.Player.Count > 0)
			{
				foreach (Player player in instance.Player)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, player.GetSerializedSize());
					bnet.protocol.matchmaking.v1.Player.Serialize(stream, player);
				}
			}
			if (instance.RequestId.Count > 0)
			{
				foreach (RequestId requestId in instance.RequestId)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, requestId.GetSerializedSize());
					bnet.protocol.matchmaking.v1.RequestId.Serialize(stream, requestId);
				}
			}
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x000CB424 File Offset: 0x000C9624
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Player.Count > 0)
			{
				foreach (Player player in this.Player)
				{
					num += 1U;
					uint serializedSize = player.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.RequestId.Count > 0)
			{
				foreach (RequestId requestId in this.RequestId)
				{
					num += 1U;
					uint serializedSize2 = requestId.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x04001641 RID: 5697
		private List<Player> _Player = new List<Player>();

		// Token: 0x04001642 RID: 5698
		private List<RequestId> _RequestId = new List<RequestId>();
	}
}
