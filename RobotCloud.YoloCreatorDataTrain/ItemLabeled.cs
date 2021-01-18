using System;
using System.Drawing;

namespace RobotCloud.YoloCreatorDataTrain
{
    public class ItemLabeled
    {
        public string lable { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Point Location { get; set; }
        public double relative_center_x { get; set; }
        public double relative_center_y { get; set; }
        public double relative_width { get; set; }
        public double relative_height { get; set; }

        public Color Color { get; set; }
        public Guid Id { get; set; }
        public string fileImage { get; set; }

        public int Order { get; set; }

        public void CalculateRelativeYolo(int org_width = 0, int org_height = 0)
        {
            if (org_width == 0 || org_height == 0)
            {
                org_width = this.Width;
                org_height = this.Height;
            }

            if (org_width == 0 || org_height == 0) return;
            //https://github.com/AlexeyAB/Yolo_mark/issues/60
            /*	float const relative_center_x = (float)(i.abs_rect.x + i.abs_rect.width / 2) / full_image_roi.cols;
             *	
							float const relative_center_y = (float)(i.abs_rect.y + i.abs_rect.height / 2) / full_image_roi.rows;

							float const relative_width = (float)i.abs_rect.width / full_image_roi.cols;

							float const relative_height = (float)i.abs_rect.height / full_image_roi.rows;*/

            this.relative_center_x = (double)(this.Location.X + org_width / 2) / (double)org_width;
            this.relative_center_y = (double)(this.Location.Y + org_height / 2) / (double)org_height;
            this.relative_width = (double)this.Width / (double)org_width;
            this.relative_height = (double)this.Height / (double)org_height;
        }

    }
}
