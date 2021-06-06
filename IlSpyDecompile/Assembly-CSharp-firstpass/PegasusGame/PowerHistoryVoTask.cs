using System.IO;
using System.Text;

namespace PegasusGame
{
	public class PowerHistoryVoTask : IProtoBuf
	{
		public bool HasSpellPrefabGuid;

		private string _SpellPrefabGuid;

		public bool HasBlocking;

		private bool _Blocking;

		public bool HasAdditionalDelayMs;

		private int _AdditionalDelayMs;

		public bool HasSpeakingEntity;

		private int _SpeakingEntity;

		public bool HasBrassRingPrefabGuid;

		private string _BrassRingPrefabGuid;

		public string SpellPrefabGuid
		{
			get
			{
				return _SpellPrefabGuid;
			}
			set
			{
				_SpellPrefabGuid = value;
				HasSpellPrefabGuid = value != null;
			}
		}

		public bool Blocking
		{
			get
			{
				return _Blocking;
			}
			set
			{
				_Blocking = value;
				HasBlocking = true;
			}
		}

		public int AdditionalDelayMs
		{
			get
			{
				return _AdditionalDelayMs;
			}
			set
			{
				_AdditionalDelayMs = value;
				HasAdditionalDelayMs = true;
			}
		}

		public int SpeakingEntity
		{
			get
			{
				return _SpeakingEntity;
			}
			set
			{
				_SpeakingEntity = value;
				HasSpeakingEntity = true;
			}
		}

		public string BrassRingPrefabGuid
		{
			get
			{
				return _BrassRingPrefabGuid;
			}
			set
			{
				_BrassRingPrefabGuid = value;
				HasBrassRingPrefabGuid = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSpellPrefabGuid)
			{
				num ^= SpellPrefabGuid.GetHashCode();
			}
			if (HasBlocking)
			{
				num ^= Blocking.GetHashCode();
			}
			if (HasAdditionalDelayMs)
			{
				num ^= AdditionalDelayMs.GetHashCode();
			}
			if (HasSpeakingEntity)
			{
				num ^= SpeakingEntity.GetHashCode();
			}
			if (HasBrassRingPrefabGuid)
			{
				num ^= BrassRingPrefabGuid.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PowerHistoryVoTask powerHistoryVoTask = obj as PowerHistoryVoTask;
			if (powerHistoryVoTask == null)
			{
				return false;
			}
			if (HasSpellPrefabGuid != powerHistoryVoTask.HasSpellPrefabGuid || (HasSpellPrefabGuid && !SpellPrefabGuid.Equals(powerHistoryVoTask.SpellPrefabGuid)))
			{
				return false;
			}
			if (HasBlocking != powerHistoryVoTask.HasBlocking || (HasBlocking && !Blocking.Equals(powerHistoryVoTask.Blocking)))
			{
				return false;
			}
			if (HasAdditionalDelayMs != powerHistoryVoTask.HasAdditionalDelayMs || (HasAdditionalDelayMs && !AdditionalDelayMs.Equals(powerHistoryVoTask.AdditionalDelayMs)))
			{
				return false;
			}
			if (HasSpeakingEntity != powerHistoryVoTask.HasSpeakingEntity || (HasSpeakingEntity && !SpeakingEntity.Equals(powerHistoryVoTask.SpeakingEntity)))
			{
				return false;
			}
			if (HasBrassRingPrefabGuid != powerHistoryVoTask.HasBrassRingPrefabGuid || (HasBrassRingPrefabGuid && !BrassRingPrefabGuid.Equals(powerHistoryVoTask.BrassRingPrefabGuid)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PowerHistoryVoTask Deserialize(Stream stream, PowerHistoryVoTask instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PowerHistoryVoTask DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryVoTask powerHistoryVoTask = new PowerHistoryVoTask();
			DeserializeLengthDelimited(stream, powerHistoryVoTask);
			return powerHistoryVoTask;
		}

		public static PowerHistoryVoTask DeserializeLengthDelimited(Stream stream, PowerHistoryVoTask instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PowerHistoryVoTask Deserialize(Stream stream, PowerHistoryVoTask instance, long limit)
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
					instance.SpellPrefabGuid = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.Blocking = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.AdditionalDelayMs = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.SpeakingEntity = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					instance.BrassRingPrefabGuid = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, PowerHistoryVoTask instance)
		{
			if (instance.HasSpellPrefabGuid)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SpellPrefabGuid));
			}
			if (instance.HasBlocking)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Blocking);
			}
			if (instance.HasAdditionalDelayMs)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AdditionalDelayMs);
			}
			if (instance.HasSpeakingEntity)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SpeakingEntity);
			}
			if (instance.HasBrassRingPrefabGuid)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BrassRingPrefabGuid));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSpellPrefabGuid)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(SpellPrefabGuid);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasBlocking)
			{
				num++;
				num++;
			}
			if (HasAdditionalDelayMs)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)AdditionalDelayMs);
			}
			if (HasSpeakingEntity)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SpeakingEntity);
			}
			if (HasBrassRingPrefabGuid)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(BrassRingPrefabGuid);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
