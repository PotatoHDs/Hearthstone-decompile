using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000C5 RID: 197
	public class GameSaveDataResponse : IProtoBuf
	{
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x000320AF File Offset: 0x000302AF
		// (set) Token: 0x06000D84 RID: 3460 RVA: 0x000320B7 File Offset: 0x000302B7
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x000320C0 File Offset: 0x000302C0
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x000320C8 File Offset: 0x000302C8
		public List<GameSaveDataUpdate> Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				this._Data = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x000320D1 File Offset: 0x000302D1
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x000320D9 File Offset: 0x000302D9
		public int ClientToken
		{
			get
			{
				return this._ClientToken;
			}
			set
			{
				this._ClientToken = value;
				this.HasClientToken = true;
			}
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x000320EC File Offset: 0x000302EC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ErrorCode.GetHashCode();
			foreach (GameSaveDataUpdate gameSaveDataUpdate in this.Data)
			{
				num ^= gameSaveDataUpdate.GetHashCode();
			}
			if (this.HasClientToken)
			{
				num ^= this.ClientToken.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00032180 File Offset: 0x00030380
		public override bool Equals(object obj)
		{
			GameSaveDataResponse gameSaveDataResponse = obj as GameSaveDataResponse;
			if (gameSaveDataResponse == null)
			{
				return false;
			}
			if (!this.ErrorCode.Equals(gameSaveDataResponse.ErrorCode))
			{
				return false;
			}
			if (this.Data.Count != gameSaveDataResponse.Data.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Data.Count; i++)
			{
				if (!this.Data[i].Equals(gameSaveDataResponse.Data[i]))
				{
					return false;
				}
			}
			return this.HasClientToken == gameSaveDataResponse.HasClientToken && (!this.HasClientToken || this.ClientToken.Equals(gameSaveDataResponse.ClientToken));
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0003223C File Offset: 0x0003043C
		public void Deserialize(Stream stream)
		{
			GameSaveDataResponse.Deserialize(stream, this);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00032246 File Offset: 0x00030446
		public static GameSaveDataResponse Deserialize(Stream stream, GameSaveDataResponse instance)
		{
			return GameSaveDataResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00032254 File Offset: 0x00030454
		public static GameSaveDataResponse DeserializeLengthDelimited(Stream stream)
		{
			GameSaveDataResponse gameSaveDataResponse = new GameSaveDataResponse();
			GameSaveDataResponse.DeserializeLengthDelimited(stream, gameSaveDataResponse);
			return gameSaveDataResponse;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00032270 File Offset: 0x00030470
		public static GameSaveDataResponse DeserializeLengthDelimited(Stream stream, GameSaveDataResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameSaveDataResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00032298 File Offset: 0x00030498
		public static GameSaveDataResponse Deserialize(Stream stream, GameSaveDataResponse instance, long limit)
		{
			if (instance.Data == null)
			{
				instance.Data = new List<GameSaveDataUpdate>();
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
				else if (num != 8)
				{
					if (num != 18)
					{
						if (num != 24)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.ClientToken = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.Data.Add(GameSaveDataUpdate.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0003235F File Offset: 0x0003055F
		public void Serialize(Stream stream)
		{
			GameSaveDataResponse.Serialize(stream, this);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00032368 File Offset: 0x00030568
		public static void Serialize(Stream stream, GameSaveDataResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			if (instance.Data.Count > 0)
			{
				foreach (GameSaveDataUpdate gameSaveDataUpdate in instance.Data)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, gameSaveDataUpdate.GetSerializedSize());
					GameSaveDataUpdate.Serialize(stream, gameSaveDataUpdate);
				}
			}
			if (instance.HasClientToken)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ClientToken));
			}
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00032410 File Offset: 0x00030610
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			if (this.Data.Count > 0)
			{
				foreach (GameSaveDataUpdate gameSaveDataUpdate in this.Data)
				{
					num += 1U;
					uint serializedSize = gameSaveDataUpdate.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasClientToken)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ClientToken));
			}
			num += 1U;
			return num;
		}

		// Token: 0x0400049C RID: 1180
		private List<GameSaveDataUpdate> _Data = new List<GameSaveDataUpdate>();

		// Token: 0x0400049D RID: 1181
		public bool HasClientToken;

		// Token: 0x0400049E RID: 1182
		private int _ClientToken;

		// Token: 0x020005D4 RID: 1492
		public enum PacketID
		{
			// Token: 0x04001FC1 RID: 8129
			ID = 358
		}
	}
}
