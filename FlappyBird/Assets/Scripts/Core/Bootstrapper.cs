using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flappy_Assgnmt3.Core
{
    public static class Bootstrapper
    {
        private const string bootstrapSceneName = "Bootstrap";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void RunBootstrap()
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if (scene != null && scene.name == bootstrapSceneName) return;
            }

            SceneManager.LoadScene(bootstrapSceneName, LoadSceneMode.Additive);
        }
    }
}