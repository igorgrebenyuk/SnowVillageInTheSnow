using static System.Net.Mime.MediaTypeNames;

namespace SnowVillageInTheSnow;

partial class SnowfallForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        timer1 = new System.Windows.Forms.Timer(components);
        SuspendLayout();
        // 
        // SnowfallForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
        ClientSize = new System.Drawing.Size(1187, 855);
        Cursor = System.Windows.Forms.Cursors.IBeam;
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        KeyPreview = true;
        SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
        Text = "Snowfall_in_Village";
        WindowState = System.Windows.Forms.FormWindowState.Maximized;
        Load += SnowfallForm_Load;
        KeyDown += SnowfallForm_KeyDown;
        ResumeLayout(false);
    }

    private System.Windows.Forms.Timer timer1;

    #endregion
}