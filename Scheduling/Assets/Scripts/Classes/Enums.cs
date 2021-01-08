using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oof{

public enum Dicipline
{
    ProC = 0,
    ProP = 1,
    Game = 2,
    Data = 3,
    Comp = 4,
    Oper = 5,
    ProL = 6,
    Cybe = 7,
    Mobi = 8,
    Arit = 9,
    Netw = 10,
    Theo = 11,
    Para = 12
};

public enum Timeslot
{
    NoClass = 0,
    MW1 = 1,
    MW2 = 2,
    MW3 = 3,
    MW4 = 4,
    TR1 = 5,
    TR2 = 6,
    TR3 = 7,
    TR4 = 8
};

public class RandomFunctions
{
    public string TimeSlotToString(Timeslot time)
    {
        switch(time)
        {
            case Timeslot.NoClass:
                return "No class";
            case Timeslot.MW1:
                return "MW 9:00 am ~ 10:15 am";
            case Timeslot.MW2:
                return "MW 10:30 am ~ 11:45 am";
            case Timeslot.MW3:
                return "MW 12:00 pm ~ 1:15 pm";
            case Timeslot.MW4:
                return "MW 1:30 pm ~ 2:45 pm";
            case Timeslot.TR1:
                return "TR 9:00 am ~ 10:15 am";
            case Timeslot.TR2:
                return "TR 10:30 am ~ 11:45 am";
            case Timeslot.TR3:
                return "TR 12:00 pm ~ 1:15 pm";
            case Timeslot.TR4:
                return "TR 1:30 pm ~ 2:45 pm";
        }
        return "null";
    }

    public string DeciplineToString(Dicipline dicipline)
    {
        switch(dicipline)
        {
            case Dicipline.ProC:
                return "Programming - C++";
            case Dicipline.ProP:
                return "Programming - Python";
            case Dicipline.Game:
                return "Game Development";
            case Dicipline.Data:
                return "Data Structures and Algorithms";
            case Dicipline.Comp:
                return "Computer Organization";
            case Dicipline.Oper:
                return "Operating Systems";
            case Dicipline.ProL:
                return "Programming Languages";
            case Dicipline.Cybe:
                return "Cybersecurity";
            case Dicipline.Mobi:
                return "Mobile Applications";
            case Dicipline.Arit:
                return "Artificial Intelligence";
            case Dicipline.Netw:
                return "Networks";
            case Dicipline.Theo:
                return "Theory of Computation";
            case Dicipline.Para:
                return "Parallel and Distributed Systems";
        }
        return "null";
    }
}

}