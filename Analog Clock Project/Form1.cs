using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analoc_Clock
{
    public partial class Form1 : Form
    {
        Timer t = new Timer();
        // WIDTH VE HEIGHT PICTUREBOX'IN SİZE'I SECOND , MINHAND VE HRHAND'IN UZUNLUĞU
        // 
        int WIDTH = 300, HEIGHT = 300, SECOND = 140, MINHAND = 110, HRHAND = 80;

        // X VE Y KORDİNATLARININ TANITILMASI
        int cx, cy;

        Bitmap bmp; //bmp değişkeni picturebox1'in kullanmka için tanmımladık
        Graphics g;



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(WIDTH , HEIGHT ); // picturebox1 size'nı bmp değişkenine kopyaladık

            cx = WIDTH / 2;
            cy = HEIGHT / 2; // picturebox1 in orta noktasını bu değişkenini konumlandırdık

            t.Interval = 1000; // timer'ı ilerlerleme aralığı 1 sn ayarlamak için yazdık.
            t.Tick += new EventHandler(this.t_Tick); //timerı başlatmak için
            t.Start();
        }
        private void t_Tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp); //bmp bizim picturebox1 alanımız bunun üstünde çeşitli çizimler yapabilmek için Graphics komutuyla g değişkeinine atarız


            //Sistemden saat bilgilerini çektik.
            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;

            int[] handCoord = new int[2]; // iki elemanlı bir dizi(array) oluşturduk

            g.Clear(Color.Transparent); // akrep yelkovan ve saniyenin bir sonraki konuma gittiğinde önceki oluşan konumu temizler
            

            handCoord = msCoord(ss, SECOND); //sistemden aldığımız saat bilgilerini mscoord fonk kullanarak handcoord dizisine yazarak gönderdik
            g.DrawLine(new Pen(Color.Red, 1f), new Point(cx, cy), new Point(handCoord[0], handCoord[1])); //Drawline komutunda ise saniye çubuğunu çizdirdik

            handCoord = msCoord(mm, MINHAND); //sistemden aldığımız saat bilgilerini mscoord fonk kullanarak handcoord dizisine yazarak gönderdik
            g.DrawLine(new Pen(Color.Black, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1])); //Drawline komutunda ise dakika çubuğunu çizdirdik

            handCoord = hrCoord(hh % 12, mm, HRHAND); //sistemden aldığımız saat bilgilerini mscoord fonk kullanarak handcoord dizisine yazarak gönderdik
            g.DrawLine(new Pen(Color.Blue, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1])); //Drawline komutunda ise saat çubuğunu çizdirdik

            pictureBox1.Image = bmp; //oluşan bmp'yi picturebox1'e aktardık
            pictureBox1.Parent = pictureBox2; // picturebox1 (picturebox1'in picturebox2 üzerinde transparent'in düzgün çalışması için yazdık

            this.Text = "Analog Clock  -  " + hh + ":" + mm + ':' + ss; // Formun ismini sistemden alınan saat bilgilerini dijital bir şekilde yazdırdık

            int kalansaay = Math.Abs(16 - hh);
            int kalandak = Math.Abs(34 - mm);
            int kalansan = Math.Abs(59 - ss);
            label1.Text = Convert.ToString(kalansaay+ ":"+ "Saat" + kalandak + ":" + "Dakika" + kalansan + ":" + "Saniye");

            int kalansaat = (12 - hh);
            int kalandakk = (15 - mm);
            int kalansann = (59 - ss);
            int kalansaat1 = Math.Abs(12 - hh);
            int kalandakk1 = Math.Abs(15 - mm);
            int kalansann1 = Math.Abs(59 - ss);
            int kalansure = (kalansaat1 * 60) + kalandakk1;
            if ((kalansaat) < 0)
            {
              label3.Text = "Mola Saati Geçti";
              label2.Hide();
              label5.Text = Convert.ToString(kalansure+"Dakika geçti");
            }
            else if (kalansaat<0 && kalandakk <0)
            {
                label3.Text = "Mola Saati Geçti";
                label2.Hide();
                label5.Text = Convert.ToString(kalansure + "Dakika geçti");
            }
            else if (kalansaat==0 && kalandakk == 0)
            {
                label2.Text = "İyi dinlenmeler.";
                label5.Hide();
                label3.Hide();
            }
            else if (kalansaat < 0 && kalandakk > 0)
            {
                label2.Text = Convert.ToString(kalansaat1 + " " + "Saat" + kalandakk1 + " " + "Dakika" + kalansann1 + " " + "Saniye");
                label5.Hide();
            }
            else
            label2.Text = Convert.ToString(kalansaat1 + " " + "Saat" + kalandakk1 + " " + "Dakika" + kalansann1 + " " + "Saniye");
            label5.Hide();

        }
        //Dakika ve saniye için yazılan fonksiyon
        private int[] msCoord(int val, int hlen) // Value = sistemin saati hlen= çubuğun uzunluğu
        {
            int[] coord = new int[2];  // Yeni iki elemanlı dizi tanımı
            val *= 6; // value'muz o anki değeri derece cinsine çevirme
            if (val >= 0 && val <= 180) // Yeni derecemiz 0-180 arası olursa
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else // Yeni derecemiz 181-360 arası olursa
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
        private int[] hrCoord(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];
            // each hour makes 30 degrees , each min makes 0.5 degrees
            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
    }
}
