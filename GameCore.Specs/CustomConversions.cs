using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace GameCore.Specs {
    //indicates SF to look inside of this class
    [Binding]
    public class CustomConversions {
        //can supply regex
        [StepArgumentTransformation(@"(\d+) days ago")]
        public DateTime DaysAgoTransformation(int daysAgo) {
            return DateTime.Now.Subtract(TimeSpan.FromDays(daysAgo));
        }
        
    }
}
