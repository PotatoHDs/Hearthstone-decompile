using System;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x02000360 RID: 864
	public class GetAchievementsFileResponse : IProtoBuf
	{
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06003698 RID: 13976 RVA: 0x000B42EB File Offset: 0x000B24EB
		// (set) Token: 0x06003699 RID: 13977 RVA: 0x000B42F3 File Offset: 0x000B24F3
		public ContentHandle ContentHandle
		{
			get
			{
				return this._ContentHandle;
			}
			set
			{
				this._ContentHandle = value;
				this.HasContentHandle = (value != null);
			}
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000B4306 File Offset: 0x000B2506
		public void SetContentHandle(ContentHandle val)
		{
			this.ContentHandle = val;
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000B4310 File Offset: 0x000B2510
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasContentHandle)
			{
				num ^= this.ContentHandle.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x000B4340 File Offset: 0x000B2540
		public override bool Equals(object obj)
		{
			GetAchievementsFileResponse getAchievementsFileResponse = obj as GetAchievementsFileResponse;
			return getAchievementsFileResponse != null && this.HasContentHandle == getAchievementsFileResponse.HasContentHandle && (!this.HasContentHandle || this.ContentHandle.Equals(getAchievementsFileResponse.ContentHandle));
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x0600369D RID: 13981 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000B4385 File Offset: 0x000B2585
		public static GetAchievementsFileResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAchievementsFileResponse>(bs, 0, -1);
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000B438F File Offset: 0x000B258F
		public void Deserialize(Stream stream)
		{
			GetAchievementsFileResponse.Deserialize(stream, this);
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000B4399 File Offset: 0x000B2599
		public static GetAchievementsFileResponse Deserialize(Stream stream, GetAchievementsFileResponse instance)
		{
			return GetAchievementsFileResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000B43A4 File Offset: 0x000B25A4
		public static GetAchievementsFileResponse DeserializeLengthDelimited(Stream stream)
		{
			GetAchievementsFileResponse getAchievementsFileResponse = new GetAchievementsFileResponse();
			GetAchievementsFileResponse.DeserializeLengthDelimited(stream, getAchievementsFileResponse);
			return getAchievementsFileResponse;
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000B43C0 File Offset: 0x000B25C0
		public static GetAchievementsFileResponse DeserializeLengthDelimited(Stream stream, GetAchievementsFileResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAchievementsFileResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000B43E8 File Offset: 0x000B25E8
		public static GetAchievementsFileResponse Deserialize(Stream stream, GetAchievementsFileResponse instance, long limit)
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
					if (instance.ContentHandle == null)
					{
						instance.ContentHandle = ContentHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						ContentHandle.DeserializeLengthDelimited(stream, instance.ContentHandle);
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

		// Token: 0x060036A4 RID: 13988 RVA: 0x000B4482 File Offset: 0x000B2682
		public void Serialize(Stream stream)
		{
			GetAchievementsFileResponse.Serialize(stream, this);
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000B448B File Offset: 0x000B268B
		public static void Serialize(Stream stream, GetAchievementsFileResponse instance)
		{
			if (instance.HasContentHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ContentHandle.GetSerializedSize());
				ContentHandle.Serialize(stream, instance.ContentHandle);
			}
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000B44BC File Offset: 0x000B26BC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasContentHandle)
			{
				num += 1U;
				uint serializedSize = this.ContentHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x0400149A RID: 5274
		public bool HasContentHandle;

		// Token: 0x0400149B RID: 5275
		private ContentHandle _ContentHandle;
	}
}
