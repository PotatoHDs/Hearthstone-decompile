using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class CardBackModifications : IProtoBuf
	{
		private List<CardBackModification> _CardBackModifications_ = new List<CardBackModification>();

		public List<CardBackModification> CardBackModifications_
		{
			get
			{
				return _CardBackModifications_;
			}
			set
			{
				_CardBackModifications_ = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (CardBackModification item in CardBackModifications_)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CardBackModifications cardBackModifications = obj as CardBackModifications;
			if (cardBackModifications == null)
			{
				return false;
			}
			if (CardBackModifications_.Count != cardBackModifications.CardBackModifications_.Count)
			{
				return false;
			}
			for (int i = 0; i < CardBackModifications_.Count; i++)
			{
				if (!CardBackModifications_[i].Equals(cardBackModifications.CardBackModifications_[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CardBackModifications Deserialize(Stream stream, CardBackModifications instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CardBackModifications DeserializeLengthDelimited(Stream stream)
		{
			CardBackModifications cardBackModifications = new CardBackModifications();
			DeserializeLengthDelimited(stream, cardBackModifications);
			return cardBackModifications;
		}

		public static CardBackModifications DeserializeLengthDelimited(Stream stream, CardBackModifications instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CardBackModifications Deserialize(Stream stream, CardBackModifications instance, long limit)
		{
			if (instance.CardBackModifications_ == null)
			{
				instance.CardBackModifications_ = new List<CardBackModification>();
			}
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
					instance.CardBackModifications_.Add(CardBackModification.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, CardBackModifications instance)
		{
			if (instance.CardBackModifications_.Count <= 0)
			{
				return;
			}
			foreach (CardBackModification item in instance.CardBackModifications_)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				CardBackModification.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (CardBackModifications_.Count > 0)
			{
				foreach (CardBackModification item in CardBackModifications_)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
