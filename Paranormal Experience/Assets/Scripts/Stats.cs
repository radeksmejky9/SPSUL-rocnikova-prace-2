using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Stats : GameSingleton<Stats>
    {
        public StatsData statsData;
        public void Load(StatsData data)
        {
            this.statsData = data;
        }
    }
}
