
using System.Windows.Controls;
using WpfApp1.aquarium;

namespace aquarium.aquarium
{
    class Rock : AquariumContent
    {
        public Rock(int[] _coords, string _name)
        {
            coords = _coords;
            name = _name;
            gridElem = new Label();
            gridElem.Content += "Name: " + name;
        }
    }
}
