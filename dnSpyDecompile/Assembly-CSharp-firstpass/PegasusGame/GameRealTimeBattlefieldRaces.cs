using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001A1 RID: 417
	public class GameRealTimeBattlefieldRaces : IProtoBuf
	{
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x0005CC62 File Offset: 0x0005AE62
		// (set) Token: 0x06001A37 RID: 6711 RVA: 0x0005CC6A File Offset: 0x0005AE6A
		public int PlayerId { get; set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x0005CC73 File Offset: 0x0005AE73
		// (set) Token: 0x06001A39 RID: 6713 RVA: 0x0005CC7B File Offset: 0x0005AE7B
		public List<GameRealTimeRaceCount> Races
		{
			get
			{
				return this._Races;
			}
			set
			{
				this._Races = value;
			}
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0005CC84 File Offset: 0x0005AE84
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.PlayerId.GetHashCode();
			foreach (GameRealTimeRaceCount gameRealTimeRaceCount in this.Races)
			{
				num ^= gameRealTimeRaceCount.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0005CCF8 File Offset: 0x0005AEF8
		public override bool Equals(object obj)
		{
			GameRealTimeBattlefieldRaces gameRealTimeBattlefieldRaces = obj as GameRealTimeBattlefieldRaces;
			if (gameRealTimeBattlefieldRaces == null)
			{
				return false;
			}
			if (!this.PlayerId.Equals(gameRealTimeBattlefieldRaces.PlayerId))
			{
				return false;
			}
			if (this.Races.Count != gameRealTimeBattlefieldRaces.Races.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Races.Count; i++)
			{
				if (!this.Races[i].Equals(gameRealTimeBattlefieldRaces.Races[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x0005CD7B File Offset: 0x0005AF7B
		public void Deserialize(Stream stream)
		{
			GameRealTimeBattlefieldRaces.Deserialize(stream, this);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0005CD85 File Offset: 0x0005AF85
		public static GameRealTimeBattlefieldRaces Deserialize(Stream stream, GameRealTimeBattlefieldRaces instance)
		{
			return GameRealTimeBattlefieldRaces.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0005CD90 File Offset: 0x0005AF90
		public static GameRealTimeBattlefieldRaces DeserializeLengthDelimited(Stream stream)
		{
			GameRealTimeBattlefieldRaces gameRealTimeBattlefieldRaces = new GameRealTimeBattlefieldRaces();
			GameRealTimeBattlefieldRaces.DeserializeLengthDelimited(stream, gameRealTimeBattlefieldRaces);
			return gameRealTimeBattlefieldRaces;
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x0005CDAC File Offset: 0x0005AFAC
		public static GameRealTimeBattlefieldRaces DeserializeLengthDelimited(Stream stream, GameRealTimeBattlefieldRaces instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameRealTimeBattlefieldRaces.Deserialize(stream, instance, num);
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0005CDD4 File Offset: 0x0005AFD4
		public static GameRealTimeBattlefieldRaces Deserialize(Stream stream, GameRealTimeBattlefieldRaces instance, long limit)
		{
			if (instance.Races == null)
			{
				instance.Races = new List<GameRealTimeRaceCount>();
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
				else if (num != 8)
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
						instance.Races.Add(GameRealTimeRaceCount.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0005CE84 File Offset: 0x0005B084
		public void Serialize(Stream stream)
		{
			GameRealTimeBattlefieldRaces.Serialize(stream, this);
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0005CE90 File Offset: 0x0005B090
		public static void Serialize(Stream stream, GameRealTimeBattlefieldRaces instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PlayerId));
			if (instance.Races.Count > 0)
			{
				foreach (GameRealTimeRaceCount gameRealTimeRaceCount in instance.Races)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, gameRealTimeRaceCount.GetSerializedSize());
					GameRealTimeRaceCount.Serialize(stream, gameRealTimeRaceCount);
				}
			}
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0005CF1C File Offset: 0x0005B11C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PlayerId));
			if (this.Races.Count > 0)
			{
				foreach (GameRealTimeRaceCount gameRealTimeRaceCount in this.Races)
				{
					num += 1U;
					uint serializedSize = gameRealTimeRaceCount.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x040009CA RID: 2506
		private List<GameRealTimeRaceCount> _Races = new List<GameRealTimeRaceCount>();

		// Token: 0x0200063C RID: 1596
		public enum PacketID
		{
			// Token: 0x040020EC RID: 8428
			ID = 31
		}
	}
}
