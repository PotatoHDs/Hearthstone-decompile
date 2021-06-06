using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000C6 RID: 198
	public class SetGameSaveDataResponse : IProtoBuf
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x000324C7 File Offset: 0x000306C7
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x000324CF File Offset: 0x000306CF
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x000324D8 File Offset: 0x000306D8
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x000324E0 File Offset: 0x000306E0
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

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x000324E9 File Offset: 0x000306E9
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x000324F1 File Offset: 0x000306F1
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

		// Token: 0x06000D9A RID: 3482 RVA: 0x00032504 File Offset: 0x00030704
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

		// Token: 0x06000D9B RID: 3483 RVA: 0x00032598 File Offset: 0x00030798
		public override bool Equals(object obj)
		{
			SetGameSaveDataResponse setGameSaveDataResponse = obj as SetGameSaveDataResponse;
			if (setGameSaveDataResponse == null)
			{
				return false;
			}
			if (!this.ErrorCode.Equals(setGameSaveDataResponse.ErrorCode))
			{
				return false;
			}
			if (this.Data.Count != setGameSaveDataResponse.Data.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Data.Count; i++)
			{
				if (!this.Data[i].Equals(setGameSaveDataResponse.Data[i]))
				{
					return false;
				}
			}
			return this.HasClientToken == setGameSaveDataResponse.HasClientToken && (!this.HasClientToken || this.ClientToken.Equals(setGameSaveDataResponse.ClientToken));
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00032654 File Offset: 0x00030854
		public void Deserialize(Stream stream)
		{
			SetGameSaveDataResponse.Deserialize(stream, this);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0003265E File Offset: 0x0003085E
		public static SetGameSaveDataResponse Deserialize(Stream stream, SetGameSaveDataResponse instance)
		{
			return SetGameSaveDataResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0003266C File Offset: 0x0003086C
		public static SetGameSaveDataResponse DeserializeLengthDelimited(Stream stream)
		{
			SetGameSaveDataResponse setGameSaveDataResponse = new SetGameSaveDataResponse();
			SetGameSaveDataResponse.DeserializeLengthDelimited(stream, setGameSaveDataResponse);
			return setGameSaveDataResponse;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00032688 File Offset: 0x00030888
		public static SetGameSaveDataResponse DeserializeLengthDelimited(Stream stream, SetGameSaveDataResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetGameSaveDataResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x000326B0 File Offset: 0x000308B0
		public static SetGameSaveDataResponse Deserialize(Stream stream, SetGameSaveDataResponse instance, long limit)
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

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00032777 File Offset: 0x00030977
		public void Serialize(Stream stream)
		{
			SetGameSaveDataResponse.Serialize(stream, this);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00032780 File Offset: 0x00030980
		public static void Serialize(Stream stream, SetGameSaveDataResponse instance)
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

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00032828 File Offset: 0x00030A28
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

		// Token: 0x040004A0 RID: 1184
		private List<GameSaveDataUpdate> _Data = new List<GameSaveDataUpdate>();

		// Token: 0x040004A1 RID: 1185
		public bool HasClientToken;

		// Token: 0x040004A2 RID: 1186
		private int _ClientToken;

		// Token: 0x020005D5 RID: 1493
		public enum PacketID
		{
			// Token: 0x04001FC3 RID: 8131
			ID = 360
		}
	}
}
