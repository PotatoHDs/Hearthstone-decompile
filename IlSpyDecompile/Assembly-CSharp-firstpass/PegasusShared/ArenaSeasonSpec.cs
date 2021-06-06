using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	public class ArenaSeasonSpec : IProtoBuf
	{
		public bool HasRewardPaperPrefab;

		private string _RewardPaperPrefab;

		public bool HasRewardPaperPrefabPhone;

		private string _RewardPaperPrefabPhone;

		public bool HasDraftPaperTexture;

		private string _DraftPaperTexture;

		public bool HasDraftPaperTexturePhone;

		private string _DraftPaperTexturePhone;

		public bool HasDraftPaperTextColor;

		private string _DraftPaperTextColor;

		private List<LocalizedString> _Strings = new List<LocalizedString>();

		public GameContentSeasonSpec GameContentSeason { get; set; }

		public string RewardPaperPrefab
		{
			get
			{
				return _RewardPaperPrefab;
			}
			set
			{
				_RewardPaperPrefab = value;
				HasRewardPaperPrefab = value != null;
			}
		}

		public string RewardPaperPrefabPhone
		{
			get
			{
				return _RewardPaperPrefabPhone;
			}
			set
			{
				_RewardPaperPrefabPhone = value;
				HasRewardPaperPrefabPhone = value != null;
			}
		}

		public string DraftPaperTexture
		{
			get
			{
				return _DraftPaperTexture;
			}
			set
			{
				_DraftPaperTexture = value;
				HasDraftPaperTexture = value != null;
			}
		}

		public string DraftPaperTexturePhone
		{
			get
			{
				return _DraftPaperTexturePhone;
			}
			set
			{
				_DraftPaperTexturePhone = value;
				HasDraftPaperTexturePhone = value != null;
			}
		}

		public string DraftPaperTextColor
		{
			get
			{
				return _DraftPaperTextColor;
			}
			set
			{
				_DraftPaperTextColor = value;
				HasDraftPaperTextColor = value != null;
			}
		}

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
			if (HasRewardPaperPrefab)
			{
				hashCode ^= RewardPaperPrefab.GetHashCode();
			}
			if (HasRewardPaperPrefabPhone)
			{
				hashCode ^= RewardPaperPrefabPhone.GetHashCode();
			}
			if (HasDraftPaperTexture)
			{
				hashCode ^= DraftPaperTexture.GetHashCode();
			}
			if (HasDraftPaperTexturePhone)
			{
				hashCode ^= DraftPaperTexturePhone.GetHashCode();
			}
			if (HasDraftPaperTextColor)
			{
				hashCode ^= DraftPaperTextColor.GetHashCode();
			}
			foreach (LocalizedString @string in Strings)
			{
				hashCode ^= @string.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ArenaSeasonSpec arenaSeasonSpec = obj as ArenaSeasonSpec;
			if (arenaSeasonSpec == null)
			{
				return false;
			}
			if (!GameContentSeason.Equals(arenaSeasonSpec.GameContentSeason))
			{
				return false;
			}
			if (HasRewardPaperPrefab != arenaSeasonSpec.HasRewardPaperPrefab || (HasRewardPaperPrefab && !RewardPaperPrefab.Equals(arenaSeasonSpec.RewardPaperPrefab)))
			{
				return false;
			}
			if (HasRewardPaperPrefabPhone != arenaSeasonSpec.HasRewardPaperPrefabPhone || (HasRewardPaperPrefabPhone && !RewardPaperPrefabPhone.Equals(arenaSeasonSpec.RewardPaperPrefabPhone)))
			{
				return false;
			}
			if (HasDraftPaperTexture != arenaSeasonSpec.HasDraftPaperTexture || (HasDraftPaperTexture && !DraftPaperTexture.Equals(arenaSeasonSpec.DraftPaperTexture)))
			{
				return false;
			}
			if (HasDraftPaperTexturePhone != arenaSeasonSpec.HasDraftPaperTexturePhone || (HasDraftPaperTexturePhone && !DraftPaperTexturePhone.Equals(arenaSeasonSpec.DraftPaperTexturePhone)))
			{
				return false;
			}
			if (HasDraftPaperTextColor != arenaSeasonSpec.HasDraftPaperTextColor || (HasDraftPaperTextColor && !DraftPaperTextColor.Equals(arenaSeasonSpec.DraftPaperTextColor)))
			{
				return false;
			}
			if (Strings.Count != arenaSeasonSpec.Strings.Count)
			{
				return false;
			}
			for (int i = 0; i < Strings.Count; i++)
			{
				if (!Strings[i].Equals(arenaSeasonSpec.Strings[i]))
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

		public static ArenaSeasonSpec Deserialize(Stream stream, ArenaSeasonSpec instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ArenaSeasonSpec DeserializeLengthDelimited(Stream stream)
		{
			ArenaSeasonSpec arenaSeasonSpec = new ArenaSeasonSpec();
			DeserializeLengthDelimited(stream, arenaSeasonSpec);
			return arenaSeasonSpec;
		}

		public static ArenaSeasonSpec DeserializeLengthDelimited(Stream stream, ArenaSeasonSpec instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ArenaSeasonSpec Deserialize(Stream stream, ArenaSeasonSpec instance, long limit)
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
				case 18:
					instance.RewardPaperPrefab = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.RewardPaperPrefabPhone = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.DraftPaperTexture = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.DraftPaperTexturePhone = ProtocolParser.ReadString(stream);
					continue;
				case 50:
					instance.DraftPaperTextColor = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ArenaSeasonSpec instance)
		{
			if (instance.GameContentSeason == null)
			{
				throw new ArgumentNullException("GameContentSeason", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameContentSeason.GetSerializedSize());
			GameContentSeasonSpec.Serialize(stream, instance.GameContentSeason);
			if (instance.HasRewardPaperPrefab)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RewardPaperPrefab));
			}
			if (instance.HasRewardPaperPrefabPhone)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RewardPaperPrefabPhone));
			}
			if (instance.HasDraftPaperTexture)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DraftPaperTexture));
			}
			if (instance.HasDraftPaperTexturePhone)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DraftPaperTexturePhone));
			}
			if (instance.HasDraftPaperTextColor)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DraftPaperTextColor));
			}
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
			if (HasRewardPaperPrefab)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(RewardPaperPrefab);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasRewardPaperPrefabPhone)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(RewardPaperPrefabPhone);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasDraftPaperTexture)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(DraftPaperTexture);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasDraftPaperTexturePhone)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(DraftPaperTexturePhone);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasDraftPaperTextColor)
			{
				num++;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(DraftPaperTextColor);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
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
