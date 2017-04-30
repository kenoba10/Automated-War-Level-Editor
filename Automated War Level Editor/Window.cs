using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Automated_War_Level_Editor
{

    public partial class Window : Form
    {

        public static readonly string INSTALL_LOCATION = "./";

        public static readonly float WIDTH = 618.0f;

        private List<Wall> walls;
        private List<Unit> units;

        private List<Bitmap> wallBitmaps;
        private List<Bitmap> unitBitmaps;

        private Dictionary<string, Wall> wallDictionary;
        private Dictionary<string, Unit> unitDictionary;

        private List<string> undoActions;
        private List<string> redoActions;

        private string file;
        private bool saved;

        public Window()
        {

            Level = null;

            InitializeComponent();

            Bitmap bitmap = new Bitmap(map.Width, map.Height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {

                g.Clear(Color.FromArgb(64, 32, 0));

            }

            map.Image = bitmap;

            LoadStartingPositions();
            LoadWalls();
            LoadUnits();

            undoActions = new List<string>();
            redoActions = new List<string>();

            file = "";
            saved = true;

            Application.ApplicationExit += new EventHandler(OnExit);

        }

        private void LoadStartingPositions()
        {

            listBox1.Items.Add("Starting Position 1");
            listBox1.Items.Add("Starting Position 2");
            listBox1.Items.Add("Starting Position 3");
            listBox1.Items.Add("Starting Position 4");
            listBox1.Items.Add("Starting Position 5");
            listBox1.Items.Add("Starting Position 6");
            listBox1.Items.Add("Starting Position 7");
            listBox1.Items.Add("Starting Position 8");

        }

        private void LoadWalls()
        {

            walls = AssetLoader.LoadWalls();
            wallBitmaps = AssetLoader.LoadWallBitmaps(walls);
            wallDictionary = new Dictionary<string, Wall>();

            foreach(Wall wall in walls)
            {

                wallDictionary.Add(wall.ID, wall);
                listBox2.Items.Add(wall.Name + " Wall");

            }

        }

        private void LoadUnits()
        {

            units = AssetLoader.LoadUnits();
            unitBitmaps = AssetLoader.LoadUnitBitmaps(units);
            unitDictionary = new Dictionary<string, Unit>();

            foreach (Unit unit in units)
            {

                unitDictionary.Add(unit.ID, unit);
                listBox3.Items.Add(unit.Name);

            }

        }

        private void OnNewClicked(object sender, EventArgs e)
        {

            if (Level != null && !saved)
            {

                DialogResult result = MessageBox.Show("Would you like to save your current level?", "Save", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {

                    if (file == "")
                    {

                        SaveFileDialog dialog = new SaveFileDialog();

                        dialog.InitialDirectory = "./";
                        dialog.Filter = "JSON|*.json";

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {

                            AssetLoader.SaveLevel(Level, dialog.FileName);

                            file = dialog.FileName;

                        }

                    }
                    else
                    {

                        AssetLoader.SaveLevel(Level, file);

                    }

                }

                if(result != DialogResult.Cancel)
                {

                    WindowLevelInfo level = new WindowLevelInfo(this, true);

                    if (level.ShowDialog() == DialogResult.OK)
                    {

                        UpdateMap();

                        undoActions.Clear();
                        redoActions.Clear();

                        saved = false;

                    }

                }

            }
            else
            {

                WindowLevelInfo level = new WindowLevelInfo(this, true);

                if (level.ShowDialog() == DialogResult.OK)
                {

                    UpdateMap();

                    undoActions.Clear();
                    redoActions.Clear();

                    saved = false;

                }

            }
            
        }

        private void OnOpenClicked(object sender, EventArgs e)
        {

            if (Level != null && !saved)
            {

                DialogResult result = MessageBox.Show("Would you like to save your current level?", "Save", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {

                    if (file == "")
                    {

                        SaveFileDialog dialog = new SaveFileDialog();

                        dialog.InitialDirectory = "./";
                        dialog.Filter = "JSON|*.json";

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {

                            AssetLoader.SaveLevel(Level, dialog.FileName);

                            file = dialog.FileName;

                        }

                    }
                    else
                    {

                        AssetLoader.SaveLevel(Level, file);

                    }

                }

                if (result != DialogResult.Cancel)
                {

                    OpenFileDialog dialog = new OpenFileDialog();

                    dialog.InitialDirectory = "./";
                    dialog.Filter = "JSON|*.json";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {

                        Level = AssetLoader.OpenLevel(dialog.FileName);

                        file = dialog.FileName;

                        UpdateMap();

                        undoActions.Clear();
                        redoActions.Clear();

                        saved = true;

                    }

                }

            }
            else
            {

                OpenFileDialog dialog = new OpenFileDialog();

                dialog.InitialDirectory = "./";
                dialog.Filter = "JSON|*.json";

                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    Level = AssetLoader.OpenLevel(dialog.FileName);

                    file = dialog.FileName;

                    UpdateMap();

                    undoActions.Clear();
                    redoActions.Clear();

                    saved = true;

                }

            }

        }

        private void OnSaveClicked(object sender, EventArgs e)
        {

            if (Level != null)
            {

                if (file == "")
                {

                    SaveFileDialog dialog = new SaveFileDialog();

                    dialog.InitialDirectory = "./";
                    dialog.Filter = "JSON|*.json";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {

                        AssetLoader.SaveLevel(Level, dialog.FileName);

                        file = dialog.FileName;
                        saved = true;

                    }

                }
                else
                {

                    AssetLoader.SaveLevel(Level, file);

                    saved = true;

                }

            }

        }

        private void OnSaveAsClicked(object sender, EventArgs e)
        {

            if (Level != null)
            {

                SaveFileDialog dialog = new SaveFileDialog();

                dialog.InitialDirectory = "./";
                dialog.Filter = "JSON|*.json";

                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    AssetLoader.SaveLevel(Level, dialog.FileName);

                    file = dialog.FileName;
                    saved = true;

                }

            }

        }

        private void OnQuitClicked(object sender, EventArgs e)
        {

            Application.Exit();

        }

        private void OnExit(object sender, EventArgs e)
        {

            if (Level != null && !saved)
            {

                DialogResult result = MessageBox.Show("Would you like to save your current level?", "Save", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {

                    if (file == "")
                    {

                        SaveFileDialog dialog = new SaveFileDialog();

                        dialog.InitialDirectory = "./";
                        dialog.Filter = "JSON|*.json";

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {

                            AssetLoader.SaveLevel(Level, dialog.FileName);

                            file = dialog.FileName;

                        }

                    }
                    else
                    {

                        AssetLoader.SaveLevel(Level, file);

                    }

                }

            }

        }

        private void OnUndoClicked(object sender, EventArgs e)
        {

            if (Level != null && undoActions.Count > 0)
            {

                string actionToUndo = undoActions[undoActions.Count - 1];

                undoActions.Remove(actionToUndo);
                redoActions.Add(actionToUndo);

                string[] commands = actionToUndo.Split('_');

                switch (commands[0])
                {

                    case "startingPosition":
                        Level.StartingPositions[int.Parse(commands[1])] = new Position(int.Parse(commands[2]), int.Parse(commands[3]));
                        break;
                    case "wall":
                        if(commands[1] == "add")
                        {

                            Level.Tiles[int.Parse(commands[3])][int.Parse(commands[4])] = "";

                        }
                        else
                        {

                            Level.Tiles[int.Parse(commands[3])][int.Parse(commands[4])] = "wall_" + commands[2];

                        }
                        break;
                    case "unit":
                        if (commands[1] == "add")
                        {

                            Level.Tiles[int.Parse(commands[3])][int.Parse(commands[4])] = "";

                        }
                        else
                        {

                            Level.Tiles[int.Parse(commands[3])][int.Parse(commands[4])] = "unit_" + commands[2];

                        }
                        break;

                }

                UpdateMap();

                saved = false;

            }

        }

        private void OnRedoClicked(object sender, EventArgs e)
        {

            if (Level != null && redoActions.Count > 0)
            {

                string actionToRedo = redoActions[redoActions.Count - 1];

                redoActions.Remove(actionToRedo);
                undoActions.Add(actionToRedo);

                string[] commands = actionToRedo.Split('_');

                switch (commands[0])
                {

                    case "startingPosition":
                        Level.StartingPositions[int.Parse(commands[1])] = new Position(int.Parse(commands[4]), int.Parse(commands[5]));
                        break;
                    case "wall":
                        if (commands[1] == "add")
                        {

                            Level.Tiles[int.Parse(commands[3])][int.Parse(commands[4])] = "wall_" + commands[2];

                        }
                        else
                        {

                            Level.Tiles[int.Parse(commands[3])][int.Parse(commands[4])] = "";

                        }
                        break;
                    case "unit":
                        if (commands[1] == "add")
                        {

                            Level.Tiles[int.Parse(commands[3])][int.Parse(commands[4])] = "unit_" + commands[2];

                        }
                        else
                        {

                            Level.Tiles[int.Parse(commands[3])][int.Parse(commands[4])] = "";

                        }
                        break;

                }

                UpdateMap();

                saved = false;

            }

        }

        private void OnShowStartsClicked(object sender, EventArgs e)
        {

            tabControl1.SelectedIndex = 0;

        }

        private void OnShowWallsClicked(object sender, EventArgs e)
        {

            tabControl1.SelectedIndex = 1;

        }

        private void onShowUnitsClicked(object sender, EventArgs e)
        {

            tabControl1.SelectedIndex = 2;

        }

        private void OnEditInfoClicked(object sender, EventArgs e)
        {

            if (Level != null)
            {

                WindowLevelInfo level = new WindowLevelInfo(this, false);

                if (level.ShowDialog() == DialogResult.OK)
                {

                    UpdateMap();

                    saved = false;

                }

            }

        }

        private void OnAboutClicked(object sender, EventArgs e)
        {

            MessageBox.Show("Automated War Level Editor(v1.0.0)\nCreated By: Distropian Games");

        }

        private void OnMapClicked(object sender, MouseEventArgs e)
        {

            if (Level != null)
            {

                float width = Level.Tiles.Length;
                float tileWidth = Window.WIDTH / width;

                float tileX = Math.Max(0.0f, Math.Min(width - 1.0f, e.Location.X / tileWidth));
                float tileY = Math.Max(0.0f, Math.Min(width - 1.0f, e.Location.Y / tileWidth));

                if (e.Button == MouseButtons.Left)
                {

                    switch (tabControl1.SelectedIndex)
                    {

                        case 0:
                            if (listBox1.SelectedIndex != -1)
                            {

                                undoActions.Add("startingPosition_" + listBox1.SelectedIndex + "_" + Level.StartingPositions[listBox1.SelectedIndex].X + "_" + Level.StartingPositions[listBox1.SelectedIndex].Y + "_" + (int)tileX + "_" + (int)tileY);

                                Level.StartingPositions[listBox1.SelectedIndex] = new Position((int)tileX, (int)tileY);

                                UpdateMap();

                                saved = false;

                            }
                            break;
                        case 1:
                            if (listBox2.SelectedIndex != -1)
                            {

                                if (Level.Tiles[(int)tileX][(int)tileY] != "")
                                {

                                    string type = Level.Tiles[(int)tileX][(int)tileY].Split('_')[0];
                                    string id = Level.Tiles[(int)tileX][(int)tileY].Split('_')[1];

                                    undoActions.Add(type + "_remove_" + id + "_" + (int)tileX + "_" + (int)tileY);

                                }

                                undoActions.Add("wall_add_" + walls[listBox2.SelectedIndex].ID + "_" + (int)tileX + "_" + (int)tileY);

                                Level.Tiles[(int)tileX][(int)tileY] = "wall_" + walls[listBox2.SelectedIndex].ID;

                                UpdateMap();

                                saved = false;

                            }
                            break;
                        case 2:
                            if (listBox3.SelectedIndex != -1)
                            {

                                if (Level.Tiles[(int)tileX][(int)tileY] != "")
                                {

                                    string type = Level.Tiles[(int)tileX][(int)tileY].Split('_')[0];
                                    string id = Level.Tiles[(int)tileX][(int)tileY].Split('_')[1];

                                    undoActions.Add(type + "_remove_" + id + "_" + (int)tileX + "_" + (int)tileY);

                                }

                                undoActions.Add("unit_add_" + units[listBox3.SelectedIndex].ID + "_" + (int)tileX + "_" + (int)tileY);

                                Level.Tiles[(int)tileX][(int)tileY] = "unit_" + units[listBox3.SelectedIndex].ID;

                                UpdateMap();

                                saved = false;

                            }
                            break;

                    }

                }
                else if (e.Button == MouseButtons.Right)
                {

                    if (Level.Tiles[(int)tileX][(int)tileY] != "")
                    {

                        string type = Level.Tiles[(int)tileX][(int)tileY].Split('_')[0];
                        string id = Level.Tiles[(int)tileX][(int)tileY].Split('_')[1];

                        undoActions.Add(type + "_remove_" + id + "_" + (int)tileX + "_" + (int)tileY);

                    }

                    Level.Tiles[(int)tileX][(int)tileY] = "";

                    UpdateMap();

                    saved = false;

                }

            }

        }

        private void UpdateMap()
        {

            float width = Level.Tiles.Length;
            float tileWidth = Window.WIDTH / width;

            using (Graphics g = Graphics.FromImage(map.Image))
            {

                g.Clear(Color.FromArgb(64, 32, 0));

                for (int i = 0; i < 8; i++)
                {

                    Position position = Level.StartingPositions[i];
                    g.FillRectangle(Brushes.White, position.X * tileWidth, position.Y * tileWidth, tileWidth, tileWidth);

                }

                for (int i = 0; i < width; i++)
                {

                    for (int j = 0; j < width; j++)
                    {

                        string tile = Level.Tiles[i][j];

                        if (tile != "")
                        {

                            if (tile.StartsWith("wall_"))
                            {

                                g.DrawImage(wallBitmaps[walls.IndexOf(wallDictionary[tile.Substring("wall_".Length)])], i * tileWidth, j * tileWidth, tileWidth, tileWidth);

                            }
                            else if (tile.StartsWith("unit_"))
                            {

                                g.DrawImage(unitBitmaps[units.IndexOf(unitDictionary[tile.Substring("unit_".Length)])], i * tileWidth, j * tileWidth, tileWidth, tileWidth);

                            }

                        }

                    }

                }

                for (int i = 0; i < width; i++)
                {

                    for (int j = 0; j < width; j++)
                    {

                        g.DrawRectangle(Pens.Black, i * tileWidth, j * tileWidth, tileWidth, tileWidth);

                    }

                }

                g.FillRectangle(Brushes.White, Window.WIDTH / 2.0f - (tileWidth / 4.0f), Window.WIDTH / 2.0f - (tileWidth / 4.0f), tileWidth / 2.0f, tileWidth / 2.0f);

            }

            map.Invalidate();

        }

        public Level Level
        {

            get;
            set;

        }

    }

}
