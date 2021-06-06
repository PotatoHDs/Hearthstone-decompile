using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.config
{
	public class ServiceAliases : IProtoBuf
	{
		private List<ProtocolAlias> _ProtocolAlias = new List<ProtocolAlias>();

		public List<ProtocolAlias> ProtocolAlias
		{
			get
			{
				return _ProtocolAlias;
			}
			set
			{
				_ProtocolAlias = value;
			}
		}

		public List<ProtocolAlias> ProtocolAliasList => _ProtocolAlias;

		public int ProtocolAliasCount => _ProtocolAlias.Count;

		public bool IsInitialized => true;

		public void AddProtocolAlias(ProtocolAlias val)
		{
			_ProtocolAlias.Add(val);
		}

		public void ClearProtocolAlias()
		{
			_ProtocolAlias.Clear();
		}

		public void SetProtocolAlias(List<ProtocolAlias> val)
		{
			ProtocolAlias = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (ProtocolAlias item in ProtocolAlias)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ServiceAliases serviceAliases = obj as ServiceAliases;
			if (serviceAliases == null)
			{
				return false;
			}
			if (ProtocolAlias.Count != serviceAliases.ProtocolAlias.Count)
			{
				return false;
			}
			for (int i = 0; i < ProtocolAlias.Count; i++)
			{
				if (!ProtocolAlias[i].Equals(serviceAliases.ProtocolAlias[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static ServiceAliases ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ServiceAliases>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ServiceAliases Deserialize(Stream stream, ServiceAliases instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ServiceAliases DeserializeLengthDelimited(Stream stream)
		{
			ServiceAliases serviceAliases = new ServiceAliases();
			DeserializeLengthDelimited(stream, serviceAliases);
			return serviceAliases;
		}

		public static ServiceAliases DeserializeLengthDelimited(Stream stream, ServiceAliases instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ServiceAliases Deserialize(Stream stream, ServiceAliases instance, long limit)
		{
			if (instance.ProtocolAlias == null)
			{
				instance.ProtocolAlias = new List<ProtocolAlias>();
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
					instance.ProtocolAlias.Add(bnet.protocol.config.ProtocolAlias.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ServiceAliases instance)
		{
			if (instance.ProtocolAlias.Count <= 0)
			{
				return;
			}
			foreach (ProtocolAlias item in instance.ProtocolAlias)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.config.ProtocolAlias.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (ProtocolAlias.Count > 0)
			{
				foreach (ProtocolAlias item in ProtocolAlias)
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
