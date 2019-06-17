
namespace aquarium.aquarium
{
    class Aquarium
    {
        public Aquarium[,] cells;
        public int aquariumSizeRow;
        public int aquariumSizeColumn;

        public Aquarium ()
        {
            aquariumSizeRow = 6;
            aquariumSizeColumn = 5;
            cells = new Aquarium[aquariumSizeRow, aquariumSizeColumn];
        }
        
    }
}
