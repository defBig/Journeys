using System;

namespace Journeys
{
    class Program
    {
        
        public static void Main(string[] args)
        {
            //1 1 E
            //RFRFRFRF
            //1 1 E

            //3 2 N
            //FRRFLLFFRRFLL
            //3 3 N

            //0 3 W
            //LLFFFLFLFL
            //2 4 S
            ChallengeBuilder challengeBuilder = new ChallengeBuilder();
            Console.WriteLine(challengeBuilder.GenerateRandomJourney(1, 1, 'E'));
        }
    }
}
