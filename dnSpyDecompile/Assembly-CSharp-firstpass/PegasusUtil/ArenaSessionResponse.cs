using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000093 RID: 147
	public class ArenaSessionResponse : IProtoBuf
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00024D7B File Offset: 0x00022F7B
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x00024D83 File Offset: 0x00022F83
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00024D8C File Offset: 0x00022F8C
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x00024D94 File Offset: 0x00022F94
		public ArenaSession Session
		{
			get
			{
				return this._Session;
			}
			set
			{
				this._Session = value;
				this.HasSession = (value != null);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00024DA7 File Offset: 0x00022FA7
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x00024DAF File Offset: 0x00022FAF
		public ArenaSeasonInfo CurrentSeason
		{
			get
			{
				return this._CurrentSeason;
			}
			set
			{
				this._CurrentSeason = value;
				this.HasCurrentSeason = (value != null);
			}
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00024DC4 File Offset: 0x00022FC4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ErrorCode.GetHashCode();
			if (this.HasSession)
			{
				num ^= this.Session.GetHashCode();
			}
			if (this.HasCurrentSeason)
			{
				num ^= this.CurrentSeason.GetHashCode();
			}
			return num;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00024E24 File Offset: 0x00023024
		public override bool Equals(object obj)
		{
			ArenaSessionResponse arenaSessionResponse = obj as ArenaSessionResponse;
			return arenaSessionResponse != null && this.ErrorCode.Equals(arenaSessionResponse.ErrorCode) && this.HasSession == arenaSessionResponse.HasSession && (!this.HasSession || this.Session.Equals(arenaSessionResponse.Session)) && this.HasCurrentSeason == arenaSessionResponse.HasCurrentSeason && (!this.HasCurrentSeason || this.CurrentSeason.Equals(arenaSessionResponse.CurrentSeason));
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00024EB7 File Offset: 0x000230B7
		public void Deserialize(Stream stream)
		{
			ArenaSessionResponse.Deserialize(stream, this);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00024EC1 File Offset: 0x000230C1
		public static ArenaSessionResponse Deserialize(Stream stream, ArenaSessionResponse instance)
		{
			return ArenaSessionResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00024ECC File Offset: 0x000230CC
		public static ArenaSessionResponse DeserializeLengthDelimited(Stream stream)
		{
			ArenaSessionResponse arenaSessionResponse = new ArenaSessionResponse();
			ArenaSessionResponse.DeserializeLengthDelimited(stream, arenaSessionResponse);
			return arenaSessionResponse;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00024EE8 File Offset: 0x000230E8
		public static ArenaSessionResponse DeserializeLengthDelimited(Stream stream, ArenaSessionResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ArenaSessionResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00024F10 File Offset: 0x00023110
		public static ArenaSessionResponse Deserialize(Stream stream, ArenaSessionResponse instance, long limit)
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
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.CurrentSeason == null)
						{
							instance.CurrentSeason = ArenaSeasonInfo.DeserializeLengthDelimited(stream);
						}
						else
						{
							ArenaSeasonInfo.DeserializeLengthDelimited(stream, instance.CurrentSeason);
						}
					}
					else if (instance.Session == null)
					{
						instance.Session = ArenaSession.DeserializeLengthDelimited(stream);
					}
					else
					{
						ArenaSession.DeserializeLengthDelimited(stream, instance.Session);
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

		// Token: 0x060009FC RID: 2556 RVA: 0x00024FF8 File Offset: 0x000231F8
		public void Serialize(Stream stream)
		{
			ArenaSessionResponse.Serialize(stream, this);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00025004 File Offset: 0x00023204
		public static void Serialize(Stream stream, ArenaSessionResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			if (instance.HasSession)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Session.GetSerializedSize());
				ArenaSession.Serialize(stream, instance.Session);
			}
			if (instance.HasCurrentSeason)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.CurrentSeason.GetSerializedSize());
				ArenaSeasonInfo.Serialize(stream, instance.CurrentSeason);
			}
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00025080 File Offset: 0x00023280
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			if (this.HasSession)
			{
				num += 1U;
				uint serializedSize = this.Session.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasCurrentSeason)
			{
				num += 1U;
				uint serializedSize2 = this.CurrentSeason.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1U;
		}

		// Token: 0x0400037A RID: 890
		public bool HasSession;

		// Token: 0x0400037B RID: 891
		private ArenaSession _Session;

		// Token: 0x0400037C RID: 892
		public bool HasCurrentSeason;

		// Token: 0x0400037D RID: 893
		private ArenaSeasonInfo _CurrentSeason;

		// Token: 0x020005A5 RID: 1445
		public enum PacketID
		{
			// Token: 0x04001F4F RID: 8015
			ID = 351
		}
	}
}
