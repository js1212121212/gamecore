using System;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;
using TechTalk.SpecFlow.Assist;

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

        [Given(@"I have the following attributes")]
        public void GivenIHaveTheFollowingAttributes(Table table) {

            //use table data to create an instance of Player Attributes
            var attributes = table.CreateInstance<PlayerAttributes>();
            
            //var race = table.Rows.First(row => row["attribute"] == "Race")["value"];
            //var resistance = table.Rows.First(row => row["attribute"] == "Resistance")["value"];

            _player.Race = attributes.Race;
            _player.DamageResistance = attributes.Resistance;        
        }

        [Given(@"my character is set to (.*)")]
        public void GivenMyCharacterIsSetToHealer(CharacterClass characterClass) {
            _player.CharacterClass = characterClass;

        }

        [When(@"cast a healing spell")]
        public void WhenCaseAHealingSpell() {
            _player.CastHealingSpell();
        }



    }
}

