using System;

namespace HTML_Crawler_3._0.Data_Structures
{
    class WrapClass<T>
    {
        public int Depth;
        public T Value;

        public WrapClass(int depth, T value)
        {
            Depth = depth;
            Value = value;
        }
    }
}
