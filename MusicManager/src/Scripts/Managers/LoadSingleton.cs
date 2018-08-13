using System;

namespace MusicManager
{
    class LoadSingleton<T> where T : class, new()
    {
        private static Lazy<T> instance = new Lazy<T>(() => SaveManager.Load<T>(new T()));

        public static T Instance
        {
            get
            {
                return instance.Value;
            }
        }
    }
}