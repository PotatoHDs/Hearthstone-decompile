using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	public class FSGConfig : IProtoBuf
	{
		public bool HasIsInnkeeper;

		private bool _IsInnkeeper;

		public bool HasIsSetupComplete;

		private bool _IsSetupComplete;

		public bool HasIsLargeScaleFsg;

		private bool _IsLargeScaleFsg;

		public bool HasFsgName;

		private string _FsgName;

		public bool HasTavernId;

		private long _TavernId;

		public long FsgId { get; set; }

		public long UnixStartTimeWithSlush { get; set; }

		public long UnixEndTimeWithSlush { get; set; }

		public string TavernName { get; set; }

		public TavernSignData SignData { get; set; }

		public long UnixOfficialStartTime { get; set; }

		public long UnixOfficialEndTime { get; set; }

		public long PatronCount { get; set; }

		public bool IsInnkeeper
		{
			get
			{
				return _IsInnkeeper;
			}
			set
			{
				_IsInnkeeper = value;
				HasIsInnkeeper = true;
			}
		}

		public bool IsSetupComplete
		{
			get
			{
				return _IsSetupComplete;
			}
			set
			{
				_IsSetupComplete = value;
				HasIsSetupComplete = true;
			}
		}

		public BnetId FsgInnkeeperAccountId { get; set; }

		public bool IsLargeScaleFsg
		{
			get
			{
				return _IsLargeScaleFsg;
			}
			set
			{
				_IsLargeScaleFsg = value;
				HasIsLargeScaleFsg = true;
			}
		}

		public string FsgName
		{
			get
			{
				return _FsgName;
			}
			set
			{
				_FsgName = value;
				HasFsgName = value != null;
			}
		}

		public long TavernId
		{
			get
			{
				return _TavernId;
			}
			set
			{
				_TavernId = value;
				HasTavernId = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= FsgId.GetHashCode();
			hashCode ^= UnixStartTimeWithSlush.GetHashCode();
			hashCode ^= UnixEndTimeWithSlush.GetHashCode();
			hashCode ^= TavernName.GetHashCode();
			hashCode ^= SignData.GetHashCode();
			hashCode ^= UnixOfficialStartTime.GetHashCode();
			hashCode ^= UnixOfficialEndTime.GetHashCode();
			hashCode ^= PatronCount.GetHashCode();
			if (HasIsInnkeeper)
			{
				hashCode ^= IsInnkeeper.GetHashCode();
			}
			if (HasIsSetupComplete)
			{
				hashCode ^= IsSetupComplete.GetHashCode();
			}
			hashCode ^= FsgInnkeeperAccountId.GetHashCode();
			if (HasIsLargeScaleFsg)
			{
				hashCode ^= IsLargeScaleFsg.GetHashCode();
			}
			if (HasFsgName)
			{
				hashCode ^= FsgName.GetHashCode();
			}
			if (HasTavernId)
			{
				hashCode ^= TavernId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			FSGConfig fSGConfig = obj as FSGConfig;
			if (fSGConfig == null)
			{
				return false;
			}
			if (!FsgId.Equals(fSGConfig.FsgId))
			{
				return false;
			}
			if (!UnixStartTimeWithSlush.Equals(fSGConfig.UnixStartTimeWithSlush))
			{
				return false;
			}
			if (!UnixEndTimeWithSlush.Equals(fSGConfig.UnixEndTimeWithSlush))
			{
				return false;
			}
			if (!TavernName.Equals(fSGConfig.TavernName))
			{
				return false;
			}
			if (!SignData.Equals(fSGConfig.SignData))
			{
				return false;
			}
			if (!UnixOfficialStartTime.Equals(fSGConfig.UnixOfficialStartTime))
			{
				return false;
			}
			if (!UnixOfficialEndTime.Equals(fSGConfig.UnixOfficialEndTime))
			{
				return false;
			}
			if (!PatronCount.Equals(fSGConfig.PatronCount))
			{
				return false;
			}
			if (HasIsInnkeeper != fSGConfig.HasIsInnkeeper || (HasIsInnkeeper && !IsInnkeeper.Equals(fSGConfig.IsInnkeeper)))
			{
				return false;
			}
			if (HasIsSetupComplete != fSGConfig.HasIsSetupComplete || (HasIsSetupComplete && !IsSetupComplete.Equals(fSGConfig.IsSetupComplete)))
			{
				return false;
			}
			if (!FsgInnkeeperAccountId.Equals(fSGConfig.FsgInnkeeperAccountId))
			{
				return false;
			}
			if (HasIsLargeScaleFsg != fSGConfig.HasIsLargeScaleFsg || (HasIsLargeScaleFsg && !IsLargeScaleFsg.Equals(fSGConfig.IsLargeScaleFsg)))
			{
				return false;
			}
			if (HasFsgName != fSGConfig.HasFsgName || (HasFsgName && !FsgName.Equals(fSGConfig.FsgName)))
			{
				return false;
			}
			if (HasTavernId != fSGConfig.HasTavernId || (HasTavernId && !TavernId.Equals(fSGConfig.TavernId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FSGConfig Deserialize(Stream stream, FSGConfig instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FSGConfig DeserializeLengthDelimited(Stream stream)
		{
			FSGConfig fSGConfig = new FSGConfig();
			DeserializeLengthDelimited(stream, fSGConfig);
			return fSGConfig;
		}

		public static FSGConfig DeserializeLengthDelimited(Stream stream, FSGConfig instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FSGConfig Deserialize(Stream stream, FSGConfig instance, long limit)
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
					instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.UnixStartTimeWithSlush = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.UnixEndTimeWithSlush = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.TavernName = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					if (instance.SignData == null)
					{
						instance.SignData = TavernSignData.DeserializeLengthDelimited(stream);
					}
					else
					{
						TavernSignData.DeserializeLengthDelimited(stream, instance.SignData);
					}
					continue;
				case 48:
					instance.UnixOfficialStartTime = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 80:
					instance.UnixOfficialEndTime = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.PatronCount = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.IsInnkeeper = ProtocolParser.ReadBool(stream);
					continue;
				case 72:
					instance.IsSetupComplete = ProtocolParser.ReadBool(stream);
					continue;
				case 90:
					if (instance.FsgInnkeeperAccountId == null)
					{
						instance.FsgInnkeeperAccountId = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.FsgInnkeeperAccountId);
					}
					continue;
				case 96:
					instance.IsLargeScaleFsg = ProtocolParser.ReadBool(stream);
					continue;
				case 106:
					instance.FsgName = ProtocolParser.ReadString(stream);
					continue;
				case 112:
					instance.TavernId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, FSGConfig instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.UnixStartTimeWithSlush);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.UnixEndTimeWithSlush);
			if (instance.TavernName == null)
			{
				throw new ArgumentNullException("TavernName", "Required by proto specification.");
			}
			stream.WriteByte(34);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TavernName));
			if (instance.SignData == null)
			{
				throw new ArgumentNullException("SignData", "Required by proto specification.");
			}
			stream.WriteByte(42);
			ProtocolParser.WriteUInt32(stream, instance.SignData.GetSerializedSize());
			TavernSignData.Serialize(stream, instance.SignData);
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.UnixOfficialStartTime);
			stream.WriteByte(80);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.UnixOfficialEndTime);
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PatronCount);
			if (instance.HasIsInnkeeper)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.IsInnkeeper);
			}
			if (instance.HasIsSetupComplete)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.IsSetupComplete);
			}
			if (instance.FsgInnkeeperAccountId == null)
			{
				throw new ArgumentNullException("FsgInnkeeperAccountId", "Required by proto specification.");
			}
			stream.WriteByte(90);
			ProtocolParser.WriteUInt32(stream, instance.FsgInnkeeperAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.FsgInnkeeperAccountId);
			if (instance.HasIsLargeScaleFsg)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteBool(stream, instance.IsLargeScaleFsg);
			}
			if (instance.HasFsgName)
			{
				stream.WriteByte(106);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FsgName));
			}
			if (instance.HasTavernId)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TavernId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)FsgId);
			num += ProtocolParser.SizeOfUInt64((ulong)UnixStartTimeWithSlush);
			num += ProtocolParser.SizeOfUInt64((ulong)UnixEndTimeWithSlush);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(TavernName);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint serializedSize = SignData.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt64((ulong)UnixOfficialStartTime);
			num += ProtocolParser.SizeOfUInt64((ulong)UnixOfficialEndTime);
			num += ProtocolParser.SizeOfUInt64((ulong)PatronCount);
			if (HasIsInnkeeper)
			{
				num++;
				num++;
			}
			if (HasIsSetupComplete)
			{
				num++;
				num++;
			}
			uint serializedSize2 = FsgInnkeeperAccountId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (HasIsLargeScaleFsg)
			{
				num++;
				num++;
			}
			if (HasFsgName)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(FsgName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasTavernId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TavernId);
			}
			return num + 9;
		}
	}
}
