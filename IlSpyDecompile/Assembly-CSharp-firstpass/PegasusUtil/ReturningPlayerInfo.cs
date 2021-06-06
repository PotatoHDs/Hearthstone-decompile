using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class ReturningPlayerInfo : IProtoBuf
	{
		public bool HasAbTestGroup;

		private uint _AbTestGroup;

		public ReturningPlayerStatus Status { get; set; }

		public uint AbTestGroup
		{
			get
			{
				return _AbTestGroup;
			}
			set
			{
				_AbTestGroup = value;
				HasAbTestGroup = true;
			}
		}

		public long NotificationSuppressionTimeDays { get; set; }

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Status.GetHashCode();
			if (HasAbTestGroup)
			{
				hashCode ^= AbTestGroup.GetHashCode();
			}
			return hashCode ^ NotificationSuppressionTimeDays.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ReturningPlayerInfo returningPlayerInfo = obj as ReturningPlayerInfo;
			if (returningPlayerInfo == null)
			{
				return false;
			}
			if (!Status.Equals(returningPlayerInfo.Status))
			{
				return false;
			}
			if (HasAbTestGroup != returningPlayerInfo.HasAbTestGroup || (HasAbTestGroup && !AbTestGroup.Equals(returningPlayerInfo.AbTestGroup)))
			{
				return false;
			}
			if (!NotificationSuppressionTimeDays.Equals(returningPlayerInfo.NotificationSuppressionTimeDays))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ReturningPlayerInfo Deserialize(Stream stream, ReturningPlayerInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ReturningPlayerInfo DeserializeLengthDelimited(Stream stream)
		{
			ReturningPlayerInfo returningPlayerInfo = new ReturningPlayerInfo();
			DeserializeLengthDelimited(stream, returningPlayerInfo);
			return returningPlayerInfo;
		}

		public static ReturningPlayerInfo DeserializeLengthDelimited(Stream stream, ReturningPlayerInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ReturningPlayerInfo Deserialize(Stream stream, ReturningPlayerInfo instance, long limit)
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
					instance.Status = (ReturningPlayerStatus)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.AbTestGroup = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.NotificationSuppressionTimeDays = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ReturningPlayerInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Status);
			if (instance.HasAbTestGroup)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.AbTestGroup);
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.NotificationSuppressionTimeDays);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Status);
			if (HasAbTestGroup)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(AbTestGroup);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)NotificationSuppressionTimeDays);
			return num + 2;
		}
	}
}
