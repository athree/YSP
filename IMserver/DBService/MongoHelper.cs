﻿using MongoDB.Bson;
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
            string conString = ConfigurationManager.
                ConnectionStrings["MongoDB"].ConnectionString;
            var con = new MongoConnectionStringBuilder(conString);
            _mongoClient = new MongoClient(conString);
            _mongoServer = _mongoClient.GetServer();
            _mongodb= _mongoServer.GetDatabase(con.DatabaseName);
            Collection = _mongodb.GetCollection<T>(typeof(T).Name.ToLower());
        }

        //public IList<T> FindAll()
        //{
        //    return Collection.FindAll().ToList();
        //}

        public IQueryable<T> FindAll()
        {
            return Collection.FindAll().AsQueryable<T>();
        }
        public IQueryable<T> FindBy(Expression<Func<T, bool>> expression)
        {
            var query = Query<T>.Where(expression);
            var document = Collection.Find(query).AsQueryable();
            return document;
        }
        public IQueryable<T> FindNBy(Expression<Func<T, IEnumerable<bool>>> expression, int num)
        {
            var query = Query<T>.Size<bool>(expression,num);
            var document = Collection.Find(query).AsQueryable();
            return document;
          
          
        }
        public T FindOneBy(Expression<Func<T, bool>> expression)
        {
            var query = Query<T>.Where(expression);
            var document = Collection.FindOne(query);
            return document;
        }
        public T FindById(ObjectId id)
        {
            return Collection.FindOneById(id);
        }
     
        public bool Insert(T entity)
        {
            return Collection.Insert(entity).Ok;
        }
        public bool Update(T entity)
        {
            return Collection.Save(entity).Ok;
        }
        public bool Delete(Expression<Func<T, bool>> expression)
        {
            var query = Query<T>.Where(expression);
            return Collection.Remove(query).Ok;
        }
        public bool Delete(ObjectId id)
        {
           
            return Collection.Remove(Query.EQ("_id", id)).Ok;
        }
    }
}