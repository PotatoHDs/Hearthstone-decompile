using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000BA RID: 186
	public class AssetsVersionResponse : IProtoBuf
	{
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0002FD8B File Offset: 0x0002DF8B
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x0002FD93 File Offset: 0x0002DF93
		public int Version { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0002FD9C File Offset: 0x0002DF9C
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x0002FDA4 File Offset: 0x0002DFA4
		public ReturningPlayerInfo ReturningPlayerInfo
		{
			get
			{
				return this._ReturningPlayerInfo;
			}
			set
			{
				this._ReturningPlayerInfo = value;
				this.HasReturningPlayerInfo = (value != null);
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002FDB8 File Offset: 0x0002DFB8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Version.GetHashCode();
			if (this.HasReturningPlayerInfo)
			{
				num ^= this.ReturningPlayerInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002FDFC File Offset: 0x0002DFFC
		public override bool Equals(object obj)
		{
			AssetsVersionResponse assetsVersionResponse = obj as AssetsVersionResponse;
			return assetsVersionResponse != null && this.Version.Equals(assetsVersionResponse.Version) && this.HasReturningPlayerInfo == assetsVersionResponse.HasReturningPlayerInfo && (!this.HasReturningPlayerInfo || this.ReturningPlayerInfo.Equals(assetsVersionResponse.ReturningPlayerInfo));
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0002FE59 File Offset: 0x0002E059
		public void Deserialize(Stream stream)
		{
			AssetsVersionResponse.Deserialize(stream, this);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0002FE63 File Offset: 0x0002E063
		public static AssetsVersionResponse Deserialize(Stream stream, AssetsVersionResponse instance)
		{
			return AssetsVersionResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0002FE70 File Offset: 0x0002E070
		public static AssetsVersionResponse DeserializeLengthDelimited(Stream stream)
		{
			AssetsVersionResponse assetsVersionResponse = new AssetsVersionResponse();
			AssetsVersionResponse.DeserializeLengthDelimited(stream, assetsVersionResponse);
			return assetsVersionResponse;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0002FE8C File Offset: 0x0002E08C
		public static AssetsVersionResponse DeserializeLengthDelimited(Stream stream, AssetsVersionResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AssetsVersionResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002FEB4 File Offset: 0x0002E0B4
		public static AssetsVersionResponse Deserialize(Stream stream, AssetsVersionResponse instance, long limit)
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
					else if (instance.ReturningPlayerInfo == null)
					{
						instance.ReturningPlayerInfo = ReturningPlayerInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						ReturningPlayerInfo.DeserializeLengthDelimited(stream, instance.ReturningPlayerInfo);
					}
				}
				else
				{
					instance.Version = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002FF66 File Offset: 0x0002E166
		public void Serialize(Stream stream)
		{
			AssetsVersionResponse.Serialize(stream, this);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002FF70 File Offset: 0x0002E170
		public static void Serialize(Stream stream, AssetsVersionResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Version));
			if (instance.HasReturningPlayerInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ReturningPlayerInfo.GetSerializedSize());
				ReturningPlayerInfo.Serialize(stream, instance.ReturningPlayerInfo);
			}
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002FFC0 File Offset: 0x0002E1C0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Version));
			if (this.HasReturningPlayerInfo)
			{
				num += 1U;
				uint serializedSize = this.ReturningPlayerInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1U;
		}

		// Token: 0x04000470 RID: 1136
		public bool HasReturningPlayerInfo;

		// Token: 0x04000471 RID: 1137
		private ReturningPlayerInfo _ReturningPlayerInfo;

		// Token: 0x020005C7 RID: 1479
		public enum PacketID
		{
			// Token: 0x04001FA2 RID: 8098
			ID = 304
		}
	}
}
