using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class CollectionLeftRightClick : IProtoBuf
	{
		public enum Target
		{
			CARD = 1,
			HERO_SKIN,
			CARD_BACK
		}

		public bool HasTarget_;

		private Target _Target_;

		public Target Target_
		{
			get
			{
				return _Target_;
			}
			set
			{
				_Target_ = value;
				HasTarget_ = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTarget_)
			{
				num ^= Target_.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CollectionLeftRightClick collectionLeftRightClick = obj as CollectionLeftRightClick;
			if (collectionLeftRightClick == null)
			{
				return false;
			}
			if (HasTarget_ != collectionLeftRightClick.HasTarget_ || (HasTarget_ && !Target_.Equals(collectionLeftRightClick.Target_)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CollectionLeftRightClick Deserialize(Stream stream, CollectionLeftRightClick instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CollectionLeftRightClick DeserializeLengthDelimited(Stream stream)
		{
			CollectionLeftRightClick collectionLeftRightClick = new CollectionLeftRightClick();
			DeserializeLengthDelimited(stream, collectionLeftRightClick);
			return collectionLeftRightClick;
		}

		public static CollectionLeftRightClick DeserializeLengthDelimited(Stream stream, CollectionLeftRightClick instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CollectionLeftRightClick Deserialize(Stream stream, CollectionLeftRightClick instance, long limit)
		{
			instance.Target_ = Target.CARD;
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
					instance.Target_ = (Target)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, CollectionLeftRightClick instance)
		{
			if (instance.HasTarget_)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Target_);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTarget_)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Target_);
			}
			return num;
		}
	}
}
