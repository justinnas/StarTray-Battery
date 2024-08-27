using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing;

namespace StarTray_Battery
{
    public static class IconUtils
    {
        public static Icon GenerateBatteryIconImage(int percentage, Image batteryIcon, Color batteryColor, FontFamily customFontFamily, bool isCharging)
        {
            int iconSize = 32;
            string percentageText = percentage.ToString();

            Bitmap bitmap = new Bitmap(iconSize, iconSize);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                int fontSize = 18;
                int moveX = 1;
                int moveY = 3;

                if (percentage >= 100)
                {
                    fontSize = 16;
                    moveX = 0;
                    moveY = 2;
                }
                else if (percentage < 10)
                {
                    moveX = 0;
                }

                if (isCharging) {
                    moveX = -4;
                }

                graphics.Clear(Color.Transparent);

                // Set background image
                graphics.DrawImage(batteryIcon, new Rectangle(0, 0, iconSize, iconSize));

                // Draw the battery percentage text
                using (Font font = new Font(customFontFamily, fontSize))
                {
                    using (Brush brush = new SolidBrush(batteryColor))
                    {
                        if (batteryColor == Color.Black)
                        {
                            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit; // Disable anti-aliasing
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                        }

                        SizeF textSize = graphics.MeasureString(percentageText, font);

                        float x = (bitmap.Width - textSize.Width) / 2 + moveX;
                        float y = (bitmap.Height - textSize.Height) / 2 + moveY;

                        graphics.DrawString(percentageText, font, brush, new PointF(x, y));
                    }
                }
            }

            Icon icon = Icon.FromHandle(bitmap.GetHicon());
            return icon;
        }
    }
}
