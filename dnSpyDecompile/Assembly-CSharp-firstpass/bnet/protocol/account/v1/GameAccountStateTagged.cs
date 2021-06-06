using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200053D RID: 1341
	public class GameAccountStateTagged : IProtoBuf
	{
		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x060060C9 RID: 24777 RVA: 0x00125007 File Offset: 0x00123207
		// (set) Token: 0x060060CA RID: 24778 RVA: 0x0012500F File Offset: 0x0012320F
		public GameAccountState GameAccountState
		{
			get
			{
				return this._GameAccountState;
			}
			set
			{
				this._GameAccountState = value;
				this.HasGameAccountState = (value != null);
			}
		}

		// Token: 0x060060CB RID: 24779 RVA: 0x00125022 File Offset: 0x00123222
		public void SetGameAccountState(GameAccountState val)
		{
			this.GameAccountState = val;
		}

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x060060CC RID: 24780 RVA: 0x0012502B File Offset: 0x0012322B
		// (set) Token: 0x060060CD RID: 24781 RVA: 0x00125033 File Offset: 0x00123233
		public GameAccountFieldTags GameAccountTags
		{
			get
			{
				return this._GameAccountTags;
			}
			set
			{
				this._GameAccountTags = value;
				this.HasGameAccountTags = (value != null);
			}
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x00125046 File Offset: 0x00123246
		public void SetGameAccountTags(GameAccountFieldTags val)
		{
			this.GameAccountTags = val;
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x00125050 File Offset: 0x00123250
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameAccountState)
			{
				num ^= this.GameAccountState.GetHashCode();
			}
			if (this.HasGameAccountTags)
			{
				num ^= this.GameAccountTags.GetHashCode();
			}
			return num;
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x00125098 File Offset: 0x00123298
		public override bool Equals(object obj)
		{
			GameAccountStateTagged gameAccountStateTagged = obj as GameAccountStateTagged;
			return gameAccountStateTagged != null && this.HasGameAccountState == gameAccountStateTagged.HasGameAccountState && (!this.HasGameAccountState || this.GameAccountState.Equals(gameAccountStateTagged.GameAccountState)) && this.HasGameAccountTags == gameAccountStateTagged.HasGameAccountTags && (!this.HasGameAccountTags || this.GameAccountTags.Equals(gameAccountStateTagged.GameAccountTags));
		}

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x060060D1 RID: 24785 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060060D2 RID: 24786 RVA: 0x00125108 File Offset: 0x00123308
		public static GameAccountStateTagged ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountStateTagged>(bs, 0, -1);
		}

		// Token: 0x060060D3 RID: 24787 RVA: 0x00125112 File Offset: 0x00123312
		public void Deserialize(Stream stream)
		{
			GameAccountStateTagged.Deserialize(stream, this);
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x0012511C File Offset: 0x0012331C
		public static GameAccountStateTagged Deserialize(Stream stream, GameAccountStateTagged instance)
		{
			return GameAccountStateTagged.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x00125128 File Offset: 0x00123328
		public static GameAccountStateTagged DeserializeLengthDelimited(Stream stream)
		{
			GameAccountStateTagged gameAccountStateTagged = new GameAccountStateTagged();
			GameAccountStateTagged.DeserializeLengthDelimited(stream, gameAccountStateTagged);
			return gameAccountStateTagged;
		}

		// Token: 0x060060D6 RID: 24790 RVA: 0x00125144 File Offset: 0x00123344
		public static GameAccountStateTagged DeserializeLengthDelimited(Stream stream, GameAccountStateTagged instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountStateTagged.Deserialize(stream, instance, num);
		}

		// Token: 0x060060D7 RID: 24791 RVA: 0x0012516C File Offset: 0x0012336C
		public static GameAccountStateTagged Deserialize(Stream stream, GameAccountStateTagged instance, long limit)
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
				else if (num != 10)
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
					else if (instance.GameAccountTags == null)
					{
						instance.GameAccountTags = GameAccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountFieldTags.DeserializeLengthDelimited(stream, instance.GameAccountTags);
					}
				}
				else if (instance.GameAccountState == null)
				{
					instance.GameAccountState = GameAccountState.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountState.DeserializeLengthDelimited(stream, instance.GameAccountState);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060060D8 RID: 24792 RVA: 0x0012523E File Offset: 0x0012343E
		public void Serialize(Stream stream)
		{
			GameAccountStateTagged.Serialize(stream, this);
		}

		// Token: 0x060060D9 RID: 24793 RVA: 0x00125248 File Offset: 0x00123448
		public static void Serialize(Stream stream, GameAccountStateTagged instance)
		{
			if (instance.HasGameAccountState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountState.GetSerializedSize());
				GameAccountState.Serialize(stream, instance.GameAccountState);
			}
			if (instance.HasGameAccountTags)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountTags.GetSerializedSize());
				GameAccountFieldTags.Serialize(stream, instance.GameAccountTags);
			}
		}

		// Token: 0x060060DA RID: 24794 RVA: 0x001252B0 File Offset: 0x001234B0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameAccountState)
			{
				num += 1U;
				uint serializedSize = this.GameAccountState.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGameAccountTags)
			{
				num += 1U;
				uint serializedSize2 = this.GameAccountTags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001DCB RID: 7627
		public bool HasGameAccountState;

		// Token: 0x04001DCC RID: 7628
		private GameAccountState _GameAccountState;

		// Token: 0x04001DCD RID: 7629
		public bool HasGameAccountTags;

		// Token: 0x04001DCE RID: 7630
		private GameAccountFieldTags _GameAccountTags;
	}
}
