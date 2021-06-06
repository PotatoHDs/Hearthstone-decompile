using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class ProfileProgress : IProtoBuf
	{
		public enum PacketID
		{
			ID = 233
		}

		public bool HasLastForge;

		private Date _LastForge;

		public long Progress { get; set; }

		public int BestForge { get; set; }

		public Date LastForge
		{
			get
			{
				return _LastForge;
			}
			set
			{
				_LastForge = value;
				HasLastForge = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Progress.GetHashCode();
			hashCode ^= BestForge.GetHashCode();
			if (HasLastForge)
			{
				hashCode ^= LastForge.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ProfileProgress profileProgress = obj as ProfileProgress;
			if (profileProgress == null)
			{
				return false;
			}
			if (!Progress.Equals(profileProgress.Progress))
			{
				return false;
			}
			if (!BestForge.Equals(profileProgress.BestForge))
			{
				return false;
			}
			if (HasLastForge != profileProgress.HasLastForge || (HasLastForge && !LastForge.Equals(profileProgress.LastForge)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileProgress Deserialize(Stream stream, ProfileProgress instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileProgress DeserializeLengthDelimited(Stream stream)
		{
			ProfileProgress profileProgress = new ProfileProgress();
			DeserializeLengthDelimited(stream, profileProgress);
			return profileProgress;
		}

		public static ProfileProgress DeserializeLengthDelimited(Stream stream, ProfileProgress instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileProgress Deserialize(Stream stream, ProfileProgress instance, long limit)
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
					instance.Progress = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.BestForge = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					if (instance.LastForge == null)
					{
						instance.LastForge = Date.DeserializeLengthDelimited(stream);
					}
					else
					{
						Date.DeserializeLengthDelimited(stream, instance.LastForge);
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

		public static void Serialize(Stream stream, ProfileProgress instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Progress);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.BestForge);
			if (instance.HasLastForge)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.LastForge.GetSerializedSize());
				Date.Serialize(stream, instance.LastForge);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Progress);
			num += ProtocolParser.SizeOfUInt64((ulong)BestForge);
			if (HasLastForge)
			{
				num++;
				uint serializedSize = LastForge.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 2;
		}
	}
}
