using System;
using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeRewardCard : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 3
		}

		public bool HasQuantity;

		private int _Quantity;

		public CardDef Card { get; set; }

		public int Quantity
		{
			get
			{
				return _Quantity;
			}
			set
			{
				_Quantity = value;
				HasQuantity = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Card.GetHashCode();
			if (HasQuantity)
			{
				hashCode ^= Quantity.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeRewardCard profileNoticeRewardCard = obj as ProfileNoticeRewardCard;
			if (profileNoticeRewardCard == null)
			{
				return false;
			}
			if (!Card.Equals(profileNoticeRewardCard.Card))
			{
				return false;
			}
			if (HasQuantity != profileNoticeRewardCard.HasQuantity || (HasQuantity && !Quantity.Equals(profileNoticeRewardCard.Quantity)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeRewardCard Deserialize(Stream stream, ProfileNoticeRewardCard instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeRewardCard DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeRewardCard profileNoticeRewardCard = new ProfileNoticeRewardCard();
			DeserializeLengthDelimited(stream, profileNoticeRewardCard);
			return profileNoticeRewardCard;
		}

		public static ProfileNoticeRewardCard DeserializeLengthDelimited(Stream stream, ProfileNoticeRewardCard instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeRewardCard Deserialize(Stream stream, ProfileNoticeRewardCard instance, long limit)
		{
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					if (instance.Card == null)
					{
						instance.Card = CardDef.DeserializeLengthDelimited(stream);
					}
					else
					{
						CardDef.DeserializeLengthDelimited(stream, instance.Card);
					}
					continue;
				case 16:
					instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Quantity);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Card.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasQuantity)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Quantity);
			}
			return num + 1;
		}
	}
}
