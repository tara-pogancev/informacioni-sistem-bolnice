using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace SIMS.Repositories
{
    public abstract class GenericFileRepository<KeyType, Entity, StorageType>
        where StorageType : GenericFileRepository<KeyType, Entity, StorageType>, new()
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
        protected abstract void ShouldSerialize(Entity entity);

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


        public void Save(Entity Entity)
        {
            Dictionary<KeyType, Entity> entities = ReadFile();
            ShouldSerialize(Entity);
            KeyType key = getKey(Entity);

            if (entities.ContainsKey(key))
            {
                return;
            }

            entities[key] = Entity;

            WriteFile(entities);

            return;
        }

        public Dictionary<KeyType, Entity> ReadAll()
        {
            return ReadFile();
        }

        public Entity FindById(KeyType key)
        {
            Dictionary<KeyType, Entity> entities = ReadFile();
            Entity retVal;

            if (!entities.TryGetValue(key, out retVal))
            {
                return default;
            }

            return retVal;
        }

        public void Update(Entity Entity)
        {
            Dictionary<KeyType, Entity> entities = ReadFile();

            ShouldSerialize(Entity);
            KeyType key = getKey(Entity);

            if (!entities.ContainsKey(key))
            {
                return;
            }

            entities[key] = Entity;

            WriteFile(entities);

            return;
        }

        public void CreateOrUpdate(Entity Entity)
        {
            Dictionary<KeyType, Entity> entities = ReadFile();
            ShouldSerialize(Entity);
            KeyType key = getKey(Entity);

            entities[key] = Entity;

            WriteFile(entities);
        }
        

        public void Delete(KeyType key)
        {
            Dictionary<KeyType, Entity> entities = ReadFile();

            bool retVal = entities.Remove(key);

            RemoveReferences(key);

            WriteFile(entities);
        }

        public void Delete(Entity entity)
        {
             Delete(getKey(entity));
        }

        public List<Entity> GetAll()
        {
            return this.ReadFile().Values.ToList();
        }

    }
}
