using System.IO;
using System.Text;

namespace PegasusGame
{
	public class PowerHistoryStart : IProtoBuf
	{
		public bool HasEffectCardId;

		private string _EffectCardId;

		public bool HasEffectIndex;

		private int _EffectIndex;

		public bool HasTriggerKeyword;

		private int _TriggerKeyword;

		public bool HasShowInHistory;

		private bool _ShowInHistory;

		public bool HasIsDeferrable;

		private bool _IsDeferrable;

		public bool HasIsBatchable;

		private bool _IsBatchable;

		public bool HasIsDeferBlocker;

		private bool _IsDeferBlocker;

		public bool HasForceShowBigCard;

		private bool _ForceShowBigCard;

		public HistoryBlock.Type Type { get; set; }

		public int SubOption { get; set; }

		public int Source { get; set; }

		public int Target { get; set; }

		public string EffectCardId
		{
			get
			{
				return _EffectCardId;
			}
			set
			{
				_EffectCardId = value;
				HasEffectCardId = value != null;
			}
		}

		public int EffectIndex
		{
			get
			{
				return _EffectIndex;
			}
			set
			{
				_EffectIndex = value;
				HasEffectIndex = true;
			}
		}

		public int TriggerKeyword
		{
			get
			{
				return _TriggerKeyword;
			}
			set
			{
				_TriggerKeyword = value;
				HasTriggerKeyword = true;
			}
		}

		public bool ShowInHistory
		{
			get
			{
				return _ShowInHistory;
			}
			set
			{
				_ShowInHistory = value;
				HasShowInHistory = true;
			}
		}

		public bool IsDeferrable
		{
			get
			{
				return _IsDeferrable;
			}
			set
			{
				_IsDeferrable = value;
				HasIsDeferrable = true;
			}
		}

		public bool IsBatchable
		{
			get
			{
				return _IsBatchable;
			}
			set
			{
				_IsBatchable = value;
				HasIsBatchable = true;
			}
		}

		public bool IsDeferBlocker
		{
			get
			{
				return _IsDeferBlocker;
			}
			set
			{
				_IsDeferBlocker = value;
				HasIsDeferBlocker = true;
			}
		}

		public bool ForceShowBigCard
		{
			get
			{
				return _ForceShowBigCard;
			}
			set
			{
				_ForceShowBigCard = value;
				HasForceShowBigCard = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Type.GetHashCode();
			hashCode ^= SubOption.GetHashCode();
			hashCode ^= Source.GetHashCode();
			hashCode ^= Target.GetHashCode();
			if (HasEffectCardId)
			{
				hashCode ^= EffectCardId.GetHashCode();
			}
			if (HasEffectIndex)
			{
				hashCode ^= EffectIndex.GetHashCode();
			}
			if (HasTriggerKeyword)
			{
				hashCode ^= TriggerKeyword.GetHashCode();
			}
			if (HasShowInHistory)
			{
				hashCode ^= ShowInHistory.GetHashCode();
			}
			if (HasIsDeferrable)
			{
				hashCode ^= IsDeferrable.GetHashCode();
			}
			if (HasIsBatchable)
			{
				hashCode ^= IsBatchable.GetHashCode();
			}
			if (HasIsDeferBlocker)
			{
				hashCode ^= IsDeferBlocker.GetHashCode();
			}
			if (HasForceShowBigCard)
			{
				hashCode ^= ForceShowBigCard.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PowerHistoryStart powerHistoryStart = obj as PowerHistoryStart;
			if (powerHistoryStart == null)
			{
				return false;
			}
			if (!Type.Equals(powerHistoryStart.Type))
			{
				return false;
			}
			if (!SubOption.Equals(powerHistoryStart.SubOption))
			{
				return false;
			}
			if (!Source.Equals(powerHistoryStart.Source))
			{
				return false;
			}
			if (!Target.Equals(powerHistoryStart.Target))
			{
				return false;
			}
			if (HasEffectCardId != powerHistoryStart.HasEffectCardId || (HasEffectCardId && !EffectCardId.Equals(powerHistoryStart.EffectCardId)))
			{
				return false;
			}
			if (HasEffectIndex != powerHistoryStart.HasEffectIndex || (HasEffectIndex && !EffectIndex.Equals(powerHistoryStart.EffectIndex)))
			{
				return false;
			}
			if (HasTriggerKeyword != powerHistoryStart.HasTriggerKeyword || (HasTriggerKeyword && !TriggerKeyword.Equals(powerHistoryStart.TriggerKeyword)))
			{
				return false;
			}
			if (HasShowInHistory != powerHistoryStart.HasShowInHistory || (HasShowInHistory && !ShowInHistory.Equals(powerHistoryStart.ShowInHistory)))
			{
				return false;
			}
			if (HasIsDeferrable != powerHistoryStart.HasIsDeferrable || (HasIsDeferrable && !IsDeferrable.Equals(powerHistoryStart.IsDeferrable)))
			{
				return false;
			}
			if (HasIsBatchable != powerHistoryStart.HasIsBatchable || (HasIsBatchable && !IsBatchable.Equals(powerHistoryStart.IsBatchable)))
			{
				return false;
			}
			if (HasIsDeferBlocker != powerHistoryStart.HasIsDeferBlocker || (HasIsDeferBlocker && !IsDeferBlocker.Equals(powerHistoryStart.IsDeferBlocker)))
			{
				return false;
			}
			if (HasForceShowBigCard != powerHistoryStart.HasForceShowBigCard || (HasForceShowBigCard && !ForceShowBigCard.Equals(powerHistoryStart.ForceShowBigCard)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PowerHistoryStart Deserialize(Stream stream, PowerHistoryStart instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PowerHistoryStart DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryStart powerHistoryStart = new PowerHistoryStart();
			DeserializeLengthDelimited(stream, powerHistoryStart);
			return powerHistoryStart;
		}

		public static PowerHistoryStart DeserializeLengthDelimited(Stream stream, PowerHistoryStart instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PowerHistoryStart Deserialize(Stream stream, PowerHistoryStart instance, long limit)
		{
			instance.EffectIndex = 0;
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
					instance.Type = (HistoryBlock.Type)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.SubOption = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Source = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Target = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					instance.EffectCardId = ProtocolParser.ReadString(stream);
					continue;
				case 48:
					instance.EffectIndex = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.TriggerKeyword = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.ShowInHistory = ProtocolParser.ReadBool(stream);
					continue;
				case 72:
					instance.IsDeferrable = ProtocolParser.ReadBool(stream);
					continue;
				case 80:
					instance.IsBatchable = ProtocolParser.ReadBool(stream);
					continue;
				case 88:
					instance.IsDeferBlocker = ProtocolParser.ReadBool(stream);
					continue;
				case 96:
					instance.ForceShowBigCard = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, PowerHistoryStart instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Type);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SubOption);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Source);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Target);
			if (instance.HasEffectCardId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EffectCardId));
			}
			if (instance.HasEffectIndex)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.EffectIndex);
			}
			if (instance.HasTriggerKeyword)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TriggerKeyword);
			}
			if (instance.HasShowInHistory)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.ShowInHistory);
			}
			if (instance.HasIsDeferrable)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.IsDeferrable);
			}
			if (instance.HasIsBatchable)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.IsBatchable);
			}
			if (instance.HasIsDeferBlocker)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.IsDeferBlocker);
			}
			if (instance.HasForceShowBigCard)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteBool(stream, instance.ForceShowBigCard);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Type);
			num += ProtocolParser.SizeOfUInt64((ulong)SubOption);
			num += ProtocolParser.SizeOfUInt64((ulong)Source);
			num += ProtocolParser.SizeOfUInt64((ulong)Target);
			if (HasEffectCardId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(EffectCardId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasEffectIndex)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)EffectIndex);
			}
			if (HasTriggerKeyword)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TriggerKeyword);
			}
			if (HasShowInHistory)
			{
				num++;
				num++;
			}
			if (HasIsDeferrable)
			{
				num++;
				num++;
			}
			if (HasIsBatchable)
			{
				num++;
				num++;
			}
			if (HasIsDeferBlocker)
			{
				num++;
				num++;
			}
			if (HasForceShowBigCard)
			{
				num++;
				num++;
			}
			return num + 4;
		}
	}
}
