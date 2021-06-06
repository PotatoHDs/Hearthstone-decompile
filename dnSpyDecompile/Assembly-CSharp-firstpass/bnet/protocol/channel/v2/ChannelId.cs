using System;
using System.IO;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000470 RID: 1136
	public class ChannelId : IProtoBuf
	{
		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06004E29 RID: 20009 RVA: 0x000F2B0D File Offset: 0x000F0D0D
		// (set) Token: 0x06004E2A RID: 20010 RVA: 0x000F2B15 File Offset: 0x000F0D15
		public ProcessId Host
		{
			get
			{
				return this._Host;
			}
			set
			{
				this._Host = value;
				this.HasHost = (value != null);
			}
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x000F2B28 File Offset: 0x000F0D28
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06004E2C RID: 20012 RVA: 0x000F2B31 File Offset: 0x000F0D31
		// (set) Token: 0x06004E2D RID: 20013 RVA: 0x000F2B39 File Offset: 0x000F0D39
		public uint Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x000F2B49 File Offset: 0x000F0D49
		public void SetId(uint val)
		{
			this.Id = val;
		}

		// Token: 0x06004E2F RID: 20015 RVA: 0x000F2B54 File Offset: 0x000F0D54
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004E30 RID: 20016 RVA: 0x000F2BA0 File Offset: 0x000F0DA0
		public override bool Equals(object obj)
		{
			ChannelId channelId = obj as ChannelId;
			return channelId != null && this.HasHost == channelId.HasHost && (!this.HasHost || this.Host.Equals(channelId.Host)) && this.HasId == channelId.HasId && (!this.HasId || this.Id.Equals(channelId.Id));
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06004E31 RID: 20017 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x000F2C13 File Offset: 0x000F0E13
		public static ChannelId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelId>(bs, 0, -1);
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x000F2C1D File Offset: 0x000F0E1D
		public void Deserialize(Stream stream)
		{
			ChannelId.Deserialize(stream, this);
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x000F2C27 File Offset: 0x000F0E27
		public static ChannelId Deserialize(Stream stream, ChannelId instance)
		{
			return ChannelId.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x000F2C34 File Offset: 0x000F0E34
		public static ChannelId DeserializeLengthDelimited(Stream stream)
		{
			ChannelId channelId = new ChannelId();
			ChannelId.DeserializeLengthDelimited(stream, channelId);
			return channelId;
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x000F2C50 File Offset: 0x000F0E50
		public static ChannelId DeserializeLengthDelimited(Stream stream, ChannelId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelId.Deserialize(stream, instance, num);
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x000F2C78 File Offset: 0x000F0E78
		public static ChannelId Deserialize(Stream stream, ChannelId instance, long limit)
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
				else if (num != 18)
				{
					if (num != 29)
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
						instance.Id = binaryReader.ReadUInt32();
					}
				}
				else if (instance.Host == null)
				{
					instance.Host = ProcessId.DeserializeLengthDelimited(stream);
				}
				else
				{
					ProcessId.DeserializeLengthDelimited(stream, instance.Host);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x000F2D31 File Offset: 0x000F0F31
		public void Serialize(Stream stream)
		{
			ChannelId.Serialize(stream, this);
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x000F2D3C File Offset: 0x000F0F3C
		public static void Serialize(Stream stream, ChannelId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasHost)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.HasId)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Id);
			}
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x000F2D9C File Offset: 0x000F0F9C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize = this.Host.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasId)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x0400196C RID: 6508
		public bool HasHost;

		// Token: 0x0400196D RID: 6509
		private ProcessId _Host;

		// Token: 0x0400196E RID: 6510
		public bool HasId;

		// Token: 0x0400196F RID: 6511
		private uint _Id;
	}
}
