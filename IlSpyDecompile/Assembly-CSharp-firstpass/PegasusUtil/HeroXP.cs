using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class HeroXP : IProtoBuf
	{
		public enum PacketID
		{
			ID = 283
		}

		private List<HeroXPInfo> _XpInfos = new List<HeroXPInfo>();

		public List<HeroXPInfo> XpInfos
		{
			get
			{
				return _XpInfos;
			}
			set
			{
				_XpInfos = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (HeroXPInfo xpInfo in XpInfos)
			{
				num ^= xpInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			HeroXP heroXP = obj as HeroXP;
			if (heroXP == null)
			{
				return false;
			}
			if (XpInfos.Count != heroXP.XpInfos.Count)
			{
				return false;
			}
			for (int i = 0; i < XpInfos.Count; i++)
			{
				if (!XpInfos[i].Equals(heroXP.XpInfos[i]))
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

		public static HeroXP Deserialize(Stream stream, HeroXP instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static HeroXP DeserializeLengthDelimited(Stream stream)
		{
			HeroXP heroXP = new HeroXP();
			DeserializeLengthDelimited(stream, heroXP);
			return heroXP;
		}

		public static HeroXP DeserializeLengthDelimited(Stream stream, HeroXP instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static HeroXP Deserialize(Stream stream, HeroXP instance, long limit)
		{
			if (instance.XpInfos == null)
			{
				instance.XpInfos = new List<HeroXPInfo>();
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
					instance.XpInfos.Add(HeroXPInfo.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, HeroXP instance)
		{
			if (instance.XpInfos.Count <= 0)
			{
				return;
			}
			foreach (HeroXPInfo xpInfo in instance.XpInfos)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, xpInfo.GetSerializedSize());
				HeroXPInfo.Serialize(stream, xpInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (XpInfos.Count > 0)
			{
				foreach (HeroXPInfo xpInfo in XpInfos)
				{
					num++;
					uint serializedSize = xpInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
