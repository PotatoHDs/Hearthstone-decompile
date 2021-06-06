using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class BoosterModifications : IProtoBuf
	{
		private List<BoosterInfo> _Modifications = new List<BoosterInfo>();

		public List<BoosterInfo> Modifications
		{
			get
			{
				return _Modifications;
			}
			set
			{
				_Modifications = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (BoosterInfo modification in Modifications)
			{
				num ^= modification.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BoosterModifications boosterModifications = obj as BoosterModifications;
			if (boosterModifications == null)
			{
				return false;
			}
			if (Modifications.Count != boosterModifications.Modifications.Count)
			{
				return false;
			}
			for (int i = 0; i < Modifications.Count; i++)
			{
				if (!Modifications[i].Equals(boosterModifications.Modifications[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BoosterModifications Deserialize(Stream stream, BoosterModifications instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BoosterModifications DeserializeLengthDelimited(Stream stream)
		{
			BoosterModifications boosterModifications = new BoosterModifications();
			DeserializeLengthDelimited(stream, boosterModifications);
			return boosterModifications;
		}

		public static BoosterModifications DeserializeLengthDelimited(Stream stream, BoosterModifications instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BoosterModifications Deserialize(Stream stream, BoosterModifications instance, long limit)
		{
			if (instance.Modifications == null)
			{
				instance.Modifications = new List<BoosterInfo>();
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
					instance.Modifications.Add(BoosterInfo.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, BoosterModifications instance)
		{
			if (instance.Modifications.Count <= 0)
			{
				return;
			}
			foreach (BoosterInfo modification in instance.Modifications)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, modification.GetSerializedSize());
				BoosterInfo.Serialize(stream, modification);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Modifications.Count > 0)
			{
				foreach (BoosterInfo modification in Modifications)
				{
					num++;
					uint serializedSize = modification.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
