using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	public class GetAuthorizedDataResponse : IProtoBuf
	{
		private List<AuthorizedData> _Data = new List<AuthorizedData>();

		public List<AuthorizedData> Data
		{
			get
			{
				return _Data;
			}
			set
			{
				_Data = value;
			}
		}

		public List<AuthorizedData> DataList => _Data;

		public int DataCount => _Data.Count;

		public bool IsInitialized => true;

		public void AddData(AuthorizedData val)
		{
			_Data.Add(val);
		}

		public void ClearData()
		{
			_Data.Clear();
		}

		public void SetData(List<AuthorizedData> val)
		{
			Data = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (AuthorizedData datum in Data)
			{
				num ^= datum.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetAuthorizedDataResponse getAuthorizedDataResponse = obj as GetAuthorizedDataResponse;
			if (getAuthorizedDataResponse == null)
			{
				return false;
			}
			if (Data.Count != getAuthorizedDataResponse.Data.Count)
			{
				return false;
			}
			for (int i = 0; i < Data.Count; i++)
			{
				if (!Data[i].Equals(getAuthorizedDataResponse.Data[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static GetAuthorizedDataResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAuthorizedDataResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetAuthorizedDataResponse Deserialize(Stream stream, GetAuthorizedDataResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetAuthorizedDataResponse DeserializeLengthDelimited(Stream stream)
		{
			GetAuthorizedDataResponse getAuthorizedDataResponse = new GetAuthorizedDataResponse();
			DeserializeLengthDelimited(stream, getAuthorizedDataResponse);
			return getAuthorizedDataResponse;
		}

		public static GetAuthorizedDataResponse DeserializeLengthDelimited(Stream stream, GetAuthorizedDataResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetAuthorizedDataResponse Deserialize(Stream stream, GetAuthorizedDataResponse instance, long limit)
		{
			if (instance.Data == null)
			{
				instance.Data = new List<AuthorizedData>();
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
					instance.Data.Add(AuthorizedData.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GetAuthorizedDataResponse instance)
		{
			if (instance.Data.Count <= 0)
			{
				return;
			}
			foreach (AuthorizedData datum in instance.Data)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, datum.GetSerializedSize());
				AuthorizedData.Serialize(stream, datum);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Data.Count > 0)
			{
				foreach (AuthorizedData datum in Data)
				{
					num++;
					uint serializedSize = datum.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
