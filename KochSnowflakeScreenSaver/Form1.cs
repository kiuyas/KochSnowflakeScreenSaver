using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KochSnowflakeScreenSaver
{
    public partial class Form1 : Form
    {
        #region Preview API's

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        #endregion

        #region variables and properties

        private int count = 0;

        private List<KochData> list;

        private int W, H;

        private Random random = new Random();

        private KochSettings settings = new KochSettings();

        private KochGenerator generator = new KochGenerator();
        public bool IsPreviewMode { get; set; }

        #endregion

        #region constructors

        /// <summary>
        /// デザイナ用
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// スクリーンセーバ用
        /// </summary>
        /// <param name="Bounds"></param>
        public Form1(Rectangle bounds)
        {
            InitializeComponent();

            Bounds = bounds;

            Initialize();

            timer1.Start();
        }

        /// <summary>
        /// プレビュー用
        /// </summary>
        /// <param name="PreviewHandle"></param>
        public Form1(IntPtr PreviewHandle)
        {
            InitializeComponent();

            SetParent(Handle, PreviewHandle);
            SetWindowLong(Handle, -16, new IntPtr(GetWindowLong(Handle, -16) | 0x40000000));
            Rectangle ParentRect;
            GetClientRect(PreviewHandle, out ParentRect);
            Size = ParentRect.Size;
            Location = new Point(0, 0);
            IsPreviewMode = true;

            Initialize();

            W = Size.Width;
            H = Size.Height;

            timer1.Start();
        }

        private void Initialize()
        {
            Cursor.Hide();
            WindowState = FormWindowState.Maximized;
            W = Screen.PrimaryScreen.Bounds.Width;
            H = Screen.PrimaryScreen.Bounds.Height;

            list = new List<KochData>();
        }

        #endregion

        #region 初期処理

        /// <summary>
        /// 描くデータの準備
        /// </summary>
        private void Loop()
        {
            if (list.Count > 100)
            {
                list = new List<KochData>();
            }

            // 図形作成など
            double cx = random.Next(W);
            double cy = random.Next(H);
            double r = random.Next(100) + 10;
            Pen pen = new Pen[] { Pens.Blue, Pens.Red, Pens.Lime, Pens.Magenta, Pens.Cyan, Pens.Yellow, Pens.White }[random.Next(7)];
            double adjustAngle = random.Next(360) * Math.PI / 180;
            KochData data = new KochData(cx, cy, r, 6, adjustAngle, pen);
            generator.Execute(data, settings);
            list.Add(data);

            Refresh();
        }

        #endregion

        #region event handlers

        /// <summary>
        /// 描くデータのリフレッシュ
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            Loop();
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            KochDrawer.Execute(e.Graphics, list);
        }

        #endregion
    }
}
