using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class GetAssetRequest : IProtoBuf
	{
		public enum PacketID
		{
			ID = 321,
			System = 0
		}

		private List<AssetKey> _Requests = new List<AssetKey>();

		public bool HasFsgId;

		private long _FsgId;

		public bool HasFsgSharedSecretKey;

		private byte[] _FsgSharedSecretKey;

		public List<AssetKey> Requests
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

		public int ClientToken { get; set; }

		public long FsgId
		{
			get
			{
				return _FsgId;
			}
			set
			{
				_FsgId = value;
				HasFsgId = true;
			}
		}

		public byte[] FsgSharedSecretKey
		{
			get
			{
				return _FsgSharedSecretKey;
			}
			set
			{
				_FsgSharedSecretKey = value;
				HasFsgSharedSecretKey = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (AssetKey request in Requests)
			{
				num ^= request.GetHashCode();
			}
			num ^= ClientToken.GetHashCode();
			if (HasFsgId)
			{
				num ^= FsgId.GetHashCode();
			}
			if (HasFsgSharedSecretKey)
			{
				num ^= FsgSharedSecretKey.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetAssetRequest getAssetRequest = obj as GetAssetRequest;
			if (getAssetRequest == null)
			{
				return false;
			}
			if (Requests.Count != getAssetRequest.Requests.Count)
			{
				return false;
			}
			for (int i = 0; i < Requests.Count; i++)
			{
				if (!Requests[i].Equals(getAssetRequest.Requests[i]))
				{
					return false;
				}
			}
			if (!ClientToken.Equals(getAssetRequest.ClientToken))
			{
				return false;
			}
			if (HasFsgId != getAssetRequest.HasFsgId || (HasFsgId && !FsgId.Equals(getAssetRequest.FsgId)))
			{
				return false;
			}
			if (HasFsgSharedSecretKey != getAssetRequest.HasFsgSharedSecretKey || (HasFsgSharedSecretKey && !FsgSharedSecretKey.Equals(getAssetRequest.FsgSharedSecretKey)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetAssetRequest Deserialize(Stream stream, GetAssetRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetAssetRequest DeserializeLengthDelimited(Stream stream)
		{
			GetAssetRequest getAssetRequest = new GetAssetRequest();
			DeserializeLengthDelimited(stream, getAssetRequest);
			return getAssetRequest;
		}

		public static GetAssetRequest DeserializeLengthDelimited(Stream stream, GetAssetRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetAssetRequest Deserialize(Stream stream, GetAssetRequest instance, long limit)
		{
			if (instance.Requests == null)
			{
				instance.Requests = new List<AssetKey>();
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
					instance.Requests.Add(AssetKey.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.ClientToken = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 100u:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 101u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.FsgSharedSecretKey = ProtocolParser.ReadBytes(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
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

		public static void Serialize(Stream stream, GetAssetRequest instance)
		{
			if (instance.Requests.Count > 0)
			{
				foreach (AssetKey request in instance.Requests)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, request.GetSerializedSize());
					AssetKey.Serialize(stream, request);
				}
			}
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientToken);
			if (instance.HasFsgId)
			{
				stream.WriteByte(160);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			}
			if (instance.HasFsgSharedSecretKey)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, instance.FsgSharedSecretKey);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Requests.Count > 0)
			{
				foreach (AssetKey request in Requests)
				{
					num++;
					uint serializedSize = request.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += ProtocolParser.SizeOfUInt64((ulong)ClientToken);
			if (HasFsgId)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)FsgId);
			}
			if (HasFsgSharedSecretKey)
			{
				num += 2;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(FsgSharedSecretKey.Length) + FsgSharedSecretKey.Length);
			}
			return num + 1;
		}
	}
}
