using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotCloud.YoloCreatorDataTrain
{
   public class CustomPictureBox: PictureBox
    {
        public CustomPictureBox() : base() {

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
              ControlStyles.SupportsTransparentBackColor, true);
            this.DoubleBuffered = true;
            this.UpdateStyles();
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
    }
}
