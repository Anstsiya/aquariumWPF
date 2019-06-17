using System;
using System.Collections.Generic;
using System.Windows.Controls;
using WpfApp1.aquarium;

namespace aquarium.aquarium
{
    abstract class Fish: AquariumContent
    {
        protected int age;
        protected bool isMale;
        protected bool isPregnant;
        protected int pregnancyPeriod;
        public double energyLevel;
        protected bool isAdult;
        public bool isChecked;
        public bool isMoved;

        //
        protected int maxAge;
        protected int adultAgeStart;
        protected int oldAgeStart;
        protected int maxEnergyLevel;
        protected double digestibilityLevel;
        protected int maxPregnancyPeriod;
        //

        protected List<int> positionAfterMove;

        protected Fish(int[] _coords, string _name, int _age, bool _isMale, int _energyLevel, bool _isPregnant, int _pregnancyPeriod)
        {
            coords = _coords;
            name = _name;
            maxAge = 20;
            adultAgeStart = 5;
            oldAgeStart = 15;
            maxPregnancyPeriod = 3;
            maxEnergyLevel = 5;
            
            age = _age;
            isMale = _isMale;
            isAdult = age >= adultAgeStart && age < oldAgeStart ? true : false;
            //hungryLevel = 5;
            energyLevel = _energyLevel;
            pregnancyPeriod = _pregnancyPeriod;
            isPregnant = _isPregnant;
            // pregnancyPeriod = 0;
            //isPregnant = false;
            digestibilityLevel = new Random().NextDouble();
            
            isChecked = false;
            isMoved = false;

            gridElem = new Label();
            gridElem.Content += "Name: " + name;
            gridElem.Content += "\nAge: " + age;
            var sex = isMale ? "Male" : "Female";
            gridElem.Content += "\nSex: " + sex;
            gridElem.Content += "\nEnergy level: " + energyLevel;
        }

        public void  checkFish (int currentRow, int currentCol, object[,] cells, Aquarium aquarium, Grid DynamicGrid)
        {
            if (!isChecked)
            {
                //check age ranks 
                if (age < maxAge)
                {
                    age++;
                    if (age >= adultAgeStart && age < oldAgeStart)
                    {
                        isAdult = true;
                    }
                }
                else
                {
                    die(currentRow, currentCol, cells, DynamicGrid);
                }

                //check pregnancy 
                if (isMale == false && isPregnant == true)
                {
                    if (pregnancyPeriod == maxPregnancyPeriod)
                    {
                        giveBirth(currentRow, currentCol, cells, aquarium, DynamicGrid);
                    }
                    else
                    {
                        pregnancyPeriod++;
                    }
                }

                //check hungry level
                if (energyLevel == 0)
                {
                    die(currentRow, currentCol, cells, DynamicGrid);
                } else
                {
                    if (energyLevel < 1)
                    {
                        energyLevel = 0;
                    } else
                    {
                        energyLevel--;
                    }
                    
                }

                isChecked = true;

                string content = "";
                content += "Name: " + name;
                content += "\nAge: " + age;
                var sex = isMale ? "Male" : "Female";
                content += "\nSex: " + sex;
                content += "\nEnergy level: " + energyLevel;

                gridElem.Content = content;
            }
        }
        
        public virtual void lifeCycle (int currentRow, int currentCol, object[,] cells, Aquarium aquarium, Grid DynamicGrid)
        {
            var c = cells;
            positionAfterMove = new List<int>();
            if (!isMoved)
            {
                move(currentRow, currentCol, c, aquarium, DynamicGrid);

                
                int rowPosition = positionAfterMove.Count != 0 ? positionAfterMove[0] : currentRow;
                int colPosition = positionAfterMove.Count != 0 ? positionAfterMove[1] : currentCol;

                c = cells;
                checkAria(rowPosition, colPosition, c, aquarium, DynamicGrid);

                isMoved = true;
            }
            
        }

        protected virtual void checkAria(int currentRow, int currentCol, object[,] cells, Aquarium aquarium, Grid DynamicGrid)
        {

        }

        protected void move (int currentRow, int currentCol, object [,] cells, Aquarium aquarium, Grid DynamicGrid)
        {
            var nextStep = getFreeCell(currentRow, currentCol, cells, aquarium.aquariumSizeRow, aquarium.aquariumSizeColumn);
            if (nextStep != null)
            {
                
                cells[nextStep[0], nextStep[1]] = this;
                cells[currentRow, currentCol] = null;
                positionAfterMove.Add(nextStep[0]);
                positionAfterMove.Add(nextStep[1]);

                DynamicGrid.Children.Remove(gridElem);
                Grid.SetRow(gridElem, nextStep[0]);
                Grid.SetColumn(gridElem, nextStep[1]);
                DynamicGrid.Children.Add(gridElem);

            }
            
        }

        protected void eat (double digestibilityLevel, double mass)
        {
            var amountOfEnergy = digestibilityLevel * mass;
            var energyInLack = maxEnergyLevel - energyLevel;
            var resultOfEnergy = amountOfEnergy <= energyInLack ? amountOfEnergy : energyInLack;
            energyLevel = energyLevel + resultOfEnergy;

            string content = "";
            content += "Name: " + name;
            content += "\nAge: " + age;
            var sex = isMale ? "Male" : "Female";
            content += "\nSex: " + sex;
            content += "\nEnergy level: " + energyLevel;

            gridElem.Content = content;
        }

        protected void grow ()
        {
            age++;
        }

        public void die (int currentRow, int currentCol, object[,] cells, Grid DynamicGrid)
        {
            cells[currentRow, currentCol] = null;
            DynamicGrid.Children.Remove(gridElem);
        }

        protected virtual void giveBirth (int currentRow, int currentCol, object[,] cells, Aquarium aquarium, Grid DynamicGrid)
        {
            isPregnant = false;
            pregnancyPeriod = 0;
            
        }

        protected void makeChild ()
        {
            isPregnant = true;
            pregnancyPeriod = 1;
        }

        protected int[] getFreeCell (int currentRow, int currentCol, object[,] cells, int aquariumSizeRow, int aquariumSizeColumn)
        {
            var cellsAround = getCellsAround(currentRow, currentCol, aquariumSizeRow, aquariumSizeColumn);

            List<int[]> stepVariants = new List<int[]>();

            foreach (int[] cell in cellsAround)
            {
                if (cells[cell[0], cell[1]] == null)
                {
                    stepVariants.Add(cell);
                }
            }
            var res = stepVariants.Count == 0 ? null : stepVariants[new Random().Next(0, stepVariants.Count)];
            return res;
        }
        
        protected List<int[]> getCellsAround (int currentRow, int currentCol, int aquariumSizeRow, int aquariumSizeColumn)
        {
            List<int[]> cellsAround = new List<int[]>();
            if (currentRow - 1 >= 0 )
            {
                cellsAround.Add(new int[] { currentRow - 1, currentCol });
            }
            if (currentCol + 1 <= aquariumSizeColumn - 1 )
            {
                cellsAround.Add(new int[] { currentRow, currentCol + 1 });
            }
            if (currentRow + 1 <= aquariumSizeRow - 1 )
            {
                cellsAround.Add(new int[] { currentRow + 1, currentCol });
            }
            if (currentCol - 1 >= 0 )
            {
                cellsAround.Add(new int[] { currentRow, currentCol - 1 });
            }

            return cellsAround;
        }
    }
}
