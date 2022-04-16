using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[System.Serializable]
public class StatsData
{
    int gameCount;
    int winCount;
    int saltsPlaced;
    int saltProcs;
    int booksPlaced;
    int emfCount;

    public StatsData(Stats instance)
    {
        this.gameCount = instance.statsData.gameCount;
        this.winCount = instance.statsData.winCount;
        this.saltsPlaced = instance.statsData.saltsPlaced;
        this.saltProcs = instance.statsData.saltProcs;
        this.booksPlaced = instance.statsData.booksPlaced;
        this.emfCount = instance.statsData.emfCount;
    }
}

