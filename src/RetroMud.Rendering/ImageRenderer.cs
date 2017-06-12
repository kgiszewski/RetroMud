//using System;
//using System.Linq;

//namespace RetroMud.Rendering
//{
//    public class ImageRenderer
//    {
//        static int[] cColors = { 0x000000, 0x000080, 0x008000, 0x008080, 0x800000, 0x800080, 0x808000, 0xC0C0C0, 0x808080, 0x0000FF, 0x00FF00, 0x00FFFF, 0xFF0000, 0xFF00FF, 0xFFFF00, 0xFFFFFF };

//        public void ConsoleWritePixel(Color cValue)
//        {
//            Color[] cTable = cColors.Select(x => Color.FromArgb(x)).ToArray();
//            char[] rList = new char[] { (char)9617, (char)9618, (char)9619, (char)9608 }; // 1/4, 2/4, 3/4, 4/4
//            int[] bestHit = new int[] { 0, 0, 4, int.MaxValue }; //ForeColor, BackColor, Symbol, Score

//            for (int rChar = rList.Length; rChar > 0; rChar--)
//            {
//                for (int cFore = 0; cFore < cTable.Length; cFore++)
//                {
//                    for (int cBack = 0; cBack < cTable.Length; cBack++)
//                    {
//                        int R = (cTable[cFore].R * rChar + cTable[cBack].R * (rList.Length - rChar)) / rList.Length;
//                        int G = (cTable[cFore].G * rChar + cTable[cBack].G * (rList.Length - rChar)) / rList.Length;
//                        int B = (cTable[cFore].B * rChar + cTable[cBack].B * (rList.Length - rChar)) / rList.Length;
//                        int iScore = (cValue.R - R) * (cValue.R - R) + (cValue.G - G) * (cValue.G - G) + (cValue.B - B) * (cValue.B - B);
//                        if (!(rChar > 1 && rChar < 4 && iScore > 50000)) // rule out too weird combinations
//                        {
//                            if (iScore < bestHit[3])
//                            {
//                                bestHit[3] = iScore; //Score
//                                bestHit[0] = cFore;  //ForeColor
//                                bestHit[1] = cBack;  //BackColor
//                                bestHit[2] = rChar;  //Symbol
//                            }
//                        }
//                    }
//                }
//            }
//            Console.ForegroundColor = (ConsoleColor)bestHit[0];
//            Console.BackgroundColor = (ConsoleColor)bestHit[1];
//            Console.Write(rList[bestHit[2] - 1]);
//        }


//        public void ConsoleWriteImage(Bitmap bmpSrc)
//        {
//            int sMax = 39;
//            decimal percent = Math.Min(decimal.Divide(sMax, bmpSrc.Width), decimal.Divide(sMax, bmpSrc.Height));
//            Size resSize = new Size((int)(bmpSrc.Width * percent), (int)(bmpSrc.Height * percent));
//            Func<System.Drawing.Color, int> ToConsoleColor = c =>
//            {
//                int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0;
//                index |= (c.R > 64) ? 4 : 0;
//                index |= (c.G > 64) ? 2 : 0;
//                index |= (c.B > 64) ? 1 : 0;
//                return index;
//            };
//            Bitmap bmpMin = new Bitmap(bmpSrc, resSize);
//            for (int i = 0; i < resSize.Height; i++)
//            {
//                for (int j = 0; j < resSize.Width; j++)
//                {
//                    Console.ForegroundColor = (ConsoleColor)ToConsoleColor(bmpMin.GetPixel(j, i));
//                    Console.Write("██");
//                }
//                System.Console.WriteLine();
//            }
//        }
//    }
//}
