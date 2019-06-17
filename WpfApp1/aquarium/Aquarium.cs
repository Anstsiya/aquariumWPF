
using System.Collections.Generic;

namespace aquarium.aquarium
{
    class Aquarium
    {
        public object[,] cells;
        public int aquariumSizeRow;
        public int aquariumSizeColumn;
        public List<Predator> Predators;
        public List<Herbivore> Herbivores;
        public List<Rock> Rocks;
        public List<Seaweed> Seaweeds;

        public Aquarium (int _aquariumSizeRow, int _aquariumSizeColumn, List<Predator> _predators,
            List<Herbivore> _herbivores, List<Rock> _rocks, List<Seaweed> _seaweeds)
        {
            aquariumSizeRow = _aquariumSizeRow;
            aquariumSizeColumn = _aquariumSizeColumn;
            cells = new object[aquariumSizeRow, aquariumSizeColumn];
            Predators = _predators;
            Herbivores = _herbivores;
            Rocks = _rocks;
            Seaweeds = _seaweeds;

            foreach(var prd in Predators)
            {
                var row = prd.coords[0];
                var column = prd.coords[1];
                if (row >= 0 && row <= aquariumSizeRow - 1
                    && column >= 0 && column <= aquariumSizeColumn - 1
                    && cells[row, column] == null)
                {
                    cells[row, column] = prd;
                }
            }


            foreach (var hrb in Herbivores)
            {
                var row = hrb.coords[0];
                var column = hrb.coords[1];
                if (row >= 0 && row <= aquariumSizeRow - 1
                    && column >= 0 && column <= aquariumSizeColumn - 1
                    && cells[row, column] == null)
                {
                    cells[row, column] = hrb;
                }
            }


            foreach (var swd in Seaweeds)
            {
                var row = swd.coords[0];
                var column = swd.coords[1];
                if (row >= 0 && row <= aquariumSizeRow - 1
                    && column >= 0 && column <= aquariumSizeColumn - 1
                    && cells[row, column] == null)
                {
                    cells[row, column] = swd;
                }
            }


            foreach (var rck in Rocks)
            {
                var row = rck.coords[0];
                var column = rck.coords[1];
                if (row >= 0 && row <= aquariumSizeRow - 1
                    && column >= 0 && column <= aquariumSizeColumn - 1
                    && cells[row, column] == null)
                {
                    cells[row, column] = rck;
                }
            }
        }
        
    }
}
