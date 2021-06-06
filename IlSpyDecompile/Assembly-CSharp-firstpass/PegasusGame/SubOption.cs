using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	public class SubOption : IProtoBuf
	{
		private List<TargetOption> _Targets = new List<TargetOption>();

		public bool HasPlayErrorParam;

		private int _PlayErrorParam;

		public int Id { get; set; }

		public List<TargetOption> Targets
		{
			get
			{
				return _Targets;
			}
			set
			{
				_Targets = value;
			}
		}

		public int PlayError { get; set; }

		public int PlayErrorParam
		{
			get
			{
				return _PlayErrorParam;
			}
			set
			{
				_PlayErrorParam = value;
				HasPlayErrorParam = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			foreach (TargetOption target in Targets)
			{
				hashCode ^= target.GetHashCode();
			}
			hashCode ^= PlayError.GetHashCode();
			if (HasPlayErrorParam)
			{
				hashCode ^= PlayErrorParam.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			SubOption subOption = obj as SubOption;
			if (subOption == null)
			{
				return false;
			}
			if (!Id.Equals(subOption.Id))
			{
				return false;
			}
			if (Targets.Count != subOption.Targets.Count)
			{
				return false;
			}
			for (int i = 0; i < Targets.Count; i++)
			{
				if (!Targets[i].Equals(subOption.Targets[i]))
				{
					return false;
				}
			}
			if (!PlayError.Equals(subOption.PlayError))
			{
				return false;
			}
			if (HasPlayErrorParam != subOption.HasPlayErrorParam || (HasPlayErrorParam && !PlayErrorParam.Equals(subOption.PlayErrorParam)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubOption Deserialize(Stream stream, SubOption instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubOption DeserializeLengthDelimited(Stream stream)
		{
			SubOption subOption = new SubOption();
			DeserializeLengthDelimited(stream, subOption);
			return subOption;
		}

		public static SubOption DeserializeLengthDelimited(Stream stream, SubOption instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubOption Deserialize(Stream stream, SubOption instance, long limit)
		{
			if (instance.Targets == null)
			{
				instance.Targets = new List<TargetOption>();
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
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.Targets.Add(TargetOption.DeserializeLengthDelimited(stream));
					continue;
				case 32:
					instance.PlayError = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.PlayErrorParam = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SubOption instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			if (instance.Targets.Count > 0)
			{
				foreach (TargetOption target in instance.Targets)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, target.GetSerializedSize());
					TargetOption.Serialize(stream, target);
				}
			}
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayError);
			if (instance.HasPlayErrorParam)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayErrorParam);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			if (Targets.Count > 0)
			{
				foreach (TargetOption target in Targets)
				{
					num++;
					uint serializedSize = target.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += ProtocolParser.SizeOfUInt64((ulong)PlayError);
			if (HasPlayErrorParam)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PlayErrorParam);
			}
			return num + 2;
		}
	}
}
