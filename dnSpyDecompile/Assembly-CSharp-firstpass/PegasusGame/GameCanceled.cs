using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001CD RID: 461
	public class GameCanceled : IProtoBuf
	{
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x00067B44 File Offset: 0x00065D44
		// (set) Token: 0x06001D6B RID: 7531 RVA: 0x00067B4C File Offset: 0x00065D4C
		public GameCanceled.Reason Reason_ { get; set; }

		// Token: 0x06001D6C RID: 7532 RVA: 0x00067B58 File Offset: 0x00065D58
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Reason_.GetHashCode();
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x00067B88 File Offset: 0x00065D88
		public override bool Equals(object obj)
		{
			GameCanceled gameCanceled = obj as GameCanceled;
			return gameCanceled != null && this.Reason_.Equals(gameCanceled.Reason_);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00067BC5 File Offset: 0x00065DC5
		public void Deserialize(Stream stream)
		{
			GameCanceled.Deserialize(stream, this);
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x00067BCF File Offset: 0x00065DCF
		public static GameCanceled Deserialize(Stream stream, GameCanceled instance)
		{
			return GameCanceled.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x00067BDC File Offset: 0x00065DDC
		public static GameCanceled DeserializeLengthDelimited(Stream stream)
		{
			GameCanceled gameCanceled = new GameCanceled();
			GameCanceled.DeserializeLengthDelimited(stream, gameCanceled);
			return gameCanceled;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x00067BF8 File Offset: 0x00065DF8
		public static GameCanceled DeserializeLengthDelimited(Stream stream, GameCanceled instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameCanceled.Deserialize(stream, instance, num);
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x00067C20 File Offset: 0x00065E20
		public static GameCanceled Deserialize(Stream stream, GameCanceled instance, long limit)
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
				else if (num == 8)
				{
					instance.Reason_ = (GameCanceled.Reason)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001D73 RID: 7539 RVA: 0x00067CA0 File Offset: 0x00065EA0
		public void Serialize(Stream stream)
		{
			GameCanceled.Serialize(stream, this);
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00067CA9 File Offset: 0x00065EA9
		public static void Serialize(Stream stream, GameCanceled instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Reason_));
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00067CBF File Offset: 0x00065EBF
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Reason_)) + 1U;
		}

		// Token: 0x02000656 RID: 1622
		public enum Reason
		{
			// Token: 0x04002141 RID: 8513
			OPPONENT_TIMEOUT = 1,
			// Token: 0x04002142 RID: 8514
			PLAYER_LOADING_TIMEOUT,
			// Token: 0x04002143 RID: 8515
			PLAYER_LOADING_DISCONNECTED
		}

		// Token: 0x02000657 RID: 1623
		public enum PacketID
		{
			// Token: 0x04002145 RID: 8517
			ID = 12
		}
	}
}
