using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	public class GetSessionStateRequest : IProtoBuf
	{
		public bool HasHandle;

		private GameAccountHandle _Handle;

		public bool HasIncludeBillingDisabled;

		private bool _IncludeBillingDisabled;

		public GameAccountHandle Handle
		{
			get
			{
				return _Handle;
			}
			set
			{
				_Handle = value;
				HasHandle = value != null;
			}
		}

		public bool IncludeBillingDisabled
		{
			get
			{
				return _IncludeBillingDisabled;
			}
			set
			{
				_IncludeBillingDisabled = value;
				HasIncludeBillingDisabled = true;
			}
		}

		public bool IsInitialized => true;

		public void SetHandle(GameAccountHandle val)
		{
			Handle = val;
		}

		public void SetIncludeBillingDisabled(bool val)
		{
			IncludeBillingDisabled = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasHandle)
			{
				num ^= Handle.GetHashCode();
			}
			if (HasIncludeBillingDisabled)
			{
				num ^= IncludeBillingDisabled.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetSessionStateRequest getSessionStateRequest = obj as GetSessionStateRequest;
			if (getSessionStateRequest == null)
			{
				return false;
			}
			if (HasHandle != getSessionStateRequest.HasHandle || (HasHandle && !Handle.Equals(getSessionStateRequest.Handle)))
			{
				return false;
			}
			if (HasIncludeBillingDisabled != getSessionStateRequest.HasIncludeBillingDisabled || (HasIncludeBillingDisabled && !IncludeBillingDisabled.Equals(getSessionStateRequest.IncludeBillingDisabled)))
			{
				return false;
			}
			return true;
		}

		public static GetSessionStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetSessionStateRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetSessionStateRequest Deserialize(Stream stream, GetSessionStateRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetSessionStateRequest DeserializeLengthDelimited(Stream stream)
		{
			GetSessionStateRequest getSessionStateRequest = new GetSessionStateRequest();
			DeserializeLengthDelimited(stream, getSessionStateRequest);
			return getSessionStateRequest;
		}

		public static GetSessionStateRequest DeserializeLengthDelimited(Stream stream, GetSessionStateRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetSessionStateRequest Deserialize(Stream stream, GetSessionStateRequest instance, long limit)
		{
			instance.IncludeBillingDisabled = false;
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
					if (instance.Handle == null)
					{
						instance.Handle = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.Handle);
					}
					continue;
				case 16:
					instance.IncludeBillingDisabled = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GetSessionStateRequest instance)
		{
			if (instance.HasHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Handle.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Handle);
			}
			if (instance.HasIncludeBillingDisabled)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.IncludeBillingDisabled);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasHandle)
			{
				num++;
				uint serializedSize = Handle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasIncludeBillingDisabled)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
