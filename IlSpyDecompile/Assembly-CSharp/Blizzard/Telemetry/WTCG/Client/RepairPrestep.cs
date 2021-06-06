using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class RepairPrestep : IProtoBuf
	{
		public bool HasDoubletapFingers;

		private int _DoubletapFingers;

		public bool HasLocales;

		private int _Locales;

		public int DoubletapFingers
		{
			get
			{
				return _DoubletapFingers;
			}
			set
			{
				_DoubletapFingers = value;
				HasDoubletapFingers = true;
			}
		}

		public int Locales
		{
			get
			{
				return _Locales;
			}
			set
			{
				_Locales = value;
				HasLocales = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDoubletapFingers)
			{
				num ^= DoubletapFingers.GetHashCode();
			}
			if (HasLocales)
			{
				num ^= Locales.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RepairPrestep repairPrestep = obj as RepairPrestep;
			if (repairPrestep == null)
			{
				return false;
			}
			if (HasDoubletapFingers != repairPrestep.HasDoubletapFingers || (HasDoubletapFingers && !DoubletapFingers.Equals(repairPrestep.DoubletapFingers)))
			{
				return false;
			}
			if (HasLocales != repairPrestep.HasLocales || (HasLocales && !Locales.Equals(repairPrestep.Locales)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RepairPrestep Deserialize(Stream stream, RepairPrestep instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RepairPrestep DeserializeLengthDelimited(Stream stream)
		{
			RepairPrestep repairPrestep = new RepairPrestep();
			DeserializeLengthDelimited(stream, repairPrestep);
			return repairPrestep;
		}

		public static RepairPrestep DeserializeLengthDelimited(Stream stream, RepairPrestep instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RepairPrestep Deserialize(Stream stream, RepairPrestep instance, long limit)
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
				case 8:
					instance.DoubletapFingers = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Locales = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, RepairPrestep instance)
		{
			if (instance.HasDoubletapFingers)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DoubletapFingers);
			}
			if (instance.HasLocales)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Locales);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasDoubletapFingers)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DoubletapFingers);
			}
			if (HasLocales)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Locales);
			}
			return num;
		}
	}
}
