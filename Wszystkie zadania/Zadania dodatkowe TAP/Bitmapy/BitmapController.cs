using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmapy
{
    class BitmapController
    {
        public Bitmap Bmp { get; set; }


        public Bitmap LoadBitmap(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path is empty!");
            }

            //wymuszone czekanie 5 sekund aby sprawdzic czy GUI zachowuje sie poprawnie
            //System.Threading.Thread.Sleep(5000);

            Bmp = new Bitmap(path);
            return Bmp;
        }


        public Task<Bitmap> LoadBitmapAsync(string path)
        {
            return Task.Run(() =>
            {
                if(string.IsNullOrEmpty(path))
                {
                    throw new ArgumentException("Path is empty!");
                }

                //wymuszone czekanie 5 sekund aby sprawdzic czy GUI zachowuje sie poprawnie
                System.Threading.Thread.Sleep(5000);

                Bmp = new Bitmap(path);
                return Bmp;
            });
        }

        

    }
}
