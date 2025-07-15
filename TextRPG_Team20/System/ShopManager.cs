using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20
{
    internal class ShopManager
    {
        private static ShopManager? _instance;
        public static ShopManager Instance 
        {
            get
            {
                if(_instance == null)
                    _instance = new ShopManager();
                return _instance;
            } 
        }
    }
}
