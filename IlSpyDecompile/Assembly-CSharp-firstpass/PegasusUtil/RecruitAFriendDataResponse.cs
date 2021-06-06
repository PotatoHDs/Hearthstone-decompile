using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class RecruitAFriendDataResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 338
		}

		private List<RecruitData> _TopRecruits = new List<RecruitData>();

		public uint TotalRecruitCount { get; set; }

		public List<RecruitData> TopRecruits
		{
			get
			{
				return _TopRecruits;
			}
			set
			{
				_TopRecruits = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= TotalRecruitCount.GetHashCode();
			foreach (RecruitData topRecruit in TopRecruits)
			{
				hashCode ^= topRecruit.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			RecruitAFriendDataResponse recruitAFriendDataResponse = obj as RecruitAFriendDataResponse;
			if (recruitAFriendDataResponse == null)
			{
				return false;
			}
			if (!TotalRecruitCount.Equals(recruitAFriendDataResponse.TotalRecruitCount))
			{
				return false;
			}
			if (TopRecruits.Count != recruitAFriendDataResponse.TopRecruits.Count)
			{
				return false;
			}
			for (int i = 0; i < TopRecruits.Count; i++)
			{
				if (!TopRecruits[i].Equals(recruitAFriendDataResponse.TopRecruits[i]))
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

		public static RecruitAFriendDataResponse Deserialize(Stream stream, RecruitAFriendDataResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RecruitAFriendDataResponse DeserializeLengthDelimited(Stream stream)
		{
			RecruitAFriendDataResponse recruitAFriendDataResponse = new RecruitAFriendDataResponse();
			DeserializeLengthDelimited(stream, recruitAFriendDataResponse);
			return recruitAFriendDataResponse;
		}

		public static RecruitAFriendDataResponse DeserializeLengthDelimited(Stream stream, RecruitAFriendDataResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RecruitAFriendDataResponse Deserialize(Stream stream, RecruitAFriendDataResponse instance, long limit)
		{
			if (instance.TopRecruits == null)
			{
				instance.TopRecruits = new List<RecruitData>();
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
				case 8:
					instance.TotalRecruitCount = ProtocolParser.ReadUInt32(stream);
					continue;
				case 18:
					instance.TopRecruits.Add(RecruitData.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, RecruitAFriendDataResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.TotalRecruitCount);
			if (instance.TopRecruits.Count <= 0)
			{
				return;
			}
			foreach (RecruitData topRecruit in instance.TopRecruits)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, topRecruit.GetSerializedSize());
				RecruitData.Serialize(stream, topRecruit);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(TotalRecruitCount);
			if (TopRecruits.Count > 0)
			{
				foreach (RecruitData topRecruit in TopRecruits)
				{
					num++;
					uint serializedSize = topRecruit.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 1;
		}
	}
}
