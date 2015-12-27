using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Automated_War_Level_Editor
{

    public static class AssetLoader
    {

        public static List<Wall> LoadWalls()
        {

            List<Wall> walls = new List<Wall>();

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Wall));

            string[] files = Directory.GetFiles(Window.INSTALL_LOCATION + "content/walls/");

            foreach(string file in files)
            {

                using (FileStream stream = new FileStream(file, FileMode.Open))
                {

                    Wall wall = (Wall)serializer.ReadObject(stream);

                    walls.Add(wall);

                }

            }

            return walls;

        }

        public static List<Bitmap> LoadWallBitmaps(List<Wall> walls)
        {

            List<Bitmap> bitmaps = new List<Bitmap>();

            foreach(Wall wall in walls)
            {

                bitmaps.Add(new Bitmap(Window.INSTALL_LOCATION + "textures/walls/" + wall.ID + ".png"));

            }

            return bitmaps;

        }

        public static List<Tank> LoadTanks()
        {

            List<Tank> tanks = new List<Tank>();

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Tank));

            string[] files = Directory.GetFiles(Window.INSTALL_LOCATION + "content/units/tanks/");

            foreach (string file in files)
            {

                using (FileStream stream = new FileStream(file, FileMode.Open))
                {

                    Tank tank = (Tank)serializer.ReadObject(stream);

                    tanks.Add(tank);

                }

            }

            return tanks;

        }

        public static List<Bitmap> LoadTankBitmaps(List<Tank> tanks)
        {

            List<Bitmap> bitmaps = new List<Bitmap>();

            foreach (Tank tank in tanks)
            {

                bitmaps.Add(new Bitmap(Window.INSTALL_LOCATION + "textures/units/tanks/" + tank.ID + ".png"));

            }

            return bitmaps;

        }

        public static Level OpenLevel(string path)
        {

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Level));
                return (Level)serializer.ReadObject(stream);

            }

        }

        public static void SaveLevel(Level level, string path)
        {

            using (MemoryStream stream = new MemoryStream())
            {

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Level));

                serializer.WriteObject(stream, level);

                File.WriteAllText(path, Encoding.UTF8.GetString(stream.ToArray()));

            }

        }

    }

}
