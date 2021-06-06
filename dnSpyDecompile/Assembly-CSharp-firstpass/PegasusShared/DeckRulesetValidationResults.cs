using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000148 RID: 328
	public class DeckRulesetValidationResults : IProtoBuf
	{
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x0004A543 File Offset: 0x00048743
		// (set) Token: 0x060015B1 RID: 5553 RVA: 0x0004A54B File Offset: 0x0004874B
		public int DeckRulesetId { get; set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x0004A554 File Offset: 0x00048754
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x0004A55C File Offset: 0x0004875C
		public ErrorCode ErrorCode
		{
			get
			{
				return this._ErrorCode;
			}
			set
			{
				this._ErrorCode = value;
				this.HasErrorCode = true;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x0004A56C File Offset: 0x0004876C
		// (set) Token: 0x060015B5 RID: 5557 RVA: 0x0004A574 File Offset: 0x00048774
		public List<DeckRulesetViolation> Violations
		{
			get
			{
				return this._Violations;
			}
			set
			{
				this._Violations = value;
			}
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0004A580 File Offset: 0x00048780
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.DeckRulesetId.GetHashCode();
			if (this.HasErrorCode)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			foreach (DeckRulesetViolation deckRulesetViolation in this.Violations)
			{
				num ^= deckRulesetViolation.GetHashCode();
			}
			return num;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0004A614 File Offset: 0x00048814
		public override bool Equals(object obj)
		{
			DeckRulesetValidationResults deckRulesetValidationResults = obj as DeckRulesetValidationResults;
			if (deckRulesetValidationResults == null)
			{
				return false;
			}
			if (!this.DeckRulesetId.Equals(deckRulesetValidationResults.DeckRulesetId))
			{
				return false;
			}
			if (this.HasErrorCode != deckRulesetValidationResults.HasErrorCode || (this.HasErrorCode && !this.ErrorCode.Equals(deckRulesetValidationResults.ErrorCode)))
			{
				return false;
			}
			if (this.Violations.Count != deckRulesetValidationResults.Violations.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Violations.Count; i++)
			{
				if (!this.Violations[i].Equals(deckRulesetValidationResults.Violations[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x0004A6D0 File Offset: 0x000488D0
		public void Deserialize(Stream stream)
		{
			DeckRulesetValidationResults.Deserialize(stream, this);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0004A6DA File Offset: 0x000488DA
		public static DeckRulesetValidationResults Deserialize(Stream stream, DeckRulesetValidationResults instance)
		{
			return DeckRulesetValidationResults.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0004A6E8 File Offset: 0x000488E8
		public static DeckRulesetValidationResults DeserializeLengthDelimited(Stream stream)
		{
			DeckRulesetValidationResults deckRulesetValidationResults = new DeckRulesetValidationResults();
			DeckRulesetValidationResults.DeserializeLengthDelimited(stream, deckRulesetValidationResults);
			return deckRulesetValidationResults;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0004A704 File Offset: 0x00048904
		public static DeckRulesetValidationResults DeserializeLengthDelimited(Stream stream, DeckRulesetValidationResults instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckRulesetValidationResults.Deserialize(stream, instance, num);
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0004A72C File Offset: 0x0004892C
		public static DeckRulesetValidationResults Deserialize(Stream stream, DeckRulesetValidationResults instance, long limit)
		{
			instance.DeckRulesetId = 0;
			instance.ErrorCode = ErrorCode.ERROR_OK;
			if (instance.Violations == null)
			{
				instance.Violations = new List<DeckRulesetViolation>();
			}
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
				else if (num != 8)
				{
					if (num != 16)
					{
						if (num != 26)
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
							instance.Violations.Add(DeckRulesetViolation.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.DeckRulesetId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x0004A801 File Offset: 0x00048A01
		public void Serialize(Stream stream)
		{
			DeckRulesetValidationResults.Serialize(stream, this);
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x0004A80C File Offset: 0x00048A0C
		public static void Serialize(Stream stream, DeckRulesetValidationResults instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeckRulesetId));
			if (instance.HasErrorCode)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			}
			if (instance.Violations.Count > 0)
			{
				foreach (DeckRulesetViolation deckRulesetViolation in instance.Violations)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, deckRulesetViolation.GetSerializedSize());
					DeckRulesetViolation.Serialize(stream, deckRulesetViolation);
				}
			}
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0004A8B4 File Offset: 0x00048AB4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeckRulesetId));
			if (this.HasErrorCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			}
			if (this.Violations.Count > 0)
			{
				foreach (DeckRulesetViolation deckRulesetViolation in this.Violations)
				{
					num += 1U;
					uint serializedSize = deckRulesetViolation.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x040006AC RID: 1708
		public bool HasErrorCode;

		// Token: 0x040006AD RID: 1709
		private ErrorCode _ErrorCode;

		// Token: 0x040006AE RID: 1710
		private List<DeckRulesetViolation> _Violations = new List<DeckRulesetViolation>();
	}
}
