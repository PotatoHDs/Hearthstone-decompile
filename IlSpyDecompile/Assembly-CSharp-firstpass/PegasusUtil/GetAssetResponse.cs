using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class GetAssetResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 322
		}

		private List<AssetResponse> _Responses = new List<AssetResponse>();

		public List<AssetResponse> Responses
		{
			get
			{
				return _Responses;
			}
			set
			{
				_Responses = value;
			}
		}

		public int ClientToken { get; set; }

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (AssetResponse response in Responses)
			{
				num ^= response.GetHashCode();
			}
			return num ^ ClientToken.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GetAssetResponse getAssetResponse = obj as GetAssetResponse;
			if (getAssetResponse == null)
			{
				return false;
			}
			if (Responses.Count != getAssetResponse.Responses.Count)
			{
				return false;
			}
			for (int i = 0; i < Responses.Count; i++)
			{
				if (!Responses[i].Equals(getAssetResponse.Responses[i]))
				{
					return false;
				}
			}
			if (!ClientToken.Equals(getAssetResponse.ClientToken))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetAssetResponse Deserialize(Stream stream, GetAssetResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetAssetResponse DeserializeLengthDelimited(Stream stream)
		{
			GetAssetResponse getAssetResponse = new GetAssetResponse();
			DeserializeLengthDelimited(stream, getAssetResponse);
			return getAssetResponse;
		}

		public static GetAssetResponse DeserializeLengthDelimited(Stream stream, GetAssetResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetAssetResponse Deserialize(Stream stream, GetAssetResponse instance, long limit)
		{
			if (instance.Responses == null)
			{
				instance.Responses = new List<AssetResponse>();
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
					instance.Responses.Add(AssetResponse.DeserializeLengthDelimited(stream));
					continue;
				case 24:
					instance.ClientToken = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GetAssetResponse instance)
		{
			if (instance.Responses.Count > 0)
			{
				foreach (AssetResponse response in instance.Responses)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, response.GetSerializedSize());
					AssetResponse.Serialize(stream, response);
				}
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientToken);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Responses.Count > 0)
			{
				foreach (AssetResponse response in Responses)
				{
					num++;
					uint serializedSize = response.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += ProtocolParser.SizeOfUInt64((ulong)ClientToken);
			return num + 1;
		}
	}
}
