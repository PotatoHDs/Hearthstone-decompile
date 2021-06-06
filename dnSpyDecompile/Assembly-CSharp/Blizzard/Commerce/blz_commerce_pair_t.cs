using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001231 RID: 4657
	public class blz_commerce_pair_t : IDisposable
	{
		// Token: 0x0600D0E5 RID: 53477 RVA: 0x003E08D1 File Offset: 0x003DEAD1
		internal blz_commerce_pair_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D0E6 RID: 53478 RVA: 0x003E08ED File Offset: 0x003DEAED
		internal static HandleRef getCPtr(blz_commerce_pair_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D0E7 RID: 53479 RVA: 0x003E0904 File Offset: 0x003DEB04
		~blz_commerce_pair_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D0E8 RID: 53480 RVA: 0x003E0934 File Offset: 0x003DEB34
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D0E9 RID: 53481 RVA: 0x003E0944 File Offset: 0x003DEB44
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_pair_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x0600D0EB RID: 53483 RVA: 0x003E09CA File Offset: 0x003DEBCA
		// (set) Token: 0x0600D0EA RID: 53482 RVA: 0x003E09BC File Offset: 0x003DEBBC
		public string key
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_pair_t_key_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_pair_t_key_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x0600D0ED RID: 53485 RVA: 0x003E09E5 File Offset: 0x003DEBE5
		// (set) Token: 0x0600D0EC RID: 53484 RVA: 0x003E09D7 File Offset: 0x003DEBD7
		public IntPtr data
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_pair_t_data_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_pair_t_data_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D0EE RID: 53486 RVA: 0x003E09F2 File Offset: 0x003DEBF2
		public blz_commerce_pair_t() : this(battlenet_commercePINVOKE.new_blz_commerce_pair_t(), true)
		{
		}

		// Token: 0x0400A295 RID: 41621
		private HandleRef swigCPtr;

		// Token: 0x0400A296 RID: 41622
		protected bool swigCMemOwn;
	}
}
