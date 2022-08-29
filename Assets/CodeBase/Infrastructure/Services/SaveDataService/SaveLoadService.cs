using UnityEngine;

namespace CodeBase.Infrastructure.Services.Progress
{
    public class SaveLoadService<T> : ISaveLoadService<T> where T : new ()
    {
        public void Save(string key, T data)
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, json);
        }
        public bool Load(string key, out T data)
        {
            // data = new T();
            // return false;
            //for developing
             var json = PlayerPrefs.GetString(key, "");
             if (json == "")
             {
                 data = new T();
                 return false;
             }
             else
             {
                 data = JsonUtility.FromJson<T>(json);
                 return true;
             }
            
        }
        
    }
}