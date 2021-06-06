using System;
using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	// Token: 0x02000025 RID: 37
	public class FormatDeckValidity : IProtoBuf
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000802E File Offset: 0x0000622E
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00008036 File Offset: 0x00006236
		public FormatType FormatType { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000803F File Offset: 0x0000623F
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00008047 File Offset: 0x00006247
		public bool ValidDeck { get; set; }

		// Token: 0x060001C4 RID: 452 RVA: 0x00008050 File Offset: 0x00006250
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.FormatType.GetHashCode() ^ this.ValidDeck.GetHashCode();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000808C File Offset: 0x0000628C
		public override bool Equals(object obj)
		{
			FormatDeckValidity formatDeckValidity = obj as FormatDeckValidity;
			return formatDeckValidity != null && this.FormatType.Equals(formatDeckValidity.FormatType) && this.ValidDeck.Equals(formatDeckValidity.ValidDeck);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000080E1 File Offset: 0x000062E1
		public void Deserialize(Stream stream)
		{
			FormatDeckValidity.Deserialize(stream, this);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000080EB File Offset: 0x000062EB
		public static FormatDeckValidity Deserialize(Stream stream, FormatDeckValidity instance)
		{
			return FormatDeckValidity.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000080F8 File Offset: 0x000062F8
		public static FormatDeckValidity DeserializeLengthDelimited(Stream stream)
		{
			FormatDeckValidity formatDeckValidity = new FormatDeckValidity();
			FormatDeckValidity.DeserializeLengthDelimited(stream, formatDeckValidity);
			return formatDeckValidity;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008114 File Offset: 0x00006314
		public static FormatDeckValidity DeserializeLengthDelimited(Stream stream, FormatDeckValidity instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FormatDeckValidity.Deserialize(stream, instance, num);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000813C File Offset: 0x0000633C
		public static FormatDeckValidity Deserialize(Stream stream, FormatDeckValidity instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.ValidDeck = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000081D4 File Offset: 0x000063D4
		public void Serialize(Stream stream)
		{
			FormatDeckValidity.Serialize(stream, this);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000081DD File Offset: 0x000063DD
		public static void Serialize(Stream stream, FormatDeckValidity instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.ValidDeck);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008207 File Offset: 0x00006407
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType)) + 1U + 2U;
		}
	}
}
