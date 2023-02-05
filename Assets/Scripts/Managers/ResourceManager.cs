using Lfish.Pattern;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine;
using System.Collections.Generic;

namespace Lfish
{
    public class ResourceManager : SingletonCommon<ResourceManager>
    {
        public void LoadAssetAsyn<T>(string key, UnityAction<T> onCompleted)
        {
            var loader = Addressables.LoadAssetAsync<T>(key);
            if (!loader.IsValid())
            {
                Debug.LogError($"Error: failed at loading {key}");
                return;
            }
            loader.Completed += handle =>
            {
                var result = handle.Result;
                onCompleted?.Invoke(result);
                // EventManager.Instance.FireEvent(EventTypes.RES_LOADED, result);
            };
        }

        public void Unload<T>(T obj)
        {
            Addressables.Release(obj);
        }
    }
}
