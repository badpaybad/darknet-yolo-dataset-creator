using System;
using System.Drawing;
using System.Windows.Forms;

namespace RobotCloud.YoloCreatorDataTrain
{
    public class CustomGroupBox : Panel
    {
        public Color _BorderColor { get; set; } = Color.Red;
        Label _lbl = new Label() { Location = new Point(2, 2), Text = "Lable" };

        Label _lblCenter = new Label() { Text = "o", ForeColor = Color.Red };

        Image _parentImage;

        public string _Lable
        {
            get { return _lbl.Text; }
            set
            {
                _lbl.Text = value;
            }
        }

        public int _order { get; set; }
        public Guid _BoxId { get; set; }
        public string _PathFile { get; set; }

        public double relative_center_x { get; set; }
        public double relative_center_y { get; set; }
        public double relative_width { get; set; }
        public double relative_height { get; set; }

        PaintEventArgs _paint;

        public static Random _rnd = new Random();
        public CustomGroupBox(Image parentImage) : base()
        {
            //this.DoubleBuffered = true;
            this.BorderStyle = BorderStyle.FixedSingle;

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
              ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            //base.BackColor = Color.FromArgb(25, Color.Black);//Added this because image wasnt redrawn when resizing form

            _parentImage = parentImage;
            this.BackColor = Color.FromArgb(25, _BorderColor);
            _btnDel.BackColor = this.BackColor;//Added this because image wasnt redrawn when resizing form

            _lbl.AutoSize = true;
            _lblCenter.AutoSize = true;
            //_lblCenter.Visible = false;
            _btnDel.Click += BtnDel_Click;
            _lbl.Click += _lbl_Click;
            this.Click += CustomGroupBox_Click;

            this.Controls.Add(_btnDel);
            this.Controls.Add(_lbl);

            this.Controls.Add(_lblCenter);
        }

        Button _btnDel = new Button();
        private const int HTTRANSPARENT = -1;
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WM_NCHITTEST)
                m.Result = (IntPtr)HTTRANSPARENT;
            else
                base.WndProc(ref m);
        }
        public Color DarkerColor(Color cl)
        {
            return Color.FromArgb(cl.R - cl.R / 3, cl.G - cl.G / 3, cl.B - cl.B / 3);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Color bgColor = Color.FromArgb(25, _BorderColor);

            _btnDel.BackColor = bgColor;//Added this because image wasnt redrawn when resizing form

            _btnDel.Text = "{X}";
            _btnDel.Margin = new Padding(0, 0, 0, 0);
            _btnDel.Padding = new Padding(0, 0, 0, 0);
            _btnDel.Width = 30;
            _btnDel.ForeColor = DarkerColor(_BorderColor);
            _btnDel.Location = new Point(this.Width - 34, 2);
            _lbl.ForeColor = DarkerColor(_BorderColor);
            _paint = e;
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                _BorderColor, 2, ButtonBorderStyle.Solid, // left
                _BorderColor, 2, ButtonBorderStyle.Solid, // top
                _BorderColor, 2, ButtonBorderStyle.Solid, // right
                _BorderColor, 2, ButtonBorderStyle.Solid);// bottom

            this.BackColor = bgColor;//Added this because image wasnt redrawn when resizing form

            DrawCenterPoint();

            this.Update();
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x20;
        //        return cp;
        //    }
        //}

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{

        //}

        private void CustomGroupBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me != null && me.Button == MouseButtons.Right)
            {
                this.BringToFront();
                Actived?.Invoke(this);
            }
        }

        private void _lbl_Click(object sender, EventArgs e)
        {
            this.BringToFront();
            _BorderColor = Color.FromArgb(_rnd.Next(50, 255), _rnd.Next(50, 255), _rnd.Next(50, 255));
            Actived?.Invoke(this);
        }

        public void RefreshView()
        {
            this.Controls.Clear();
            this.Controls.Add(_btnDel);
            this.Controls.Add(_lbl);
            this.Controls.Add(_lblCenter);
            DrawCenterPoint();

            // this.Invalidate();
            //this.Refresh();
        }

        void DrawCenterPoint()
        {
            if (this.relative_center_x != 0)
            {
                _lblCenter.ForeColor = DarkerColor(_BorderColor);
                _lblCenter.Visible = true;
                _lblCenter.Location = new Point((int)this.relative_center_x * _parentImage.Width + this.Width / 2, (int)this.relative_center_y * _parentImage.Height + this.Height / 2);

                _lblCenter.Text = $"o {_lblCenter.Location} ({Math.Round(this.relative_width * _parentImage.Width, 0)},{Math.Round(this.relative_height * _parentImage.Height, 0)})";
            }
            else
            {
                _lblCenter.Visible = false;
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("Delete lable? " + _lbl.Text, "Delete lable? " + _lbl.Text, MessageBoxButtons.YesNo);
            if (r == DialogResult.No) return;

            Guid id = this._BoxId;
            Deleted?.Invoke(id);
            this.Dispose();
        }
        public event Action<Guid> Deleted;
        public event Action<CustomGroupBox> Actived;
    }
}
