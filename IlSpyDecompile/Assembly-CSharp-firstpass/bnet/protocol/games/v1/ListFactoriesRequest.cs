using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class ListFactoriesRequest : IProtoBuf
	{
		public bool HasStartIndex;

		private uint _StartIndex;

		public bool HasMaxResults;

		private uint _MaxResults;

		public AttributeFilter Filter { get; set; }

		public uint StartIndex
		{
			get
			{
				return _StartIndex;
			}
			set
			{
				_StartIndex = value;
				HasStartIndex = true;
			}
		}

		public uint MaxResults
		{
			get
			{
				return _MaxResults;
			}
			set
			{
				_MaxResults = value;
				HasMaxResults = true;
			}
		}

		public bool IsInitialized => true;

		public void SetFilter(AttributeFilter val)
		{
			Filter = val;
		}

		public void SetStartIndex(uint val)
		{
			StartIndex = val;
		}

		public void SetMaxResults(uint val)
		{
			MaxResults = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Filter.GetHashCode();
			if (HasStartIndex)
			{
				hashCode ^= StartIndex.GetHashCode();
			}
			if (HasMaxResults)
			{
				hashCode ^= MaxResults.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ListFactoriesRequest listFactoriesRequest = obj as ListFactoriesRequest;
			if (listFactoriesRequest == null)
			{
				return false;
			}
			if (!Filter.Equals(listFactoriesRequest.Filter))
			{
				return false;
			}
			if (HasStartIndex != listFactoriesRequest.HasStartIndex || (HasStartIndex && !StartIndex.Equals(listFactoriesRequest.StartIndex)))
			{
				return false;
			}
			if (HasMaxResults != listFactoriesRequest.HasMaxResults || (HasMaxResults && !MaxResults.Equals(listFactoriesRequest.MaxResults)))
			{
				return false;
			}
			return true;
		}

		public static ListFactoriesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListFactoriesRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ListFactoriesRequest Deserialize(Stream stream, ListFactoriesRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ListFactoriesRequest DeserializeLengthDelimited(Stream stream)
		{
			ListFactoriesRequest listFactoriesRequest = new ListFactoriesRequest();
			DeserializeLengthDelimited(stream, listFactoriesRequest);
			return listFactoriesRequest;
		}

		public static ListFactoriesRequest DeserializeLengthDelimited(Stream stream, ListFactoriesRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ListFactoriesRequest Deserialize(Stream stream, ListFactoriesRequest instance, long limit)
		{
			instance.StartIndex = 0u;
			instance.MaxResults = 100u;
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
					if (instance.Filter == null)
					{
						instance.Filter = AttributeFilter.DeserializeLengthDelimited(stream);
					}
					else
					{
						AttributeFilter.DeserializeLengthDelimited(stream, instance.Filter);
					}
					continue;
				case 16:
					instance.StartIndex = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.MaxResults = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, ListFactoriesRequest instance)
		{
			if (instance.Filter == null)
			{
				throw new ArgumentNullException("Filter", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Filter.GetSerializedSize());
			AttributeFilter.Serialize(stream, instance.Filter);
			if (instance.HasStartIndex)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.StartIndex);
			}
			if (instance.HasMaxResults)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.MaxResults);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Filter.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasStartIndex)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(StartIndex);
			}
			if (HasMaxResults)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MaxResults);
			}
			return num + 1;
		}
	}
}
