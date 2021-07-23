using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using ToDoAPI.Dtos;
using DataAccesslayer;
using DataAccesslayer.Models;

namespace ToDoAPI.Controllers
{

    public class MongoTodosController : ApiController
    {

        //  public IMongoDatabase mongoDb = new MongoClient(ConfigurationManager.AppSettings["connectionString"]).GetDatabase("Todo");
        MongoConnectionHelper mongoDb;
        IMongoDatabase context;
        IMongoCollection<MongoTodo> collection;

        public MongoTodosController()
        {
            mongoDb = new MongoConnectionHelper();
            context = mongoDb.GetConnection();
            collection = context.GetCollection<MongoTodo>("Todos");

        }
        [HttpGet]
        public IHttpActionResult Todos()
        {
            var todos = collection.Find(new BsonDocument()).ToList().Select(Mapper.Map<MongoTodo, MongoTodoDto>);
            return Ok(todos);

        }

        [HttpGet]
        public IHttpActionResult Todo(string id)
        {
            try
            {

                //  collection.Find(Builders<Employee>.Filter.Where(s => s.Id == id)).FirstOrDefault()
                var todo = Mapper.Map<MongoTodo, MongoTodoDto>(collection.Find(Builders<MongoTodo>.Filter.Where(x => x.Id == id)).FirstOrDefault());
                if (todo == null)
                    return BadRequest("Please enter valid id");
                return Ok(todo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Todo([FromBody] MongoTodoDto todo)
        {
            var MongoTodo = Mapper.Map<MongoTodoDto, MongoTodo>(todo);
            collection.InsertOneAsync(MongoTodo);
            todo.Id = MongoTodo.Id;
            return Created(new Uri(Request.RequestUri + "/" + todo.Id), todo);
        }

        [HttpPut]
        public IHttpActionResult TodoUpdate([FromUri] string id, [FromBody] MongoTodoDto todo)
        {
            var result = collection.FindOneAndUpdateAsync(Builders<MongoTodo>.Filter.Where(x => x.Id == id), Builders<MongoTodo>.Update.Set("TodoData", todo.TodoData).Set("IsCompleted", todo.IsCompleted)).Result;
            // var res = collecion.FindOneAndReplace(Builders<Todo>.Filter.Eq("id",))
            return Ok(result);
        }

        [HttpDelete]
        public IHttpActionResult TodoDelete(string id)
        {

            var result = collection.DeleteOne(Builders<MongoTodo>.Filter.Where(x => x.Id == id));
            //  var result =  collection.DeleteOneAsync(Builders<Todo>.Filter.Eq("_id", id)).Result;
            return Ok(result);
        }


    }
}
