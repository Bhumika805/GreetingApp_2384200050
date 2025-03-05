﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Service;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        
        string GetGreetingbyName(UserRequestModel request);
        Greeting AddGreeting(Greeting greeting);
       
    }
}
