using System;
using System.Windows.Forms;

namespace Automated_War_Level_Editor
{

    public partial class WindowLevelInfo : Form
    {

        private Window window;
        private bool create;

        public WindowLevelInfo(Window window, bool create)
        {

            this.window = window;
            this.create = create;

            InitializeComponent();

            if (!create)
            {

                textBox1.Text = window.Level.Title;
                textBox2.Text = window.Level.Story;
                comboBox1.SelectedIndex = window.Level.Tiles.Length / 8 - 1;

            }
            else
            {

                comboBox1.SelectedIndex = 1;

            }

        }

        private void OnOkClicked(object sender, EventArgs e)
        {

            if (create)
            {

                window.Level = new Level(textBox1.Text, textBox2.Text, (comboBox1.SelectedIndex + 1) * 8);

            }
            else
            {

                window.Level.Title = textBox1.Text;
                window.Level.Story = textBox2.Text;

                int width = (comboBox1.SelectedIndex + 1) * 8;

                for (int i = 0; i < 8; i++)
                {

                    window.Level.StartingPositions[i] = new Position(width - 1, width - 1);

                }

                string[][] newTiles = new string[width][];

                for (int i = 0; i < width; i++)
                {

                    newTiles[i] = new string[width];

                    for (int j = 0; j < width; j++)
                    {

                        newTiles[i][j] = "";

                    }

                }

                for (int i = 0; i < Math.Min(window.Level.Tiles.Length, width); i++)
                {

                    for (int j = 0; j < Math.Min(window.Level.Tiles.Length, width); j++)
                    {

                        newTiles[i][j] = window.Level.Tiles[i][j];

                    }

                }

                window.Level.Tiles = newTiles;

            }

            DialogResult = DialogResult.OK;

        }

    }

}
