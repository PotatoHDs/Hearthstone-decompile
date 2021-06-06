using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200007A RID: 122
	public class GameSaveDataRequest : IProtoBuf
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0001BF7E File Offset: 0x0001A17E
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0001BF86 File Offset: 0x0001A186
		public List<long> KeyIds
		{
			get
			{
				return this._KeyIds;
			}
			set
			{
				this._KeyIds = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0001BF8F File Offset: 0x0001A18F
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0001BF97 File Offset: 0x0001A197
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

		// Token: 0x060007AA RID: 1962 RVA: 0x0001BFA8 File Offset: 0x0001A1A8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (long num2 in this.KeyIds)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasClientToken)
			{
				num ^= this.ClientToken.GetHashCode();
			}
			return num;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001C024 File Offset: 0x0001A224
		public override bool Equals(object obj)
		{
			GameSaveDataRequest gameSaveDataRequest = obj as GameSaveDataRequest;
			if (gameSaveDataRequest == null)
			{
				return false;
			}
			if (this.KeyIds.Count != gameSaveDataRequest.KeyIds.Count)
			{
				return false;
			}
			for (int i = 0; i < this.KeyIds.Count; i++)
			{
				if (!this.KeyIds[i].Equals(gameSaveDataRequest.KeyIds[i]))
				{
					return false;
				}
			}
			return this.HasClientToken == gameSaveDataRequest.HasClientToken && (!this.HasClientToken || this.ClientToken.Equals(gameSaveDataRequest.ClientToken));
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001C0C0 File Offset: 0x0001A2C0
		public void Deserialize(Stream stream)
		{
			GameSaveDataRequest.Deserialize(stream, this);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001C0CA File Offset: 0x0001A2CA
		public static GameSaveDataRequest Deserialize(Stream stream, GameSaveDataRequest instance)
		{
			return GameSaveDataRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001C0D8 File Offset: 0x0001A2D8
		public static GameSaveDataRequest DeserializeLengthDelimited(Stream stream)
		{
			GameSaveDataRequest gameSaveDataRequest = new GameSaveDataRequest();
			GameSaveDataRequest.DeserializeLengthDelimited(stream, gameSaveDataRequest);
			return gameSaveDataRequest;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001C0F4 File Offset: 0x0001A2F4
		public static GameSaveDataRequest DeserializeLengthDelimited(Stream stream, GameSaveDataRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameSaveDataRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001C11C File Offset: 0x0001A31C
		public static GameSaveDataRequest Deserialize(Stream stream, GameSaveDataRequest instance, long limit)
		{
			if (instance.KeyIds == null)
			{
				instance.KeyIds = new List<long>();
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
					instance.KeyIds.Add((long)ProtocolParser.ReadUInt64(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001C1CC File Offset: 0x0001A3CC
		public void Serialize(Stream stream)
		{
			GameSaveDataRequest.Serialize(stream, this);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001C1D8 File Offset: 0x0001A3D8
		public static void Serialize(Stream stream, GameSaveDataRequest instance)
		{
			if (instance.KeyIds.Count > 0)
			{
				foreach (long val in instance.KeyIds)
				{
					stream.WriteByte(8);
					ProtocolParser.WriteUInt64(stream, (ulong)val);
				}
			}
			if (instance.HasClientToken)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ClientToken));
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001C260 File Offset: 0x0001A460
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.KeyIds.Count > 0)
			{
				foreach (long val in this.KeyIds)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)val);
				}
			}
			if (this.HasClientToken)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ClientToken));
			}
			return num;
		}

		// Token: 0x04000262 RID: 610
		private List<long> _KeyIds = new List<long>();

		// Token: 0x04000263 RID: 611
		public bool HasClientToken;

		// Token: 0x04000264 RID: 612
		private int _ClientToken;

		// Token: 0x0200058D RID: 1421
		public enum PacketID
		{
			// Token: 0x04001F03 RID: 7939
			ID = 357,
			// Token: 0x04001F04 RID: 7940
			System = 0
		}
	}
}
