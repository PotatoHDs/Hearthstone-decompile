using System;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x0200035F RID: 863
	public class GetAchievementsFileRequest : IProtoBuf
	{
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06003688 RID: 13960 RVA: 0x000B40E6 File Offset: 0x000B22E6
		// (set) Token: 0x06003689 RID: 13961 RVA: 0x000B40EE File Offset: 0x000B22EE
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

		// Token: 0x0600368A RID: 13962 RVA: 0x000B4101 File Offset: 0x000B2301
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x000B410C File Offset: 0x000B230C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000B413C File Offset: 0x000B233C
		public override bool Equals(object obj)
		{
			GetAchievementsFileRequest getAchievementsFileRequest = obj as GetAchievementsFileRequest;
			return getAchievementsFileRequest != null && this.HasHost == getAchievementsFileRequest.HasHost && (!this.HasHost || this.Host.Equals(getAchievementsFileRequest.Host));
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x0600368D RID: 13965 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x000B4181 File Offset: 0x000B2381
		public static GetAchievementsFileRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAchievementsFileRequest>(bs, 0, -1);
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000B418B File Offset: 0x000B238B
		public void Deserialize(Stream stream)
		{
			GetAchievementsFileRequest.Deserialize(stream, this);
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000B4195 File Offset: 0x000B2395
		public static GetAchievementsFileRequest Deserialize(Stream stream, GetAchievementsFileRequest instance)
		{
			return GetAchievementsFileRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000B41A0 File Offset: 0x000B23A0
		public static GetAchievementsFileRequest DeserializeLengthDelimited(Stream stream)
		{
			GetAchievementsFileRequest getAchievementsFileRequest = new GetAchievementsFileRequest();
			GetAchievementsFileRequest.DeserializeLengthDelimited(stream, getAchievementsFileRequest);
			return getAchievementsFileRequest;
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000B41BC File Offset: 0x000B23BC
		public static GetAchievementsFileRequest DeserializeLengthDelimited(Stream stream, GetAchievementsFileRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAchievementsFileRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x000B41E4 File Offset: 0x000B23E4
		public static GetAchievementsFileRequest Deserialize(Stream stream, GetAchievementsFileRequest instance, long limit)
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
				else if (num == 10)
				{
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
					}
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

		// Token: 0x06003694 RID: 13972 RVA: 0x000B427E File Offset: 0x000B247E
		public void Serialize(Stream stream)
		{
			GetAchievementsFileRequest.Serialize(stream, this);
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000B4287 File Offset: 0x000B2487
		public static void Serialize(Stream stream, GetAchievementsFileRequest instance)
		{
			if (instance.HasHost)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000B42B8 File Offset: 0x000B24B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize = this.Host.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001498 RID: 5272
		public bool HasHost;

		// Token: 0x04001499 RID: 5273
		private ProcessId _Host;
	}
}
