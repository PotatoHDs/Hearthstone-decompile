using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	public class UpdateParentalControlsAndCAISRequest : IProtoBuf
	{
		public bool HasAccount;

		private AccountId _Account;

		public bool HasParentalControlInfo;

		private ParentalControlInfo _ParentalControlInfo;

		public bool HasCaisId;

		private string _CaisId;

		public bool HasSessionStartTime;

		private ulong _SessionStartTime;

		public bool HasStartTime;

		private ulong _StartTime;

		public bool HasEndTime;

		private ulong _EndTime;

		public AccountId Account
		{
			get
			{
				return _Account;
			}
			set
			{
				_Account = value;
				HasAccount = value != null;
			}
		}

		public ParentalControlInfo ParentalControlInfo
		{
			get
			{
				return _ParentalControlInfo;
			}
			set
			{
				_ParentalControlInfo = value;
				HasParentalControlInfo = value != null;
			}
		}

		public string CaisId
		{
			get
			{
				return _CaisId;
			}
			set
			{
				_CaisId = value;
				HasCaisId = value != null;
			}
		}

		public ulong SessionStartTime
		{
			get
			{
				return _SessionStartTime;
			}
			set
			{
				_SessionStartTime = value;
				HasSessionStartTime = true;
			}
		}

		public ulong StartTime
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

		public ulong EndTime
		{
			get
			{
				return _EndTime;
			}
			set
			{
				_EndTime = value;
				HasEndTime = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAccount(AccountId val)
		{
			Account = val;
		}

		public void SetParentalControlInfo(ParentalControlInfo val)
		{
			ParentalControlInfo = val;
		}

		public void SetCaisId(string val)
		{
			CaisId = val;
		}

		public void SetSessionStartTime(ulong val)
		{
			SessionStartTime = val;
		}

		public void SetStartTime(ulong val)
		{
			StartTime = val;
		}

		public void SetEndTime(ulong val)
		{
			EndTime = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccount)
			{
				num ^= Account.GetHashCode();
			}
			if (HasParentalControlInfo)
			{
				num ^= ParentalControlInfo.GetHashCode();
			}
			if (HasCaisId)
			{
				num ^= CaisId.GetHashCode();
			}
			if (HasSessionStartTime)
			{
				num ^= SessionStartTime.GetHashCode();
			}
			if (HasStartTime)
			{
				num ^= StartTime.GetHashCode();
			}
			if (HasEndTime)
			{
				num ^= EndTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateParentalControlsAndCAISRequest updateParentalControlsAndCAISRequest = obj as UpdateParentalControlsAndCAISRequest;
			if (updateParentalControlsAndCAISRequest == null)
			{
				return false;
			}
			if (HasAccount != updateParentalControlsAndCAISRequest.HasAccount || (HasAccount && !Account.Equals(updateParentalControlsAndCAISRequest.Account)))
			{
				return false;
			}
			if (HasParentalControlInfo != updateParentalControlsAndCAISRequest.HasParentalControlInfo || (HasParentalControlInfo && !ParentalControlInfo.Equals(updateParentalControlsAndCAISRequest.ParentalControlInfo)))
			{
				return false;
			}
			if (HasCaisId != updateParentalControlsAndCAISRequest.HasCaisId || (HasCaisId && !CaisId.Equals(updateParentalControlsAndCAISRequest.CaisId)))
			{
				return false;
			}
			if (HasSessionStartTime != updateParentalControlsAndCAISRequest.HasSessionStartTime || (HasSessionStartTime && !SessionStartTime.Equals(updateParentalControlsAndCAISRequest.SessionStartTime)))
			{
				return false;
			}
			if (HasStartTime != updateParentalControlsAndCAISRequest.HasStartTime || (HasStartTime && !StartTime.Equals(updateParentalControlsAndCAISRequest.StartTime)))
			{
				return false;
			}
			if (HasEndTime != updateParentalControlsAndCAISRequest.HasEndTime || (HasEndTime && !EndTime.Equals(updateParentalControlsAndCAISRequest.EndTime)))
			{
				return false;
			}
			return true;
		}

		public static UpdateParentalControlsAndCAISRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateParentalControlsAndCAISRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateParentalControlsAndCAISRequest Deserialize(Stream stream, UpdateParentalControlsAndCAISRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateParentalControlsAndCAISRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateParentalControlsAndCAISRequest updateParentalControlsAndCAISRequest = new UpdateParentalControlsAndCAISRequest();
			DeserializeLengthDelimited(stream, updateParentalControlsAndCAISRequest);
			return updateParentalControlsAndCAISRequest;
		}

		public static UpdateParentalControlsAndCAISRequest DeserializeLengthDelimited(Stream stream, UpdateParentalControlsAndCAISRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateParentalControlsAndCAISRequest Deserialize(Stream stream, UpdateParentalControlsAndCAISRequest instance, long limit)
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
					if (instance.Account == null)
					{
						instance.Account = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.Account);
					}
					continue;
				case 18:
					if (instance.ParentalControlInfo == null)
					{
						instance.ParentalControlInfo = ParentalControlInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						ParentalControlInfo.DeserializeLengthDelimited(stream, instance.ParentalControlInfo);
					}
					continue;
				case 26:
					instance.CaisId = ProtocolParser.ReadString(stream);
					continue;
				case 32:
					instance.SessionStartTime = ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.StartTime = ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.EndTime = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, UpdateParentalControlsAndCAISRequest instance)
		{
			if (instance.HasAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
				AccountId.Serialize(stream, instance.Account);
			}
			if (instance.HasParentalControlInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ParentalControlInfo.GetSerializedSize());
				ParentalControlInfo.Serialize(stream, instance.ParentalControlInfo);
			}
			if (instance.HasCaisId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CaisId));
			}
			if (instance.HasSessionStartTime)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.SessionStartTime);
			}
			if (instance.HasStartTime)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.StartTime);
			}
			if (instance.HasEndTime)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.EndTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAccount)
			{
				num++;
				uint serializedSize = Account.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasParentalControlInfo)
			{
				num++;
				uint serializedSize2 = ParentalControlInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasCaisId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(CaisId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasSessionStartTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(SessionStartTime);
			}
			if (HasStartTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(StartTime);
			}
			if (HasEndTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(EndTime);
			}
			return num;
		}
	}
}
