using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class GetDeckContents : IProtoBuf
	{
		public enum PacketID
		{
			ID = 214,
			System = 0
		}

		private List<long> _DeckId = new List<long>();

		public List<long> DeckId
		{
			get
			{
				return _DeckId;
			}
			set
			{
				_DeckId = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (long item in DeckId)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetDeckContents getDeckContents = obj as GetDeckContents;
			if (getDeckContents == null)
			{
				return false;
			}
			if (DeckId.Count != getDeckContents.DeckId.Count)
			{
				return false;
			}
			for (int i = 0; i < DeckId.Count; i++)
			{
				if (!DeckId[i].Equals(getDeckContents.DeckId[i]))
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

		public static GetDeckContents Deserialize(Stream stream, GetDeckContents instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetDeckContents DeserializeLengthDelimited(Stream stream)
		{
			GetDeckContents getDeckContents = new GetDeckContents();
			DeserializeLengthDelimited(stream, getDeckContents);
			return getDeckContents;
		}

		public static GetDeckContents DeserializeLengthDelimited(Stream stream, GetDeckContents instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetDeckContents Deserialize(Stream stream, GetDeckContents instance, long limit)
		{
			if (instance.DeckId == null)
			{
				instance.DeckId = new List<long>();
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
					instance.DeckId.Add((long)ProtocolParser.ReadUInt64(stream));
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

		public static void Serialize(Stream stream, GetDeckContents instance)
		{
			if (instance.DeckId.Count <= 0)
			{
				return;
			}
			foreach (long item in instance.DeckId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (DeckId.Count > 0)
			{
				foreach (long item in DeckId)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)item);
				}
				return num;
			}
			return num;
		}
	}
}
