using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200014E RID: 334
	public class ScenarioGuestHeroDbRecord : IProtoBuf
	{
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x0004BB1F File Offset: 0x00049D1F
		// (set) Token: 0x06001619 RID: 5657 RVA: 0x0004BB27 File Offset: 0x00049D27
		public int ScenarioId { get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x0004BB30 File Offset: 0x00049D30
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x0004BB38 File Offset: 0x00049D38
		public int GuestHeroId { get; set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x0004BB41 File Offset: 0x00049D41
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x0004BB49 File Offset: 0x00049D49
		public int SortOrder { get; set; }

		// Token: 0x0600161E RID: 5662 RVA: 0x0004BB54 File Offset: 0x00049D54
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ScenarioId.GetHashCode() ^ this.GuestHeroId.GetHashCode() ^ this.SortOrder.GetHashCode();
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0004BB9C File Offset: 0x00049D9C
		public override bool Equals(object obj)
		{
			ScenarioGuestHeroDbRecord scenarioGuestHeroDbRecord = obj as ScenarioGuestHeroDbRecord;
			return scenarioGuestHeroDbRecord != null && this.ScenarioId.Equals(scenarioGuestHeroDbRecord.ScenarioId) && this.GuestHeroId.Equals(scenarioGuestHeroDbRecord.GuestHeroId) && this.SortOrder.Equals(scenarioGuestHeroDbRecord.SortOrder);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0004BBFE File Offset: 0x00049DFE
		public void Deserialize(Stream stream)
		{
			ScenarioGuestHeroDbRecord.Deserialize(stream, this);
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0004BC08 File Offset: 0x00049E08
		public static ScenarioGuestHeroDbRecord Deserialize(Stream stream, ScenarioGuestHeroDbRecord instance)
		{
			return ScenarioGuestHeroDbRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0004BC14 File Offset: 0x00049E14
		public static ScenarioGuestHeroDbRecord DeserializeLengthDelimited(Stream stream)
		{
			ScenarioGuestHeroDbRecord scenarioGuestHeroDbRecord = new ScenarioGuestHeroDbRecord();
			ScenarioGuestHeroDbRecord.DeserializeLengthDelimited(stream, scenarioGuestHeroDbRecord);
			return scenarioGuestHeroDbRecord;
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0004BC30 File Offset: 0x00049E30
		public static ScenarioGuestHeroDbRecord DeserializeLengthDelimited(Stream stream, ScenarioGuestHeroDbRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ScenarioGuestHeroDbRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0004BC58 File Offset: 0x00049E58
		public static ScenarioGuestHeroDbRecord Deserialize(Stream stream, ScenarioGuestHeroDbRecord instance, long limit)
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
					if (num != 16)
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
							instance.SortOrder = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.GuestHeroId = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.ScenarioId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x0004BD08 File Offset: 0x00049F08
		public void Serialize(Stream stream)
		{
			ScenarioGuestHeroDbRecord.Serialize(stream, this);
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0004BD11 File Offset: 0x00049F11
		public static void Serialize(Stream stream, ScenarioGuestHeroDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ScenarioId));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GuestHeroId));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SortOrder));
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0004BD51 File Offset: 0x00049F51
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.ScenarioId)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.GuestHeroId)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.SortOrder)) + 3U;
		}
	}
}
