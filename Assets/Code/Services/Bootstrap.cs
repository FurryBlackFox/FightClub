using Code.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Services
{
    public class Bootstrap : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static async void InitServices()
        {
            if (SceneManager.GetActiveScene().name != Consts.Scenes.BootstrapSceneName)
                SceneManager.LoadScene(Consts.Scenes.BootstrapSceneName);
            
            var serviceLocator = new ServiceLocator();

            var resourcesManager = new ResourcesManager();
            serviceLocator.TryRegisterService(resourcesManager);

            var settingsProvider = new SettingsProvider(resourcesManager);
            serviceLocator.TryRegisterService(settingsProvider);
            
            await settingsProvider.ReadSettingsFromFile();

            SceneManager.LoadSceneAsync(Consts.Scenes.MainSceneName);
        }
    }
}