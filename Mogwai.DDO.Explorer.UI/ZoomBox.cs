using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mogwai.DDO.Explorer.UI
{
    public class ZoomBox : PictureBox
    {
        private readonly Matrix _previewTransform = new Matrix();
        private float _zoomFactor = 1.1f;
        private float? _currentZoom = null;

        public ZoomBox()
        {
            this.MouseWheel += new MouseEventHandler(MyPictureBox_MouseWheel);
        }

        void MyPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_currentZoom == null)
            {
                _currentZoom = 1.0f;
            }

            if (e.Delta > 0)
            {
                _currentZoom *= _zoomFactor;
            }
            else
            {
                _currentZoom *= (1 / _zoomFactor);
            }

            Focus();
            if (Focused && e.Delta != 0)
            {
                ZoomScroll(e.Location, e.Delta > 0);
            }
        }

        public new Image Image // overrides
        {
            get
            {
                return base.Image;
            }
            set
            {
                _currentZoom = null;
                base.Image = value;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            g.Transform = _previewTransform;

            base.OnPaint(pe);
        }

        private void ZoomScroll(Point location, bool zoomIn)
        {
            // make zoom-point (cursor location) our origin
            // _previewTransform.Translate(-location.X, -location.Y);

            // perform zoom (at origin)
            if (zoomIn)
                _previewTransform.Scale(_zoomFactor, _zoomFactor);
            else
                _previewTransform.Scale(1 / _zoomFactor, 1 / _zoomFactor);

            // translate origin back to cursor
            // _previewTransform.Translate(location.X, location.Y);

            Invalidate();
        }

    }
}
