using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class SelectGameAccountRequest : IProtoBuf
	{
		public EntityId GameAccountId { get; set; }

		public bool IsInitialized => true;

		public void SetGameAccountId(EntityId val)
		{
			GameAccountId = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ GameAccountId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SelectGameAccountRequest selectGameAccountRequest = obj as SelectGameAccountRequest;
			if (selectGameAccountRequest == null)
			{
				return false;
			}
			if (!GameAccountId.Equals(selectGameAccountRequest.GameAccountId))
			{
				return false;
			}
			return true;
		}

		public static SelectGameAccountRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SelectGameAccountRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SelectGameAccountRequest Deserialize(Stream stream, SelectGameAccountRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SelectGameAccountRequest DeserializeLengthDelimited(Stream stream)
		{
			SelectGameAccountRequest selectGameAccountRequest = new SelectGameAccountRequest();
			DeserializeLengthDelimited(stream, selectGameAccountRequest);
			return selectGameAccountRequest;
		}

		public static SelectGameAccountRequest DeserializeLengthDelimited(Stream stream, SelectGameAccountRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SelectGameAccountRequest Deserialize(Stream stream, SelectGameAccountRequest instance, long limit)
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
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
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

		public static void Serialize(Stream stream, SelectGameAccountRequest instance)
		{
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			EntityId.Serialize(stream, instance.GameAccountId);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = GameAccountId.GetSerializedSize();
			return 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1;
		}
	}
}
