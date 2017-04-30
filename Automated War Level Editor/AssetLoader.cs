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

        public static List<Unit> LoadUnits()
        {

            List<Unit> units = new List<Unit>();

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Unit));

            string[] files = Directory.GetFiles(Window.INSTALL_LOCATION + "content/units/");

            foreach (string file in files)
            {

                using (FileStream stream = new FileStream(file, FileMode.Open))
                {

                    Unit unit = (Unit)serializer.ReadObject(stream);

                    units.Add(unit);

                }

            }

            return units;

        }

        public static List<Bitmap> LoadUnitBitmaps(List<Unit> units)
        {

            List<Bitmap> bitmaps = new List<Bitmap>();

            foreach (Unit unit in units)
            {

                bitmaps.Add(new Bitmap(Window.INSTALL_LOCATION + "textures/units/" + unit.ID + "/editor.png"));

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
