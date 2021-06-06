using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011A8 RID: 4520
	public class ButtonPressed : IProtoBuf
	{
		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x0600C806 RID: 51206 RVA: 0x003C1644 File Offset: 0x003BF844
		// (set) Token: 0x0600C807 RID: 51207 RVA: 0x003C164C File Offset: 0x003BF84C
		public string ButtonName
		{
			get
			{
				return this._ButtonName;
			}
			set
			{
				this._ButtonName = value;
				this.HasButtonName = (value != null);
			}
		}

		// Token: 0x0600C808 RID: 51208 RVA: 0x003C1660 File Offset: 0x003BF860
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasButtonName)
			{
				num ^= this.ButtonName.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C809 RID: 51209 RVA: 0x003C1690 File Offset: 0x003BF890
		public override bool Equals(object obj)
		{
			ButtonPressed buttonPressed = obj as ButtonPressed;
			return buttonPressed != null && this.HasButtonName == buttonPressed.HasButtonName && (!this.HasButtonName || this.ButtonName.Equals(buttonPressed.ButtonName));
		}

		// Token: 0x0600C80A RID: 51210 RVA: 0x003C16D5 File Offset: 0x003BF8D5
		public void Deserialize(Stream stream)
		{
			ButtonPressed.Deserialize(stream, this);
		}

		// Token: 0x0600C80B RID: 51211 RVA: 0x003C16DF File Offset: 0x003BF8DF
		public static ButtonPressed Deserialize(Stream stream, ButtonPressed instance)
		{
			return ButtonPressed.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C80C RID: 51212 RVA: 0x003C16EC File Offset: 0x003BF8EC
		public static ButtonPressed DeserializeLengthDelimited(Stream stream)
		{
			ButtonPressed buttonPressed = new ButtonPressed();
			ButtonPressed.DeserializeLengthDelimited(stream, buttonPressed);
			return buttonPressed;
		}

		// Token: 0x0600C80D RID: 51213 RVA: 0x003C1708 File Offset: 0x003BF908
		public static ButtonPressed DeserializeLengthDelimited(Stream stream, ButtonPressed instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ButtonPressed.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C80E RID: 51214 RVA: 0x003C1730 File Offset: 0x003BF930
		public static ButtonPressed Deserialize(Stream stream, ButtonPressed instance, long limit)
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
					instance.ButtonName = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600C80F RID: 51215 RVA: 0x003C17B0 File Offset: 0x003BF9B0
		public void Serialize(Stream stream)
		{
			ButtonPressed.Serialize(stream, this);
		}

		// Token: 0x0600C810 RID: 51216 RVA: 0x003C17B9 File Offset: 0x003BF9B9
		public static void Serialize(Stream stream, ButtonPressed instance)
		{
			if (instance.HasButtonName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ButtonName));
			}
		}

		// Token: 0x0600C811 RID: 51217 RVA: 0x003C17E4 File Offset: 0x003BF9E4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasButtonName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ButtonName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04009EC7 RID: 40647
		public bool HasButtonName;

		// Token: 0x04009EC8 RID: 40648
		private string _ButtonName;
	}
}
