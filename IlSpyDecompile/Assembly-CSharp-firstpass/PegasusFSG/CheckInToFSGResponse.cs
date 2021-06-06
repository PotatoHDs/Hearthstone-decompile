using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	public class CheckInToFSGResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 505
		}

		private List<FSGPatron> _FsgAttendees = new List<FSGPatron>();

		public bool HasPlayerRecord;

		private TavernBrawlPlayerRecord _PlayerRecord;

		public bool HasFsgSharedSecretKey;

		private byte[] _FsgSharedSecretKey;

		private List<int> _InnkeeperSelectedBrawlLibraryItemId = new List<int>();

		public ErrorCode ErrorCode { get; set; }

		public long FsgId { get; set; }

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

		public TavernBrawlPlayerRecord PlayerRecord
		{
			get
			{
				return _PlayerRecord;
			}
			set
			{
				_PlayerRecord = value;
				HasPlayerRecord = value != null;
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
			hashCode ^= FsgId.GetHashCode();
			foreach (FSGPatron fsgAttendee in FsgAttendees)
			{
				hashCode ^= fsgAttendee.GetHashCode();
			}
			if (HasPlayerRecord)
			{
				hashCode ^= PlayerRecord.GetHashCode();
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
			CheckInToFSGResponse checkInToFSGResponse = obj as CheckInToFSGResponse;
			if (checkInToFSGResponse == null)
			{
				return false;
			}
			if (!ErrorCode.Equals(checkInToFSGResponse.ErrorCode))
			{
				return false;
			}
			if (!FsgId.Equals(checkInToFSGResponse.FsgId))
			{
				return false;
			}
			if (FsgAttendees.Count != checkInToFSGResponse.FsgAttendees.Count)
			{
				return false;
			}
			for (int i = 0; i < FsgAttendees.Count; i++)
			{
				if (!FsgAttendees[i].Equals(checkInToFSGResponse.FsgAttendees[i]))
				{
					return false;
				}
			}
			if (HasPlayerRecord != checkInToFSGResponse.HasPlayerRecord || (HasPlayerRecord && !PlayerRecord.Equals(checkInToFSGResponse.PlayerRecord)))
			{
				return false;
			}
			if (HasFsgSharedSecretKey != checkInToFSGResponse.HasFsgSharedSecretKey || (HasFsgSharedSecretKey && !FsgSharedSecretKey.Equals(checkInToFSGResponse.FsgSharedSecretKey)))
			{
				return false;
			}
			if (InnkeeperSelectedBrawlLibraryItemId.Count != checkInToFSGResponse.InnkeeperSelectedBrawlLibraryItemId.Count)
			{
				return false;
			}
			for (int j = 0; j < InnkeeperSelectedBrawlLibraryItemId.Count; j++)
			{
				if (!InnkeeperSelectedBrawlLibraryItemId[j].Equals(checkInToFSGResponse.InnkeeperSelectedBrawlLibraryItemId[j]))
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

		public static CheckInToFSGResponse Deserialize(Stream stream, CheckInToFSGResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CheckInToFSGResponse DeserializeLengthDelimited(Stream stream)
		{
			CheckInToFSGResponse checkInToFSGResponse = new CheckInToFSGResponse();
			DeserializeLengthDelimited(stream, checkInToFSGResponse);
			return checkInToFSGResponse;
		}

		public static CheckInToFSGResponse DeserializeLengthDelimited(Stream stream, CheckInToFSGResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CheckInToFSGResponse Deserialize(Stream stream, CheckInToFSGResponse instance, long limit)
		{
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
				case 16:
					instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.FsgAttendees.Add(FSGPatron.DeserializeLengthDelimited(stream));
					continue;
				case 34:
					if (instance.PlayerRecord == null)
					{
						instance.PlayerRecord = TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream);
					}
					else
					{
						TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream, instance.PlayerRecord);
					}
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

		public static void Serialize(Stream stream, CheckInToFSGResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			if (instance.FsgAttendees.Count > 0)
			{
				foreach (FSGPatron fsgAttendee in instance.FsgAttendees)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, fsgAttendee.GetSerializedSize());
					FSGPatron.Serialize(stream, fsgAttendee);
				}
			}
			if (instance.HasPlayerRecord)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.PlayerRecord.GetSerializedSize());
				TavernBrawlPlayerRecord.Serialize(stream, instance.PlayerRecord);
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
			num += ProtocolParser.SizeOfUInt64((ulong)FsgId);
			if (FsgAttendees.Count > 0)
			{
				foreach (FSGPatron fsgAttendee in FsgAttendees)
				{
					num++;
					uint serializedSize = fsgAttendee.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasPlayerRecord)
			{
				num++;
				uint serializedSize2 = PlayerRecord.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
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
			return num + 2;
		}
	}
}
