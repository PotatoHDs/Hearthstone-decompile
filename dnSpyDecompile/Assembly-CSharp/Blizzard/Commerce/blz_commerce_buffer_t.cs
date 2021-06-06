using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001247 RID: 4679
	public class blz_commerce_buffer_t : IDisposable
	{
		// Token: 0x0600D199 RID: 53657 RVA: 0x003E1E54 File Offset: 0x003E0054
		internal blz_commerce_buffer_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D19A RID: 53658 RVA: 0x003E1E70 File Offset: 0x003E0070
		internal static HandleRef getCPtr(blz_commerce_buffer_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D19B RID: 53659 RVA: 0x003E1E88 File Offset: 0x003E0088
		~blz_commerce_buffer_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D19C RID: 53660 RVA: 0x003E1EB8 File Offset: 0x003E00B8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D19D RID: 53661 RVA: 0x003E1EC8 File Offset: 0x003E00C8
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_buffer_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x0600D19E RID: 53662 RVA: 0x003E1F40 File Offset: 0x003E0140
		public byte[] bytes
		{
			get
			{
				int len = this.len;
				IntPtr source = battlenet_commercePINVOKE.blz_commerce_buffer_t_bytes_get(this.swigCPtr);
				byte[] array = new byte[len];
				Marshal.Copy(source, array, 0, len);
				return array;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x0600D1A0 RID: 53664 RVA: 0x003E1F7D File Offset: 0x003E017D
		// (set) Token: 0x0600D19F RID: 53663 RVA: 0x003E1F6F File Offset: 0x003E016F
		public int len
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_buffer_t_len_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_buffer_t_len_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D1A1 RID: 53665 RVA: 0x003E1F8A File Offset: 0x003E018A
		public blz_commerce_buffer_t() : this(battlenet_commercePINVOKE.new_blz_commerce_buffer_t(), true)
		{
		}

		// Token: 0x0400A2EC RID: 41708
		private HandleRef swigCPtr;

		// Token: 0x0400A2ED RID: 41709
		protected bool swigCMemOwn;
	}
}
