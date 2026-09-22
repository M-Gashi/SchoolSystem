using SchoolSystem.Enums;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolSystem.Presentation.Quran.Parts
{
    public class clsPictureBox : PictureBox
    {
        // ___________________________________________________________________________________
        // ___________________________________________________________ Enums _________________
       
        public enum enOverlayStyle { Full,FillLevel }


        // ___________________________________________________________________________________
        // ___________________________________________________________ Fields ________________

        private bool _selected;  

        private enOverlayStyle _overlayStyle = enOverlayStyle.Full;

        private int _progress = 0;

        // ___________________________________________________________________________________
        // ___________________________________________________________ Properties ____________

        public Color OverlayColor { get; set; } = Color.Black;

        public int OverlayAlpha { get; set; } = 120;

        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                Invalidate(); // إعادة رسم الـ PictureBox مباشرة
            }
        }

        public enOverlayStyle OverlayStyle
        {
            get => _overlayStyle;
            set
            {
                _overlayStyle = value;
                Invalidate();
            }
        }  

        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                Invalidate();
            }
        }




        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (!Selected)
                return;

            switch (OverlayStyle)
            {
                case enOverlayStyle.Full:
                    {
                        using (SolidBrush brush =
                            new SolidBrush(Color.FromArgb(OverlayAlpha, OverlayColor)))
                        {
                            pe.Graphics.FillRectangle(brush, ClientRectangle);
                        }

                        break;
                    }

                case enOverlayStyle.FillLevel:
                    {
                        if (Progress <= 0)
                            break;

                        const int MaxLevel = 5;

                        int fillHeight = (ClientRectangle.Height * Progress) / MaxLevel;

                        Rectangle fillRect = new Rectangle(
                            0,
                            ClientRectangle.Height - fillHeight,
                            ClientRectangle.Width,
                            fillHeight);

                        using (SolidBrush brush =
                            new SolidBrush(Color.FromArgb(OverlayAlpha, Color.Green)))
                        {
                            pe.Graphics.FillRectangle(brush, fillRect);
                        }

                        break;
                    }
            }
        }


    }
}