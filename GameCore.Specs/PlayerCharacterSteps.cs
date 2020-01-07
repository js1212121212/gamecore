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

        [When("I take (.*) damage")]
        public void WhenITakeDamage (int damage) {
            _player.Hit(damage);
        }

        [Then(@"My health should now be (.*)")]
        public void ThenMyHealthShouldNowBe (int expectedHealth) {
            Assert.Equal(expectedHealth, _player.Health);
        }

        [Then(@"I should be dead")]
        public void ThenIShouldBeDead() {
            Assert.Equal(0, _player.Health);
            Assert.True(_player.IsDead);
        }

        [Given(@"I have a damage resistance of (.*)")]
        public void GivenIHaveADamageResistanceOf(int damageResistance) {
            _player.DamageResistance = damageResistance;
        

        }

        [Given(@"I'm an Elf")]
        public void GivenIMAnElf() {
            _player.Race = "Elf";
        }


    }
}

