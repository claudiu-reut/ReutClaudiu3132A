using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Reut
{
    class Window3D : GameWindow
    {
        private KeyboardState previousKeyboard;
        private readonly Axes ax;
        private readonly Triangle triunghi;
        private readonly Cube cub;
        private readonly Randomizer rand;
        int r1 = 0, g1 =0, b1 = 0;
        int r2 = 0 ,g2 = 0, b2 = 0;
        int r3 = 0, g3 = 0, b3 = 0;
   
        int x = 3, y = 3, z = 3;
        int c = 0;

       


       

        Matrix4 view = Matrix4.LookAt(new Vector3(3.0f, 3.0f, 3.0f),
             new Vector3(0.0f, 0.0f, 0.0f),
             new Vector3(0.0f, 1.0f, 0.0f));

        public Window3D() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;
            ax = new Axes();
            triunghi = new Triangle();
            cub = new Cube();
            rand = new Randomizer();
            Console.WriteLine("OpenGl versiunea: " + GL.GetString(StringName.Version));
            Title = "OpenGl versiunea: " + GL.GetString(StringName.Version) + " (mod imediat)";
            Console.WriteLine("Pentru triunghi: ");
            Console.WriteLine("\tTineti apasat tasta :\n\t1-Modificarea culorii punctului de pe axa X\n\t2-Modificarea culorii punctului de pe axa Y\n\t3-Modificarea culorii punctului de pe axa Z\n");
            Console.WriteLine("\tTinand apasat pe una din taste, apasati: \n\t R-cresterea valorii campului Red \n\t G-cresterea valorii campului Green \n\t B-cresterea valorii campului Blue");
            Console.WriteLine("\tTasta T - Comutare vizibilitate Triunghi\n");
           
            Console.WriteLine("Pentru cub: ");
            Console.WriteLine("\tTasta C - Comutare vizibilitate cub");
            Console.WriteLine("\tTasta ↑ - Crestere valori RGB pentru o fata a cubului");
            Console.WriteLine("\tTasta ↓ - Scadere valori RGB pentru o fata a cubului");
            Console.WriteLine("\tTasta V - Valori de culoare random pentru celelalte fete ale cubului");

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);






            GL.ClearColor(0.972f, 0.972f, 1, 0);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);


            Matrix4 lookat = Matrix4.LookAt(x, y, z, 0, 0, 0, 0, 1, 0);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref view);


        }


        


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            KeyboardState input = Keyboard.GetState();
            








            if (keyboard[Key.Escape])
            {
                Exit();
            }


            //Cerinta 2 Modificați aplicația pentru a manipula valorile RGB pentru fiecare
           // vertex ce definește un triunghi.Afișați valorile RGB în consolă.

            if (keyboard[Key.Number1])//tasta 1 pentru vertexul 1
            {
                if (keyboard[Key.R] && r1 < 255)
                {
                    r1++;
                }
                if (keyboard[Key.G] && g1 < 255)
                {
                    g1++;

                }
                if (keyboard[Key.B] && b1 < 255)
                {
                    b1++;
                }

                Console.Write("\rVertex 1: \t r1 =" + r1.ToString() + " g1 = " + g1.ToString() + " b1 = " + b1.ToString());// afiseaza in consola valorile rgb
            }
            if (keyboard[Key.Number2])//tasta 2 pentru vertexul 2
            {
                if (keyboard[Key.R]&&r2<255)
                {
                    r2++;
                }
                if (keyboard[Key.G] && g2 < 255)
                {
                    g2++;

                }
                if (keyboard[Key.B] && b2 < 255)
                {
                    b2++;
                }

                Console.Write("\rVertex 2: \t r2 =" + r2.ToString() + " g2 = " + g2.ToString() + " b2 = " + b2.ToString());// afiseaza in consola valorile rgb
            }
            if (keyboard[Key.Number3])//tasta 3 pentru vertexul 3
            {
                if (keyboard[Key.R] && r3 < 255)
                {
                    r3++;
                }
                if (keyboard[Key.G] && g3< 255)
                {
                    g3++;

                }
                if (keyboard[Key.B] && b3 < 255)
                {
                    b3++;
                }

                Console.Write("\rVertex 3: \t r3 =" + r3.ToString() + " g3 = " + g3.ToString() + " b3 = " + b3.ToString());// afiseaza in consola valorile rgb
            }
            if (keyboard[Key.Space])
            {   //la apasarea space se reseteaza toate valorile inapoi la 0
                r1 = 0;
                g1 = 0;
                b1 = 0;
                r2 = 0;
                g2 = 0;
                b2 = 0;
                r3 = 0;
                g3 = 0;
                b3 = 0;

            }
            if(keyboard[Key.T] && !previousKeyboard[Key.T])
            {
                triunghi.ToggleVisibility();
            }
            if (keyboard[Key.C] && !previousKeyboard[Key.C])
            {
                cub.ToggleVisibility();
            }
          
            //
            //Cerinta 1-Creați o aplicație care la apăsarea unui set de taste va modifica
           // culoarea unei fețe a unui cub 3D
            if (keyboard[Key.Up])
            {   if(c<255)
                c++;
                Console.Write("\rCub \t r =" + c.ToString() + " g = " + c.ToString() + " b = " + c.ToString());
            }
            if (keyboard[Key.Down])
            {     if(c>0)
                c--;
                Console.Write("\rCub \t r =" + c.ToString() + " g = " + c.ToString() + " b = " + c.ToString());
            }
            //Cerinta 3- Implementați un mecanism de modificare a culorilor
            if (keyboard[Key.V])
            {
                cub.ChangeColor(rand.RandomColor());
                cub.ChangeColor2(rand.RandomColor());
            }
            previousKeyboard = keyboard;




        }

        
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.Color3(0.7f, 0.0f, 1.0f);


            // DrawTriangle();

            ax.Draw();
            
            triunghi.ChangeColor1(r1, g1, b1);
            triunghi.ChangeColor2(r2, g2, b2);
            triunghi.ChangeColor3(r3, g3, b3);
            triunghi.Draw();
            cub.ChangeColor1(c, c, c);
           
            cub.Draw();


            // Se lucrează în modul DOUBLE BUFFERED - câtă vreme se afișează o imagine randată, o alta se randează în background apoi cele 2 sunt schimbate...
            SwapBuffers();
        }
       

       


       
        
    }
}

