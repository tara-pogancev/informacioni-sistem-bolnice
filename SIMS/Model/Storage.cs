using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Model
{
    public abstract class Storage<KeyType, Entity, StorageType>
        where StorageType : Storage<KeyType, Entity, StorageType>, new()
    {

        private static StorageType _instance = new StorageType();
        public static StorageType Instance
        {
            get
            {
                return _instance;
            }
        }

        protected abstract string getPath();
        protected abstract KeyType getKey(Entity entity);
        protected abstract void RemoveReferences(KeyType key);

        private Dictionary<KeyType, Entity> readFile()
        {
            string path = getPath();

            if (!File.Exists(path))
            {
                File.Create(path).Close();
                return new Dictionary<KeyType, Entity>();
            }

            string json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<Dictionary<KeyType, Entity>>(json);
        }

        private void WriteFile(Dictionary<KeyType, Entity> entities)
        {
            string path = getPath();
            string json = JsonSerializer.Serialize(entities);

            File.WriteAllText(path, json);
        }


        public bool Create(Entity Entity)
        {
            Dictionary<KeyType, Entity> entities = readFile();

            KeyType key = getKey(Entity);

            if (entities.ContainsKey(key))
            {
                return false;
            }

            entities[key] = Entity;

            WriteFile(entities);

            return true;
        }

        public Dictionary<KeyType, Entity> ReadAll()
        {
            return readFile();
        }

        public Entity Read(KeyType key)
        {
            Dictionary<KeyType, Entity> entities = readFile();
            Entity retVal;

            if (!entities.TryGetValue(key, out retVal))
            {
                return default;
            }

            return retVal;
        }

        public bool Update(Entity Entity)
        {
            Dictionary<KeyType, Entity> entities = readFile();

            KeyType key = getKey(Entity);

            if (!entities.ContainsKey(key))
            {
                return false;
            }

            entities[key] = Entity;

            WriteFile(entities);

            return true;
        }


        public bool Delete(KeyType key)
        {
            Dictionary<KeyType, Entity> entities = readFile();

            bool retVal = entities.Remove(key);

            RemoveReferences(key);

            WriteFile(entities);

            return retVal;
        }

    }
}
