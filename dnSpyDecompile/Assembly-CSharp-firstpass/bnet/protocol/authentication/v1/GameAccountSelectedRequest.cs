using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004F9 RID: 1273
	public class GameAccountSelectedRequest : IProtoBuf
	{
		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06005A74 RID: 23156 RVA: 0x00114195 File Offset: 0x00112395
		// (set) Token: 0x06005A75 RID: 23157 RVA: 0x0011419D File Offset: 0x0011239D
		public uint Result { get; set; }

		// Token: 0x06005A76 RID: 23158 RVA: 0x001141A6 File Offset: 0x001123A6
		public void SetResult(uint val)
		{
			this.Result = val;
		}

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06005A77 RID: 23159 RVA: 0x001141AF File Offset: 0x001123AF
		// (set) Token: 0x06005A78 RID: 23160 RVA: 0x001141B7 File Offset: 0x001123B7
		public EntityId GameAccountId
		{
			get
			{
				return this._GameAccountId;
			}
			set
			{
				this._GameAccountId = value;
				this.HasGameAccountId = (value != null);
			}
		}

		// Token: 0x06005A79 RID: 23161 RVA: 0x001141CA File Offset: 0x001123CA
		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x06005A7A RID: 23162 RVA: 0x001141D4 File Offset: 0x001123D4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Result.GetHashCode();
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005A7B RID: 23163 RVA: 0x00114218 File Offset: 0x00112418
		public override bool Equals(object obj)
		{
			GameAccountSelectedRequest gameAccountSelectedRequest = obj as GameAccountSelectedRequest;
			return gameAccountSelectedRequest != null && this.Result.Equals(gameAccountSelectedRequest.Result) && this.HasGameAccountId == gameAccountSelectedRequest.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(gameAccountSelectedRequest.GameAccountId));
		}

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06005A7C RID: 23164 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005A7D RID: 23165 RVA: 0x00114275 File Offset: 0x00112475
		public static GameAccountSelectedRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountSelectedRequest>(bs, 0, -1);
		}

		// Token: 0x06005A7E RID: 23166 RVA: 0x0011427F File Offset: 0x0011247F
		public void Deserialize(Stream stream)
		{
			GameAccountSelectedRequest.Deserialize(stream, this);
		}

		// Token: 0x06005A7F RID: 23167 RVA: 0x00114289 File Offset: 0x00112489
		public static GameAccountSelectedRequest Deserialize(Stream stream, GameAccountSelectedRequest instance)
		{
			return GameAccountSelectedRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005A80 RID: 23168 RVA: 0x00114294 File Offset: 0x00112494
		public static GameAccountSelectedRequest DeserializeLengthDelimited(Stream stream)
		{
			GameAccountSelectedRequest gameAccountSelectedRequest = new GameAccountSelectedRequest();
			GameAccountSelectedRequest.DeserializeLengthDelimited(stream, gameAccountSelectedRequest);
			return gameAccountSelectedRequest;
		}

		// Token: 0x06005A81 RID: 23169 RVA: 0x001142B0 File Offset: 0x001124B0
		public static GameAccountSelectedRequest DeserializeLengthDelimited(Stream stream, GameAccountSelectedRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountSelectedRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x001142D8 File Offset: 0x001124D8
		public static GameAccountSelectedRequest Deserialize(Stream stream, GameAccountSelectedRequest instance, long limit)
		{
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
				}
				else
				{
					instance.Result = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005A83 RID: 23171 RVA: 0x00114389 File Offset: 0x00112589
		public void Serialize(Stream stream)
		{
			GameAccountSelectedRequest.Serialize(stream, this);
		}

		// Token: 0x06005A84 RID: 23172 RVA: 0x00114394 File Offset: 0x00112594
		public static void Serialize(Stream stream, GameAccountSelectedRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Result);
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
		}

		// Token: 0x06005A85 RID: 23173 RVA: 0x001143E4 File Offset: 0x001125E4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt32(this.Result);
			if (this.HasGameAccountId)
			{
				num += 1U;
				uint serializedSize = this.GameAccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1U;
		}

		// Token: 0x04001C25 RID: 7205
		public bool HasGameAccountId;

		// Token: 0x04001C26 RID: 7206
		private EntityId _GameAccountId;
	}
}
