using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	public class TavernBrawlInfo : IProtoBuf
	{
		public enum PacketID
		{
			ID = 316
		}

		public bool HasCurrentTavernBrawl;

		private TavernBrawlSeasonSpec _CurrentTavernBrawl;

		public bool HasNextStartSecondsFromNow;

		private ulong _NextStartSecondsFromNow;

		public bool HasBrawlType;

		private BrawlType _BrawlType;

		public bool HasMyRecord;

		private TavernBrawlPlayerRecord _MyRecord;

		public bool HasDeprecatedStoreInstructionPrefab;

		private string _DeprecatedStoreInstructionPrefab;

		public bool HasDeprecatedStoreInstructionPrefabPhone;

		private string _DeprecatedStoreInstructionPrefabPhone;

		public TavernBrawlSeasonSpec CurrentTavernBrawl
		{
			get
			{
				return _CurrentTavernBrawl;
			}
			set
			{
				_CurrentTavernBrawl = value;
				HasCurrentTavernBrawl = value != null;
			}
		}

		public ulong NextStartSecondsFromNow
		{
			get
			{
				return _NextStartSecondsFromNow;
			}
			set
			{
				_NextStartSecondsFromNow = value;
				HasNextStartSecondsFromNow = true;
			}
		}

		public BrawlType BrawlType
		{
			get
			{
				return _BrawlType;
			}
			set
			{
				_BrawlType = value;
				HasBrawlType = true;
			}
		}

		public TavernBrawlPlayerRecord MyRecord
		{
			get
			{
				return _MyRecord;
			}
			set
			{
				_MyRecord = value;
				HasMyRecord = value != null;
			}
		}

		public string DeprecatedStoreInstructionPrefab
		{
			get
			{
				return _DeprecatedStoreInstructionPrefab;
			}
			set
			{
				_DeprecatedStoreInstructionPrefab = value;
				HasDeprecatedStoreInstructionPrefab = value != null;
			}
		}

		public string DeprecatedStoreInstructionPrefabPhone
		{
			get
			{
				return _DeprecatedStoreInstructionPrefabPhone;
			}
			set
			{
				_DeprecatedStoreInstructionPrefabPhone = value;
				HasDeprecatedStoreInstructionPrefabPhone = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasCurrentTavernBrawl)
			{
				num ^= CurrentTavernBrawl.GetHashCode();
			}
			if (HasNextStartSecondsFromNow)
			{
				num ^= NextStartSecondsFromNow.GetHashCode();
			}
			if (HasBrawlType)
			{
				num ^= BrawlType.GetHashCode();
			}
			if (HasMyRecord)
			{
				num ^= MyRecord.GetHashCode();
			}
			if (HasDeprecatedStoreInstructionPrefab)
			{
				num ^= DeprecatedStoreInstructionPrefab.GetHashCode();
			}
			if (HasDeprecatedStoreInstructionPrefabPhone)
			{
				num ^= DeprecatedStoreInstructionPrefabPhone.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			TavernBrawlInfo tavernBrawlInfo = obj as TavernBrawlInfo;
			if (tavernBrawlInfo == null)
			{
				return false;
			}
			if (HasCurrentTavernBrawl != tavernBrawlInfo.HasCurrentTavernBrawl || (HasCurrentTavernBrawl && !CurrentTavernBrawl.Equals(tavernBrawlInfo.CurrentTavernBrawl)))
			{
				return false;
			}
			if (HasNextStartSecondsFromNow != tavernBrawlInfo.HasNextStartSecondsFromNow || (HasNextStartSecondsFromNow && !NextStartSecondsFromNow.Equals(tavernBrawlInfo.NextStartSecondsFromNow)))
			{
				return false;
			}
			if (HasBrawlType != tavernBrawlInfo.HasBrawlType || (HasBrawlType && !BrawlType.Equals(tavernBrawlInfo.BrawlType)))
			{
				return false;
			}
			if (HasMyRecord != tavernBrawlInfo.HasMyRecord || (HasMyRecord && !MyRecord.Equals(tavernBrawlInfo.MyRecord)))
			{
				return false;
			}
			if (HasDeprecatedStoreInstructionPrefab != tavernBrawlInfo.HasDeprecatedStoreInstructionPrefab || (HasDeprecatedStoreInstructionPrefab && !DeprecatedStoreInstructionPrefab.Equals(tavernBrawlInfo.DeprecatedStoreInstructionPrefab)))
			{
				return false;
			}
			if (HasDeprecatedStoreInstructionPrefabPhone != tavernBrawlInfo.HasDeprecatedStoreInstructionPrefabPhone || (HasDeprecatedStoreInstructionPrefabPhone && !DeprecatedStoreInstructionPrefabPhone.Equals(tavernBrawlInfo.DeprecatedStoreInstructionPrefabPhone)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TavernBrawlInfo Deserialize(Stream stream, TavernBrawlInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TavernBrawlInfo DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlInfo tavernBrawlInfo = new TavernBrawlInfo();
			DeserializeLengthDelimited(stream, tavernBrawlInfo);
			return tavernBrawlInfo;
		}

		public static TavernBrawlInfo DeserializeLengthDelimited(Stream stream, TavernBrawlInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TavernBrawlInfo Deserialize(Stream stream, TavernBrawlInfo instance, long limit)
		{
			instance.BrawlType = BrawlType.BRAWL_TYPE_UNKNOWN;
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
					if (instance.CurrentTavernBrawl == null)
					{
						instance.CurrentTavernBrawl = TavernBrawlSeasonSpec.DeserializeLengthDelimited(stream);
					}
					else
					{
						TavernBrawlSeasonSpec.DeserializeLengthDelimited(stream, instance.CurrentTavernBrawl);
					}
					continue;
				case 16:
					instance.NextStartSecondsFromNow = ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.BrawlType = (BrawlType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					if (instance.MyRecord == null)
					{
						instance.MyRecord = TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream);
					}
					else
					{
						TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream, instance.MyRecord);
					}
					continue;
				case 42:
					instance.DeprecatedStoreInstructionPrefab = ProtocolParser.ReadString(stream);
					continue;
				case 50:
					instance.DeprecatedStoreInstructionPrefabPhone = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, TavernBrawlInfo instance)
		{
			if (instance.HasCurrentTavernBrawl)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.CurrentTavernBrawl.GetSerializedSize());
				TavernBrawlSeasonSpec.Serialize(stream, instance.CurrentTavernBrawl);
			}
			if (instance.HasNextStartSecondsFromNow)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.NextStartSecondsFromNow);
			}
			if (instance.HasBrawlType)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BrawlType);
			}
			if (instance.HasMyRecord)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.MyRecord.GetSerializedSize());
				TavernBrawlPlayerRecord.Serialize(stream, instance.MyRecord);
			}
			if (instance.HasDeprecatedStoreInstructionPrefab)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeprecatedStoreInstructionPrefab));
			}
			if (instance.HasDeprecatedStoreInstructionPrefabPhone)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeprecatedStoreInstructionPrefabPhone));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasCurrentTavernBrawl)
			{
				num++;
				uint serializedSize = CurrentTavernBrawl.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasNextStartSecondsFromNow)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(NextStartSecondsFromNow);
			}
			if (HasBrawlType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BrawlType);
			}
			if (HasMyRecord)
			{
				num++;
				uint serializedSize2 = MyRecord.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasDeprecatedStoreInstructionPrefab)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(DeprecatedStoreInstructionPrefab);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasDeprecatedStoreInstructionPrefabPhone)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(DeprecatedStoreInstructionPrefabPhone);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
