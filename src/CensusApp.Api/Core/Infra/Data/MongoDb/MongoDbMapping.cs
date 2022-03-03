using CensusApp.Api.Core.Infra.Data.MongoDb.Maps;
using System;
using System.Collections.Generic;

namespace CensusApp.Api.Core.Infra.Data.MongoDb
{
    public class MongoDbMapping
    {
        private IList<IMongoDbClassMap> _mapList;
        public MongoDbMapping()
        {
            _mapList = new List<IMongoDbClassMap>();
        }

        public MongoDbMapping Add<T>() where T : IMongoDbClassMap
        {
            _mapList.Add(Activator.CreateInstance<T>());
            return this;
        }
        public MongoDbMapping Initialize()
        {
            foreach (var item in _mapList)
            {
                item.CreateMap();
            }
            
            return this;
        }

    }
}
