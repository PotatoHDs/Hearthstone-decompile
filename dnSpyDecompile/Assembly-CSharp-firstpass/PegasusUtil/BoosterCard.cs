using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000042 RID: 66
	public class BoosterCard : IProtoBuf
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00010E0E File Offset: 0x0000F00E
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00010E16 File Offset: 0x0000F016
		public CardDef CardDef { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00010E1F File Offset: 0x0000F01F
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00010E27 File Offset: 0x0000F027
		public Date InsertDate { get; set; }

		// Token: 0x060003FE RID: 1022 RVA: 0x00010E30 File Offset: 0x0000F030
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.CardDef.GetHashCode() ^ this.InsertDate.GetHashCode();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00010E58 File Offset: 0x0000F058
		public override bool Equals(object obj)
		{
			BoosterCard boosterCard = obj as BoosterCard;
			return boosterCard != null && this.CardDef.Equals(boosterCard.CardDef) && this.InsertDate.Equals(boosterCard.InsertDate);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00010E9C File Offset: 0x0000F09C
		public void Deserialize(Stream stream)
		{
			BoosterCard.Deserialize(stream, this);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00010EA6 File Offset: 0x0000F0A6
		public static BoosterCard Deserialize(Stream stream, BoosterCard instance)
		{
			return BoosterCard.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00010EB4 File Offset: 0x0000F0B4
		public static BoosterCard DeserializeLengthDelimited(Stream stream)
		{
			BoosterCard boosterCard = new BoosterCard();
			BoosterCard.DeserializeLengthDelimited(stream, boosterCard);
			return boosterCard;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00010ED0 File Offset: 0x0000F0D0
		public static BoosterCard DeserializeLengthDelimited(Stream stream, BoosterCard instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BoosterCard.Deserialize(stream, instance, num);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00010EF8 File Offset: 0x0000F0F8
		public static BoosterCard Deserialize(Stream stream, BoosterCard instance, long limit)
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
				else if (num != 10)
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
					else if (instance.InsertDate == null)
					{
						instance.InsertDate = Date.DeserializeLengthDelimited(stream);
					}
					else
					{
						Date.DeserializeLengthDelimited(stream, instance.InsertDate);
					}
				}
				else if (instance.CardDef == null)
				{
					instance.CardDef = CardDef.DeserializeLengthDelimited(stream);
				}
				else
				{
					CardDef.DeserializeLengthDelimited(stream, instance.CardDef);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00010FCA File Offset: 0x0000F1CA
		public void Serialize(Stream stream)
		{
			BoosterCard.Serialize(stream, this);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00010FD4 File Offset: 0x0000F1D4
		public static void Serialize(Stream stream, BoosterCard instance)
		{
			if (instance.CardDef == null)
			{
				throw new ArgumentNullException("CardDef", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.CardDef.GetSerializedSize());
			CardDef.Serialize(stream, instance.CardDef);
			if (instance.InsertDate == null)
			{
				throw new ArgumentNullException("InsertDate", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.InsertDate.GetSerializedSize());
			Date.Serialize(stream, instance.InsertDate);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001105C File Offset: 0x0000F25C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.CardDef.GetSerializedSize();
			uint num2 = num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = this.InsertDate.GetSerializedSize();
			return num2 + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 2U;
		}
	}
}
