using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000529 RID: 1321
	public class GameAccountFieldTags : IProtoBuf
	{
		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06005E73 RID: 24179 RVA: 0x0011E289 File Offset: 0x0011C489
		// (set) Token: 0x06005E74 RID: 24180 RVA: 0x0011E291 File Offset: 0x0011C491
		public uint GameLevelInfoTag
		{
			get
			{
				return this._GameLevelInfoTag;
			}
			set
			{
				this._GameLevelInfoTag = value;
				this.HasGameLevelInfoTag = true;
			}
		}

		// Token: 0x06005E75 RID: 24181 RVA: 0x0011E2A1 File Offset: 0x0011C4A1
		public void SetGameLevelInfoTag(uint val)
		{
			this.GameLevelInfoTag = val;
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x06005E76 RID: 24182 RVA: 0x0011E2AA File Offset: 0x0011C4AA
		// (set) Token: 0x06005E77 RID: 24183 RVA: 0x0011E2B2 File Offset: 0x0011C4B2
		public uint GameTimeInfoTag
		{
			get
			{
				return this._GameTimeInfoTag;
			}
			set
			{
				this._GameTimeInfoTag = value;
				this.HasGameTimeInfoTag = true;
			}
		}

		// Token: 0x06005E78 RID: 24184 RVA: 0x0011E2C2 File Offset: 0x0011C4C2
		public void SetGameTimeInfoTag(uint val)
		{
			this.GameTimeInfoTag = val;
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06005E79 RID: 24185 RVA: 0x0011E2CB File Offset: 0x0011C4CB
		// (set) Token: 0x06005E7A RID: 24186 RVA: 0x0011E2D3 File Offset: 0x0011C4D3
		public uint GameStatusTag
		{
			get
			{
				return this._GameStatusTag;
			}
			set
			{
				this._GameStatusTag = value;
				this.HasGameStatusTag = true;
			}
		}

		// Token: 0x06005E7B RID: 24187 RVA: 0x0011E2E3 File Offset: 0x0011C4E3
		public void SetGameStatusTag(uint val)
		{
			this.GameStatusTag = val;
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06005E7C RID: 24188 RVA: 0x0011E2EC File Offset: 0x0011C4EC
		// (set) Token: 0x06005E7D RID: 24189 RVA: 0x0011E2F4 File Offset: 0x0011C4F4
		public uint RafInfoTag
		{
			get
			{
				return this._RafInfoTag;
			}
			set
			{
				this._RafInfoTag = value;
				this.HasRafInfoTag = true;
			}
		}

		// Token: 0x06005E7E RID: 24190 RVA: 0x0011E304 File Offset: 0x0011C504
		public void SetRafInfoTag(uint val)
		{
			this.RafInfoTag = val;
		}

		// Token: 0x06005E7F RID: 24191 RVA: 0x0011E310 File Offset: 0x0011C510
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameLevelInfoTag)
			{
				num ^= this.GameLevelInfoTag.GetHashCode();
			}
			if (this.HasGameTimeInfoTag)
			{
				num ^= this.GameTimeInfoTag.GetHashCode();
			}
			if (this.HasGameStatusTag)
			{
				num ^= this.GameStatusTag.GetHashCode();
			}
			if (this.HasRafInfoTag)
			{
				num ^= this.RafInfoTag.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005E80 RID: 24192 RVA: 0x0011E390 File Offset: 0x0011C590
		public override bool Equals(object obj)
		{
			GameAccountFieldTags gameAccountFieldTags = obj as GameAccountFieldTags;
			return gameAccountFieldTags != null && this.HasGameLevelInfoTag == gameAccountFieldTags.HasGameLevelInfoTag && (!this.HasGameLevelInfoTag || this.GameLevelInfoTag.Equals(gameAccountFieldTags.GameLevelInfoTag)) && this.HasGameTimeInfoTag == gameAccountFieldTags.HasGameTimeInfoTag && (!this.HasGameTimeInfoTag || this.GameTimeInfoTag.Equals(gameAccountFieldTags.GameTimeInfoTag)) && this.HasGameStatusTag == gameAccountFieldTags.HasGameStatusTag && (!this.HasGameStatusTag || this.GameStatusTag.Equals(gameAccountFieldTags.GameStatusTag)) && this.HasRafInfoTag == gameAccountFieldTags.HasRafInfoTag && (!this.HasRafInfoTag || this.RafInfoTag.Equals(gameAccountFieldTags.RafInfoTag));
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06005E81 RID: 24193 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005E82 RID: 24194 RVA: 0x0011E462 File Offset: 0x0011C662
		public static GameAccountFieldTags ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountFieldTags>(bs, 0, -1);
		}

		// Token: 0x06005E83 RID: 24195 RVA: 0x0011E46C File Offset: 0x0011C66C
		public void Deserialize(Stream stream)
		{
			GameAccountFieldTags.Deserialize(stream, this);
		}

		// Token: 0x06005E84 RID: 24196 RVA: 0x0011E476 File Offset: 0x0011C676
		public static GameAccountFieldTags Deserialize(Stream stream, GameAccountFieldTags instance)
		{
			return GameAccountFieldTags.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005E85 RID: 24197 RVA: 0x0011E484 File Offset: 0x0011C684
		public static GameAccountFieldTags DeserializeLengthDelimited(Stream stream)
		{
			GameAccountFieldTags gameAccountFieldTags = new GameAccountFieldTags();
			GameAccountFieldTags.DeserializeLengthDelimited(stream, gameAccountFieldTags);
			return gameAccountFieldTags;
		}

		// Token: 0x06005E86 RID: 24198 RVA: 0x0011E4A0 File Offset: 0x0011C6A0
		public static GameAccountFieldTags DeserializeLengthDelimited(Stream stream, GameAccountFieldTags instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountFieldTags.Deserialize(stream, instance, num);
		}

		// Token: 0x06005E87 RID: 24199 RVA: 0x0011E4C8 File Offset: 0x0011C6C8
		public static GameAccountFieldTags Deserialize(Stream stream, GameAccountFieldTags instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num <= 29)
					{
						if (num == 21)
						{
							instance.GameLevelInfoTag = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 29)
						{
							instance.GameTimeInfoTag = binaryReader.ReadUInt32();
							continue;
						}
					}
					else
					{
						if (num == 37)
						{
							instance.GameStatusTag = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 45)
						{
							instance.RafInfoTag = binaryReader.ReadUInt32();
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005E88 RID: 24200 RVA: 0x0011E5A0 File Offset: 0x0011C7A0
		public void Serialize(Stream stream)
		{
			GameAccountFieldTags.Serialize(stream, this);
		}

		// Token: 0x06005E89 RID: 24201 RVA: 0x0011E5AC File Offset: 0x0011C7AC
		public static void Serialize(Stream stream, GameAccountFieldTags instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasGameLevelInfoTag)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.GameLevelInfoTag);
			}
			if (instance.HasGameTimeInfoTag)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.GameTimeInfoTag);
			}
			if (instance.HasGameStatusTag)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.GameStatusTag);
			}
			if (instance.HasRafInfoTag)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.RafInfoTag);
			}
		}

		// Token: 0x06005E8A RID: 24202 RVA: 0x0011E630 File Offset: 0x0011C830
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameLevelInfoTag)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasGameTimeInfoTag)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasGameStatusTag)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasRafInfoTag)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04001D07 RID: 7431
		public bool HasGameLevelInfoTag;

		// Token: 0x04001D08 RID: 7432
		private uint _GameLevelInfoTag;

		// Token: 0x04001D09 RID: 7433
		public bool HasGameTimeInfoTag;

		// Token: 0x04001D0A RID: 7434
		private uint _GameTimeInfoTag;

		// Token: 0x04001D0B RID: 7435
		public bool HasGameStatusTag;

		// Token: 0x04001D0C RID: 7436
		private uint _GameStatusTag;

		// Token: 0x04001D0D RID: 7437
		public bool HasRafInfoTag;

		// Token: 0x04001D0E RID: 7438
		private uint _RafInfoTag;
	}
}
