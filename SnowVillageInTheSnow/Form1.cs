using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SnowVillageInTheSnow;

public class Snowflake
{
    public float X, Y, Speed;
    public int Size;
}

public partial class Form1 : Form
{
    private Bitmap _canvas;
    private Graphics _g;
    private Image _bgImageRaw;
    private Bitmap _bgScaled;
    private Image _snowflakeImg;
    private PictureBox _screen;
    private System.Windows.Forms.Timer _timer;

    private List<Snowflake> _snowflakes = new List<Snowflake>();
    private Random _rng = new Random();
    private bool _isStarted = false;

    public Form1()
    {
        InitializeComponent();
        this.WindowState = FormWindowState.Maximized;
        this.FormBorderStyle = FormBorderStyle.None;
        this.KeyPreview = true;
        this.KeyDown += Form1_KeyDown;
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape) this.Close();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
            _bgImageRaw = Image.FromFile(@"Resourses\Snow_Village.png");
        _snowflakeImg = Image.FromFile(@"Resourses\BLACK_Png_snowflake.png");

        _screen = new PictureBox { Dock = DockStyle.Fill };
        _screen.MouseDown += Screen_MouseDown;
        this.Controls.Add(_screen);

        _bgScaled = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        using (Graphics g = Graphics.FromImage(_bgScaled))
        {
            g.InterpolationMode = InterpolationMode.Low;
            g.DrawImage(_bgImageRaw, 0, 0, _bgScaled.Width, _bgScaled.Height);
        }

        _canvas = new Bitmap(_bgScaled.Width, _bgScaled.Height);
        _g = Graphics.FromImage(_canvas);
        _g.CompositingQuality = CompositingQuality.HighSpeed;
        _g.InterpolationMode = InterpolationMode.Low;
        _g.SmoothingMode = SmoothingMode.HighSpeed;

        for (int i = 0; i < 100; i++)
        {
            var s = CreateSnowflake(false);
            s.Y = _rng.Next(-_bgScaled.Height, -10);
            _snowflakes.Add(s);
        }

        _timer = new System.Windows.Forms.Timer { Interval = 30 };
        _timer.Tick += UpdateFrame;

        DrawScene();
    }

    private void Screen_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && !_isStarted)
        {
            _timer.Start();
            _isStarted = true;
        }
    }

    private Snowflake CreateSnowflake(bool randomY)
    {
        return new Snowflake
        {
            X = _rng.Next(0, _bgScaled.Width),
            Y = randomY ? _rng.Next(0, _bgScaled.Height) : _rng.Next(-100, -10),
            Speed = (float)(_rng.NextDouble() * 4 + 2),
            Size = _rng.Next(10, 35)
        };
    }

    private void UpdateFrame(object sender, EventArgs e)
    {
        _g.DrawImageUnscaled(_bgScaled, 0, 0);

        foreach (var f in _snowflakes)
        {
            f.Y += f.Speed;

            if (f.Y > _bgScaled.Height)
            {
                f.Y = -f.Size;
                f.X = _rng.Next(0, _bgScaled.Width);
            }

            _g.DrawImage(_snowflakeImg, f.X, f.Y, f.Size, f.Size);
        }

        _screen.Image = _canvas;
    }

    private void DrawScene()
    {
        _g.DrawImageUnscaled(_bgScaled, 0, 0);
        foreach (var f in _snowflakes)
        {
            _g.DrawImage(_snowflakeImg, f.X, f.Y, f.Size, f.Size);
        }
        _screen.Image = _canvas;
    }
}