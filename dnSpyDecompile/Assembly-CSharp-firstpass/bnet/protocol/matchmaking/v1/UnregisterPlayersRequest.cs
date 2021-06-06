using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003BE RID: 958
	public class UnregisterPlayersRequest : IProtoBuf
	{
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06003E78 RID: 15992 RVA: 0x000C88A3 File Offset: 0x000C6AA3
		// (set) Token: 0x06003E79 RID: 15993 RVA: 0x000C88AB File Offset: 0x000C6AAB
		public uint MatchmakerId
		{
			get
			{
				return this._MatchmakerId;
			}
			set
			{
				this._MatchmakerId = value;
				this.HasMatchmakerId = true;
			}
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x000C88BB File Offset: 0x000C6ABB
		public void SetMatchmakerId(uint val)
		{
			this.MatchmakerId = val;
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06003E7B RID: 15995 RVA: 0x000C88C4 File Offset: 0x000C6AC4
		// (set) Token: 0x06003E7C RID: 15996 RVA: 0x000C88CC File Offset: 0x000C6ACC
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

		// Token: 0x06003E7D RID: 15997 RVA: 0x000C88DF File Offset: 0x000C6ADF
		public void SetRequestId(RequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06003E7E RID: 15998 RVA: 0x000C88E8 File Offset: 0x000C6AE8
		// (set) Token: 0x06003E7F RID: 15999 RVA: 0x000C88F0 File Offset: 0x000C6AF0
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

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06003E80 RID: 16000 RVA: 0x000C88E8 File Offset: 0x000C6AE8
		public List<Player> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06003E81 RID: 16001 RVA: 0x000C88F9 File Offset: 0x000C6AF9
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x000C8906 File Offset: 0x000C6B06
		public void AddPlayer(Player val)
		{
			this._Player.Add(val);
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x000C8914 File Offset: 0x000C6B14
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x000C8921 File Offset: 0x000C6B21
		public void SetPlayer(List<Player> val)
		{
			this.Player = val;
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06003E85 RID: 16005 RVA: 0x000C892A File Offset: 0x000C6B2A
		// (set) Token: 0x06003E86 RID: 16006 RVA: 0x000C8932 File Offset: 0x000C6B32
		public ulong MatchmakerGuid
		{
			get
			{
				return this._MatchmakerGuid;
			}
			set
			{
				this._MatchmakerGuid = value;
				this.HasMatchmakerGuid = true;
			}
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x000C8942 File Offset: 0x000C6B42
		public void SetMatchmakerGuid(ulong val)
		{
			this.MatchmakerGuid = val;
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x000C894C File Offset: 0x000C6B4C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMatchmakerId)
			{
				num ^= this.MatchmakerId.GetHashCode();
			}
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			foreach (Player player in this.Player)
			{
				num ^= player.GetHashCode();
			}
			if (this.HasMatchmakerGuid)
			{
				num ^= this.MatchmakerGuid.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x000C89F8 File Offset: 0x000C6BF8
		public override bool Equals(object obj)
		{
			UnregisterPlayersRequest unregisterPlayersRequest = obj as UnregisterPlayersRequest;
			if (unregisterPlayersRequest == null)
			{
				return false;
			}
			if (this.HasMatchmakerId != unregisterPlayersRequest.HasMatchmakerId || (this.HasMatchmakerId && !this.MatchmakerId.Equals(unregisterPlayersRequest.MatchmakerId)))
			{
				return false;
			}
			if (this.HasRequestId != unregisterPlayersRequest.HasRequestId || (this.HasRequestId && !this.RequestId.Equals(unregisterPlayersRequest.RequestId)))
			{
				return false;
			}
			if (this.Player.Count != unregisterPlayersRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Player.Count; i++)
			{
				if (!this.Player[i].Equals(unregisterPlayersRequest.Player[i]))
				{
					return false;
				}
			}
			return this.HasMatchmakerGuid == unregisterPlayersRequest.HasMatchmakerGuid && (!this.HasMatchmakerGuid || this.MatchmakerGuid.Equals(unregisterPlayersRequest.MatchmakerGuid));
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x000C8AEA File Offset: 0x000C6CEA
		public static UnregisterPlayersRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnregisterPlayersRequest>(bs, 0, -1);
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x000C8AF4 File Offset: 0x000C6CF4
		public void Deserialize(Stream stream)
		{
			UnregisterPlayersRequest.Deserialize(stream, this);
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x000C8AFE File Offset: 0x000C6CFE
		public static UnregisterPlayersRequest Deserialize(Stream stream, UnregisterPlayersRequest instance)
		{
			return UnregisterPlayersRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x000C8B0C File Offset: 0x000C6D0C
		public static UnregisterPlayersRequest DeserializeLengthDelimited(Stream stream)
		{
			UnregisterPlayersRequest unregisterPlayersRequest = new UnregisterPlayersRequest();
			UnregisterPlayersRequest.DeserializeLengthDelimited(stream, unregisterPlayersRequest);
			return unregisterPlayersRequest;
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x000C8B28 File Offset: 0x000C6D28
		public static UnregisterPlayersRequest DeserializeLengthDelimited(Stream stream, UnregisterPlayersRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnregisterPlayersRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x000C8B50 File Offset: 0x000C6D50
		public static UnregisterPlayersRequest Deserialize(Stream stream, UnregisterPlayersRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
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
				else
				{
					if (num <= 18)
					{
						if (num == 13)
						{
							instance.MatchmakerId = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 18)
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
					else
					{
						if (num == 26)
						{
							instance.Player.Add(bnet.protocol.matchmaking.v1.Player.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 33)
						{
							instance.MatchmakerGuid = binaryReader.ReadUInt64();
							continue;
						}
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

		// Token: 0x06003E91 RID: 16017 RVA: 0x000C8C5A File Offset: 0x000C6E5A
		public void Serialize(Stream stream)
		{
			UnregisterPlayersRequest.Serialize(stream, this);
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x000C8C64 File Offset: 0x000C6E64
		public static void Serialize(Stream stream, UnregisterPlayersRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.MatchmakerId);
			}
			if (instance.HasRequestId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.Player.Count > 0)
			{
				foreach (Player player in instance.Player)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, player.GetSerializedSize());
					bnet.protocol.matchmaking.v1.Player.Serialize(stream, player);
				}
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(33);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x000C8D48 File Offset: 0x000C6F48
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMatchmakerId)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasRequestId)
			{
				num += 1U;
				uint serializedSize = this.RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Player.Count > 0)
			{
				foreach (Player player in this.Player)
				{
					num += 1U;
					uint serializedSize2 = player.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasMatchmakerGuid)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x04001609 RID: 5641
		public bool HasMatchmakerId;

		// Token: 0x0400160A RID: 5642
		private uint _MatchmakerId;

		// Token: 0x0400160B RID: 5643
		public bool HasRequestId;

		// Token: 0x0400160C RID: 5644
		private RequestId _RequestId;

		// Token: 0x0400160D RID: 5645
		private List<Player> _Player = new List<Player>();

		// Token: 0x0400160E RID: 5646
		public bool HasMatchmakerGuid;

		// Token: 0x0400160F RID: 5647
		private ulong _MatchmakerGuid;
	}
}
