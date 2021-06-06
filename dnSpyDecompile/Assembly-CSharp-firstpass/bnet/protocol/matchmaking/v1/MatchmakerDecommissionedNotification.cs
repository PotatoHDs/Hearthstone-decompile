using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003C4 RID: 964
	public class MatchmakerDecommissionedNotification : IProtoBuf
	{
		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06003EF4 RID: 16116 RVA: 0x000C9B87 File Offset: 0x000C7D87
		// (set) Token: 0x06003EF5 RID: 16117 RVA: 0x000C9B8F File Offset: 0x000C7D8F
		public ulong MatchmakerGuid
		{
			get
			{
				return this._MatchmakerGuid;
			}
			set
			{
				this._MatchmakerGuid = value;
				this.HasMatchmakerGuid = true;
			}
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x000C9B9F File Offset: 0x000C7D9F
		public void SetMatchmakerGuid(ulong val)
		{
			this.MatchmakerGuid = val;
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x000C9BA8 File Offset: 0x000C7DA8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMatchmakerGuid)
			{
				num ^= this.MatchmakerGuid.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x000C9BDC File Offset: 0x000C7DDC
		public override bool Equals(object obj)
		{
			MatchmakerDecommissionedNotification matchmakerDecommissionedNotification = obj as MatchmakerDecommissionedNotification;
			return matchmakerDecommissionedNotification != null && this.HasMatchmakerGuid == matchmakerDecommissionedNotification.HasMatchmakerGuid && (!this.HasMatchmakerGuid || this.MatchmakerGuid.Equals(matchmakerDecommissionedNotification.MatchmakerGuid));
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06003EF9 RID: 16121 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x000C9C24 File Offset: 0x000C7E24
		public static MatchmakerDecommissionedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MatchmakerDecommissionedNotification>(bs, 0, -1);
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x000C9C2E File Offset: 0x000C7E2E
		public void Deserialize(Stream stream)
		{
			MatchmakerDecommissionedNotification.Deserialize(stream, this);
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x000C9C38 File Offset: 0x000C7E38
		public static MatchmakerDecommissionedNotification Deserialize(Stream stream, MatchmakerDecommissionedNotification instance)
		{
			return MatchmakerDecommissionedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x000C9C44 File Offset: 0x000C7E44
		public static MatchmakerDecommissionedNotification DeserializeLengthDelimited(Stream stream)
		{
			MatchmakerDecommissionedNotification matchmakerDecommissionedNotification = new MatchmakerDecommissionedNotification();
			MatchmakerDecommissionedNotification.DeserializeLengthDelimited(stream, matchmakerDecommissionedNotification);
			return matchmakerDecommissionedNotification;
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x000C9C60 File Offset: 0x000C7E60
		public static MatchmakerDecommissionedNotification DeserializeLengthDelimited(Stream stream, MatchmakerDecommissionedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MatchmakerDecommissionedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x000C9C88 File Offset: 0x000C7E88
		public static MatchmakerDecommissionedNotification Deserialize(Stream stream, MatchmakerDecommissionedNotification instance, long limit)
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
				else if (num == 9)
				{
					instance.MatchmakerGuid = binaryReader.ReadUInt64();
				}
				else
				{
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

		// Token: 0x06003F00 RID: 16128 RVA: 0x000C9D0F File Offset: 0x000C7F0F
		public void Serialize(Stream stream)
		{
			MatchmakerDecommissionedNotification.Serialize(stream, this);
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x000C9D18 File Offset: 0x000C7F18
		public static void Serialize(Stream stream, MatchmakerDecommissionedNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x000C9D48 File Offset: 0x000C7F48
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMatchmakerGuid)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x04001624 RID: 5668
		public bool HasMatchmakerGuid;

		// Token: 0x04001625 RID: 5669
		private ulong _MatchmakerGuid;
	}
}
