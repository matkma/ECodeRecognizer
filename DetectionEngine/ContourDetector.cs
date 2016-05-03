using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace DetectionEngine
{
    public class ContourDetector
    {
        public List<Rect> Detect(Mat input)
        {
            Mat[] contours;
            List<Rect> rects;
            input.FindContours(out contours, OutputArray.Create(new Mat()), ContourRetrieval.FloodFill, ContourChain.ApproxSimple);
            using (new Window("test".ToString(), input))
            {
                Cv2.WaitKey();
            }
            Bound(contours, out rects);
            return rects;
        }

        private void Bound(Mat[] contours, out List<Rect> bound)
        {
            bound = new List<Rect>();
            foreach (var contour in contours.Where(contour => contour.Total() > 0))
            {
                var contourPoly = contour.ApproxPolyDP(3, true);
                var rect = Cv2.BoundingRect(contourPoly);
                if (rect.Width > rect.Height)
                {
                    bound.Add(rect);
                }
            }
        }

        public void DetectLetters(Mat canny)
        {
            
        }
    }
}
