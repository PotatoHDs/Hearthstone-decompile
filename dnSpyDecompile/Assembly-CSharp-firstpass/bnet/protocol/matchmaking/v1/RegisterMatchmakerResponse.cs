using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003C6 RID: 966
	public class RegisterMatchmakerResponse : IProtoBuf
	{
		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06003F1D RID: 16157 RVA: 0x000CA1D6 File Offset: 0x000C83D6
		// (set) Token: 0x06003F1E RID: 16158 RVA: 0x000CA1DE File Offset: 0x000C83DE
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

		// Token: 0x06003F1F RID: 16159 RVA: 0x000CA1EE File Offset: 0x000C83EE
		public void SetMatchmakerGuid(ulong val)
		{
			this.MatchmakerGuid = val;
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x000CA1F8 File Offset: 0x000C83F8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMatchmakerGuid)
			{
				num ^= this.MatchmakerGuid.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x000CA22C File Offset: 0x000C842C
		public override bool Equals(object obj)
		{
			RegisterMatchmakerResponse registerMatchmakerResponse = obj as RegisterMatchmakerResponse;
			return registerMatchmakerResponse != null && this.HasMatchmakerGuid == registerMatchmakerResponse.HasMatchmakerGuid && (!this.HasMatchmakerGuid || this.MatchmakerGuid.Equals(registerMatchmakerResponse.MatchmakerGuid));
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06003F22 RID: 16162 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x000CA274 File Offset: 0x000C8474
		public static RegisterMatchmakerResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterMatchmakerResponse>(bs, 0, -1);
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x000CA27E File Offset: 0x000C847E
		public void Deserialize(Stream stream)
		{
			RegisterMatchmakerResponse.Deserialize(stream, this);
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x000CA288 File Offset: 0x000C8488
		public static RegisterMatchmakerResponse Deserialize(Stream stream, RegisterMatchmakerResponse instance)
		{
			return RegisterMatchmakerResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x000CA294 File Offset: 0x000C8494
		public static RegisterMatchmakerResponse DeserializeLengthDelimited(Stream stream)
		{
			RegisterMatchmakerResponse registerMatchmakerResponse = new RegisterMatchmakerResponse();
			RegisterMatchmakerResponse.DeserializeLengthDelimited(stream, registerMatchmakerResponse);
			return registerMatchmakerResponse;
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x000CA2B0 File Offset: 0x000C84B0
		public static RegisterMatchmakerResponse DeserializeLengthDelimited(Stream stream, RegisterMatchmakerResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegisterMatchmakerResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x000CA2D8 File Offset: 0x000C84D8
		public static RegisterMatchmakerResponse Deserialize(Stream stream, RegisterMatchmakerResponse instance, long limit)
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

		// Token: 0x06003F29 RID: 16169 RVA: 0x000CA35F File Offset: 0x000C855F
		public void Serialize(Stream stream)
		{
			RegisterMatchmakerResponse.Serialize(stream, this);
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x000CA368 File Offset: 0x000C8568
		public static void Serialize(Stream stream, RegisterMatchmakerResponse instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x000CA398 File Offset: 0x000C8598
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

		// Token: 0x0400162E RID: 5678
		public bool HasMatchmakerGuid;

		// Token: 0x0400162F RID: 5679
		private ulong _MatchmakerGuid;
	}
}
