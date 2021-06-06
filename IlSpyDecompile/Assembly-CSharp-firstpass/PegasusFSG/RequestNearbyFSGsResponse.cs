using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	public class RequestNearbyFSGsResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 504
		}

		private List<FSGConfig> _FSGs = new List<FSGConfig>();

		public bool HasCheckedInFsgId;

		private long _CheckedInFsgId;

		private List<FSGPatron> _FsgAttendees = new List<FSGPatron>();

		public bool HasFsgSharedSecretKey;

		private byte[] _FsgSharedSecretKey;

		private List<int> _InnkeeperSelectedBrawlLibraryItemId = new List<int>();

		public ErrorCode ErrorCode { get; set; }

		public List<FSGConfig> FSGs
		{
			get
			{
				return _FSGs;
			}
			set
			{
				_FSGs = value;
			}
		}

		public long CheckedInFsgId
		{
			get
			{
				return _CheckedInFsgId;
			}
			set
			{
				_CheckedInFsgId = value;
				HasCheckedInFsgId = true;
			}
		}

		public List<FSGPatron> FsgAttendees
		{
			get
			{
				return _FsgAttendees;
			}
			set
			{
				_FsgAttendees = value;
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

		public List<int> InnkeeperSelectedBrawlLibraryItemId
		{
			get
			{
				return _InnkeeperSelectedBrawlLibraryItemId;
			}
			set
			{
				_InnkeeperSelectedBrawlLibraryItemId = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ErrorCode.GetHashCode();
			foreach (FSGConfig fSG in FSGs)
			{
				hashCode ^= fSG.GetHashCode();
			}
			if (HasCheckedInFsgId)
			{
				hashCode ^= CheckedInFsgId.GetHashCode();
			}
			foreach (FSGPatron fsgAttendee in FsgAttendees)
			{
				hashCode ^= fsgAttendee.GetHashCode();
			}
			if (HasFsgSharedSecretKey)
			{
				hashCode ^= FsgSharedSecretKey.GetHashCode();
			}
			foreach (int item in InnkeeperSelectedBrawlLibraryItemId)
			{
				hashCode ^= item.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			RequestNearbyFSGsResponse requestNearbyFSGsResponse = obj as RequestNearbyFSGsResponse;
			if (requestNearbyFSGsResponse == null)
			{
				return false;
			}
			if (!ErrorCode.Equals(requestNearbyFSGsResponse.ErrorCode))
			{
				return false;
			}
			if (FSGs.Count != requestNearbyFSGsResponse.FSGs.Count)
			{
				return false;
			}
			for (int i = 0; i < FSGs.Count; i++)
			{
				if (!FSGs[i].Equals(requestNearbyFSGsResponse.FSGs[i]))
				{
					return false;
				}
			}
			if (HasCheckedInFsgId != requestNearbyFSGsResponse.HasCheckedInFsgId || (HasCheckedInFsgId && !CheckedInFsgId.Equals(requestNearbyFSGsResponse.CheckedInFsgId)))
			{
				return false;
			}
			if (FsgAttendees.Count != requestNearbyFSGsResponse.FsgAttendees.Count)
			{
				return false;
			}
			for (int j = 0; j < FsgAttendees.Count; j++)
			{
				if (!FsgAttendees[j].Equals(requestNearbyFSGsResponse.FsgAttendees[j]))
				{
					return false;
				}
			}
			if (HasFsgSharedSecretKey != requestNearbyFSGsResponse.HasFsgSharedSecretKey || (HasFsgSharedSecretKey && !FsgSharedSecretKey.Equals(requestNearbyFSGsResponse.FsgSharedSecretKey)))
			{
				return false;
			}
			if (InnkeeperSelectedBrawlLibraryItemId.Count != requestNearbyFSGsResponse.InnkeeperSelectedBrawlLibraryItemId.Count)
			{
				return false;
			}
			for (int k = 0; k < InnkeeperSelectedBrawlLibraryItemId.Count; k++)
			{
				if (!InnkeeperSelectedBrawlLibraryItemId[k].Equals(requestNearbyFSGsResponse.InnkeeperSelectedBrawlLibraryItemId[k]))
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

		public static RequestNearbyFSGsResponse Deserialize(Stream stream, RequestNearbyFSGsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RequestNearbyFSGsResponse DeserializeLengthDelimited(Stream stream)
		{
			RequestNearbyFSGsResponse requestNearbyFSGsResponse = new RequestNearbyFSGsResponse();
			DeserializeLengthDelimited(stream, requestNearbyFSGsResponse);
			return requestNearbyFSGsResponse;
		}

		public static RequestNearbyFSGsResponse DeserializeLengthDelimited(Stream stream, RequestNearbyFSGsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RequestNearbyFSGsResponse Deserialize(Stream stream, RequestNearbyFSGsResponse instance, long limit)
		{
			if (instance.FSGs == null)
			{
				instance.FSGs = new List<FSGConfig>();
			}
			if (instance.FsgAttendees == null)
			{
				instance.FsgAttendees = new List<FSGPatron>();
			}
			if (instance.InnkeeperSelectedBrawlLibraryItemId == null)
			{
				instance.InnkeeperSelectedBrawlLibraryItemId = new List<int>();
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
				case 8:
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.FSGs.Add(FSGConfig.DeserializeLengthDelimited(stream));
					continue;
				case 24:
					instance.CheckedInFsgId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.FsgAttendees.Add(FSGPatron.DeserializeLengthDelimited(stream));
					continue;
				case 42:
					instance.FsgSharedSecretKey = ProtocolParser.ReadBytes(stream);
					continue;
				case 48:
					instance.InnkeeperSelectedBrawlLibraryItemId.Add((int)ProtocolParser.ReadUInt64(stream));
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

		public static void Serialize(Stream stream, RequestNearbyFSGsResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
			if (instance.FSGs.Count > 0)
			{
				foreach (FSGConfig fSG in instance.FSGs)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, fSG.GetSerializedSize());
					FSGConfig.Serialize(stream, fSG);
				}
			}
			if (instance.HasCheckedInFsgId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CheckedInFsgId);
			}
			if (instance.FsgAttendees.Count > 0)
			{
				foreach (FSGPatron fsgAttendee in instance.FsgAttendees)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, fsgAttendee.GetSerializedSize());
					FSGPatron.Serialize(stream, fsgAttendee);
				}
			}
			if (instance.HasFsgSharedSecretKey)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, instance.FsgSharedSecretKey);
			}
			if (instance.InnkeeperSelectedBrawlLibraryItemId.Count <= 0)
			{
				return;
			}
			foreach (int item in instance.InnkeeperSelectedBrawlLibraryItemId)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode);
			if (FSGs.Count > 0)
			{
				foreach (FSGConfig fSG in FSGs)
				{
					num++;
					uint serializedSize = fSG.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasCheckedInFsgId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CheckedInFsgId);
			}
			if (FsgAttendees.Count > 0)
			{
				foreach (FSGPatron fsgAttendee in FsgAttendees)
				{
					num++;
					uint serializedSize2 = fsgAttendee.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasFsgSharedSecretKey)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(FsgSharedSecretKey.Length) + FsgSharedSecretKey.Length);
			}
			if (InnkeeperSelectedBrawlLibraryItemId.Count > 0)
			{
				foreach (int item in InnkeeperSelectedBrawlLibraryItemId)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)item);
				}
			}
			return num + 1;
		}
	}
}
