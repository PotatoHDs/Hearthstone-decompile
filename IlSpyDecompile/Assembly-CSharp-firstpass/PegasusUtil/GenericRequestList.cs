using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class GenericRequestList : IProtoBuf
	{
		public enum PacketID
		{
			ID = 327,
			System = 0
		}

		private List<GenericRequest> _Requests = new List<GenericRequest>();

		public List<GenericRequest> Requests
		{
			get
			{
				return _Requests;
			}
			set
			{
				_Requests = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (GenericRequest request in Requests)
			{
				num ^= request.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GenericRequestList genericRequestList = obj as GenericRequestList;
			if (genericRequestList == null)
			{
				return false;
			}
			if (Requests.Count != genericRequestList.Requests.Count)
			{
				return false;
			}
			for (int i = 0; i < Requests.Count; i++)
			{
				if (!Requests[i].Equals(genericRequestList.Requests[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GenericRequestList Deserialize(Stream stream, GenericRequestList instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GenericRequestList DeserializeLengthDelimited(Stream stream)
		{
			GenericRequestList genericRequestList = new GenericRequestList();
			DeserializeLengthDelimited(stream, genericRequestList);
			return genericRequestList;
		}

		public static GenericRequestList DeserializeLengthDelimited(Stream stream, GenericRequestList instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GenericRequestList Deserialize(Stream stream, GenericRequestList instance, long limit)
		{
			if (instance.Requests == null)
			{
				instance.Requests = new List<GenericRequest>();
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
					instance.Requests.Add(GenericRequest.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GenericRequestList instance)
		{
			if (instance.Requests.Count <= 0)
			{
				return;
			}
			foreach (GenericRequest request in instance.Requests)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, request.GetSerializedSize());
				GenericRequest.Serialize(stream, request);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Requests.Count > 0)
			{
				foreach (GenericRequest request in Requests)
				{
					num++;
					uint serializedSize = request.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
