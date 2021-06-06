using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000167 RID: 359
	public class GameSaveDataUpdate : IProtoBuf
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x000567B6 File Offset: 0x000549B6
		// (set) Token: 0x060018A2 RID: 6306 RVA: 0x000567BE File Offset: 0x000549BE
		public EventType EventType { get; set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x000567C7 File Offset: 0x000549C7
		// (set) Token: 0x060018A4 RID: 6308 RVA: 0x000567CF File Offset: 0x000549CF
		public GameSaveOwnerType OwnerType
		{
			get
			{
				return this._OwnerType;
			}
			set
			{
				this._OwnerType = value;
				this.HasOwnerType = true;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x000567DF File Offset: 0x000549DF
		// (set) Token: 0x060018A6 RID: 6310 RVA: 0x000567E7 File Offset: 0x000549E7
		public long OwnerId
		{
			get
			{
				return this._OwnerId;
			}
			set
			{
				this._OwnerId = value;
				this.HasOwnerId = true;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x000567F7 File Offset: 0x000549F7
		// (set) Token: 0x060018A8 RID: 6312 RVA: 0x000567FF File Offset: 0x000549FF
		public List<GameSaveKey> Tuple
		{
			get
			{
				return this._Tuple;
			}
			set
			{
				this._Tuple = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x00056808 File Offset: 0x00054A08
		// (set) Token: 0x060018AA RID: 6314 RVA: 0x00056810 File Offset: 0x00054A10
		public GameSaveDataValue Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				this._Value = value;
				this.HasValue = (value != null);
			}
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00056824 File Offset: 0x00054A24
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.EventType.GetHashCode();
			if (this.HasOwnerType)
			{
				num ^= this.OwnerType.GetHashCode();
			}
			if (this.HasOwnerId)
			{
				num ^= this.OwnerId.GetHashCode();
			}
			foreach (GameSaveKey gameSaveKey in this.Tuple)
			{
				num ^= gameSaveKey.GetHashCode();
			}
			if (this.HasValue)
			{
				num ^= this.Value.GetHashCode();
			}
			return num;
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x000568F0 File Offset: 0x00054AF0
		public override bool Equals(object obj)
		{
			GameSaveDataUpdate gameSaveDataUpdate = obj as GameSaveDataUpdate;
			if (gameSaveDataUpdate == null)
			{
				return false;
			}
			if (!this.EventType.Equals(gameSaveDataUpdate.EventType))
			{
				return false;
			}
			if (this.HasOwnerType != gameSaveDataUpdate.HasOwnerType || (this.HasOwnerType && !this.OwnerType.Equals(gameSaveDataUpdate.OwnerType)))
			{
				return false;
			}
			if (this.HasOwnerId != gameSaveDataUpdate.HasOwnerId || (this.HasOwnerId && !this.OwnerId.Equals(gameSaveDataUpdate.OwnerId)))
			{
				return false;
			}
			if (this.Tuple.Count != gameSaveDataUpdate.Tuple.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Tuple.Count; i++)
			{
				if (!this.Tuple[i].Equals(gameSaveDataUpdate.Tuple[i]))
				{
					return false;
				}
			}
			return this.HasValue == gameSaveDataUpdate.HasValue && (!this.HasValue || this.Value.Equals(gameSaveDataUpdate.Value));
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00056A16 File Offset: 0x00054C16
		public void Deserialize(Stream stream)
		{
			GameSaveDataUpdate.Deserialize(stream, this);
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00056A20 File Offset: 0x00054C20
		public static GameSaveDataUpdate Deserialize(Stream stream, GameSaveDataUpdate instance)
		{
			return GameSaveDataUpdate.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00056A2C File Offset: 0x00054C2C
		public static GameSaveDataUpdate DeserializeLengthDelimited(Stream stream)
		{
			GameSaveDataUpdate gameSaveDataUpdate = new GameSaveDataUpdate();
			GameSaveDataUpdate.DeserializeLengthDelimited(stream, gameSaveDataUpdate);
			return gameSaveDataUpdate;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00056A48 File Offset: 0x00054C48
		public static GameSaveDataUpdate DeserializeLengthDelimited(Stream stream, GameSaveDataUpdate instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameSaveDataUpdate.Deserialize(stream, instance, num);
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00056A70 File Offset: 0x00054C70
		public static GameSaveDataUpdate Deserialize(Stream stream, GameSaveDataUpdate instance, long limit)
		{
			instance.EventType = EventType.EVT_NONE;
			instance.OwnerType = GameSaveOwnerType.GAME_SAVE_OWNER_TYPE_UNKNOWN;
			if (instance.Tuple == null)
			{
				instance.Tuple = new List<GameSaveKey>();
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.EventType = (EventType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.OwnerType = (GameSaveOwnerType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.OwnerId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							instance.Tuple.Add(GameSaveKey.DeserializeLengthDelimited(stream));
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
						if (instance.Value == null)
						{
							instance.Value = GameSaveDataValue.DeserializeLengthDelimited(stream);
						}
						else
						{
							GameSaveDataValue.DeserializeLengthDelimited(stream, instance.Value);
						}
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00056BA6 File Offset: 0x00054DA6
		public void Serialize(Stream stream)
		{
			GameSaveDataUpdate.Serialize(stream, this);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00056BB0 File Offset: 0x00054DB0
		public static void Serialize(Stream stream, GameSaveDataUpdate instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.EventType));
			if (instance.HasOwnerType)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.OwnerType));
			}
			if (instance.HasOwnerId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.OwnerId);
			}
			if (instance.Tuple.Count > 0)
			{
				foreach (GameSaveKey gameSaveKey in instance.Tuple)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, gameSaveKey.GetSerializedSize());
					GameSaveKey.Serialize(stream, gameSaveKey);
				}
			}
			if (instance.HasValue)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.Value.GetSerializedSize());
				GameSaveDataValue.Serialize(stream, instance.Value);
			}
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00056CAC File Offset: 0x00054EAC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.EventType));
			if (this.HasOwnerType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.OwnerType));
			}
			if (this.HasOwnerId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.OwnerId);
			}
			if (this.Tuple.Count > 0)
			{
				foreach (GameSaveKey gameSaveKey in this.Tuple)
				{
					num += 1U;
					uint serializedSize = gameSaveKey.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasValue)
			{
				num += 2U;
				uint serializedSize2 = this.Value.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			num += 1U;
			return num;
		}

		// Token: 0x040007E1 RID: 2017
		public bool HasOwnerType;

		// Token: 0x040007E2 RID: 2018
		private GameSaveOwnerType _OwnerType;

		// Token: 0x040007E3 RID: 2019
		public bool HasOwnerId;

		// Token: 0x040007E4 RID: 2020
		private long _OwnerId;

		// Token: 0x040007E5 RID: 2021
		private List<GameSaveKey> _Tuple = new List<GameSaveKey>();

		// Token: 0x040007E6 RID: 2022
		public bool HasValue;

		// Token: 0x040007E7 RID: 2023
		private GameSaveDataValue _Value;
	}
}
