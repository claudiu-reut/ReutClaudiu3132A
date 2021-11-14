using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace Laborator2
{
   
    class ImmediateMode:GameWindow
    {

        KeyboardState lastKeyPress;
        float r1 = 0.0f, g1 = 0.0f, b1 = 0.0f;
        float r2 = 0.0f, g2 = 0.0f, b2 = 0.0f;
        float r3 = 0.0f, g3 = 0.0f, b3 = 0.0f;
        private const int XYZ_SIZE = 75;
        int x=3, y=3, z=3;

        float speed = 0.1f;
       

        Vector3 position = new Vector3(0.0f, 0.0f, 3.0f);
        Vector3 front = new Vector3(0.0f, 0.0f, -1.0f);
        Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

        Matrix4 view = Matrix4.LookAt(new Vector3(3.0f, 3.0f, 3.0f),
             new Vector3(0.0f, 0.0f, 0.0f),
             new Vector3(0.0f, 1.0f, 0.0f));

        public ImmediateMode() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            Console.WriteLine("OpenGl versiunea: " + GL.GetString(StringName.Version));
            Title = "OpenGl versiunea: " + GL.GetString(StringName.Version) + " (mod imediat)";
            Console.WriteLine("Tineti apasat tasta :\n1-Modificarea culorii punctului de pe axa X\n2-Modificarea culorii punctului de pe axa Y\n3-Modificarea culorii punctului de pe axa Z\n");
            Console.WriteLine("Tinand apasat pe una din taste, apasati: \n R-cresterea valorii campului Red \n G-cresterea valorii campului Green \n B-cresterea valorii campului Blue");
            Console.WriteLine("WASD-controlul camerei inainte, inapoi, stanga dreapta");
            Console.WriteLine("Mouse- Click stanga in partea de sus a ferestrei pentru a muta camera mai sus, click stanga in partea de jos pentru a o muta mai in jos");
            Console.WriteLine(r1.ToString());
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
            //Cerinta 5
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
            int x_curent = mouse.X;
            int y_curent = mouse.Y;
            GL.LoadMatrix(ref view);
            
            view = Matrix4.LookAt(position, position + front, up);
            if (input.IsKeyDown(Key.W))
            {
                position += front * speed; //inainte
                
                
            }

            if (input.IsKeyDown(Key.S))
            {
                position -= front * speed; //inapoi
               
            }

            if (input.IsKeyDown(Key.A))
            {
                position -= Vector3.Normalize(Vector3.Cross(front, up)) * speed; //stanga
               
            }

            if (input.IsKeyDown(Key.D))
            {
                position += Vector3.Normalize(Vector3.Cross(front, up)) * speed; //dreapta
                
            }

          



            if (keyboard[Key.Escape])
            {
                Exit();
            }

            /*Cerinta 8-Creați o aplicație care la apăsarea unui set de taste va modifica
            culoarea unui triunghi(coordonatele acestuia vor fi încărcate dintr - un
             fișier text) între valorile minime și maxime, pentru fiecare canal de
                       culoare.

            Cerinta 9-Modificați aplicația pentru a manipula valorile RGB pentru fiecare
            vertex ce definește un triunghi.Afișați valorile RGB în consolă.*/


            if (keyboard[Key.Number1])//tasta 1 pentru vertexul 1
            {
                if (keyboard[Key.R])
                {
                    r1 = r1 + 0.03f;
                }
                if (keyboard[Key.G])
                {
                    g1 = g1 + 0.03f;

                }
                if (keyboard[Key.B])
                {
                    b1 = b1 + 0.03f;
                }
               
                Console.Write("\rVertex 1: \t r1 =" + r1.ToString() + " g1 = " + g1.ToString() + " b1 = " + b1.ToString());// afiseaza in consola valorile rgb
            }
            if (keyboard[Key.Number2])//tasta 2 pentru vertexul 2
            {
                if (keyboard[Key.R])
                {
                    r2 = r2 + 0.03f;
                }
                if (keyboard[Key.G])
                {
                    g2 = g2 + 0.03f;

                }
                if (keyboard[Key.B])
                {
                    b2 = b2 + 0.03f;
                }
                
                Console.Write("\rVertex 2: \t r2 =" + r2.ToString() + " g2 = " + g2.ToString() + " b2 = " + b2.ToString());// afiseaza in consola valorile rgb
            }
            if (keyboard[Key.Number3])//tasta 3 pentru vertexul 3
            {
                if (keyboard[Key.R])
                {
                    r3 = r3 + 0.03f;
                }
                if (keyboard[Key.G])
                {
                    g3 = g3 + 0.03f;

                }
                if (keyboard[Key.B])
                {
                    b3 = b3 + 0.03f;
                }
                
                Console.Write("\rVertex 3: \t r3 =" + r3.ToString() + " g3 = " + g3.ToString() + " b3 = " + b3.ToString());// afiseaza in consola valorile rgb
            }
            if(keyboard[Key.Space])
            {   //la apasarea space se reseteaza toate valorile inapoi la 0
                r1 = 0.0f;
                g1 = 0.0f;
                b1 = 0.0f;
                r2 = 0.0f;
                g2 = 0.0f;
                b2 = 0.0f;
                r3 = 0.0f;
                g3 = 0.0f;
                b3 = 0.0f;
                
            }
            
            
           

            if ((y_curent <=Height/2) && mouse[MouseButton.Left]) //misca camera in sus cand este apasat click stanga in partea de sus a ferestrei
            {
                
                position += up * speed; //Up

            }
            if ((y_curent >= Height/2) && mouse[MouseButton.Left]) //misca camera in jos cand este apasat click stanga in partea de jos a ferestrei
            {
                
                position -= up * speed; //Down

            }


        }
        
       
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.Color3(0.7f, 0.0f, 1.0f);

            
            GL.Begin(BeginMode.Triangles);
            GL.Color3(r1, g1, b1);

            GL.Vertex3(0F, 1F, 0);

            GL.Color3(r2, g2, b2);

            GL.Vertex3(1, 0f, 0);

            GL.Color3(r3, g3, b3);

            GL.Vertex3(0, 0, 1);
            GL.End();

            DrawAxes();
            
            


            // Se lucrează în modul DOUBLE BUFFERED - câtă vreme se afișează o imagine randată, o alta se randează în background apoi cele 2 sunt schimbate...
            SwapBuffers();
        }
       
        private void DrawAxes()
        {
            //Cerinta 3- efectul rulării comenzii GL.LineWidth(float)
            GL.LineWidth(3.0f);

            // Desenează axa Ox (cu roșu).
            //Cerinta 1-Desenați axele de coordonate din aplicația-
            //template folosind un singur apel GL.Begin().
            GL.Begin(PrimitiveType.Lines);
           GL.Color4(1.0f,0,0,0.9f);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(XYZ_SIZE, 0, 0);
            
            //GL.End();

            // Desenează axa Oy (cu galben).
           // GL.Begin(PrimitiveType.Lines);
            GL.Color3(1.0f,0.8f,0);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, XYZ_SIZE, 0); ;
           // GL.End();

            // Desenează axa Oz (cu verde).
           // GL.Begin(PrimitiveType.Lines);
            GL.Color3(0,1.0f,0);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, XYZ_SIZE);
           GL.End();
           
        }
       

        private void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(0.75f,0.75f,075f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(0.95f,1.0f,0.94f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(1.0f,0.89f,0.7f);

            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(0.8f,0.36f,0.36f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(0.85f,0.43f,0.57f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(0.13f,0.54f,0.13f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();
        }
        private void DrawObjects()
        {



        }


        static void Main(string[] args)
        {
            using (ImmediateMode example = new ImmediateMode())
            {
                example.Run(30.0, 0.0);
            }
            
            
        }
    }
}
