using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace HSCachedDeckCompletion
{
	public class HSCachedDeckCompletionResponse : IProtoBuf
	{
		public bool HasPlayerId;

		private long _PlayerId;

		private List<DeckCardData> _PlayerDeckCard = new List<DeckCardData>();

		private List<DeckCardData> _IdealDeckCard = new List<DeckCardData>();

		public bool HasDeckId;

		private long _DeckId;

		public long PlayerId
		{
			get
			{
				return _PlayerId;
			}
			set
			{
				_PlayerId = value;
				HasPlayerId = true;
			}
		}

		public List<DeckCardData> PlayerDeckCard
		{
			get
			{
				return _PlayerDeckCard;
			}
			set
			{
				_PlayerDeckCard = value;
			}
		}

		public List<DeckCardData> IdealDeckCard
		{
			get
			{
				return _IdealDeckCard;
			}
			set
			{
				_IdealDeckCard = value;
			}
		}

		public long DeckId
		{
			get
			{
				return _DeckId;
			}
			set
			{
				_DeckId = value;
				HasDeckId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayerId)
			{
				num ^= PlayerId.GetHashCode();
			}
			foreach (DeckCardData item in PlayerDeckCard)
			{
				num ^= item.GetHashCode();
			}
			foreach (DeckCardData item2 in IdealDeckCard)
			{
				num ^= item2.GetHashCode();
			}
			if (HasDeckId)
			{
				num ^= DeckId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			HSCachedDeckCompletionResponse hSCachedDeckCompletionResponse = obj as HSCachedDeckCompletionResponse;
			if (hSCachedDeckCompletionResponse == null)
			{
				return false;
			}
			if (HasPlayerId != hSCachedDeckCompletionResponse.HasPlayerId || (HasPlayerId && !PlayerId.Equals(hSCachedDeckCompletionResponse.PlayerId)))
			{
				return false;
			}
			if (PlayerDeckCard.Count != hSCachedDeckCompletionResponse.PlayerDeckCard.Count)
			{
				return false;
			}
			for (int i = 0; i < PlayerDeckCard.Count; i++)
			{
				if (!PlayerDeckCard[i].Equals(hSCachedDeckCompletionResponse.PlayerDeckCard[i]))
				{
					return false;
				}
			}
			if (IdealDeckCard.Count != hSCachedDeckCompletionResponse.IdealDeckCard.Count)
			{
				return false;
			}
			for (int j = 0; j < IdealDeckCard.Count; j++)
			{
				if (!IdealDeckCard[j].Equals(hSCachedDeckCompletionResponse.IdealDeckCard[j]))
				{
					return false;
				}
			}
			if (HasDeckId != hSCachedDeckCompletionResponse.HasDeckId || (HasDeckId && !DeckId.Equals(hSCachedDeckCompletionResponse.DeckId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static HSCachedDeckCompletionResponse Deserialize(Stream stream, HSCachedDeckCompletionResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static HSCachedDeckCompletionResponse DeserializeLengthDelimited(Stream stream)
		{
			HSCachedDeckCompletionResponse hSCachedDeckCompletionResponse = new HSCachedDeckCompletionResponse();
			DeserializeLengthDelimited(stream, hSCachedDeckCompletionResponse);
			return hSCachedDeckCompletionResponse;
		}

		public static HSCachedDeckCompletionResponse DeserializeLengthDelimited(Stream stream, HSCachedDeckCompletionResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static HSCachedDeckCompletionResponse Deserialize(Stream stream, HSCachedDeckCompletionResponse instance, long limit)
		{
			if (instance.PlayerDeckCard == null)
			{
				instance.PlayerDeckCard = new List<DeckCardData>();
			}
			if (instance.IdealDeckCard == null)
			{
				instance.IdealDeckCard = new List<DeckCardData>();
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
					instance.PlayerId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.PlayerDeckCard.Add(DeckCardData.DeserializeLengthDelimited(stream));
					continue;
				case 26:
					instance.IdealDeckCard.Add(DeckCardData.DeserializeLengthDelimited(stream));
					continue;
				case 32:
					instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, HSCachedDeckCompletionResponse instance)
		{
			if (instance.HasPlayerId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			}
			if (instance.PlayerDeckCard.Count > 0)
			{
				foreach (DeckCardData item in instance.PlayerDeckCard)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					DeckCardData.Serialize(stream, item);
				}
			}
			if (instance.IdealDeckCard.Count > 0)
			{
				foreach (DeckCardData item2 in instance.IdealDeckCard)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
					DeckCardData.Serialize(stream, item2);
				}
			}
			if (instance.HasDeckId)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPlayerId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PlayerId);
			}
			if (PlayerDeckCard.Count > 0)
			{
				foreach (DeckCardData item in PlayerDeckCard)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (IdealDeckCard.Count > 0)
			{
				foreach (DeckCardData item2 in IdealDeckCard)
				{
					num++;
					uint serializedSize2 = item2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasDeckId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeckId);
			}
			return num;
		}
	}
}
