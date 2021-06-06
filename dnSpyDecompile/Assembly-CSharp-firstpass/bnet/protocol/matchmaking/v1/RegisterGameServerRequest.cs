using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.v2;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003EF RID: 1007
	public class RegisterGameServerRequest : IProtoBuf
	{
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x060042A6 RID: 17062 RVA: 0x000D36FC File Offset: 0x000D18FC
		// (set) Token: 0x060042A7 RID: 17063 RVA: 0x000D3704 File Offset: 0x000D1904
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

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x060042A8 RID: 17064 RVA: 0x000D36FC File Offset: 0x000D18FC
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x000D370D File Offset: 0x000D190D
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x000D371A File Offset: 0x000D191A
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x000D3728 File Offset: 0x000D1928
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x000D3735 File Offset: 0x000D1935
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x060042AD RID: 17069 RVA: 0x000D373E File Offset: 0x000D193E
		// (set) Token: 0x060042AE RID: 17070 RVA: 0x000D3746 File Offset: 0x000D1946
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x000D3756 File Offset: 0x000D1956
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x000D375F File Offset: 0x000D195F
		// (set) Token: 0x060042B1 RID: 17073 RVA: 0x000D3767 File Offset: 0x000D1967
		public GameServerProperties ServerProperties
		{
			get
			{
				return this._ServerProperties;
			}
			set
			{
				this._ServerProperties = value;
				this.HasServerProperties = (value != null);
			}
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x000D377A File Offset: 0x000D197A
		public void SetServerProperties(GameServerProperties val)
		{
			this.ServerProperties = val;
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x060042B3 RID: 17075 RVA: 0x000D3783 File Offset: 0x000D1983
		// (set) Token: 0x060042B4 RID: 17076 RVA: 0x000D378B File Offset: 0x000D198B
		public uint Priority
		{
			get
			{
				return this._Priority;
			}
			set
			{
				this._Priority = value;
				this.HasPriority = true;
			}
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x000D379B File Offset: 0x000D199B
		public void SetPriority(uint val)
		{
			this.Priority = val;
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x060042B6 RID: 17078 RVA: 0x000D37A4 File Offset: 0x000D19A4
		// (set) Token: 0x060042B7 RID: 17079 RVA: 0x000D37AC File Offset: 0x000D19AC
		public ulong GameServerGuid
		{
			get
			{
				return this._GameServerGuid;
			}
			set
			{
				this._GameServerGuid = value;
				this.HasGameServerGuid = true;
			}
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x000D37BC File Offset: 0x000D19BC
		public void SetGameServerGuid(ulong val)
		{
			this.GameServerGuid = val;
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x000D37C8 File Offset: 0x000D19C8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasServerProperties)
			{
				num ^= this.ServerProperties.GetHashCode();
			}
			if (this.HasPriority)
			{
				num ^= this.Priority.GetHashCode();
			}
			if (this.HasGameServerGuid)
			{
				num ^= this.GameServerGuid.GetHashCode();
			}
			return num;
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x000D388C File Offset: 0x000D1A8C
		public override bool Equals(object obj)
		{
			RegisterGameServerRequest registerGameServerRequest = obj as RegisterGameServerRequest;
			if (registerGameServerRequest == null)
			{
				return false;
			}
			if (this.Attribute.Count != registerGameServerRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(registerGameServerRequest.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasProgram == registerGameServerRequest.HasProgram && (!this.HasProgram || this.Program.Equals(registerGameServerRequest.Program)) && this.HasServerProperties == registerGameServerRequest.HasServerProperties && (!this.HasServerProperties || this.ServerProperties.Equals(registerGameServerRequest.ServerProperties)) && this.HasPriority == registerGameServerRequest.HasPriority && (!this.HasPriority || this.Priority.Equals(registerGameServerRequest.Priority)) && this.HasGameServerGuid == registerGameServerRequest.HasGameServerGuid && (!this.HasGameServerGuid || this.GameServerGuid.Equals(registerGameServerRequest.GameServerGuid));
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x060042BB RID: 17083 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x000D39AC File Offset: 0x000D1BAC
		public static RegisterGameServerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterGameServerRequest>(bs, 0, -1);
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x000D39B6 File Offset: 0x000D1BB6
		public void Deserialize(Stream stream)
		{
			RegisterGameServerRequest.Deserialize(stream, this);
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x000D39C0 File Offset: 0x000D1BC0
		public static RegisterGameServerRequest Deserialize(Stream stream, RegisterGameServerRequest instance)
		{
			return RegisterGameServerRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x000D39CC File Offset: 0x000D1BCC
		public static RegisterGameServerRequest DeserializeLengthDelimited(Stream stream)
		{
			RegisterGameServerRequest registerGameServerRequest = new RegisterGameServerRequest();
			RegisterGameServerRequest.DeserializeLengthDelimited(stream, registerGameServerRequest);
			return registerGameServerRequest;
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x000D39E8 File Offset: 0x000D1BE8
		public static RegisterGameServerRequest DeserializeLengthDelimited(Stream stream, RegisterGameServerRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegisterGameServerRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x000D3A10 File Offset: 0x000D1C10
		public static RegisterGameServerRequest Deserialize(Stream stream, RegisterGameServerRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
			}
			instance.Priority = 0U;
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
					if (num <= 21)
					{
						if (num == 10)
						{
							instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 21)
						{
							instance.Program = binaryReader.ReadUInt32();
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 32)
						{
							instance.Priority = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 41)
						{
							instance.GameServerGuid = binaryReader.ReadUInt64();
							continue;
						}
					}
					else
					{
						if (instance.ServerProperties == null)
						{
							instance.ServerProperties = GameServerProperties.DeserializeLengthDelimited(stream);
							continue;
						}
						GameServerProperties.DeserializeLengthDelimited(stream, instance.ServerProperties);
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

		// Token: 0x060042C2 RID: 17090 RVA: 0x000D3B3D File Offset: 0x000D1D3D
		public void Serialize(Stream stream)
		{
			RegisterGameServerRequest.Serialize(stream, this);
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x000D3B48 File Offset: 0x000D1D48
		public static void Serialize(Stream stream, RegisterGameServerRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasServerProperties)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ServerProperties.GetSerializedSize());
				GameServerProperties.Serialize(stream, instance.ServerProperties);
			}
			if (instance.HasPriority)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.Priority);
			}
			if (instance.HasGameServerGuid)
			{
				stream.WriteByte(41);
				binaryWriter.Write(instance.GameServerGuid);
			}
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x000D3C48 File Offset: 0x000D1E48
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasServerProperties)
			{
				num += 1U;
				uint serializedSize2 = this.ServerProperties.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasPriority)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Priority);
			}
			if (this.HasGameServerGuid)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x040016E4 RID: 5860
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x040016E5 RID: 5861
		public bool HasProgram;

		// Token: 0x040016E6 RID: 5862
		private uint _Program;

		// Token: 0x040016E7 RID: 5863
		public bool HasServerProperties;

		// Token: 0x040016E8 RID: 5864
		private GameServerProperties _ServerProperties;

		// Token: 0x040016E9 RID: 5865
		public bool HasPriority;

		// Token: 0x040016EA RID: 5866
		private uint _Priority;

		// Token: 0x040016EB RID: 5867
		public bool HasGameServerGuid;

		// Token: 0x040016EC RID: 5868
		private ulong _GameServerGuid;
	}
}
