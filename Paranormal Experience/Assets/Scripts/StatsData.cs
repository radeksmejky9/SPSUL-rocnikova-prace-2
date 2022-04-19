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
    int ghostWritingCount;
    int emfCount;
    int freezingTempsCount;
    int ghostEventCount;

    public StatsData(Stats instance)
    {
        this.GameCount = instance.statsData.GameCount;
        this.WinCount = instance.statsData.WinCount;
        this.SaltsPlaced = instance.statsData.SaltsPlaced;
        this.SaltProcs = instance.statsData.SaltProcs;
        this.BooksPlaced = instance.statsData.BooksPlaced;
        this.EmfCount = instance.statsData.EmfCount;
        this.FreezingTempsCount = instance.statsData.FreezingTempsCount;
        this.GhostWritingCount = instance.statsData.GhostWritingCount;
        this.GhostEventCount = instance.statsData.GhostEventCount;
    }

    public StatsData(int gameCount, int winCount, int saltsPlaced, int saltProcs, int booksPlaced, int ghostWritingCount, int emfCount, int freezingTempsCount, int ghostEventCount)
    {
        this.gameCount = gameCount;
        this.winCount = winCount;
        this.saltsPlaced = saltsPlaced;
        this.saltProcs = saltProcs;
        this.booksPlaced = booksPlaced;
        this.ghostWritingCount = ghostWritingCount;
        this.emfCount = emfCount;
        this.freezingTempsCount = freezingTempsCount;
        this.ghostEventCount = ghostEventCount;
    }

    public int GameCount { get => gameCount; set => gameCount = value; }
    public int WinCount { get => winCount; set => winCount = value; }
    public int SaltsPlaced { get => saltsPlaced; set => saltsPlaced = value; }
    public int SaltProcs { get => saltProcs; set => saltProcs = value; }
    public int BooksPlaced { get => booksPlaced; set => booksPlaced = value; }
    public int GhostWritingCount { get => ghostWritingCount; set => ghostWritingCount = value; }
    public int EmfCount { get => emfCount; set => emfCount = value; }
    public int FreezingTempsCount { get => freezingTempsCount; set => freezingTempsCount = value; }
    public int GhostEventCount { get => ghostEventCount; set => ghostEventCount = value; }
}

