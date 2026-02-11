using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    internal class cropImage
    {
        public static Image CropImage(Image source, Rectangle cropArea)
        {
            Bitmap bmp = new Bitmap(cropArea.Width, cropArea.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(source, new Rectangle(0, 0, bmp.Width, bmp.Height),
                    cropArea,
                    GraphicsUnit.Pixel);
            }
            return bmp;
        }
    }
}
