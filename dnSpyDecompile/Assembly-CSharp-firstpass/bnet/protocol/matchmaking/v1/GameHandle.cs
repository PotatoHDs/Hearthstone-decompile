using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003DD RID: 989
	public class GameHandle : IProtoBuf
	{
		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06004102 RID: 16642 RVA: 0x000CEE43 File Offset: 0x000CD043
		// (set) Token: 0x06004103 RID: 16643 RVA: 0x000CEE4B File Offset: 0x000CD04B
		public MatchmakerHandle Matchmaker
		{
			get
			{
				return this._Matchmaker;
			}
			set
			{
				this._Matchmaker = value;
				this.HasMatchmaker = (value != null);
			}
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x000CEE5E File Offset: 0x000CD05E
		public void SetMatchmaker(MatchmakerHandle val)
		{
			this.Matchmaker = val;
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06004105 RID: 16645 RVA: 0x000CEE67 File Offset: 0x000CD067
		// (set) Token: 0x06004106 RID: 16646 RVA: 0x000CEE6F File Offset: 0x000CD06F
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

		// Token: 0x06004107 RID: 16647 RVA: 0x000CEE82 File Offset: 0x000CD082
		public void SetGameServer(HostProxyPair val)
		{
			this.GameServer = val;
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06004108 RID: 16648 RVA: 0x000CEE8B File Offset: 0x000CD08B
		// (set) Token: 0x06004109 RID: 16649 RVA: 0x000CEE93 File Offset: 0x000CD093
		public uint GameInstanceId
		{
			get
			{
				return this._GameInstanceId;
			}
			set
			{
				this._GameInstanceId = value;
				this.HasGameInstanceId = true;
			}
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x000CEEA3 File Offset: 0x000CD0A3
		public void SetGameInstanceId(uint val)
		{
			this.GameInstanceId = val;
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x0600410B RID: 16651 RVA: 0x000CEEAC File Offset: 0x000CD0AC
		// (set) Token: 0x0600410C RID: 16652 RVA: 0x000CEEB4 File Offset: 0x000CD0B4
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

		// Token: 0x0600410D RID: 16653 RVA: 0x000CEEC4 File Offset: 0x000CD0C4
		public void SetMatchmakerGuid(ulong val)
		{
			this.MatchmakerGuid = val;
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x0600410E RID: 16654 RVA: 0x000CEECD File Offset: 0x000CD0CD
		// (set) Token: 0x0600410F RID: 16655 RVA: 0x000CEED5 File Offset: 0x000CD0D5
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

		// Token: 0x06004110 RID: 16656 RVA: 0x000CEEE5 File Offset: 0x000CD0E5
		public void SetGameServerGuid(ulong val)
		{
			this.GameServerGuid = val;
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x000CEEF0 File Offset: 0x000CD0F0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMatchmaker)
			{
				num ^= this.Matchmaker.GetHashCode();
			}
			if (this.HasGameServer)
			{
				num ^= this.GameServer.GetHashCode();
			}
			if (this.HasGameInstanceId)
			{
				num ^= this.GameInstanceId.GetHashCode();
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

		// Token: 0x06004112 RID: 16658 RVA: 0x000CEF84 File Offset: 0x000CD184
		public override bool Equals(object obj)
		{
			GameHandle gameHandle = obj as GameHandle;
			return gameHandle != null && this.HasMatchmaker == gameHandle.HasMatchmaker && (!this.HasMatchmaker || this.Matchmaker.Equals(gameHandle.Matchmaker)) && this.HasGameServer == gameHandle.HasGameServer && (!this.HasGameServer || this.GameServer.Equals(gameHandle.GameServer)) && this.HasGameInstanceId == gameHandle.HasGameInstanceId && (!this.HasGameInstanceId || this.GameInstanceId.Equals(gameHandle.GameInstanceId)) && this.HasMatchmakerGuid == gameHandle.HasMatchmakerGuid && (!this.HasMatchmakerGuid || this.MatchmakerGuid.Equals(gameHandle.MatchmakerGuid)) && this.HasGameServerGuid == gameHandle.HasGameServerGuid && (!this.HasGameServerGuid || this.GameServerGuid.Equals(gameHandle.GameServerGuid));
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06004113 RID: 16659 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x000CF07E File Offset: 0x000CD27E
		public static GameHandle ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameHandle>(bs, 0, -1);
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x000CF088 File Offset: 0x000CD288
		public void Deserialize(Stream stream)
		{
			GameHandle.Deserialize(stream, this);
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x000CF092 File Offset: 0x000CD292
		public static GameHandle Deserialize(Stream stream, GameHandle instance)
		{
			return GameHandle.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x000CF0A0 File Offset: 0x000CD2A0
		public static GameHandle DeserializeLengthDelimited(Stream stream)
		{
			GameHandle gameHandle = new GameHandle();
			GameHandle.DeserializeLengthDelimited(stream, gameHandle);
			return gameHandle;
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x000CF0BC File Offset: 0x000CD2BC
		public static GameHandle DeserializeLengthDelimited(Stream stream, GameHandle instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameHandle.Deserialize(stream, instance, num);
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x000CF0E4 File Offset: 0x000CD2E4
		public static GameHandle Deserialize(Stream stream, GameHandle instance, long limit)
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
						if (num != 10)
						{
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
							if (instance.Matchmaker == null)
							{
								instance.Matchmaker = MatchmakerHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							MatchmakerHandle.DeserializeLengthDelimited(stream, instance.Matchmaker);
							continue;
						}
					}
					else
					{
						if (num == 29)
						{
							instance.GameInstanceId = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 33)
						{
							instance.MatchmakerGuid = binaryReader.ReadUInt64();
							continue;
						}
						if (num == 41)
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

		// Token: 0x0600411A RID: 16666 RVA: 0x000CF20F File Offset: 0x000CD40F
		public void Serialize(Stream stream)
		{
			GameHandle.Serialize(stream, this);
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x000CF218 File Offset: 0x000CD418
		public static void Serialize(Stream stream, GameHandle instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmaker)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Matchmaker.GetSerializedSize());
				MatchmakerHandle.Serialize(stream, instance.Matchmaker);
			}
			if (instance.HasGameServer)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameServer.GetSerializedSize());
				HostProxyPair.Serialize(stream, instance.GameServer);
			}
			if (instance.HasGameInstanceId)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.GameInstanceId);
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(33);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
			if (instance.HasGameServerGuid)
			{
				stream.WriteByte(41);
				binaryWriter.Write(instance.GameServerGuid);
			}
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x000CF2DC File Offset: 0x000CD4DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMatchmaker)
			{
				num += 1U;
				uint serializedSize = this.Matchmaker.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGameServer)
			{
				num += 1U;
				uint serializedSize2 = this.GameServer.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasGameInstanceId)
			{
				num += 1U;
				num += 4U;
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

		// Token: 0x04001689 RID: 5769
		public bool HasMatchmaker;

		// Token: 0x0400168A RID: 5770
		private MatchmakerHandle _Matchmaker;

		// Token: 0x0400168B RID: 5771
		public bool HasGameServer;

		// Token: 0x0400168C RID: 5772
		private HostProxyPair _GameServer;

		// Token: 0x0400168D RID: 5773
		public bool HasGameInstanceId;

		// Token: 0x0400168E RID: 5774
		private uint _GameInstanceId;

		// Token: 0x0400168F RID: 5775
		public bool HasMatchmakerGuid;

		// Token: 0x04001690 RID: 5776
		private ulong _MatchmakerGuid;

		// Token: 0x04001691 RID: 5777
		public bool HasGameServerGuid;

		// Token: 0x04001692 RID: 5778
		private ulong _GameServerGuid;
	}
}
