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
            (int X, int Y, char Direction) Final = Initial;
            int numberOfCommands = _random.Next(15);
            StringBuilder allCommands = new StringBuilder();
            
            for (int i = 0; i < numberOfCommands; i++)
            {
                allCommands.Append(_validCommands[_random.Next(_validCommands.Length)]);
            }
            string effectiveCommands = Simplify(allCommands.ToString());

            for (int i = 0; i < effectiveCommands.Length; i++)
            {
                switch (effectiveCommands[i])
                {
                    case 'R':
                        TurnRight(ref Final);
                        break;
                    case 'L':
                        TurnLeft(ref Final);
                        break;
                    case 'F':
                        GoFoward(ref Final);
                        break;
                }
            }
            return new Journey { Initial = Initial, Commands = allCommands.ToString(), Final = Final };
        }

        private string Simplify(string commands)
        {
            StringBuilder effectiveCommands = new StringBuilder();
            int turnsRight = 0;
            foreach (char command in commands)
            {
                switch (command)
                {
                    case 'R':
                        turnsRight++;
                        break;
                    case 'L':
                        turnsRight--;
                        break;
                    case 'F':
                        turnsRight = ApplyTurns(effectiveCommands, turnsRight);
                        effectiveCommands.Append('F');
                        turnsRight = 0;
                        break;
                }
            }
            turnsRight = ApplyTurns(effectiveCommands, turnsRight);
            return effectiveCommands.ToString();
        }

        private static int ApplyTurns(StringBuilder effectiveCommands, int turnsRight)
        {
            if (turnsRight > 0)
            {
                while (turnsRight-- > 0)
                {
                    effectiveCommands.Append('R');
                }
            }
            else if (turnsRight < 0)
            {
                while (turnsRight++ < 0)
                {
                    effectiveCommands.Append('L');
                }
            }

            return turnsRight;
        }

        private void GoFoward(ref (int X, int Y, char Direction) Final)
        {
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

        private void TurnLeft(ref (int X, int Y, char Direction) Final)
        {
            for (int j = _validDirections.Length - 1; j > 0; j--)
            {
                if (_validDirections[j] == Final.Direction)
                {
                    Final.Direction = _validDirections[j - 1];
                    break;
                }
            }
        }

        private void TurnRight(ref (int X, int Y, char Direction) Final)
        {
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
