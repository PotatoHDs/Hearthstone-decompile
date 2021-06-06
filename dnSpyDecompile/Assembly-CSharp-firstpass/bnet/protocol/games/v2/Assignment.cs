using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.games.v2
{
	// Token: 0x0200036E RID: 878
	public class Assignment : IProtoBuf
	{
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060037AC RID: 14252 RVA: 0x000B6BEC File Offset: 0x000B4DEC
		// (set) Token: 0x060037AD RID: 14253 RVA: 0x000B6BF4 File Offset: 0x000B4DF4
		public GameAccountHandle GameAccount
		{
			get
			{
				return this._GameAccount;
			}
			set
			{
				this._GameAccount = value;
				this.HasGameAccount = (value != null);
			}
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000B6C07 File Offset: 0x000B4E07
		public void SetGameAccount(GameAccountHandle val)
		{
			this.GameAccount = val;
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060037AF RID: 14255 RVA: 0x000B6C10 File Offset: 0x000B4E10
		// (set) Token: 0x060037B0 RID: 14256 RVA: 0x000B6C18 File Offset: 0x000B4E18
		public uint TeamIndex
		{
			get
			{
				return this._TeamIndex;
			}
			set
			{
				this._TeamIndex = value;
				this.HasTeamIndex = true;
			}
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000B6C28 File Offset: 0x000B4E28
		public void SetTeamIndex(uint val)
		{
			this.TeamIndex = val;
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000B6C34 File Offset: 0x000B4E34
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameAccount)
			{
				num ^= this.GameAccount.GetHashCode();
			}
			if (this.HasTeamIndex)
			{
				num ^= this.TeamIndex.GetHashCode();
			}
			return num;
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000B6C80 File Offset: 0x000B4E80
		public override bool Equals(object obj)
		{
			Assignment assignment = obj as Assignment;
			return assignment != null && this.HasGameAccount == assignment.HasGameAccount && (!this.HasGameAccount || this.GameAccount.Equals(assignment.GameAccount)) && this.HasTeamIndex == assignment.HasTeamIndex && (!this.HasTeamIndex || this.TeamIndex.Equals(assignment.TeamIndex));
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060037B4 RID: 14260 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000B6CF3 File Offset: 0x000B4EF3
		public static Assignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Assignment>(bs, 0, -1);
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000B6CFD File Offset: 0x000B4EFD
		public void Deserialize(Stream stream)
		{
			Assignment.Deserialize(stream, this);
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000B6D07 File Offset: 0x000B4F07
		public static Assignment Deserialize(Stream stream, Assignment instance)
		{
			return Assignment.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000B6D14 File Offset: 0x000B4F14
		public static Assignment DeserializeLengthDelimited(Stream stream)
		{
			Assignment assignment = new Assignment();
			Assignment.DeserializeLengthDelimited(stream, assignment);
			return assignment;
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000B6D30 File Offset: 0x000B4F30
		public static Assignment DeserializeLengthDelimited(Stream stream, Assignment instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Assignment.Deserialize(stream, instance, num);
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x000B6D58 File Offset: 0x000B4F58
		public static Assignment Deserialize(Stream stream, Assignment instance, long limit)
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
						instance.TeamIndex = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.GameAccount == null)
				{
					instance.GameAccount = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.GameAccount);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000B6E0A File Offset: 0x000B500A
		public void Serialize(Stream stream)
		{
			Assignment.Serialize(stream, this);
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x000B6E14 File Offset: 0x000B5014
		public static void Serialize(Stream stream, Assignment instance)
		{
			if (instance.HasGameAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
			if (instance.HasTeamIndex)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.TeamIndex);
			}
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000B6E6C File Offset: 0x000B506C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameAccount)
			{
				num += 1U;
				uint serializedSize = this.GameAccount.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasTeamIndex)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.TeamIndex);
			}
			return num;
		}

		// Token: 0x040014C9 RID: 5321
		public bool HasGameAccount;

		// Token: 0x040014CA RID: 5322
		private GameAccountHandle _GameAccount;

		// Token: 0x040014CB RID: 5323
		public bool HasTeamIndex;

		// Token: 0x040014CC RID: 5324
		private uint _TeamIndex;
	}
}
