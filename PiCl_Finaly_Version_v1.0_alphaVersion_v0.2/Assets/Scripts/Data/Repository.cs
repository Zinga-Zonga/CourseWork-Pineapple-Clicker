using Assets.Scripts.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Data
{
    internal class Repository<T> : IRepository<T> where T : IEntity
    {
        public List<T> _repository = new List<T>();
        
        public void Add(T entity)
        {
            _repository.Add(entity);
        }

        public List<T> GetAll()
        {
            
            string path = typeof(T).Name.ToString();
            
            if (!File.Exists(Application.persistentDataPath + $"/{path}.json"))
            {
                return null;
            }

            using (var f = File.OpenText(Application.persistentDataPath + $"/{path}.json"))
            {
                Debug.Log("Repository");
                Debug.Log(Application.persistentDataPath);
                var json = f.ReadToEnd();
                _repository = JsonConvert.DeserializeObject<List<T>>(json,
                    new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                return _repository;

            }
        }
        public List<T> GetList()
        {
            return _repository;
        }
        public T GetById(int id)
        {
            return _repository.FirstOrDefault(entity => entity.Id == id);
        }


        public void Remove(int id)
        {
            var entity = _repository.FirstOrDefault(entity => entity.Id == id);

            if (entity != null)
            {
                _repository.Remove(entity);
            }
        }
    }
}
