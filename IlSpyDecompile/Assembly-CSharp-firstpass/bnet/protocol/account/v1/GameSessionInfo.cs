using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameSessionInfo : IProtoBuf
	{
		public bool HasStartTime;

		private uint _StartTime;

		public bool HasLocation;

		private GameSessionLocation _Location;

		public bool HasHasBenefactor;

		private bool _HasBenefactor;

		public bool HasIsUsingIgr;

		private bool _IsUsingIgr;

		public bool HasParentalControlsActive;

		private bool _ParentalControlsActive;

		public bool HasStartTimeSec;

		private ulong _StartTimeSec;

		public bool HasIgrId;

		private IgrId _IgrId;

		public uint StartTime
		{
			get
			{
				return _StartTime;
			}
			set
			{
				_StartTime = value;
				HasStartTime = true;
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

		public bool IsUsingIgr
		{
			get
			{
				return _IsUsingIgr;
			}
			set
			{
				_IsUsingIgr = value;
				HasIsUsingIgr = true;
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

		public ulong StartTimeSec
		{
			get
			{
				return _StartTimeSec;
			}
			set
			{
				_StartTimeSec = value;
				HasStartTimeSec = true;
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

		public void SetStartTime(uint val)
		{
			StartTime = val;
		}

		public void SetLocation(GameSessionLocation val)
		{
			Location = val;
		}

		public void SetHasBenefactor(bool val)
		{
			HasBenefactor = val;
		}

		public void SetIsUsingIgr(bool val)
		{
			IsUsingIgr = val;
		}

		public void SetParentalControlsActive(bool val)
		{
			ParentalControlsActive = val;
		}

		public void SetStartTimeSec(ulong val)
		{
			StartTimeSec = val;
		}

		public void SetIgrId(IgrId val)
		{
			IgrId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasStartTime)
			{
				num ^= StartTime.GetHashCode();
			}
			if (HasLocation)
			{
				num ^= Location.GetHashCode();
			}
			if (HasHasBenefactor)
			{
				num ^= HasBenefactor.GetHashCode();
			}
			if (HasIsUsingIgr)
			{
				num ^= IsUsingIgr.GetHashCode();
			}
			if (HasParentalControlsActive)
			{
				num ^= ParentalControlsActive.GetHashCode();
			}
			if (HasStartTimeSec)
			{
				num ^= StartTimeSec.GetHashCode();
			}
			if (HasIgrId)
			{
				num ^= IgrId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameSessionInfo gameSessionInfo = obj as GameSessionInfo;
			if (gameSessionInfo == null)
			{
				return false;
			}
			if (HasStartTime != gameSessionInfo.HasStartTime || (HasStartTime && !StartTime.Equals(gameSessionInfo.StartTime)))
			{
				return false;
			}
			if (HasLocation != gameSessionInfo.HasLocation || (HasLocation && !Location.Equals(gameSessionInfo.Location)))
			{
				return false;
			}
			if (HasHasBenefactor != gameSessionInfo.HasHasBenefactor || (HasHasBenefactor && !HasBenefactor.Equals(gameSessionInfo.HasBenefactor)))
			{
				return false;
			}
			if (HasIsUsingIgr != gameSessionInfo.HasIsUsingIgr || (HasIsUsingIgr && !IsUsingIgr.Equals(gameSessionInfo.IsUsingIgr)))
			{
				return false;
			}
			if (HasParentalControlsActive != gameSessionInfo.HasParentalControlsActive || (HasParentalControlsActive && !ParentalControlsActive.Equals(gameSessionInfo.ParentalControlsActive)))
			{
				return false;
			}
			if (HasStartTimeSec != gameSessionInfo.HasStartTimeSec || (HasStartTimeSec && !StartTimeSec.Equals(gameSessionInfo.StartTimeSec)))
			{
				return false;
			}
			if (HasIgrId != gameSessionInfo.HasIgrId || (HasIgrId && !IgrId.Equals(gameSessionInfo.IgrId)))
			{
				return false;
			}
			return true;
		}

		public static GameSessionInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameSessionInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameSessionInfo Deserialize(Stream stream, GameSessionInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameSessionInfo DeserializeLengthDelimited(Stream stream)
		{
			GameSessionInfo gameSessionInfo = new GameSessionInfo();
			DeserializeLengthDelimited(stream, gameSessionInfo);
			return gameSessionInfo;
		}

		public static GameSessionInfo DeserializeLengthDelimited(Stream stream, GameSessionInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameSessionInfo Deserialize(Stream stream, GameSessionInfo instance, long limit)
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
				case 24:
					instance.StartTime = ProtocolParser.ReadUInt32(stream);
					continue;
				case 34:
					if (instance.Location == null)
					{
						instance.Location = GameSessionLocation.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameSessionLocation.DeserializeLengthDelimited(stream, instance.Location);
					}
					continue;
				case 40:
					instance.HasBenefactor = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.IsUsingIgr = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.ParentalControlsActive = ProtocolParser.ReadBool(stream);
					continue;
				case 64:
					instance.StartTimeSec = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GameSessionInfo instance)
		{
			if (instance.HasStartTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.StartTime);
			}
			if (instance.HasLocation)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Location.GetSerializedSize());
				GameSessionLocation.Serialize(stream, instance.Location);
			}
			if (instance.HasHasBenefactor)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.HasBenefactor);
			}
			if (instance.HasIsUsingIgr)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.IsUsingIgr);
			}
			if (instance.HasParentalControlsActive)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.ParentalControlsActive);
			}
			if (instance.HasStartTimeSec)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, instance.StartTimeSec);
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
			if (HasStartTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(StartTime);
			}
			if (HasLocation)
			{
				num++;
				uint serializedSize = Location.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasHasBenefactor)
			{
				num++;
				num++;
			}
			if (HasIsUsingIgr)
			{
				num++;
				num++;
			}
			if (HasParentalControlsActive)
			{
				num++;
				num++;
			}
			if (HasStartTimeSec)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(StartTimeSec);
			}
			if (HasIgrId)
			{
				num++;
				uint serializedSize2 = IgrId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
