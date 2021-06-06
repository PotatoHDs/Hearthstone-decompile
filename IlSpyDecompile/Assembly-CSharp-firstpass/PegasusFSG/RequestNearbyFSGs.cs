using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusFSG
{
	public class RequestNearbyFSGs : IProtoBuf
	{
		public enum PacketID
		{
			ID = 501,
			System = 3
		}

		public bool HasLocation;

		private GPSCoords _Location;

		private List<string> _Bssids = new List<string>();

		public bool HasPlatform;

		private Platform _Platform;

		public GPSCoords Location
		{
			get
			{
				return _Location;
			}
			set
			{
				_Location = value;
				HasLocation = value != null;
			}
		}

		public List<string> Bssids
		{
			get
			{
				return _Bssids;
			}
			set
			{
				_Bssids = value;
			}
		}

		public Platform Platform
		{
			get
			{
				return _Platform;
			}
			set
			{
				_Platform = value;
				HasPlatform = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasLocation)
			{
				num ^= Location.GetHashCode();
			}
			foreach (string bssid in Bssids)
			{
				num ^= bssid.GetHashCode();
			}
			if (HasPlatform)
			{
				num ^= Platform.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RequestNearbyFSGs requestNearbyFSGs = obj as RequestNearbyFSGs;
			if (requestNearbyFSGs == null)
			{
				return false;
			}
			if (HasLocation != requestNearbyFSGs.HasLocation || (HasLocation && !Location.Equals(requestNearbyFSGs.Location)))
			{
				return false;
			}
			if (Bssids.Count != requestNearbyFSGs.Bssids.Count)
			{
				return false;
			}
			for (int i = 0; i < Bssids.Count; i++)
			{
				if (!Bssids[i].Equals(requestNearbyFSGs.Bssids[i]))
				{
					return false;
				}
			}
			if (HasPlatform != requestNearbyFSGs.HasPlatform || (HasPlatform && !Platform.Equals(requestNearbyFSGs.Platform)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RequestNearbyFSGs Deserialize(Stream stream, RequestNearbyFSGs instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RequestNearbyFSGs DeserializeLengthDelimited(Stream stream)
		{
			RequestNearbyFSGs requestNearbyFSGs = new RequestNearbyFSGs();
			DeserializeLengthDelimited(stream, requestNearbyFSGs);
			return requestNearbyFSGs;
		}

		public static RequestNearbyFSGs DeserializeLengthDelimited(Stream stream, RequestNearbyFSGs instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RequestNearbyFSGs Deserialize(Stream stream, RequestNearbyFSGs instance, long limit)
		{
			if (instance.Bssids == null)
			{
				instance.Bssids = new List<string>();
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
					if (instance.Location == null)
					{
						instance.Location = GPSCoords.DeserializeLengthDelimited(stream);
					}
					else
					{
						GPSCoords.DeserializeLengthDelimited(stream, instance.Location);
					}
					continue;
				case 18:
					instance.Bssids.Add(ProtocolParser.ReadString(stream));
					continue;
				case 26:
					if (instance.Platform == null)
					{
						instance.Platform = Platform.DeserializeLengthDelimited(stream);
					}
					else
					{
						Platform.DeserializeLengthDelimited(stream, instance.Platform);
					}
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

		public static void Serialize(Stream stream, RequestNearbyFSGs instance)
		{
			if (instance.HasLocation)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Location.GetSerializedSize());
				GPSCoords.Serialize(stream, instance.Location);
			}
			if (instance.Bssids.Count > 0)
			{
				foreach (string bssid in instance.Bssids)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(bssid));
				}
			}
			if (instance.HasPlatform)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
				Platform.Serialize(stream, instance.Platform);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasLocation)
			{
				num++;
				uint serializedSize = Location.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (Bssids.Count > 0)
			{
				foreach (string bssid in Bssids)
				{
					num++;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(bssid);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
			}
			if (HasPlatform)
			{
				num++;
				uint serializedSize2 = Platform.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
