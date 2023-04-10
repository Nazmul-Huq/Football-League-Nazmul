using FootballLeague.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Util
{
    public abstract class FixtureGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        public static List<Game> createRound(in List<string> list, int round)
        {
            List<Game> games = new List<Game>();
            var midPosition = (list.Count) / 2;
            List<string> listWithoutLastTeam = list.SkipLast(1).ToList();
            var rotatedItems = Rotate(rotation: round - 1, items: listWithoutLastTeam);
            var firstGroup = rotatedItems.Take(midPosition).ToList();
            List<string> secondGroup = (list.TakeLast(1).Concat((rotatedItems.TakeLast(midPosition - 1)).Reverse())).ToList();
            if (round % 2 != 0)
            {
                var q = firstGroup.Zip(secondGroup).ToList();
                q.ForEach(x =>
                {
                    Game game = new Game(x.First, x.Second);
                    games.Add(game);
                });
            }
            else
            {
                var q = secondGroup.Zip(firstGroup).ToList();
                q.ForEach(x =>
                {
                    Game game = new Game(x.First, x.Second);
                    games.Add(game);
                });
            }
            return games;
        }

        /// <summary>
        /// Rotate a list by given rotation
        /// </summary>
        /// <param name="items"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        private static List<string> Rotate(List<string> items, int rotation)
        {
            return items.Skip(rotation).Concat(items.Take(rotation)).ToList();
        }
    }

} // namespace ends here
