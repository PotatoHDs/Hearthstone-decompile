using System.IO;

namespace PegasusUtil
{
	public class RerollQuestResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 607,
			System = 0
		}

		public bool HasRerolledQuestId;

		private int _RerolledQuestId;

		public bool HasGrantedQuestId;

		private int _GrantedQuestId;

		public bool HasSuccess;

		private bool _Success;

		public int RerolledQuestId
		{
			get
			{
				return _RerolledQuestId;
			}
			set
			{
				_RerolledQuestId = value;
				HasRerolledQuestId = true;
			}
		}

		public int GrantedQuestId
		{
			get
			{
				return _GrantedQuestId;
			}
			set
			{
				_GrantedQuestId = value;
				HasGrantedQuestId = true;
			}
		}

		public bool Success
		{
			get
			{
				return _Success;
			}
			set
			{
				_Success = value;
				HasSuccess = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRerolledQuestId)
			{
				num ^= RerolledQuestId.GetHashCode();
			}
			if (HasGrantedQuestId)
			{
				num ^= GrantedQuestId.GetHashCode();
			}
			if (HasSuccess)
			{
				num ^= Success.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RerollQuestResponse rerollQuestResponse = obj as RerollQuestResponse;
			if (rerollQuestResponse == null)
			{
				return false;
			}
			if (HasRerolledQuestId != rerollQuestResponse.HasRerolledQuestId || (HasRerolledQuestId && !RerolledQuestId.Equals(rerollQuestResponse.RerolledQuestId)))
			{
				return false;
			}
			if (HasGrantedQuestId != rerollQuestResponse.HasGrantedQuestId || (HasGrantedQuestId && !GrantedQuestId.Equals(rerollQuestResponse.GrantedQuestId)))
			{
				return false;
			}
			if (HasSuccess != rerollQuestResponse.HasSuccess || (HasSuccess && !Success.Equals(rerollQuestResponse.Success)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RerollQuestResponse Deserialize(Stream stream, RerollQuestResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RerollQuestResponse DeserializeLengthDelimited(Stream stream)
		{
			RerollQuestResponse rerollQuestResponse = new RerollQuestResponse();
			DeserializeLengthDelimited(stream, rerollQuestResponse);
			return rerollQuestResponse;
		}

		public static RerollQuestResponse DeserializeLengthDelimited(Stream stream, RerollQuestResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RerollQuestResponse Deserialize(Stream stream, RerollQuestResponse instance, long limit)
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
					instance.RerolledQuestId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.GrantedQuestId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Success = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, RerollQuestResponse instance)
		{
			if (instance.HasRerolledQuestId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RerolledQuestId);
			}
			if (instance.HasGrantedQuestId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GrantedQuestId);
			}
			if (instance.HasSuccess)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Success);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRerolledQuestId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RerolledQuestId);
			}
			if (HasGrantedQuestId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GrantedQuestId);
			}
			if (HasSuccess)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
