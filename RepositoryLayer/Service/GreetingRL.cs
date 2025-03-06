using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {
        public string GetGreetingbyName(UserRequestModel request)
        { 
                if (!string.IsNullOrWhiteSpace(request.FirstName) && !string.IsNullOrWhiteSpace(request.LastName))
                {
                return $"Hello, {request.FirstName} {request.LastName}!";
                }
                else if (!string.IsNullOrWhiteSpace(request.FirstName))
                {
                return $"Hello, {request.FirstName}!";
                }
                else if (!string.IsNullOrWhiteSpace(request.LastName))
                {
                return $"Hello, {request.LastName}!";
                }
                return "Hello World!";
        }
        private readonly GreetingDbContext _context;

        public GreetingRL(GreetingDbContext context)
        {
            _context = context;
        }
        public Greeting AddGreeting(Greeting greeting)
        {
            _context.Greetings.Add(greeting);
            _context.SaveChanges();
            return greeting;
        }
        public string GetGreetingById(int id)
        {
            var greeting = _context.Greetings.FirstOrDefault(x => x.Id == id);
            return greeting?.Message ?? "Greeting not found";
        }

        public List<Greeting> GetGreetingList()
        {
            return _context.Greetings.ToList();
        }

        public Greeting UpdateGreetingMessage(int id, string newMessage)
        {
            var greeting = _context.Greetings.FirstOrDefault(x => x.Id == id);

            if (greeting != null)
            {
                greeting.Message = newMessage;
                _context.SaveChanges();
            }

            return greeting;
        }
        public bool DeleteGreeting(int id)
        {
            var greeting = _context.Greetings.FirstOrDefault(x => x.Id == id);

            if (greeting == null)
            {
                return false;
            }

            _context.Greetings.Remove(greeting);
            _context.SaveChanges();

            return true;
        }


    }

}

