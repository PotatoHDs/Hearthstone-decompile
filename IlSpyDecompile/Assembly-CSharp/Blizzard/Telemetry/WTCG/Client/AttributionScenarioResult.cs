using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class AttributionScenarioResult : IProtoBuf
	{
		public bool HasApplicationId;

		private string _ApplicationId;

		public bool HasDeviceType;

		private string _DeviceType;

		public bool HasFirstInstallDate;

		private ulong _FirstInstallDate;

		public bool HasBundleId;

		private string _BundleId;

		public bool HasScenarioId;

		private int _ScenarioId;

		public bool HasResult;

		private string _Result;

		public bool HasBossId;

		private int _BossId;

		public bool HasIdentifier;

		private IdentifierInfo _Identifier;

		public string ApplicationId
		{
			get
			{
				return _ApplicationId;
			}
			set
			{
				_ApplicationId = value;
				HasApplicationId = value != null;
			}
		}

		public string DeviceType
		{
			get
			{
				return _DeviceType;
			}
			set
			{
				_DeviceType = value;
				HasDeviceType = value != null;
			}
		}

		public ulong FirstInstallDate
		{
			get
			{
				return _FirstInstallDate;
			}
			set
			{
				_FirstInstallDate = value;
				HasFirstInstallDate = true;
			}
		}

		public string BundleId
		{
			get
			{
				return _BundleId;
			}
			set
			{
				_BundleId = value;
				HasBundleId = value != null;
			}
		}

		public int ScenarioId
		{
			get
			{
				return _ScenarioId;
			}
			set
			{
				_ScenarioId = value;
				HasScenarioId = true;
			}
		}

		public string Result
		{
			get
			{
				return _Result;
			}
			set
			{
				_Result = value;
				HasResult = value != null;
			}
		}

		public int BossId
		{
			get
			{
				return _BossId;
			}
			set
			{
				_BossId = value;
				HasBossId = true;
			}
		}

		public IdentifierInfo Identifier
		{
			get
			{
				return _Identifier;
			}
			set
			{
				_Identifier = value;
				HasIdentifier = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasApplicationId)
			{
				num ^= ApplicationId.GetHashCode();
			}
			if (HasDeviceType)
			{
				num ^= DeviceType.GetHashCode();
			}
			if (HasFirstInstallDate)
			{
				num ^= FirstInstallDate.GetHashCode();
			}
			if (HasBundleId)
			{
				num ^= BundleId.GetHashCode();
			}
			if (HasScenarioId)
			{
				num ^= ScenarioId.GetHashCode();
			}
			if (HasResult)
			{
				num ^= Result.GetHashCode();
			}
			if (HasBossId)
			{
				num ^= BossId.GetHashCode();
			}
			if (HasIdentifier)
			{
				num ^= Identifier.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AttributionScenarioResult attributionScenarioResult = obj as AttributionScenarioResult;
			if (attributionScenarioResult == null)
			{
				return false;
			}
			if (HasApplicationId != attributionScenarioResult.HasApplicationId || (HasApplicationId && !ApplicationId.Equals(attributionScenarioResult.ApplicationId)))
			{
				return false;
			}
			if (HasDeviceType != attributionScenarioResult.HasDeviceType || (HasDeviceType && !DeviceType.Equals(attributionScenarioResult.DeviceType)))
			{
				return false;
			}
			if (HasFirstInstallDate != attributionScenarioResult.HasFirstInstallDate || (HasFirstInstallDate && !FirstInstallDate.Equals(attributionScenarioResult.FirstInstallDate)))
			{
				return false;
			}
			if (HasBundleId != attributionScenarioResult.HasBundleId || (HasBundleId && !BundleId.Equals(attributionScenarioResult.BundleId)))
			{
				return false;
			}
			if (HasScenarioId != attributionScenarioResult.HasScenarioId || (HasScenarioId && !ScenarioId.Equals(attributionScenarioResult.ScenarioId)))
			{
				return false;
			}
			if (HasResult != attributionScenarioResult.HasResult || (HasResult && !Result.Equals(attributionScenarioResult.Result)))
			{
				return false;
			}
			if (HasBossId != attributionScenarioResult.HasBossId || (HasBossId && !BossId.Equals(attributionScenarioResult.BossId)))
			{
				return false;
			}
			if (HasIdentifier != attributionScenarioResult.HasIdentifier || (HasIdentifier && !Identifier.Equals(attributionScenarioResult.Identifier)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AttributionScenarioResult Deserialize(Stream stream, AttributionScenarioResult instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AttributionScenarioResult DeserializeLengthDelimited(Stream stream)
		{
			AttributionScenarioResult attributionScenarioResult = new AttributionScenarioResult();
			DeserializeLengthDelimited(stream, attributionScenarioResult);
			return attributionScenarioResult;
		}

		public static AttributionScenarioResult DeserializeLengthDelimited(Stream stream, AttributionScenarioResult instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AttributionScenarioResult Deserialize(Stream stream, AttributionScenarioResult instance, long limit)
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
					instance.ScenarioId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Result = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.BossId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 100u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ApplicationId = ProtocolParser.ReadString(stream);
						}
						break;
					case 101u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeviceType = ProtocolParser.ReadString(stream);
						}
						break;
					case 102u:
						if (key.WireType == Wire.Varint)
						{
							instance.FirstInstallDate = ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 103u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.BundleId = ProtocolParser.ReadString(stream);
						}
						break;
					case 1000u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.Identifier == null)
							{
								instance.Identifier = IdentifierInfo.DeserializeLengthDelimited(stream);
							}
							else
							{
								IdentifierInfo.DeserializeLengthDelimited(stream, instance.Identifier);
							}
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
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

		public static void Serialize(Stream stream, AttributionScenarioResult instance)
		{
			if (instance.HasApplicationId)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
			if (instance.HasDeviceType)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceType));
			}
			if (instance.HasFirstInstallDate)
			{
				stream.WriteByte(176);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt64(stream, instance.FirstInstallDate);
			}
			if (instance.HasBundleId)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BundleId));
			}
			if (instance.HasScenarioId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ScenarioId);
			}
			if (instance.HasResult)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Result));
			}
			if (instance.HasBossId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BossId);
			}
			if (instance.HasIdentifier)
			{
				stream.WriteByte(194);
				stream.WriteByte(62);
				ProtocolParser.WriteUInt32(stream, instance.Identifier.GetSerializedSize());
				IdentifierInfo.Serialize(stream, instance.Identifier);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasApplicationId)
			{
				num += 2;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasDeviceType)
			{
				num += 2;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(DeviceType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasFirstInstallDate)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64(FirstInstallDate);
			}
			if (HasBundleId)
			{
				num += 2;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(BundleId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasScenarioId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ScenarioId);
			}
			if (HasResult)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(Result);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasBossId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BossId);
			}
			if (HasIdentifier)
			{
				num += 2;
				uint serializedSize = Identifier.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
