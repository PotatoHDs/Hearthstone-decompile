using System;
using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x020004A0 RID: 1184
	public class UpdateMemberStateRequest : IProtoBuf
	{
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x0600528B RID: 21131 RVA: 0x000FF439 File Offset: 0x000FD639
		// (set) Token: 0x0600528C RID: 21132 RVA: 0x000FF441 File Offset: 0x000FD641
		public ChannelId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x000FF454 File Offset: 0x000FD654
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x0600528E RID: 21134 RVA: 0x000FF45D File Offset: 0x000FD65D
		// (set) Token: 0x0600528F RID: 21135 RVA: 0x000FF465 File Offset: 0x000FD665
		public UpdateMemberStateOptions Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
				this.HasOptions = (value != null);
			}
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x000FF478 File Offset: 0x000FD678
		public void SetOptions(UpdateMemberStateOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x000FF484 File Offset: 0x000FD684
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x000FF4CC File Offset: 0x000FD6CC
		public override bool Equals(object obj)
		{
			UpdateMemberStateRequest updateMemberStateRequest = obj as UpdateMemberStateRequest;
			return updateMemberStateRequest != null && this.HasChannelId == updateMemberStateRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(updateMemberStateRequest.ChannelId)) && this.HasOptions == updateMemberStateRequest.HasOptions && (!this.HasOptions || this.Options.Equals(updateMemberStateRequest.Options));
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06005293 RID: 21139 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x000FF53C File Offset: 0x000FD73C
		public static UpdateMemberStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateMemberStateRequest>(bs, 0, -1);
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x000FF546 File Offset: 0x000FD746
		public void Deserialize(Stream stream)
		{
			UpdateMemberStateRequest.Deserialize(stream, this);
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x000FF550 File Offset: 0x000FD750
		public static UpdateMemberStateRequest Deserialize(Stream stream, UpdateMemberStateRequest instance)
		{
			return UpdateMemberStateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005297 RID: 21143 RVA: 0x000FF55C File Offset: 0x000FD75C
		public static UpdateMemberStateRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateMemberStateRequest updateMemberStateRequest = new UpdateMemberStateRequest();
			UpdateMemberStateRequest.DeserializeLengthDelimited(stream, updateMemberStateRequest);
			return updateMemberStateRequest;
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x000FF578 File Offset: 0x000FD778
		public static UpdateMemberStateRequest DeserializeLengthDelimited(Stream stream, UpdateMemberStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateMemberStateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005299 RID: 21145 RVA: 0x000FF5A0 File Offset: 0x000FD7A0
		public static UpdateMemberStateRequest Deserialize(Stream stream, UpdateMemberStateRequest instance, long limit)
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
				else if (num != 10)
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
					else if (instance.Options == null)
					{
						instance.Options = UpdateMemberStateOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						UpdateMemberStateOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
				}
				else if (instance.ChannelId == null)
				{
					instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
				}
				else
				{
					ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600529A RID: 21146 RVA: 0x000FF672 File Offset: 0x000FD872
		public void Serialize(Stream stream)
		{
			UpdateMemberStateRequest.Serialize(stream, this);
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x000FF67C File Offset: 0x000FD87C
		public static void Serialize(Stream stream, UpdateMemberStateRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				UpdateMemberStateOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x000FF6E4 File Offset: 0x000FD8E4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize2 = this.Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001A6C RID: 6764
		public bool HasChannelId;

		// Token: 0x04001A6D RID: 6765
		private ChannelId _ChannelId;

		// Token: 0x04001A6E RID: 6766
		public bool HasOptions;

		// Token: 0x04001A6F RID: 6767
		private UpdateMemberStateOptions _Options;
	}
}
