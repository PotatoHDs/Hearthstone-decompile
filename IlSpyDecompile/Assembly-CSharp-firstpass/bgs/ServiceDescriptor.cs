using System;

namespace bgs
{
	public class ServiceDescriptor
	{
		private string name;

		private uint id;

		private uint hash;

		protected MethodDescriptor[] Methods;

		private const uint INVALID_SERVICE_ID = 255u;

		public string Name => name;

		public uint Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public uint Hash => hash;

		public bool Imported { get; set; }

		public bool Exported { get; set; }

		public ServiceDescriptor(string n)
		{
			name = n;
			id = 255u;
			hash = Compute32.Hash(name);
			Console.WriteLine("service: " + n + ", hash: " + hash);
		}

		public void RegisterMethodListener(uint method_id, RPCContextDelegate callback)
		{
			if (Methods != null && method_id != 0 && method_id <= Methods.Length)
			{
				Methods[method_id].RegisterListener(callback);
			}
		}

		public string GetMethodName(uint method_id)
		{
			if (Methods != null && method_id != 0 && method_id <= Methods.Length)
			{
				return Methods[method_id].Name;
			}
			return "";
		}

		public int GetMethodCount()
		{
			if (Methods == null)
			{
				return 0;
			}
			return Methods.Length;
		}

		public MethodDescriptor GetMethodDescriptor(uint method_id)
		{
			if (Methods == null)
			{
				return null;
			}
			if (method_id >= Methods.Length)
			{
				return null;
			}
			return Methods[method_id];
		}

		public MethodDescriptor.ParseMethod GetParser(uint method_id)
		{
			if (Methods == null)
			{
				BattleNet.Log.LogWarning("ServiceDescriptor unable to get parser, no methods have been set.");
				return null;
			}
			if (method_id == 0)
			{
				BattleNet.Log.LogWarning("ServiceDescriptor {0} unable to get parser, invalid index={1}/{2}", Hash, method_id, Methods.Length);
				return null;
			}
			if (method_id >= Methods.Length)
			{
				BattleNet.Log.LogWarning("ServiceDescriptor {0} unable to get parser, invalid index={1}/{2}", Hash, method_id, Methods.Length);
				return null;
			}
			if (Methods[method_id].Parser == null)
			{
				BattleNet.Log.LogWarning("ServiceDescriptor {0} unable to get parser, invalid index={1}/{2}", Hash, method_id, Methods.Length);
				return null;
			}
			return Methods[method_id].Parser;
		}

		public bool HasMethodListener(uint method_id)
		{
			if (Methods != null && method_id != 0 && method_id < Methods.Length)
			{
				return Methods[method_id].HasListener();
			}
			return false;
		}

		public void NotifyMethodListener(RPCContext context)
		{
			if (Methods != null && context.Header.MethodId != 0 && context.Header.MethodId <= Methods.Length)
			{
				Methods[context.Header.MethodId].NotifyListener(context);
			}
		}
	}
}
