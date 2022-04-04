using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX.Models
{
    public class Crash
    {
        [Key]
        [Required]
        public int CRASH_ID { get; set; }
        public string CRASH_DATETIME { get; set; }
        public string ROUTE { get; set; }
        public double MILEPOINT { get; set; }
        public string LAT_UTM_Y { get; set; }
        public string LONG_UTM_X { get; set; }
        public string CITY { get; set; }
        public string COUNTY_NAME { get; set; }
        public int CRASH_SEVERITY_ID { get; set; }
        public string WORK_ZONE_RELATED { get; set; }
        public bool PEDESTRIAN_INVOLVED { get; set; }
        public bool BICYCLIST_INVOLVED { get; set; }
        public bool MOTORCYCLE_INVOLVED { get; set; }
        public bool IMPROPER_RESTRAINT { get; set; }
        public bool UNRESTRAINED { get; set; }
        public bool DUI { get; set; }
        public bool INTERSECTION_RELATED { get; set; }
        public bool WILD_ANIMAL_RELATED { get; set; }
        public bool DOMESTIC_ANIMAL_RELATED { get; set; }
        public bool OVERTURN_ROLLOVER { get; set; }
        public bool COMMERCIAL_MOTOR_VEH_INVOLVED { get; set; }
        public bool TEENAGE_DRIVER_INVOLVED { get; set; }
        public bool OLDER_DRIVER_INVOLVED { get; set; }
        public bool NIGHT_DARK_CONDITION { get; set; }
        public bool SINGLE_VEHICLE { get; set; }
        public bool DISTRACTED_DRIVING { get; set; }
        public bool DROWSY_DRIVING { get; set; }
        public bool ROADWAY_DEPARTURE { get; set; }
        [NotMapped]
        public DateTime CRASH_DATE => DateTime.Parse(CRASH_DATETIME);
        [NotMapped]
        public bool WORK_ZONE_BOOL => WORK_ZONE_RELATED == "TRUE" ? true : false;
    }
}