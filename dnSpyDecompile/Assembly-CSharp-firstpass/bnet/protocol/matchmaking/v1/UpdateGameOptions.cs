using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.v2;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003E0 RID: 992
	public class UpdateGameOptions : IProtoBuf
	{
		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06004160 RID: 16736 RVA: 0x000D0092 File Offset: 0x000CE292
		// (set) Token: 0x06004161 RID: 16737 RVA: 0x000D009A File Offset: 0x000CE29A
		public GameHandle GameHandle
		{
			get
			{
				return this._GameHandle;
			}
			set
			{
				this._GameHandle = value;
				this.HasGameHandle = (value != null);
			}
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x000D00AD File Offset: 0x000CE2AD
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06004163 RID: 16739 RVA: 0x000D00B6 File Offset: 0x000CE2B6
		// (set) Token: 0x06004164 RID: 16740 RVA: 0x000D00BE File Offset: 0x000CE2BE
		public List<bnet.protocol.v2.Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x000D00B6 File Offset: 0x000CE2B6
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06004166 RID: 16742 RVA: 0x000D00C7 File Offset: 0x000CE2C7
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x000D00D4 File Offset: 0x000CE2D4
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x000D00E2 File Offset: 0x000CE2E2
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x000D00EF File Offset: 0x000CE2EF
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x0600416A RID: 16746 RVA: 0x000D00F8 File Offset: 0x000CE2F8
		// (set) Token: 0x0600416B RID: 16747 RVA: 0x000D0100 File Offset: 0x000CE300
		public bool ReplaceAttributes
		{
			get
			{
				return this._ReplaceAttributes;
			}
			set
			{
				this._ReplaceAttributes = value;
				this.HasReplaceAttributes = true;
			}
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x000D0110 File Offset: 0x000CE310
		public void SetReplaceAttributes(bool val)
		{
			this.ReplaceAttributes = val;
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x000D011C File Offset: 0x000CE31C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasReplaceAttributes)
			{
				num ^= this.ReplaceAttributes.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x000D01B0 File Offset: 0x000CE3B0
		public override bool Equals(object obj)
		{
			UpdateGameOptions updateGameOptions = obj as UpdateGameOptions;
			if (updateGameOptions == null)
			{
				return false;
			}
			if (this.HasGameHandle != updateGameOptions.HasGameHandle || (this.HasGameHandle && !this.GameHandle.Equals(updateGameOptions.GameHandle)))
			{
				return false;
			}
			if (this.Attribute.Count != updateGameOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(updateGameOptions.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasReplaceAttributes == updateGameOptions.HasReplaceAttributes && (!this.HasReplaceAttributes || this.ReplaceAttributes.Equals(updateGameOptions.ReplaceAttributes));
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x0600416F RID: 16751 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x000D0274 File Offset: 0x000CE474
		public static UpdateGameOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateGameOptions>(bs, 0, -1);
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x000D027E File Offset: 0x000CE47E
		public void Deserialize(Stream stream)
		{
			UpdateGameOptions.Deserialize(stream, this);
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x000D0288 File Offset: 0x000CE488
		public static UpdateGameOptions Deserialize(Stream stream, UpdateGameOptions instance)
		{
			return UpdateGameOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x000D0294 File Offset: 0x000CE494
		public static UpdateGameOptions DeserializeLengthDelimited(Stream stream)
		{
			UpdateGameOptions updateGameOptions = new UpdateGameOptions();
			UpdateGameOptions.DeserializeLengthDelimited(stream, updateGameOptions);
			return updateGameOptions;
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x000D02B0 File Offset: 0x000CE4B0
		public static UpdateGameOptions DeserializeLengthDelimited(Stream stream, UpdateGameOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateGameOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x000D02D8 File Offset: 0x000CE4D8
		public static UpdateGameOptions Deserialize(Stream stream, UpdateGameOptions instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
			}
			instance.ReplaceAttributes = true;
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
						if (num != 24)
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
							instance.ReplaceAttributes = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.GameHandle == null)
				{
					instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x000D03C5 File Offset: 0x000CE5C5
		public void Serialize(Stream stream)
		{
			UpdateGameOptions.Serialize(stream, this);
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x000D03D0 File Offset: 0x000CE5D0
		public static void Serialize(Stream stream, UpdateGameOptions instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasReplaceAttributes)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.ReplaceAttributes);
			}
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x000D0490 File Offset: 0x000CE690
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize = this.GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasReplaceAttributes)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x0400169F RID: 5791
		public bool HasGameHandle;

		// Token: 0x040016A0 RID: 5792
		private GameHandle _GameHandle;

		// Token: 0x040016A1 RID: 5793
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x040016A2 RID: 5794
		public bool HasReplaceAttributes;

		// Token: 0x040016A3 RID: 5795
		private bool _ReplaceAttributes;
	}
}
