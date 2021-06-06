using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.authentication.v1
{
	public class LogonResult : IProtoBuf
	{
		public bool HasAccountId;

		private EntityId _AccountId;

		private List<EntityId> _GameAccountId = new List<EntityId>();

		public bool HasEmail;

		private string _Email;

		private List<uint> _AvailableRegion = new List<uint>();

		public bool HasConnectedRegion;

		private uint _ConnectedRegion;

		public bool HasBattleTag;

		private string _BattleTag;

		public bool HasGeoipCountry;

		private string _GeoipCountry;

		public bool HasSessionKey;

		private byte[] _SessionKey;

		public bool HasRestrictedMode;

		private bool _RestrictedMode;

		public bool HasClientId;

		private string _ClientId;

		public uint ErrorCode { get; set; }

		public EntityId AccountId
		{
			get
			{
				return _AccountId;
			}
			set
			{
				_AccountId = value;
				HasAccountId = value != null;
			}
		}

		public List<EntityId> GameAccountId
		{
			get
			{
				return _GameAccountId;
			}
			set
			{
				_GameAccountId = value;
			}
		}

		public List<EntityId> GameAccountIdList => _GameAccountId;

		public int GameAccountIdCount => _GameAccountId.Count;

		public string Email
		{
			get
			{
				return _Email;
			}
			set
			{
				_Email = value;
				HasEmail = value != null;
			}
		}

		public List<uint> AvailableRegion
		{
			get
			{
				return _AvailableRegion;
			}
			set
			{
				_AvailableRegion = value;
			}
		}

		public List<uint> AvailableRegionList => _AvailableRegion;

		public int AvailableRegionCount => _AvailableRegion.Count;

		public uint ConnectedRegion
		{
			get
			{
				return _ConnectedRegion;
			}
			set
			{
				_ConnectedRegion = value;
				HasConnectedRegion = true;
			}
		}

		public string BattleTag
		{
			get
			{
				return _BattleTag;
			}
			set
			{
				_BattleTag = value;
				HasBattleTag = value != null;
			}
		}

		public string GeoipCountry
		{
			get
			{
				return _GeoipCountry;
			}
			set
			{
				_GeoipCountry = value;
				HasGeoipCountry = value != null;
			}
		}

		public byte[] SessionKey
		{
			get
			{
				return _SessionKey;
			}
			set
			{
				_SessionKey = value;
				HasSessionKey = value != null;
			}
		}

		public bool RestrictedMode
		{
			get
			{
				return _RestrictedMode;
			}
			set
			{
				_RestrictedMode = value;
				HasRestrictedMode = true;
			}
		}

		public string ClientId
		{
			get
			{
				return _ClientId;
			}
			set
			{
				_ClientId = value;
				HasClientId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetErrorCode(uint val)
		{
			ErrorCode = val;
		}

		public void SetAccountId(EntityId val)
		{
			AccountId = val;
		}

		public void AddGameAccountId(EntityId val)
		{
			_GameAccountId.Add(val);
		}

		public void ClearGameAccountId()
		{
			_GameAccountId.Clear();
		}

		public void SetGameAccountId(List<EntityId> val)
		{
			GameAccountId = val;
		}

		public void SetEmail(string val)
		{
			Email = val;
		}

		public void AddAvailableRegion(uint val)
		{
			_AvailableRegion.Add(val);
		}

		public void ClearAvailableRegion()
		{
			_AvailableRegion.Clear();
		}

		public void SetAvailableRegion(List<uint> val)
		{
			AvailableRegion = val;
		}

		public void SetConnectedRegion(uint val)
		{
			ConnectedRegion = val;
		}

		public void SetBattleTag(string val)
		{
			BattleTag = val;
		}

		public void SetGeoipCountry(string val)
		{
			GeoipCountry = val;
		}

		public void SetSessionKey(byte[] val)
		{
			SessionKey = val;
		}

		public void SetRestrictedMode(bool val)
		{
			RestrictedMode = val;
		}

		public void SetClientId(string val)
		{
			ClientId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ErrorCode.GetHashCode();
			if (HasAccountId)
			{
				hashCode ^= AccountId.GetHashCode();
			}
			foreach (EntityId item in GameAccountId)
			{
				hashCode ^= item.GetHashCode();
			}
			if (HasEmail)
			{
				hashCode ^= Email.GetHashCode();
			}
			foreach (uint item2 in AvailableRegion)
			{
				hashCode ^= item2.GetHashCode();
			}
			if (HasConnectedRegion)
			{
				hashCode ^= ConnectedRegion.GetHashCode();
			}
			if (HasBattleTag)
			{
				hashCode ^= BattleTag.GetHashCode();
			}
			if (HasGeoipCountry)
			{
				hashCode ^= GeoipCountry.GetHashCode();
			}
			if (HasSessionKey)
			{
				hashCode ^= SessionKey.GetHashCode();
			}
			if (HasRestrictedMode)
			{
				hashCode ^= RestrictedMode.GetHashCode();
			}
			if (HasClientId)
			{
				hashCode ^= ClientId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			LogonResult logonResult = obj as LogonResult;
			if (logonResult == null)
			{
				return false;
			}
			if (!ErrorCode.Equals(logonResult.ErrorCode))
			{
				return false;
			}
			if (HasAccountId != logonResult.HasAccountId || (HasAccountId && !AccountId.Equals(logonResult.AccountId)))
			{
				return false;
			}
			if (GameAccountId.Count != logonResult.GameAccountId.Count)
			{
				return false;
			}
			for (int i = 0; i < GameAccountId.Count; i++)
			{
				if (!GameAccountId[i].Equals(logonResult.GameAccountId[i]))
				{
					return false;
				}
			}
			if (HasEmail != logonResult.HasEmail || (HasEmail && !Email.Equals(logonResult.Email)))
			{
				return false;
			}
			if (AvailableRegion.Count != logonResult.AvailableRegion.Count)
			{
				return false;
			}
			for (int j = 0; j < AvailableRegion.Count; j++)
			{
				if (!AvailableRegion[j].Equals(logonResult.AvailableRegion[j]))
				{
					return false;
				}
			}
			if (HasConnectedRegion != logonResult.HasConnectedRegion || (HasConnectedRegion && !ConnectedRegion.Equals(logonResult.ConnectedRegion)))
			{
				return false;
			}
			if (HasBattleTag != logonResult.HasBattleTag || (HasBattleTag && !BattleTag.Equals(logonResult.BattleTag)))
			{
				return false;
			}
			if (HasGeoipCountry != logonResult.HasGeoipCountry || (HasGeoipCountry && !GeoipCountry.Equals(logonResult.GeoipCountry)))
			{
				return false;
			}
			if (HasSessionKey != logonResult.HasSessionKey || (HasSessionKey && !SessionKey.Equals(logonResult.SessionKey)))
			{
				return false;
			}
			if (HasRestrictedMode != logonResult.HasRestrictedMode || (HasRestrictedMode && !RestrictedMode.Equals(logonResult.RestrictedMode)))
			{
				return false;
			}
			if (HasClientId != logonResult.HasClientId || (HasClientId && !ClientId.Equals(logonResult.ClientId)))
			{
				return false;
			}
			return true;
		}

		public static LogonResult ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<LogonResult>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static LogonResult Deserialize(Stream stream, LogonResult instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static LogonResult DeserializeLengthDelimited(Stream stream)
		{
			LogonResult logonResult = new LogonResult();
			DeserializeLengthDelimited(stream, logonResult);
			return logonResult;
		}

		public static LogonResult DeserializeLengthDelimited(Stream stream, LogonResult instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static LogonResult Deserialize(Stream stream, LogonResult instance, long limit)
		{
			if (instance.GameAccountId == null)
			{
				instance.GameAccountId = new List<EntityId>();
			}
			if (instance.AvailableRegion == null)
			{
				instance.AvailableRegion = new List<uint>();
			}
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
					instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
					continue;
				case 18:
					if (instance.AccountId == null)
					{
						instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
					}
					continue;
				case 26:
					instance.GameAccountId.Add(EntityId.DeserializeLengthDelimited(stream));
					continue;
				case 34:
					instance.Email = ProtocolParser.ReadString(stream);
					continue;
				case 40:
					instance.AvailableRegion.Add(ProtocolParser.ReadUInt32(stream));
					continue;
				case 48:
					instance.ConnectedRegion = ProtocolParser.ReadUInt32(stream);
					continue;
				case 58:
					instance.BattleTag = ProtocolParser.ReadString(stream);
					continue;
				case 66:
					instance.GeoipCountry = ProtocolParser.ReadString(stream);
					continue;
				case 74:
					instance.SessionKey = ProtocolParser.ReadBytes(stream);
					continue;
				case 80:
					instance.RestrictedMode = ProtocolParser.ReadBool(stream);
					continue;
				case 90:
					instance.ClientId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, LogonResult instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
			if (instance.HasAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.GameAccountId.Count > 0)
			{
				foreach (EntityId item in instance.GameAccountId)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					EntityId.Serialize(stream, item);
				}
			}
			if (instance.HasEmail)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Email));
			}
			if (instance.AvailableRegion.Count > 0)
			{
				foreach (uint item2 in instance.AvailableRegion)
				{
					stream.WriteByte(40);
					ProtocolParser.WriteUInt32(stream, item2);
				}
			}
			if (instance.HasConnectedRegion)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.ConnectedRegion);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasGeoipCountry)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GeoipCountry));
			}
			if (instance.HasSessionKey)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, instance.SessionKey);
			}
			if (instance.HasRestrictedMode)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.RestrictedMode);
			}
			if (instance.HasClientId)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(ErrorCode);
			if (HasAccountId)
			{
				num++;
				uint serializedSize = AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (GameAccountId.Count > 0)
			{
				foreach (EntityId item in GameAccountId)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasEmail)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Email);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (AvailableRegion.Count > 0)
			{
				foreach (uint item2 in AvailableRegion)
				{
					num++;
					num += ProtocolParser.SizeOfUInt32(item2);
				}
			}
			if (HasConnectedRegion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ConnectedRegion);
			}
			if (HasBattleTag)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasGeoipCountry)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(GeoipCountry);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasSessionKey)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(SessionKey.Length) + SessionKey.Length);
			}
			if (HasRestrictedMode)
			{
				num++;
				num++;
			}
			if (HasClientId)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(ClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num + 1;
		}
	}
}
