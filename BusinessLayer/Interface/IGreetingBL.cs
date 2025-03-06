using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IGreetingBL
    {
        string GetGreetingMessage();
        string GetGreetingbyName(UserRequestModel request);

        Greeting AddGreeting(Greeting greeting);
        string GetGreetingById(int id);
        List<Greeting> GetGreetingList();
        Greeting UpdateGreetingMessage(int id, string newMessage);
        bool DeleteGreeting(int id);

    }
}
