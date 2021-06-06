using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.connection.v1
{
	public class BindResponse : IProtoBuf
	{
		private List<uint> _ImportedServiceId = new List<uint>();

		public List<uint> ImportedServiceId
		{
			get
			{
				return _ImportedServiceId;
			}
			set
			{
				_ImportedServiceId = value;
			}
		}

		public List<uint> ImportedServiceIdList => _ImportedServiceId;

		public int ImportedServiceIdCount => _ImportedServiceId.Count;

		public bool IsInitialized => true;

		public void AddImportedServiceId(uint val)
		{
			_ImportedServiceId.Add(val);
		}

		public void ClearImportedServiceId()
		{
			_ImportedServiceId.Clear();
		}

		public void SetImportedServiceId(List<uint> val)
		{
			ImportedServiceId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (uint item in ImportedServiceId)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BindResponse bindResponse = obj as BindResponse;
			if (bindResponse == null)
			{
				return false;
			}
			if (ImportedServiceId.Count != bindResponse.ImportedServiceId.Count)
			{
				return false;
			}
			for (int i = 0; i < ImportedServiceId.Count; i++)
			{
				if (!ImportedServiceId[i].Equals(bindResponse.ImportedServiceId[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static BindResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BindResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BindResponse Deserialize(Stream stream, BindResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BindResponse DeserializeLengthDelimited(Stream stream)
		{
			BindResponse bindResponse = new BindResponse();
			DeserializeLengthDelimited(stream, bindResponse);
			return bindResponse;
		}

		public static BindResponse DeserializeLengthDelimited(Stream stream, BindResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BindResponse Deserialize(Stream stream, BindResponse instance, long limit)
		{
			if (instance.ImportedServiceId == null)
			{
				instance.ImportedServiceId = new List<uint>();
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
				{
					long num2 = ProtocolParser.ReadUInt32(stream);
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.ImportedServiceId.Add(ProtocolParser.ReadUInt32(stream));
					}
					if (stream.Position == num2)
					{
						continue;
					}
					throw new ProtocolBufferException("Read too many bytes in packed data");
				}
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

		public static void Serialize(Stream stream, BindResponse instance)
		{
			if (instance.ImportedServiceId.Count <= 0)
			{
				return;
			}
			stream.WriteByte(10);
			uint num = 0u;
			foreach (uint item in instance.ImportedServiceId)
			{
				num += ProtocolParser.SizeOfUInt32(item);
			}
			ProtocolParser.WriteUInt32(stream, num);
			foreach (uint item2 in instance.ImportedServiceId)
			{
				ProtocolParser.WriteUInt32(stream, item2);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (ImportedServiceId.Count > 0)
			{
				num++;
				uint num2 = num;
				foreach (uint item in ImportedServiceId)
				{
					num += ProtocolParser.SizeOfUInt32(item);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			return num;
		}
	}
}
