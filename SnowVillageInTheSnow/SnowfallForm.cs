using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SnowVillageInTheSnow;

/// <summary>
/// Основная форма приложения, отображающая анимацию снегопада в деревне.
/// </summary>
public partial class SnowfallForm : Form
{
    private const int SnowflakesCount = 100;
    private const int MinSnowflakeSize = 10;
    private const int MaxSnowflakeSize = 35;
    private const float MinSnowflakeSpeed = 2.0f;
    private const float MaxSnowflakeSpeed = 6.0f;
    private const int TimerIntervalMilliseconds = 30;
    private const int InitialSpawnOffset = 10;
    private const int ZeroCoordinate = 0;

    private Bitmap canvas;
    private Graphics graphics;
    private Image backgroundImageRaw;
    private Bitmap backgroundScaled;
    private Image snowflakeImage;
    private PictureBox screenPictureBox;
    private System.Windows.Forms.Timer updateTimer;

    private List<Snowflake> snowflakes = [];
    private Random random = new();

    /// <summary>
    /// Инициализирует новый экземпляр формы <see cref="SnowfallForm"/>.
    /// </summary>
    public SnowfallForm()
    {
        InitializeComponent();
    }

    private void SnowfallForm_KeyDown(object sender, KeyEventArgs e)
    {
        Close();
    }

    private void SnowfallForm_Load(object sender, EventArgs e)
    {
        backgroundImageRaw = Properties.Resources.Snow_Village;
        snowflakeImage = Properties.Resources.BLACK_Png_snowflake;

        screenPictureBox = new PictureBox
        {
            Dock = DockStyle.Fill,
            BackColor = Color.Black
        };
        Controls.Add(screenPictureBox);

        backgroundScaled = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        using (var g = Graphics.FromImage(backgroundScaled))
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(backgroundImageRaw, ZeroCoordinate, ZeroCoordinate, backgroundScaled.Width, backgroundScaled.Height);
        }

        canvas = new Bitmap(backgroundScaled.Width, backgroundScaled.Height);
        graphics = Graphics.FromImage(canvas);
        graphics.InterpolationMode = InterpolationMode.Low;
        graphics.SmoothingMode = SmoothingMode.HighSpeed;

        updateTimer = new System.Windows.Forms.Timer { Interval = TimerIntervalMilliseconds };
        updateTimer.Tick += UpdateFrame;

        StartSnowfall();
    }

    private void StartSnowfall()
    {
        for (var index = 0; index < SnowflakesCount; index++)
        {
            snowflakes.Add(CreateSnowflake(-backgroundScaled.Height, -InitialSpawnOffset));
        }

        updateTimer.Start();
    }

    private Snowflake CreateSnowflake(int minY, int maxY)
    {
        return new Snowflake
        {
            X = random.Next(ZeroCoordinate, backgroundScaled.Width),
            Y = random.Next(minY, maxY),
            Speed = (float)(random.NextDouble() * (MaxSnowflakeSpeed - MinSnowflakeSpeed) + MinSnowflakeSpeed),
            Size = random.Next(MinSnowflakeSize, MaxSnowflakeSize + 1)
        };
    }

    private void UpdateFrame(object sender, EventArgs e)
    {
        graphics.DrawImageUnscaled(backgroundScaled, Point.Empty);

        foreach (var snowflake in snowflakes)
        {
            snowflake.Y += snowflake.Speed;

            if (snowflake.Y > backgroundScaled.Height)
            {
                snowflake.Y = -snowflake.Size;
                snowflake.X = random.Next(ZeroCoordinate, backgroundScaled.Width);
            }

            graphics.DrawImage(snowflakeImage, snowflake.X, snowflake.Y, snowflake.Size, snowflake.Size);
        }

        screenPictureBox.Image = canvas;
    }
}