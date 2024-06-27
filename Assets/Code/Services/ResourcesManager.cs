using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Code.Services
{
    public class ResourcesManager : IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource = new();
        
        public ResourcesManager()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }

        private void OnSceneUnloaded(Scene scene)
        {
            UnloadAssets();
        }
        
        private void UnloadAssets()
        {
            Resources.UnloadUnusedAssets();
        }

        public async Task<T> LoadAsset<T>(string path) where T : Object
        {
            var result = Resources.LoadAsync<T>(path);

            while (!result.isDone)
            {
                if (_cancellationTokenSource.IsCancellationRequested)
                    return null;

                await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
            }

            return result.asset as T;
        }
        
        
    }
}