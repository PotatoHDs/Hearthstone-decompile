using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x0200015D RID: 349
	public class ArenaSeasonSpec : IProtoBuf
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x00052B17 File Offset: 0x00050D17
		// (set) Token: 0x060017BA RID: 6074 RVA: 0x00052B1F File Offset: 0x00050D1F
		public GameContentSeasonSpec GameContentSeason { get; set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x00052B28 File Offset: 0x00050D28
		// (set) Token: 0x060017BC RID: 6076 RVA: 0x00052B30 File Offset: 0x00050D30
		public string RewardPaperPrefab
		{
			get
			{
				return this._RewardPaperPrefab;
			}
			set
			{
				this._RewardPaperPrefab = value;
				this.HasRewardPaperPrefab = (value != null);
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x00052B43 File Offset: 0x00050D43
		// (set) Token: 0x060017BE RID: 6078 RVA: 0x00052B4B File Offset: 0x00050D4B
		public string RewardPaperPrefabPhone
		{
			get
			{
				return this._RewardPaperPrefabPhone;
			}
			set
			{
				this._RewardPaperPrefabPhone = value;
				this.HasRewardPaperPrefabPhone = (value != null);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x00052B5E File Offset: 0x00050D5E
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x00052B66 File Offset: 0x00050D66
		public string DraftPaperTexture
		{
			get
			{
				return this._DraftPaperTexture;
			}
			set
			{
				this._DraftPaperTexture = value;
				this.HasDraftPaperTexture = (value != null);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x00052B79 File Offset: 0x00050D79
		// (set) Token: 0x060017C2 RID: 6082 RVA: 0x00052B81 File Offset: 0x00050D81
		public string DraftPaperTexturePhone
		{
			get
			{
				return this._DraftPaperTexturePhone;
			}
			set
			{
				this._DraftPaperTexturePhone = value;
				this.HasDraftPaperTexturePhone = (value != null);
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x00052B94 File Offset: 0x00050D94
		// (set) Token: 0x060017C4 RID: 6084 RVA: 0x00052B9C File Offset: 0x00050D9C
		public string DraftPaperTextColor
		{
			get
			{
				return this._DraftPaperTextColor;
			}
			set
			{
				this._DraftPaperTextColor = value;
				this.HasDraftPaperTextColor = (value != null);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x00052BAF File Offset: 0x00050DAF
		// (set) Token: 0x060017C6 RID: 6086 RVA: 0x00052BB7 File Offset: 0x00050DB7
		public List<LocalizedString> Strings
		{
			get
			{
				return this._Strings;
			}
			set
			{
				this._Strings = value;
			}
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00052BC0 File Offset: 0x00050DC0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameContentSeason.GetHashCode();
			if (this.HasRewardPaperPrefab)
			{
				num ^= this.RewardPaperPrefab.GetHashCode();
			}
			if (this.HasRewardPaperPrefabPhone)
			{
				num ^= this.RewardPaperPrefabPhone.GetHashCode();
			}
			if (this.HasDraftPaperTexture)
			{
				num ^= this.DraftPaperTexture.GetHashCode();
			}
			if (this.HasDraftPaperTexturePhone)
			{
				num ^= this.DraftPaperTexturePhone.GetHashCode();
			}
			if (this.HasDraftPaperTextColor)
			{
				num ^= this.DraftPaperTextColor.GetHashCode();
			}
			foreach (LocalizedString localizedString in this.Strings)
			{
				num ^= localizedString.GetHashCode();
			}
			return num;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00052CA0 File Offset: 0x00050EA0
		public override bool Equals(object obj)
		{
			ArenaSeasonSpec arenaSeasonSpec = obj as ArenaSeasonSpec;
			if (arenaSeasonSpec == null)
			{
				return false;
			}
			if (!this.GameContentSeason.Equals(arenaSeasonSpec.GameContentSeason))
			{
				return false;
			}
			if (this.HasRewardPaperPrefab != arenaSeasonSpec.HasRewardPaperPrefab || (this.HasRewardPaperPrefab && !this.RewardPaperPrefab.Equals(arenaSeasonSpec.RewardPaperPrefab)))
			{
				return false;
			}
			if (this.HasRewardPaperPrefabPhone != arenaSeasonSpec.HasRewardPaperPrefabPhone || (this.HasRewardPaperPrefabPhone && !this.RewardPaperPrefabPhone.Equals(arenaSeasonSpec.RewardPaperPrefabPhone)))
			{
				return false;
			}
			if (this.HasDraftPaperTexture != arenaSeasonSpec.HasDraftPaperTexture || (this.HasDraftPaperTexture && !this.DraftPaperTexture.Equals(arenaSeasonSpec.DraftPaperTexture)))
			{
				return false;
			}
			if (this.HasDraftPaperTexturePhone != arenaSeasonSpec.HasDraftPaperTexturePhone || (this.HasDraftPaperTexturePhone && !this.DraftPaperTexturePhone.Equals(arenaSeasonSpec.DraftPaperTexturePhone)))
			{
				return false;
			}
			if (this.HasDraftPaperTextColor != arenaSeasonSpec.HasDraftPaperTextColor || (this.HasDraftPaperTextColor && !this.DraftPaperTextColor.Equals(arenaSeasonSpec.DraftPaperTextColor)))
			{
				return false;
			}
			if (this.Strings.Count != arenaSeasonSpec.Strings.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Strings.Count; i++)
			{
				if (!this.Strings[i].Equals(arenaSeasonSpec.Strings[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00052DF7 File Offset: 0x00050FF7
		public void Deserialize(Stream stream)
		{
			ArenaSeasonSpec.Deserialize(stream, this);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00052E01 File Offset: 0x00051001
		public static ArenaSeasonSpec Deserialize(Stream stream, ArenaSeasonSpec instance)
		{
			return ArenaSeasonSpec.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00052E0C File Offset: 0x0005100C
		public static ArenaSeasonSpec DeserializeLengthDelimited(Stream stream)
		{
			ArenaSeasonSpec arenaSeasonSpec = new ArenaSeasonSpec();
			ArenaSeasonSpec.DeserializeLengthDelimited(stream, arenaSeasonSpec);
			return arenaSeasonSpec;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00052E28 File Offset: 0x00051028
		public static ArenaSeasonSpec DeserializeLengthDelimited(Stream stream, ArenaSeasonSpec instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ArenaSeasonSpec.Deserialize(stream, instance, num);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00052E50 File Offset: 0x00051050
		public static ArenaSeasonSpec Deserialize(Stream stream, ArenaSeasonSpec instance, long limit)
		{
			if (instance.Strings == null)
			{
				instance.Strings = new List<LocalizedString>();
			}
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.RewardPaperPrefab = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 26)
							{
								instance.RewardPaperPrefabPhone = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.GameContentSeason == null)
							{
								instance.GameContentSeason = GameContentSeasonSpec.DeserializeLengthDelimited(stream);
								continue;
							}
							GameContentSeasonSpec.DeserializeLengthDelimited(stream, instance.GameContentSeason);
							continue;
						}
					}
					else
					{
						if (num == 34)
						{
							instance.DraftPaperTexture = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 42)
						{
							instance.DraftPaperTexturePhone = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 50)
						{
							instance.DraftPaperTextColor = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 100U)
					{
						ProtocolParser.SkipKey(stream, key);
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						instance.Strings.Add(LocalizedString.DeserializeLengthDelimited(stream));
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00052FAF File Offset: 0x000511AF
		public void Serialize(Stream stream)
		{
			ArenaSeasonSpec.Serialize(stream, this);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00052FB8 File Offset: 0x000511B8
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
			if (instance.Strings.Count > 0)
			{
				foreach (LocalizedString localizedString in instance.Strings)
				{
					stream.WriteByte(162);
					stream.WriteByte(6);
					ProtocolParser.WriteUInt32(stream, localizedString.GetSerializedSize());
					LocalizedString.Serialize(stream, localizedString);
				}
			}
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00053134 File Offset: 0x00051334
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameContentSeason.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasRewardPaperPrefab)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.RewardPaperPrefab);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasRewardPaperPrefabPhone)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.RewardPaperPrefabPhone);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasDraftPaperTexture)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.DraftPaperTexture);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasDraftPaperTexturePhone)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.DraftPaperTexturePhone);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasDraftPaperTextColor)
			{
				num += 1U;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.DraftPaperTextColor);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (this.Strings.Count > 0)
			{
				foreach (LocalizedString localizedString in this.Strings)
				{
					num += 2U;
					uint serializedSize2 = localizedString.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000786 RID: 1926
		public bool HasRewardPaperPrefab;

		// Token: 0x04000787 RID: 1927
		private string _RewardPaperPrefab;

		// Token: 0x04000788 RID: 1928
		public bool HasRewardPaperPrefabPhone;

		// Token: 0x04000789 RID: 1929
		private string _RewardPaperPrefabPhone;

		// Token: 0x0400078A RID: 1930
		public bool HasDraftPaperTexture;

		// Token: 0x0400078B RID: 1931
		private string _DraftPaperTexture;

		// Token: 0x0400078C RID: 1932
		public bool HasDraftPaperTexturePhone;

		// Token: 0x0400078D RID: 1933
		private string _DraftPaperTexturePhone;

		// Token: 0x0400078E RID: 1934
		public bool HasDraftPaperTextColor;

		// Token: 0x0400078F RID: 1935
		private string _DraftPaperTextColor;

		// Token: 0x04000790 RID: 1936
		private List<LocalizedString> _Strings = new List<LocalizedString>();
	}
}
