﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20
{
    internal interface IState
    {
        public void EnterState();
        public void UpdateState();
        public void ExitState();
    }
}
