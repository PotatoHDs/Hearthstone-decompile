using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class BoosterList : IProtoBuf
	{
		private List<BoosterInfo> _List = new List<BoosterInfo>();

		public List<BoosterInfo> List
		{
			get
			{
				return _List;
			}
			set
			{
				_List = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (BoosterInfo item in List)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BoosterList boosterList = obj as BoosterList;
			if (boosterList == null)
			{
				return false;
			}
			if (List.Count != boosterList.List.Count)
			{
				return false;
			}
			for (int i = 0; i < List.Count; i++)
			{
				if (!List[i].Equals(boosterList.List[i]))
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

		public static BoosterList Deserialize(Stream stream, BoosterList instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BoosterList DeserializeLengthDelimited(Stream stream)
		{
			BoosterList boosterList = new BoosterList();
			DeserializeLengthDelimited(stream, boosterList);
			return boosterList;
		}

		public static BoosterList DeserializeLengthDelimited(Stream stream, BoosterList instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BoosterList Deserialize(Stream stream, BoosterList instance, long limit)
		{
			if (instance.List == null)
			{
				instance.List = new List<BoosterInfo>();
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
					instance.List.Add(BoosterInfo.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, BoosterList instance)
		{
			if (instance.List.Count <= 0)
			{
				return;
			}
			foreach (BoosterInfo item in instance.List)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				BoosterInfo.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (List.Count > 0)
			{
				foreach (BoosterInfo item in List)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
