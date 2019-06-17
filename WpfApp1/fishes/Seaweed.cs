
namespace aquarium.aquarium
{
    class Seaweed : Aquarium
    {

        public int size;

        protected int maxSize;

        Seaweed (int _size)
        {
            maxSize = 3;
            size = _size;
        }

        public void checkStatus ()
        {
            if ( size < maxSize)
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
