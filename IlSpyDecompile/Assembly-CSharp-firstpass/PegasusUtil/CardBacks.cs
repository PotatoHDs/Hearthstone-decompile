using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class CardBacks : IProtoBuf
	{
		public enum PacketID
		{
			ID = 236,
			System = 0
		}

		private List<int> _CardBacks_ = new List<int>();

		public int FavoriteCardBack { get; set; }

		public List<int> CardBacks_
		{
			get
			{
				return _CardBacks_;
			}
			set
			{
				_CardBacks_ = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= FavoriteCardBack.GetHashCode();
			foreach (int item in CardBacks_)
			{
				hashCode ^= item.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			CardBacks cardBacks = obj as CardBacks;
			if (cardBacks == null)
			{
				return false;
			}
			if (!FavoriteCardBack.Equals(cardBacks.FavoriteCardBack))
			{
				return false;
			}
			if (CardBacks_.Count != cardBacks.CardBacks_.Count)
			{
				return false;
			}
			for (int i = 0; i < CardBacks_.Count; i++)
			{
				if (!CardBacks_[i].Equals(cardBacks.CardBacks_[i]))
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

		public static CardBacks Deserialize(Stream stream, CardBacks instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CardBacks DeserializeLengthDelimited(Stream stream)
		{
			CardBacks cardBacks = new CardBacks();
			DeserializeLengthDelimited(stream, cardBacks);
			return cardBacks;
		}

		public static CardBacks DeserializeLengthDelimited(Stream stream, CardBacks instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CardBacks Deserialize(Stream stream, CardBacks instance, long limit)
		{
			if (instance.CardBacks_ == null)
			{
				instance.CardBacks_ = new List<int>();
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
				case 8:
					instance.FavoriteCardBack = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.CardBacks_.Add((int)ProtocolParser.ReadUInt64(stream));
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

		public static void Serialize(Stream stream, CardBacks instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FavoriteCardBack);
			if (instance.CardBacks_.Count <= 0)
			{
				return;
			}
			foreach (int item in instance.CardBacks_)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)FavoriteCardBack);
			if (CardBacks_.Count > 0)
			{
				foreach (int item in CardBacks_)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)item);
				}
			}
			return num + 1;
		}
	}
}
