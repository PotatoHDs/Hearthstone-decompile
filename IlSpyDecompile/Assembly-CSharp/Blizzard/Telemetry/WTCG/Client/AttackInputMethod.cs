using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class AttackInputMethod : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasTotalNumAttacks;

		private long _TotalNumAttacks;

		public bool HasTotalClickAttacks;

		private long _TotalClickAttacks;

		public bool HasPercentClickAttacks;

		private int _PercentClickAttacks;

		public bool HasTotalDragAttacks;

		private long _TotalDragAttacks;

		public bool HasPercentDragAttacks;

		private int _PercentDragAttacks;

		public DeviceInfo DeviceInfo
		{
			get
			{
				return _DeviceInfo;
			}
			set
			{
				_DeviceInfo = value;
				HasDeviceInfo = value != null;
			}
		}

		public long TotalNumAttacks
		{
			get
			{
				return _TotalNumAttacks;
			}
			set
			{
				_TotalNumAttacks = value;
				HasTotalNumAttacks = true;
			}
		}

		public long TotalClickAttacks
		{
			get
			{
				return _TotalClickAttacks;
			}
			set
			{
				_TotalClickAttacks = value;
				HasTotalClickAttacks = true;
			}
		}

		public int PercentClickAttacks
		{
			get
			{
				return _PercentClickAttacks;
			}
			set
			{
				_PercentClickAttacks = value;
				HasPercentClickAttacks = true;
			}
		}

		public long TotalDragAttacks
		{
			get
			{
				return _TotalDragAttacks;
			}
			set
			{
				_TotalDragAttacks = value;
				HasTotalDragAttacks = true;
			}
		}

		public int PercentDragAttacks
		{
			get
			{
				return _PercentDragAttacks;
			}
			set
			{
				_PercentDragAttacks = value;
				HasPercentDragAttacks = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasTotalNumAttacks)
			{
				num ^= TotalNumAttacks.GetHashCode();
			}
			if (HasTotalClickAttacks)
			{
				num ^= TotalClickAttacks.GetHashCode();
			}
			if (HasPercentClickAttacks)
			{
				num ^= PercentClickAttacks.GetHashCode();
			}
			if (HasTotalDragAttacks)
			{
				num ^= TotalDragAttacks.GetHashCode();
			}
			if (HasPercentDragAttacks)
			{
				num ^= PercentDragAttacks.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AttackInputMethod attackInputMethod = obj as AttackInputMethod;
			if (attackInputMethod == null)
			{
				return false;
			}
			if (HasDeviceInfo != attackInputMethod.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(attackInputMethod.DeviceInfo)))
			{
				return false;
			}
			if (HasTotalNumAttacks != attackInputMethod.HasTotalNumAttacks || (HasTotalNumAttacks && !TotalNumAttacks.Equals(attackInputMethod.TotalNumAttacks)))
			{
				return false;
			}
			if (HasTotalClickAttacks != attackInputMethod.HasTotalClickAttacks || (HasTotalClickAttacks && !TotalClickAttacks.Equals(attackInputMethod.TotalClickAttacks)))
			{
				return false;
			}
			if (HasPercentClickAttacks != attackInputMethod.HasPercentClickAttacks || (HasPercentClickAttacks && !PercentClickAttacks.Equals(attackInputMethod.PercentClickAttacks)))
			{
				return false;
			}
			if (HasTotalDragAttacks != attackInputMethod.HasTotalDragAttacks || (HasTotalDragAttacks && !TotalDragAttacks.Equals(attackInputMethod.TotalDragAttacks)))
			{
				return false;
			}
			if (HasPercentDragAttacks != attackInputMethod.HasPercentDragAttacks || (HasPercentDragAttacks && !PercentDragAttacks.Equals(attackInputMethod.PercentDragAttacks)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AttackInputMethod Deserialize(Stream stream, AttackInputMethod instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AttackInputMethod DeserializeLengthDelimited(Stream stream)
		{
			AttackInputMethod attackInputMethod = new AttackInputMethod();
			DeserializeLengthDelimited(stream, attackInputMethod);
			return attackInputMethod;
		}

		public static AttackInputMethod DeserializeLengthDelimited(Stream stream, AttackInputMethod instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AttackInputMethod Deserialize(Stream stream, AttackInputMethod instance, long limit)
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
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 16:
					instance.TotalNumAttacks = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.TotalClickAttacks = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.PercentClickAttacks = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.TotalDragAttacks = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.PercentDragAttacks = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AttackInputMethod instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasTotalNumAttacks)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TotalNumAttacks);
			}
			if (instance.HasTotalClickAttacks)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TotalClickAttacks);
			}
			if (instance.HasPercentClickAttacks)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PercentClickAttacks);
			}
			if (instance.HasTotalDragAttacks)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TotalDragAttacks);
			}
			if (instance.HasPercentDragAttacks)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PercentDragAttacks);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize = DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasTotalNumAttacks)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TotalNumAttacks);
			}
			if (HasTotalClickAttacks)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TotalClickAttacks);
			}
			if (HasPercentClickAttacks)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PercentClickAttacks);
			}
			if (HasTotalDragAttacks)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TotalDragAttacks);
			}
			if (HasPercentDragAttacks)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PercentDragAttacks);
			}
			return num;
		}
	}
}
