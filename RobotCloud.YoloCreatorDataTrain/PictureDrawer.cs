using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotCloud.YoloCreatorDataTrain
{
    public class PictureDrawer
    {
        Point _currentPos = new Point(0, 0);

        Point _1st;
        Point _2nd;

        PictureBox _picBox;
        public Bitmap _img;
        string _pathFile;
        FileInfo _fileInfo;

        public Dictionary<Guid, CustomGroupBox> _draweds = new Dictionary<Guid, CustomGroupBox>();

        CustomGroupBox _horizonl;
        CustomGroupBox _vertical;
        Form _windowForm;

        int _order;

        int _originalWidth;
        int _originalHeight;
        float _zoom = 1;
        public PictureDrawer(CustomPictureBox picBox, Form windowForm)
        {

            _windowForm = windowForm;
            _picBox = picBox;

            _horizonl = new CustomGroupBox(_img) { _BorderColor = Color.Black, Height = 1, Width = windowForm.Width };
            _vertical = new CustomGroupBox(_img) { _BorderColor = Color.Black, Height = windowForm.Height, Width = 1 };

            _picBox.Controls.Add(_horizonl);
            _picBox.Controls.Add(_vertical);
            _horizonl.BringToFront();
            _vertical.BringToFront();

            _picBox.MouseUp += _picBox_MouseUp; ;
            _picBox.MouseDown += _picBox_MouseDown;
            _picBox.MouseMove += _picBox_MouseMove;

        }

        public void Zoom(float zoom = 1)
        {
            if (zoom <= 0) zoom = 1;

            _zoom = zoom;
        }
        public MemoryStream ResizeImage(MemoryStream imgSource, int width = 2048, int height = 0)
        {
            var bmpSrc = new Bitmap(imgSource);

            if (bmpSrc.Width < 80 || bmpSrc.Height < 80)
            {
                width = 85;
                height = 0;
            }

            if (height != 0 && width == 0)
            {
                width = Convert.ToInt32(bmpSrc.Width * height / (double)bmpSrc.Height);
            }
            else
            {
                height = Convert.ToInt32(bmpSrc.Height * width / (double)bmpSrc.Width);
            }
            var resized = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            var ms = new MemoryStream();
            ImageAttributes wrapMode = new ImageAttributes();

            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            var graphics = Graphics.FromImage(resized);

            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.DrawImage(bmpSrc, new Rectangle(0, 0, width, height), 0, 0, bmpSrc.Width, bmpSrc.Height, GraphicsUnit.Pixel, wrapMode);

            resized.Save(ms, bmpSrc.RawFormat);

            ms.Position = 0;

            return ms;
        }
        public void Load(string pathFile)
        {
            if (!string.IsNullOrEmpty(pathFile))
            {
                _img = new Bitmap(pathFile);
                _picBox.Image = _img;
                _picBox.Width = _img.Width;
                _picBox.Height = _img.Height;
            }

            _originalHeight = _picBox.Height;
            _originalWidth = _picBox.Width;
            var sizeToShow = 2048;
            if (_originalWidth > sizeToShow)
            {
                var ms = new MemoryStream();
                _img.Save(ms, _img.RawFormat);

                _zoom = sizeToShow / ((float)_originalWidth);

                var resized = ResizeImage(ms, sizeToShow);

                _img = new Bitmap(resized);
                _picBox.Image = _img;
                _picBox.Width = _img.Width;
                _picBox.Height = _img.Height;
            }

            _pathFile = pathFile;
            _fileInfo = new FileInfo(_pathFile);

            _idBoxCurrent = Guid.Empty;
            _horizonl.Width = _img.Width;
            _vertical.Height = _img.Height;

            FileInfo f = new FileInfo(pathFile);
            var fileData = Path.Combine(f.DirectoryName, $"{f.Name}.data.json");

            if (File.Exists(fileData))
            {
                _lockPositionUp = true;
                List<ItemLabeled> data;

                using (StreamReader sr = new StreamReader(fileData))
                {
                    data = JsonConvert.DeserializeObject<List<ItemLabeled>>(sr.ReadToEnd());
                }

                _draweds = new Dictionary<Guid, CustomGroupBox>();

                foreach (var d in data)
                {
                    CustomGroupBox cgb = new CustomGroupBox(_img)
                    {
                        Location = d.Location,
                        _Lable = d.lable,
                        _BorderColor = d.Color,
                        _PathFile = d.fileImage,
                        Width = d.Width,
                        Height = d.Height,
                        _BoxId = d.Id,
                        relative_center_x = d.relative_center_x,
                        relative_center_y = d.relative_center_y,
                        relative_height = d.relative_height,
                        relative_width = d.relative_width,
                        _order = d.Order
                    };

                    cgb.Actived += _currentBox_Actived;
                    cgb.Deleted += _currentBox_Deleted;

                    _draweds.Add(d.Id, cgb);
                }

                Refresh();
            }

        }
        public static Random _rnd = new Random();
        public Guid _idBoxCurrent;
        CustomGroupBox GetCurrentBox(Guid id)
        {
            _draweds.TryGetValue(id, out CustomGroupBox c);
            return c;
        }
        private void _picBox_MouseDown(object sender, MouseEventArgs e)
        {
            _lockPositionUp = false;
            _1st = new Point(e.Location.X, e.Location.Y);
            _order++;
            CustomGroupBox _currentBox = new CustomGroupBox(_img)
            {
                _BoxId = Guid.NewGuid(),
                Name = $"temp_{DateTime.Now.Ticks}",
                Location = _1st,
                _BorderColor = Color.FromArgb(_rnd.Next(150, 255), _rnd.Next(150, 255), _rnd.Next(150, 255)),
                _order = _order
            };
            _idBoxCurrent = _currentBox._BoxId;

            _draweds[_currentBox._BoxId] = _currentBox;

            _picBox.Controls.Add(_currentBox);

            _currentBox.BringToFront();
            _currentBox.Actived += _currentBox_Actived;
            _currentBox.Deleted += _currentBox_Deleted;
        }

        private void _currentBox_Deleted(Guid obj)
        {
            _draweds.Remove(obj);
            Refresh();

            ItemDeleted?.Invoke(this, obj);
        }

        private void _currentBox_Actived(CustomGroupBox obj)
        {
            _idBoxCurrent = obj._BoxId;
            
            if (_draweds.TryGetValue(_idBoxCurrent, out CustomGroupBox g))
            {
                g._BorderColor = obj._BorderColor;
            }
            Refresh();
            ItemActived?.Invoke(this, obj);
        }

        public event Action<PictureDrawer, Guid> ItemDeleted;

        public event Action<PictureDrawer, CustomGroupBox> ItemActived;

        private void _picBox_MouseMove(object sender, MouseEventArgs e)
        {
            _horizonl.Location = new Point(e.Location.X + 2, e.Location.Y + 2);
            _vertical.Location = new Point(e.Location.X + 2, e.Location.Y + 2);

            if (_lockPositionUp) return;

            var _currentBox = GetCurrentBox(_idBoxCurrent);

            if (_currentBox == null) return;
            _currentBox.Width = e.Location.X - _1st.X;
            _currentBox.Height = e.Location.Y - _1st.Y;

            _windowForm.Text = $"box: {_currentBox.Width}-{_currentBox.Height} {_currentBox.Location} | mouse: {e.Location}";

            Refresh();
        }

        bool _lockPositionUp = false;
        private void _picBox_MouseUp(object sender, MouseEventArgs e)
        {
            var _currentBox = GetCurrentBox(_idBoxCurrent);
            if (_currentBox == null) return;
            if (_lockPositionUp) return;

            _lockPositionUp = true;
            
            _idBoxCurrent = Guid.Empty;

            _2nd = new Point(e.Location.X, e.Location.Y);


            if (_currentBox.Width <= 0 || _currentBox.Height <= 0)
            {
                _windowForm.Text = $"box: invalid | mouse: {e.Location}";
                _draweds.Remove(_currentBox._BoxId);
                _currentBox.Dispose();
                _currentBox = null;
                return;
            }

            string lable;
            lable = Interaction.InputBox("Lable", "Lable", _fileInfo.Name.Replace(_fileInfo.Extension, ""));
            if (string.IsNullOrEmpty(lable))
            {
                lable = _currentBox._BoxId.ToString();
            }

            _currentBox._Lable = lable;

            _currentBox._PathFile = _pathFile;

            CalculateRelativeYolo(_currentBox);

            Refresh();

            ItemDrawDone?.Invoke(this, _currentBox);
        }

        private void CalculateRelativeYolo(CustomGroupBox currentBox)
        {
            //https://github.com/AlexeyAB/Yolo_mark/issues/60
            /*	float const relative_center_x = (float)(i.abs_rect.x + i.abs_rect.width / 2) / full_image_roi.cols;
             *	
							float const relative_center_y = (float)(i.abs_rect.y + i.abs_rect.height / 2) / full_image_roi.rows;

							float const relative_width = (float)i.abs_rect.width / full_image_roi.cols;

							float const relative_height = (float)i.abs_rect.height / full_image_roi.rows;*/

            currentBox.relative_center_x = ((double)(currentBox.Location.X + currentBox.Width / 2) / (double)_img.Width);
            currentBox.relative_center_y = ((double)(currentBox.Location.Y + currentBox.Height / 2) / (double)_img.Height);
            currentBox.relative_width = ((double)currentBox.Width / (double)_img.Width);
            currentBox.relative_height = ((double)currentBox.Height / (double)_img.Height);
        }

        public event Action<PictureDrawer, CustomGroupBox> ItemDrawDone;

        private void picMain_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        public void Delete(Guid id)
        {
            _draweds.Remove(id);
            Refresh();
        }

        DateTime _lastRefresh;
        public void Refresh()
        {
            if (DateTime.Now.Subtract(_lastRefresh).TotalMilliseconds <= 80) return;

            _lastRefresh = DateTime.Now;

            _picBox.Controls.Clear();

            //var temp = _draweds.Where(i => i.Value._Id == Guid.Empty || string.IsNullOrEmpty(i.Value._PathFile)
            //|| i.Value.Width < 5 || i.Value.Height < 5
            //).ToList();
            //foreach (var t in temp)
            //{
            //    _draweds.Remove(t.Value._Id);
            //}
            CustomGroupBox activeOne = null;
            foreach (var d in _draweds.OrderBy(i => i.Value._order))
            {
                _picBox.Controls.Add(d.Value);

                _picBox.Controls.SetChildIndex(d.Value, d.Value._order);

                d.Value.RefreshView();

                d.Value.BringToFront();

                if (d.Key == _idBoxCurrent) activeOne = d.Value;

            }

            if (activeOne != null) activeOne.BringToFront();

            _picBox.Controls.Add(_horizonl);
            _picBox.Controls.Add(_vertical);

            _horizonl.BringToFront();
            _vertical.BringToFront();

            _picBox.Invalidate();
            _picBox.Refresh();
        }

        public void FocusItem(Guid id)
        {
            _idBoxCurrent = id;
            Refresh();
        }

        public void BringToFront(Guid id)
        {
            if (_draweds.TryGetValue(id, out CustomGroupBox b))
            {
                b.BringToFront();
            }
        }
    }
}
