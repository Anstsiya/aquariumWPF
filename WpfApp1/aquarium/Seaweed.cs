
using System.Windows.Controls;
using WpfApp1.aquarium;

namespace aquarium.aquarium
{
    class Seaweed : AquariumContent
    {

        public int size;
        protected int maxSize;

        public Seaweed (int[] _coords, string _name, int _size)
        {
            coords = _coords;
            name = _name;
            maxSize = 3;
            size = _size;
            gridElem = new Label();
            gridElem.Content += "Name: " + name;
            gridElem.Content += "\nSize: " + size;
            
        }

        public void checkStatus ()
        {
            if (size < maxSize)
            {
                grow();
            }
        }

        protected void grow ()
        {
            if (size < maxSize)
            {
                size++;
            }
        }

        public void decreaseSize ()
        {
            size = 0;
        }
    }
}
