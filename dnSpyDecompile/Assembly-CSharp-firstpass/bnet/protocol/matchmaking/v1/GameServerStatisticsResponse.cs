using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003D1 RID: 977
	public class GameServerStatisticsResponse : IProtoBuf
	{
		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x000CC604 File Offset: 0x000CA804
		// (set) Token: 0x06004007 RID: 16391 RVA: 0x000CC60C File Offset: 0x000CA80C
		public int NumGameServers
		{
			get
			{
				return this._NumGameServers;
			}
			set
			{
				this._NumGameServers = value;
				this.HasNumGameServers = true;
			}
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x000CC61C File Offset: 0x000CA81C
		public void SetNumGameServers(int val)
		{
			this.NumGameServers = val;
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06004009 RID: 16393 RVA: 0x000CC625 File Offset: 0x000CA825
		// (set) Token: 0x0600400A RID: 16394 RVA: 0x000CC62D File Offset: 0x000CA82D
		public int NumAvailableSlots
		{
			get
			{
				return this._NumAvailableSlots;
			}
			set
			{
				this._NumAvailableSlots = value;
				this.HasNumAvailableSlots = true;
			}
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x000CC63D File Offset: 0x000CA83D
		public void SetNumAvailableSlots(int val)
		{
			this.NumAvailableSlots = val;
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x000CC646 File Offset: 0x000CA846
		// (set) Token: 0x0600400D RID: 16397 RVA: 0x000CC64E File Offset: 0x000CA84E
		public int NumTotalSlots
		{
			get
			{
				return this._NumTotalSlots;
			}
			set
			{
				this._NumTotalSlots = value;
				this.HasNumTotalSlots = true;
			}
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x000CC65E File Offset: 0x000CA85E
		public void SetNumTotalSlots(int val)
		{
			this.NumTotalSlots = val;
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x000CC668 File Offset: 0x000CA868
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasNumGameServers)
			{
				num ^= this.NumGameServers.GetHashCode();
			}
			if (this.HasNumAvailableSlots)
			{
				num ^= this.NumAvailableSlots.GetHashCode();
			}
			if (this.HasNumTotalSlots)
			{
				num ^= this.NumTotalSlots.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x000CC6D0 File Offset: 0x000CA8D0
		public override bool Equals(object obj)
		{
			GameServerStatisticsResponse gameServerStatisticsResponse = obj as GameServerStatisticsResponse;
			return gameServerStatisticsResponse != null && this.HasNumGameServers == gameServerStatisticsResponse.HasNumGameServers && (!this.HasNumGameServers || this.NumGameServers.Equals(gameServerStatisticsResponse.NumGameServers)) && this.HasNumAvailableSlots == gameServerStatisticsResponse.HasNumAvailableSlots && (!this.HasNumAvailableSlots || this.NumAvailableSlots.Equals(gameServerStatisticsResponse.NumAvailableSlots)) && this.HasNumTotalSlots == gameServerStatisticsResponse.HasNumTotalSlots && (!this.HasNumTotalSlots || this.NumTotalSlots.Equals(gameServerStatisticsResponse.NumTotalSlots));
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06004011 RID: 16401 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x000CC774 File Offset: 0x000CA974
		public static GameServerStatisticsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameServerStatisticsResponse>(bs, 0, -1);
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x000CC77E File Offset: 0x000CA97E
		public void Deserialize(Stream stream)
		{
			GameServerStatisticsResponse.Deserialize(stream, this);
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x000CC788 File Offset: 0x000CA988
		public static GameServerStatisticsResponse Deserialize(Stream stream, GameServerStatisticsResponse instance)
		{
			return GameServerStatisticsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x000CC794 File Offset: 0x000CA994
		public static GameServerStatisticsResponse DeserializeLengthDelimited(Stream stream)
		{
			GameServerStatisticsResponse gameServerStatisticsResponse = new GameServerStatisticsResponse();
			GameServerStatisticsResponse.DeserializeLengthDelimited(stream, gameServerStatisticsResponse);
			return gameServerStatisticsResponse;
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x000CC7B0 File Offset: 0x000CA9B0
		public static GameServerStatisticsResponse DeserializeLengthDelimited(Stream stream, GameServerStatisticsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameServerStatisticsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x000CC7D8 File Offset: 0x000CA9D8
		public static GameServerStatisticsResponse Deserialize(Stream stream, GameServerStatisticsResponse instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
					{
						if (num != 24)
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
							instance.NumTotalSlots = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.NumAvailableSlots = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.NumGameServers = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x000CC888 File Offset: 0x000CAA88
		public void Serialize(Stream stream)
		{
			GameServerStatisticsResponse.Serialize(stream, this);
		}

		// Token: 0x06004019 RID: 16409 RVA: 0x000CC894 File Offset: 0x000CAA94
		public static void Serialize(Stream stream, GameServerStatisticsResponse instance)
		{
			if (instance.HasNumGameServers)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NumGameServers));
			}
			if (instance.HasNumAvailableSlots)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NumAvailableSlots));
			}
			if (instance.HasNumTotalSlots)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NumTotalSlots));
			}
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x000CC8F8 File Offset: 0x000CAAF8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasNumGameServers)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NumGameServers));
			}
			if (this.HasNumAvailableSlots)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NumAvailableSlots));
			}
			if (this.HasNumTotalSlots)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NumTotalSlots));
			}
			return num;
		}

		// Token: 0x0400165F RID: 5727
		public bool HasNumGameServers;

		// Token: 0x04001660 RID: 5728
		private int _NumGameServers;

		// Token: 0x04001661 RID: 5729
		public bool HasNumAvailableSlots;

		// Token: 0x04001662 RID: 5730
		private int _NumAvailableSlots;

		// Token: 0x04001663 RID: 5731
		public bool HasNumTotalSlots;

		// Token: 0x04001664 RID: 5732
		private int _NumTotalSlots;
	}
}
