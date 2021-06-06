using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000150 RID: 336
	public class ScenarioDbRecord : IProtoBuf
	{
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0004BF68 File Offset: 0x0004A168
		// (set) Token: 0x06001639 RID: 5689 RVA: 0x0004BF70 File Offset: 0x0004A170
		public int Id { get; set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0004BF79 File Offset: 0x0004A179
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x0004BF81 File Offset: 0x0004A181
		public string NoteDesc
		{
			get
			{
				return this._NoteDesc;
			}
			set
			{
				this._NoteDesc = value;
				this.HasNoteDesc = (value != null);
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0004BF94 File Offset: 0x0004A194
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x0004BF9C File Offset: 0x0004A19C
		public int NumHumanPlayers { get; set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0004BFA5 File Offset: 0x0004A1A5
		// (set) Token: 0x0600163F RID: 5695 RVA: 0x0004BFAD File Offset: 0x0004A1AD
		public long Player1HeroCardId { get; set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x0004BFB6 File Offset: 0x0004A1B6
		// (set) Token: 0x06001641 RID: 5697 RVA: 0x0004BFBE File Offset: 0x0004A1BE
		public long Player2HeroCardId { get; set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0004BFC7 File Offset: 0x0004A1C7
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x0004BFCF File Offset: 0x0004A1CF
		public bool IsExpert { get; set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0004BFD8 File Offset: 0x0004A1D8
		// (set) Token: 0x06001645 RID: 5701 RVA: 0x0004BFE0 File Offset: 0x0004A1E0
		public int AdventureId { get; set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0004BFE9 File Offset: 0x0004A1E9
		// (set) Token: 0x06001647 RID: 5703 RVA: 0x0004BFF1 File Offset: 0x0004A1F1
		public int AdventureModeId
		{
			get
			{
				return this._AdventureModeId;
			}
			set
			{
				this._AdventureModeId = value;
				this.HasAdventureModeId = true;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0004C001 File Offset: 0x0004A201
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x0004C009 File Offset: 0x0004A209
		public int WingId { get; set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0004C012 File Offset: 0x0004A212
		// (set) Token: 0x0600164B RID: 5707 RVA: 0x0004C01A File Offset: 0x0004A21A
		public int SortOrder { get; set; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0004C023 File Offset: 0x0004A223
		// (set) Token: 0x0600164D RID: 5709 RVA: 0x0004C02B File Offset: 0x0004A22B
		public long ClientPlayer2HeroCardId
		{
			get
			{
				return this._ClientPlayer2HeroCardId;
			}
			set
			{
				this._ClientPlayer2HeroCardId = value;
				this.HasClientPlayer2HeroCardId = true;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0004C03B File Offset: 0x0004A23B
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x0004C043 File Offset: 0x0004A243
		public string TavernBrawlTexture
		{
			get
			{
				return this._TavernBrawlTexture;
			}
			set
			{
				this._TavernBrawlTexture = value;
				this.HasTavernBrawlTexture = (value != null);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0004C056 File Offset: 0x0004A256
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x0004C05E File Offset: 0x0004A25E
		public string TavernBrawlTexturePhone
		{
			get
			{
				return this._TavernBrawlTexturePhone;
			}
			set
			{
				this._TavernBrawlTexturePhone = value;
				this.HasTavernBrawlTexturePhone = (value != null);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x0004C071 File Offset: 0x0004A271
		// (set) Token: 0x06001653 RID: 5715 RVA: 0x0004C079 File Offset: 0x0004A279
		public Vector2 TavernBrawlTexturePhoneOffset
		{
			get
			{
				return this._TavernBrawlTexturePhoneOffset;
			}
			set
			{
				this._TavernBrawlTexturePhoneOffset = value;
				this.HasTavernBrawlTexturePhoneOffset = (value != null);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x0004C08C File Offset: 0x0004A28C
		// (set) Token: 0x06001655 RID: 5717 RVA: 0x0004C094 File Offset: 0x0004A294
		public bool IsCoop
		{
			get
			{
				return this._IsCoop;
			}
			set
			{
				this._IsCoop = value;
				this.HasIsCoop = true;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x0004C0A4 File Offset: 0x0004A2A4
		// (set) Token: 0x06001657 RID: 5719 RVA: 0x0004C0AC File Offset: 0x0004A2AC
		public int DeckRulesetId
		{
			get
			{
				return this._DeckRulesetId;
			}
			set
			{
				this._DeckRulesetId = value;
				this.HasDeckRulesetId = true;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0004C0BC File Offset: 0x0004A2BC
		// (set) Token: 0x06001659 RID: 5721 RVA: 0x0004C0C4 File Offset: 0x0004A2C4
		public RuleType RuleType
		{
			get
			{
				return this._RuleType;
			}
			set
			{
				this._RuleType = value;
				this.HasRuleType = true;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0004C0D4 File Offset: 0x0004A2D4
		// (set) Token: 0x0600165B RID: 5723 RVA: 0x0004C0DC File Offset: 0x0004A2DC
		public string ScriptObject
		{
			get
			{
				return this._ScriptObject;
			}
			set
			{
				this._ScriptObject = value;
				this.HasScriptObject = (value != null);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x0004C0EF File Offset: 0x0004A2EF
		// (set) Token: 0x0600165D RID: 5725 RVA: 0x0004C0F7 File Offset: 0x0004A2F7
		public List<ScenarioGuestHeroDbRecord> GuestHeroes
		{
			get
			{
				return this._GuestHeroes;
			}
			set
			{
				this._GuestHeroes = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0004C100 File Offset: 0x0004A300
		// (set) Token: 0x0600165F RID: 5727 RVA: 0x0004C108 File Offset: 0x0004A308
		public bool OneSimPerPlayer
		{
			get
			{
				return this._OneSimPerPlayer;
			}
			set
			{
				this._OneSimPerPlayer = value;
				this.HasOneSimPerPlayer = true;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x0004C118 File Offset: 0x0004A318
		// (set) Token: 0x06001661 RID: 5729 RVA: 0x0004C120 File Offset: 0x0004A320
		public List<ClassExclusionDbRecord> ClassExclusions
		{
			get
			{
				return this._ClassExclusions;
			}
			set
			{
				this._ClassExclusions = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0004C129 File Offset: 0x0004A329
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x0004C131 File Offset: 0x0004A331
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

		// Token: 0x06001664 RID: 5732 RVA: 0x0004C13C File Offset: 0x0004A33C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			if (this.HasNoteDesc)
			{
				num ^= this.NoteDesc.GetHashCode();
			}
			num ^= this.NumHumanPlayers.GetHashCode();
			num ^= this.Player1HeroCardId.GetHashCode();
			num ^= this.Player2HeroCardId.GetHashCode();
			num ^= this.IsExpert.GetHashCode();
			num ^= this.AdventureId.GetHashCode();
			if (this.HasAdventureModeId)
			{
				num ^= this.AdventureModeId.GetHashCode();
			}
			num ^= this.WingId.GetHashCode();
			num ^= this.SortOrder.GetHashCode();
			if (this.HasClientPlayer2HeroCardId)
			{
				num ^= this.ClientPlayer2HeroCardId.GetHashCode();
			}
			if (this.HasTavernBrawlTexture)
			{
				num ^= this.TavernBrawlTexture.GetHashCode();
			}
			if (this.HasTavernBrawlTexturePhone)
			{
				num ^= this.TavernBrawlTexturePhone.GetHashCode();
			}
			if (this.HasTavernBrawlTexturePhoneOffset)
			{
				num ^= this.TavernBrawlTexturePhoneOffset.GetHashCode();
			}
			if (this.HasIsCoop)
			{
				num ^= this.IsCoop.GetHashCode();
			}
			if (this.HasDeckRulesetId)
			{
				num ^= this.DeckRulesetId.GetHashCode();
			}
			if (this.HasRuleType)
			{
				num ^= this.RuleType.GetHashCode();
			}
			if (this.HasScriptObject)
			{
				num ^= this.ScriptObject.GetHashCode();
			}
			foreach (ScenarioGuestHeroDbRecord scenarioGuestHeroDbRecord in this.GuestHeroes)
			{
				num ^= scenarioGuestHeroDbRecord.GetHashCode();
			}
			if (this.HasOneSimPerPlayer)
			{
				num ^= this.OneSimPerPlayer.GetHashCode();
			}
			foreach (ClassExclusionDbRecord classExclusionDbRecord in this.ClassExclusions)
			{
				num ^= classExclusionDbRecord.GetHashCode();
			}
			foreach (LocalizedString localizedString in this.Strings)
			{
				num ^= localizedString.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0004C3C4 File Offset: 0x0004A5C4
		public override bool Equals(object obj)
		{
			ScenarioDbRecord scenarioDbRecord = obj as ScenarioDbRecord;
			if (scenarioDbRecord == null)
			{
				return false;
			}
			if (!this.Id.Equals(scenarioDbRecord.Id))
			{
				return false;
			}
			if (this.HasNoteDesc != scenarioDbRecord.HasNoteDesc || (this.HasNoteDesc && !this.NoteDesc.Equals(scenarioDbRecord.NoteDesc)))
			{
				return false;
			}
			if (!this.NumHumanPlayers.Equals(scenarioDbRecord.NumHumanPlayers))
			{
				return false;
			}
			if (!this.Player1HeroCardId.Equals(scenarioDbRecord.Player1HeroCardId))
			{
				return false;
			}
			if (!this.Player2HeroCardId.Equals(scenarioDbRecord.Player2HeroCardId))
			{
				return false;
			}
			if (!this.IsExpert.Equals(scenarioDbRecord.IsExpert))
			{
				return false;
			}
			if (!this.AdventureId.Equals(scenarioDbRecord.AdventureId))
			{
				return false;
			}
			if (this.HasAdventureModeId != scenarioDbRecord.HasAdventureModeId || (this.HasAdventureModeId && !this.AdventureModeId.Equals(scenarioDbRecord.AdventureModeId)))
			{
				return false;
			}
			if (!this.WingId.Equals(scenarioDbRecord.WingId))
			{
				return false;
			}
			if (!this.SortOrder.Equals(scenarioDbRecord.SortOrder))
			{
				return false;
			}
			if (this.HasClientPlayer2HeroCardId != scenarioDbRecord.HasClientPlayer2HeroCardId || (this.HasClientPlayer2HeroCardId && !this.ClientPlayer2HeroCardId.Equals(scenarioDbRecord.ClientPlayer2HeroCardId)))
			{
				return false;
			}
			if (this.HasTavernBrawlTexture != scenarioDbRecord.HasTavernBrawlTexture || (this.HasTavernBrawlTexture && !this.TavernBrawlTexture.Equals(scenarioDbRecord.TavernBrawlTexture)))
			{
				return false;
			}
			if (this.HasTavernBrawlTexturePhone != scenarioDbRecord.HasTavernBrawlTexturePhone || (this.HasTavernBrawlTexturePhone && !this.TavernBrawlTexturePhone.Equals(scenarioDbRecord.TavernBrawlTexturePhone)))
			{
				return false;
			}
			if (this.HasTavernBrawlTexturePhoneOffset != scenarioDbRecord.HasTavernBrawlTexturePhoneOffset || (this.HasTavernBrawlTexturePhoneOffset && !this.TavernBrawlTexturePhoneOffset.Equals(scenarioDbRecord.TavernBrawlTexturePhoneOffset)))
			{
				return false;
			}
			if (this.HasIsCoop != scenarioDbRecord.HasIsCoop || (this.HasIsCoop && !this.IsCoop.Equals(scenarioDbRecord.IsCoop)))
			{
				return false;
			}
			if (this.HasDeckRulesetId != scenarioDbRecord.HasDeckRulesetId || (this.HasDeckRulesetId && !this.DeckRulesetId.Equals(scenarioDbRecord.DeckRulesetId)))
			{
				return false;
			}
			if (this.HasRuleType != scenarioDbRecord.HasRuleType || (this.HasRuleType && !this.RuleType.Equals(scenarioDbRecord.RuleType)))
			{
				return false;
			}
			if (this.HasScriptObject != scenarioDbRecord.HasScriptObject || (this.HasScriptObject && !this.ScriptObject.Equals(scenarioDbRecord.ScriptObject)))
			{
				return false;
			}
			if (this.GuestHeroes.Count != scenarioDbRecord.GuestHeroes.Count)
			{
				return false;
			}
			for (int i = 0; i < this.GuestHeroes.Count; i++)
			{
				if (!this.GuestHeroes[i].Equals(scenarioDbRecord.GuestHeroes[i]))
				{
					return false;
				}
			}
			if (this.HasOneSimPerPlayer != scenarioDbRecord.HasOneSimPerPlayer || (this.HasOneSimPerPlayer && !this.OneSimPerPlayer.Equals(scenarioDbRecord.OneSimPerPlayer)))
			{
				return false;
			}
			if (this.ClassExclusions.Count != scenarioDbRecord.ClassExclusions.Count)
			{
				return false;
			}
			for (int j = 0; j < this.ClassExclusions.Count; j++)
			{
				if (!this.ClassExclusions[j].Equals(scenarioDbRecord.ClassExclusions[j]))
				{
					return false;
				}
			}
			if (this.Strings.Count != scenarioDbRecord.Strings.Count)
			{
				return false;
			}
			for (int k = 0; k < this.Strings.Count; k++)
			{
				if (!this.Strings[k].Equals(scenarioDbRecord.Strings[k]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0004C79A File Offset: 0x0004A99A
		public void Deserialize(Stream stream)
		{
			ScenarioDbRecord.Deserialize(stream, this);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0004C7A4 File Offset: 0x0004A9A4
		public static ScenarioDbRecord Deserialize(Stream stream, ScenarioDbRecord instance)
		{
			return ScenarioDbRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0004C7B0 File Offset: 0x0004A9B0
		public static ScenarioDbRecord DeserializeLengthDelimited(Stream stream)
		{
			ScenarioDbRecord scenarioDbRecord = new ScenarioDbRecord();
			ScenarioDbRecord.DeserializeLengthDelimited(stream, scenarioDbRecord);
			return scenarioDbRecord;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0004C7CC File Offset: 0x0004A9CC
		public static ScenarioDbRecord DeserializeLengthDelimited(Stream stream, ScenarioDbRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ScenarioDbRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0004C7F4 File Offset: 0x0004A9F4
		public static ScenarioDbRecord Deserialize(Stream stream, ScenarioDbRecord instance, long limit)
		{
			instance.RuleType = RuleType.RULE_NONE;
			if (instance.GuestHeroes == null)
			{
				instance.GuestHeroes = new List<ScenarioGuestHeroDbRecord>();
			}
			if (instance.ClassExclusions == null)
			{
				instance.ClassExclusions = new List<ClassExclusionDbRecord>();
			}
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
					if (num <= 56)
					{
						if (num <= 24)
						{
							if (num == 8)
							{
								instance.Id = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 18)
							{
								instance.NoteDesc = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 24)
							{
								instance.NumHumanPlayers = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else if (num <= 40)
						{
							if (num == 32)
							{
								instance.Player1HeroCardId = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 40)
							{
								instance.Player2HeroCardId = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 48)
							{
								instance.IsExpert = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 56)
							{
								instance.AdventureId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 88)
					{
						if (num <= 72)
						{
							if (num == 64)
							{
								instance.AdventureModeId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 72)
							{
								instance.WingId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 80)
							{
								instance.SortOrder = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 88)
							{
								instance.ClientPlayer2HeroCardId = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 106)
					{
						if (num == 98)
						{
							instance.TavernBrawlTexture = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 106)
						{
							instance.TavernBrawlTexturePhone = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else if (num != 114)
					{
						if (num == 120)
						{
							instance.IsCoop = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (instance.TavernBrawlTexturePhoneOffset == null)
						{
							instance.TavernBrawlTexturePhoneOffset = Vector2.DeserializeLengthDelimited(stream);
							continue;
						}
						Vector2.DeserializeLengthDelimited(stream, instance.TavernBrawlTexturePhoneOffset);
						continue;
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					switch (field)
					{
					case 16U:
						if (key.WireType == Wire.Varint)
						{
							instance.DeckRulesetId = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 17U:
						if (key.WireType == Wire.Varint)
						{
							instance.RuleType = (RuleType)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 18U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ScriptObject = ProtocolParser.ReadString(stream);
						}
						break;
					case 19U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.GuestHeroes.Add(ScenarioGuestHeroDbRecord.DeserializeLengthDelimited(stream));
						}
						break;
					case 20U:
						if (key.WireType == Wire.Varint)
						{
							instance.OneSimPerPlayer = ProtocolParser.ReadBool(stream);
						}
						break;
					case 21U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ClassExclusions.Add(ClassExclusionDbRecord.DeserializeLengthDelimited(stream));
						}
						break;
					default:
						if (field != 100U)
						{
							ProtocolParser.SkipKey(stream, key);
						}
						else if (key.WireType == Wire.LengthDelimited)
						{
							instance.Strings.Add(LocalizedString.DeserializeLengthDelimited(stream));
						}
						break;
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0004CB88 File Offset: 0x0004AD88
		public void Serialize(Stream stream)
		{
			ScenarioDbRecord.Serialize(stream, this);
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0004CB94 File Offset: 0x0004AD94
		public static void Serialize(Stream stream, ScenarioDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			if (instance.HasNoteDesc)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.NoteDesc));
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NumHumanPlayers));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Player1HeroCardId);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Player2HeroCardId);
			stream.WriteByte(48);
			ProtocolParser.WriteBool(stream, instance.IsExpert);
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AdventureId));
			if (instance.HasAdventureModeId)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AdventureModeId));
			}
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.WingId));
			stream.WriteByte(80);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SortOrder));
			if (instance.HasClientPlayer2HeroCardId)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientPlayer2HeroCardId);
			}
			if (instance.HasTavernBrawlTexture)
			{
				stream.WriteByte(98);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TavernBrawlTexture));
			}
			if (instance.HasTavernBrawlTexturePhone)
			{
				stream.WriteByte(106);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TavernBrawlTexturePhone));
			}
			if (instance.HasTavernBrawlTexturePhoneOffset)
			{
				stream.WriteByte(114);
				ProtocolParser.WriteUInt32(stream, instance.TavernBrawlTexturePhoneOffset.GetSerializedSize());
				Vector2.Serialize(stream, instance.TavernBrawlTexturePhoneOffset);
			}
			if (instance.HasIsCoop)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteBool(stream, instance.IsCoop);
			}
			if (instance.HasDeckRulesetId)
			{
				stream.WriteByte(128);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeckRulesetId));
			}
			if (instance.HasRuleType)
			{
				stream.WriteByte(136);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RuleType));
			}
			if (instance.HasScriptObject)
			{
				stream.WriteByte(146);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ScriptObject));
			}
			if (instance.GuestHeroes.Count > 0)
			{
				foreach (ScenarioGuestHeroDbRecord scenarioGuestHeroDbRecord in instance.GuestHeroes)
				{
					stream.WriteByte(154);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, scenarioGuestHeroDbRecord.GetSerializedSize());
					ScenarioGuestHeroDbRecord.Serialize(stream, scenarioGuestHeroDbRecord);
				}
			}
			if (instance.HasOneSimPerPlayer)
			{
				stream.WriteByte(160);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.OneSimPerPlayer);
			}
			if (instance.ClassExclusions.Count > 0)
			{
				foreach (ClassExclusionDbRecord classExclusionDbRecord in instance.ClassExclusions)
				{
					stream.WriteByte(170);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, classExclusionDbRecord.GetSerializedSize());
					ClassExclusionDbRecord.Serialize(stream, classExclusionDbRecord);
				}
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

		// Token: 0x0600166D RID: 5741 RVA: 0x0004CF30 File Offset: 0x0004B130
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			if (this.HasNoteDesc)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.NoteDesc);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NumHumanPlayers));
			num += ProtocolParser.SizeOfUInt64((ulong)this.Player1HeroCardId);
			num += ProtocolParser.SizeOfUInt64((ulong)this.Player2HeroCardId);
			num += 1U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AdventureId));
			if (this.HasAdventureModeId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AdventureModeId));
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.WingId));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SortOrder));
			if (this.HasClientPlayer2HeroCardId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ClientPlayer2HeroCardId);
			}
			if (this.HasTavernBrawlTexture)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.TavernBrawlTexture);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasTavernBrawlTexturePhone)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.TavernBrawlTexturePhone);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasTavernBrawlTexturePhoneOffset)
			{
				num += 1U;
				uint serializedSize = this.TavernBrawlTexturePhoneOffset.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasIsCoop)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasDeckRulesetId)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeckRulesetId));
			}
			if (this.HasRuleType)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RuleType));
			}
			if (this.HasScriptObject)
			{
				num += 2U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.ScriptObject);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.GuestHeroes.Count > 0)
			{
				foreach (ScenarioGuestHeroDbRecord scenarioGuestHeroDbRecord in this.GuestHeroes)
				{
					num += 2U;
					uint serializedSize2 = scenarioGuestHeroDbRecord.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasOneSimPerPlayer)
			{
				num += 2U;
				num += 1U;
			}
			if (this.ClassExclusions.Count > 0)
			{
				foreach (ClassExclusionDbRecord classExclusionDbRecord in this.ClassExclusions)
				{
					num += 2U;
					uint serializedSize3 = classExclusionDbRecord.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.Strings.Count > 0)
			{
				foreach (LocalizedString localizedString in this.Strings)
				{
					num += 2U;
					uint serializedSize4 = localizedString.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			num += 8U;
			return num;
		}

		// Token: 0x040006CA RID: 1738
		public bool HasNoteDesc;

		// Token: 0x040006CB RID: 1739
		private string _NoteDesc;

		// Token: 0x040006D1 RID: 1745
		public bool HasAdventureModeId;

		// Token: 0x040006D2 RID: 1746
		private int _AdventureModeId;

		// Token: 0x040006D5 RID: 1749
		public bool HasClientPlayer2HeroCardId;

		// Token: 0x040006D6 RID: 1750
		private long _ClientPlayer2HeroCardId;

		// Token: 0x040006D7 RID: 1751
		public bool HasTavernBrawlTexture;

		// Token: 0x040006D8 RID: 1752
		private string _TavernBrawlTexture;

		// Token: 0x040006D9 RID: 1753
		public bool HasTavernBrawlTexturePhone;

		// Token: 0x040006DA RID: 1754
		private string _TavernBrawlTexturePhone;

		// Token: 0x040006DB RID: 1755
		public bool HasTavernBrawlTexturePhoneOffset;

		// Token: 0x040006DC RID: 1756
		private Vector2 _TavernBrawlTexturePhoneOffset;

		// Token: 0x040006DD RID: 1757
		public bool HasIsCoop;

		// Token: 0x040006DE RID: 1758
		private bool _IsCoop;

		// Token: 0x040006DF RID: 1759
		public bool HasDeckRulesetId;

		// Token: 0x040006E0 RID: 1760
		private int _DeckRulesetId;

		// Token: 0x040006E1 RID: 1761
		public bool HasRuleType;

		// Token: 0x040006E2 RID: 1762
		private RuleType _RuleType;

		// Token: 0x040006E3 RID: 1763
		public bool HasScriptObject;

		// Token: 0x040006E4 RID: 1764
		private string _ScriptObject;

		// Token: 0x040006E5 RID: 1765
		private List<ScenarioGuestHeroDbRecord> _GuestHeroes = new List<ScenarioGuestHeroDbRecord>();

		// Token: 0x040006E6 RID: 1766
		public bool HasOneSimPerPlayer;

		// Token: 0x040006E7 RID: 1767
		private bool _OneSimPerPlayer;

		// Token: 0x040006E8 RID: 1768
		private List<ClassExclusionDbRecord> _ClassExclusions = new List<ClassExclusionDbRecord>();

		// Token: 0x040006E9 RID: 1769
		private List<LocalizedString> _Strings = new List<LocalizedString>();
	}
}
