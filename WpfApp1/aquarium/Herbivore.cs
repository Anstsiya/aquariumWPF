using System;
using System.Windows.Controls;

namespace aquarium.aquarium
{
    class Herbivore : Fish
    {
        public Herbivore(int[] coords, string name, int age, bool isMale, int hungryLevel = 5, bool isPregnant = false, int pregnancyPeriod = 0) : base(coords, name, age, isMale, hungryLevel, isPregnant, pregnancyPeriod)
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
                    if (type == typeof(Seaweed))
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


        protected override void giveBirth(int currentRow, int currentCol, object[,] cells, Aquarium aquarium, Grid DynamicGrid)
        {
            base.giveBirth(currentRow, currentCol, cells, aquarium, DynamicGrid);

            bool[] variants = new bool[] { true, false };

            var freeRandomCell = getFreeCell(currentRow, currentCol, cells, aquarium.aquariumSizeRow, aquarium.aquariumSizeColumn);

            Herbivore baby = new Herbivore(new int[] { freeRandomCell[0], freeRandomCell[1] }, "Child of " + this.name, 1, variants[new Random().Next(2)]);

            baby.digestibilityLevel = new Random().NextDouble();

            cells[freeRandomCell[0], freeRandomCell[1]] = baby;

            Grid.SetRow(baby.gridElem, freeRandomCell[0]);
            Grid.SetColumn(baby.gridElem, freeRandomCell[1]);
            DynamicGrid.Children.Add(baby.gridElem);
        }

    }
}
