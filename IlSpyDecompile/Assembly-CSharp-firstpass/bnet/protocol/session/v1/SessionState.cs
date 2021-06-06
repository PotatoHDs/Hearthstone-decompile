using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	public class SessionState : IProtoBuf
	{
		public bool HasHandle;

		private GameAccountHandle _Handle;

		public bool HasClientAddress;

		private string _ClientAddress;

		public bool HasLastTickTime;

		private ulong _LastTickTime;

		public bool HasCreateTime;

		private ulong _CreateTime;

		public bool HasParentalControlsActive;

		private bool _ParentalControlsActive;

		public bool HasLocation;

		private GameSessionLocation _Location;

		public bool HasUsingIgrAddress;

		private bool _UsingIgrAddress;

		public bool HasHasBenefactor;

		private bool _HasBenefactor;

		public bool HasIgrId;

		private IgrId _IgrId;

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

		public string ClientAddress
		{
			get
			{
				return _ClientAddress;
			}
			set
			{
				_ClientAddress = value;
				HasClientAddress = value != null;
			}
		}

		public ulong LastTickTime
		{
			get
			{
				return _LastTickTime;
			}
			set
			{
				_LastTickTime = value;
				HasLastTickTime = true;
			}
		}

		public ulong CreateTime
		{
			get
			{
				return _CreateTime;
			}
			set
			{
				_CreateTime = value;
				HasCreateTime = true;
			}
		}

		public bool ParentalControlsActive
		{
			get
			{
				return _ParentalControlsActive;
			}
			set
			{
				_ParentalControlsActive = value;
				HasParentalControlsActive = true;
			}
		}

		public GameSessionLocation Location
		{
			get
			{
				return _Location;
			}
			set
			{
				_Location = value;
				HasLocation = value != null;
			}
		}

		public bool UsingIgrAddress
		{
			get
			{
				return _UsingIgrAddress;
			}
			set
			{
				_UsingIgrAddress = value;
				HasUsingIgrAddress = true;
			}
		}

		public bool HasBenefactor
		{
			get
			{
				return _HasBenefactor;
			}
			set
			{
				_HasBenefactor = value;
				HasHasBenefactor = true;
			}
		}

		public IgrId IgrId
		{
			get
			{
				return _IgrId;
			}
			set
			{
				_IgrId = value;
				HasIgrId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetHandle(GameAccountHandle val)
		{
			Handle = val;
		}

		public void SetClientAddress(string val)
		{
			ClientAddress = val;
		}

		public void SetLastTickTime(ulong val)
		{
			LastTickTime = val;
		}

		public void SetCreateTime(ulong val)
		{
			CreateTime = val;
		}

		public void SetParentalControlsActive(bool val)
		{
			ParentalControlsActive = val;
		}

		public void SetLocation(GameSessionLocation val)
		{
			Location = val;
		}

		public void SetUsingIgrAddress(bool val)
		{
			UsingIgrAddress = val;
		}

		public void SetHasBenefactor(bool val)
		{
			HasBenefactor = val;
		}

		public void SetIgrId(IgrId val)
		{
			IgrId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasHandle)
			{
				num ^= Handle.GetHashCode();
			}
			if (HasClientAddress)
			{
				num ^= ClientAddress.GetHashCode();
			}
			if (HasLastTickTime)
			{
				num ^= LastTickTime.GetHashCode();
			}
			if (HasCreateTime)
			{
				num ^= CreateTime.GetHashCode();
			}
			if (HasParentalControlsActive)
			{
				num ^= ParentalControlsActive.GetHashCode();
			}
			if (HasLocation)
			{
				num ^= Location.GetHashCode();
			}
			if (HasUsingIgrAddress)
			{
				num ^= UsingIgrAddress.GetHashCode();
			}
			if (HasHasBenefactor)
			{
				num ^= HasBenefactor.GetHashCode();
			}
			if (HasIgrId)
			{
				num ^= IgrId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SessionState sessionState = obj as SessionState;
			if (sessionState == null)
			{
				return false;
			}
			if (HasHandle != sessionState.HasHandle || (HasHandle && !Handle.Equals(sessionState.Handle)))
			{
				return false;
			}
			if (HasClientAddress != sessionState.HasClientAddress || (HasClientAddress && !ClientAddress.Equals(sessionState.ClientAddress)))
			{
				return false;
			}
			if (HasLastTickTime != sessionState.HasLastTickTime || (HasLastTickTime && !LastTickTime.Equals(sessionState.LastTickTime)))
			{
				return false;
			}
			if (HasCreateTime != sessionState.HasCreateTime || (HasCreateTime && !CreateTime.Equals(sessionState.CreateTime)))
			{
				return false;
			}
			if (HasParentalControlsActive != sessionState.HasParentalControlsActive || (HasParentalControlsActive && !ParentalControlsActive.Equals(sessionState.ParentalControlsActive)))
			{
				return false;
			}
			if (HasLocation != sessionState.HasLocation || (HasLocation && !Location.Equals(sessionState.Location)))
			{
				return false;
			}
			if (HasUsingIgrAddress != sessionState.HasUsingIgrAddress || (HasUsingIgrAddress && !UsingIgrAddress.Equals(sessionState.UsingIgrAddress)))
			{
				return false;
			}
			if (HasHasBenefactor != sessionState.HasHasBenefactor || (HasHasBenefactor && !HasBenefactor.Equals(sessionState.HasBenefactor)))
			{
				return false;
			}
			if (HasIgrId != sessionState.HasIgrId || (HasIgrId && !IgrId.Equals(sessionState.IgrId)))
			{
				return false;
			}
			return true;
		}

		public static SessionState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SessionState>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SessionState Deserialize(Stream stream, SessionState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SessionState DeserializeLengthDelimited(Stream stream)
		{
			SessionState sessionState = new SessionState();
			DeserializeLengthDelimited(stream, sessionState);
			return sessionState;
		}

		public static SessionState DeserializeLengthDelimited(Stream stream, SessionState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SessionState Deserialize(Stream stream, SessionState instance, long limit)
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
					if (instance.Handle == null)
					{
						instance.Handle = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.Handle);
					}
					continue;
				case 18:
					instance.ClientAddress = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.LastTickTime = ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.CreateTime = ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.ParentalControlsActive = ProtocolParser.ReadBool(stream);
					continue;
				case 50:
					if (instance.Location == null)
					{
						instance.Location = GameSessionLocation.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameSessionLocation.DeserializeLengthDelimited(stream, instance.Location);
					}
					continue;
				case 56:
					instance.UsingIgrAddress = ProtocolParser.ReadBool(stream);
					continue;
				case 64:
					instance.HasBenefactor = ProtocolParser.ReadBool(stream);
					continue;
				case 74:
					if (instance.IgrId == null)
					{
						instance.IgrId = IgrId.DeserializeLengthDelimited(stream);
					}
					else
					{
						IgrId.DeserializeLengthDelimited(stream, instance.IgrId);
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

		public static void Serialize(Stream stream, SessionState instance)
		{
			if (instance.HasHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Handle.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Handle);
			}
			if (instance.HasClientAddress)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientAddress));
			}
			if (instance.HasLastTickTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.LastTickTime);
			}
			if (instance.HasCreateTime)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.CreateTime);
			}
			if (instance.HasParentalControlsActive)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.ParentalControlsActive);
			}
			if (instance.HasLocation)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.Location.GetSerializedSize());
				GameSessionLocation.Serialize(stream, instance.Location);
			}
			if (instance.HasUsingIgrAddress)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.UsingIgrAddress);
			}
			if (instance.HasHasBenefactor)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.HasBenefactor);
			}
			if (instance.HasIgrId)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteUInt32(stream, instance.IgrId.GetSerializedSize());
				IgrId.Serialize(stream, instance.IgrId);
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
			if (HasClientAddress)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ClientAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasLastTickTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(LastTickTime);
			}
			if (HasCreateTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(CreateTime);
			}
			if (HasParentalControlsActive)
			{
				num++;
				num++;
			}
			if (HasLocation)
			{
				num++;
				uint serializedSize2 = Location.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasUsingIgrAddress)
			{
				num++;
				num++;
			}
			if (HasHasBenefactor)
			{
				num++;
				num++;
			}
			if (HasIgrId)
			{
				num++;
				uint serializedSize3 = IgrId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
