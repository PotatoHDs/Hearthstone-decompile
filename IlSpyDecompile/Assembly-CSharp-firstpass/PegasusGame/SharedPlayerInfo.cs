using System;
using System.IO;
using PegasusShared;

namespace PegasusGame
{
	public class SharedPlayerInfo : IProtoBuf
	{
		public int Id { get; set; }

		public BnetId GameAccountId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Id.GetHashCode() ^ GameAccountId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SharedPlayerInfo sharedPlayerInfo = obj as SharedPlayerInfo;
			if (sharedPlayerInfo == null)
			{
				return false;
			}
			if (!Id.Equals(sharedPlayerInfo.Id))
			{
				return false;
			}
			if (!GameAccountId.Equals(sharedPlayerInfo.GameAccountId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SharedPlayerInfo Deserialize(Stream stream, SharedPlayerInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SharedPlayerInfo DeserializeLengthDelimited(Stream stream)
		{
			SharedPlayerInfo sharedPlayerInfo = new SharedPlayerInfo();
			DeserializeLengthDelimited(stream, sharedPlayerInfo);
			return sharedPlayerInfo;
		}

		public static SharedPlayerInfo DeserializeLengthDelimited(Stream stream, SharedPlayerInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SharedPlayerInfo Deserialize(Stream stream, SharedPlayerInfo instance, long limit)
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
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.GameAccountId);
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

		public static void Serialize(Stream stream, SharedPlayerInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.GameAccountId);
		}

		public uint GetSerializedSize()
		{
			uint num = 0 + ProtocolParser.SizeOfUInt64((ulong)Id);
			uint serializedSize = GameAccountId.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 2;
		}
	}
}
