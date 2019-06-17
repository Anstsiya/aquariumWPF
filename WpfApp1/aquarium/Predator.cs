using System;
using System.Windows.Controls;

namespace aquarium.aquarium
{
    class Predator : Fish
    {

        public Predator(int[] coords, string name, int age, bool isMale, int hungryLevel = 5, bool isPregnant = false, int pregnancyPeriod = 0) : base (coords, name, age, isMale, hungryLevel, isPregnant, pregnancyPeriod)
        {

        }
        
        protected override void checkAria(int currentRow, int currentCol, object[,] cells, Aquarium aquarium, Grid DynamicGrid)
        {
            var cellsAround = getCellsAround(currentRow, currentCol, aquarium.aquariumSizeRow, aquarium.aquariumSizeColumn);

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
                            if (!this.isMale && !isPregnant)
                            {
                                makeChild();
                            } 
                            else if (!foundedFish.isMale && !foundedFish.isPregnant)
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
                            foundedFish.die(cell[0], cell[1], cells, DynamicGrid);
                        }
                       
                    }
                }
            }
        }

        protected override void giveBirth(int currentRow, int currentCol, object[,] cells, Aquarium aquarium, Grid DynamicGrid)
        {
            base.giveBirth(currentRow, currentCol, cells, aquarium, DynamicGrid);

            bool[] variants = new bool[] { true, false };

            var freeRandomCell = getFreeCell(currentRow, currentCol, cells, aquarium.aquariumSizeRow, aquarium.aquariumSizeColumn);

            Predator baby = new Predator(new int[] { freeRandomCell[0], freeRandomCell[1] }, "Child of " + this.name , 1, variants[new Random().Next(2)]);

            baby.digestibilityLevel = new Random().NextDouble();
            baby.isChecked = true;

            cells[freeRandomCell[0], freeRandomCell[1]] = baby;

            Grid.SetRow(baby.gridElem, freeRandomCell[0]);
            Grid.SetColumn(baby.gridElem, freeRandomCell[1]);
            DynamicGrid.Children.Add(baby.gridElem);
        }
        
        
    }
}
