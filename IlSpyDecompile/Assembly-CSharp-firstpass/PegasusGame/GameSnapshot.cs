using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusGame
{
	public class GameSnapshot : IProtoBuf
	{
		private List<string> _PlayerEntity = new List<string>();

		private List<string> _DeckGuid1 = new List<string>();

		private List<int> _DeckPremium1 = new List<int>();

		private List<string> _DeckGuid2 = new List<string>();

		private List<int> _DeckPremium2 = new List<int>();

		private List<PendingEvent> _Events = new List<PendingEvent>();

		public bool HasGameHash;

		private ulong _GameHash;

		public bool HasEventTimingHash;

		private ulong _EventTimingHash;

		public bool HasOriginalGameId;

		private long _OriginalGameId;

		public bool HasGameStartTime;

		private long _GameStartTime;

		public uint InitialSeed { get; set; }

		public int ScenarioId { get; set; }

		public int BoardId { get; set; }

		public string Game { get; set; }

		public bool IsExpertAi { get; set; }

		public GameType GameType { get; set; }

		public FormatType FormatType { get; set; }

		public int Players { get; set; }

		public List<string> PlayerEntity
		{
			get
			{
				return _PlayerEntity;
			}
			set
			{
				_PlayerEntity = value;
			}
		}

		public string HeroGuid1 { get; set; }

		public int HeroPremium1 { get; set; }

		public List<string> DeckGuid1
		{
			get
			{
				return _DeckGuid1;
			}
			set
			{
				_DeckGuid1 = value;
			}
		}

		public List<int> DeckPremium1
		{
			get
			{
				return _DeckPremium1;
			}
			set
			{
				_DeckPremium1 = value;
			}
		}

		public int CardBack1 { get; set; }

		public string HeroGuid2 { get; set; }

		public int HeroPremium2 { get; set; }

		public List<string> DeckGuid2
		{
			get
			{
				return _DeckGuid2;
			}
			set
			{
				_DeckGuid2 = value;
			}
		}

		public List<int> DeckPremium2
		{
			get
			{
				return _DeckPremium2;
			}
			set
			{
				_DeckPremium2 = value;
			}
		}

		public int CardBack2 { get; set; }

		public List<PendingEvent> Events
		{
			get
			{
				return _Events;
			}
			set
			{
				_Events = value;
			}
		}

		public ulong GameHash
		{
			get
			{
				return _GameHash;
			}
			set
			{
				_GameHash = value;
				HasGameHash = true;
			}
		}

		public ulong EventTimingHash
		{
			get
			{
				return _EventTimingHash;
			}
			set
			{
				_EventTimingHash = value;
				HasEventTimingHash = true;
			}
		}

		public long OriginalGameId
		{
			get
			{
				return _OriginalGameId;
			}
			set
			{
				_OriginalGameId = value;
				HasOriginalGameId = true;
			}
		}

		public long GameStartTime
		{
			get
			{
				return _GameStartTime;
			}
			set
			{
				_GameStartTime = value;
				HasGameStartTime = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= InitialSeed.GetHashCode();
			hashCode ^= ScenarioId.GetHashCode();
			hashCode ^= BoardId.GetHashCode();
			hashCode ^= Game.GetHashCode();
			hashCode ^= IsExpertAi.GetHashCode();
			hashCode ^= GameType.GetHashCode();
			hashCode ^= FormatType.GetHashCode();
			hashCode ^= Players.GetHashCode();
			foreach (string item in PlayerEntity)
			{
				hashCode ^= item.GetHashCode();
			}
			hashCode ^= HeroGuid1.GetHashCode();
			hashCode ^= HeroPremium1.GetHashCode();
			foreach (string item2 in DeckGuid1)
			{
				hashCode ^= item2.GetHashCode();
			}
			foreach (int item3 in DeckPremium1)
			{
				hashCode ^= item3.GetHashCode();
			}
			hashCode ^= CardBack1.GetHashCode();
			hashCode ^= HeroGuid2.GetHashCode();
			hashCode ^= HeroPremium2.GetHashCode();
			foreach (string item4 in DeckGuid2)
			{
				hashCode ^= item4.GetHashCode();
			}
			foreach (int item5 in DeckPremium2)
			{
				hashCode ^= item5.GetHashCode();
			}
			hashCode ^= CardBack2.GetHashCode();
			foreach (PendingEvent @event in Events)
			{
				hashCode ^= @event.GetHashCode();
			}
			if (HasGameHash)
			{
				hashCode ^= GameHash.GetHashCode();
			}
			if (HasEventTimingHash)
			{
				hashCode ^= EventTimingHash.GetHashCode();
			}
			if (HasOriginalGameId)
			{
				hashCode ^= OriginalGameId.GetHashCode();
			}
			if (HasGameStartTime)
			{
				hashCode ^= GameStartTime.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GameSnapshot gameSnapshot = obj as GameSnapshot;
			if (gameSnapshot == null)
			{
				return false;
			}
			if (!InitialSeed.Equals(gameSnapshot.InitialSeed))
			{
				return false;
			}
			if (!ScenarioId.Equals(gameSnapshot.ScenarioId))
			{
				return false;
			}
			if (!BoardId.Equals(gameSnapshot.BoardId))
			{
				return false;
			}
			if (!Game.Equals(gameSnapshot.Game))
			{
				return false;
			}
			if (!IsExpertAi.Equals(gameSnapshot.IsExpertAi))
			{
				return false;
			}
			if (!GameType.Equals(gameSnapshot.GameType))
			{
				return false;
			}
			if (!FormatType.Equals(gameSnapshot.FormatType))
			{
				return false;
			}
			if (!Players.Equals(gameSnapshot.Players))
			{
				return false;
			}
			if (PlayerEntity.Count != gameSnapshot.PlayerEntity.Count)
			{
				return false;
			}
			for (int i = 0; i < PlayerEntity.Count; i++)
			{
				if (!PlayerEntity[i].Equals(gameSnapshot.PlayerEntity[i]))
				{
					return false;
				}
			}
			if (!HeroGuid1.Equals(gameSnapshot.HeroGuid1))
			{
				return false;
			}
			if (!HeroPremium1.Equals(gameSnapshot.HeroPremium1))
			{
				return false;
			}
			if (DeckGuid1.Count != gameSnapshot.DeckGuid1.Count)
			{
				return false;
			}
			for (int j = 0; j < DeckGuid1.Count; j++)
			{
				if (!DeckGuid1[j].Equals(gameSnapshot.DeckGuid1[j]))
				{
					return false;
				}
			}
			if (DeckPremium1.Count != gameSnapshot.DeckPremium1.Count)
			{
				return false;
			}
			for (int k = 0; k < DeckPremium1.Count; k++)
			{
				if (!DeckPremium1[k].Equals(gameSnapshot.DeckPremium1[k]))
				{
					return false;
				}
			}
			if (!CardBack1.Equals(gameSnapshot.CardBack1))
			{
				return false;
			}
			if (!HeroGuid2.Equals(gameSnapshot.HeroGuid2))
			{
				return false;
			}
			if (!HeroPremium2.Equals(gameSnapshot.HeroPremium2))
			{
				return false;
			}
			if (DeckGuid2.Count != gameSnapshot.DeckGuid2.Count)
			{
				return false;
			}
			for (int l = 0; l < DeckGuid2.Count; l++)
			{
				if (!DeckGuid2[l].Equals(gameSnapshot.DeckGuid2[l]))
				{
					return false;
				}
			}
			if (DeckPremium2.Count != gameSnapshot.DeckPremium2.Count)
			{
				return false;
			}
			for (int m = 0; m < DeckPremium2.Count; m++)
			{
				if (!DeckPremium2[m].Equals(gameSnapshot.DeckPremium2[m]))
				{
					return false;
				}
			}
			if (!CardBack2.Equals(gameSnapshot.CardBack2))
			{
				return false;
			}
			if (Events.Count != gameSnapshot.Events.Count)
			{
				return false;
			}
			for (int n = 0; n < Events.Count; n++)
			{
				if (!Events[n].Equals(gameSnapshot.Events[n]))
				{
					return false;
				}
			}
			if (HasGameHash != gameSnapshot.HasGameHash || (HasGameHash && !GameHash.Equals(gameSnapshot.GameHash)))
			{
				return false;
			}
			if (HasEventTimingHash != gameSnapshot.HasEventTimingHash || (HasEventTimingHash && !EventTimingHash.Equals(gameSnapshot.EventTimingHash)))
			{
				return false;
			}
			if (HasOriginalGameId != gameSnapshot.HasOriginalGameId || (HasOriginalGameId && !OriginalGameId.Equals(gameSnapshot.OriginalGameId)))
			{
				return false;
			}
			if (HasGameStartTime != gameSnapshot.HasGameStartTime || (HasGameStartTime && !GameStartTime.Equals(gameSnapshot.GameStartTime)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameSnapshot Deserialize(Stream stream, GameSnapshot instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameSnapshot DeserializeLengthDelimited(Stream stream)
		{
			GameSnapshot gameSnapshot = new GameSnapshot();
			DeserializeLengthDelimited(stream, gameSnapshot);
			return gameSnapshot;
		}

		public static GameSnapshot DeserializeLengthDelimited(Stream stream, GameSnapshot instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameSnapshot Deserialize(Stream stream, GameSnapshot instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.PlayerEntity == null)
			{
				instance.PlayerEntity = new List<string>();
			}
			if (instance.DeckGuid1 == null)
			{
				instance.DeckGuid1 = new List<string>();
			}
			if (instance.DeckPremium1 == null)
			{
				instance.DeckPremium1 = new List<int>();
			}
			if (instance.DeckGuid2 == null)
			{
				instance.DeckGuid2 = new List<string>();
			}
			if (instance.DeckPremium2 == null)
			{
				instance.DeckPremium2 = new List<int>();
			}
			if (instance.Events == null)
			{
				instance.Events = new List<PendingEvent>();
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
				case 13:
					instance.InitialSeed = binaryReader.ReadUInt32();
					continue;
				case 16:
					instance.ScenarioId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.BoardId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.Game = ProtocolParser.ReadString(stream);
					continue;
				case 40:
					instance.IsExpertAi = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.Players = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 82:
					instance.PlayerEntity.Add(ProtocolParser.ReadString(stream));
					continue;
				case 98:
					instance.HeroGuid1 = ProtocolParser.ReadString(stream);
					continue;
				case 104:
					instance.HeroPremium1 = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 114:
					instance.DeckGuid1.Add(ProtocolParser.ReadString(stream));
					continue;
				case 120:
					instance.DeckPremium1.Add((int)ProtocolParser.ReadUInt64(stream));
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
							instance.CardBack1 = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 17u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.HeroGuid2 = ProtocolParser.ReadString(stream);
						}
						break;
					case 18u:
						if (key.WireType == Wire.Varint)
						{
							instance.HeroPremium2 = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 19u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeckGuid2.Add(ProtocolParser.ReadString(stream));
						}
						break;
					case 20u:
						if (key.WireType == Wire.Varint)
						{
							instance.DeckPremium2.Add((int)ProtocolParser.ReadUInt64(stream));
						}
						break;
					case 21u:
						if (key.WireType == Wire.Varint)
						{
							instance.CardBack2 = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 22u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Events.Add(PendingEvent.DeserializeLengthDelimited(stream));
						}
						break;
					case 23u:
						if (key.WireType == Wire.Varint)
						{
							instance.GameHash = ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 24u:
						if (key.WireType == Wire.Varint)
						{
							instance.EventTimingHash = ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 25u:
						if (key.WireType == Wire.Varint)
						{
							instance.OriginalGameId = (long)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 26u:
						if (key.WireType == Wire.Varint)
						{
							instance.GameStartTime = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GameSnapshot instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.InitialSeed);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ScenarioId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.BoardId);
			if (instance.Game == null)
			{
				throw new ArgumentNullException("Game", "Required by proto specification.");
			}
			stream.WriteByte(34);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Game));
			stream.WriteByte(40);
			ProtocolParser.WriteBool(stream, instance.IsExpertAi);
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.GameType);
			stream.WriteByte(64);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Players);
			if (instance.PlayerEntity.Count > 0)
			{
				foreach (string item in instance.PlayerEntity)
				{
					stream.WriteByte(82);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(item));
				}
			}
			if (instance.HeroGuid1 == null)
			{
				throw new ArgumentNullException("HeroGuid1", "Required by proto specification.");
			}
			stream.WriteByte(98);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.HeroGuid1));
			stream.WriteByte(104);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.HeroPremium1);
			if (instance.DeckGuid1.Count > 0)
			{
				foreach (string item2 in instance.DeckGuid1)
				{
					stream.WriteByte(114);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(item2));
				}
			}
			if (instance.DeckPremium1.Count > 0)
			{
				foreach (int item3 in instance.DeckPremium1)
				{
					stream.WriteByte(120);
					ProtocolParser.WriteUInt64(stream, (ulong)item3);
				}
			}
			stream.WriteByte(128);
			stream.WriteByte(1);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CardBack1);
			if (instance.HeroGuid2 == null)
			{
				throw new ArgumentNullException("HeroGuid2", "Required by proto specification.");
			}
			stream.WriteByte(138);
			stream.WriteByte(1);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.HeroGuid2));
			stream.WriteByte(144);
			stream.WriteByte(1);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.HeroPremium2);
			if (instance.DeckGuid2.Count > 0)
			{
				foreach (string item4 in instance.DeckGuid2)
				{
					stream.WriteByte(154);
					stream.WriteByte(1);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(item4));
				}
			}
			if (instance.DeckPremium2.Count > 0)
			{
				foreach (int item5 in instance.DeckPremium2)
				{
					stream.WriteByte(160);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt64(stream, (ulong)item5);
				}
			}
			stream.WriteByte(168);
			stream.WriteByte(1);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CardBack2);
			if (instance.Events.Count > 0)
			{
				foreach (PendingEvent @event in instance.Events)
				{
					stream.WriteByte(178);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, @event.GetSerializedSize());
					PendingEvent.Serialize(stream, @event);
				}
			}
			if (instance.HasGameHash)
			{
				stream.WriteByte(184);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, instance.GameHash);
			}
			if (instance.HasEventTimingHash)
			{
				stream.WriteByte(192);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, instance.EventTimingHash);
			}
			if (instance.HasOriginalGameId)
			{
				stream.WriteByte(200);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.OriginalGameId);
			}
			if (instance.HasGameStartTime)
			{
				stream.WriteByte(208);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameStartTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 4;
			num += ProtocolParser.SizeOfUInt64((ulong)ScenarioId);
			num += ProtocolParser.SizeOfUInt64((ulong)BoardId);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Game);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num++;
			num += ProtocolParser.SizeOfUInt64((ulong)GameType);
			num += ProtocolParser.SizeOfUInt64((ulong)FormatType);
			num += ProtocolParser.SizeOfUInt64((ulong)Players);
			if (PlayerEntity.Count > 0)
			{
				foreach (string item in PlayerEntity)
				{
					num++;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(item);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(HeroGuid1);
			num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			num += ProtocolParser.SizeOfUInt64((ulong)HeroPremium1);
			if (DeckGuid1.Count > 0)
			{
				foreach (string item2 in DeckGuid1)
				{
					num++;
					uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(item2);
					num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
				}
			}
			if (DeckPremium1.Count > 0)
			{
				foreach (int item3 in DeckPremium1)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)item3);
				}
			}
			num += ProtocolParser.SizeOfUInt64((ulong)CardBack1);
			uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(HeroGuid2);
			num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			num += ProtocolParser.SizeOfUInt64((ulong)HeroPremium2);
			if (DeckGuid2.Count > 0)
			{
				foreach (string item4 in DeckGuid2)
				{
					num += 2;
					uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(item4);
					num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
				}
			}
			if (DeckPremium2.Count > 0)
			{
				foreach (int item5 in DeckPremium2)
				{
					num += 2;
					num += ProtocolParser.SizeOfUInt64((ulong)item5);
				}
			}
			num += ProtocolParser.SizeOfUInt64((ulong)CardBack2);
			if (Events.Count > 0)
			{
				foreach (PendingEvent @event in Events)
				{
					num += 2;
					uint serializedSize = @event.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasGameHash)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64(GameHash);
			}
			if (HasEventTimingHash)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64(EventTimingHash);
			}
			if (HasOriginalGameId)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)OriginalGameId);
			}
			if (HasGameStartTime)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)GameStartTime);
			}
			return num + 18;
		}
	}
}
