using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x0200122F RID: 4655
	public class blz_commerce_result_t : IDisposable
	{
		// Token: 0x0600D0CF RID: 53455 RVA: 0x003E062B File Offset: 0x003DE82B
		internal blz_commerce_result_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D0D0 RID: 53456 RVA: 0x003E0647 File Offset: 0x003DE847
		internal static HandleRef getCPtr(blz_commerce_result_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D0D1 RID: 53457 RVA: 0x003E0660 File Offset: 0x003DE860
		~blz_commerce_result_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D0D2 RID: 53458 RVA: 0x003E0690 File Offset: 0x003DE890
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D0D3 RID: 53459 RVA: 0x003E06A0 File Offset: 0x003DE8A0
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_result_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x0600D0D5 RID: 53461 RVA: 0x003E0726 File Offset: 0x003DE926
		// (set) Token: 0x0600D0D4 RID: 53460 RVA: 0x003E0718 File Offset: 0x003DE918
		public blz_commerce_result_state_t state
		{
			get
			{
				return (blz_commerce_result_state_t)battlenet_commercePINVOKE.blz_commerce_result_t_state_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_result_t_state_set(this.swigCPtr, (int)value);
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x0600D0D7 RID: 53463 RVA: 0x003E0741 File Offset: 0x003DE941
		// (set) Token: 0x0600D0D6 RID: 53462 RVA: 0x003E0733 File Offset: 0x003DE933
		public IntPtr data
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_result_t_data_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_result_t_data_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x0600D0D9 RID: 53465 RVA: 0x003E075C File Offset: 0x003DE95C
		// (set) Token: 0x0600D0D8 RID: 53464 RVA: 0x003E074E File Offset: 0x003DE94E
		public int reference_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_result_t_reference_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_result_t_reference_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D0DA RID: 53466 RVA: 0x003E0769 File Offset: 0x003DE969
		public blz_commerce_result_t() : this(battlenet_commercePINVOKE.new_blz_commerce_result_t(), true)
		{
		}

		// Token: 0x0400A291 RID: 41617
		private HandleRef swigCPtr;

		// Token: 0x0400A292 RID: 41618
		protected bool swigCMemOwn;
	}
}
