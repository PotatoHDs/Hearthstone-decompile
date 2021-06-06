using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class ClientStaticAssetsResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 341
		}

		private List<AssetRecordInfo> _AssetsToGet = new List<AssetRecordInfo>();

		public List<AssetRecordInfo> AssetsToGet
		{
			get
			{
				return _AssetsToGet;
			}
			set
			{
				_AssetsToGet = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (AssetRecordInfo item in AssetsToGet)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ClientStaticAssetsResponse clientStaticAssetsResponse = obj as ClientStaticAssetsResponse;
			if (clientStaticAssetsResponse == null)
			{
				return false;
			}
			if (AssetsToGet.Count != clientStaticAssetsResponse.AssetsToGet.Count)
			{
				return false;
			}
			for (int i = 0; i < AssetsToGet.Count; i++)
			{
				if (!AssetsToGet[i].Equals(clientStaticAssetsResponse.AssetsToGet[i]))
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

		public static ClientStaticAssetsResponse Deserialize(Stream stream, ClientStaticAssetsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ClientStaticAssetsResponse DeserializeLengthDelimited(Stream stream)
		{
			ClientStaticAssetsResponse clientStaticAssetsResponse = new ClientStaticAssetsResponse();
			DeserializeLengthDelimited(stream, clientStaticAssetsResponse);
			return clientStaticAssetsResponse;
		}

		public static ClientStaticAssetsResponse DeserializeLengthDelimited(Stream stream, ClientStaticAssetsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ClientStaticAssetsResponse Deserialize(Stream stream, ClientStaticAssetsResponse instance, long limit)
		{
			if (instance.AssetsToGet == null)
			{
				instance.AssetsToGet = new List<AssetRecordInfo>();
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
					instance.AssetsToGet.Add(AssetRecordInfo.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ClientStaticAssetsResponse instance)
		{
			if (instance.AssetsToGet.Count <= 0)
			{
				return;
			}
			foreach (AssetRecordInfo item in instance.AssetsToGet)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				AssetRecordInfo.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (AssetsToGet.Count > 0)
			{
				foreach (AssetRecordInfo item in AssetsToGet)
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
