using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITSPublicApp.Domain.Models
{
    public class JobDemand
    {
        public int JobDemandID { get; set; }        
        public bool IsStanding { get; set; }        
        public bool IsWalking { get; set; }        
        public bool IsWorkATHeightOrClimb { get; set; }        
        public bool IsExtendedOrProlonged { get; set; }        
        public bool IsVocationalDriving { get; set; }        
        public bool IsDrivingLGVOrPCVs { get; set; }        
        public bool IsDrivingForkliftTrucks { get; set; }        
        public bool IsWorkWithChemials { get; set; }        
        public bool IsWorkBiologicalOrChemical { get; set; }        
        public bool IsWorkWithSkinIrritants { get; set; }        
        public bool IsWorkWithDengerousMachinery { get; set; }        
        public bool IsNightWork { get; set; }        
        public bool IsShiftWork { get; set; }        
        public bool IsWorkInConfinedSpaces { get; set; }        
        public bool IsWorkWithDustOrFumes { get; set; }        
        public bool IsLiftOrCarryHeavyItems { get; set; }        
        public bool IsWorkWithComputerOrScreens { get; set; }        
        public bool IsWorkTowardsTagetOrPressuredsituation { get; set; }        
        public bool IsWorkWithAdultOrChildren { get; set; }        
        public bool IsHealthCareWorker { get; set; }        
        public bool IsOccasionalOverseasTravel { get; set; }        
        public bool IsOutsideWork { get; set; }        
        public bool IsNoisedHarzardArea { get; set; }        
        public bool IsHandlingFood { get; set; }        
        public string FreeText { get; set; }
    }
}
