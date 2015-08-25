using System;
using System.Collections.Generic;
using System.Text;

namespace GameStateManagement
{
    public static class RandomGenerator
    {
        static Random random = new Random();
        
        public static Random getRandom()
        {
            return random;
        }

    }
}
