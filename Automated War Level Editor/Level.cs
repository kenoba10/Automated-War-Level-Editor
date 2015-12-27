using System;
using System.Runtime.Serialization;

namespace Automated_War_Level_Editor
{

    [DataContract]
    public class Position
    {

        public Position()
        {

        }

        public Position(int x, int y)
        {

            X = x;
            Y = y;

        }

        [DataMember(Name ="x")]
        public int X
        {

            get;
            set;

        }

        [DataMember(Name ="y")]
        public int Y
        {

            get;
            set;

        }

    }

    [DataContract]
    public class Level
    {

        public Level()
        {

        }

        public Level(string title, string story, int width)
        {

            Title = title;
            Story = story;

            StartingPositions = new Position[8];

            for(int i = 0; i < 8; i++)
            {

                StartingPositions[i] = new Position(width - 1, width - 1);

            }

            Tiles = new string[width][];

            for(int i = 0; i < width; i++)
            {

                Tiles[i] = new string[width];

                for(int j = 0; j < width; j++)
                {

                    Tiles[i][j] = "";

                }

            }

        }

        [DataMember(Name ="title")]
        public string Title
        {

            get;
            set;

        }

        [DataMember(Name ="story")]
        public string Story
        {

            get;
            set;

        }

        [DataMember(Name ="startingPositions")]
        public Position[] StartingPositions
        {

            get;
            set;

        }

        [DataMember(Name ="tiles")]
        public string[][] Tiles
        {

            get;
            set;

        }

    }

}
