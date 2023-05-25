using FilterManagerApp.Data.Entities;
using FilterManagerApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterManagerApp
{
    public class App : IApp
    {
        private readonly IUserCommunication _userCommunication;
        private readonly IEventHandlerService _eventHandlerService;

        public App(IUserCommunication userCommunication, IEventHandlerService eventHandlerService)
        {
            _userCommunication = userCommunication;
            _eventHandlerService = eventHandlerService;
        }
        public void Run()
        {
            _eventHandlerService.SubscribeToEvents();
            _userCommunication.RunIntro();
            _userCommunication.RunMenu();           

        }
    }
}
