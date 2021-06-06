using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000A2 RID: 162
	public class DBAction : IProtoBuf
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00029182 File Offset: 0x00027382
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x0002918A File Offset: 0x0002738A
		public DatabaseAction Action { get; set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00029193 File Offset: 0x00027393
		// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x0002919B File Offset: 0x0002739B
		public DatabaseResult Result { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x000291A4 File Offset: 0x000273A4
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x000291AC File Offset: 0x000273AC
		public long MetaData
		{
			get
			{
				return this._MetaData;
			}
			set
			{
				this._MetaData = value;
				this.HasMetaData = true;
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000291BC File Offset: 0x000273BC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Action.GetHashCode();
			num ^= this.Result.GetHashCode();
			if (this.HasMetaData)
			{
				num ^= this.MetaData.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00029220 File Offset: 0x00027420
		public override bool Equals(object obj)
		{
			DBAction dbaction = obj as DBAction;
			return dbaction != null && this.Action.Equals(dbaction.Action) && this.Result.Equals(dbaction.Result) && this.HasMetaData == dbaction.HasMetaData && (!this.HasMetaData || this.MetaData.Equals(dbaction.MetaData));
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000292AE File Offset: 0x000274AE
		public void Deserialize(Stream stream)
		{
			DBAction.Deserialize(stream, this);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000292B8 File Offset: 0x000274B8
		public static DBAction Deserialize(Stream stream, DBAction instance)
		{
			return DBAction.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000292C4 File Offset: 0x000274C4
		public static DBAction DeserializeLengthDelimited(Stream stream)
		{
			DBAction dbaction = new DBAction();
			DBAction.DeserializeLengthDelimited(stream, dbaction);
			return dbaction;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000292E0 File Offset: 0x000274E0
		public static DBAction DeserializeLengthDelimited(Stream stream, DBAction instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DBAction.Deserialize(stream, instance, num);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00029308 File Offset: 0x00027508
		public static DBAction Deserialize(Stream stream, DBAction instance, long limit)
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
							instance.MetaData = (long)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.Result = (DatabaseResult)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Action = (DatabaseAction)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000293B7 File Offset: 0x000275B7
		public void Serialize(Stream stream)
		{
			DBAction.Serialize(stream, this);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000293C0 File Offset: 0x000275C0
		public static void Serialize(Stream stream, DBAction instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Action));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Result));
			if (instance.HasMetaData)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MetaData);
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00029414 File Offset: 0x00027614
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Action));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Result));
			if (this.HasMetaData)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.MetaData);
			}
			return num + 2U;
		}

		// Token: 0x040003C3 RID: 963
		public bool HasMetaData;

		// Token: 0x040003C4 RID: 964
		private long _MetaData;

		// Token: 0x020005AC RID: 1452
		public enum PacketID
		{
			// Token: 0x04001F5E RID: 8030
			ID = 216
		}
	}
}
