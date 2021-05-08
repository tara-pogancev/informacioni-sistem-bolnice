using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private Dictionary<KeyType, Entity> ReadFile()
        {
            string path = getPath();

            if (!File.Exists(path))
            {
                File.Create(path).Close();
                return new Dictionary<KeyType, Entity>();
            }
            FileInfo fi = new FileInfo(path);
            if (fi.Length == 0)
            {
                return new Dictionary<KeyType, Entity>();
            }

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<Dictionary<KeyType, Entity>>(json);
        }

        private void WriteFile(Dictionary<KeyType, Entity> entities)
        {
            string path = getPath();
            string json = JsonConvert.SerializeObject(entities, Formatting.Indented);

            File.WriteAllText(path, json);
        }


        public bool Create(Entity Entity)
        {
            Dictionary<KeyType, Entity> entities = ReadFile();

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
            return ReadFile();
        }

        public Entity Read(KeyType key)
        {
            Dictionary<KeyType, Entity> entities = ReadFile();
            Entity retVal;

            if (!entities.TryGetValue(key, out retVal))
            {
                return default;
            }

            return retVal;
        }

        public bool Update(Entity Entity)
        {
            Dictionary<KeyType, Entity> entities = ReadFile();

            KeyType key = getKey(Entity);

            if (!entities.ContainsKey(key))
            {
                return false;
            }

            entities[key] = Entity;

            WriteFile(entities);

            return true;
        }

        public void CreateOrUpdate(Entity Entity)
        {
            Dictionary<KeyType, Entity> entities = ReadFile();

            KeyType key = getKey(Entity);

            entities[key] = Entity;

            WriteFile(entities);
        }


        public bool Delete(KeyType key)
        {
            Dictionary<KeyType, Entity> entities = ReadFile();

            bool retVal = entities.Remove(key);

            RemoveReferences(key);

            WriteFile(entities);

            return retVal;
        }

        public bool Delete(Entity entity)
        {
            return Delete(getKey(entity));
        }

        public List<Entity> ReadList()
        {
            return this.ReadFile().Values.ToList();
        }

    }
}
