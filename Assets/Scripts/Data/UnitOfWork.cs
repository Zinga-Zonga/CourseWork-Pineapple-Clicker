using Assets.Scripts.Entities;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.Data
{
    internal class UnitOfWork<T> : IUnitOfWork<T> where T : IEntity
    {
        public IRepository<T> Repository { get; } = new Repository<T>();

        public void SaveChanges()
        {
            var entities = Repository.GetList();
          
            var json = JsonConvert.SerializeObject(entities, Formatting.Indented,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });


            string path = entities.GetType().GenericTypeArguments[0].Name;
            using (var f = File.CreateText(Application.persistentDataPath + $"/{path}.json"))
            {
                f.Write(json);
            }
        }
    }
}
