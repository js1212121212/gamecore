using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace GameCore.Specs {
    //indicates SF to look inside of this class
    [Binding]
    public class CustomConversions {
        //can supply regex
        [StepArgumentTransformation(@"(\d+) days ago")]
        public DateTime DaysAgoTransformation(int daysAgo) {
            return DateTime.Now.Subtract(TimeSpan.FromDays(daysAgo));
        }

        //not using regex so not limiting step argument
        [StepArgumentTransformation]
        public IEnumerable<Weapon> WeaponsTranformation(Table table) {
            //convert table to IEnumerable object
            return table.CreateSet<Weapon>();
        }
        
    }
}
