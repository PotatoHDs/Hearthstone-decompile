using System;
using System.Diagnostics;
using Blizzard.T5.AssetManager;
using Hearthstone.UI.Internal;
using Hearthstone.UI.Logging;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FF4 RID: 4084
	[Serializable]
	public class Override
	{
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x0600B170 RID: 45424 RVA: 0x0036BBA9 File Offset: 0x00369DA9
		public double ValueDouble
		{
			get
			{
				return this.m_valueDouble;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x0600B171 RID: 45425 RVA: 0x0036BBB1 File Offset: 0x00369DB1
		public NestedReference NestedReference
		{
			get
			{
				return this.m_nestedReference;
			}
		}

		// Token: 0x0600B172 RID: 45426 RVA: 0x0036BBBC File Offset: 0x00369DBC
		public void Apply(Override.ApplyCallbackDelegate callback, object userData = null, bool loadSynchronously = false)
		{
			this.Abort();
			this.m_callback = callback;
			this.m_callbackUserData = userData;
			UnityEngine.Object @object = null;
			try
			{
				NestedReferenceTargetInfo nestedReferenceTargetInfo;
				if (!this.m_nestedReference.Resolve(out nestedReferenceTargetInfo))
				{
					this.CompleteAsyncOperation(AsyncOperationResult.Failure);
				}
				else
				{
					switch (this.m_nestedReference.TargetType)
					{
					case NestedReference.TargetTypes.String:
						this.SetValueOnTarget(nestedReferenceTargetInfo, this.m_valueString, false);
						break;
					case NestedReference.TargetTypes.Color:
						this.SetValueOnTarget(nestedReferenceTargetInfo, this.m_valueColor, false);
						break;
					case NestedReference.TargetTypes.Float:
						this.SetValueOnTarget(nestedReferenceTargetInfo, (float)this.m_valueDouble, false);
						break;
					case NestedReference.TargetTypes.Bool:
						this.SetValueOnTarget(nestedReferenceTargetInfo, this.m_valueBool, false);
						break;
					case NestedReference.TargetTypes.Int:
						this.SetValueOnTarget(nestedReferenceTargetInfo, (int)this.m_valueLong, false);
						break;
					case NestedReference.TargetTypes.Material:
					case NestedReference.TargetTypes.Texture:
					case NestedReference.TargetTypes.Mesh:
						this.LoadAssetAndAssignToProperty(nestedReferenceTargetInfo, this.m_asyncOperationId, loadSynchronously);
						break;
					case NestedReference.TargetTypes.Double:
						this.SetValueOnTarget(nestedReferenceTargetInfo, this.m_valueDouble, false);
						break;
					case NestedReference.TargetTypes.Long:
						this.SetValueOnTarget(nestedReferenceTargetInfo, this.m_valueLong, false);
						break;
					case NestedReference.TargetTypes.Vector2:
					case NestedReference.TargetTypes.Vector3:
					case NestedReference.TargetTypes.Vector4:
						this.SetValueOnTarget(nestedReferenceTargetInfo, this.m_valueVector, true);
						break;
					case NestedReference.TargetTypes.Enum:
						this.SetValueOnTarget(nestedReferenceTargetInfo, Enum.Parse(nestedReferenceTargetInfo.Property.PropertyType, this.m_valueString), false);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogErrorFormat("Error when applying override '{0}' to '{1}': {2}", new object[]
				{
					@object,
					this.m_valueString,
					ex
				});
			}
		}

		// Token: 0x0600B173 RID: 45427 RVA: 0x0036BD70 File Offset: 0x00369F70
		public void ApplyWithValue(Override.ApplyCallbackDelegate callback, object value, object userData = null, bool implicitConversion = false)
		{
			this.Abort();
			this.m_callback = callback;
			this.m_callbackUserData = userData;
			UnityEngine.Object @object = null;
			try
			{
				NestedReferenceTargetInfo targetInfo;
				if (!this.m_nestedReference.Resolve(out targetInfo))
				{
					this.CompleteAsyncOperation(AsyncOperationResult.Failure);
				}
				else
				{
					this.SetValueOnTarget(targetInfo, value, implicitConversion);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogErrorFormat("Error when applying override '{0}' to '{1}': {2}", new object[]
				{
					@object,
					value,
					ex
				});
			}
		}

		// Token: 0x0600B174 RID: 45428 RVA: 0x0036BDE8 File Offset: 0x00369FE8
		public void Abort()
		{
			this.CompleteAsyncOperation(AsyncOperationResult.Aborted);
			this.m_asyncOperationId++;
		}

		// Token: 0x0600B175 RID: 45429 RVA: 0x0036BE00 File Offset: 0x0036A000
		public void RegisterReadyListener(Action<object> action, object payload = null)
		{
			this.NestedReference.RegisterReadyListener(action, payload);
		}

		// Token: 0x0600B176 RID: 45430 RVA: 0x0036BE20 File Offset: 0x0036A020
		public void RemoveReadyOrInactiveListener(Action<object> action)
		{
			this.NestedReference.RemoveReadyOrInactiveListener();
		}

		// Token: 0x0600B177 RID: 45431 RVA: 0x0036BE3B File Offset: 0x0036A03B
		public bool Resolve(out GameObject gameObject)
		{
			return this.m_nestedReference.Resolve(out gameObject);
		}

		// Token: 0x0600B178 RID: 45432 RVA: 0x0036BE4C File Offset: 0x0036A04C
		private void LoadAssetAndAssignToProperty(NestedReferenceTargetInfo property, int asyncOperationId, bool loadSynchronously)
		{
			this.m_propertyPendingAssignment = property;
			AssetReference assetReference = AssetReference.CreateFromAssetString(this.m_valueAsset);
			if (assetReference == null)
			{
				this.SetValueOnTarget(property, null, false);
				return;
			}
			if (loadSynchronously)
			{
				NestedReference.TargetTypes targetType = this.m_nestedReference.TargetType;
				if (targetType == NestedReference.TargetTypes.Material)
				{
					this.HandleAssetLoaded<Material>(assetReference, AssetLoader.Get().LoadAsset<Material>(assetReference, AssetLoadingOptions.None), asyncOperationId);
					return;
				}
				if (targetType == NestedReference.TargetTypes.Texture)
				{
					this.HandleAssetLoaded<Texture>(assetReference, AssetLoader.Get().LoadAsset<Texture>(assetReference, AssetLoadingOptions.None), asyncOperationId);
					return;
				}
				if (targetType != NestedReference.TargetTypes.Mesh)
				{
					return;
				}
				this.HandleMeshLoaded(assetReference, AssetLoader.Get().LoadMesh(assetReference, false, false), asyncOperationId);
				return;
			}
			else
			{
				NestedReference.TargetTypes targetType = this.m_nestedReference.TargetType;
				if (targetType == NestedReference.TargetTypes.Material)
				{
					AssetLoader.Get().LoadAsset<Material>(assetReference, new AssetHandleCallback<Material>(this.HandleAssetLoaded<Material>), asyncOperationId, AssetLoadingOptions.None);
					return;
				}
				if (targetType == NestedReference.TargetTypes.Texture)
				{
					AssetLoader.Get().LoadAsset<Texture>(assetReference, new AssetHandleCallback<Texture>(this.HandleAssetLoaded<Texture>), asyncOperationId, AssetLoadingOptions.None);
					return;
				}
				if (targetType != NestedReference.TargetTypes.Mesh)
				{
					return;
				}
				AssetLoader.Get().LoadMesh(assetReference, new AssetLoader.ObjectCallback(this.HandleMeshLoaded), asyncOperationId, false, false);
				return;
			}
		}

		// Token: 0x0600B179 RID: 45433 RVA: 0x0036BF64 File Offset: 0x0036A164
		private void HandleAssetLoaded<T>(AssetReference assetRef, AssetHandle<T> assetHandle, object asyncOperationId) where T : UnityEngine.Object
		{
			if (this.m_asyncOperationId != (int)asyncOperationId)
			{
				AssetHandle.SafeDispose<T>(ref assetHandle);
				return;
			}
			HearthstoneServices.Get<WidgetRunner>().RegisterAssetHandle(this.m_propertyPendingAssignment.Target, assetHandle);
			this.SetValueOnTarget(this.m_propertyPendingAssignment, (assetHandle != null) ? assetHandle.Asset : default(T), false);
		}

		// Token: 0x0600B17A RID: 45434 RVA: 0x0036BFC3 File Offset: 0x0036A1C3
		private void HandleMeshLoaded(AssetReference assetReference, UnityEngine.Object obj, object asyncOperationId)
		{
			if (this.m_asyncOperationId == (int)asyncOperationId)
			{
				this.SetValueOnTarget(this.m_propertyPendingAssignment, obj, false);
			}
		}

		// Token: 0x0600B17B RID: 45435 RVA: 0x0036BFE4 File Offset: 0x0036A1E4
		private void SetValueOnTarget(NestedReferenceTargetInfo targetInfo, object value, bool implicitConversion = false)
		{
			AsyncOperationResult result = AsyncOperationResult.Failure;
			try
			{
				IDynamicPropertyResolver dynamicPropertyResolver;
				if (targetInfo.Property != null)
				{
					if (implicitConversion)
					{
						IConvertible convertible = value as IConvertible;
						if (convertible != null)
						{
							value = convertible.ToType(targetInfo.Property.PropertyType, null);
						}
						else if (value is Vector4)
						{
							if (targetInfo.Property.PropertyType == typeof(Vector2))
							{
								value = new Vector2(((Vector4)value).x, ((Vector4)value).y);
							}
							else if (targetInfo.Property.PropertyType == typeof(Vector3))
							{
								value = new Vector3(((Vector4)value).x, ((Vector4)value).y, ((Vector4)value).z);
							}
						}
					}
					targetInfo.Property.SetValue(targetInfo.Target, value, null);
					result = AsyncOperationResult.Success;
				}
				else if ((dynamicPropertyResolver = DynamicPropertyResolvers.TryGetResolver(targetInfo.Target)) != null)
				{
					if (implicitConversion)
					{
						IConvertible convertible2 = value as IConvertible;
						if (convertible2 != null)
						{
							foreach (DynamicPropertyInfo dynamicPropertyInfo in dynamicPropertyResolver.DynamicProperties)
							{
								if (dynamicPropertyInfo.Id == targetInfo.Path)
								{
									value = convertible2.ToType(dynamicPropertyInfo.Type, null);
									break;
								}
							}
						}
					}
					if (dynamicPropertyResolver.SetDynamicPropertyValue(targetInfo.Path, value))
					{
						result = AsyncOperationResult.Success;
					}
				}
			}
			catch (Exception)
			{
				result = AsyncOperationResult.Failure;
			}
			this.CompleteAsyncOperation(result);
		}

		// Token: 0x0600B17C RID: 45436 RVA: 0x0036C1A0 File Offset: 0x0036A3A0
		public bool TryGetValueFromTarget<T>(out T value)
		{
			return this.m_nestedReference.TryGetValueFromTarget<T>(out value);
		}

		// Token: 0x0600B17D RID: 45437 RVA: 0x0036C1AE File Offset: 0x0036A3AE
		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type, LogLevel level = LogLevel.Info)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, level, type);
		}

		// Token: 0x0600B17E RID: 45438 RVA: 0x0036C1C0 File Offset: 0x0036A3C0
		private void CompleteAsyncOperation(AsyncOperationResult result)
		{
			this.NestedReference.RemoveReadyOrInactiveListener();
			if (this.m_callback != null)
			{
				Override.ApplyCallbackDelegate callback = this.m_callback;
				this.m_callback = null;
				callback(result, this.m_callbackUserData);
			}
		}

		// Token: 0x040095A6 RID: 38310
		[SerializeField]
		private NestedReference m_nestedReference;

		// Token: 0x040095A7 RID: 38311
		[SerializeField]
		private string m_valueString;

		// Token: 0x040095A8 RID: 38312
		[SerializeField]
		private Color m_valueColor;

		// Token: 0x040095A9 RID: 38313
		[SerializeField]
		private double m_valueDouble;

		// Token: 0x040095AA RID: 38314
		[SerializeField]
		private bool m_valueBool;

		// Token: 0x040095AB RID: 38315
		[SerializeField]
		private string m_valueAsset;

		// Token: 0x040095AC RID: 38316
		[SerializeField]
		private long m_valueLong;

		// Token: 0x040095AD RID: 38317
		[SerializeField]
		private Vector4 m_valueVector;

		// Token: 0x040095AE RID: 38318
		private Override.ApplyCallbackDelegate m_callback;

		// Token: 0x040095AF RID: 38319
		private object m_callbackUserData;

		// Token: 0x040095B0 RID: 38320
		private NestedReferenceTargetInfo m_propertyPendingAssignment;

		// Token: 0x040095B1 RID: 38321
		private int m_asyncOperationId;

		// Token: 0x02002823 RID: 10275
		// (Invoke) Token: 0x06013B15 RID: 80661
		public delegate void ApplyCallbackDelegate(AsyncOperationResult result, object userData);
	}
}
