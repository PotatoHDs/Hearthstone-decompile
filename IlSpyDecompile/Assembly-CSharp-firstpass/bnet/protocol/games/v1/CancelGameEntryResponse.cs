using System.IO;

namespace bnet.protocol.games.v1
{
	public class CancelGameEntryResponse : IProtoBuf
	{
		public bool HasEntireGameEntryCancelled;

		private bool _EntireGameEntryCancelled;

		public bool EntireGameEntryCancelled
		{
			get
			{
				return _EntireGameEntryCancelled;
			}
			set
			{
				_EntireGameEntryCancelled = value;
				HasEntireGameEntryCancelled = true;
			}
		}

		public bool IsInitialized => true;

		public void SetEntireGameEntryCancelled(bool val)
		{
			EntireGameEntryCancelled = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasEntireGameEntryCancelled)
			{
				num ^= EntireGameEntryCancelled.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CancelGameEntryResponse cancelGameEntryResponse = obj as CancelGameEntryResponse;
			if (cancelGameEntryResponse == null)
			{
				return false;
			}
			if (HasEntireGameEntryCancelled != cancelGameEntryResponse.HasEntireGameEntryCancelled || (HasEntireGameEntryCancelled && !EntireGameEntryCancelled.Equals(cancelGameEntryResponse.EntireGameEntryCancelled)))
			{
				return false;
			}
			return true;
		}

		public static CancelGameEntryResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CancelGameEntryResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CancelGameEntryResponse Deserialize(Stream stream, CancelGameEntryResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CancelGameEntryResponse DeserializeLengthDelimited(Stream stream)
		{
			CancelGameEntryResponse cancelGameEntryResponse = new CancelGameEntryResponse();
			DeserializeLengthDelimited(stream, cancelGameEntryResponse);
			return cancelGameEntryResponse;
		}

		public static CancelGameEntryResponse DeserializeLengthDelimited(Stream stream, CancelGameEntryResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CancelGameEntryResponse Deserialize(Stream stream, CancelGameEntryResponse instance, long limit)
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
					instance.EntireGameEntryCancelled = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, CancelGameEntryResponse instance)
		{
			if (instance.HasEntireGameEntryCancelled)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.EntireGameEntryCancelled);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasEntireGameEntryCancelled)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
