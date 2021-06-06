using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace SpectatorProto
{
	public class JoinInfo : IProtoBuf
	{
		public bool HasServerIpAddress;

		private string _ServerIpAddress;

		public bool HasServerPort;

		private uint _ServerPort;

		public bool HasGameHandle;

		private int _GameHandle;

		public bool HasSecretKey;

		private string _SecretKey;

		public bool HasIsJoinable;

		private bool _IsJoinable;

		public bool HasCurrentNumSpectators;

		private int _CurrentNumSpectators;

		public bool HasMaxNumSpectators;

		private int _MaxNumSpectators;

		public bool HasGameType;

		private GameType _GameType;

		public bool HasFormatType;

		private FormatType _FormatType;

		public bool HasMissionId;

		private int _MissionId;

		public bool HasBrawlLibraryItemId;

		private int _BrawlLibraryItemId;

		private List<BnetId> _SpectatedPlayers = new List<BnetId>();

		public bool HasPartyId;

		private BnetId _PartyId;

		public string ServerIpAddress
		{
			get
			{
				return _ServerIpAddress;
			}
			set
			{
				_ServerIpAddress = value;
				HasServerIpAddress = value != null;
			}
		}

		public uint ServerPort
		{
			get
			{
				return _ServerPort;
			}
			set
			{
				_ServerPort = value;
				HasServerPort = true;
			}
		}

		public int GameHandle
		{
			get
			{
				return _GameHandle;
			}
			set
			{
				_GameHandle = value;
				HasGameHandle = true;
			}
		}

		public string SecretKey
		{
			get
			{
				return _SecretKey;
			}
			set
			{
				_SecretKey = value;
				HasSecretKey = value != null;
			}
		}

		public bool IsJoinable
		{
			get
			{
				return _IsJoinable;
			}
			set
			{
				_IsJoinable = value;
				HasIsJoinable = true;
			}
		}

		public int CurrentNumSpectators
		{
			get
			{
				return _CurrentNumSpectators;
			}
			set
			{
				_CurrentNumSpectators = value;
				HasCurrentNumSpectators = true;
			}
		}

		public int MaxNumSpectators
		{
			get
			{
				return _MaxNumSpectators;
			}
			set
			{
				_MaxNumSpectators = value;
				HasMaxNumSpectators = true;
			}
		}

		public GameType GameType
		{
			get
			{
				return _GameType;
			}
			set
			{
				_GameType = value;
				HasGameType = true;
			}
		}

		public FormatType FormatType
		{
			get
			{
				return _FormatType;
			}
			set
			{
				_FormatType = value;
				HasFormatType = true;
			}
		}

		public int MissionId
		{
			get
			{
				return _MissionId;
			}
			set
			{
				_MissionId = value;
				HasMissionId = true;
			}
		}

		public int BrawlLibraryItemId
		{
			get
			{
				return _BrawlLibraryItemId;
			}
			set
			{
				_BrawlLibraryItemId = value;
				HasBrawlLibraryItemId = true;
			}
		}

		public List<BnetId> SpectatedPlayers
		{
			get
			{
				return _SpectatedPlayers;
			}
			set
			{
				_SpectatedPlayers = value;
			}
		}

		public BnetId PartyId
		{
			get
			{
				return _PartyId;
			}
			set
			{
				_PartyId = value;
				HasPartyId = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasServerIpAddress)
			{
				num ^= ServerIpAddress.GetHashCode();
			}
			if (HasServerPort)
			{
				num ^= ServerPort.GetHashCode();
			}
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			if (HasSecretKey)
			{
				num ^= SecretKey.GetHashCode();
			}
			if (HasIsJoinable)
			{
				num ^= IsJoinable.GetHashCode();
			}
			if (HasCurrentNumSpectators)
			{
				num ^= CurrentNumSpectators.GetHashCode();
			}
			if (HasMaxNumSpectators)
			{
				num ^= MaxNumSpectators.GetHashCode();
			}
			if (HasGameType)
			{
				num ^= GameType.GetHashCode();
			}
			if (HasFormatType)
			{
				num ^= FormatType.GetHashCode();
			}
			if (HasMissionId)
			{
				num ^= MissionId.GetHashCode();
			}
			if (HasBrawlLibraryItemId)
			{
				num ^= BrawlLibraryItemId.GetHashCode();
			}
			foreach (BnetId spectatedPlayer in SpectatedPlayers)
			{
				num ^= spectatedPlayer.GetHashCode();
			}
			if (HasPartyId)
			{
				num ^= PartyId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			JoinInfo joinInfo = obj as JoinInfo;
			if (joinInfo == null)
			{
				return false;
			}
			if (HasServerIpAddress != joinInfo.HasServerIpAddress || (HasServerIpAddress && !ServerIpAddress.Equals(joinInfo.ServerIpAddress)))
			{
				return false;
			}
			if (HasServerPort != joinInfo.HasServerPort || (HasServerPort && !ServerPort.Equals(joinInfo.ServerPort)))
			{
				return false;
			}
			if (HasGameHandle != joinInfo.HasGameHandle || (HasGameHandle && !GameHandle.Equals(joinInfo.GameHandle)))
			{
				return false;
			}
			if (HasSecretKey != joinInfo.HasSecretKey || (HasSecretKey && !SecretKey.Equals(joinInfo.SecretKey)))
			{
				return false;
			}
			if (HasIsJoinable != joinInfo.HasIsJoinable || (HasIsJoinable && !IsJoinable.Equals(joinInfo.IsJoinable)))
			{
				return false;
			}
			if (HasCurrentNumSpectators != joinInfo.HasCurrentNumSpectators || (HasCurrentNumSpectators && !CurrentNumSpectators.Equals(joinInfo.CurrentNumSpectators)))
			{
				return false;
			}
			if (HasMaxNumSpectators != joinInfo.HasMaxNumSpectators || (HasMaxNumSpectators && !MaxNumSpectators.Equals(joinInfo.MaxNumSpectators)))
			{
				return false;
			}
			if (HasGameType != joinInfo.HasGameType || (HasGameType && !GameType.Equals(joinInfo.GameType)))
			{
				return false;
			}
			if (HasFormatType != joinInfo.HasFormatType || (HasFormatType && !FormatType.Equals(joinInfo.FormatType)))
			{
				return false;
			}
			if (HasMissionId != joinInfo.HasMissionId || (HasMissionId && !MissionId.Equals(joinInfo.MissionId)))
			{
				return false;
			}
			if (HasBrawlLibraryItemId != joinInfo.HasBrawlLibraryItemId || (HasBrawlLibraryItemId && !BrawlLibraryItemId.Equals(joinInfo.BrawlLibraryItemId)))
			{
				return false;
			}
			if (SpectatedPlayers.Count != joinInfo.SpectatedPlayers.Count)
			{
				return false;
			}
			for (int i = 0; i < SpectatedPlayers.Count; i++)
			{
				if (!SpectatedPlayers[i].Equals(joinInfo.SpectatedPlayers[i]))
				{
					return false;
				}
			}
			if (HasPartyId != joinInfo.HasPartyId || (HasPartyId && !PartyId.Equals(joinInfo.PartyId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static JoinInfo Deserialize(Stream stream, JoinInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static JoinInfo DeserializeLengthDelimited(Stream stream)
		{
			JoinInfo joinInfo = new JoinInfo();
			DeserializeLengthDelimited(stream, joinInfo);
			return joinInfo;
		}

		public static JoinInfo DeserializeLengthDelimited(Stream stream, JoinInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static JoinInfo Deserialize(Stream stream, JoinInfo instance, long limit)
		{
			instance.GameType = GameType.GT_UNKNOWN;
			instance.FormatType = FormatType.FT_UNKNOWN;
			if (instance.SpectatedPlayers == null)
			{
				instance.SpectatedPlayers = new List<BnetId>();
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
				case 10:
					instance.ServerIpAddress = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.ServerPort = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.GameHandle = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.SecretKey = ProtocolParser.ReadString(stream);
					continue;
				case 40:
					instance.IsJoinable = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.CurrentNumSpectators = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.MaxNumSpectators = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 96:
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.MissionId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 104:
					instance.BrawlLibraryItemId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 82:
					instance.SpectatedPlayers.Add(BnetId.DeserializeLengthDelimited(stream));
					continue;
				case 90:
					if (instance.PartyId == null)
					{
						instance.PartyId = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.PartyId);
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

		public static void Serialize(Stream stream, JoinInfo instance)
		{
			if (instance.HasServerIpAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ServerIpAddress));
			}
			if (instance.HasServerPort)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.ServerPort);
			}
			if (instance.HasGameHandle)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameHandle);
			}
			if (instance.HasSecretKey)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SecretKey));
			}
			if (instance.HasIsJoinable)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.IsJoinable);
			}
			if (instance.HasCurrentNumSpectators)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrentNumSpectators);
			}
			if (instance.HasMaxNumSpectators)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxNumSpectators);
			}
			if (instance.HasGameType)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameType);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			}
			if (instance.HasMissionId)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MissionId);
			}
			if (instance.HasBrawlLibraryItemId)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BrawlLibraryItemId);
			}
			if (instance.SpectatedPlayers.Count > 0)
			{
				foreach (BnetId spectatedPlayer in instance.SpectatedPlayers)
				{
					stream.WriteByte(82);
					ProtocolParser.WriteUInt32(stream, spectatedPlayer.GetSerializedSize());
					BnetId.Serialize(stream, spectatedPlayer);
				}
			}
			if (instance.HasPartyId)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.PartyId.GetSerializedSize());
				BnetId.Serialize(stream, instance.PartyId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasServerIpAddress)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ServerIpAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasServerPort)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ServerPort);
			}
			if (HasGameHandle)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GameHandle);
			}
			if (HasSecretKey)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(SecretKey);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasIsJoinable)
			{
				num++;
				num++;
			}
			if (HasCurrentNumSpectators)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CurrentNumSpectators);
			}
			if (HasMaxNumSpectators)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MaxNumSpectators);
			}
			if (HasGameType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GameType);
			}
			if (HasFormatType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FormatType);
			}
			if (HasMissionId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MissionId);
			}
			if (HasBrawlLibraryItemId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BrawlLibraryItemId);
			}
			if (SpectatedPlayers.Count > 0)
			{
				foreach (BnetId spectatedPlayer in SpectatedPlayers)
				{
					num++;
					uint serializedSize = spectatedPlayer.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasPartyId)
			{
				num++;
				uint serializedSize2 = PartyId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
