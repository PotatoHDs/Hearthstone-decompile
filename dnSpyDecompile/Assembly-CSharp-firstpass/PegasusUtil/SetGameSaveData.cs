using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x0200007B RID: 123
	public class SetGameSaveData : IProtoBuf
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0001C2FB File Offset: 0x0001A4FB
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0001C303 File Offset: 0x0001A503
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

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0001C30C File Offset: 0x0001A50C
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x0001C314 File Offset: 0x0001A514
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

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001C324 File Offset: 0x0001A524
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
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

		// Token: 0x060007BA RID: 1978 RVA: 0x0001C3A0 File Offset: 0x0001A5A0
		public override bool Equals(object obj)
		{
			SetGameSaveData setGameSaveData = obj as SetGameSaveData;
			if (setGameSaveData == null)
			{
				return false;
			}
			if (this.Data.Count != setGameSaveData.Data.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Data.Count; i++)
			{
				if (!this.Data[i].Equals(setGameSaveData.Data[i]))
				{
					return false;
				}
			}
			return this.HasClientToken == setGameSaveData.HasClientToken && (!this.HasClientToken || this.ClientToken.Equals(setGameSaveData.ClientToken));
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001C439 File Offset: 0x0001A639
		public void Deserialize(Stream stream)
		{
			SetGameSaveData.Deserialize(stream, this);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001C443 File Offset: 0x0001A643
		public static SetGameSaveData Deserialize(Stream stream, SetGameSaveData instance)
		{
			return SetGameSaveData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001C450 File Offset: 0x0001A650
		public static SetGameSaveData DeserializeLengthDelimited(Stream stream)
		{
			SetGameSaveData setGameSaveData = new SetGameSaveData();
			SetGameSaveData.DeserializeLengthDelimited(stream, setGameSaveData);
			return setGameSaveData;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001C46C File Offset: 0x0001A66C
		public static SetGameSaveData DeserializeLengthDelimited(Stream stream, SetGameSaveData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetGameSaveData.Deserialize(stream, instance, num);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001C494 File Offset: 0x0001A694
		public static SetGameSaveData Deserialize(Stream stream, SetGameSaveData instance, long limit)
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
				else if (num != 10)
				{
					if (num != 16)
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
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001C545 File Offset: 0x0001A745
		public void Serialize(Stream stream)
		{
			SetGameSaveData.Serialize(stream, this);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001C550 File Offset: 0x0001A750
		public static void Serialize(Stream stream, SetGameSaveData instance)
		{
			if (instance.Data.Count > 0)
			{
				foreach (GameSaveDataUpdate gameSaveDataUpdate in instance.Data)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, gameSaveDataUpdate.GetSerializedSize());
					GameSaveDataUpdate.Serialize(stream, gameSaveDataUpdate);
				}
			}
			if (instance.HasClientToken)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ClientToken));
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001C5E4 File Offset: 0x0001A7E4
		public uint GetSerializedSize()
		{
			uint num = 0U;
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
			return num;
		}

		// Token: 0x04000265 RID: 613
		private List<GameSaveDataUpdate> _Data = new List<GameSaveDataUpdate>();

		// Token: 0x04000266 RID: 614
		public bool HasClientToken;

		// Token: 0x04000267 RID: 615
		private int _ClientToken;

		// Token: 0x0200058E RID: 1422
		public enum PacketID
		{
			// Token: 0x04001F06 RID: 7942
			ID = 359,
			// Token: 0x04001F07 RID: 7943
			System = 0
		}
	}
}
