using System;
using System.Drawing.Drawing2D;
using System.Web;
using System.Drawing;
using Web.Helper;


namespace YZ.Web.Asp
{
    public partial class CheckCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string checkCode = CreateRandomCode(4);
            Session[SessionHelper.P_ValidCodeKey] = checkCode;
            CreateImage(checkCode);
        }

        private void CreateImage(string checkCode)
        {
            HttpCookie cookie = new HttpCookie("code") { Value = checkCode, Expires = DateTime.Now.AddDays(2) };
            Response.Cookies.Add(cookie);

            Bitmap image = new Bitmap(Convert.ToInt32(Math.Ceiling((decimal)(checkCode.Length * 30))), 48);
            Graphics g = Graphics.FromImage(image);
            try
            {
                Random random = new Random();
                g.Clear(Color.AliceBlue );

                //画背景上的噪点
                for (int i = 0; i < 80; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);//Comic Sans MS
                }

                //把文字放到画布上
                Font font = new Font("幼圆", 32, FontStyle.Bold);
                //System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(3, 3, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.1f, true);
                //g.DrawString(checkCode, font, new SolidBrush(Color.Blue),1,1);

                const float angle = 40; //角度
                float l = 0;
                char[] code = checkCode.ToCharArray();
                for (int i = 0; i < code.Length; i++)
                {
                    string s = new string(code[i], 1);
                    g.ResetTransform();
                    SizeF size = g.MeasureString(s, font);
                    g.TranslateTransform(l + size.Width / 2, size.Height / 2); //设置旋转中心为文字中心
                    g.RotateTransform((float)(random.NextDouble() * angle * 2 - angle)); //旋转
                    PointF pointF = new PointF(-20 - (i * 10), -size.Height / 2);
                    g.DrawString(s, font, RndBrush(Color.AliceBlue, random), pointF);
                    l += size.Width;
                }

                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        private SolidBrush RndBrush(Color backColor, Random rnd)
        {
            int r, g, b;
            do
            {
                r = rnd.Next(255);
            } while (Math.Abs(r - backColor.R) < 50);  //这是为了控制颜色不要和背景色太接近
            do
            {
                g = rnd.Next(255);
            } while (Math.Abs(g - backColor.G) < 50);
            do
            {
                b = rnd.Next(255);
            } while (Math.Abs(b - backColor.B) < 50);
            return new SolidBrush(Color.FromArgb(255, r, g, b));
        }

        public string CreateRandomCode(int codeCount)
        {//,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z
            const string allChar = "0,1,2,3,4,5,6,7,8,9";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(10);
                if (temp != -1 && temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
    }
}