using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MusicManager
{
    class SaveManager : Singleton<SaveManager>
    {
        static string CurrentDirectory
        {
            get
            {
                return Android.OS.Environment.ExternalStorageDirectory + "//MusicManagerTest";
            }
        }

        public static void Save(object obj)
        {
            string fullPath = GetJSONFullPath(obj);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            /*
            JObject o;
            {
                o = new JObject();
                foreach (string propertyName in properties)
                {
                    PropertyInfo property = obj.GetType().GetProperty(propertyName);

                    if (property != null)
                    {
                        o.Add(new JProperty(propertyName, JsonConvert.SerializeObject(property.GetValue(obj))));
                    }
                    else
                    {
                        DebugManager.Log("Property not found on Save: " + propertyName);
                    }
                }
            }
            */
            File.WriteAllText(fullPath, JsonConvert.SerializeObject(obj, Formatting.Indented));
        }

        public static T Load<T>(object obj)
        {
            string fullPath = GetJSONFullPath(obj);

            if (!File.Exists(fullPath))
            {
                return (T)obj;
            }

            using (StreamReader reader = File.OpenText(fullPath))
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                reader.Close();

                return (T)JsonConvert.DeserializeObject<T>(o.ToString());                
            }
        }

        #region Helper classes

        static string GetJSONFileName(object obj)
        {
            return obj.ToString().Split('.').LastOrDefault() + ".json";
        }

        static string GetJSONFullPath(object obj)
        {
            return CurrentDirectory + "//" + GetJSONFileName(obj);
        }

        #endregion
    }
}