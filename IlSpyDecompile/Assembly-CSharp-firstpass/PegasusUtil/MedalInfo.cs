using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class MedalInfo : IProtoBuf
	{
		public enum PacketID
		{
			ID = 232
		}

		private List<MedalInfoData> _MedalData = new List<MedalInfoData>();

		public List<MedalInfoData> MedalData
		{
			get
			{
				return _MedalData;
			}
			set
			{
				_MedalData = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (MedalInfoData medalDatum in MedalData)
			{
				num ^= medalDatum.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MedalInfo medalInfo = obj as MedalInfo;
			if (medalInfo == null)
			{
				return false;
			}
			if (MedalData.Count != medalInfo.MedalData.Count)
			{
				return false;
			}
			for (int i = 0; i < MedalData.Count; i++)
			{
				if (!MedalData[i].Equals(medalInfo.MedalData[i]))
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

		public static MedalInfo Deserialize(Stream stream, MedalInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MedalInfo DeserializeLengthDelimited(Stream stream)
		{
			MedalInfo medalInfo = new MedalInfo();
			DeserializeLengthDelimited(stream, medalInfo);
			return medalInfo;
		}

		public static MedalInfo DeserializeLengthDelimited(Stream stream, MedalInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MedalInfo Deserialize(Stream stream, MedalInfo instance, long limit)
		{
			if (instance.MedalData == null)
			{
				instance.MedalData = new List<MedalInfoData>();
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
				case 26:
					instance.MedalData.Add(MedalInfoData.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, MedalInfo instance)
		{
			if (instance.MedalData.Count <= 0)
			{
				return;
			}
			foreach (MedalInfoData medalDatum in instance.MedalData)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, medalDatum.GetSerializedSize());
				MedalInfoData.Serialize(stream, medalDatum);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (MedalData.Count > 0)
			{
				foreach (MedalInfoData medalDatum in MedalData)
				{
					num++;
					uint serializedSize = medalDatum.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
