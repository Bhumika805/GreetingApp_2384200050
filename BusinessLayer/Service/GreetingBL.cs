using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Service;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {
        private readonly IGreetingRL _greetingRL; // Declare the field

        public GreetingBL(IGreetingRL greetingRL)
        {
            _greetingRL = greetingRL; // Initialize in the constructor
        }
        
        public string GetGreetingbyName(UserRequestModel request)
        {
            return _greetingRL.GetGreetingbyName(request);
        }

        public Greeting AddGreeting(Greeting greeting)
        {
            return _greetingRL.AddGreeting(greeting);
        }
        public string GetGreetingMessage()
        {
            return "Hello World";
        }

        
    }
}
