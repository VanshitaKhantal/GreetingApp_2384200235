using ModelLayer.Model;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        /// <summary>
        /// It takes one method signature that saves a greeting message
        /// </summary>
        /// <param name="greetingModel"></param>
        void SaveGreeting(GreetingModel greetingModel);
    }
}
