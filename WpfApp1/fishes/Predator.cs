using System;

namespace aquarium.aquarium
{
    class Predator : Fish
    {

        public Predator(int age, bool isMale, int hungryLevel = 5, bool isPregnant = false, int pregnancyPeriod = 0) : base (age, isMale, hungryLevel, isPregnant, pregnancyPeriod)
        {

        }
        
        protected override void checkAria(int currentRow, int currentCol, Aquarium[,] cells)
        {
            var cellsAround = getCellsAround(currentRow, currentCol);

            foreach (int[] cell in cellsAround)
            {
                var obj = cells[cell[0], cell[1]];
                if (obj != null)
                {
                    var type = obj.GetType();
                    if (type == this.GetType())
                    {
                        var foundedFish = (Predator)obj;
                        if (this.isMale != foundedFish.isMale && isAdult && foundedFish.isAdult)
                        {
                            if (!this.isMale)
                            {
                                makeChild();
                            } 
                            else if (!foundedFish.isMale)
                            {
                                foundedFish.makeChild();
                            }
                        }
                    }
                    if (type == typeof(Herbivore))
                    {
                        var foundedFish = (Herbivore)obj;
                        if (energyLevel < maxEnergyLevel)
                        {
                            eat(digestibilityLevel, foundedFish.energyLevel);
                            foundedFish.die(cell[0], cell[1], cells);
                        }
                       
                    }
                }
            }
        }

        protected override void giveBirth(int currentRow, int currentCol, Aquarium[,] cells)
        {
            base.giveBirth(currentRow, currentCol, cells);

            bool[] variants = new bool[] { true, false };

            Predator baby = new Predator(1, variants[new Random().Next(2)]);

            baby.digestibilityLevel = new Random().Next(0, 1);

            var freeRandomCell = getFreeCell(currentRow, currentCol, cells);

            cells[freeRandomCell[0], freeRandomCell[1]] = baby;
        }
    }
}
