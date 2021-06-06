using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	public class GetSessionStateByBenefactorRequest : IProtoBuf
	{
		public bool HasBenefactorHandle;

		private GameAccountHandle _BenefactorHandle;

		public bool HasIncludeBillingDisabled;

		private bool _IncludeBillingDisabled;

		public bool HasBenefactorUuid;

		private string _BenefactorUuid;

		public GameAccountHandle BenefactorHandle
		{
			get
			{
				return _BenefactorHandle;
			}
			set
			{
				_BenefactorHandle = value;
				HasBenefactorHandle = value != null;
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

		public string BenefactorUuid
		{
			get
			{
				return _BenefactorUuid;
			}
			set
			{
				_BenefactorUuid = value;
				HasBenefactorUuid = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetBenefactorHandle(GameAccountHandle val)
		{
			BenefactorHandle = val;
		}

		public void SetIncludeBillingDisabled(bool val)
		{
			IncludeBillingDisabled = val;
		}

		public void SetBenefactorUuid(string val)
		{
			BenefactorUuid = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasBenefactorHandle)
			{
				num ^= BenefactorHandle.GetHashCode();
			}
			if (HasIncludeBillingDisabled)
			{
				num ^= IncludeBillingDisabled.GetHashCode();
			}
			if (HasBenefactorUuid)
			{
				num ^= BenefactorUuid.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetSessionStateByBenefactorRequest getSessionStateByBenefactorRequest = obj as GetSessionStateByBenefactorRequest;
			if (getSessionStateByBenefactorRequest == null)
			{
				return false;
			}
			if (HasBenefactorHandle != getSessionStateByBenefactorRequest.HasBenefactorHandle || (HasBenefactorHandle && !BenefactorHandle.Equals(getSessionStateByBenefactorRequest.BenefactorHandle)))
			{
				return false;
			}
			if (HasIncludeBillingDisabled != getSessionStateByBenefactorRequest.HasIncludeBillingDisabled || (HasIncludeBillingDisabled && !IncludeBillingDisabled.Equals(getSessionStateByBenefactorRequest.IncludeBillingDisabled)))
			{
				return false;
			}
			if (HasBenefactorUuid != getSessionStateByBenefactorRequest.HasBenefactorUuid || (HasBenefactorUuid && !BenefactorUuid.Equals(getSessionStateByBenefactorRequest.BenefactorUuid)))
			{
				return false;
			}
			return true;
		}

		public static GetSessionStateByBenefactorRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetSessionStateByBenefactorRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetSessionStateByBenefactorRequest Deserialize(Stream stream, GetSessionStateByBenefactorRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetSessionStateByBenefactorRequest DeserializeLengthDelimited(Stream stream)
		{
			GetSessionStateByBenefactorRequest getSessionStateByBenefactorRequest = new GetSessionStateByBenefactorRequest();
			DeserializeLengthDelimited(stream, getSessionStateByBenefactorRequest);
			return getSessionStateByBenefactorRequest;
		}

		public static GetSessionStateByBenefactorRequest DeserializeLengthDelimited(Stream stream, GetSessionStateByBenefactorRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetSessionStateByBenefactorRequest Deserialize(Stream stream, GetSessionStateByBenefactorRequest instance, long limit)
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
					if (instance.BenefactorHandle == null)
					{
						instance.BenefactorHandle = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.BenefactorHandle);
					}
					continue;
				case 16:
					instance.IncludeBillingDisabled = ProtocolParser.ReadBool(stream);
					continue;
				case 26:
					instance.BenefactorUuid = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, GetSessionStateByBenefactorRequest instance)
		{
			if (instance.HasBenefactorHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.BenefactorHandle.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.BenefactorHandle);
			}
			if (instance.HasIncludeBillingDisabled)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.IncludeBillingDisabled);
			}
			if (instance.HasBenefactorUuid)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BenefactorUuid));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasBenefactorHandle)
			{
				num++;
				uint serializedSize = BenefactorHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasIncludeBillingDisabled)
			{
				num++;
				num++;
			}
			if (HasBenefactorUuid)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(BenefactorUuid);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
