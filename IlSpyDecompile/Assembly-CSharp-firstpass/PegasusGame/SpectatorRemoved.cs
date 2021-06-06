using System.IO;
using PegasusShared;

namespace PegasusGame
{
	public class SpectatorRemoved : IProtoBuf
	{
		public enum SpectatorRemovedReason
		{
			SPECTATOR_REMOVED_REASON_KICKED,
			SPECTATOR_REMOVED_REASON_GAMEOVER
		}

		public bool HasRemovedBy;

		private BnetId _RemovedBy;

		public int ReasonCode { get; set; }

		public BnetId RemovedBy
		{
			get
			{
				return _RemovedBy;
			}
			set
			{
				_RemovedBy = value;
				HasRemovedBy = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ReasonCode.GetHashCode();
			if (HasRemovedBy)
			{
				hashCode ^= RemovedBy.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			SpectatorRemoved spectatorRemoved = obj as SpectatorRemoved;
			if (spectatorRemoved == null)
			{
				return false;
			}
			if (!ReasonCode.Equals(spectatorRemoved.ReasonCode))
			{
				return false;
			}
			if (HasRemovedBy != spectatorRemoved.HasRemovedBy || (HasRemovedBy && !RemovedBy.Equals(spectatorRemoved.RemovedBy)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SpectatorRemoved Deserialize(Stream stream, SpectatorRemoved instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SpectatorRemoved DeserializeLengthDelimited(Stream stream)
		{
			SpectatorRemoved spectatorRemoved = new SpectatorRemoved();
			DeserializeLengthDelimited(stream, spectatorRemoved);
			return spectatorRemoved;
		}

		public static SpectatorRemoved DeserializeLengthDelimited(Stream stream, SpectatorRemoved instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SpectatorRemoved Deserialize(Stream stream, SpectatorRemoved instance, long limit)
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
					instance.ReasonCode = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.RemovedBy == null)
					{
						instance.RemovedBy = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.RemovedBy);
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

		public static void Serialize(Stream stream, SpectatorRemoved instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ReasonCode);
			if (instance.HasRemovedBy)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.RemovedBy.GetSerializedSize());
				BnetId.Serialize(stream, instance.RemovedBy);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)ReasonCode);
			if (HasRemovedBy)
			{
				num++;
				uint serializedSize = RemovedBy.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1;
		}
	}
}
