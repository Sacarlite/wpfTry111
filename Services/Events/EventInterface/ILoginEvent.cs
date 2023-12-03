﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfTry.VievModel;

namespace wpfTry.Services.Events.EventInterface
{
    public interface ILoginEvent
    {
        public event EventHandler<LoginEventArgs> Login;
        public void InvokeEvent(RolleModel item);

    }
}
