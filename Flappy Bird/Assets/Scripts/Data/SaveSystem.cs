using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace FlappyBird.Data
{
    public static class SaveSystem
    {
        public static void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            string path = Application.persistentDataPath + "/data.bin";
            FileStream stream = new FileStream(path, FileMode.Create);

            SaveData data = new SaveData();

            bf.Serialize(stream, data);
            stream.Close();
        }

        public static SaveData Load()
        {
            string path = Application.persistentDataPath + "/data.bin";
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = null;
            SaveData data = null;

            if (File.Exists(path))
            {
                stream = new FileStream(path, FileMode.Open);

                data = bf.Deserialize(stream) as SaveData;
                stream.Close();

                return data;
            }
            stream = new FileStream(path, FileMode.Create);

            data = new SaveData(true);
            bf.Serialize(stream, data);
            stream.Close();
            return data;
        }

    }
}
