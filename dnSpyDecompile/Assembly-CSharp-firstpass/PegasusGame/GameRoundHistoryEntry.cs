using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	// Token: 0x0200019D RID: 413
	public class GameRoundHistoryEntry : IProtoBuf
	{
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x0005C083 File Offset: 0x0005A283
		// (set) Token: 0x060019F3 RID: 6643 RVA: 0x0005C08B File Offset: 0x0005A28B
		public List<GameRoundHistoryPlayerEntry> Combats
		{
			get
			{
				return this._Combats;
			}
			set
			{
				this._Combats = value;
			}
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0005C094 File Offset: 0x0005A294
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (GameRoundHistoryPlayerEntry gameRoundHistoryPlayerEntry in this.Combats)
			{
				num ^= gameRoundHistoryPlayerEntry.GetHashCode();
			}
			return num;
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x0005C0F8 File Offset: 0x0005A2F8
		public override bool Equals(object obj)
		{
			GameRoundHistoryEntry gameRoundHistoryEntry = obj as GameRoundHistoryEntry;
			if (gameRoundHistoryEntry == null)
			{
				return false;
			}
			if (this.Combats.Count != gameRoundHistoryEntry.Combats.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Combats.Count; i++)
			{
				if (!this.Combats[i].Equals(gameRoundHistoryEntry.Combats[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0005C163 File Offset: 0x0005A363
		public void Deserialize(Stream stream)
		{
			GameRoundHistoryEntry.Deserialize(stream, this);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0005C16D File Offset: 0x0005A36D
		public static GameRoundHistoryEntry Deserialize(Stream stream, GameRoundHistoryEntry instance)
		{
			return GameRoundHistoryEntry.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0005C178 File Offset: 0x0005A378
		public static GameRoundHistoryEntry DeserializeLengthDelimited(Stream stream)
		{
			GameRoundHistoryEntry gameRoundHistoryEntry = new GameRoundHistoryEntry();
			GameRoundHistoryEntry.DeserializeLengthDelimited(stream, gameRoundHistoryEntry);
			return gameRoundHistoryEntry;
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0005C194 File Offset: 0x0005A394
		public static GameRoundHistoryEntry DeserializeLengthDelimited(Stream stream, GameRoundHistoryEntry instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameRoundHistoryEntry.Deserialize(stream, instance, num);
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x0005C1BC File Offset: 0x0005A3BC
		public static GameRoundHistoryEntry Deserialize(Stream stream, GameRoundHistoryEntry instance, long limit)
		{
			if (instance.Combats == null)
			{
				instance.Combats = new List<GameRoundHistoryPlayerEntry>();
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
					instance.Combats.Add(GameRoundHistoryPlayerEntry.DeserializeLengthDelimited(stream));
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

		// Token: 0x060019FB RID: 6651 RVA: 0x0005C254 File Offset: 0x0005A454
		public void Serialize(Stream stream)
		{
			GameRoundHistoryEntry.Serialize(stream, this);
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x0005C260 File Offset: 0x0005A460
		public static void Serialize(Stream stream, GameRoundHistoryEntry instance)
		{
			if (instance.Combats.Count > 0)
			{
				foreach (GameRoundHistoryPlayerEntry gameRoundHistoryPlayerEntry in instance.Combats)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, gameRoundHistoryPlayerEntry.GetSerializedSize());
					GameRoundHistoryPlayerEntry.Serialize(stream, gameRoundHistoryPlayerEntry);
				}
			}
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0005C2D8 File Offset: 0x0005A4D8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Combats.Count > 0)
			{
				foreach (GameRoundHistoryPlayerEntry gameRoundHistoryPlayerEntry in this.Combats)
				{
					num += 1U;
					uint serializedSize = gameRoundHistoryPlayerEntry.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040009BD RID: 2493
		private List<GameRoundHistoryPlayerEntry> _Combats = new List<GameRoundHistoryPlayerEntry>();
	}
}
