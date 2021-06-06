using System;
using System.IO;

namespace bnet.protocol.presence.v1
{
	public class OwnershipRequest : IProtoBuf
	{
		public bool HasReleaseOwnership;

		private bool _ReleaseOwnership;

		public EntityId EntityId { get; set; }

		public bool ReleaseOwnership
		{
			get
			{
				return _ReleaseOwnership;
			}
			set
			{
				_ReleaseOwnership = value;
				HasReleaseOwnership = true;
			}
		}

		public bool IsInitialized => true;

		public void SetEntityId(EntityId val)
		{
			EntityId = val;
		}

		public void SetReleaseOwnership(bool val)
		{
			ReleaseOwnership = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= EntityId.GetHashCode();
			if (HasReleaseOwnership)
			{
				hashCode ^= ReleaseOwnership.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			OwnershipRequest ownershipRequest = obj as OwnershipRequest;
			if (ownershipRequest == null)
			{
				return false;
			}
			if (!EntityId.Equals(ownershipRequest.EntityId))
			{
				return false;
			}
			if (HasReleaseOwnership != ownershipRequest.HasReleaseOwnership || (HasReleaseOwnership && !ReleaseOwnership.Equals(ownershipRequest.ReleaseOwnership)))
			{
				return false;
			}
			return true;
		}

		public static OwnershipRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<OwnershipRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static OwnershipRequest Deserialize(Stream stream, OwnershipRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static OwnershipRequest DeserializeLengthDelimited(Stream stream)
		{
			OwnershipRequest ownershipRequest = new OwnershipRequest();
			DeserializeLengthDelimited(stream, ownershipRequest);
			return ownershipRequest;
		}

		public static OwnershipRequest DeserializeLengthDelimited(Stream stream, OwnershipRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static OwnershipRequest Deserialize(Stream stream, OwnershipRequest instance, long limit)
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
					if (instance.EntityId == null)
					{
						instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
					}
					continue;
				case 16:
					instance.ReleaseOwnership = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, OwnershipRequest instance)
		{
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
			if (instance.HasReleaseOwnership)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.ReleaseOwnership);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = EntityId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasReleaseOwnership)
			{
				num++;
				num++;
			}
			return num + 1;
		}
	}
}
