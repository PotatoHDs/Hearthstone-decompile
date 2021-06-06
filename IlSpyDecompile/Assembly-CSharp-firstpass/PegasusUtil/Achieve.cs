using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class Achieve : IProtoBuf
	{
		public bool HasCompletionCount;

		private int _CompletionCount;

		public bool HasActive;

		private bool _Active;

		public bool HasStartedCount;

		private int _StartedCount;

		public bool HasDateGiven;

		private Date _DateGiven;

		public bool HasDateCompleted;

		private Date _DateCompleted;

		public bool HasDoNotAck;

		private bool _DoNotAck;

		public bool HasIntervalRewardCount;

		private int _IntervalRewardCount;

		public bool HasIntervalRewardStart;

		private Date _IntervalRewardStart;

		public int Id { get; set; }

		public int Progress { get; set; }

		public int AckProgress { get; set; }

		public int CompletionCount
		{
			get
			{
				return _CompletionCount;
			}
			set
			{
				_CompletionCount = value;
				HasCompletionCount = true;
			}
		}

		public bool Active
		{
			get
			{
				return _Active;
			}
			set
			{
				_Active = value;
				HasActive = true;
			}
		}

		public int StartedCount
		{
			get
			{
				return _StartedCount;
			}
			set
			{
				_StartedCount = value;
				HasStartedCount = true;
			}
		}

		public Date DateGiven
		{
			get
			{
				return _DateGiven;
			}
			set
			{
				_DateGiven = value;
				HasDateGiven = value != null;
			}
		}

		public Date DateCompleted
		{
			get
			{
				return _DateCompleted;
			}
			set
			{
				_DateCompleted = value;
				HasDateCompleted = value != null;
			}
		}

		public bool DoNotAck
		{
			get
			{
				return _DoNotAck;
			}
			set
			{
				_DoNotAck = value;
				HasDoNotAck = true;
			}
		}

		public int IntervalRewardCount
		{
			get
			{
				return _IntervalRewardCount;
			}
			set
			{
				_IntervalRewardCount = value;
				HasIntervalRewardCount = true;
			}
		}

		public Date IntervalRewardStart
		{
			get
			{
				return _IntervalRewardStart;
			}
			set
			{
				_IntervalRewardStart = value;
				HasIntervalRewardStart = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			hashCode ^= Progress.GetHashCode();
			hashCode ^= AckProgress.GetHashCode();
			if (HasCompletionCount)
			{
				hashCode ^= CompletionCount.GetHashCode();
			}
			if (HasActive)
			{
				hashCode ^= Active.GetHashCode();
			}
			if (HasStartedCount)
			{
				hashCode ^= StartedCount.GetHashCode();
			}
			if (HasDateGiven)
			{
				hashCode ^= DateGiven.GetHashCode();
			}
			if (HasDateCompleted)
			{
				hashCode ^= DateCompleted.GetHashCode();
			}
			if (HasDoNotAck)
			{
				hashCode ^= DoNotAck.GetHashCode();
			}
			if (HasIntervalRewardCount)
			{
				hashCode ^= IntervalRewardCount.GetHashCode();
			}
			if (HasIntervalRewardStart)
			{
				hashCode ^= IntervalRewardStart.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Achieve achieve = obj as Achieve;
			if (achieve == null)
			{
				return false;
			}
			if (!Id.Equals(achieve.Id))
			{
				return false;
			}
			if (!Progress.Equals(achieve.Progress))
			{
				return false;
			}
			if (!AckProgress.Equals(achieve.AckProgress))
			{
				return false;
			}
			if (HasCompletionCount != achieve.HasCompletionCount || (HasCompletionCount && !CompletionCount.Equals(achieve.CompletionCount)))
			{
				return false;
			}
			if (HasActive != achieve.HasActive || (HasActive && !Active.Equals(achieve.Active)))
			{
				return false;
			}
			if (HasStartedCount != achieve.HasStartedCount || (HasStartedCount && !StartedCount.Equals(achieve.StartedCount)))
			{
				return false;
			}
			if (HasDateGiven != achieve.HasDateGiven || (HasDateGiven && !DateGiven.Equals(achieve.DateGiven)))
			{
				return false;
			}
			if (HasDateCompleted != achieve.HasDateCompleted || (HasDateCompleted && !DateCompleted.Equals(achieve.DateCompleted)))
			{
				return false;
			}
			if (HasDoNotAck != achieve.HasDoNotAck || (HasDoNotAck && !DoNotAck.Equals(achieve.DoNotAck)))
			{
				return false;
			}
			if (HasIntervalRewardCount != achieve.HasIntervalRewardCount || (HasIntervalRewardCount && !IntervalRewardCount.Equals(achieve.IntervalRewardCount)))
			{
				return false;
			}
			if (HasIntervalRewardStart != achieve.HasIntervalRewardStart || (HasIntervalRewardStart && !IntervalRewardStart.Equals(achieve.IntervalRewardStart)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Achieve Deserialize(Stream stream, Achieve instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Achieve DeserializeLengthDelimited(Stream stream)
		{
			Achieve achieve = new Achieve();
			DeserializeLengthDelimited(stream, achieve);
			return achieve;
		}

		public static Achieve DeserializeLengthDelimited(Stream stream, Achieve instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Achieve Deserialize(Stream stream, Achieve instance, long limit)
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
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Progress = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.AckProgress = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.CompletionCount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.Active = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.StartedCount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 58:
					if (instance.DateGiven == null)
					{
						instance.DateGiven = Date.DeserializeLengthDelimited(stream);
					}
					else
					{
						Date.DeserializeLengthDelimited(stream, instance.DateGiven);
					}
					continue;
				case 66:
					if (instance.DateCompleted == null)
					{
						instance.DateCompleted = Date.DeserializeLengthDelimited(stream);
					}
					else
					{
						Date.DeserializeLengthDelimited(stream, instance.DateCompleted);
					}
					continue;
				case 72:
					instance.DoNotAck = ProtocolParser.ReadBool(stream);
					continue;
				case 80:
					instance.IntervalRewardCount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 90:
					if (instance.IntervalRewardStart == null)
					{
						instance.IntervalRewardStart = Date.DeserializeLengthDelimited(stream);
					}
					else
					{
						Date.DeserializeLengthDelimited(stream, instance.IntervalRewardStart);
					}
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

		public static void Serialize(Stream stream, Achieve instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Progress);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.AckProgress);
			if (instance.HasCompletionCount)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CompletionCount);
			}
			if (instance.HasActive)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.Active);
			}
			if (instance.HasStartedCount)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.StartedCount);
			}
			if (instance.HasDateGiven)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.DateGiven.GetSerializedSize());
				Date.Serialize(stream, instance.DateGiven);
			}
			if (instance.HasDateCompleted)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.DateCompleted.GetSerializedSize());
				Date.Serialize(stream, instance.DateCompleted);
			}
			if (instance.HasDoNotAck)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.DoNotAck);
			}
			if (instance.HasIntervalRewardCount)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.IntervalRewardCount);
			}
			if (instance.HasIntervalRewardStart)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.IntervalRewardStart.GetSerializedSize());
				Date.Serialize(stream, instance.IntervalRewardStart);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			num += ProtocolParser.SizeOfUInt64((ulong)Progress);
			num += ProtocolParser.SizeOfUInt64((ulong)AckProgress);
			if (HasCompletionCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CompletionCount);
			}
			if (HasActive)
			{
				num++;
				num++;
			}
			if (HasStartedCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)StartedCount);
			}
			if (HasDateGiven)
			{
				num++;
				uint serializedSize = DateGiven.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasDateCompleted)
			{
				num++;
				uint serializedSize2 = DateCompleted.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasDoNotAck)
			{
				num++;
				num++;
			}
			if (HasIntervalRewardCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)IntervalRewardCount);
			}
			if (HasIntervalRewardStart)
			{
				num++;
				uint serializedSize3 = IntervalRewardStart.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 3;
		}
	}
}
