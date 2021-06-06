using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003C3 RID: 963
	public class GameServerRemovedNotification : IProtoBuf
	{
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06003EDB RID: 16091 RVA: 0x000C9757 File Offset: 0x000C7957
		// (set) Token: 0x06003EDC RID: 16092 RVA: 0x000C975F File Offset: 0x000C795F
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

		// Token: 0x06003EDD RID: 16093 RVA: 0x000C976F File Offset: 0x000C796F
		public void SetMatchmakerId(uint val)
		{
			this.MatchmakerId = val;
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06003EDE RID: 16094 RVA: 0x000C9778 File Offset: 0x000C7978
		// (set) Token: 0x06003EDF RID: 16095 RVA: 0x000C9780 File Offset: 0x000C7980
		public HostProxyPair GameServer
		{
			get
			{
				return this._GameServer;
			}
			set
			{
				this._GameServer = value;
				this.HasGameServer = (value != null);
			}
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x000C9793 File Offset: 0x000C7993
		public void SetGameServer(HostProxyPair val)
		{
			this.GameServer = val;
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06003EE1 RID: 16097 RVA: 0x000C979C File Offset: 0x000C799C
		// (set) Token: 0x06003EE2 RID: 16098 RVA: 0x000C97A4 File Offset: 0x000C79A4
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

		// Token: 0x06003EE3 RID: 16099 RVA: 0x000C97B4 File Offset: 0x000C79B4
		public void SetMatchmakerGuid(ulong val)
		{
			this.MatchmakerGuid = val;
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06003EE4 RID: 16100 RVA: 0x000C97BD File Offset: 0x000C79BD
		// (set) Token: 0x06003EE5 RID: 16101 RVA: 0x000C97C5 File Offset: 0x000C79C5
		public ulong GameServerGuid
		{
			get
			{
				return this._GameServerGuid;
			}
			set
			{
				this._GameServerGuid = value;
				this.HasGameServerGuid = true;
			}
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x000C97D5 File Offset: 0x000C79D5
		public void SetGameServerGuid(ulong val)
		{
			this.GameServerGuid = val;
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x000C97E0 File Offset: 0x000C79E0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMatchmakerId)
			{
				num ^= this.MatchmakerId.GetHashCode();
			}
			if (this.HasGameServer)
			{
				num ^= this.GameServer.GetHashCode();
			}
			if (this.HasMatchmakerGuid)
			{
				num ^= this.MatchmakerGuid.GetHashCode();
			}
			if (this.HasGameServerGuid)
			{
				num ^= this.GameServerGuid.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x000C985C File Offset: 0x000C7A5C
		public override bool Equals(object obj)
		{
			GameServerRemovedNotification gameServerRemovedNotification = obj as GameServerRemovedNotification;
			return gameServerRemovedNotification != null && this.HasMatchmakerId == gameServerRemovedNotification.HasMatchmakerId && (!this.HasMatchmakerId || this.MatchmakerId.Equals(gameServerRemovedNotification.MatchmakerId)) && this.HasGameServer == gameServerRemovedNotification.HasGameServer && (!this.HasGameServer || this.GameServer.Equals(gameServerRemovedNotification.GameServer)) && this.HasMatchmakerGuid == gameServerRemovedNotification.HasMatchmakerGuid && (!this.HasMatchmakerGuid || this.MatchmakerGuid.Equals(gameServerRemovedNotification.MatchmakerGuid)) && this.HasGameServerGuid == gameServerRemovedNotification.HasGameServerGuid && (!this.HasGameServerGuid || this.GameServerGuid.Equals(gameServerRemovedNotification.GameServerGuid));
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06003EE9 RID: 16105 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x000C992B File Offset: 0x000C7B2B
		public static GameServerRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameServerRemovedNotification>(bs, 0, -1);
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x000C9935 File Offset: 0x000C7B35
		public void Deserialize(Stream stream)
		{
			GameServerRemovedNotification.Deserialize(stream, this);
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x000C993F File Offset: 0x000C7B3F
		public static GameServerRemovedNotification Deserialize(Stream stream, GameServerRemovedNotification instance)
		{
			return GameServerRemovedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x000C994C File Offset: 0x000C7B4C
		public static GameServerRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			GameServerRemovedNotification gameServerRemovedNotification = new GameServerRemovedNotification();
			GameServerRemovedNotification.DeserializeLengthDelimited(stream, gameServerRemovedNotification);
			return gameServerRemovedNotification;
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x000C9968 File Offset: 0x000C7B68
		public static GameServerRemovedNotification DeserializeLengthDelimited(Stream stream, GameServerRemovedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameServerRemovedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x000C9990 File Offset: 0x000C7B90
		public static GameServerRemovedNotification Deserialize(Stream stream, GameServerRemovedNotification instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
							if (instance.GameServer == null)
							{
								instance.GameServer = HostProxyPair.DeserializeLengthDelimited(stream);
								continue;
							}
							HostProxyPair.DeserializeLengthDelimited(stream, instance.GameServer);
							continue;
						}
					}
					else
					{
						if (num == 25)
						{
							instance.MatchmakerGuid = binaryReader.ReadUInt64();
							continue;
						}
						if (num == 33)
						{
							instance.GameServerGuid = binaryReader.ReadUInt64();
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

		// Token: 0x06003EF0 RID: 16112 RVA: 0x000C9A82 File Offset: 0x000C7C82
		public void Serialize(Stream stream)
		{
			GameServerRemovedNotification.Serialize(stream, this);
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x000C9A8C File Offset: 0x000C7C8C
		public static void Serialize(Stream stream, GameServerRemovedNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.MatchmakerId);
			}
			if (instance.HasGameServer)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameServer.GetSerializedSize());
				HostProxyPair.Serialize(stream, instance.GameServer);
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
			if (instance.HasGameServerGuid)
			{
				stream.WriteByte(33);
				binaryWriter.Write(instance.GameServerGuid);
			}
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x000C9B24 File Offset: 0x000C7D24
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMatchmakerId)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasGameServer)
			{
				num += 1U;
				uint serializedSize = this.GameServer.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasMatchmakerGuid)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasGameServerGuid)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x0400161C RID: 5660
		public bool HasMatchmakerId;

		// Token: 0x0400161D RID: 5661
		private uint _MatchmakerId;

		// Token: 0x0400161E RID: 5662
		public bool HasGameServer;

		// Token: 0x0400161F RID: 5663
		private HostProxyPair _GameServer;

		// Token: 0x04001620 RID: 5664
		public bool HasMatchmakerGuid;

		// Token: 0x04001621 RID: 5665
		private ulong _MatchmakerGuid;

		// Token: 0x04001622 RID: 5666
		public bool HasGameServerGuid;

		// Token: 0x04001623 RID: 5667
		private ulong _GameServerGuid;
	}
}
