using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Movimentum.Model {
    public class BarThing : Thing {
        public BarThing(string name, IEnumerable<ConstAnchor> anchors) : base(name, anchors) {
        }

        public override void Draw(Graphics drawingPane, 
                                  IDictionary<string, ConstVector> anchorLocations) {
            float height = drawingPane.VisibleClipBounds.Height;
            Point[] points = Anchors
                .Select(a => new Point(
                    (int)Math.Round(anchorLocations[a.Name].X), 
                    (int)(height - Math.Round(anchorLocations[a.Name].Y))))
                .ToArray();
            drawingPane.DrawLines(new Pen(Color.Orange, 3), points);
        }
    }
}