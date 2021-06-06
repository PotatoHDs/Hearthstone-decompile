using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class GetFactoryInfoResponse : IProtoBuf
	{
		private List<Attribute> _Attribute = new List<Attribute>();

		private List<GameStatsBucket> _StatsBucket = new List<GameStatsBucket>();

		public List<Attribute> Attribute
		{
			get
			{
				return _Attribute;
			}
			set
			{
				_Attribute = value;
			}
		}

		public List<Attribute> AttributeList => _Attribute;

		public int AttributeCount => _Attribute.Count;

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

		public void AddAttribute(Attribute val)
		{
			_Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			_Attribute.Clear();
		}

		public void SetAttribute(List<Attribute> val)
		{
			Attribute = val;
		}

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
			foreach (Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			foreach (GameStatsBucket item2 in StatsBucket)
			{
				num ^= item2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetFactoryInfoResponse getFactoryInfoResponse = obj as GetFactoryInfoResponse;
			if (getFactoryInfoResponse == null)
			{
				return false;
			}
			if (Attribute.Count != getFactoryInfoResponse.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(getFactoryInfoResponse.Attribute[i]))
				{
					return false;
				}
			}
			if (StatsBucket.Count != getFactoryInfoResponse.StatsBucket.Count)
			{
				return false;
			}
			for (int j = 0; j < StatsBucket.Count; j++)
			{
				if (!StatsBucket[j].Equals(getFactoryInfoResponse.StatsBucket[j]))
				{
					return false;
				}
			}
			return true;
		}

		public static GetFactoryInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFactoryInfoResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetFactoryInfoResponse Deserialize(Stream stream, GetFactoryInfoResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetFactoryInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFactoryInfoResponse getFactoryInfoResponse = new GetFactoryInfoResponse();
			DeserializeLengthDelimited(stream, getFactoryInfoResponse);
			return getFactoryInfoResponse;
		}

		public static GetFactoryInfoResponse DeserializeLengthDelimited(Stream stream, GetFactoryInfoResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetFactoryInfoResponse Deserialize(Stream stream, GetFactoryInfoResponse instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
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
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 18:
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

		public static void Serialize(Stream stream, GetFactoryInfoResponse instance)
		{
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute item in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, item);
				}
			}
			if (instance.StatsBucket.Count <= 0)
			{
				return;
			}
			foreach (GameStatsBucket item2 in instance.StatsBucket)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
				GameStatsBucket.Serialize(stream, item2);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (StatsBucket.Count > 0)
			{
				foreach (GameStatsBucket item2 in StatsBucket)
				{
					num++;
					uint serializedSize2 = item2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
