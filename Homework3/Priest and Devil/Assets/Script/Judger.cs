using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judger
{
    private BoatController boat;
    private CoastController fromCoast;
    private CoastController toCoast;

    public Judger(CoastController fromCoast, CoastController toCoast, BoatController boat)
    {
        this.fromCoast = fromCoast;
        this.toCoast = toCoast;
        this.boat = boat;
    }

    public int GameState()
    {
        int state = Check();
        return state;
    }

    private int Check()
    {
        int from_priest = 0;
        int from_devil = 0;
        int to_priest = 0;
        int to_devil = 0;

        int[] fromCount = fromCoast.GetobjectsNumber();
        from_priest += fromCount[0];
        from_devil += fromCount[1];

        int[] toCount = toCoast.GetobjectsNumber();
        to_priest += toCount[0];
        to_devil += toCount[1];

        // win
        if (to_priest + to_devil == 6)
            return 2;

        int[] boatCount = boat.GetobjectsNumber();
        // on destination
        if (boat.get_State() == -1)
        {
            to_priest += boatCount[0];
            to_devil += boatCount[1];
        }
        // at start
        else
        {
            from_priest += boatCount[0];
            from_devil += boatCount[1];
        }
        // lose
        if ((from_priest < from_devil && from_priest > 0) || (to_priest < to_devil && to_priest > 0))
        {
            return 1;
        }
        // not finish
        return 0;
    }
}