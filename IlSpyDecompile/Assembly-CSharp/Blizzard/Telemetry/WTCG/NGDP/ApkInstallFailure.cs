using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	public class ApkInstallFailure : IProtoBuf
	{
		public bool HasUpdatedVersion;

		private string _UpdatedVersion;

		public bool HasReason;

		private string _Reason;

		public string UpdatedVersion
		{
			get
			{
				return _UpdatedVersion;
			}
			set
			{
				_UpdatedVersion = value;
				HasUpdatedVersion = value != null;
			}
		}

		public string Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasUpdatedVersion)
			{
				num ^= UpdatedVersion.GetHashCode();
			}
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ApkInstallFailure apkInstallFailure = obj as ApkInstallFailure;
			if (apkInstallFailure == null)
			{
				return false;
			}
			if (HasUpdatedVersion != apkInstallFailure.HasUpdatedVersion || (HasUpdatedVersion && !UpdatedVersion.Equals(apkInstallFailure.UpdatedVersion)))
			{
				return false;
			}
			if (HasReason != apkInstallFailure.HasReason || (HasReason && !Reason.Equals(apkInstallFailure.Reason)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ApkInstallFailure Deserialize(Stream stream, ApkInstallFailure instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ApkInstallFailure DeserializeLengthDelimited(Stream stream)
		{
			ApkInstallFailure apkInstallFailure = new ApkInstallFailure();
			DeserializeLengthDelimited(stream, apkInstallFailure);
			return apkInstallFailure;
		}

		public static ApkInstallFailure DeserializeLengthDelimited(Stream stream, ApkInstallFailure instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ApkInstallFailure Deserialize(Stream stream, ApkInstallFailure instance, long limit)
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
				case 18:
					instance.UpdatedVersion = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.Reason = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ApkInstallFailure instance)
		{
			if (instance.HasUpdatedVersion)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UpdatedVersion));
			}
			if (instance.HasReason)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reason));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasUpdatedVersion)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(UpdatedVersion);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasReason)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Reason);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
