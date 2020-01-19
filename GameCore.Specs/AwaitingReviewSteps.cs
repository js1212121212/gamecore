using TechTalk.SpecFlow;

namespace GameCore.Specs
{
    [Binding]
    [Scope(Tag = "awaitingReviewBeforeStartingWork")]
    public class AwaitingReviewSteps
    {
        [Given (".*")]
        [When (".*")]
        [Then (".*")]
        //using regex that is going to match anything
        public void Empty()
        {

        }
    }
}
