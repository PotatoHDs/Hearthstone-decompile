using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003C7 RID: 967
	public class UnregisterMatchmakerRequest : IProtoBuf
	{
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06003F2D RID: 16173 RVA: 0x000CA3B8 File Offset: 0x000C85B8
		// (set) Token: 0x06003F2E RID: 16174 RVA: 0x000CA3C0 File Offset: 0x000C85C0
		public uint MatchmakerId
		{
			get
			{
				return this._MatchmakerId;
			}
			set
			{
				this._MatchmakerId = value;
				this.HasMatchmakerId = true;
			}
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x000CA3D0 File Offset: 0x000C85D0
		public void SetMatchmakerId(uint val)
		{
			this.MatchmakerId = val;
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06003F30 RID: 16176 RVA: 0x000CA3D9 File Offset: 0x000C85D9
		// (set) Token: 0x06003F31 RID: 16177 RVA: 0x000CA3E1 File Offset: 0x000C85E1
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

		// Token: 0x06003F32 RID: 16178 RVA: 0x000CA3F1 File Offset: 0x000C85F1
		public void SetMatchmakerGuid(ulong val)
		{
			this.MatchmakerGuid = val;
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x000CA3FC File Offset: 0x000C85FC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMatchmakerId)
			{
				num ^= this.MatchmakerId.GetHashCode();
			}
			if (this.HasMatchmakerGuid)
			{
				num ^= this.MatchmakerGuid.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x000CA448 File Offset: 0x000C8648
		public override bool Equals(object obj)
		{
			UnregisterMatchmakerRequest unregisterMatchmakerRequest = obj as UnregisterMatchmakerRequest;
			return unregisterMatchmakerRequest != null && this.HasMatchmakerId == unregisterMatchmakerRequest.HasMatchmakerId && (!this.HasMatchmakerId || this.MatchmakerId.Equals(unregisterMatchmakerRequest.MatchmakerId)) && this.HasMatchmakerGuid == unregisterMatchmakerRequest.HasMatchmakerGuid && (!this.HasMatchmakerGuid || this.MatchmakerGuid.Equals(unregisterMatchmakerRequest.MatchmakerGuid));
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06003F35 RID: 16181 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x000CA4BE File Offset: 0x000C86BE
		public static UnregisterMatchmakerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnregisterMatchmakerRequest>(bs, 0, -1);
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x000CA4C8 File Offset: 0x000C86C8
		public void Deserialize(Stream stream)
		{
			UnregisterMatchmakerRequest.Deserialize(stream, this);
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x000CA4D2 File Offset: 0x000C86D2
		public static UnregisterMatchmakerRequest Deserialize(Stream stream, UnregisterMatchmakerRequest instance)
		{
			return UnregisterMatchmakerRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x000CA4E0 File Offset: 0x000C86E0
		public static UnregisterMatchmakerRequest DeserializeLengthDelimited(Stream stream)
		{
			UnregisterMatchmakerRequest unregisterMatchmakerRequest = new UnregisterMatchmakerRequest();
			UnregisterMatchmakerRequest.DeserializeLengthDelimited(stream, unregisterMatchmakerRequest);
			return unregisterMatchmakerRequest;
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x000CA4FC File Offset: 0x000C86FC
		public static UnregisterMatchmakerRequest DeserializeLengthDelimited(Stream stream, UnregisterMatchmakerRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnregisterMatchmakerRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x000CA524 File Offset: 0x000C8724
		public static UnregisterMatchmakerRequest Deserialize(Stream stream, UnregisterMatchmakerRequest instance, long limit)
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
				else if (num != 13)
				{
					if (num != 17)
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
						instance.MatchmakerGuid = binaryReader.ReadUInt64();
					}
				}
				else
				{
					instance.MatchmakerId = binaryReader.ReadUInt32();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x000CA5C3 File Offset: 0x000C87C3
		public void Serialize(Stream stream)
		{
			UnregisterMatchmakerRequest.Serialize(stream, this);
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x000CA5CC File Offset: 0x000C87CC
		public static void Serialize(Stream stream, UnregisterMatchmakerRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.MatchmakerId);
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x000CA618 File Offset: 0x000C8818
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMatchmakerId)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasMatchmakerGuid)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x04001630 RID: 5680
		public bool HasMatchmakerId;

		// Token: 0x04001631 RID: 5681
		private uint _MatchmakerId;

		// Token: 0x04001632 RID: 5682
		public bool HasMatchmakerGuid;

		// Token: 0x04001633 RID: 5683
		private ulong _MatchmakerGuid;
	}
}
