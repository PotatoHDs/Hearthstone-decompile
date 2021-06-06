using System.IO;

namespace PegasusUtil
{
	public class SetProgressResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 296
		}

		public enum Result
		{
			SUCCESS = 1,
			FAILED,
			ALREADY_DONE
		}

		public bool HasProgress;

		private long _Progress;

		public Result Result_ { get; set; }

		public long Progress
		{
			get
			{
				return _Progress;
			}
			set
			{
				_Progress = value;
				HasProgress = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Result_.GetHashCode();
			if (HasProgress)
			{
				hashCode ^= Progress.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			SetProgressResponse setProgressResponse = obj as SetProgressResponse;
			if (setProgressResponse == null)
			{
				return false;
			}
			if (!Result_.Equals(setProgressResponse.Result_))
			{
				return false;
			}
			if (HasProgress != setProgressResponse.HasProgress || (HasProgress && !Progress.Equals(setProgressResponse.Progress)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SetProgressResponse Deserialize(Stream stream, SetProgressResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SetProgressResponse DeserializeLengthDelimited(Stream stream)
		{
			SetProgressResponse setProgressResponse = new SetProgressResponse();
			DeserializeLengthDelimited(stream, setProgressResponse);
			return setProgressResponse;
		}

		public static SetProgressResponse DeserializeLengthDelimited(Stream stream, SetProgressResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SetProgressResponse Deserialize(Stream stream, SetProgressResponse instance, long limit)
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
					instance.Result_ = (Result)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Progress = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SetProgressResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Result_);
			if (instance.HasProgress)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Progress);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Result_);
			if (HasProgress)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Progress);
			}
			return num + 1;
		}
	}
}
