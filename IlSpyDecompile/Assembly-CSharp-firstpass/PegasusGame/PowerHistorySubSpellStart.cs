using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	public class PowerHistorySubSpellStart : IProtoBuf
	{
		public bool HasSourceEntityId;

		private int _SourceEntityId;

		private List<int> _TargetEntityIds = new List<int>();

		public string SpellPrefabGuid { get; set; }

		public int SourceEntityId
		{
			get
			{
				return _SourceEntityId;
			}
			set
			{
				_SourceEntityId = value;
				HasSourceEntityId = true;
			}
		}

		public List<int> TargetEntityIds
		{
			get
			{
				return _TargetEntityIds;
			}
			set
			{
				_TargetEntityIds = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= SpellPrefabGuid.GetHashCode();
			if (HasSourceEntityId)
			{
				hashCode ^= SourceEntityId.GetHashCode();
			}
			foreach (int targetEntityId in TargetEntityIds)
			{
				hashCode ^= targetEntityId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PowerHistorySubSpellStart powerHistorySubSpellStart = obj as PowerHistorySubSpellStart;
			if (powerHistorySubSpellStart == null)
			{
				return false;
			}
			if (!SpellPrefabGuid.Equals(powerHistorySubSpellStart.SpellPrefabGuid))
			{
				return false;
			}
			if (HasSourceEntityId != powerHistorySubSpellStart.HasSourceEntityId || (HasSourceEntityId && !SourceEntityId.Equals(powerHistorySubSpellStart.SourceEntityId)))
			{
				return false;
			}
			if (TargetEntityIds.Count != powerHistorySubSpellStart.TargetEntityIds.Count)
			{
				return false;
			}
			for (int i = 0; i < TargetEntityIds.Count; i++)
			{
				if (!TargetEntityIds[i].Equals(powerHistorySubSpellStart.TargetEntityIds[i]))
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

		public static PowerHistorySubSpellStart Deserialize(Stream stream, PowerHistorySubSpellStart instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PowerHistorySubSpellStart DeserializeLengthDelimited(Stream stream)
		{
			PowerHistorySubSpellStart powerHistorySubSpellStart = new PowerHistorySubSpellStart();
			DeserializeLengthDelimited(stream, powerHistorySubSpellStart);
			return powerHistorySubSpellStart;
		}

		public static PowerHistorySubSpellStart DeserializeLengthDelimited(Stream stream, PowerHistorySubSpellStart instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PowerHistorySubSpellStart Deserialize(Stream stream, PowerHistorySubSpellStart instance, long limit)
		{
			if (instance.TargetEntityIds == null)
			{
				instance.TargetEntityIds = new List<int>();
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
					instance.SpellPrefabGuid = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.SourceEntityId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.TargetEntityIds.Add((int)ProtocolParser.ReadUInt64(stream));
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

		public static void Serialize(Stream stream, PowerHistorySubSpellStart instance)
		{
			if (instance.SpellPrefabGuid == null)
			{
				throw new ArgumentNullException("SpellPrefabGuid", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SpellPrefabGuid));
			if (instance.HasSourceEntityId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SourceEntityId);
			}
			if (instance.TargetEntityIds.Count <= 0)
			{
				return;
			}
			foreach (int targetEntityId in instance.TargetEntityIds)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)targetEntityId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(SpellPrefabGuid);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (HasSourceEntityId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SourceEntityId);
			}
			if (TargetEntityIds.Count > 0)
			{
				foreach (int targetEntityId in TargetEntityIds)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)targetEntityId);
				}
			}
			return num + 1;
		}
	}
}
