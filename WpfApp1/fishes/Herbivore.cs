using System;

namespace aquarium.aquarium
{
    class Herbivore : Fish
    {
        public Herbivore(int age, bool isMale, int hungryLevel = 5, bool isPregnant = false, int pregnancyPeriod = 0) : base(age, isMale, hungryLevel, isPregnant, pregnancyPeriod)
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
                        var foundedFish = (Herbivore)obj;
                        if (this.isMale != foundedFish.isMale)
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
                    else if (type == typeof(Seaweed))
                    {
                        var foundedObj = (Seaweed)obj;
                        if (energyLevel < maxEnergyLevel)
                        {
                            eat(digestibilityLevel, foundedObj.size);
                            foundedObj.decreaseSize();
                        }

                    }
                }
            }
        }

        protected override void giveBirth(int currentRow, int currentCol, Aquarium[,] cells)
        {
            base.giveBirth(currentRow, currentCol, cells);

            bool[] variants = new bool[] { true, false };

            Herbivore baby = new Herbivore(1, variants[new Random().Next(2)]);

            var freeRandomCell = getFreeCell(currentRow, currentCol, cells);

            cells[freeRandomCell[0], freeRandomCell[1]] = baby;
        }

    }
}
