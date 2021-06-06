using System.IO;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	public class ApkUpdate : IProtoBuf
	{
		public bool HasInstalledVersion;

		private int _InstalledVersion;

		public bool HasAssetVersion;

		private int _AssetVersion;

		public bool HasAgentVersion;

		private int _AgentVersion;

		public int InstalledVersion
		{
			get
			{
				return _InstalledVersion;
			}
			set
			{
				_InstalledVersion = value;
				HasInstalledVersion = true;
			}
		}

		public int AssetVersion
		{
			get
			{
				return _AssetVersion;
			}
			set
			{
				_AssetVersion = value;
				HasAssetVersion = true;
			}
		}

		public int AgentVersion
		{
			get
			{
				return _AgentVersion;
			}
			set
			{
				_AgentVersion = value;
				HasAgentVersion = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasInstalledVersion)
			{
				num ^= InstalledVersion.GetHashCode();
			}
			if (HasAssetVersion)
			{
				num ^= AssetVersion.GetHashCode();
			}
			if (HasAgentVersion)
			{
				num ^= AgentVersion.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ApkUpdate apkUpdate = obj as ApkUpdate;
			if (apkUpdate == null)
			{
				return false;
			}
			if (HasInstalledVersion != apkUpdate.HasInstalledVersion || (HasInstalledVersion && !InstalledVersion.Equals(apkUpdate.InstalledVersion)))
			{
				return false;
			}
			if (HasAssetVersion != apkUpdate.HasAssetVersion || (HasAssetVersion && !AssetVersion.Equals(apkUpdate.AssetVersion)))
			{
				return false;
			}
			if (HasAgentVersion != apkUpdate.HasAgentVersion || (HasAgentVersion && !AgentVersion.Equals(apkUpdate.AgentVersion)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ApkUpdate Deserialize(Stream stream, ApkUpdate instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ApkUpdate DeserializeLengthDelimited(Stream stream)
		{
			ApkUpdate apkUpdate = new ApkUpdate();
			DeserializeLengthDelimited(stream, apkUpdate);
			return apkUpdate;
		}

		public static ApkUpdate DeserializeLengthDelimited(Stream stream, ApkUpdate instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ApkUpdate Deserialize(Stream stream, ApkUpdate instance, long limit)
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
					instance.InstalledVersion = ProtocolParser.ReadZInt32(stream);
					continue;
				case 16:
					instance.AssetVersion = ProtocolParser.ReadZInt32(stream);
					continue;
				case 24:
					instance.AgentVersion = ProtocolParser.ReadZInt32(stream);
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

		public static void Serialize(Stream stream, ApkUpdate instance)
		{
			if (instance.HasInstalledVersion)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteZInt32(stream, instance.InstalledVersion);
			}
			if (instance.HasAssetVersion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteZInt32(stream, instance.AssetVersion);
			}
			if (instance.HasAgentVersion)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteZInt32(stream, instance.AgentVersion);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasInstalledVersion)
			{
				num++;
				num += ProtocolParser.SizeOfZInt32(InstalledVersion);
			}
			if (HasAssetVersion)
			{
				num++;
				num += ProtocolParser.SizeOfZInt32(AssetVersion);
			}
			if (HasAgentVersion)
			{
				num++;
				num += ProtocolParser.SizeOfZInt32(AgentVersion);
			}
			return num;
		}
	}
}
