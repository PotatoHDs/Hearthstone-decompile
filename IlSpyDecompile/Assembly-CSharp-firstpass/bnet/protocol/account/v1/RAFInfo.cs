using System.IO;

namespace bnet.protocol.account.v1
{
	public class RAFInfo : IProtoBuf
	{
		public bool HasRafInfo;

		private byte[] _RafInfo;

		public byte[] RafInfo
		{
			get
			{
				return _RafInfo;
			}
			set
			{
				_RafInfo = value;
				HasRafInfo = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetRafInfo(byte[] val)
		{
			RafInfo = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRafInfo)
			{
				num ^= RafInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RAFInfo rAFInfo = obj as RAFInfo;
			if (rAFInfo == null)
			{
				return false;
			}
			if (HasRafInfo != rAFInfo.HasRafInfo || (HasRafInfo && !RafInfo.Equals(rAFInfo.RafInfo)))
			{
				return false;
			}
			return true;
		}

		public static RAFInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RAFInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RAFInfo Deserialize(Stream stream, RAFInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RAFInfo DeserializeLengthDelimited(Stream stream)
		{
			RAFInfo rAFInfo = new RAFInfo();
			DeserializeLengthDelimited(stream, rAFInfo);
			return rAFInfo;
		}

		public static RAFInfo DeserializeLengthDelimited(Stream stream, RAFInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RAFInfo Deserialize(Stream stream, RAFInfo instance, long limit)
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
				case 10:
					instance.RafInfo = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, RAFInfo instance)
		{
			if (instance.HasRafInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.RafInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRafInfo)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(RafInfo.Length) + RafInfo.Length);
			}
			return num;
		}
	}
}
