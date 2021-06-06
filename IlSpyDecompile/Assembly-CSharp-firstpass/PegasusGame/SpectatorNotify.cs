using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	public class SpectatorNotify : IProtoBuf
	{
		public enum PacketID
		{
			ID = 24
		}

		private List<SpectatorChange> _SpectatorChange = new List<SpectatorChange>();

		public bool HasSpectatorPasswordUpdate;

		private string _SpectatorPasswordUpdate;

		public bool HasSpectatorRemoved;

		private SpectatorRemoved _SpectatorRemoved;

		public int PlayerId { get; set; }

		public List<SpectatorChange> SpectatorChange
		{
			get
			{
				return _SpectatorChange;
			}
			set
			{
				_SpectatorChange = value;
			}
		}

		public string SpectatorPasswordUpdate
		{
			get
			{
				return _SpectatorPasswordUpdate;
			}
			set
			{
				_SpectatorPasswordUpdate = value;
				HasSpectatorPasswordUpdate = value != null;
			}
		}

		public SpectatorRemoved SpectatorRemoved
		{
			get
			{
				return _SpectatorRemoved;
			}
			set
			{
				_SpectatorRemoved = value;
				HasSpectatorRemoved = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= PlayerId.GetHashCode();
			foreach (SpectatorChange item in SpectatorChange)
			{
				hashCode ^= item.GetHashCode();
			}
			if (HasSpectatorPasswordUpdate)
			{
				hashCode ^= SpectatorPasswordUpdate.GetHashCode();
			}
			if (HasSpectatorRemoved)
			{
				hashCode ^= SpectatorRemoved.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			SpectatorNotify spectatorNotify = obj as SpectatorNotify;
			if (spectatorNotify == null)
			{
				return false;
			}
			if (!PlayerId.Equals(spectatorNotify.PlayerId))
			{
				return false;
			}
			if (SpectatorChange.Count != spectatorNotify.SpectatorChange.Count)
			{
				return false;
			}
			for (int i = 0; i < SpectatorChange.Count; i++)
			{
				if (!SpectatorChange[i].Equals(spectatorNotify.SpectatorChange[i]))
				{
					return false;
				}
			}
			if (HasSpectatorPasswordUpdate != spectatorNotify.HasSpectatorPasswordUpdate || (HasSpectatorPasswordUpdate && !SpectatorPasswordUpdate.Equals(spectatorNotify.SpectatorPasswordUpdate)))
			{
				return false;
			}
			if (HasSpectatorRemoved != spectatorNotify.HasSpectatorRemoved || (HasSpectatorRemoved && !SpectatorRemoved.Equals(spectatorNotify.SpectatorRemoved)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SpectatorNotify Deserialize(Stream stream, SpectatorNotify instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SpectatorNotify DeserializeLengthDelimited(Stream stream)
		{
			SpectatorNotify spectatorNotify = new SpectatorNotify();
			DeserializeLengthDelimited(stream, spectatorNotify);
			return spectatorNotify;
		}

		public static SpectatorNotify DeserializeLengthDelimited(Stream stream, SpectatorNotify instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SpectatorNotify Deserialize(Stream stream, SpectatorNotify instance, long limit)
		{
			if (instance.SpectatorChange == null)
			{
				instance.SpectatorChange = new List<SpectatorChange>();
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
					instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.SpectatorChange.Add(PegasusGame.SpectatorChange.DeserializeLengthDelimited(stream));
					continue;
				case 42:
					instance.SpectatorPasswordUpdate = ProtocolParser.ReadString(stream);
					continue;
				case 50:
					if (instance.SpectatorRemoved == null)
					{
						instance.SpectatorRemoved = SpectatorRemoved.DeserializeLengthDelimited(stream);
					}
					else
					{
						SpectatorRemoved.DeserializeLengthDelimited(stream, instance.SpectatorRemoved);
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

		public static void Serialize(Stream stream, SpectatorNotify instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			if (instance.SpectatorChange.Count > 0)
			{
				foreach (SpectatorChange item in instance.SpectatorChange)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					PegasusGame.SpectatorChange.Serialize(stream, item);
				}
			}
			if (instance.HasSpectatorPasswordUpdate)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SpectatorPasswordUpdate));
			}
			if (instance.HasSpectatorRemoved)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.SpectatorRemoved.GetSerializedSize());
				SpectatorRemoved.Serialize(stream, instance.SpectatorRemoved);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)PlayerId);
			if (SpectatorChange.Count > 0)
			{
				foreach (SpectatorChange item in SpectatorChange)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasSpectatorPasswordUpdate)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(SpectatorPasswordUpdate);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasSpectatorRemoved)
			{
				num++;
				uint serializedSize2 = SpectatorRemoved.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1;
		}
	}
}
