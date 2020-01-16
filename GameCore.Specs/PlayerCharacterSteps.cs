using System;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;
using TechTalk.SpecFlow.Assist;
using System.Collections.Generic;

namespace GameCore.Specs {

    [Binding]
    public class PlayerCharacterSteps {

        private readonly PlayerCharacterStepsContext _context;

        public PlayerCharacterSteps (PlayerCharacterStepsContext context) {
            //when instantiated, context injected too
            //inject the context
            _context = context;
        }

        //[Given(@"I'm a new player")]
        //public void GivenImANewPlayer() {
        //    _context.Player = new PlayerCharacter();
        //}

        [When("I take (.*) damage")]
        public void WhenITakeDamage (int damage) {
            _context.Player.Hit(damage);
        }

        [Then(@"My health should now be (.*)")]
        public void ThenMyHealthShouldNowBe (int expectedHealth) {
            Assert.Equal(expectedHealth, _context.Player.Health);
        }

        [Then(@"I should be dead")]
        public void ThenIShouldBeDead() {
            Assert.Equal(0, _context.Player.Health);
            Assert.True(_context.Player.IsDead);
        }

        [Given(@"I have a damage resistance of (.*)")]
        public void GivenIHaveADamageResistanceOf(int damageResistance) {
            _context.Player.DamageResistance = damageResistance;        
        }

        [Given(@"I'm an Elf")]
        public void GivenIMAnElf() {
            _context.Player.Race = "Elf";
        }

        [Given(@"I have the following attributes")]
        public void GivenIHaveTheFollowingAttributes(Table table) {

            //use table data to create an instance of Player Attributes
            //var attributes = table.CreateInstance<PlayerAttributes>();

            //manually read the table data using methods in the table object
            //var race = table.Rows.First(row => row["attribute"] == "Race")["value"];
            //var resistance = table.Rows.First(row => row["attribute"] == "Resistance")["value"];

            dynamic attributes = table.CreateDynamicInstance();

            _context.Player.Race = attributes.Race;
            _context.Player.DamageResistance = attributes.Resistance; 
        }

        [Given(@"my character is set to (.*)")]
        public void GivenMyCharacterIsSetToHealer(CharacterClass characterClass) {
            _context.Player.CharacterClass = characterClass;
        }

        [When(@"cast a healing spell")]
        public void WhenCaseAHealingSpell() {
            _context.Player.CastHealingSpell();
        }

        [Given(@"I have the following magical items")]
        public void GivenIHaveTheFollowingMagicalItems(Table table) {
            //weakly typed example
            //foreach (var row in table.Rows) {
            //    var name = row["item"];
            //    var value = row["value"];
            //    var power = row["power"];
            //    _context.Player.MagicalItems.Add(new MagicalItem {
            //        Name = name,
            //        Value = int.Parse(value),
            //        Power = int.Parse(power)
            //    });
            //}

            //strongly typed example. this class is coming from production code
            //IEnumerable<MagicalItem> items = table.CreateSet<MagicalItem>();
            //_context.Player.MagicalItems.AddRange(items);

            //dynamic equivalent
            //use dynamic set to get dynamic objects then loop through 
            IEnumerable<dynamic> items = table.CreateDynamicSet();
            foreach (var magicalItem in items) {
                _context.Player.MagicalItems.Add(new MagicalItem {
                    Name = magicalItem.name,
                    Value = magicalItem.value,
                    Power = magicalItem.power
                });
            }

        }

        [Then(@"My total magical power should be (.*)")]
        public void ThenMyTotalMagicalPowerShouldBe(int expectedPower) {
            Assert.Equal(expectedPower, _context.Player.MagicalPower);
        }

        [Given(@"I last slept (.* days ago)")]
        //capturing the entire phrase of '3 days ago' and conversion class will convert it to DateTime
        public void GivenILastSleptDaysAgo(DateTime lastSlept) {
            _context.Player.LastSleepTime = lastSlept;
        }

        [When(@"I read a restore health scroll")]
        public void WhenIReadARestoreHealthScroll() {
            _context.Player.ReadHealthScroll();            
        }

        [Given(@"I have the following weapons")]
        public void GivenIHaveTheFollowingWeapons(IEnumerable<Weapon> weapons) {
            _context.Player.Weapons.AddRange(weapons);
        }

        [Then(@"My weapons should be worth (.*)")]
        public void ThenMyWeaponsShouldBeWorth(int value) {
            Assert.Equal(value, _context.Player.WeaponsValue);
        }

        [Given(@"I have an Amulet with a power of (.*)")]
        public void GivenIHaveAnAmuletWithAPowerOf(int power) {

            //Add amulet to player's magical items
            _context.Player.MagicalItems.Add(new MagicalItem {
                Name = "Amulet",
                Power = power
            });

            //Store the starting power so it can be retrieved in the Then step
            _context.StartingMagicalPower = power;

        }

        [When(@"I use a magical Amulet")]
        public void WhenIUseAMagicalAmulet() {

            _context.Player.UseMagicalItem("Amulet");

            //Player Character Instance.UseMagicalItem("Amulet");

        }

        [Then(@"the Amulet power should not be reduced")]
        public void ThenTheAmuletPowerShouldNotBeReduced() {
            
            int expectedPower;

            //get starting magical power from when step
            expectedPower = _context.StartingMagicalPower;

            //get the first item with the name of Amulet
            Assert.Equal(expectedPower, _context.Player.MagicalItems.First(item => item.Name == "Amulet").Power);

        }


    }
}

