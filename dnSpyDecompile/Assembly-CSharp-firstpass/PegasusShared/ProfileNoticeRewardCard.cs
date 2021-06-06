using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000129 RID: 297
	public class ProfileNoticeRewardCard : IProtoBuf
	{
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x00044318 File Offset: 0x00042518
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x00044320 File Offset: 0x00042520
		public CardDef Card { get; set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x00044329 File Offset: 0x00042529
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x00044331 File Offset: 0x00042531
		public int Quantity
		{
			get
			{
				return this._Quantity;
			}
			set
			{
				this._Quantity = value;
				this.HasQuantity = true;
			}
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00044344 File Offset: 0x00042544
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Card.GetHashCode();
			if (this.HasQuantity)
			{
				num ^= this.Quantity.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00044388 File Offset: 0x00042588
		public override bool Equals(object obj)
		{
			ProfileNoticeRewardCard profileNoticeRewardCard = obj as ProfileNoticeRewardCard;
			return profileNoticeRewardCard != null && this.Card.Equals(profileNoticeRewardCard.Card) && this.HasQuantity == profileNoticeRewardCard.HasQuantity && (!this.HasQuantity || this.Quantity.Equals(profileNoticeRewardCard.Quantity));
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x000443E5 File Offset: 0x000425E5
		public void Deserialize(Stream stream)
		{
			ProfileNoticeRewardCard.Deserialize(stream, this);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x000443EF File Offset: 0x000425EF
		public static ProfileNoticeRewardCard Deserialize(Stream stream, ProfileNoticeRewardCard instance)
		{
			return ProfileNoticeRewardCard.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000443FC File Offset: 0x000425FC
		public static ProfileNoticeRewardCard DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeRewardCard profileNoticeRewardCard = new ProfileNoticeRewardCard();
			ProfileNoticeRewardCard.DeserializeLengthDelimited(stream, profileNoticeRewardCard);
			return profileNoticeRewardCard;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00044418 File Offset: 0x00042618
		public static ProfileNoticeRewardCard DeserializeLengthDelimited(Stream stream, ProfileNoticeRewardCard instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeRewardCard.Deserialize(stream, instance, num);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00044440 File Offset: 0x00042640
		public static ProfileNoticeRewardCard Deserialize(Stream stream, ProfileNoticeRewardCard instance, long limit)
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
						instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.Card == null)
				{
					instance.Card = CardDef.DeserializeLengthDelimited(stream);
				}
				else
				{
					CardDef.DeserializeLengthDelimited(stream, instance.Card);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x000444F3 File Offset: 0x000426F3
		public void Serialize(Stream stream)
		{
			ProfileNoticeRewardCard.Serialize(stream, this);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x000444FC File Offset: 0x000426FC
		public static void Serialize(Stream stream, ProfileNoticeRewardCard instance)
		{
			if (instance.Card == null)
			{
				throw new ArgumentNullException("Card", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Card.GetSerializedSize());
			CardDef.Serialize(stream, instance.Card);
			if (instance.HasQuantity)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Quantity));
			}
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00044564 File Offset: 0x00042764
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Card.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasQuantity)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Quantity));
			}
			return num + 1U;
		}

		// Token: 0x04000614 RID: 1556
		public bool HasQuantity;

		// Token: 0x04000615 RID: 1557
		private int _Quantity;

		// Token: 0x0200061B RID: 1563
		public enum NoticeID
		{
			// Token: 0x04002089 RID: 8329
			ID = 3
		}
	}
}
