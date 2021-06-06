using System;
using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeGenericRewardChest : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 20
		}

		public bool HasRewardChestByteSize;

		private uint _RewardChestByteSize;

		public bool HasRewardChestHash;

		private byte[] _RewardChestHash;

		public int RewardChestAssetId { get; set; }

		public RewardChest RewardChest { get; set; }

		public uint RewardChestByteSize
		{
			get
			{
				return _RewardChestByteSize;
			}
			set
			{
				_RewardChestByteSize = value;
				HasRewardChestByteSize = true;
			}
		}

		public byte[] RewardChestHash
		{
			get
			{
				return _RewardChestHash;
			}
			set
			{
				_RewardChestHash = value;
				HasRewardChestHash = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= RewardChestAssetId.GetHashCode();
			hashCode ^= RewardChest.GetHashCode();
			if (HasRewardChestByteSize)
			{
				hashCode ^= RewardChestByteSize.GetHashCode();
			}
			if (HasRewardChestHash)
			{
				hashCode ^= RewardChestHash.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeGenericRewardChest profileNoticeGenericRewardChest = obj as ProfileNoticeGenericRewardChest;
			if (profileNoticeGenericRewardChest == null)
			{
				return false;
			}
			if (!RewardChestAssetId.Equals(profileNoticeGenericRewardChest.RewardChestAssetId))
			{
				return false;
			}
			if (!RewardChest.Equals(profileNoticeGenericRewardChest.RewardChest))
			{
				return false;
			}
			if (HasRewardChestByteSize != profileNoticeGenericRewardChest.HasRewardChestByteSize || (HasRewardChestByteSize && !RewardChestByteSize.Equals(profileNoticeGenericRewardChest.RewardChestByteSize)))
			{
				return false;
			}
			if (HasRewardChestHash != profileNoticeGenericRewardChest.HasRewardChestHash || (HasRewardChestHash && !RewardChestHash.Equals(profileNoticeGenericRewardChest.RewardChestHash)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeGenericRewardChest Deserialize(Stream stream, ProfileNoticeGenericRewardChest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeGenericRewardChest DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeGenericRewardChest profileNoticeGenericRewardChest = new ProfileNoticeGenericRewardChest();
			DeserializeLengthDelimited(stream, profileNoticeGenericRewardChest);
			return profileNoticeGenericRewardChest;
		}

		public static ProfileNoticeGenericRewardChest DeserializeLengthDelimited(Stream stream, ProfileNoticeGenericRewardChest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeGenericRewardChest Deserialize(Stream stream, ProfileNoticeGenericRewardChest instance, long limit)
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
				case 8:
					instance.RewardChestAssetId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.RewardChest == null)
					{
						instance.RewardChest = RewardChest.DeserializeLengthDelimited(stream);
					}
					else
					{
						RewardChest.DeserializeLengthDelimited(stream, instance.RewardChest);
					}
					continue;
				case 24:
					instance.RewardChestByteSize = ProtocolParser.ReadUInt32(stream);
					continue;
				case 34:
					instance.RewardChestHash = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeGenericRewardChest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardChestAssetId);
			if (instance.RewardChest == null)
			{
				throw new ArgumentNullException("RewardChest", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.RewardChest.GetSerializedSize());
			RewardChest.Serialize(stream, instance.RewardChest);
			if (instance.HasRewardChestByteSize)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.RewardChestByteSize);
			}
			if (instance.HasRewardChestHash)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, instance.RewardChestHash);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)RewardChestAssetId);
			uint serializedSize = RewardChest.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasRewardChestByteSize)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(RewardChestByteSize);
			}
			if (HasRewardChestHash)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(RewardChestHash.Length) + RewardChestHash.Length);
			}
			return num + 2;
		}
	}
}
