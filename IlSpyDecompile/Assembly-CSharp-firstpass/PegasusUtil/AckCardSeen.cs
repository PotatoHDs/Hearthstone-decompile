using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class AckCardSeen : IProtoBuf
	{
		public enum PacketID
		{
			ID = 223,
			System = 0
		}

		private List<CardDef> _CardDefs = new List<CardDef>();

		public List<CardDef> CardDefs
		{
			get
			{
				return _CardDefs;
			}
			set
			{
				_CardDefs = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (CardDef cardDef in CardDefs)
			{
				num ^= cardDef.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AckCardSeen ackCardSeen = obj as AckCardSeen;
			if (ackCardSeen == null)
			{
				return false;
			}
			if (CardDefs.Count != ackCardSeen.CardDefs.Count)
			{
				return false;
			}
			for (int i = 0; i < CardDefs.Count; i++)
			{
				if (!CardDefs[i].Equals(ackCardSeen.CardDefs[i]))
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

		public static AckCardSeen Deserialize(Stream stream, AckCardSeen instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AckCardSeen DeserializeLengthDelimited(Stream stream)
		{
			AckCardSeen ackCardSeen = new AckCardSeen();
			DeserializeLengthDelimited(stream, ackCardSeen);
			return ackCardSeen;
		}

		public static AckCardSeen DeserializeLengthDelimited(Stream stream, AckCardSeen instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AckCardSeen Deserialize(Stream stream, AckCardSeen instance, long limit)
		{
			if (instance.CardDefs == null)
			{
				instance.CardDefs = new List<CardDef>();
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
					instance.CardDefs.Add(CardDef.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, AckCardSeen instance)
		{
			if (instance.CardDefs.Count <= 0)
			{
				return;
			}
			foreach (CardDef cardDef in instance.CardDefs)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, cardDef.GetSerializedSize());
				CardDef.Serialize(stream, cardDef);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (CardDefs.Count > 0)
			{
				foreach (CardDef cardDef in CardDefs)
				{
					num++;
					uint serializedSize = cardDef.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
