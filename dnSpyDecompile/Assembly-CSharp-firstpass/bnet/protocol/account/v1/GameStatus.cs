using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000533 RID: 1331
	public class GameStatus : IProtoBuf
	{
		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06005FCE RID: 24526 RVA: 0x00122434 File Offset: 0x00120634
		// (set) Token: 0x06005FCF RID: 24527 RVA: 0x0012243C File Offset: 0x0012063C
		public bool IsSuspended
		{
			get
			{
				return this._IsSuspended;
			}
			set
			{
				this._IsSuspended = value;
				this.HasIsSuspended = true;
			}
		}

		// Token: 0x06005FD0 RID: 24528 RVA: 0x0012244C File Offset: 0x0012064C
		public void SetIsSuspended(bool val)
		{
			this.IsSuspended = val;
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06005FD1 RID: 24529 RVA: 0x00122455 File Offset: 0x00120655
		// (set) Token: 0x06005FD2 RID: 24530 RVA: 0x0012245D File Offset: 0x0012065D
		public bool IsBanned
		{
			get
			{
				return this._IsBanned;
			}
			set
			{
				this._IsBanned = value;
				this.HasIsBanned = true;
			}
		}

		// Token: 0x06005FD3 RID: 24531 RVA: 0x0012246D File Offset: 0x0012066D
		public void SetIsBanned(bool val)
		{
			this.IsBanned = val;
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x06005FD4 RID: 24532 RVA: 0x00122476 File Offset: 0x00120676
		// (set) Token: 0x06005FD5 RID: 24533 RVA: 0x0012247E File Offset: 0x0012067E
		public ulong SuspensionExpires
		{
			get
			{
				return this._SuspensionExpires;
			}
			set
			{
				this._SuspensionExpires = value;
				this.HasSuspensionExpires = true;
			}
		}

		// Token: 0x06005FD6 RID: 24534 RVA: 0x0012248E File Offset: 0x0012068E
		public void SetSuspensionExpires(ulong val)
		{
			this.SuspensionExpires = val;
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06005FD7 RID: 24535 RVA: 0x00122497 File Offset: 0x00120697
		// (set) Token: 0x06005FD8 RID: 24536 RVA: 0x0012249F File Offset: 0x0012069F
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x06005FD9 RID: 24537 RVA: 0x001224AF File Offset: 0x001206AF
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06005FDA RID: 24538 RVA: 0x001224B8 File Offset: 0x001206B8
		// (set) Token: 0x06005FDB RID: 24539 RVA: 0x001224C0 File Offset: 0x001206C0
		public bool IsLocked
		{
			get
			{
				return this._IsLocked;
			}
			set
			{
				this._IsLocked = value;
				this.HasIsLocked = true;
			}
		}

		// Token: 0x06005FDC RID: 24540 RVA: 0x001224D0 File Offset: 0x001206D0
		public void SetIsLocked(bool val)
		{
			this.IsLocked = val;
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06005FDD RID: 24541 RVA: 0x001224D9 File Offset: 0x001206D9
		// (set) Token: 0x06005FDE RID: 24542 RVA: 0x001224E1 File Offset: 0x001206E1
		public bool IsBamUnlockable
		{
			get
			{
				return this._IsBamUnlockable;
			}
			set
			{
				this._IsBamUnlockable = value;
				this.HasIsBamUnlockable = true;
			}
		}

		// Token: 0x06005FDF RID: 24543 RVA: 0x001224F1 File Offset: 0x001206F1
		public void SetIsBamUnlockable(bool val)
		{
			this.IsBamUnlockable = val;
		}

		// Token: 0x06005FE0 RID: 24544 RVA: 0x001224FC File Offset: 0x001206FC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIsSuspended)
			{
				num ^= this.IsSuspended.GetHashCode();
			}
			if (this.HasIsBanned)
			{
				num ^= this.IsBanned.GetHashCode();
			}
			if (this.HasSuspensionExpires)
			{
				num ^= this.SuspensionExpires.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasIsLocked)
			{
				num ^= this.IsLocked.GetHashCode();
			}
			if (this.HasIsBamUnlockable)
			{
				num ^= this.IsBamUnlockable.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005FE1 RID: 24545 RVA: 0x001225AC File Offset: 0x001207AC
		public override bool Equals(object obj)
		{
			GameStatus gameStatus = obj as GameStatus;
			return gameStatus != null && this.HasIsSuspended == gameStatus.HasIsSuspended && (!this.HasIsSuspended || this.IsSuspended.Equals(gameStatus.IsSuspended)) && this.HasIsBanned == gameStatus.HasIsBanned && (!this.HasIsBanned || this.IsBanned.Equals(gameStatus.IsBanned)) && this.HasSuspensionExpires == gameStatus.HasSuspensionExpires && (!this.HasSuspensionExpires || this.SuspensionExpires.Equals(gameStatus.SuspensionExpires)) && this.HasProgram == gameStatus.HasProgram && (!this.HasProgram || this.Program.Equals(gameStatus.Program)) && this.HasIsLocked == gameStatus.HasIsLocked && (!this.HasIsLocked || this.IsLocked.Equals(gameStatus.IsLocked)) && this.HasIsBamUnlockable == gameStatus.HasIsBamUnlockable && (!this.HasIsBamUnlockable || this.IsBamUnlockable.Equals(gameStatus.IsBamUnlockable));
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06005FE2 RID: 24546 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005FE3 RID: 24547 RVA: 0x001226DA File Offset: 0x001208DA
		public static GameStatus ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameStatus>(bs, 0, -1);
		}

		// Token: 0x06005FE4 RID: 24548 RVA: 0x001226E4 File Offset: 0x001208E4
		public void Deserialize(Stream stream)
		{
			GameStatus.Deserialize(stream, this);
		}

		// Token: 0x06005FE5 RID: 24549 RVA: 0x001226EE File Offset: 0x001208EE
		public static GameStatus Deserialize(Stream stream, GameStatus instance)
		{
			return GameStatus.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005FE6 RID: 24550 RVA: 0x001226FC File Offset: 0x001208FC
		public static GameStatus DeserializeLengthDelimited(Stream stream)
		{
			GameStatus gameStatus = new GameStatus();
			GameStatus.DeserializeLengthDelimited(stream, gameStatus);
			return gameStatus;
		}

		// Token: 0x06005FE7 RID: 24551 RVA: 0x00122718 File Offset: 0x00120918
		public static GameStatus DeserializeLengthDelimited(Stream stream, GameStatus instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameStatus.Deserialize(stream, instance, num);
		}

		// Token: 0x06005FE8 RID: 24552 RVA: 0x00122740 File Offset: 0x00120940
		public static GameStatus Deserialize(Stream stream, GameStatus instance, long limit)
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
					if (num <= 48)
					{
						if (num == 32)
						{
							instance.IsSuspended = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 40)
						{
							instance.IsBanned = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 48)
						{
							instance.SuspensionExpires = ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 61)
						{
							instance.Program = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 64)
						{
							instance.IsLocked = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 72)
						{
							instance.IsBamUnlockable = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06005FE9 RID: 24553 RVA: 0x00122847 File Offset: 0x00120A47
		public void Serialize(Stream stream)
		{
			GameStatus.Serialize(stream, this);
		}

		// Token: 0x06005FEA RID: 24554 RVA: 0x00122850 File Offset: 0x00120A50
		public static void Serialize(Stream stream, GameStatus instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasIsSuspended)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsSuspended);
			}
			if (instance.HasIsBanned)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.IsBanned);
			}
			if (instance.HasSuspensionExpires)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.SuspensionExpires);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(61);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasIsLocked)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.IsLocked);
			}
			if (instance.HasIsBamUnlockable)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.IsBamUnlockable);
			}
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x0012290C File Offset: 0x00120B0C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIsSuspended)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsBanned)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasSuspensionExpires)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.SuspensionExpires);
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasIsLocked)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsBamUnlockable)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001D89 RID: 7561
		public bool HasIsSuspended;

		// Token: 0x04001D8A RID: 7562
		private bool _IsSuspended;

		// Token: 0x04001D8B RID: 7563
		public bool HasIsBanned;

		// Token: 0x04001D8C RID: 7564
		private bool _IsBanned;

		// Token: 0x04001D8D RID: 7565
		public bool HasSuspensionExpires;

		// Token: 0x04001D8E RID: 7566
		private ulong _SuspensionExpires;

		// Token: 0x04001D8F RID: 7567
		public bool HasProgram;

		// Token: 0x04001D90 RID: 7568
		private uint _Program;

		// Token: 0x04001D91 RID: 7569
		public bool HasIsLocked;

		// Token: 0x04001D92 RID: 7570
		private bool _IsLocked;

		// Token: 0x04001D93 RID: 7571
		public bool HasIsBamUnlockable;

		// Token: 0x04001D94 RID: 7572
		private bool _IsBamUnlockable;
	}
}
