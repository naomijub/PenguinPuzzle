using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PenguinPuzzle.Map
{
    public class MapReader
    {
        public string[] levels { get; set; }
        private int[] CurrentLevel { get; set; }

        public MapReader() {
            string path = "C:/Users/JAT/Desktop/Goof Troop/PenguinPuzzle/PenguinPuzzle/Content/level.txt";
            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist." + path);
            }
            else {
                levels = File.ReadAllLines(path);
                Console.WriteLine(levels[0]);
            }
        }

        public int[] returnCurrentLevel(int level) {
            CurrentLevel = new int[levels[level].Length];
            string str = levels[level];
            for (int i = 0; i < levels[level].Length; i++) {
                CurrentLevel[i] = Int32.Parse(str.Substring(i,1));
            }
            return CurrentLevel;
        }

        public int[] getCurrentLevel() {
            return CurrentLevel;
        }
    }
}
