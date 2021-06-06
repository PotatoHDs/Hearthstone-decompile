using System.IO;
using HSCachedDeckCompletion;

namespace PegasusUtil
{
	public class SmartDeckRequest : IProtoBuf
	{
		public enum PacketID
		{
			ID = 369,
			System = 5
		}

		public bool HasRequestMessage;

		private HSCachedDeckCompletionRequest _RequestMessage;

		public HSCachedDeckCompletionRequest RequestMessage
		{
			get
			{
				return _RequestMessage;
			}
			set
			{
				_RequestMessage = value;
				HasRequestMessage = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRequestMessage)
			{
				num ^= RequestMessage.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SmartDeckRequest smartDeckRequest = obj as SmartDeckRequest;
			if (smartDeckRequest == null)
			{
				return false;
			}
			if (HasRequestMessage != smartDeckRequest.HasRequestMessage || (HasRequestMessage && !RequestMessage.Equals(smartDeckRequest.RequestMessage)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SmartDeckRequest Deserialize(Stream stream, SmartDeckRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SmartDeckRequest DeserializeLengthDelimited(Stream stream)
		{
			SmartDeckRequest smartDeckRequest = new SmartDeckRequest();
			DeserializeLengthDelimited(stream, smartDeckRequest);
			return smartDeckRequest;
		}

		public static SmartDeckRequest DeserializeLengthDelimited(Stream stream, SmartDeckRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SmartDeckRequest Deserialize(Stream stream, SmartDeckRequest instance, long limit)
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
					if (instance.RequestMessage == null)
					{
						instance.RequestMessage = HSCachedDeckCompletionRequest.DeserializeLengthDelimited(stream);
					}
					else
					{
						HSCachedDeckCompletionRequest.DeserializeLengthDelimited(stream, instance.RequestMessage);
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

		public static void Serialize(Stream stream, SmartDeckRequest instance)
		{
			if (instance.HasRequestMessage)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestMessage.GetSerializedSize());
				HSCachedDeckCompletionRequest.Serialize(stream, instance.RequestMessage);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRequestMessage)
			{
				num++;
				uint serializedSize = RequestMessage.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
