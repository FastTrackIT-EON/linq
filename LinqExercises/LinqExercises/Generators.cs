using System.Collections.Generic;

namespace LinqExercises
{
    public static class Generators
    {
        public static IEnumerable<int> GenerateAllNumbers()
        {
            int counter = 0;
            while (true)
            {
                yield return counter;
                counter++;
            }
        }
    }
}
