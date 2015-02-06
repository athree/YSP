using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace IMserver.DBservice
{
    public class MongoHelper<T> where T:class
    {
        private MongoClient _mongoClient;
        private MongoServer _mongoServer;
        private MongoDatabase _mongodb;

        private readonly MongoCollection<T> Collection;
        public MongoHelper()
        {
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;
                var con = new MongoConnectionStringBuilder(conString);
                _mongoClient = new MongoClient(conString);
                _mongoServer = _mongoClient.GetServer();
                _mongodb = _mongoServer.GetDatabase(con.DatabaseName);
                Collection = _mongodb.GetCollection<T>(typeof(T).Name.ToLower());
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }

        public IQueryable<T> FindAll()
        {
            try
            {
                return Collection.FindAll().AsQueryable<T>();
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);
            }

        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> expression)
        {
            try
            {
                var query = Query<T>.Where(expression);
                var document = Collection.Find(query).AsQueryable();
                return document;
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }

        public IQueryable<T> FindNBy(Expression<Func<T, IEnumerable<bool>>> expression, int num)
        {
            try
            {
                var query = Query<T>.Size<bool>(expression,num);
                var document = Collection.Find(query).AsQueryable();
                return document;
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }

        public T FindOneBy(Expression<Func<T, bool>> expression)
        {
            try
            {
                var query = Query<T>.Where(expression);
                var document = Collection.FindOne(query);
                return document;
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }

        public T FindById(ObjectId id)
        {
            try
            {
                return Collection.FindOneById(id);
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }
     
        public bool Insert(T entity)
        {
            try
            {
                return Collection.Insert(entity).Ok;
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }

        public bool Update(T entity)
        {
            try
            {
                return Collection.Save(entity).Ok;
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }

        public bool Delete(Expression<Func<T, bool>> expression)
        {
            try
            {
                var query = Query<T>.Where(expression);
                return Collection.Remove(query).Ok;
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }

        public bool Delete(ObjectId id)
        {
           try
            {
                return Collection.Remove(Query.EQ("_id", id)).Ok;
            }
           catch (Exception ep)
           {
               throw new Exception(ep.Message);
           }
        }
    }
}