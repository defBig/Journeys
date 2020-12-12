using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Journeys
{
    class ChallengeBuilder
    {
        private readonly char[] _validCommands = { 'R', 'L', 'F' };
        private readonly char[] _validDirections = { 'N', 'E', 'S', 'W', 'N' };
        private readonly Random _random;

        public ChallengeBuilder()
        {
            _random = new Random();
        }
        public Journey GenerateRandomJourney(int x, int y, char direction)
        {
            (int X, int Y, char Direction) Initial = (x, y, direction);
            int numberOfCommands = _random.Next(15);
            StringBuilder commandsBuilder = new StringBuilder();
            (int X, int Y, char Direction) Final = Initial;

            for (int i = 0; i < numberOfCommands; i++)
            {
                switch (_random.Next(3))
                {
                    case 0:
                        TurnRight(commandsBuilder, ref Final);
                        break;
                    case 1:
                        TurnLeft(commandsBuilder, ref Final);
                        break;
                    case 2:
                        GoFoward(commandsBuilder, ref Final);
                        break;
                }
            }
            return new Journey { Initial = Initial, Commands = commandsBuilder.ToString(), Final = Final };
        }

        private void GoFoward(StringBuilder commandsBuilder, ref (int X, int Y, char Direction) Final)
        {
            commandsBuilder.Append('F');
            switch (Final.Direction)
            {
                case 'N':
                    Final.Y++;
                    break;
                case 'E':
                    Final.X++;
                    break;
                case 'S':
                    Final.Y--;
                    break;
                case 'W':
                    Final.X--;
                    break;
            }
        }

        private void TurnLeft(StringBuilder commandsBuilder, ref (int X, int Y, char Direction) Final)
        {
            commandsBuilder.Append('L');
            for (int j = _validDirections.Length - 1; j > 0; j--)
            {
                if (_validDirections[j] == Final.Direction)
                {
                    Final.Direction = _validDirections[j - 1];
                    break;
                }
            }
        }

        private void TurnRight(StringBuilder commandsBuilder, ref (int X, int Y, char Direction) Final)
        {
            commandsBuilder.Append('R');
            for (int j = 0; j < _validDirections.Length - 1; j++)
            {
                if (_validDirections[j] == Final.Direction)
                {
                    Final.Direction = _validDirections[j + 1];
                    break;
                }
            }
        }
    }
}
