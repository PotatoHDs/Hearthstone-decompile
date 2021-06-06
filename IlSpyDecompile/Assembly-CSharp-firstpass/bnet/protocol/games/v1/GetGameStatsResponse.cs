using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class GetGameStatsResponse : IProtoBuf
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
			GetGameStatsResponse getGameStatsResponse = obj as GetGameStatsResponse;
			if (getGameStatsResponse == null)
			{
				return false;
			}
			if (StatsBucket.Count != getGameStatsResponse.StatsBucket.Count)
			{
				return false;
			}
			for (int i = 0; i < StatsBucket.Count; i++)
			{
				if (!StatsBucket[i].Equals(getGameStatsResponse.StatsBucket[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static GetGameStatsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameStatsResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGameStatsResponse Deserialize(Stream stream, GetGameStatsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGameStatsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGameStatsResponse getGameStatsResponse = new GetGameStatsResponse();
			DeserializeLengthDelimited(stream, getGameStatsResponse);
			return getGameStatsResponse;
		}

		public static GetGameStatsResponse DeserializeLengthDelimited(Stream stream, GetGameStatsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGameStatsResponse Deserialize(Stream stream, GetGameStatsResponse instance, long limit)
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

		public static void Serialize(Stream stream, GetGameStatsResponse instance)
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
