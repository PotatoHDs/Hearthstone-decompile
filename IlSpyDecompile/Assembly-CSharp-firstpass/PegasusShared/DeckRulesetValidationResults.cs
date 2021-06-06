using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	public class DeckRulesetValidationResults : IProtoBuf
	{
		public bool HasErrorCode;

		private ErrorCode _ErrorCode;

		private List<DeckRulesetViolation> _Violations = new List<DeckRulesetViolation>();

		public int DeckRulesetId { get; set; }

		public ErrorCode ErrorCode
		{
			get
			{
				return _ErrorCode;
			}
			set
			{
				_ErrorCode = value;
				HasErrorCode = true;
			}
		}

		public List<DeckRulesetViolation> Violations
		{
			get
			{
				return _Violations;
			}
			set
			{
				_Violations = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= DeckRulesetId.GetHashCode();
			if (HasErrorCode)
			{
				hashCode ^= ErrorCode.GetHashCode();
			}
			foreach (DeckRulesetViolation violation in Violations)
			{
				hashCode ^= violation.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DeckRulesetValidationResults deckRulesetValidationResults = obj as DeckRulesetValidationResults;
			if (deckRulesetValidationResults == null)
			{
				return false;
			}
			if (!DeckRulesetId.Equals(deckRulesetValidationResults.DeckRulesetId))
			{
				return false;
			}
			if (HasErrorCode != deckRulesetValidationResults.HasErrorCode || (HasErrorCode && !ErrorCode.Equals(deckRulesetValidationResults.ErrorCode)))
			{
				return false;
			}
			if (Violations.Count != deckRulesetValidationResults.Violations.Count)
			{
				return false;
			}
			for (int i = 0; i < Violations.Count; i++)
			{
				if (!Violations[i].Equals(deckRulesetValidationResults.Violations[i]))
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

		public static DeckRulesetValidationResults Deserialize(Stream stream, DeckRulesetValidationResults instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckRulesetValidationResults DeserializeLengthDelimited(Stream stream)
		{
			DeckRulesetValidationResults deckRulesetValidationResults = new DeckRulesetValidationResults();
			DeserializeLengthDelimited(stream, deckRulesetValidationResults);
			return deckRulesetValidationResults;
		}

		public static DeckRulesetValidationResults DeserializeLengthDelimited(Stream stream, DeckRulesetValidationResults instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckRulesetValidationResults Deserialize(Stream stream, DeckRulesetValidationResults instance, long limit)
		{
			instance.DeckRulesetId = 0;
			instance.ErrorCode = ErrorCode.ERROR_OK;
			if (instance.Violations == null)
			{
				instance.Violations = new List<DeckRulesetViolation>();
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
					instance.DeckRulesetId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.Violations.Add(DeckRulesetViolation.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, DeckRulesetValidationResults instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckRulesetId);
			if (instance.HasErrorCode)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
			}
			if (instance.Violations.Count <= 0)
			{
				return;
			}
			foreach (DeckRulesetViolation violation in instance.Violations)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, violation.GetSerializedSize());
				DeckRulesetViolation.Serialize(stream, violation);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)DeckRulesetId);
			if (HasErrorCode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode);
			}
			if (Violations.Count > 0)
			{
				foreach (DeckRulesetViolation violation in Violations)
				{
					num++;
					uint serializedSize = violation.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 1;
		}
	}
}
