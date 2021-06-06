using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001CC RID: 460
	public class TurnTimer : IProtoBuf
	{
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x000678F3 File Offset: 0x00065AF3
		// (set) Token: 0x06001D5A RID: 7514 RVA: 0x000678FB File Offset: 0x00065AFB
		public int Seconds { get; set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x00067904 File Offset: 0x00065B04
		// (set) Token: 0x06001D5C RID: 7516 RVA: 0x0006790C File Offset: 0x00065B0C
		public int Turn { get; set; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x00067915 File Offset: 0x00065B15
		// (set) Token: 0x06001D5E RID: 7518 RVA: 0x0006791D File Offset: 0x00065B1D
		public bool Show { get; set; }

		// Token: 0x06001D5F RID: 7519 RVA: 0x00067928 File Offset: 0x00065B28
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Seconds.GetHashCode() ^ this.Turn.GetHashCode() ^ this.Show.GetHashCode();
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x00067970 File Offset: 0x00065B70
		public override bool Equals(object obj)
		{
			TurnTimer turnTimer = obj as TurnTimer;
			return turnTimer != null && this.Seconds.Equals(turnTimer.Seconds) && this.Turn.Equals(turnTimer.Turn) && this.Show.Equals(turnTimer.Show);
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x000679D2 File Offset: 0x00065BD2
		public void Deserialize(Stream stream)
		{
			TurnTimer.Deserialize(stream, this);
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x000679DC File Offset: 0x00065BDC
		public static TurnTimer Deserialize(Stream stream, TurnTimer instance)
		{
			return TurnTimer.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x000679E8 File Offset: 0x00065BE8
		public static TurnTimer DeserializeLengthDelimited(Stream stream)
		{
			TurnTimer turnTimer = new TurnTimer();
			TurnTimer.DeserializeLengthDelimited(stream, turnTimer);
			return turnTimer;
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x00067A04 File Offset: 0x00065C04
		public static TurnTimer DeserializeLengthDelimited(Stream stream, TurnTimer instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TurnTimer.Deserialize(stream, instance, num);
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x00067A2C File Offset: 0x00065C2C
		public static TurnTimer Deserialize(Stream stream, TurnTimer instance, long limit)
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
							instance.Show = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.Turn = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Seconds = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x00067ADB File Offset: 0x00065CDB
		public void Serialize(Stream stream)
		{
			TurnTimer.Serialize(stream, this);
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x00067AE4 File Offset: 0x00065CE4
		public static void Serialize(Stream stream, TurnTimer instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Seconds));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Turn));
			stream.WriteByte(24);
			ProtocolParser.WriteBool(stream, instance.Show);
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x00067B23 File Offset: 0x00065D23
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Seconds)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Turn)) + 1U + 3U;
		}

		// Token: 0x02000655 RID: 1621
		public enum PacketID
		{
			// Token: 0x0400213F RID: 8511
			ID = 9
		}
	}
}
