using System.IO;

namespace PegasusGame
{
	public class PowerHistoryData : IProtoBuf
	{
		public bool HasFullEntity;

		private PowerHistoryEntity _FullEntity;

		public bool HasShowEntity;

		private PowerHistoryEntity _ShowEntity;

		public bool HasHideEntity;

		private PowerHistoryHide _HideEntity;

		public bool HasTagChange;

		private PowerHistoryTagChange _TagChange;

		public bool HasCreateGame;

		private PowerHistoryCreateGame _CreateGame;

		public bool HasPowerStart;

		private PowerHistoryStart _PowerStart;

		public bool HasPowerEnd;

		private PowerHistoryEnd _PowerEnd;

		public bool HasMetaData;

		private PowerHistoryMetaData _MetaData;

		public bool HasChangeEntity;

		private PowerHistoryEntity _ChangeEntity;

		public bool HasResetGame;

		private PowerHistoryResetGame _ResetGame;

		public bool HasSubSpellStart;

		private PowerHistorySubSpellStart _SubSpellStart;

		public bool HasSubSpellEnd;

		private PowerHistorySubSpellEnd _SubSpellEnd;

		public bool HasVoSpell;

		private PowerHistoryVoTask _VoSpell;

		public bool HasCachedTagForDormantChange;

		private PowerHistoryCachedTagForDormantChange _CachedTagForDormantChange;

		public bool HasShuffleDeck;

		private PowerHistoryShuffleDeck _ShuffleDeck;

		public PowerHistoryEntity FullEntity
		{
			get
			{
				return _FullEntity;
			}
			set
			{
				_FullEntity = value;
				HasFullEntity = value != null;
			}
		}

		public PowerHistoryEntity ShowEntity
		{
			get
			{
				return _ShowEntity;
			}
			set
			{
				_ShowEntity = value;
				HasShowEntity = value != null;
			}
		}

		public PowerHistoryHide HideEntity
		{
			get
			{
				return _HideEntity;
			}
			set
			{
				_HideEntity = value;
				HasHideEntity = value != null;
			}
		}

		public PowerHistoryTagChange TagChange
		{
			get
			{
				return _TagChange;
			}
			set
			{
				_TagChange = value;
				HasTagChange = value != null;
			}
		}

		public PowerHistoryCreateGame CreateGame
		{
			get
			{
				return _CreateGame;
			}
			set
			{
				_CreateGame = value;
				HasCreateGame = value != null;
			}
		}

		public PowerHistoryStart PowerStart
		{
			get
			{
				return _PowerStart;
			}
			set
			{
				_PowerStart = value;
				HasPowerStart = value != null;
			}
		}

		public PowerHistoryEnd PowerEnd
		{
			get
			{
				return _PowerEnd;
			}
			set
			{
				_PowerEnd = value;
				HasPowerEnd = value != null;
			}
		}

		public PowerHistoryMetaData MetaData
		{
			get
			{
				return _MetaData;
			}
			set
			{
				_MetaData = value;
				HasMetaData = value != null;
			}
		}

		public PowerHistoryEntity ChangeEntity
		{
			get
			{
				return _ChangeEntity;
			}
			set
			{
				_ChangeEntity = value;
				HasChangeEntity = value != null;
			}
		}

		public PowerHistoryResetGame ResetGame
		{
			get
			{
				return _ResetGame;
			}
			set
			{
				_ResetGame = value;
				HasResetGame = value != null;
			}
		}

		public PowerHistorySubSpellStart SubSpellStart
		{
			get
			{
				return _SubSpellStart;
			}
			set
			{
				_SubSpellStart = value;
				HasSubSpellStart = value != null;
			}
		}

		public PowerHistorySubSpellEnd SubSpellEnd
		{
			get
			{
				return _SubSpellEnd;
			}
			set
			{
				_SubSpellEnd = value;
				HasSubSpellEnd = value != null;
			}
		}

		public PowerHistoryVoTask VoSpell
		{
			get
			{
				return _VoSpell;
			}
			set
			{
				_VoSpell = value;
				HasVoSpell = value != null;
			}
		}

		public PowerHistoryCachedTagForDormantChange CachedTagForDormantChange
		{
			get
			{
				return _CachedTagForDormantChange;
			}
			set
			{
				_CachedTagForDormantChange = value;
				HasCachedTagForDormantChange = value != null;
			}
		}

		public PowerHistoryShuffleDeck ShuffleDeck
		{
			get
			{
				return _ShuffleDeck;
			}
			set
			{
				_ShuffleDeck = value;
				HasShuffleDeck = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFullEntity)
			{
				num ^= FullEntity.GetHashCode();
			}
			if (HasShowEntity)
			{
				num ^= ShowEntity.GetHashCode();
			}
			if (HasHideEntity)
			{
				num ^= HideEntity.GetHashCode();
			}
			if (HasTagChange)
			{
				num ^= TagChange.GetHashCode();
			}
			if (HasCreateGame)
			{
				num ^= CreateGame.GetHashCode();
			}
			if (HasPowerStart)
			{
				num ^= PowerStart.GetHashCode();
			}
			if (HasPowerEnd)
			{
				num ^= PowerEnd.GetHashCode();
			}
			if (HasMetaData)
			{
				num ^= MetaData.GetHashCode();
			}
			if (HasChangeEntity)
			{
				num ^= ChangeEntity.GetHashCode();
			}
			if (HasResetGame)
			{
				num ^= ResetGame.GetHashCode();
			}
			if (HasSubSpellStart)
			{
				num ^= SubSpellStart.GetHashCode();
			}
			if (HasSubSpellEnd)
			{
				num ^= SubSpellEnd.GetHashCode();
			}
			if (HasVoSpell)
			{
				num ^= VoSpell.GetHashCode();
			}
			if (HasCachedTagForDormantChange)
			{
				num ^= CachedTagForDormantChange.GetHashCode();
			}
			if (HasShuffleDeck)
			{
				num ^= ShuffleDeck.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PowerHistoryData powerHistoryData = obj as PowerHistoryData;
			if (powerHistoryData == null)
			{
				return false;
			}
			if (HasFullEntity != powerHistoryData.HasFullEntity || (HasFullEntity && !FullEntity.Equals(powerHistoryData.FullEntity)))
			{
				return false;
			}
			if (HasShowEntity != powerHistoryData.HasShowEntity || (HasShowEntity && !ShowEntity.Equals(powerHistoryData.ShowEntity)))
			{
				return false;
			}
			if (HasHideEntity != powerHistoryData.HasHideEntity || (HasHideEntity && !HideEntity.Equals(powerHistoryData.HideEntity)))
			{
				return false;
			}
			if (HasTagChange != powerHistoryData.HasTagChange || (HasTagChange && !TagChange.Equals(powerHistoryData.TagChange)))
			{
				return false;
			}
			if (HasCreateGame != powerHistoryData.HasCreateGame || (HasCreateGame && !CreateGame.Equals(powerHistoryData.CreateGame)))
			{
				return false;
			}
			if (HasPowerStart != powerHistoryData.HasPowerStart || (HasPowerStart && !PowerStart.Equals(powerHistoryData.PowerStart)))
			{
				return false;
			}
			if (HasPowerEnd != powerHistoryData.HasPowerEnd || (HasPowerEnd && !PowerEnd.Equals(powerHistoryData.PowerEnd)))
			{
				return false;
			}
			if (HasMetaData != powerHistoryData.HasMetaData || (HasMetaData && !MetaData.Equals(powerHistoryData.MetaData)))
			{
				return false;
			}
			if (HasChangeEntity != powerHistoryData.HasChangeEntity || (HasChangeEntity && !ChangeEntity.Equals(powerHistoryData.ChangeEntity)))
			{
				return false;
			}
			if (HasResetGame != powerHistoryData.HasResetGame || (HasResetGame && !ResetGame.Equals(powerHistoryData.ResetGame)))
			{
				return false;
			}
			if (HasSubSpellStart != powerHistoryData.HasSubSpellStart || (HasSubSpellStart && !SubSpellStart.Equals(powerHistoryData.SubSpellStart)))
			{
				return false;
			}
			if (HasSubSpellEnd != powerHistoryData.HasSubSpellEnd || (HasSubSpellEnd && !SubSpellEnd.Equals(powerHistoryData.SubSpellEnd)))
			{
				return false;
			}
			if (HasVoSpell != powerHistoryData.HasVoSpell || (HasVoSpell && !VoSpell.Equals(powerHistoryData.VoSpell)))
			{
				return false;
			}
			if (HasCachedTagForDormantChange != powerHistoryData.HasCachedTagForDormantChange || (HasCachedTagForDormantChange && !CachedTagForDormantChange.Equals(powerHistoryData.CachedTagForDormantChange)))
			{
				return false;
			}
			if (HasShuffleDeck != powerHistoryData.HasShuffleDeck || (HasShuffleDeck && !ShuffleDeck.Equals(powerHistoryData.ShuffleDeck)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PowerHistoryData Deserialize(Stream stream, PowerHistoryData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PowerHistoryData DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryData powerHistoryData = new PowerHistoryData();
			DeserializeLengthDelimited(stream, powerHistoryData);
			return powerHistoryData;
		}

		public static PowerHistoryData DeserializeLengthDelimited(Stream stream, PowerHistoryData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PowerHistoryData Deserialize(Stream stream, PowerHistoryData instance, long limit)
		{
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
					if (instance.FullEntity == null)
					{
						instance.FullEntity = PowerHistoryEntity.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryEntity.DeserializeLengthDelimited(stream, instance.FullEntity);
					}
					continue;
				case 18:
					if (instance.ShowEntity == null)
					{
						instance.ShowEntity = PowerHistoryEntity.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryEntity.DeserializeLengthDelimited(stream, instance.ShowEntity);
					}
					continue;
				case 26:
					if (instance.HideEntity == null)
					{
						instance.HideEntity = PowerHistoryHide.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryHide.DeserializeLengthDelimited(stream, instance.HideEntity);
					}
					continue;
				case 34:
					if (instance.TagChange == null)
					{
						instance.TagChange = PowerHistoryTagChange.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryTagChange.DeserializeLengthDelimited(stream, instance.TagChange);
					}
					continue;
				case 42:
					if (instance.CreateGame == null)
					{
						instance.CreateGame = PowerHistoryCreateGame.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryCreateGame.DeserializeLengthDelimited(stream, instance.CreateGame);
					}
					continue;
				case 50:
					if (instance.PowerStart == null)
					{
						instance.PowerStart = PowerHistoryStart.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryStart.DeserializeLengthDelimited(stream, instance.PowerStart);
					}
					continue;
				case 58:
					if (instance.PowerEnd == null)
					{
						instance.PowerEnd = PowerHistoryEnd.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryEnd.DeserializeLengthDelimited(stream, instance.PowerEnd);
					}
					continue;
				case 66:
					if (instance.MetaData == null)
					{
						instance.MetaData = PowerHistoryMetaData.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryMetaData.DeserializeLengthDelimited(stream, instance.MetaData);
					}
					continue;
				case 74:
					if (instance.ChangeEntity == null)
					{
						instance.ChangeEntity = PowerHistoryEntity.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryEntity.DeserializeLengthDelimited(stream, instance.ChangeEntity);
					}
					continue;
				case 82:
					if (instance.ResetGame == null)
					{
						instance.ResetGame = PowerHistoryResetGame.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryResetGame.DeserializeLengthDelimited(stream, instance.ResetGame);
					}
					continue;
				case 90:
					if (instance.SubSpellStart == null)
					{
						instance.SubSpellStart = PowerHistorySubSpellStart.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistorySubSpellStart.DeserializeLengthDelimited(stream, instance.SubSpellStart);
					}
					continue;
				case 98:
					if (instance.SubSpellEnd == null)
					{
						instance.SubSpellEnd = PowerHistorySubSpellEnd.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistorySubSpellEnd.DeserializeLengthDelimited(stream, instance.SubSpellEnd);
					}
					continue;
				case 106:
					if (instance.VoSpell == null)
					{
						instance.VoSpell = PowerHistoryVoTask.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryVoTask.DeserializeLengthDelimited(stream, instance.VoSpell);
					}
					continue;
				case 114:
					if (instance.CachedTagForDormantChange == null)
					{
						instance.CachedTagForDormantChange = PowerHistoryCachedTagForDormantChange.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryCachedTagForDormantChange.DeserializeLengthDelimited(stream, instance.CachedTagForDormantChange);
					}
					continue;
				case 122:
					if (instance.ShuffleDeck == null)
					{
						instance.ShuffleDeck = PowerHistoryShuffleDeck.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryShuffleDeck.DeserializeLengthDelimited(stream, instance.ShuffleDeck);
					}
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

		public static void Serialize(Stream stream, PowerHistoryData instance)
		{
			if (instance.HasFullEntity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.FullEntity.GetSerializedSize());
				PowerHistoryEntity.Serialize(stream, instance.FullEntity);
			}
			if (instance.HasShowEntity)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ShowEntity.GetSerializedSize());
				PowerHistoryEntity.Serialize(stream, instance.ShowEntity);
			}
			if (instance.HasHideEntity)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.HideEntity.GetSerializedSize());
				PowerHistoryHide.Serialize(stream, instance.HideEntity);
			}
			if (instance.HasTagChange)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.TagChange.GetSerializedSize());
				PowerHistoryTagChange.Serialize(stream, instance.TagChange);
			}
			if (instance.HasCreateGame)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.CreateGame.GetSerializedSize());
				PowerHistoryCreateGame.Serialize(stream, instance.CreateGame);
			}
			if (instance.HasPowerStart)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.PowerStart.GetSerializedSize());
				PowerHistoryStart.Serialize(stream, instance.PowerStart);
			}
			if (instance.HasPowerEnd)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.PowerEnd.GetSerializedSize());
				PowerHistoryEnd.Serialize(stream, instance.PowerEnd);
			}
			if (instance.HasMetaData)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.MetaData.GetSerializedSize());
				PowerHistoryMetaData.Serialize(stream, instance.MetaData);
			}
			if (instance.HasChangeEntity)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteUInt32(stream, instance.ChangeEntity.GetSerializedSize());
				PowerHistoryEntity.Serialize(stream, instance.ChangeEntity);
			}
			if (instance.HasResetGame)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.ResetGame.GetSerializedSize());
				PowerHistoryResetGame.Serialize(stream, instance.ResetGame);
			}
			if (instance.HasSubSpellStart)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.SubSpellStart.GetSerializedSize());
				PowerHistorySubSpellStart.Serialize(stream, instance.SubSpellStart);
			}
			if (instance.HasSubSpellEnd)
			{
				stream.WriteByte(98);
				ProtocolParser.WriteUInt32(stream, instance.SubSpellEnd.GetSerializedSize());
				PowerHistorySubSpellEnd.Serialize(stream, instance.SubSpellEnd);
			}
			if (instance.HasVoSpell)
			{
				stream.WriteByte(106);
				ProtocolParser.WriteUInt32(stream, instance.VoSpell.GetSerializedSize());
				PowerHistoryVoTask.Serialize(stream, instance.VoSpell);
			}
			if (instance.HasCachedTagForDormantChange)
			{
				stream.WriteByte(114);
				ProtocolParser.WriteUInt32(stream, instance.CachedTagForDormantChange.GetSerializedSize());
				PowerHistoryCachedTagForDormantChange.Serialize(stream, instance.CachedTagForDormantChange);
			}
			if (instance.HasShuffleDeck)
			{
				stream.WriteByte(122);
				ProtocolParser.WriteUInt32(stream, instance.ShuffleDeck.GetSerializedSize());
				PowerHistoryShuffleDeck.Serialize(stream, instance.ShuffleDeck);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasFullEntity)
			{
				num++;
				uint serializedSize = FullEntity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasShowEntity)
			{
				num++;
				uint serializedSize2 = ShowEntity.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasHideEntity)
			{
				num++;
				uint serializedSize3 = HideEntity.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasTagChange)
			{
				num++;
				uint serializedSize4 = TagChange.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasCreateGame)
			{
				num++;
				uint serializedSize5 = CreateGame.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (HasPowerStart)
			{
				num++;
				uint serializedSize6 = PowerStart.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (HasPowerEnd)
			{
				num++;
				uint serializedSize7 = PowerEnd.GetSerializedSize();
				num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
			}
			if (HasMetaData)
			{
				num++;
				uint serializedSize8 = MetaData.GetSerializedSize();
				num += serializedSize8 + ProtocolParser.SizeOfUInt32(serializedSize8);
			}
			if (HasChangeEntity)
			{
				num++;
				uint serializedSize9 = ChangeEntity.GetSerializedSize();
				num += serializedSize9 + ProtocolParser.SizeOfUInt32(serializedSize9);
			}
			if (HasResetGame)
			{
				num++;
				uint serializedSize10 = ResetGame.GetSerializedSize();
				num += serializedSize10 + ProtocolParser.SizeOfUInt32(serializedSize10);
			}
			if (HasSubSpellStart)
			{
				num++;
				uint serializedSize11 = SubSpellStart.GetSerializedSize();
				num += serializedSize11 + ProtocolParser.SizeOfUInt32(serializedSize11);
			}
			if (HasSubSpellEnd)
			{
				num++;
				uint serializedSize12 = SubSpellEnd.GetSerializedSize();
				num += serializedSize12 + ProtocolParser.SizeOfUInt32(serializedSize12);
			}
			if (HasVoSpell)
			{
				num++;
				uint serializedSize13 = VoSpell.GetSerializedSize();
				num += serializedSize13 + ProtocolParser.SizeOfUInt32(serializedSize13);
			}
			if (HasCachedTagForDormantChange)
			{
				num++;
				uint serializedSize14 = CachedTagForDormantChange.GetSerializedSize();
				num += serializedSize14 + ProtocolParser.SizeOfUInt32(serializedSize14);
			}
			if (HasShuffleDeck)
			{
				num++;
				uint serializedSize15 = ShuffleDeck.GetSerializedSize();
				num += serializedSize15 + ProtocolParser.SizeOfUInt32(serializedSize15);
			}
			return num;
		}
	}
}
