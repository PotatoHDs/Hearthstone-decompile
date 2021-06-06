using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000165 RID: 357
	public class GameSaveDataValue : IProtoBuf
	{
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x00055A58 File Offset: 0x00053C58
		// (set) Token: 0x0600187A RID: 6266 RVA: 0x00055A60 File Offset: 0x00053C60
		public List<long> IntValue
		{
			get
			{
				return this._IntValue;
			}
			set
			{
				this._IntValue = value;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x00055A69 File Offset: 0x00053C69
		// (set) Token: 0x0600187C RID: 6268 RVA: 0x00055A71 File Offset: 0x00053C71
		public List<double> FloatValue
		{
			get
			{
				return this._FloatValue;
			}
			set
			{
				this._FloatValue = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x00055A7A File Offset: 0x00053C7A
		// (set) Token: 0x0600187E RID: 6270 RVA: 0x00055A82 File Offset: 0x00053C82
		public List<string> StringValue
		{
			get
			{
				return this._StringValue;
			}
			set
			{
				this._StringValue = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x00055A8B File Offset: 0x00053C8B
		// (set) Token: 0x06001880 RID: 6272 RVA: 0x00055A93 File Offset: 0x00053C93
		public List<long> MapKeys
		{
			get
			{
				return this._MapKeys;
			}
			set
			{
				this._MapKeys = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x00055A9C File Offset: 0x00053C9C
		// (set) Token: 0x06001882 RID: 6274 RVA: 0x00055AA4 File Offset: 0x00053CA4
		public List<GameSaveDataValue> MapValues
		{
			get
			{
				return this._MapValues;
			}
			set
			{
				this._MapValues = value;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x00055AAD File Offset: 0x00053CAD
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x00055AB5 File Offset: 0x00053CB5
		public long CreateDateUnixTimestamp
		{
			get
			{
				return this._CreateDateUnixTimestamp;
			}
			set
			{
				this._CreateDateUnixTimestamp = value;
				this.HasCreateDateUnixTimestamp = true;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x00055AC5 File Offset: 0x00053CC5
		// (set) Token: 0x06001886 RID: 6278 RVA: 0x00055ACD File Offset: 0x00053CCD
		public long ModifyDateUnixTimestamp
		{
			get
			{
				return this._ModifyDateUnixTimestamp;
			}
			set
			{
				this._ModifyDateUnixTimestamp = value;
				this.HasModifyDateUnixTimestamp = true;
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00055AE0 File Offset: 0x00053CE0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (long num2 in this.IntValue)
			{
				num ^= num2.GetHashCode();
			}
			foreach (double num3 in this.FloatValue)
			{
				num ^= num3.GetHashCode();
			}
			foreach (string text in this.StringValue)
			{
				num ^= text.GetHashCode();
			}
			foreach (long num4 in this.MapKeys)
			{
				num ^= num4.GetHashCode();
			}
			foreach (GameSaveDataValue gameSaveDataValue in this.MapValues)
			{
				num ^= gameSaveDataValue.GetHashCode();
			}
			if (this.HasCreateDateUnixTimestamp)
			{
				num ^= this.CreateDateUnixTimestamp.GetHashCode();
			}
			if (this.HasModifyDateUnixTimestamp)
			{
				num ^= this.ModifyDateUnixTimestamp.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x00055C94 File Offset: 0x00053E94
		public override bool Equals(object obj)
		{
			GameSaveDataValue gameSaveDataValue = obj as GameSaveDataValue;
			if (gameSaveDataValue == null)
			{
				return false;
			}
			if (this.IntValue.Count != gameSaveDataValue.IntValue.Count)
			{
				return false;
			}
			for (int i = 0; i < this.IntValue.Count; i++)
			{
				if (!this.IntValue[i].Equals(gameSaveDataValue.IntValue[i]))
				{
					return false;
				}
			}
			if (this.FloatValue.Count != gameSaveDataValue.FloatValue.Count)
			{
				return false;
			}
			for (int j = 0; j < this.FloatValue.Count; j++)
			{
				if (!this.FloatValue[j].Equals(gameSaveDataValue.FloatValue[j]))
				{
					return false;
				}
			}
			if (this.StringValue.Count != gameSaveDataValue.StringValue.Count)
			{
				return false;
			}
			for (int k = 0; k < this.StringValue.Count; k++)
			{
				if (!this.StringValue[k].Equals(gameSaveDataValue.StringValue[k]))
				{
					return false;
				}
			}
			if (this.MapKeys.Count != gameSaveDataValue.MapKeys.Count)
			{
				return false;
			}
			for (int l = 0; l < this.MapKeys.Count; l++)
			{
				if (!this.MapKeys[l].Equals(gameSaveDataValue.MapKeys[l]))
				{
					return false;
				}
			}
			if (this.MapValues.Count != gameSaveDataValue.MapValues.Count)
			{
				return false;
			}
			for (int m = 0; m < this.MapValues.Count; m++)
			{
				if (!this.MapValues[m].Equals(gameSaveDataValue.MapValues[m]))
				{
					return false;
				}
			}
			return this.HasCreateDateUnixTimestamp == gameSaveDataValue.HasCreateDateUnixTimestamp && (!this.HasCreateDateUnixTimestamp || this.CreateDateUnixTimestamp.Equals(gameSaveDataValue.CreateDateUnixTimestamp)) && this.HasModifyDateUnixTimestamp == gameSaveDataValue.HasModifyDateUnixTimestamp && (!this.HasModifyDateUnixTimestamp || this.ModifyDateUnixTimestamp.Equals(gameSaveDataValue.ModifyDateUnixTimestamp));
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x00055EBB File Offset: 0x000540BB
		public void Deserialize(Stream stream)
		{
			GameSaveDataValue.Deserialize(stream, this);
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00055EC5 File Offset: 0x000540C5
		public static GameSaveDataValue Deserialize(Stream stream, GameSaveDataValue instance)
		{
			return GameSaveDataValue.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00055ED0 File Offset: 0x000540D0
		public static GameSaveDataValue DeserializeLengthDelimited(Stream stream)
		{
			GameSaveDataValue gameSaveDataValue = new GameSaveDataValue();
			GameSaveDataValue.DeserializeLengthDelimited(stream, gameSaveDataValue);
			return gameSaveDataValue;
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00055EEC File Offset: 0x000540EC
		public static GameSaveDataValue DeserializeLengthDelimited(Stream stream, GameSaveDataValue instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameSaveDataValue.Deserialize(stream, instance, num);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00055F14 File Offset: 0x00054114
		public static GameSaveDataValue Deserialize(Stream stream, GameSaveDataValue instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.IntValue == null)
			{
				instance.IntValue = new List<long>();
			}
			if (instance.FloatValue == null)
			{
				instance.FloatValue = new List<double>();
			}
			if (instance.StringValue == null)
			{
				instance.StringValue = new List<string>();
			}
			if (instance.MapKeys == null)
			{
				instance.MapKeys = new List<long>();
			}
			if (instance.MapValues == null)
			{
				instance.MapValues = new List<GameSaveDataValue>();
			}
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
					if (num <= 17)
					{
						if (num == 8)
						{
							instance.IntValue.Add((long)ProtocolParser.ReadUInt64(stream));
							continue;
						}
						if (num == 17)
						{
							instance.FloatValue.Add(binaryReader.ReadDouble());
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.StringValue.Add(ProtocolParser.ReadString(stream));
							continue;
						}
						if (num == 80)
						{
							instance.MapKeys.Add((long)ProtocolParser.ReadUInt64(stream));
							continue;
						}
						if (num == 90)
						{
							instance.MapValues.Add(GameSaveDataValue.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 1000U)
					{
						if (field != 1001U)
						{
							ProtocolParser.SkipKey(stream, key);
						}
						else if (key.WireType == Wire.Varint)
						{
							instance.ModifyDateUnixTimestamp = (long)ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (key.WireType == Wire.Varint)
					{
						instance.CreateDateUnixTimestamp = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x000560C6 File Offset: 0x000542C6
		public void Serialize(Stream stream)
		{
			GameSaveDataValue.Serialize(stream, this);
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x000560D0 File Offset: 0x000542D0
		public static void Serialize(Stream stream, GameSaveDataValue instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.IntValue.Count > 0)
			{
				foreach (long val in instance.IntValue)
				{
					stream.WriteByte(8);
					ProtocolParser.WriteUInt64(stream, (ulong)val);
				}
			}
			if (instance.FloatValue.Count > 0)
			{
				foreach (double value in instance.FloatValue)
				{
					stream.WriteByte(17);
					binaryWriter.Write(value);
				}
			}
			if (instance.StringValue.Count > 0)
			{
				foreach (string s in instance.StringValue)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
			if (instance.MapKeys.Count > 0)
			{
				foreach (long val2 in instance.MapKeys)
				{
					stream.WriteByte(80);
					ProtocolParser.WriteUInt64(stream, (ulong)val2);
				}
			}
			if (instance.MapValues.Count > 0)
			{
				foreach (GameSaveDataValue gameSaveDataValue in instance.MapValues)
				{
					stream.WriteByte(90);
					ProtocolParser.WriteUInt32(stream, gameSaveDataValue.GetSerializedSize());
					GameSaveDataValue.Serialize(stream, gameSaveDataValue);
				}
			}
			if (instance.HasCreateDateUnixTimestamp)
			{
				stream.WriteByte(192);
				stream.WriteByte(62);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CreateDateUnixTimestamp);
			}
			if (instance.HasModifyDateUnixTimestamp)
			{
				stream.WriteByte(200);
				stream.WriteByte(62);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ModifyDateUnixTimestamp);
			}
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00056310 File Offset: 0x00054510
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.IntValue.Count > 0)
			{
				foreach (long val in this.IntValue)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)val);
				}
			}
			if (this.FloatValue.Count > 0)
			{
				foreach (double num2 in this.FloatValue)
				{
					num += 1U;
					num += 8U;
				}
			}
			if (this.StringValue.Count > 0)
			{
				foreach (string s in this.StringValue)
				{
					num += 1U;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
			}
			if (this.MapKeys.Count > 0)
			{
				foreach (long val2 in this.MapKeys)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)val2);
				}
			}
			if (this.MapValues.Count > 0)
			{
				foreach (GameSaveDataValue gameSaveDataValue in this.MapValues)
				{
					num += 1U;
					uint serializedSize = gameSaveDataValue.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasCreateDateUnixTimestamp)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.CreateDateUnixTimestamp);
			}
			if (this.HasModifyDateUnixTimestamp)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ModifyDateUnixTimestamp);
			}
			return num;
		}

		// Token: 0x040007D4 RID: 2004
		private List<long> _IntValue = new List<long>();

		// Token: 0x040007D5 RID: 2005
		private List<double> _FloatValue = new List<double>();

		// Token: 0x040007D6 RID: 2006
		private List<string> _StringValue = new List<string>();

		// Token: 0x040007D7 RID: 2007
		private List<long> _MapKeys = new List<long>();

		// Token: 0x040007D8 RID: 2008
		private List<GameSaveDataValue> _MapValues = new List<GameSaveDataValue>();

		// Token: 0x040007D9 RID: 2009
		public bool HasCreateDateUnixTimestamp;

		// Token: 0x040007DA RID: 2010
		private long _CreateDateUnixTimestamp;

		// Token: 0x040007DB RID: 2011
		public bool HasModifyDateUnixTimestamp;

		// Token: 0x040007DC RID: 2012
		private long _ModifyDateUnixTimestamp;
	}
}
