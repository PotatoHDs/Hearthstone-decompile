using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	public class GetAllValuesForAttributeResponse : IProtoBuf
	{
		private List<Variant> _AttributeValue = new List<Variant>();

		public List<Variant> AttributeValue
		{
			get
			{
				return _AttributeValue;
			}
			set
			{
				_AttributeValue = value;
			}
		}

		public List<Variant> AttributeValueList => _AttributeValue;

		public int AttributeValueCount => _AttributeValue.Count;

		public bool IsInitialized => true;

		public void AddAttributeValue(Variant val)
		{
			_AttributeValue.Add(val);
		}

		public void ClearAttributeValue()
		{
			_AttributeValue.Clear();
		}

		public void SetAttributeValue(List<Variant> val)
		{
			AttributeValue = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Variant item in AttributeValue)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetAllValuesForAttributeResponse getAllValuesForAttributeResponse = obj as GetAllValuesForAttributeResponse;
			if (getAllValuesForAttributeResponse == null)
			{
				return false;
			}
			if (AttributeValue.Count != getAllValuesForAttributeResponse.AttributeValue.Count)
			{
				return false;
			}
			for (int i = 0; i < AttributeValue.Count; i++)
			{
				if (!AttributeValue[i].Equals(getAllValuesForAttributeResponse.AttributeValue[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static GetAllValuesForAttributeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAllValuesForAttributeResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetAllValuesForAttributeResponse Deserialize(Stream stream, GetAllValuesForAttributeResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetAllValuesForAttributeResponse DeserializeLengthDelimited(Stream stream)
		{
			GetAllValuesForAttributeResponse getAllValuesForAttributeResponse = new GetAllValuesForAttributeResponse();
			DeserializeLengthDelimited(stream, getAllValuesForAttributeResponse);
			return getAllValuesForAttributeResponse;
		}

		public static GetAllValuesForAttributeResponse DeserializeLengthDelimited(Stream stream, GetAllValuesForAttributeResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetAllValuesForAttributeResponse Deserialize(Stream stream, GetAllValuesForAttributeResponse instance, long limit)
		{
			if (instance.AttributeValue == null)
			{
				instance.AttributeValue = new List<Variant>();
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
					instance.AttributeValue.Add(Variant.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GetAllValuesForAttributeResponse instance)
		{
			if (instance.AttributeValue.Count <= 0)
			{
				return;
			}
			foreach (Variant item in instance.AttributeValue)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				Variant.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (AttributeValue.Count > 0)
			{
				foreach (Variant item in AttributeValue)
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
