using System;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;
using TechTalk.SpecFlow.Assist;
using System.Collections.Generic;

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
            //var attributes = table.CreateInstance<PlayerAttributes>();

            //manually read the table data using methods in the table object
            //var race = table.Rows.First(row => row["attribute"] == "Race")["value"];
            //var resistance = table.Rows.First(row => row["attribute"] == "Resistance")["value"];

            dynamic attributes = table.CreateDynamicInstance();

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

        [Given(@"I have the following magical items")]
        public void GivenIHaveTheFollowingMagicalItems(Table table) {
            //weakly typed example
            //foreach (var row in table.Rows) {
            //    var name = row["item"];
            //    var value = row["value"];
            //    var power = row["power"];
            //    _player.MagicalItems.Add(new MagicalItem {
            //        Name = name,
            //        Value = int.Parse(value),
            //        Power = int.Parse(power)
            //    });
            //}

            //strongly typed example. this class is coming from production code
            //IEnumerable<MagicalItem> items = table.CreateSet<MagicalItem>();
            //_player.MagicalItems.AddRange(items);

            //dynamic equivalent
            //use dynamic set to get dynamic objects then loop through 
            IEnumerable<dynamic> items = table.CreateDynamicSet();
            foreach (var magicalItem in items) {
                _player.MagicalItems.Add(new MagicalItem {
                    Name = magicalItem.name,
                    Value = magicalItem.value,
                    Power = magicalItem.power
                });
            }

        }

        [Then(@"My total magical power should be (.*)")]
        public void ThenMyTotalMagicalPowerShouldBe(int expectedPower) {
            Assert.Equal(expectedPower, _player.MagicalPower);
        }

        [Given(@"I last slept (.* days ago)")]
        //capturing the entire phrase of '3 days ago'
        public void GivenILastSleptDaysAgo(DateTime lastSlept) {
            _player.LastSleepTime = lastSlept;
        }

        [When(@"I read a restore health scroll")]
        public void WhenIReadARestoreHealthScroll() {
            _player.ReadHealthScroll();
            
        }

    }
}

