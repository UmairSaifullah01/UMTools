using System;
using UnityEngine;
using Object = UnityEngine.Object;


namespace THEBADDEST
{


	[Serializable]
	public class InterfaceReference<T> : ISerializationCallbackReceiver
	{

		[SerializeField] Object m_Reference;
		T                       cacheValue;
		public T Reference
		{
			get
			{
				if (cacheValue is not null) return cacheValue;
				if (m_Reference is GameObject gameObject)
				{
					T component                       = gameObject.GetComponent<T>();
					if (component != null) cacheValue = component;
				}

				return cacheValue;
			}
		}

		public void OnBeforeSerialize()
		{
			if (m_Reference is GameObject gameObject)
			{
				T component = gameObject.GetComponent<T>();
				if (component != null) return;
			}
			else if (m_Reference is T) return;

			m_Reference = null;
		}

		public void OnAfterDeserialize()
		{
			if (m_Reference is GameObject)
			{
				return;
			}

			cacheValue = m_Reference is T reference1 ? reference1 : default;
		}

	}


}