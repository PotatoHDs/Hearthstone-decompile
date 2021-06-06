using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x0200019E RID: 414
	public class GameRoundHistoryPlayerEntry : IProtoBuf
	{
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x0005C35F File Offset: 0x0005A55F
		// (set) Token: 0x06001A00 RID: 6656 RVA: 0x0005C367 File Offset: 0x0005A567
		public int PlayerId { get; set; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x0005C370 File Offset: 0x0005A570
		// (set) Token: 0x06001A02 RID: 6658 RVA: 0x0005C378 File Offset: 0x0005A578
		public int PlayerOpponentId { get; set; }

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x0005C381 File Offset: 0x0005A581
		// (set) Token: 0x06001A04 RID: 6660 RVA: 0x0005C389 File Offset: 0x0005A589
		public int PlayerDamageTaken { get; set; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x0005C392 File Offset: 0x0005A592
		// (set) Token: 0x06001A06 RID: 6662 RVA: 0x0005C39A File Offset: 0x0005A59A
		public bool PlayerIsDead { get; set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0005C3A3 File Offset: 0x0005A5A3
		// (set) Token: 0x06001A08 RID: 6664 RVA: 0x0005C3AB File Offset: 0x0005A5AB
		public bool PlayerDiedThisRound { get; set; }

		// Token: 0x06001A09 RID: 6665 RVA: 0x0005C3B4 File Offset: 0x0005A5B4
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.PlayerId.GetHashCode() ^ this.PlayerOpponentId.GetHashCode() ^ this.PlayerDamageTaken.GetHashCode() ^ this.PlayerIsDead.GetHashCode() ^ this.PlayerDiedThisRound.GetHashCode();
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0005C418 File Offset: 0x0005A618
		public override bool Equals(object obj)
		{
			GameRoundHistoryPlayerEntry gameRoundHistoryPlayerEntry = obj as GameRoundHistoryPlayerEntry;
			return gameRoundHistoryPlayerEntry != null && this.PlayerId.Equals(gameRoundHistoryPlayerEntry.PlayerId) && this.PlayerOpponentId.Equals(gameRoundHistoryPlayerEntry.PlayerOpponentId) && this.PlayerDamageTaken.Equals(gameRoundHistoryPlayerEntry.PlayerDamageTaken) && this.PlayerIsDead.Equals(gameRoundHistoryPlayerEntry.PlayerIsDead) && this.PlayerDiedThisRound.Equals(gameRoundHistoryPlayerEntry.PlayerDiedThisRound);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0005C4AA File Offset: 0x0005A6AA
		public void Deserialize(Stream stream)
		{
			GameRoundHistoryPlayerEntry.Deserialize(stream, this);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0005C4B4 File Offset: 0x0005A6B4
		public static GameRoundHistoryPlayerEntry Deserialize(Stream stream, GameRoundHistoryPlayerEntry instance)
		{
			return GameRoundHistoryPlayerEntry.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0005C4C0 File Offset: 0x0005A6C0
		public static GameRoundHistoryPlayerEntry DeserializeLengthDelimited(Stream stream)
		{
			GameRoundHistoryPlayerEntry gameRoundHistoryPlayerEntry = new GameRoundHistoryPlayerEntry();
			GameRoundHistoryPlayerEntry.DeserializeLengthDelimited(stream, gameRoundHistoryPlayerEntry);
			return gameRoundHistoryPlayerEntry;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0005C4DC File Offset: 0x0005A6DC
		public static GameRoundHistoryPlayerEntry DeserializeLengthDelimited(Stream stream, GameRoundHistoryPlayerEntry instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameRoundHistoryPlayerEntry.Deserialize(stream, instance, num);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0005C504 File Offset: 0x0005A704
		public static GameRoundHistoryPlayerEntry Deserialize(Stream stream, GameRoundHistoryPlayerEntry instance, long limit)
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
						if (num == 8)
						{
							instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.PlayerOpponentId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.PlayerDamageTaken = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.PlayerIsDead = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 40)
						{
							instance.PlayerDiedThisRound = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06001A10 RID: 6672 RVA: 0x0005C5ED File Offset: 0x0005A7ED
		public void Serialize(Stream stream)
		{
			GameRoundHistoryPlayerEntry.Serialize(stream, this);
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0005C5F8 File Offset: 0x0005A7F8
		public static void Serialize(Stream stream, GameRoundHistoryPlayerEntry instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PlayerId));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PlayerOpponentId));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PlayerDamageTaken));
			stream.WriteByte(32);
			ProtocolParser.WriteBool(stream, instance.PlayerIsDead);
			stream.WriteByte(40);
			ProtocolParser.WriteBool(stream, instance.PlayerDiedThisRound);
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0005C66B File Offset: 0x0005A86B
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.PlayerId)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.PlayerOpponentId)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.PlayerDamageTaken)) + 1U + 1U + 5U;
		}
	}
}
