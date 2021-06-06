using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001C8 RID: 456
	public class PowerHistoryCachedTagForDormantChange : IProtoBuf
	{
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0006639A File Offset: 0x0006459A
		// (set) Token: 0x06001D06 RID: 7430 RVA: 0x000663A2 File Offset: 0x000645A2
		public int Entity { get; set; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x000663AB File Offset: 0x000645AB
		// (set) Token: 0x06001D08 RID: 7432 RVA: 0x000663B3 File Offset: 0x000645B3
		public int Tag { get; set; }

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x000663BC File Offset: 0x000645BC
		// (set) Token: 0x06001D0A RID: 7434 RVA: 0x000663C4 File Offset: 0x000645C4
		public int Value { get; set; }

		// Token: 0x06001D0B RID: 7435 RVA: 0x000663D0 File Offset: 0x000645D0
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Entity.GetHashCode() ^ this.Tag.GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x00066418 File Offset: 0x00064618
		public override bool Equals(object obj)
		{
			PowerHistoryCachedTagForDormantChange powerHistoryCachedTagForDormantChange = obj as PowerHistoryCachedTagForDormantChange;
			return powerHistoryCachedTagForDormantChange != null && this.Entity.Equals(powerHistoryCachedTagForDormantChange.Entity) && this.Tag.Equals(powerHistoryCachedTagForDormantChange.Tag) && this.Value.Equals(powerHistoryCachedTagForDormantChange.Value);
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0006647A File Offset: 0x0006467A
		public void Deserialize(Stream stream)
		{
			PowerHistoryCachedTagForDormantChange.Deserialize(stream, this);
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x00066484 File Offset: 0x00064684
		public static PowerHistoryCachedTagForDormantChange Deserialize(Stream stream, PowerHistoryCachedTagForDormantChange instance)
		{
			return PowerHistoryCachedTagForDormantChange.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x00066490 File Offset: 0x00064690
		public static PowerHistoryCachedTagForDormantChange DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryCachedTagForDormantChange powerHistoryCachedTagForDormantChange = new PowerHistoryCachedTagForDormantChange();
			PowerHistoryCachedTagForDormantChange.DeserializeLengthDelimited(stream, powerHistoryCachedTagForDormantChange);
			return powerHistoryCachedTagForDormantChange;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000664AC File Offset: 0x000646AC
		public static PowerHistoryCachedTagForDormantChange DeserializeLengthDelimited(Stream stream, PowerHistoryCachedTagForDormantChange instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistoryCachedTagForDormantChange.Deserialize(stream, instance, num);
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000664D4 File Offset: 0x000646D4
		public static PowerHistoryCachedTagForDormantChange Deserialize(Stream stream, PowerHistoryCachedTagForDormantChange instance, long limit)
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
							instance.Value = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.Tag = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Entity = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x00066584 File Offset: 0x00064784
		public void Serialize(Stream stream)
		{
			PowerHistoryCachedTagForDormantChange.Serialize(stream, this);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0006658D File Offset: 0x0006478D
		public static void Serialize(Stream stream, PowerHistoryCachedTagForDormantChange instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Entity));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Tag));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Value));
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x000665CD File Offset: 0x000647CD
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Entity)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Tag)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Value)) + 3U;
		}
	}
}
