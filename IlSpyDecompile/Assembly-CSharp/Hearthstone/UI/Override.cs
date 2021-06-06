using System;
using System.Diagnostics;
using Blizzard.T5.AssetManager;
using Hearthstone.UI.Internal;
using Hearthstone.UI.Logging;
using UnityEngine;

namespace Hearthstone.UI
{
	[Serializable]
	public class Override
	{
		public delegate void ApplyCallbackDelegate(AsyncOperationResult result, object userData);

		[SerializeField]
		private NestedReference m_nestedReference;

		[SerializeField]
		private string m_valueString;

		[SerializeField]
		private Color m_valueColor;

		[SerializeField]
		private double m_valueDouble;

		[SerializeField]
		private bool m_valueBool;

		[SerializeField]
		private string m_valueAsset;

		[SerializeField]
		private long m_valueLong;

		[SerializeField]
		private Vector4 m_valueVector;

		private ApplyCallbackDelegate m_callback;

		private object m_callbackUserData;

		private NestedReferenceTargetInfo m_propertyPendingAssignment;

		private int m_asyncOperationId;

		public double ValueDouble => m_valueDouble;

		public NestedReference NestedReference => m_nestedReference;

		public void Apply(ApplyCallbackDelegate callback, object userData = null, bool loadSynchronously = false)
		{
			Abort();
			m_callback = callback;
			m_callbackUserData = userData;
			UnityEngine.Object @object = null;
			try
			{
				if (!m_nestedReference.Resolve(out NestedReferenceTargetInfo targetInfo))
				{
					CompleteAsyncOperation(AsyncOperationResult.Failure);
					return;
				}
				switch (m_nestedReference.TargetType)
				{
				case NestedReference.TargetTypes.String:
					SetValueOnTarget(targetInfo, m_valueString);
					break;
				case NestedReference.TargetTypes.Enum:
					SetValueOnTarget(targetInfo, Enum.Parse(targetInfo.Property.PropertyType, m_valueString));
					break;
				case NestedReference.TargetTypes.Color:
					SetValueOnTarget(targetInfo, m_valueColor);
					break;
				case NestedReference.TargetTypes.Float:
					SetValueOnTarget(targetInfo, (float)m_valueDouble);
					break;
				case NestedReference.TargetTypes.Double:
					SetValueOnTarget(targetInfo, m_valueDouble);
					break;
				case NestedReference.TargetTypes.Int:
					SetValueOnTarget(targetInfo, (int)m_valueLong);
					break;
				case NestedReference.TargetTypes.Long:
					SetValueOnTarget(targetInfo, m_valueLong);
					break;
				case NestedReference.TargetTypes.Bool:
					SetValueOnTarget(targetInfo, m_valueBool);
					break;
				case NestedReference.TargetTypes.Vector2:
				case NestedReference.TargetTypes.Vector3:
				case NestedReference.TargetTypes.Vector4:
					SetValueOnTarget(targetInfo, m_valueVector, implicitConversion: true);
					break;
				case NestedReference.TargetTypes.Material:
				case NestedReference.TargetTypes.Texture:
				case NestedReference.TargetTypes.Mesh:
					LoadAssetAndAssignToProperty(targetInfo, m_asyncOperationId, loadSynchronously);
					break;
				case NestedReference.TargetTypes.Component:
					break;
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogErrorFormat("Error when applying override '{0}' to '{1}': {2}", @object, m_valueString, ex);
			}
		}

		public void ApplyWithValue(ApplyCallbackDelegate callback, object value, object userData = null, bool implicitConversion = false)
		{
			Abort();
			m_callback = callback;
			m_callbackUserData = userData;
			UnityEngine.Object @object = null;
			try
			{
				if (!m_nestedReference.Resolve(out NestedReferenceTargetInfo targetInfo))
				{
					CompleteAsyncOperation(AsyncOperationResult.Failure);
				}
				else
				{
					SetValueOnTarget(targetInfo, value, implicitConversion);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogErrorFormat("Error when applying override '{0}' to '{1}': {2}", @object, value, ex);
			}
		}

		public void Abort()
		{
			CompleteAsyncOperation(AsyncOperationResult.Aborted);
			m_asyncOperationId++;
		}

		public void RegisterReadyListener(Action<object> action, object payload = null)
		{
			NestedReference.RegisterReadyListener(action, payload);
		}

		public void RemoveReadyOrInactiveListener(Action<object> action)
		{
			NestedReference.RemoveReadyOrInactiveListener();
		}

		public bool Resolve(out GameObject gameObject)
		{
			return m_nestedReference.Resolve(out gameObject);
		}

		private void LoadAssetAndAssignToProperty(NestedReferenceTargetInfo property, int asyncOperationId, bool loadSynchronously)
		{
			m_propertyPendingAssignment = property;
			AssetReference assetReference = AssetReference.CreateFromAssetString(m_valueAsset);
			if (assetReference != null)
			{
				if (loadSynchronously)
				{
					switch (m_nestedReference.TargetType)
					{
					case NestedReference.TargetTypes.Material:
						HandleAssetLoaded(assetReference, AssetLoader.Get().LoadAsset<Material>(assetReference), asyncOperationId);
						break;
					case NestedReference.TargetTypes.Texture:
						HandleAssetLoaded(assetReference, AssetLoader.Get().LoadAsset<Texture>(assetReference), asyncOperationId);
						break;
					case NestedReference.TargetTypes.Mesh:
						HandleMeshLoaded(assetReference, AssetLoader.Get().LoadMesh(assetReference), asyncOperationId);
						break;
					}
				}
				else
				{
					switch (m_nestedReference.TargetType)
					{
					case NestedReference.TargetTypes.Material:
						AssetLoader.Get().LoadAsset<Material>(assetReference, HandleAssetLoaded, asyncOperationId);
						break;
					case NestedReference.TargetTypes.Texture:
						AssetLoader.Get().LoadAsset<Texture>(assetReference, HandleAssetLoaded, asyncOperationId);
						break;
					case NestedReference.TargetTypes.Mesh:
						AssetLoader.Get().LoadMesh(assetReference, HandleMeshLoaded, asyncOperationId);
						break;
					}
				}
			}
			else
			{
				SetValueOnTarget(property, null);
			}
		}

		private void HandleAssetLoaded<T>(AssetReference assetRef, AssetHandle<T> assetHandle, object asyncOperationId) where T : UnityEngine.Object
		{
			if (m_asyncOperationId != (int)asyncOperationId)
			{
				AssetHandle.SafeDispose(ref assetHandle);
				return;
			}
			HearthstoneServices.Get<WidgetRunner>().RegisterAssetHandle(m_propertyPendingAssignment.Target, assetHandle);
			SetValueOnTarget(m_propertyPendingAssignment, (assetHandle != null) ? assetHandle.Asset : null);
		}

		private void HandleMeshLoaded(AssetReference assetReference, UnityEngine.Object obj, object asyncOperationId)
		{
			if (m_asyncOperationId == (int)asyncOperationId)
			{
				SetValueOnTarget(m_propertyPendingAssignment, obj);
			}
		}

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
							foreach (DynamicPropertyInfo dynamicProperty in dynamicPropertyResolver.DynamicProperties)
							{
								if (dynamicProperty.Id == targetInfo.Path)
								{
									value = convertible2.ToType(dynamicProperty.Type, null);
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
			CompleteAsyncOperation(result);
		}

		public bool TryGetValueFromTarget<T>(out T value)
		{
			return m_nestedReference.TryGetValueFromTarget<T>(out value);
		}

		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type, LogLevel level = LogLevel.Info)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, level, type);
		}

		private void CompleteAsyncOperation(AsyncOperationResult result)
		{
			NestedReference.RemoveReadyOrInactiveListener();
			if (m_callback != null)
			{
				ApplyCallbackDelegate callback = m_callback;
				m_callback = null;
				callback(result, m_callbackUserData);
			}
		}
	}
}
