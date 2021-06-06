using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	public class ScenarioDbRecord : IProtoBuf
	{
		public bool HasNoteDesc;

		private string _NoteDesc;

		public bool HasAdventureModeId;

		private int _AdventureModeId;

		public bool HasClientPlayer2HeroCardId;

		private long _ClientPlayer2HeroCardId;

		public bool HasTavernBrawlTexture;

		private string _TavernBrawlTexture;

		public bool HasTavernBrawlTexturePhone;

		private string _TavernBrawlTexturePhone;

		public bool HasTavernBrawlTexturePhoneOffset;

		private Vector2 _TavernBrawlTexturePhoneOffset;

		public bool HasIsCoop;

		private bool _IsCoop;

		public bool HasDeckRulesetId;

		private int _DeckRulesetId;

		public bool HasRuleType;

		private RuleType _RuleType;

		public bool HasScriptObject;

		private string _ScriptObject;

		private List<ScenarioGuestHeroDbRecord> _GuestHeroes = new List<ScenarioGuestHeroDbRecord>();

		public bool HasOneSimPerPlayer;

		private bool _OneSimPerPlayer;

		private List<ClassExclusionDbRecord> _ClassExclusions = new List<ClassExclusionDbRecord>();

		private List<LocalizedString> _Strings = new List<LocalizedString>();

		public int Id { get; set; }

		public string NoteDesc
		{
			get
			{
				return _NoteDesc;
			}
			set
			{
				_NoteDesc = value;
				HasNoteDesc = value != null;
			}
		}

		public int NumHumanPlayers { get; set; }

		public long Player1HeroCardId { get; set; }

		public long Player2HeroCardId { get; set; }

		public bool IsExpert { get; set; }

		public int AdventureId { get; set; }

		public int AdventureModeId
		{
			get
			{
				return _AdventureModeId;
			}
			set
			{
				_AdventureModeId = value;
				HasAdventureModeId = true;
			}
		}

		public int WingId { get; set; }

		public int SortOrder { get; set; }

		public long ClientPlayer2HeroCardId
		{
			get
			{
				return _ClientPlayer2HeroCardId;
			}
			set
			{
				_ClientPlayer2HeroCardId = value;
				HasClientPlayer2HeroCardId = true;
			}
		}

		public string TavernBrawlTexture
		{
			get
			{
				return _TavernBrawlTexture;
			}
			set
			{
				_TavernBrawlTexture = value;
				HasTavernBrawlTexture = value != null;
			}
		}

		public string TavernBrawlTexturePhone
		{
			get
			{
				return _TavernBrawlTexturePhone;
			}
			set
			{
				_TavernBrawlTexturePhone = value;
				HasTavernBrawlTexturePhone = value != null;
			}
		}

		public Vector2 TavernBrawlTexturePhoneOffset
		{
			get
			{
				return _TavernBrawlTexturePhoneOffset;
			}
			set
			{
				_TavernBrawlTexturePhoneOffset = value;
				HasTavernBrawlTexturePhoneOffset = value != null;
			}
		}

		public bool IsCoop
		{
			get
			{
				return _IsCoop;
			}
			set
			{
				_IsCoop = value;
				HasIsCoop = true;
			}
		}

		public int DeckRulesetId
		{
			get
			{
				return _DeckRulesetId;
			}
			set
			{
				_DeckRulesetId = value;
				HasDeckRulesetId = true;
			}
		}

		public RuleType RuleType
		{
			get
			{
				return _RuleType;
			}
			set
			{
				_RuleType = value;
				HasRuleType = true;
			}
		}

		public string ScriptObject
		{
			get
			{
				return _ScriptObject;
			}
			set
			{
				_ScriptObject = value;
				HasScriptObject = value != null;
			}
		}

		public List<ScenarioGuestHeroDbRecord> GuestHeroes
		{
			get
			{
				return _GuestHeroes;
			}
			set
			{
				_GuestHeroes = value;
			}
		}

		public bool OneSimPerPlayer
		{
			get
			{
				return _OneSimPerPlayer;
			}
			set
			{
				_OneSimPerPlayer = value;
				HasOneSimPerPlayer = true;
			}
		}

		public List<ClassExclusionDbRecord> ClassExclusions
		{
			get
			{
				return _ClassExclusions;
			}
			set
			{
				_ClassExclusions = value;
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
			hashCode ^= Id.GetHashCode();
			if (HasNoteDesc)
			{
				hashCode ^= NoteDesc.GetHashCode();
			}
			hashCode ^= NumHumanPlayers.GetHashCode();
			hashCode ^= Player1HeroCardId.GetHashCode();
			hashCode ^= Player2HeroCardId.GetHashCode();
			hashCode ^= IsExpert.GetHashCode();
			hashCode ^= AdventureId.GetHashCode();
			if (HasAdventureModeId)
			{
				hashCode ^= AdventureModeId.GetHashCode();
			}
			hashCode ^= WingId.GetHashCode();
			hashCode ^= SortOrder.GetHashCode();
			if (HasClientPlayer2HeroCardId)
			{
				hashCode ^= ClientPlayer2HeroCardId.GetHashCode();
			}
			if (HasTavernBrawlTexture)
			{
				hashCode ^= TavernBrawlTexture.GetHashCode();
			}
			if (HasTavernBrawlTexturePhone)
			{
				hashCode ^= TavernBrawlTexturePhone.GetHashCode();
			}
			if (HasTavernBrawlTexturePhoneOffset)
			{
				hashCode ^= TavernBrawlTexturePhoneOffset.GetHashCode();
			}
			if (HasIsCoop)
			{
				hashCode ^= IsCoop.GetHashCode();
			}
			if (HasDeckRulesetId)
			{
				hashCode ^= DeckRulesetId.GetHashCode();
			}
			if (HasRuleType)
			{
				hashCode ^= RuleType.GetHashCode();
			}
			if (HasScriptObject)
			{
				hashCode ^= ScriptObject.GetHashCode();
			}
			foreach (ScenarioGuestHeroDbRecord guestHero in GuestHeroes)
			{
				hashCode ^= guestHero.GetHashCode();
			}
			if (HasOneSimPerPlayer)
			{
				hashCode ^= OneSimPerPlayer.GetHashCode();
			}
			foreach (ClassExclusionDbRecord classExclusion in ClassExclusions)
			{
				hashCode ^= classExclusion.GetHashCode();
			}
			foreach (LocalizedString @string in Strings)
			{
				hashCode ^= @string.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ScenarioDbRecord scenarioDbRecord = obj as ScenarioDbRecord;
			if (scenarioDbRecord == null)
			{
				return false;
			}
			if (!Id.Equals(scenarioDbRecord.Id))
			{
				return false;
			}
			if (HasNoteDesc != scenarioDbRecord.HasNoteDesc || (HasNoteDesc && !NoteDesc.Equals(scenarioDbRecord.NoteDesc)))
			{
				return false;
			}
			if (!NumHumanPlayers.Equals(scenarioDbRecord.NumHumanPlayers))
			{
				return false;
			}
			if (!Player1HeroCardId.Equals(scenarioDbRecord.Player1HeroCardId))
			{
				return false;
			}
			if (!Player2HeroCardId.Equals(scenarioDbRecord.Player2HeroCardId))
			{
				return false;
			}
			if (!IsExpert.Equals(scenarioDbRecord.IsExpert))
			{
				return false;
			}
			if (!AdventureId.Equals(scenarioDbRecord.AdventureId))
			{
				return false;
			}
			if (HasAdventureModeId != scenarioDbRecord.HasAdventureModeId || (HasAdventureModeId && !AdventureModeId.Equals(scenarioDbRecord.AdventureModeId)))
			{
				return false;
			}
			if (!WingId.Equals(scenarioDbRecord.WingId))
			{
				return false;
			}
			if (!SortOrder.Equals(scenarioDbRecord.SortOrder))
			{
				return false;
			}
			if (HasClientPlayer2HeroCardId != scenarioDbRecord.HasClientPlayer2HeroCardId || (HasClientPlayer2HeroCardId && !ClientPlayer2HeroCardId.Equals(scenarioDbRecord.ClientPlayer2HeroCardId)))
			{
				return false;
			}
			if (HasTavernBrawlTexture != scenarioDbRecord.HasTavernBrawlTexture || (HasTavernBrawlTexture && !TavernBrawlTexture.Equals(scenarioDbRecord.TavernBrawlTexture)))
			{
				return false;
			}
			if (HasTavernBrawlTexturePhone != scenarioDbRecord.HasTavernBrawlTexturePhone || (HasTavernBrawlTexturePhone && !TavernBrawlTexturePhone.Equals(scenarioDbRecord.TavernBrawlTexturePhone)))
			{
				return false;
			}
			if (HasTavernBrawlTexturePhoneOffset != scenarioDbRecord.HasTavernBrawlTexturePhoneOffset || (HasTavernBrawlTexturePhoneOffset && !TavernBrawlTexturePhoneOffset.Equals(scenarioDbRecord.TavernBrawlTexturePhoneOffset)))
			{
				return false;
			}
			if (HasIsCoop != scenarioDbRecord.HasIsCoop || (HasIsCoop && !IsCoop.Equals(scenarioDbRecord.IsCoop)))
			{
				return false;
			}
			if (HasDeckRulesetId != scenarioDbRecord.HasDeckRulesetId || (HasDeckRulesetId && !DeckRulesetId.Equals(scenarioDbRecord.DeckRulesetId)))
			{
				return false;
			}
			if (HasRuleType != scenarioDbRecord.HasRuleType || (HasRuleType && !RuleType.Equals(scenarioDbRecord.RuleType)))
			{
				return false;
			}
			if (HasScriptObject != scenarioDbRecord.HasScriptObject || (HasScriptObject && !ScriptObject.Equals(scenarioDbRecord.ScriptObject)))
			{
				return false;
			}
			if (GuestHeroes.Count != scenarioDbRecord.GuestHeroes.Count)
			{
				return false;
			}
			for (int i = 0; i < GuestHeroes.Count; i++)
			{
				if (!GuestHeroes[i].Equals(scenarioDbRecord.GuestHeroes[i]))
				{
					return false;
				}
			}
			if (HasOneSimPerPlayer != scenarioDbRecord.HasOneSimPerPlayer || (HasOneSimPerPlayer && !OneSimPerPlayer.Equals(scenarioDbRecord.OneSimPerPlayer)))
			{
				return false;
			}
			if (ClassExclusions.Count != scenarioDbRecord.ClassExclusions.Count)
			{
				return false;
			}
			for (int j = 0; j < ClassExclusions.Count; j++)
			{
				if (!ClassExclusions[j].Equals(scenarioDbRecord.ClassExclusions[j]))
				{
					return false;
				}
			}
			if (Strings.Count != scenarioDbRecord.Strings.Count)
			{
				return false;
			}
			for (int k = 0; k < Strings.Count; k++)
			{
				if (!Strings[k].Equals(scenarioDbRecord.Strings[k]))
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

		public static ScenarioDbRecord Deserialize(Stream stream, ScenarioDbRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ScenarioDbRecord DeserializeLengthDelimited(Stream stream)
		{
			ScenarioDbRecord scenarioDbRecord = new ScenarioDbRecord();
			DeserializeLengthDelimited(stream, scenarioDbRecord);
			return scenarioDbRecord;
		}

		public static ScenarioDbRecord DeserializeLengthDelimited(Stream stream, ScenarioDbRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

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
				case 18:
					instance.NoteDesc = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.NumHumanPlayers = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Player1HeroCardId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.Player2HeroCardId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.IsExpert = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.AdventureId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.AdventureModeId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.WingId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 80:
					instance.SortOrder = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 88:
					instance.ClientPlayer2HeroCardId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 98:
					instance.TavernBrawlTexture = ProtocolParser.ReadString(stream);
					continue;
				case 106:
					instance.TavernBrawlTexturePhone = ProtocolParser.ReadString(stream);
					continue;
				case 114:
					if (instance.TavernBrawlTexturePhoneOffset == null)
					{
						instance.TavernBrawlTexturePhoneOffset = Vector2.DeserializeLengthDelimited(stream);
					}
					else
					{
						Vector2.DeserializeLengthDelimited(stream, instance.TavernBrawlTexturePhoneOffset);
					}
					continue;
				case 120:
					instance.IsCoop = ProtocolParser.ReadBool(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 16u:
						if (key.WireType == Wire.Varint)
						{
							instance.DeckRulesetId = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 17u:
						if (key.WireType == Wire.Varint)
						{
							instance.RuleType = (RuleType)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 18u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ScriptObject = ProtocolParser.ReadString(stream);
						}
						break;
					case 19u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.GuestHeroes.Add(ScenarioGuestHeroDbRecord.DeserializeLengthDelimited(stream));
						}
						break;
					case 20u:
						if (key.WireType == Wire.Varint)
						{
							instance.OneSimPerPlayer = ProtocolParser.ReadBool(stream);
						}
						break;
					case 21u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ClassExclusions.Add(ClassExclusionDbRecord.DeserializeLengthDelimited(stream));
						}
						break;
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

		public static void Serialize(Stream stream, ScenarioDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			if (instance.HasNoteDesc)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.NoteDesc));
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.NumHumanPlayers);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Player1HeroCardId);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Player2HeroCardId);
			stream.WriteByte(48);
			ProtocolParser.WriteBool(stream, instance.IsExpert);
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.AdventureId);
			if (instance.HasAdventureModeId)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AdventureModeId);
			}
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.WingId);
			stream.WriteByte(80);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SortOrder);
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckRulesetId);
			}
			if (instance.HasRuleType)
			{
				stream.WriteByte(136);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RuleType);
			}
			if (instance.HasScriptObject)
			{
				stream.WriteByte(146);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ScriptObject));
			}
			if (instance.GuestHeroes.Count > 0)
			{
				foreach (ScenarioGuestHeroDbRecord guestHero in instance.GuestHeroes)
				{
					stream.WriteByte(154);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, guestHero.GetSerializedSize());
					ScenarioGuestHeroDbRecord.Serialize(stream, guestHero);
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
				foreach (ClassExclusionDbRecord classExclusion in instance.ClassExclusions)
				{
					stream.WriteByte(170);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, classExclusion.GetSerializedSize());
					ClassExclusionDbRecord.Serialize(stream, classExclusion);
				}
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
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			if (HasNoteDesc)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(NoteDesc);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			num += ProtocolParser.SizeOfUInt64((ulong)NumHumanPlayers);
			num += ProtocolParser.SizeOfUInt64((ulong)Player1HeroCardId);
			num += ProtocolParser.SizeOfUInt64((ulong)Player2HeroCardId);
			num++;
			num += ProtocolParser.SizeOfUInt64((ulong)AdventureId);
			if (HasAdventureModeId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)AdventureModeId);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)WingId);
			num += ProtocolParser.SizeOfUInt64((ulong)SortOrder);
			if (HasClientPlayer2HeroCardId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ClientPlayer2HeroCardId);
			}
			if (HasTavernBrawlTexture)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(TavernBrawlTexture);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasTavernBrawlTexturePhone)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(TavernBrawlTexturePhone);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasTavernBrawlTexturePhoneOffset)
			{
				num++;
				uint serializedSize = TavernBrawlTexturePhoneOffset.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasIsCoop)
			{
				num++;
				num++;
			}
			if (HasDeckRulesetId)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)DeckRulesetId);
			}
			if (HasRuleType)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)RuleType);
			}
			if (HasScriptObject)
			{
				num += 2;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(ScriptObject);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (GuestHeroes.Count > 0)
			{
				foreach (ScenarioGuestHeroDbRecord guestHero in GuestHeroes)
				{
					num += 2;
					uint serializedSize2 = guestHero.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasOneSimPerPlayer)
			{
				num += 2;
				num++;
			}
			if (ClassExclusions.Count > 0)
			{
				foreach (ClassExclusionDbRecord classExclusion in ClassExclusions)
				{
					num += 2;
					uint serializedSize3 = classExclusion.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (Strings.Count > 0)
			{
				foreach (LocalizedString @string in Strings)
				{
					num += 2;
					uint serializedSize4 = @string.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			return num + 8;
		}
	}
}
