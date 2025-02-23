﻿using SaveOurShip2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using RimworldMod;
using RimworldMod.VacuumIsNotFun;
using Verse;

namespace RimWorld
{
    class CompLongRangeMineralScannerSpaceAI : CompLongRangeMineralScannerSpace
    {
        public float Rate = 0.004f;
        new public bool CanUseNow
        {
            get
            {
                return false;
            }
        }

        public override void CompTick()
        {
            base.CompTick();
            if (Find.TickManager.TicksGame % 250 != 0 || !this.parent.Map.IsSpace() || !this.powerComp.PowerOn)
                return;

            float rate = Rate;
            if (mapComp.Cloaks.Any(c => c.active))
                rate /= 4;
            this.daysWorkingSinceLastMinerals += rate;
            float mtb = this.Props.mtbDays / 20;
            if (this.daysWorkingSinceLastMinerals >= this.Props.guaranteedToFindLumpAfterDaysWorking || Rand.MTBEventOccurs(mtb, 60000f, 59f))
            {
                this.FoundMinerals(null);
            }
        }
    }
}
