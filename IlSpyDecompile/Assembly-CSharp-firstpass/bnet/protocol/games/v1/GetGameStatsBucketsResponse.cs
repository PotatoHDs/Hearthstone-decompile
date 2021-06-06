using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class GetGameStatsBucketsResponse : IProtoBuf
	{
		private List<GameStatsBucket> _StatsBucket = new List<GameStatsBucket>();

		public List<GameStatsBucket> StatsBucket
		{
			get
			{
				return _StatsBucket;
			}
			set
			{
				_StatsBucket = value;
			}
		}

		public List<GameStatsBucket> StatsBucketList => _StatsBucket;

		public int StatsBucketCount => _StatsBucket.Count;

		public bool IsInitialized => true;

		public void AddStatsBucket(GameStatsBucket val)
		{
			_StatsBucket.Add(val);
		}

		public void ClearStatsBucket()
		{
			_StatsBucket.Clear();
		}

		public void SetStatsBucket(List<GameStatsBucket> val)
		{
			StatsBucket = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (GameStatsBucket item in StatsBucket)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGameStatsBucketsResponse getGameStatsBucketsResponse = obj as GetGameStatsBucketsResponse;
			if (getGameStatsBucketsResponse == null)
			{
				return false;
			}
			if (StatsBucket.Count != getGameStatsBucketsResponse.StatsBucket.Count)
			{
				return false;
			}
			for (int i = 0; i < StatsBucket.Count; i++)
			{
				if (!StatsBucket[i].Equals(getGameStatsBucketsResponse.StatsBucket[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static GetGameStatsBucketsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameStatsBucketsResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGameStatsBucketsResponse Deserialize(Stream stream, GetGameStatsBucketsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGameStatsBucketsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGameStatsBucketsResponse getGameStatsBucketsResponse = new GetGameStatsBucketsResponse();
			DeserializeLengthDelimited(stream, getGameStatsBucketsResponse);
			return getGameStatsBucketsResponse;
		}

		public static GetGameStatsBucketsResponse DeserializeLengthDelimited(Stream stream, GetGameStatsBucketsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGameStatsBucketsResponse Deserialize(Stream stream, GetGameStatsBucketsResponse instance, long limit)
		{
			if (instance.StatsBucket == null)
			{
				instance.StatsBucket = new List<GameStatsBucket>();
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
				case 10:
					instance.StatsBucket.Add(GameStatsBucket.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GetGameStatsBucketsResponse instance)
		{
			if (instance.StatsBucket.Count <= 0)
			{
				return;
			}
			foreach (GameStatsBucket item in instance.StatsBucket)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				GameStatsBucket.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (StatsBucket.Count > 0)
			{
				foreach (GameStatsBucket item in StatsBucket)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
