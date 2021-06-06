using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace HSCachedDeckCompletion
{
	public class HSCachedDeckCompletionRequest : IProtoBuf
	{
		public bool HasPlayerId;

		private long _PlayerId;

		public bool HasBnetAccountId;

		private long _BnetAccountId;

		public bool HasHeroClass;

		private int _HeroClass;

		private List<SmartDeckCardData> _InsertedCard = new List<SmartDeckCardData>();

		public bool HasDeckId;

		private long _DeckId;

		public bool HasFormatType;

		private FormatType _FormatType;

		public bool HasVersion;

		private string _Version;

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

		public long BnetAccountId
		{
			get
			{
				return _BnetAccountId;
			}
			set
			{
				_BnetAccountId = value;
				HasBnetAccountId = true;
			}
		}

		public int HeroClass
		{
			get
			{
				return _HeroClass;
			}
			set
			{
				_HeroClass = value;
				HasHeroClass = true;
			}
		}

		public List<SmartDeckCardData> InsertedCard
		{
			get
			{
				return _InsertedCard;
			}
			set
			{
				_InsertedCard = value;
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

		public FormatType FormatType
		{
			get
			{
				return _FormatType;
			}
			set
			{
				_FormatType = value;
				HasFormatType = true;
			}
		}

		public string Version
		{
			get
			{
				return _Version;
			}
			set
			{
				_Version = value;
				HasVersion = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayerId)
			{
				num ^= PlayerId.GetHashCode();
			}
			if (HasBnetAccountId)
			{
				num ^= BnetAccountId.GetHashCode();
			}
			if (HasHeroClass)
			{
				num ^= HeroClass.GetHashCode();
			}
			foreach (SmartDeckCardData item in InsertedCard)
			{
				num ^= item.GetHashCode();
			}
			if (HasDeckId)
			{
				num ^= DeckId.GetHashCode();
			}
			if (HasFormatType)
			{
				num ^= FormatType.GetHashCode();
			}
			if (HasVersion)
			{
				num ^= Version.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			HSCachedDeckCompletionRequest hSCachedDeckCompletionRequest = obj as HSCachedDeckCompletionRequest;
			if (hSCachedDeckCompletionRequest == null)
			{
				return false;
			}
			if (HasPlayerId != hSCachedDeckCompletionRequest.HasPlayerId || (HasPlayerId && !PlayerId.Equals(hSCachedDeckCompletionRequest.PlayerId)))
			{
				return false;
			}
			if (HasBnetAccountId != hSCachedDeckCompletionRequest.HasBnetAccountId || (HasBnetAccountId && !BnetAccountId.Equals(hSCachedDeckCompletionRequest.BnetAccountId)))
			{
				return false;
			}
			if (HasHeroClass != hSCachedDeckCompletionRequest.HasHeroClass || (HasHeroClass && !HeroClass.Equals(hSCachedDeckCompletionRequest.HeroClass)))
			{
				return false;
			}
			if (InsertedCard.Count != hSCachedDeckCompletionRequest.InsertedCard.Count)
			{
				return false;
			}
			for (int i = 0; i < InsertedCard.Count; i++)
			{
				if (!InsertedCard[i].Equals(hSCachedDeckCompletionRequest.InsertedCard[i]))
				{
					return false;
				}
			}
			if (HasDeckId != hSCachedDeckCompletionRequest.HasDeckId || (HasDeckId && !DeckId.Equals(hSCachedDeckCompletionRequest.DeckId)))
			{
				return false;
			}
			if (HasFormatType != hSCachedDeckCompletionRequest.HasFormatType || (HasFormatType && !FormatType.Equals(hSCachedDeckCompletionRequest.FormatType)))
			{
				return false;
			}
			if (HasVersion != hSCachedDeckCompletionRequest.HasVersion || (HasVersion && !Version.Equals(hSCachedDeckCompletionRequest.Version)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static HSCachedDeckCompletionRequest Deserialize(Stream stream, HSCachedDeckCompletionRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static HSCachedDeckCompletionRequest DeserializeLengthDelimited(Stream stream)
		{
			HSCachedDeckCompletionRequest hSCachedDeckCompletionRequest = new HSCachedDeckCompletionRequest();
			DeserializeLengthDelimited(stream, hSCachedDeckCompletionRequest);
			return hSCachedDeckCompletionRequest;
		}

		public static HSCachedDeckCompletionRequest DeserializeLengthDelimited(Stream stream, HSCachedDeckCompletionRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static HSCachedDeckCompletionRequest Deserialize(Stream stream, HSCachedDeckCompletionRequest instance, long limit)
		{
			if (instance.InsertedCard == null)
			{
				instance.InsertedCard = new List<SmartDeckCardData>();
			}
			instance.FormatType = FormatType.FT_UNKNOWN;
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
				case 16:
					instance.BnetAccountId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.HeroClass = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.InsertedCard.Add(SmartDeckCardData.DeserializeLengthDelimited(stream));
					continue;
				case 40:
					instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 58:
					instance.Version = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, HSCachedDeckCompletionRequest instance)
		{
			if (instance.HasPlayerId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			}
			if (instance.HasBnetAccountId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BnetAccountId);
			}
			if (instance.HasHeroClass)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.HeroClass);
			}
			if (instance.InsertedCard.Count > 0)
			{
				foreach (SmartDeckCardData item in instance.InsertedCard)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					SmartDeckCardData.Serialize(stream, item);
				}
			}
			if (instance.HasDeckId)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			}
			if (instance.HasVersion)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Version));
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
			if (HasBnetAccountId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BnetAccountId);
			}
			if (HasHeroClass)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)HeroClass);
			}
			if (InsertedCard.Count > 0)
			{
				foreach (SmartDeckCardData item in InsertedCard)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasDeckId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeckId);
			}
			if (HasFormatType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FormatType);
			}
			if (HasVersion)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Version);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
