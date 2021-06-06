using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class RecruitData : IProtoBuf
	{
		public bool HasGraduationDate;

		private Date _GraduationDate;

		public bool HasRecruiterReward;

		private uint _RecruiterReward;

		public BnetId GameAccountId { get; set; }

		public uint Progress { get; set; }

		public RecruitAFriendState RecruitState { get; set; }

		public Date GraduationDate
		{
			get
			{
				return _GraduationDate;
			}
			set
			{
				_GraduationDate = value;
				HasGraduationDate = value != null;
			}
		}

		public uint RecruiterReward
		{
			get
			{
				return _RecruiterReward;
			}
			set
			{
				_RecruiterReward = value;
				HasRecruiterReward = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameAccountId.GetHashCode();
			hashCode ^= Progress.GetHashCode();
			hashCode ^= RecruitState.GetHashCode();
			if (HasGraduationDate)
			{
				hashCode ^= GraduationDate.GetHashCode();
			}
			if (HasRecruiterReward)
			{
				hashCode ^= RecruiterReward.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			RecruitData recruitData = obj as RecruitData;
			if (recruitData == null)
			{
				return false;
			}
			if (!GameAccountId.Equals(recruitData.GameAccountId))
			{
				return false;
			}
			if (!Progress.Equals(recruitData.Progress))
			{
				return false;
			}
			if (!RecruitState.Equals(recruitData.RecruitState))
			{
				return false;
			}
			if (HasGraduationDate != recruitData.HasGraduationDate || (HasGraduationDate && !GraduationDate.Equals(recruitData.GraduationDate)))
			{
				return false;
			}
			if (HasRecruiterReward != recruitData.HasRecruiterReward || (HasRecruiterReward && !RecruiterReward.Equals(recruitData.RecruiterReward)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RecruitData Deserialize(Stream stream, RecruitData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RecruitData DeserializeLengthDelimited(Stream stream)
		{
			RecruitData recruitData = new RecruitData();
			DeserializeLengthDelimited(stream, recruitData);
			return recruitData;
		}

		public static RecruitData DeserializeLengthDelimited(Stream stream, RecruitData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RecruitData Deserialize(Stream stream, RecruitData instance, long limit)
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
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
					continue;
				case 16:
					instance.Progress = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.RecruitState = (RecruitAFriendState)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					if (instance.GraduationDate == null)
					{
						instance.GraduationDate = Date.DeserializeLengthDelimited(stream);
					}
					else
					{
						Date.DeserializeLengthDelimited(stream, instance.GraduationDate);
					}
					continue;
				case 40:
					instance.RecruiterReward = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, RecruitData instance)
		{
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.GameAccountId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Progress);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.RecruitState);
			if (instance.HasGraduationDate)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.GraduationDate.GetSerializedSize());
				Date.Serialize(stream, instance.GraduationDate);
			}
			if (instance.HasRecruiterReward)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.RecruiterReward);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = GameAccountId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt32(Progress);
			num += ProtocolParser.SizeOfUInt64((ulong)RecruitState);
			if (HasGraduationDate)
			{
				num++;
				uint serializedSize2 = GraduationDate.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasRecruiterReward)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(RecruiterReward);
			}
			return num + 3;
		}
	}
}
