using System;
using System.Collections.Generic;

namespace aquarium.aquarium
{
    abstract class Fish : Aquarium
    {
        protected int age;
        protected bool isMale;
        protected bool isPregnant;
        protected int pregnancyPeriod;
        public double energyLevel;
        protected bool isAdult;
        protected bool isChecked;

        //
        protected int maxAge;
        protected int adultAgeStart;
        protected int oldAgeStart;
        protected int maxEnergyLevel;
        protected double digestibilityLevel;
        protected int maxPregnancyPeriod;
        //

        protected List<int> positionAfterMove;

        protected Fish(int _age, bool _isMale, int _hungryLevel, bool _isPregnant, int _pregnancyPeriod)
        {
            maxAge = 20;
            adultAgeStart = 5;
            oldAgeStart = 15;
            maxPregnancyPeriod = 3;
            maxEnergyLevel = 5;
            

            age = _age;
            isMale = _isMale;
            isAdult = age >= adultAgeStart && age < oldAgeStart ? true : false;
            //hungryLevel = 5;
            energyLevel = _hungryLevel;
            pregnancyPeriod = _pregnancyPeriod;
            isPregnant = _isPregnant;
            // pregnancyPeriod = 0;
            //isPregnant = false;
            digestibilityLevel = new Random().Next(0, 1);
            positionAfterMove = new List<int>();
            isChecked = false;
        }

        public void  checkFish (int currentRow, int currentCol, Aquarium[,] cells)
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
                    die(currentRow, currentCol, cells);
                }

                //check pregnancy 
                if (isMale == false && isPregnant == true)
                {
                    if (pregnancyPeriod == maxPregnancyPeriod)
                    {
                        giveBirth(currentRow, currentCol, cells);
                    }
                    else
                    {
                        pregnancyPeriod++;
                    }
                }

                //check hungry level
                if (energyLevel == 0)
                {
                    die(currentRow, currentCol, cells);
                }

                isChecked = true;
            }
        }
        
        public virtual void lifeCycle (int currentRow, int currentCol, Aquarium[,] cells)
        {
            var c = cells;
            move(currentRow, currentCol, c);

            int rowPosition = positionAfterMove != null ? positionAfterMove[0] : currentRow;
            int colPosition = positionAfterMove != null ? positionAfterMove[1] : currentCol;

            checkAria(rowPosition, colPosition, c);
        }

        protected virtual void checkAria(int currentRow, int currentCol, Aquarium[,] cells)
        {

        }

        protected void move (int currentRow, int currentCol, Aquarium [,] cells)
        {
            var nextStep = getFreeCell(currentRow, currentCol, cells);
            if (nextStep != null)
            {
                cells[nextStep[0], nextStep[1]] = this;
                cells[currentRow, currentCol] = null;
                positionAfterMove.Add(nextStep[0]);
                positionAfterMove.Add(nextStep[1]);
            }
            
        }

        protected void eat (double digestibilityLevel, double mass)
        {
            var amountOfEnergy = digestibilityLevel * mass;
            var energyInLack = maxEnergyLevel - energyLevel;
            var resultOfEnergy = amountOfEnergy <= energyInLack ? amountOfEnergy : energyInLack;
            energyLevel = energyLevel + resultOfEnergy;
        }

        protected void grow ()
        {
            age++;
        }

        public void die (int currentRow, int currentCol, Aquarium[,] cells)
        {
            cells[currentRow, currentCol] = null;
        }

        protected virtual void giveBirth (int currentRow, int currentCol, Aquarium[,] cells)
        {
            isPregnant = false;
            pregnancyPeriod = 0;
            
        }

        protected void makeChild ()
        {
            isPregnant = true;
            pregnancyPeriod = 1;
        }

        protected int[] getFreeCell (int currentRow, int currentCol, Aquarium[,] cells)
        {
            var cellsAround = getCellsAround(currentRow, currentCol);

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
        
        protected List<int[]> getCellsAround (int currentRow, int currentCol)
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
