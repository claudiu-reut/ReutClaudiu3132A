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
        private readonly Camera3DIsometric cam;
        private readonly Grid grid;
        private readonly MassiveObject obj;
        int r1 = 0, g1 =0, b1 = 0;
        int r2 = 0 ,g2 = 0, b2 = 0;
        int r3 = 0, g3 = 0, b3 = 0;
        private float step = 0.001f;
        private float attStep = 25f;
        // int x = 30, y = 30, z = 30;
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
            cam = new Camera3DIsometric();
            grid = new Grid();
            obj = new MassiveObject(Color.SlateGray);
            obj.ToggleVisibility();
            cub.ToggleVisibility();
            Console.WriteLine("OpenGl versiunea: " + GL.GetString(StringName.Version));
            Title = "OpenGl versiunea: " + GL.GetString(StringName.Version) + " (mod imediat)";
            Console.WriteLine("W, A, S, D - Mutare cameră înainte, stânga, dreapta, înapoi ");
            Console.WriteLine("Q, E - Mutare cameră sus, jos");
            Console.WriteLine("Space - Vizibilitatea obiectului incarcat din fisier .obj");
            Console.WriteLine("C - Vizibilitatea cubului ");
            Console.WriteLine("Click Stânga- Inițializarea simulării gravitației");
            Console.WriteLine("R - Resetare simulare");
            Console.WriteLine("Esc - Ieșire");


           
           
           

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);





            cub.ChangeColor(rand.RandomColor());
            cub.ChangeColor2(rand.RandomColor());

            GL.ClearColor(0.692f, 0.75f, 0.707f, 0);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }
        protected override void OnResize(EventArgs e)
        {
            
           
            // set background
            

            // set viewport
            GL.Viewport(0, 0, this.Width, this.Height);

            // set perspective
            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 1024);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            // set the eye
            cam.SetCamera();



        }


        bool caderre = false;


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            KeyboardState input = Keyboard.GetState();

            if (keyboard[Key.W])
            {
                cam.MoveForward();
            }
            if (keyboard[Key.S])
            {
                cam.MoveBackward();
            }
            if (keyboard[Key.A])
            {
                cam.MoveLeft();
            }
            if (keyboard[Key.D])
            {
                cam.MoveRight();
            }
            if (keyboard[Key.Q])
            {
                cam.MoveUp();
            }
            if (keyboard[Key.E])
            {
                cam.MoveDown();
            }

            if (keyboard[Key.Space] && !previousKeyboard[Key.Space])
            {
                obj.ToggleVisibility();
            }





            if (keyboard[Key.Escape])
            {
                Exit();
            }

            if (keyboard[Key.R])
            {
               caderre = false;
                attStep = 25F;
                step = 0.001f;
            }
          
           
           
            if (keyboard[Key.C] && !previousKeyboard[Key.C])
            {
                cub.ToggleVisibility();
            }
          
          
            if (keyboard[Key.V])
            {
                cub.ChangeColor(rand.RandomColor());
                cub.ChangeColor2(rand.RandomColor());
            }
            previousKeyboard = keyboard;
            if (mouse[MouseButton.Left] )
            {
                caderre = true;
               
            }
            if(caderre==true && attStep-step > 1f)
            {
                attStep -= step;
                step = step + 0.00982f;
            }



        }

        
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            //GL.Color3(0.7f, 0.0f, 1.0f);


            grid.Draw();

            ax.Draw();


            GL.PushMatrix();
            GL.Translate(0, attStep, 0);
            cub.Draw();
           obj.Draw();
            GL.PopMatrix();
            


            // Se lucrează în modul DOUBLE BUFFERED - câtă vreme se afișează o imagine randată, o alta se randează în background apoi cele 2 sunt schimbate...
            SwapBuffers();
        }
       

       


       
        
    }
}

