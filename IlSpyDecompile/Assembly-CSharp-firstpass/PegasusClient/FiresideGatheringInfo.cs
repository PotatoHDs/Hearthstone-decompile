using System.IO;

namespace PegasusClient
{
	public class FiresideGatheringInfo : IProtoBuf
	{
		public bool HasFsgId;

		private long _FsgId;

		public long FsgId
		{
			get
			{
				return _FsgId;
			}
			set
			{
				_FsgId = value;
				HasFsgId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFsgId)
			{
				num ^= FsgId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FiresideGatheringInfo firesideGatheringInfo = obj as FiresideGatheringInfo;
			if (firesideGatheringInfo == null)
			{
				return false;
			}
			if (HasFsgId != firesideGatheringInfo.HasFsgId || (HasFsgId && !FsgId.Equals(firesideGatheringInfo.FsgId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FiresideGatheringInfo Deserialize(Stream stream, FiresideGatheringInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FiresideGatheringInfo DeserializeLengthDelimited(Stream stream)
		{
			FiresideGatheringInfo firesideGatheringInfo = new FiresideGatheringInfo();
			DeserializeLengthDelimited(stream, firesideGatheringInfo);
			return firesideGatheringInfo;
		}

		public static FiresideGatheringInfo DeserializeLengthDelimited(Stream stream, FiresideGatheringInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FiresideGatheringInfo Deserialize(Stream stream, FiresideGatheringInfo instance, long limit)
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
					instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, FiresideGatheringInfo instance)
		{
			if (instance.HasFsgId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasFsgId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FsgId);
			}
			return num;
		}
	}
}
