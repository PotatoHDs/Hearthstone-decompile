using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003C8 RID: 968
	public class UpdateControlPropertiesRequest : IProtoBuf
	{
		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06003F40 RID: 16192 RVA: 0x000CA648 File Offset: 0x000C8848
		// (set) Token: 0x06003F41 RID: 16193 RVA: 0x000CA650 File Offset: 0x000C8850
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

		// Token: 0x06003F42 RID: 16194 RVA: 0x000CA660 File Offset: 0x000C8860
		public void SetMatchmakerId(uint val)
		{
			this.MatchmakerId = val;
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06003F43 RID: 16195 RVA: 0x000CA669 File Offset: 0x000C8869
		// (set) Token: 0x06003F44 RID: 16196 RVA: 0x000CA671 File Offset: 0x000C8871
		public MatchmakerControlProperties ControlProperties
		{
			get
			{
				return this._ControlProperties;
			}
			set
			{
				this._ControlProperties = value;
				this.HasControlProperties = (value != null);
			}
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x000CA684 File Offset: 0x000C8884
		public void SetControlProperties(MatchmakerControlProperties val)
		{
			this.ControlProperties = val;
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06003F46 RID: 16198 RVA: 0x000CA68D File Offset: 0x000C888D
		// (set) Token: 0x06003F47 RID: 16199 RVA: 0x000CA695 File Offset: 0x000C8895
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

		// Token: 0x06003F48 RID: 16200 RVA: 0x000CA6A5 File Offset: 0x000C88A5
		public void SetMatchmakerGuid(ulong val)
		{
			this.MatchmakerGuid = val;
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x000CA6B0 File Offset: 0x000C88B0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMatchmakerId)
			{
				num ^= this.MatchmakerId.GetHashCode();
			}
			if (this.HasControlProperties)
			{
				num ^= this.ControlProperties.GetHashCode();
			}
			if (this.HasMatchmakerGuid)
			{
				num ^= this.MatchmakerGuid.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x000CA714 File Offset: 0x000C8914
		public override bool Equals(object obj)
		{
			UpdateControlPropertiesRequest updateControlPropertiesRequest = obj as UpdateControlPropertiesRequest;
			return updateControlPropertiesRequest != null && this.HasMatchmakerId == updateControlPropertiesRequest.HasMatchmakerId && (!this.HasMatchmakerId || this.MatchmakerId.Equals(updateControlPropertiesRequest.MatchmakerId)) && this.HasControlProperties == updateControlPropertiesRequest.HasControlProperties && (!this.HasControlProperties || this.ControlProperties.Equals(updateControlPropertiesRequest.ControlProperties)) && this.HasMatchmakerGuid == updateControlPropertiesRequest.HasMatchmakerGuid && (!this.HasMatchmakerGuid || this.MatchmakerGuid.Equals(updateControlPropertiesRequest.MatchmakerGuid));
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x000CA7B5 File Offset: 0x000C89B5
		public static UpdateControlPropertiesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateControlPropertiesRequest>(bs, 0, -1);
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x000CA7BF File Offset: 0x000C89BF
		public void Deserialize(Stream stream)
		{
			UpdateControlPropertiesRequest.Deserialize(stream, this);
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x000CA7C9 File Offset: 0x000C89C9
		public static UpdateControlPropertiesRequest Deserialize(Stream stream, UpdateControlPropertiesRequest instance)
		{
			return UpdateControlPropertiesRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x000CA7D4 File Offset: 0x000C89D4
		public static UpdateControlPropertiesRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateControlPropertiesRequest updateControlPropertiesRequest = new UpdateControlPropertiesRequest();
			UpdateControlPropertiesRequest.DeserializeLengthDelimited(stream, updateControlPropertiesRequest);
			return updateControlPropertiesRequest;
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x000CA7F0 File Offset: 0x000C89F0
		public static UpdateControlPropertiesRequest DeserializeLengthDelimited(Stream stream, UpdateControlPropertiesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateControlPropertiesRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x000CA818 File Offset: 0x000C8A18
		public static UpdateControlPropertiesRequest Deserialize(Stream stream, UpdateControlPropertiesRequest instance, long limit)
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
					if (num != 18)
					{
						if (num != 25)
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
					else if (instance.ControlProperties == null)
					{
						instance.ControlProperties = MatchmakerControlProperties.DeserializeLengthDelimited(stream);
					}
					else
					{
						MatchmakerControlProperties.DeserializeLengthDelimited(stream, instance.ControlProperties);
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

		// Token: 0x06003F52 RID: 16210 RVA: 0x000CA8ED File Offset: 0x000C8AED
		public void Serialize(Stream stream)
		{
			UpdateControlPropertiesRequest.Serialize(stream, this);
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x000CA8F8 File Offset: 0x000C8AF8
		public static void Serialize(Stream stream, UpdateControlPropertiesRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.MatchmakerId);
			}
			if (instance.HasControlProperties)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ControlProperties.GetSerializedSize());
				MatchmakerControlProperties.Serialize(stream, instance.ControlProperties);
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x000CA974 File Offset: 0x000C8B74
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMatchmakerId)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasControlProperties)
			{
				num += 1U;
				uint serializedSize = this.ControlProperties.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasMatchmakerGuid)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x04001634 RID: 5684
		public bool HasMatchmakerId;

		// Token: 0x04001635 RID: 5685
		private uint _MatchmakerId;

		// Token: 0x04001636 RID: 5686
		public bool HasControlProperties;

		// Token: 0x04001637 RID: 5687
		private MatchmakerControlProperties _ControlProperties;

		// Token: 0x04001638 RID: 5688
		public bool HasMatchmakerGuid;

		// Token: 0x04001639 RID: 5689
		private ulong _MatchmakerGuid;
	}
}
