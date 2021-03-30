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
        protected abstract void removeReferences(KeyType key);

        private Dictionary<KeyType, Entity> readFile()
        {
            if (!File.Exists(getPath()))
            {
                File.Create(getPath());
            }

            string json = File.ReadAllText(getPath());

            if (json.Equals(""))
            {
                return new Dictionary<KeyType, Entity>();
            }
            else
            {
                return JsonSerializer.Deserialize<Dictionary<KeyType, Entity>>(json);
            }
        }

        private void writeFile(Dictionary<KeyType, Entity> entities)
        {
            string json = JsonSerializer.Serialize(entities);
            File.WriteAllText(getPath(), json);
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

            writeFile(entities);

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
                return default(Entity);
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

            writeFile(entities);

            return true;
        }


        public bool Delete(KeyType key)
        {
            Dictionary<KeyType, Entity> entities = readFile();

            bool retVal = entities.Remove(key);

            removeReferences(key);

            writeFile(entities);

            return retVal;
        }

    }
}
