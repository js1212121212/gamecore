using System;
using TechTalk.SpecFlow;
using Xunit;

namespace GameCore.Specs {

    [Binding]
    public class PlayerCharacterSteps {
        private PlayerCharacter _player;

        [Given(@"I'm a new player")]
        public void GivenImANewPlayer() {
            _player = new PlayerCharacter();
        }

        [When(@"I take zero damage")]
        public void WhenITakeZeroDamage() {
            _player.Hit(0);
        }

        [Then(@"My health should now be 100")]
        public void ThenMyHealthShouldNowBe100() {
            Assert.Equal(100, _player.Health);
        }


        [When(@"I take 40 damage")]
        public void WhenITake40Damage() {
            _player.Hit(40);
        }

        [Then(@"My health should now be 60")]
        public void ThenMyHealthShouldNowBe60() {
            Assert.Equal(60, _player.Health);
        }

        [When(@"I take 100 damage")]
        public void WhenITake100Damage() {
            _player.Hit(100);
        }

        [Then(@"I should be dead")]
        public void ThenIShouldBeDead() {
            Assert.Equal(0, _player.Health);
            Assert.True(_player.IsDead);
        }


    }
}

