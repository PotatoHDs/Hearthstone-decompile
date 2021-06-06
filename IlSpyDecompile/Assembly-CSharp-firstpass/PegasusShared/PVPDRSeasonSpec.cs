using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	public class PVPDRSeasonSpec : IProtoBuf
	{
		private List<LocalizedString> _Strings = new List<LocalizedString>();

		public GameContentSeasonSpec GameContentSeason { get; set; }

		public List<LocalizedString> Strings
		{
			get
			{
				return _Strings;
			}
			set
			{
				_Strings = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameContentSeason.GetHashCode();
			foreach (LocalizedString @string in Strings)
			{
				hashCode ^= @string.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PVPDRSeasonSpec pVPDRSeasonSpec = obj as PVPDRSeasonSpec;
			if (pVPDRSeasonSpec == null)
			{
				return false;
			}
			if (!GameContentSeason.Equals(pVPDRSeasonSpec.GameContentSeason))
			{
				return false;
			}
			if (Strings.Count != pVPDRSeasonSpec.Strings.Count)
			{
				return false;
			}
			for (int i = 0; i < Strings.Count; i++)
			{
				if (!Strings[i].Equals(pVPDRSeasonSpec.Strings[i]))
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

		public static PVPDRSeasonSpec Deserialize(Stream stream, PVPDRSeasonSpec instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PVPDRSeasonSpec DeserializeLengthDelimited(Stream stream)
		{
			PVPDRSeasonSpec pVPDRSeasonSpec = new PVPDRSeasonSpec();
			DeserializeLengthDelimited(stream, pVPDRSeasonSpec);
			return pVPDRSeasonSpec;
		}

		public static PVPDRSeasonSpec DeserializeLengthDelimited(Stream stream, PVPDRSeasonSpec instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PVPDRSeasonSpec Deserialize(Stream stream, PVPDRSeasonSpec instance, long limit)
		{
			if (instance.Strings == null)
			{
				instance.Strings = new List<LocalizedString>();
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
					if (instance.GameContentSeason == null)
					{
						instance.GameContentSeason = GameContentSeasonSpec.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameContentSeasonSpec.DeserializeLengthDelimited(stream, instance.GameContentSeason);
					}
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
							instance.Strings.Add(LocalizedString.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, PVPDRSeasonSpec instance)
		{
			if (instance.GameContentSeason == null)
			{
				throw new ArgumentNullException("GameContentSeason", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameContentSeason.GetSerializedSize());
			GameContentSeasonSpec.Serialize(stream, instance.GameContentSeason);
			if (instance.Strings.Count <= 0)
			{
				return;
			}
			foreach (LocalizedString @string in instance.Strings)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, @string.GetSerializedSize());
				LocalizedString.Serialize(stream, @string);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = GameContentSeason.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (Strings.Count > 0)
			{
				foreach (LocalizedString @string in Strings)
				{
					num += 2;
					uint serializedSize2 = @string.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num + 1;
		}
	}
}
