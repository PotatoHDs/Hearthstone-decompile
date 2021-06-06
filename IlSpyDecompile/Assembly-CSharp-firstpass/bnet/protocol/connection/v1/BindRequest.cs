using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.connection.v1
{
	public class BindRequest : IProtoBuf
	{
		private List<uint> _DeprecatedImportedServiceHash = new List<uint>();

		private List<BoundService> _DeprecatedExportedService = new List<BoundService>();

		private List<BoundService> _ExportedService = new List<BoundService>();

		private List<BoundService> _ImportedService = new List<BoundService>();

		public List<uint> DeprecatedImportedServiceHash
		{
			get
			{
				return _DeprecatedImportedServiceHash;
			}
			set
			{
				_DeprecatedImportedServiceHash = value;
			}
		}

		public List<uint> DeprecatedImportedServiceHashList => _DeprecatedImportedServiceHash;

		public int DeprecatedImportedServiceHashCount => _DeprecatedImportedServiceHash.Count;

		public List<BoundService> DeprecatedExportedService
		{
			get
			{
				return _DeprecatedExportedService;
			}
			set
			{
				_DeprecatedExportedService = value;
			}
		}

		public List<BoundService> DeprecatedExportedServiceList => _DeprecatedExportedService;

		public int DeprecatedExportedServiceCount => _DeprecatedExportedService.Count;

		public List<BoundService> ExportedService
		{
			get
			{
				return _ExportedService;
			}
			set
			{
				_ExportedService = value;
			}
		}

		public List<BoundService> ExportedServiceList => _ExportedService;

		public int ExportedServiceCount => _ExportedService.Count;

		public List<BoundService> ImportedService
		{
			get
			{
				return _ImportedService;
			}
			set
			{
				_ImportedService = value;
			}
		}

		public List<BoundService> ImportedServiceList => _ImportedService;

		public int ImportedServiceCount => _ImportedService.Count;

		public bool IsInitialized => true;

		public void AddDeprecatedImportedServiceHash(uint val)
		{
			_DeprecatedImportedServiceHash.Add(val);
		}

		public void ClearDeprecatedImportedServiceHash()
		{
			_DeprecatedImportedServiceHash.Clear();
		}

		public void SetDeprecatedImportedServiceHash(List<uint> val)
		{
			DeprecatedImportedServiceHash = val;
		}

		public void AddDeprecatedExportedService(BoundService val)
		{
			_DeprecatedExportedService.Add(val);
		}

		public void ClearDeprecatedExportedService()
		{
			_DeprecatedExportedService.Clear();
		}

		public void SetDeprecatedExportedService(List<BoundService> val)
		{
			DeprecatedExportedService = val;
		}

		public void AddExportedService(BoundService val)
		{
			_ExportedService.Add(val);
		}

		public void ClearExportedService()
		{
			_ExportedService.Clear();
		}

		public void SetExportedService(List<BoundService> val)
		{
			ExportedService = val;
		}

		public void AddImportedService(BoundService val)
		{
			_ImportedService.Add(val);
		}

		public void ClearImportedService()
		{
			_ImportedService.Clear();
		}

		public void SetImportedService(List<BoundService> val)
		{
			ImportedService = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (uint item in DeprecatedImportedServiceHash)
			{
				num ^= item.GetHashCode();
			}
			foreach (BoundService item2 in DeprecatedExportedService)
			{
				num ^= item2.GetHashCode();
			}
			foreach (BoundService item3 in ExportedService)
			{
				num ^= item3.GetHashCode();
			}
			foreach (BoundService item4 in ImportedService)
			{
				num ^= item4.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BindRequest bindRequest = obj as BindRequest;
			if (bindRequest == null)
			{
				return false;
			}
			if (DeprecatedImportedServiceHash.Count != bindRequest.DeprecatedImportedServiceHash.Count)
			{
				return false;
			}
			for (int i = 0; i < DeprecatedImportedServiceHash.Count; i++)
			{
				if (!DeprecatedImportedServiceHash[i].Equals(bindRequest.DeprecatedImportedServiceHash[i]))
				{
					return false;
				}
			}
			if (DeprecatedExportedService.Count != bindRequest.DeprecatedExportedService.Count)
			{
				return false;
			}
			for (int j = 0; j < DeprecatedExportedService.Count; j++)
			{
				if (!DeprecatedExportedService[j].Equals(bindRequest.DeprecatedExportedService[j]))
				{
					return false;
				}
			}
			if (ExportedService.Count != bindRequest.ExportedService.Count)
			{
				return false;
			}
			for (int k = 0; k < ExportedService.Count; k++)
			{
				if (!ExportedService[k].Equals(bindRequest.ExportedService[k]))
				{
					return false;
				}
			}
			if (ImportedService.Count != bindRequest.ImportedService.Count)
			{
				return false;
			}
			for (int l = 0; l < ImportedService.Count; l++)
			{
				if (!ImportedService[l].Equals(bindRequest.ImportedService[l]))
				{
					return false;
				}
			}
			return true;
		}

		public static BindRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BindRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BindRequest Deserialize(Stream stream, BindRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BindRequest DeserializeLengthDelimited(Stream stream)
		{
			BindRequest bindRequest = new BindRequest();
			DeserializeLengthDelimited(stream, bindRequest);
			return bindRequest;
		}

		public static BindRequest DeserializeLengthDelimited(Stream stream, BindRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BindRequest Deserialize(Stream stream, BindRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.DeprecatedImportedServiceHash == null)
			{
				instance.DeprecatedImportedServiceHash = new List<uint>();
			}
			if (instance.DeprecatedExportedService == null)
			{
				instance.DeprecatedExportedService = new List<BoundService>();
			}
			if (instance.ExportedService == null)
			{
				instance.ExportedService = new List<BoundService>();
			}
			if (instance.ImportedService == null)
			{
				instance.ImportedService = new List<BoundService>();
			}
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
				{
					long num2 = ProtocolParser.ReadUInt32(stream);
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.DeprecatedImportedServiceHash.Add(binaryReader.ReadUInt32());
					}
					if (stream.Position == num2)
					{
						continue;
					}
					throw new ProtocolBufferException("Read too many bytes in packed data");
				}
				case 18:
					instance.DeprecatedExportedService.Add(BoundService.DeserializeLengthDelimited(stream));
					continue;
				case 26:
					instance.ExportedService.Add(BoundService.DeserializeLengthDelimited(stream));
					continue;
				case 34:
					instance.ImportedService.Add(BoundService.DeserializeLengthDelimited(stream));
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, BindRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.DeprecatedImportedServiceHash.Count > 0)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, (uint)(4 * instance.DeprecatedImportedServiceHash.Count));
				foreach (uint item in instance.DeprecatedImportedServiceHash)
				{
					binaryWriter.Write(item);
				}
			}
			if (instance.DeprecatedExportedService.Count > 0)
			{
				foreach (BoundService item2 in instance.DeprecatedExportedService)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
					BoundService.Serialize(stream, item2);
				}
			}
			if (instance.ExportedService.Count > 0)
			{
				foreach (BoundService item3 in instance.ExportedService)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, item3.GetSerializedSize());
					BoundService.Serialize(stream, item3);
				}
			}
			if (instance.ImportedService.Count <= 0)
			{
				return;
			}
			foreach (BoundService item4 in instance.ImportedService)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, item4.GetSerializedSize());
				BoundService.Serialize(stream, item4);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (DeprecatedImportedServiceHash.Count > 0)
			{
				num++;
				uint num2 = num;
				foreach (uint item in DeprecatedImportedServiceHash)
				{
					_ = item;
					num += 4;
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (DeprecatedExportedService.Count > 0)
			{
				foreach (BoundService item2 in DeprecatedExportedService)
				{
					num++;
					uint serializedSize = item2.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (ExportedService.Count > 0)
			{
				foreach (BoundService item3 in ExportedService)
				{
					num++;
					uint serializedSize2 = item3.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (ImportedService.Count > 0)
			{
				foreach (BoundService item4 in ImportedService)
				{
					num++;
					uint serializedSize3 = item4.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
				return num;
			}
			return num;
		}
	}
}
