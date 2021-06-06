using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	// Token: 0x0200019C RID: 412
	public class GameRoundHistory : IProtoBuf
	{
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x0005BDA5 File Offset: 0x00059FA5
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x0005BDAD File Offset: 0x00059FAD
		public List<GameRoundHistoryEntry> Rounds
		{
			get
			{
				return this._Rounds;
			}
			set
			{
				this._Rounds = value;
			}
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x0005BDB8 File Offset: 0x00059FB8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (GameRoundHistoryEntry gameRoundHistoryEntry in this.Rounds)
			{
				num ^= gameRoundHistoryEntry.GetHashCode();
			}
			return num;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0005BE1C File Offset: 0x0005A01C
		public override bool Equals(object obj)
		{
			GameRoundHistory gameRoundHistory = obj as GameRoundHistory;
			if (gameRoundHistory == null)
			{
				return false;
			}
			if (this.Rounds.Count != gameRoundHistory.Rounds.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Rounds.Count; i++)
			{
				if (!this.Rounds[i].Equals(gameRoundHistory.Rounds[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x0005BE87 File Offset: 0x0005A087
		public void Deserialize(Stream stream)
		{
			GameRoundHistory.Deserialize(stream, this);
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x0005BE91 File Offset: 0x0005A091
		public static GameRoundHistory Deserialize(Stream stream, GameRoundHistory instance)
		{
			return GameRoundHistory.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x0005BE9C File Offset: 0x0005A09C
		public static GameRoundHistory DeserializeLengthDelimited(Stream stream)
		{
			GameRoundHistory gameRoundHistory = new GameRoundHistory();
			GameRoundHistory.DeserializeLengthDelimited(stream, gameRoundHistory);
			return gameRoundHistory;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x0005BEB8 File Offset: 0x0005A0B8
		public static GameRoundHistory DeserializeLengthDelimited(Stream stream, GameRoundHistory instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameRoundHistory.Deserialize(stream, instance, num);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x0005BEE0 File Offset: 0x0005A0E0
		public static GameRoundHistory Deserialize(Stream stream, GameRoundHistory instance, long limit)
		{
			if (instance.Rounds == null)
			{
				instance.Rounds = new List<GameRoundHistoryEntry>();
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
					instance.Rounds.Add(GameRoundHistoryEntry.DeserializeLengthDelimited(stream));
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

		// Token: 0x060019EE RID: 6638 RVA: 0x0005BF78 File Offset: 0x0005A178
		public void Serialize(Stream stream)
		{
			GameRoundHistory.Serialize(stream, this);
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0005BF84 File Offset: 0x0005A184
		public static void Serialize(Stream stream, GameRoundHistory instance)
		{
			if (instance.Rounds.Count > 0)
			{
				foreach (GameRoundHistoryEntry gameRoundHistoryEntry in instance.Rounds)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, gameRoundHistoryEntry.GetSerializedSize());
					GameRoundHistoryEntry.Serialize(stream, gameRoundHistoryEntry);
				}
			}
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0005BFFC File Offset: 0x0005A1FC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Rounds.Count > 0)
			{
				foreach (GameRoundHistoryEntry gameRoundHistoryEntry in this.Rounds)
				{
					num += 1U;
					uint serializedSize = gameRoundHistoryEntry.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040009BC RID: 2492
		private List<GameRoundHistoryEntry> _Rounds = new List<GameRoundHistoryEntry>();

		// Token: 0x0200063B RID: 1595
		public enum PacketID
		{
			// Token: 0x040020EA RID: 8426
			ID = 30
		}
	}
}
