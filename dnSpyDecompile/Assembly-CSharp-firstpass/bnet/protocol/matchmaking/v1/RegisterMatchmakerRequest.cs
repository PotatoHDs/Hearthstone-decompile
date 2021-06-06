using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003C5 RID: 965
	public class RegisterMatchmakerRequest : IProtoBuf
	{
		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06003F04 RID: 16132 RVA: 0x000C9D68 File Offset: 0x000C7F68
		// (set) Token: 0x06003F05 RID: 16133 RVA: 0x000C9D70 File Offset: 0x000C7F70
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

		// Token: 0x06003F06 RID: 16134 RVA: 0x000C9D80 File Offset: 0x000C7F80
		public void SetMatchmakerId(uint val)
		{
			this.MatchmakerId = val;
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06003F07 RID: 16135 RVA: 0x000C9D89 File Offset: 0x000C7F89
		// (set) Token: 0x06003F08 RID: 16136 RVA: 0x000C9D91 File Offset: 0x000C7F91
		public MatchmakerAttributeInfo AttributeInfo
		{
			get
			{
				return this._AttributeInfo;
			}
			set
			{
				this._AttributeInfo = value;
				this.HasAttributeInfo = (value != null);
			}
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x000C9DA4 File Offset: 0x000C7FA4
		public void SetAttributeInfo(MatchmakerAttributeInfo val)
		{
			this.AttributeInfo = val;
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06003F0A RID: 16138 RVA: 0x000C9DAD File Offset: 0x000C7FAD
		// (set) Token: 0x06003F0B RID: 16139 RVA: 0x000C9DB5 File Offset: 0x000C7FB5
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

		// Token: 0x06003F0C RID: 16140 RVA: 0x000C9DC8 File Offset: 0x000C7FC8
		public void SetControlProperties(MatchmakerControlProperties val)
		{
			this.ControlProperties = val;
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06003F0D RID: 16141 RVA: 0x000C9DD1 File Offset: 0x000C7FD1
		// (set) Token: 0x06003F0E RID: 16142 RVA: 0x000C9DD9 File Offset: 0x000C7FD9
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

		// Token: 0x06003F0F RID: 16143 RVA: 0x000C9DE9 File Offset: 0x000C7FE9
		public void SetMatchmakerGuid(ulong val)
		{
			this.MatchmakerGuid = val;
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x000C9DF4 File Offset: 0x000C7FF4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMatchmakerId)
			{
				num ^= this.MatchmakerId.GetHashCode();
			}
			if (this.HasAttributeInfo)
			{
				num ^= this.AttributeInfo.GetHashCode();
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

		// Token: 0x06003F11 RID: 16145 RVA: 0x000C9E6C File Offset: 0x000C806C
		public override bool Equals(object obj)
		{
			RegisterMatchmakerRequest registerMatchmakerRequest = obj as RegisterMatchmakerRequest;
			return registerMatchmakerRequest != null && this.HasMatchmakerId == registerMatchmakerRequest.HasMatchmakerId && (!this.HasMatchmakerId || this.MatchmakerId.Equals(registerMatchmakerRequest.MatchmakerId)) && this.HasAttributeInfo == registerMatchmakerRequest.HasAttributeInfo && (!this.HasAttributeInfo || this.AttributeInfo.Equals(registerMatchmakerRequest.AttributeInfo)) && this.HasControlProperties == registerMatchmakerRequest.HasControlProperties && (!this.HasControlProperties || this.ControlProperties.Equals(registerMatchmakerRequest.ControlProperties)) && this.HasMatchmakerGuid == registerMatchmakerRequest.HasMatchmakerGuid && (!this.HasMatchmakerGuid || this.MatchmakerGuid.Equals(registerMatchmakerRequest.MatchmakerGuid));
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06003F12 RID: 16146 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x000C9F38 File Offset: 0x000C8138
		public static RegisterMatchmakerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterMatchmakerRequest>(bs, 0, -1);
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x000C9F42 File Offset: 0x000C8142
		public void Deserialize(Stream stream)
		{
			RegisterMatchmakerRequest.Deserialize(stream, this);
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x000C9F4C File Offset: 0x000C814C
		public static RegisterMatchmakerRequest Deserialize(Stream stream, RegisterMatchmakerRequest instance)
		{
			return RegisterMatchmakerRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x000C9F58 File Offset: 0x000C8158
		public static RegisterMatchmakerRequest DeserializeLengthDelimited(Stream stream)
		{
			RegisterMatchmakerRequest registerMatchmakerRequest = new RegisterMatchmakerRequest();
			RegisterMatchmakerRequest.DeserializeLengthDelimited(stream, registerMatchmakerRequest);
			return registerMatchmakerRequest;
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x000C9F74 File Offset: 0x000C8174
		public static RegisterMatchmakerRequest DeserializeLengthDelimited(Stream stream, RegisterMatchmakerRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegisterMatchmakerRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x000C9F9C File Offset: 0x000C819C
		public static RegisterMatchmakerRequest Deserialize(Stream stream, RegisterMatchmakerRequest instance, long limit)
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
					if (num <= 18)
					{
						if (num == 13)
						{
							instance.MatchmakerId = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 18)
						{
							if (instance.AttributeInfo == null)
							{
								instance.AttributeInfo = MatchmakerAttributeInfo.DeserializeLengthDelimited(stream);
								continue;
							}
							MatchmakerAttributeInfo.DeserializeLengthDelimited(stream, instance.AttributeInfo);
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 33)
						{
							instance.MatchmakerGuid = binaryReader.ReadUInt64();
							continue;
						}
					}
					else
					{
						if (instance.ControlProperties == null)
						{
							instance.ControlProperties = MatchmakerControlProperties.DeserializeLengthDelimited(stream);
							continue;
						}
						MatchmakerControlProperties.DeserializeLengthDelimited(stream, instance.ControlProperties);
						continue;
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

		// Token: 0x06003F19 RID: 16153 RVA: 0x000CA0AE File Offset: 0x000C82AE
		public void Serialize(Stream stream)
		{
			RegisterMatchmakerRequest.Serialize(stream, this);
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x000CA0B8 File Offset: 0x000C82B8
		public static void Serialize(Stream stream, RegisterMatchmakerRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.MatchmakerId);
			}
			if (instance.HasAttributeInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AttributeInfo.GetSerializedSize());
				MatchmakerAttributeInfo.Serialize(stream, instance.AttributeInfo);
			}
			if (instance.HasControlProperties)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ControlProperties.GetSerializedSize());
				MatchmakerControlProperties.Serialize(stream, instance.ControlProperties);
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(33);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x000CA160 File Offset: 0x000C8360
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMatchmakerId)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasAttributeInfo)
			{
				num += 1U;
				uint serializedSize = this.AttributeInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasControlProperties)
			{
				num += 1U;
				uint serializedSize2 = this.ControlProperties.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasMatchmakerGuid)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x04001626 RID: 5670
		public bool HasMatchmakerId;

		// Token: 0x04001627 RID: 5671
		private uint _MatchmakerId;

		// Token: 0x04001628 RID: 5672
		public bool HasAttributeInfo;

		// Token: 0x04001629 RID: 5673
		private MatchmakerAttributeInfo _AttributeInfo;

		// Token: 0x0400162A RID: 5674
		public bool HasControlProperties;

		// Token: 0x0400162B RID: 5675
		private MatchmakerControlProperties _ControlProperties;

		// Token: 0x0400162C RID: 5676
		public bool HasMatchmakerGuid;

		// Token: 0x0400162D RID: 5677
		private ulong _MatchmakerGuid;
	}
}
