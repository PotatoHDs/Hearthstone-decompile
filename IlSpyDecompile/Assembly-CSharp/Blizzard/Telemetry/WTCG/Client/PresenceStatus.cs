using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class PresenceStatus : IProtoBuf
	{
		public bool HasPresenceId;

		private long _PresenceId;

		public bool HasPresenceSubId;

		private long _PresenceSubId;

		public long PresenceId
		{
			get
			{
				return _PresenceId;
			}
			set
			{
				_PresenceId = value;
				HasPresenceId = true;
			}
		}

		public long PresenceSubId
		{
			get
			{
				return _PresenceSubId;
			}
			set
			{
				_PresenceSubId = value;
				HasPresenceSubId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPresenceId)
			{
				num ^= PresenceId.GetHashCode();
			}
			if (HasPresenceSubId)
			{
				num ^= PresenceSubId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PresenceStatus presenceStatus = obj as PresenceStatus;
			if (presenceStatus == null)
			{
				return false;
			}
			if (HasPresenceId != presenceStatus.HasPresenceId || (HasPresenceId && !PresenceId.Equals(presenceStatus.PresenceId)))
			{
				return false;
			}
			if (HasPresenceSubId != presenceStatus.HasPresenceSubId || (HasPresenceSubId && !PresenceSubId.Equals(presenceStatus.PresenceSubId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PresenceStatus Deserialize(Stream stream, PresenceStatus instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PresenceStatus DeserializeLengthDelimited(Stream stream)
		{
			PresenceStatus presenceStatus = new PresenceStatus();
			DeserializeLengthDelimited(stream, presenceStatus);
			return presenceStatus;
		}

		public static PresenceStatus DeserializeLengthDelimited(Stream stream, PresenceStatus instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PresenceStatus Deserialize(Stream stream, PresenceStatus instance, long limit)
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
					instance.PresenceId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.PresenceSubId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PresenceStatus instance)
		{
			if (instance.HasPresenceId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PresenceId);
			}
			if (instance.HasPresenceSubId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PresenceSubId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPresenceId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PresenceId);
			}
			if (HasPresenceSubId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PresenceSubId);
			}
			return num;
		}
	}
}
