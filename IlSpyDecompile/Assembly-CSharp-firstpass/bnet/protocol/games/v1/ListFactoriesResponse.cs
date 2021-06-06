using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class ListFactoriesResponse : IProtoBuf
	{
		private List<GameFactoryDescription> _Description = new List<GameFactoryDescription>();

		public bool HasTotalResults;

		private uint _TotalResults;

		public List<GameFactoryDescription> Description
		{
			get
			{
				return _Description;
			}
			set
			{
				_Description = value;
			}
		}

		public List<GameFactoryDescription> DescriptionList => _Description;

		public int DescriptionCount => _Description.Count;

		public uint TotalResults
		{
			get
			{
				return _TotalResults;
			}
			set
			{
				_TotalResults = value;
				HasTotalResults = true;
			}
		}

		public bool IsInitialized => true;

		public void AddDescription(GameFactoryDescription val)
		{
			_Description.Add(val);
		}

		public void ClearDescription()
		{
			_Description.Clear();
		}

		public void SetDescription(List<GameFactoryDescription> val)
		{
			Description = val;
		}

		public void SetTotalResults(uint val)
		{
			TotalResults = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (GameFactoryDescription item in Description)
			{
				num ^= item.GetHashCode();
			}
			if (HasTotalResults)
			{
				num ^= TotalResults.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ListFactoriesResponse listFactoriesResponse = obj as ListFactoriesResponse;
			if (listFactoriesResponse == null)
			{
				return false;
			}
			if (Description.Count != listFactoriesResponse.Description.Count)
			{
				return false;
			}
			for (int i = 0; i < Description.Count; i++)
			{
				if (!Description[i].Equals(listFactoriesResponse.Description[i]))
				{
					return false;
				}
			}
			if (HasTotalResults != listFactoriesResponse.HasTotalResults || (HasTotalResults && !TotalResults.Equals(listFactoriesResponse.TotalResults)))
			{
				return false;
			}
			return true;
		}

		public static ListFactoriesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListFactoriesResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ListFactoriesResponse Deserialize(Stream stream, ListFactoriesResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ListFactoriesResponse DeserializeLengthDelimited(Stream stream)
		{
			ListFactoriesResponse listFactoriesResponse = new ListFactoriesResponse();
			DeserializeLengthDelimited(stream, listFactoriesResponse);
			return listFactoriesResponse;
		}

		public static ListFactoriesResponse DeserializeLengthDelimited(Stream stream, ListFactoriesResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ListFactoriesResponse Deserialize(Stream stream, ListFactoriesResponse instance, long limit)
		{
			if (instance.Description == null)
			{
				instance.Description = new List<GameFactoryDescription>();
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
					instance.Description.Add(GameFactoryDescription.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.TotalResults = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, ListFactoriesResponse instance)
		{
			if (instance.Description.Count > 0)
			{
				foreach (GameFactoryDescription item in instance.Description)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					GameFactoryDescription.Serialize(stream, item);
				}
			}
			if (instance.HasTotalResults)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.TotalResults);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Description.Count > 0)
			{
				foreach (GameFactoryDescription item in Description)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasTotalResults)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(TotalResults);
			}
			return num;
		}
	}
}
