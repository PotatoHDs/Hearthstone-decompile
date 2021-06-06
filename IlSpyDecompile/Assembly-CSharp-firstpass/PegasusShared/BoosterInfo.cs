using System.IO;

namespace PegasusShared
{
	public class BoosterInfo : IProtoBuf
	{
		public bool HasEverGrantedCount;

		private int _EverGrantedCount;

		public int Type { get; set; }

		public int Count { get; set; }

		public int EverGrantedCount
		{
			get
			{
				return _EverGrantedCount;
			}
			set
			{
				_EverGrantedCount = value;
				HasEverGrantedCount = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Type.GetHashCode();
			hashCode ^= Count.GetHashCode();
			if (HasEverGrantedCount)
			{
				hashCode ^= EverGrantedCount.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			BoosterInfo boosterInfo = obj as BoosterInfo;
			if (boosterInfo == null)
			{
				return false;
			}
			if (!Type.Equals(boosterInfo.Type))
			{
				return false;
			}
			if (!Count.Equals(boosterInfo.Count))
			{
				return false;
			}
			if (HasEverGrantedCount != boosterInfo.HasEverGrantedCount || (HasEverGrantedCount && !EverGrantedCount.Equals(boosterInfo.EverGrantedCount)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BoosterInfo Deserialize(Stream stream, BoosterInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BoosterInfo DeserializeLengthDelimited(Stream stream)
		{
			BoosterInfo boosterInfo = new BoosterInfo();
			DeserializeLengthDelimited(stream, boosterInfo);
			return boosterInfo;
		}

		public static BoosterInfo DeserializeLengthDelimited(Stream stream, BoosterInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BoosterInfo Deserialize(Stream stream, BoosterInfo instance, long limit)
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
				case 16:
					instance.Type = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Count = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.EverGrantedCount = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, BoosterInfo instance)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Type);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Count);
			if (instance.HasEverGrantedCount)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.EverGrantedCount);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Type);
			num += ProtocolParser.SizeOfUInt64((ulong)Count);
			if (HasEverGrantedCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)EverGrantedCount);
			}
			return num + 2;
		}
	}
}
